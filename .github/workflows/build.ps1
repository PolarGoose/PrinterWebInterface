Function Info($msg) {
    Write-Host -ForegroundColor DarkGreen "`nINFO: $msg`n"
}

Function Error($msg) {
    Write-Host `n`n
    Write-Error $msg
    exit 1
}

Function CheckReturnCodeOfPreviousCommand($msg) {
    if(-Not $?) {
        Error "${msg}. Error code: $LastExitCode"
    }
}

Function CreateZipArchive($dir) {
    Info "Create zip archive `n ${dir}.zip"
    Compress-Archive -Force -Path "$dir/*" -DestinationPath "${dir}.zip"
}

Function RemoveDirectory($dir) {
    Info "Remove '$outDir'"
    Remove-Item $dir -Recurse -Force -ErrorAction SilentlyContinue 2>null
}

Function GetVersion() {
    $gitCommand = Get-Command -Name git

    $nearestTag = & "$gitCommand" describe --exact-match --tags HEAD
    if(-Not $?) {
        Info "The commit is not tagged. Use 'v0.0-dev' as a version instead"
        $nearestTag = "v0.0-dev"
    }

    $commitHash = & "$gitCommand" rev-parse --short HEAD
    CheckReturnCodeOfPreviousCommand "Failed to get git commit hash"

    return "$($nearestTag.Substring(1))-$commitHash"
}

Function Publish($slnFile, $version, $outDir) {
    Info "Run 'dotnet publish' command: `n slnFile=$slnFile `n version='$version' `n outDir=$outDir"
    $Env:DOTNET_NOLOGO = "true"
    $Env:DOTNET_CLI_TELEMETRY_OPTOUT = "true"
    dotnet publish `
        --self-contained true `
        --runtime linux-x64 `
        --configuration Release `
        --output $outDir `
        /property:PublishSingleFile=true `
        /property:IncludeAllContentForSelfExtract=true `
        /property:PublishTrimmed=true `
        /property:TrimMode=link `
        /property:DebugType=None `
        /property:Version=$version `
        $slnFile
    CheckReturnCodeOfPreviousCommand "'dotnet publish' command failed"
}

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$root = Resolve-Path "$PSScriptRoot/../.."
$projectName = "PrinterWebInterface"
$outDir = "$root/Build/Publish/$projectName"
$servicedFile = "$root/tools/PrinterWebInterface.service"

RemoveDirectory $outDir
Publish `
    -slnFile $root/$projectName.sln `
    -version (GetVersion) `
    -outDir $outDir
Info "Copy `n '$servicedFile' `n to '$outDir'"
Copy-Item $servicedFile -Destination $outDir
CreateZipArchive $outDir
