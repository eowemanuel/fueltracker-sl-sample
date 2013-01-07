[T4Scaffolding.Scaffolder(Description = "Generates ViewModel that Lists entities of a given type using DomainCollectionView")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ViewName,
	[string]$ViewModelName,
	[string]$DomainContextName,
	[string]$Namespace,
	[Array]$RelatedEntities,
	[string]$DefaultNamespace,
	[string]$OutputFolder,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$Namespace)
{
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$namespace = $namespace + ".Views"
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

if(!$PrimaryKeyName)
{
	$primaryKeyName = $foundModelType.Name+"Id"
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

$modelTypeNamePlural = [string](Get-PluralizedWord $foundModelType.Name)
if(!$modelTypeNamePlural)
{
	$modelTypeNamePlural = $foundModelType.Name + "s"
}

if(!$ViewName)
{
	$viewName = $modelTypeNamePlural + "ListView"
}
$viewControlName = $viewName+"Control"

if(!$ViewModelName)
{
	$viewModelName = $modelTypeNamePlural + "ListViewModel"
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace+"DomainContext"
}

if(!$OutputFolder)
{
	$outputFolder = "Views"
}

#View Control Generation
$outputPath = $outputFolder+ "\" + $viewControlName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive
Add-ProjectItemViaTemplate $outputPath -Template ViewListWithDCVControlXamlTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewControlName = $viewControlName;
			ViewModelName = $viewModelName;
			Namespace = $namespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added View UserControl Xaml output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$outputPath = $outputFolder+ "\" + $viewControlName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive
Add-ProjectItemViaTemplate $outputPath -Template ViewListWithDCVControlCodeBehindTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewControlName = $viewControlName;
			ViewModelName = $viewModelName;
			Namespace = $namespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added View UserControl Code behind output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

#View Page Generation
$outputPath = $outputFolder+ "\" + $viewName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template ViewListWithDCVPageXamlTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewControlName = $viewControlName;
			ViewModelName = $viewModelName;
			Namespace = $namespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added View Page Xaml output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

Add-ProjectItemViaTemplate $outputPath -Template ViewListWithDCVPageCodeBehindTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			ViewName = $viewName;
			ViewControlName = $viewControlName;
			ViewModelName = $viewModelName;
			Namespace = $namespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added View Page Code Behind output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
