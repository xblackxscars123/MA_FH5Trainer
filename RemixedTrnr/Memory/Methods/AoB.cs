using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memory.Types;
using static Memory.Imps;

namespace Memory;

public partial class Mem
{
    private const int DefaultChunkSize = 4 * 1024 * 1024;

    public Task<IEnumerable<nuint>> AoBScan(long start, long end, string search, bool writable = false, bool executable = true, bool mapped = false, int resultLimit = -1)
    {
        return AoBScan(start, end, search, false, writable, executable, mapped, resultLimit);
    }

    public unsafe Task<IEnumerable<nuint>> AoBScan(long start, long end, string search, bool readable, bool writable, bool executable, bool mapped, int resultLimit = -1)
    {
        return Task.Run(() =>
        {
            search = Utils.NormalizeSignature(search);
            var pattern = Utils.ParseSig(search, out var mask);
            if (pattern.Length == 0)
            {
                return Enumerable.Empty<nuint>();
            }

            var memoryRegions = GetEligibleMemoryRegions(start, end, readable, writable, executable, mapped);
            List<nuint> results = [];
            
            foreach (var region in memoryRegions)
            {
                var regionResults = ScanRegion(region, pattern, mask, resultLimit > 0 ? resultLimit - results.Count : -1);
                foreach (var result in regionResults)
                {
                    results.Add(result);
                    if (resultLimit > 0 && results.Count >= resultLimit)
                    {
                        return results.AsEnumerable();
                    }
                }
            }

            return results.AsEnumerable();
        });
    }

    private unsafe List<nuint> ScanRegion(MemoryRegion region, ReadOnlySpan<byte> pattern, ReadOnlySpan<byte> mask, int resultLimit)
    {
        List<nuint> matches = [];
        var overlap = Math.Max(pattern.Length - 1, 0);
        var maxChunkSize = Math.Max(DefaultChunkSize, pattern.Length);
        var offset = 0L;

        while (offset < region.Size)
        {
            var remaining = region.Size - offset;
            var bytesToRead = (int)Math.Min(maxChunkSize, remaining);
            var buffer = GC.AllocateUninitializedArray<byte>(bytesToRead);
            ulong bytesRead = 0;

            fixed (byte* bufferPtr = buffer)
            {
                ReadProcessMemory(MProc.Handle, (nuint)((long)region.BaseAddress + offset), bufferPtr, (nuint)bytesToRead, &bytesRead);
            }

            if (bytesRead > 0)
            {
                var sliceLength = (int)Math.Min((ulong)buffer.Length, bytesRead);
                foreach (var matchOffset in Utils.FindPatternOffsets(buffer.AsSpan(0, sliceLength), pattern, mask, resultLimit))
                {
                    matches.Add((nuint)((long)region.BaseAddress + offset + matchOffset));
                    if (resultLimit > 0)
                    {
                        resultLimit--;
                        if (resultLimit == 0)
                        {
                            return matches;
                        }
                    }
                }
            }

            if (bytesToRead == remaining)
            {
                return matches;
            }

            var advance = Math.Max(bytesToRead - overlap, 1);
            offset += advance;
        }

        return matches;
    }
    
    private struct MemoryRegion
    {
        public IntPtr BaseAddress;
        public long Size;
    }

    private List<MemoryRegion> GetEligibleMemoryRegions(long start, long end, bool readable, bool writable, bool executable, bool mapped)
    {
        List<MemoryRegion> memoryRegions = [];
        GetSystemInfo(out var sysInfo);

        var minAddress = (long)sysInfo.MinimumApplicationAddress;
        var maxAddress = (long)sysInfo.MaximumApplicationAddress;

        start = Math.Max(start, minAddress);
        end = Math.Min(end, maxAddress);

        var currentAddress = (IntPtr)start;

        while (VirtualQueryEx(MProc.Handle, (nuint)currentAddress, out var memInfo) != 0 &&
               currentAddress.ToInt64() < end &&
               currentAddress.ToInt64() + memInfo.RegionSize > currentAddress.ToInt64())
        {
            var isSupportedType = memInfo.Type == MemPrivate || memInfo.Type == MemImage || (mapped && memInfo.Type == MemMapped);
            if (memInfo.State != MemCommit ||
                (long)memInfo.BaseAddress >= maxAddress ||
                (memInfo.Protect & Guard) != 0 ||
                (memInfo.Protect & NoAccess) != 0 ||
                !isSupportedType)
            {
                currentAddress = IntPtr.Add((IntPtr)memInfo.BaseAddress, (int)memInfo.RegionSize);
                continue;
            }

            var isReadable = (memInfo.Protect & ReadOnly) > 0;
            var isWritable = (memInfo.Protect & ReadWrite) > 0 ||
                             (memInfo.Protect & WriteCopy) > 0 ||
                             (memInfo.Protect & ExecuteReadwrite) > 0 ||
                             (memInfo.Protect & ExecuteWriteCopy) > 0;
            var isExecutable = (memInfo.Protect & Execute) > 0 ||
                               (memInfo.Protect & ExecuteRead) > 0 ||
                               (memInfo.Protect & ExecuteReadwrite) > 0 ||
                               (memInfo.Protect & ExecuteWriteCopy) > 0;

            isReadable = readable && isReadable;
            isWritable = writable && isWritable;
            isExecutable = executable && isExecutable;

            if (!(isReadable || isWritable || isExecutable))
            {
                currentAddress = IntPtr.Add((IntPtr)memInfo.BaseAddress, (int)memInfo.RegionSize);
                continue;
            }

            if (memoryRegions.Count > 0)
            {
                var lastRegion = memoryRegions[^1];
                if (lastRegion.BaseAddress.ToInt64() + lastRegion.Size == (long)memInfo.BaseAddress)
                {
                    memoryRegions[^1] = lastRegion with { Size = lastRegion.Size + memInfo.RegionSize };
                }
                else
                {
                    memoryRegions.Add(new MemoryRegion
                    {
                        BaseAddress = (IntPtr)memInfo.BaseAddress,
                        Size = memInfo.RegionSize
                    });
                }
            }
            else
            {
                memoryRegions.Add(new MemoryRegion
                {
                    BaseAddress = (IntPtr)memInfo.BaseAddress,
                    Size = memInfo.RegionSize
                });
            }

            currentAddress = IntPtr.Add((IntPtr)memInfo.BaseAddress, (int)memInfo.RegionSize);
        }

        return memoryRegions;
    }
    
}