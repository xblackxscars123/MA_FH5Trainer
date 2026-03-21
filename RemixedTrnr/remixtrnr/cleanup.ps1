$files = Get-ChildItem -Recurse -Include *.xaml,*.xaml.cs,*.cs
foreach ($f in $files) {
  $content = Get-Content $f.FullName
  if ($content -match '^(<<<<<<<|=======|>>>>>>>)') {
    $out = @()
    foreach ($line in $content) {
      if ($line -match '^(<<<<<<<|=======|>>>>>>>)') { continue }
      $out += $line
    }
    $out | Set-Content $f.FullName
    Write-Host "Cleaned $($f.FullName)"
  }
}
