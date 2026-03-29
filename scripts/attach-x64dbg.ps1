param(
    [Parameter(Mandatory = $true)]
    [string]$X64DbgPath,

    [Parameter(Mandatory = $false)]
    [string]$ProcessName = "xpaint",

    [Parameter(Mandatory = $false)]
    [int]$WaitSeconds = 0
)

if (-not (Test-Path $X64DbgPath)) {
    Write-Error "x64dbg not found at: $X64DbgPath"
    exit 1
}

$proc = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue | Select-Object -First 1
if ($null -eq $proc -and $WaitSeconds -gt 0) {
    $deadline = (Get-Date).AddSeconds($WaitSeconds)
    while ($null -eq $proc -and (Get-Date) -lt $deadline) {
        Start-Sleep -Milliseconds 250
        $proc = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue | Select-Object -First 1
    }
}

if ($null -eq $proc) {
    Write-Error "No running process named '$ProcessName' found."
    exit 1
}

# x64dbg supports -p PID attach.
Start-Process -FilePath $X64DbgPath -ArgumentList ("-p {0}" -f $proc.Id)
Write-Host "Launching x64dbg attach to PID $($proc.Id) ($ProcessName)."
