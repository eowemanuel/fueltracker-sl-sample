[T4Scaffolding.Scaffolder(Description = "Generates View that Lists entities of a given type using DomainCollectionView")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ViewName,
	[string]$DomainContextName,
	[string]$ViewNamespace,
	[string]$ViewModelName,
	[Array]$RelatedEntities,
	[switch]$NoChildRelatedEntities = $false,
	[string]$DefaultNamespace,
	[string]$OutputFolder,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$startTime = Get-Date 

if(!$ViewNamespace)
{
	$viewNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$viewNamespace = $viewNamespace + ".Views"
}

if(!$DefaultNamespace)
{
	$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value;
}

if ($ModelType) {
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


[bool] $NoChildRelatedEntitiesBool = $false
if($NoChildRelatedEntities)
{
	$NoChildRelatedEntitiesBool = $true
}
else
{
	$NoChildRelatedEntitiesBool = $false
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

if(!$ViewName)
{
	$viewName = $foundModelType.Name + "EditView"
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace+"DomainContext";
}

if(!$OutputFolder)
{
	$outputFolder = "Views"
}

$outputPath = $outputFolder+ "\" + $viewName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive


#XAML
Add-ProjectItemViaTemplate $outputPath -Template ViewEditXamlTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewNamespace = $viewNamespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			ViewModelName = $ViewModelName;
			RelatedEntities = $relatedEntities;
			NoChildRelatedEntities = $NoChildRelatedEntitiesBool
			} `
	-SuccessMessage "Added View output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

#CodeBehind
Add-ProjectItemViaTemplate $outputPath -Template ViewEditCodeBehindTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewNamespace = $viewNamespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			ViewModelName = $ViewModelName;
			RelatedEntities = $relatedEntities;
			NoChildRelatedEntities = $NoChildRelatedEntitiesBool
			} `
	-SuccessMessage "Added View output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "Generation time : " + $duration