[T4Scaffolding.Scaffolder(Description = "Generates ViewModel that Lists entities of a given type using DomainCollectionView")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ViewModelName,
	[string]$DomainContextName,
	[string]$ViewModelNamespace,
	[string]$OutputFolder,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if($ViewModelNamespace)
{
	$viewModelNamespace = $ViewModelNamespace
}
else
{
	$viewModelNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$viewModelNamespace = $viewModelNamespace + ".ViewModels"
}
$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value;

if ($ModelType) {
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
}

if ($foundModelType) { $relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) }
if (!$relatedEntities) { $relatedEntities = @() }

if($PrimaryKeyName)
{
	$primaryKeyName = $PrimaryKeyName
}
else
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
	$viewModelName = $foundModelType.Name + "RelatedEntities"
}

if($DomainContextName)
{
	$domainContextName = $DomainContextName
}
else
{
	$domainContextName = $defaultNamespace+"DomainContext";
}

if($OutputFolder)
{
	$outputFolder = $OutputFolder
}
else
{
	$outputFolder = "Models"
}

$outputPath = $outputFolder+ "\" + $viewModelName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive



Add-ProjectItemViaTemplate $outputPath -Template ModelRelatedEntitiesTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewModelName = $viewModelName;
			ViewModelNamespace = $viewModelNamespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added ViewModel output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force