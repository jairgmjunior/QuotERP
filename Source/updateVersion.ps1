$assemblyInfo = $args[0]
Write-Host "assemblyInfo: $assemblyInfo";

$assemblyPattern = "[0-9]+(\.([0-9]+|\*)){1,3}"
$assemblyVersionPattern = 'AssemblyVersion\("([0-9]+(\.([0-9]+|\*)){1,3})"\)'

$rawVersionNumberGroup = get-content $assemblyInfo | select-string -pattern $assemblyVersionPattern | select | % { $_.Matches }            

$rawVersionNumber = $rawVersionNumberGroup.Groups[1].Value
Write-Host "rawVersionNumber: $rawVersionNumber";

$versionParts = $rawVersionNumber.Split('.')
$versionParts[2] = ([int]$versionParts[2]) + 1
$updatedAssemblyVersion = "{0}.{1}.{2}.{3}" -f $versionParts[0], $versionParts[1], $versionParts[2], $versionParts[3]
Write-Host "updatedAssemblyVersion: $updatedAssemblyVersion";

(Get-Content $assemblyInfo) | ForEach-Object { % {$_ -replace $assemblyPattern, $updatedAssemblyVersion } } | Set-Content $assemblyInfo