[T4Scaffolding.Scaffolder(Description = "Generates ViewModel that edits entities of a given type")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ViewModelName,
	[string]$DomainContextName,
	[string]$ViewModelNamespace,
	[string]$DefaultNamespace,
	[Array]$RelatedEntities,
	[switch]$NoChildRelatedEntities = $false,
	[string]$OutputFolder,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$startTime = Get-Date 

$startPowerShellCommandsTime = Get-Date 

if(!$ViewModelNamespace)
{
	$viewModelNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$viewModelNamespace = $viewModelNamespace + ".ViewModels"
}

if(!$DefaultNamespace)
{
	$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value;
}

if ($ModelType) 
{
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
}

if ($foundModelType) 
{ 
	if(!$RelatedEntities)	
	{
		$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) 
	}
}

if (!$relatedEntities) { $relatedEntities = @() }

if($NoChildRelatedEntities)
{
	$NoChildRelatedEntitiesBool = $true;
}
else
{
	$NoChildRelatedEntitiesBool = $false;
}

if(!$PrimaryKeyName)
{
	$primaryKeyName = $foundModelType.Name+"Id";
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

$modelTypeNamePlural = [string](Get-PluralizedWord $foundModelType.Name);
if(!$modelTypeNamePlural)
{
	$modelTypeNamePlural = $foundModelType.Name + "s";
}

if(!$ViewModelName)
{
	$viewModelName = $foundModelType.Name + "EditViewModel"
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace+"DomainContext";
}

if(!$OutputFolder)
{
	$outputFolder = "ViewModels"
}

$outputPath = $outputFolder+ "\" + $viewModelName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

$endPowerShellCommandsTime = Get-Date
$durationPowerShellCommands = $endPowerShellCommandsTime - $startPowerShellCommandsTime
Write-Host "PowerShell Commands execution time : " + $durationPowerShellCommands

Add-ProjectItemViaTemplate $outputPath -Template ViewModelEditTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewModelName = $viewModelName;
			ViewModelNamespace = $viewModelNamespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities ;
			NoChildRelatedEntities = $NoChildRelatedEntitiesBool
			} `
	-SuccessMessage "Added ViewModel output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "Generation execution time : " + $duration