@echo off
rem Prebuild for Data Management Plugin project.
rem Parameters: %1 = $(SolutionDir), %2 = $(ConfigurationName)
cd %1
if not exist SolutionItems\nul md SolutionItems
del /Q SolutionItems\*.*

rem -- copy core
xcopy ..\Core\bin\%2\Core.dll SolutionItems /R /Y
xcopy ..\Core\DataServices\bin\%2\Sequoia.EMS.Core.DataServices.dll SolutionItems /R /Y

rem -- copy domain objects
xcopy ..\Common\Utilities\Encryption\bin\%2\Sequoia.Utilities.Encryption.dll SolutionItems /R /Y