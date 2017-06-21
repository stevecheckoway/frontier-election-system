@echo off
rem Prebuild for Utilities project.
rem Parameters: %1 = $(SolutionDir), %2 = $(ConfigurationName)
cd %1
if not exist SolutionItems\nul md SolutionItems

rem -- Copy DomainObjects
xcopy ..\..\DomainObjects\DomainObjects\bin\%2\Sequoia.DomainObjects.dll SolutionItems /R /Y

rem -- Copy core to solution items
xcopy ..\..\Core\bin\%2\Core.dll SolutionItems /R /Y
