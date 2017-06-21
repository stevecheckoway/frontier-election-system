@echo off
rem Prebuild for Activator Plugin project.
rem Parameters: %1 = $(SolutionDir), %2 = $(ConfigurationName)
cd %1
if not exist SolutionItems\nul md SolutionItems

rem -- copy Sequoia.Ems.Interop
xcopy ..\Interop\EmsInterop\EmsInterop\bin\%2\Sequoia.Ems.Interop.dll SolutionItems /R /Y /E /H
xcopy ..\Interop\EmsInterop\EmsImaging\bin\%2\Sequoia.Ems.Imaging.dll SolutionItems /R /Y /E /H

rem -- copy Sequoia.DomainObjects
xcopy ..\DomainObjects\DomainObjects\bin\%2\Sequoia.DomainObjects.dll SolutionItems /R /Y /E /H

rem -- copy Sequoia.Utilities
xcopy ..\Common\Utilities\Utilities\bin\%2\Sequoia.Utilities.dll SolutionItems /R /Y /E /H

rem -- copy Sequoia.Utilities
xcopy ..\Common\Utilities\EntrySet\bin\%2\Sequoia.Ems.Data.Custom.dll SolutionItems /R /Y /E /H

rem -- copy Sequoia.Core
xcopy ..\Core\bin\%2\Core.dll SolutionItems /R /Y /E /H

