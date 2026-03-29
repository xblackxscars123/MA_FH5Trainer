param(
    [Parameter(Mandatory = $true)]
    [string]$X64DbgPath,

    [Parameter(Mandatory = $true)]
    [string]$TargetExe
)

if (-not (Test-Path $X64DbgPath)) {
    Write-Error "x64dbg not found at: $X64DbgPath"
    exit 1
}

if (-not (Test-Path $TargetExe)) {
    Write-Error "Target executable not found at: $TargetExe"
    exit 1
}

Start-Process -FilePath $X64DbgPath -ArgumentList ('"{0}"' -f $TargetExe)
Write-Host "Launched x64dbg with target: $TargetExe"
