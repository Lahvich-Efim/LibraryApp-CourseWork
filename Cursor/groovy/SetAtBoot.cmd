@echo off
call :isAdmin

set RUNDIR=%~dp0
set KEY=HKU\.DEFAULT\Control Panel\Cursors
REG export "%KEY%" "%RUNDIR:~0,-1%\DefaultCursors.reg"
REG add "%KEY%" /v "AppStarting" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\start.ani" /f
REG add "%KEY%" /v "Arrow" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\arrow.cur" /f
REG add "%KEY%" /v "Crosshair" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\cross.cur" /f
REG add "%KEY%" /v "Hand" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\hand.cur" /f
REG add "%KEY%" /v "Help" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\help.cur" /f
REG add "%KEY%" /v "IBeam" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\text.cur" /f
REG add "%KEY%" /v "No" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\no.cur" /f
REG add "%KEY%" /v "NWPen" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\pen.cur" /f
REG add "%KEY%" /v "SizeAll" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\move.cur" /f
REG add "%KEY%" /v "SizeNESW" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\diag2.cur" /f
REG add "%KEY%" /v "SizeNS" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\vert.cur" /f
REG add "%KEY%" /v "SizeNWSE" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\diag1.cur" /f
REG add "%KEY%" /v "SizeWE" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\hori.cur" /f
REG add "%KEY%" /v "UpArrow" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\select.cur" /f
REG add "%KEY%" /v "Wait" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\wait.ani" /f
REG add "%KEY%" /v "Pin" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\pin.cur" /f
REG add "%KEY%" /v "Person" /t REG_EXPAND_SZ /d "%%SYSTEMROOT%%\Cursors\Groovy\person.cur" /f
exit

:isAdmin
REG query "HKU\S-1-5-19\Environment" >nul
if not %ERRORLEVEL% equ 0 (
	cls & echo Please run with administrator rights.
	timeout /t 3 >nul
	exit
)
goto :EOF
