[CmdletBinding()]
param(
    [string]$Root,
    [string[]]$Path
)

$ErrorActionPreference = 'Stop'

$Root = if ([string]::IsNullOrWhiteSpace($Root)) {
    Split-Path -Parent $PSScriptRoot
}
else {
    $Root
}

$rootPath = (Resolve-Path -LiteralPath $Root).Path
$utf8 = [System.Text.UTF8Encoding]::new($false, $true)
$allowedExtensions = @('.md', '.cs', '.xaml', '.ps1', '.bat', '.yml', '.yaml')
$suspiciousCodePoints = [System.Collections.Generic.HashSet[int]]::new(
    [int[]]@(0x00C3, 0x00C4, 0x00C5, 0x00E2, 0x00F0, 0xFFFD)
)

if (-not $Path) {
    $Path = @(
        'README.md',
        'ROADMAP.md',
        'REGRESYON.md',
        'docs/00-codex-devam-kilavuzu.md',
        'docs/03-devam-notlari.md',
        'docs/06-guncel-durum-ozeti.md',
        'docs/07-review-context-ui-smoke-checklist.md',
        'docs/08-replay-indicator-ui-smoke-checklist.md',
        'EncodingKontrol.bat',
        'HizliDogrulama.bat',
        '.github/workflows',
        'tools/Test-TextEncoding.ps1',
        'src/FaturaTakip.App'
    )
}

$files = foreach ($item in $Path) {
    $candidate = if ([System.IO.Path]::IsPathRooted($item)) {
        $item
    }
    else {
        Join-Path $rootPath $item
    }

    if (-not (Test-Path -LiteralPath $candidate)) {
        throw "Encoding kontrol yolu bulunamadi: $candidate"
    }

    $resolved = (Resolve-Path -LiteralPath $candidate).Path
    if ((Get-Item -LiteralPath $resolved).PSIsContainer) {
        Get-ChildItem -LiteralPath $resolved -Recurse -File |
            Where-Object {
                $_.FullName -notmatch '[\\/](bin|obj)[\\/]' -and
                $_.Extension -in $allowedExtensions
            }
    }
    else {
        Get-Item -LiteralPath $resolved
    }
}

$issues = [System.Collections.Generic.List[object]]::new()

foreach ($file in $files | Sort-Object FullName -Unique) {
    try {
        $text = $utf8.GetString([System.IO.File]::ReadAllBytes($file.FullName))
    }
    catch {
        $issues.Add([pscustomobject]@{
            File = $file.FullName
            Line = 0
            Detail = 'Gecersiz UTF-8 byte dizisi'
        })
        continue
    }

    $line = 1
    foreach ($character in $text.ToCharArray()) {
        $codePoint = [int]$character
        $isForbiddenControl = $codePoint -lt 0x20 -and $codePoint -notin 0x09, 0x0A, 0x0D
        if ($suspiciousCodePoints.Contains($codePoint) -or $isForbiddenControl) {
            $issues.Add([pscustomobject]@{
                File = $file.FullName
                Line = $line
                Detail = ('Supheli karakter U+{0:X4}' -f $codePoint)
            })
        }

        if ($codePoint -eq 0x0A) {
            $line++
        }
    }
}

if ($issues.Count -gt 0) {
    $issues | ForEach-Object {
        Write-Error ('{0}:{1}: {2}' -f $_.File, $_.Line, $_.Detail) -ErrorAction Continue
    }
    exit 1
}

Write-Host ("Encoding kontrolu temiz: {0} dosya." -f ($files | Sort-Object FullName -Unique).Count)
exit 0
