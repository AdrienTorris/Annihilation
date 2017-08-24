cd /d %~dp0
setlocal ENABLEDELAYEDEXPANSION

SET _Origin=%1
SET _result=%_Origin:Resources=Compiled%

echo %_Origin%
echo %_result%

texgenpack --compress --format rgb8 --progress %_Origin% %_result%.ktx