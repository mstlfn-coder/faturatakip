@echo off
setlocal

cd /d "%~dp0"

powershell.exe -NoProfile -ExecutionPolicy Bypass -File ".\tools\Test-TextEncoding.ps1"
exit /b %ERRORLEVEL%
