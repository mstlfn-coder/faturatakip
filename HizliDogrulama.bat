@echo off
setlocal

cd /d "%~dp0"

echo [1/3] Encoding kontrolu...
call ".\EncodingKontrol.bat"
if errorlevel 1 goto :failed

echo.
echo [2/3] Release derleme...
dotnet build ".\FaturaTakip.sln" -c Release
if errorlevel 1 goto :failed

echo.
echo [3/3] Self-test...
dotnet run -c Release --no-build --project ".\src\FaturaTakip.App\FaturaTakip.App.csproj" -- --self-test
if errorlevel 1 goto :failed

echo.
echo Hizli dogrulama basarili tamamlandi.
exit /b 0

:failed
set "EXIT_CODE=%ERRORLEVEL%"
echo.
echo Hizli dogrulama basarisiz oldu. Cikis kodu: %EXIT_CODE%
exit /b %EXIT_CODE%
