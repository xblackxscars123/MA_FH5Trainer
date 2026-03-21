$files = Get-ChildItem -Recurse -Include *.xaml
foreach ($f in $files) {
  $lines = Get-Content $f.FullName
  if ($lines.Count -eq 0) { continue }
  $first = $lines[0].Trim()
  if ($first -like '<*') {
    $rootPrefix = $first -replace '^<([^\s>]+).*', '$1'
    $out = @($lines[0])
    for ($i = 1; $i -lt $lines.Count; $i++) {
      $line = $lines[$i]
      $trim = $line.Trim()
      if ($trim -like "<$rootPrefix*" -and $trim -notlike '*xmlns*' -and $trim -notlike '</*' -and $trim -notlike '*?>*') {
        continue
      }
      $out += $line
    }
    $out | Set-Content $f.FullName
    Write-Host "Cleaned root duplicates in $($f.FullName)"
  }
}
