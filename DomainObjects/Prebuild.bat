@echo off
rem Prebuild for Activator Plugin project.
rem Parameters: %1 = $(SolutionDir), %2 = $(ConfigurationName)
cd %1
if not exist SolutionItems\nul md SolutionItems

rem -- Enterprise Library for Validation
xcopy ..\3rdPartyDlls\MS\EntLib4\Microsoft.Practices.EnterpriseLibrary.Common.dll SolutionItems /R /Y
xcopy ..\3rdPartyDlls\MS\EntLib4\Microsoft.Practices.EnterpriseLibrary.Validation.dll SolutionItems /R /Y
xcopy ..\3rdPartyDlls\MS\EntLib4\Microsoft.Practices.ObjectBuilder2.dll SolutionItems /R /Y
