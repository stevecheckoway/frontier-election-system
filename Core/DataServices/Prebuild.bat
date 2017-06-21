@echo off
rem Prebuild for Activator Plugin project.
rem Parameters: %1 = $(SolutionDir), %2 = $(ConfigurationName)
cd %1

set solutionItemsDir=SolutionItems

echo Cleaning %solutionItemsDir% folder...
echo.
if not exist %solutionItemsDir%\nul md %solutionItemsDir%
del /Q %solutionItemsDir%\*.*

echo Copying items to %solutionItemsDir% folder...
echo.
xcopy ..\bin\%2\Core.dll %solutionItemsDir% /R /Y /E /H