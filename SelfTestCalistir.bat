@echo off
setlocal

cd /d "%~dp0"

echo Self-test baslatiliyor...
echo.

dotnet run -c Release --project ".\src\FaturaTakip.App\FaturaTakip.App.csproj" -- --self-test
set "EXIT_CODE=%ERRORLEVEL%"

echo.
if "%EXIT_CODE%"=="0" (
    echo Self-test basarili tamamlandi.
) else (
    echo Self-test basarisiz oldu. Cikis kodu: %EXIT_CODE%
)

pause
exit /b %EXIT_CODE%
