[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$UnityPath,
    [string]$ProjectPath = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$resolvedProject = (Resolve-Path $ProjectPath).Path
$manifestPath = Join-Path $resolvedProject 'Packages/manifest.json'
$lockPath = Join-Path $resolvedProject 'Packages/packages-lock.json'

if (-not (Test-Path $UnityPath -PathType Leaf)) { throw "Unity executable not found: $UnityPath" }
if (-not (Test-Path $manifestPath -PathType Leaf)) { throw "manifest.json not found: $manifestPath" }

Write-Host "Resolving packages with Unity: $UnityPath"
& $UnityPath -batchmode -nographics -quit -projectPath $resolvedProject -logFile -
if ($LASTEXITCODE -ne 0) { throw "Unity Package Manager resolution failed with exit code $LASTEXITCODE" }
if (-not (Test-Path $lockPath -PathType Leaf)) { throw "Unity did not produce packages-lock.json" }

$manifest = Get-Content $manifestPath -Raw | ConvertFrom-Json
$lock = Get-Content $lockPath -Raw | ConvertFrom-Json
$manifestNames = @($manifest.dependencies.psobject.Properties.Name)
$lockNames = @($lock.dependencies.psobject.Properties.Name)
$missing = @($manifestNames | Where-Object { $_ -notin $lockNames })
Write-Host "manifest_direct_dependencies=$($manifestNames.Count) lock_entries=$($lockNames.Count)"
if ($missing.Count -gt 0) {
    Write-Error ("Direct dependencies missing from lockfile: " + ($missing -join ', '))
    exit 2
}
Write-Host 'unity_package_resolution_ok'
