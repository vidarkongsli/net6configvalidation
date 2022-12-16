param (
    $appName = 'net6confval-test-web',
    $rg = 'net6confval-test'
)
$ErrorActionPreference = 'stop'
$path = Resolve-Path (Join-Path -Path $PSScriptRoot -ChildPath '../src')
Push-Location (Resolve-Path $path)
try {
    dotnet publish -c Release -o temp
    $archive = Join-Path -Path (Get-Location) -ChildPath "$(Get-Date -Format 'yyyy-MM-dd_hh_mm_ssz').zip"
    Compress-Archive -Path temp/* -DestinationPath $archive
    Remove-Item -Path temp -Recurse
    az webapp deploy --type zip --src-path $archive --resource-group $rg --name $appName
    Remove-Item $archive
}
finally {
    Pop-Location
}
