@echo off
setlocal

cd /d "%~dp0"

set "APP_EXE=src\FaturaTakip.App\bin\Release\net8.0-windows\FaturaTakip.App.exe"

if exist "%APP_EXE%" (
    start "" "%APP_EXE%"
    exit /b 0
)

echo Release cikti bulunamadi. Uygulama derleniyor...
dotnet build ".\FaturaTakip.sln" -c Release
if errorlevel 1 (
    echo.
    echo Derleme basarisiz oldu.
    pause
    exit /b 1
)

if not exist "%APP_EXE%" (
    echo.
    echo Uygulama dosyasi olusmadi: %APP_EXE%
    pause
    exit /b 1
)

start "" "%APP_EXE%"
exit /b 0
