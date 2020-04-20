param(
 [Parameter(Position=0)]
[string]$path ='1',
 [Parameter(Position=1)]
[string]$parameter = '2'
)
#$params = (Get-Command "$PSScriptRoot\..\firstfolder\firstscript.ps1").Parameters
$params = (Get-Command $path).Parameters
return $params.ContainsKey($parameter)