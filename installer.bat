sc stop "Safe Interpretation Engine"
ping 127.0.0.1 -n 6 > nul
installutil /u InterpCheckSvc.exe
echo compile now...
pause
installutil InterpCheckSvc.exe
sc start "Safe Interpretation Engine"