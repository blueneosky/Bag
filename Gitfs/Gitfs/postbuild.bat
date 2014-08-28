@echo off

set pb_configurationName=%1
set pb_binPath=%2

if not %pb_configurationName% == "Deploy" (goto skipDeploy)
    set pb_deployDir="%USERPROFILE%\bin\"
    
	echo Deploy to %pb_deployDir%
	xcopy /Y %pb_binPath% %pb_deployDir% > nul 2>&1
	if %errorlevel% == 0 goto skipError
	    echo Erreur durant la copy
		exit /B 1
	:skipError
:skipDeploy
