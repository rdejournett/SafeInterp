@echo on
rem step 1 - namespace reservation
netsh http add urlacl url=https://+:9002/ user=EVERYONE
rem step 2 - generating the root ca
pause
makecert.exe -sk RootCA -sky signature -pe -n CN=localhost -r -sr LocalMachine -ss Root ServerCA.cer
pause
rem step 3 - create server cert

makecert.exe -sk server -sky exchange -pe -n CN=localhost -ir LocalMachine -is Root -ic ServerCA.cer -sr LocalMachine -ss My ServerTestCert.cer
pause
rem step 4 - bind cert to port

BindCertToPort.exe ServerTestCert.cer 9002 *08oate
pause
