[T4Scaffolding.Scaffolder(Description = "Generates Deisgn time data collection for a specifiet type")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$DesignModelName,
	[string]$DomainContextName,
	[Array]$RelatedEntities,
	[string]$Namespace,
	[string]$OutputFolder,
	[string]$DefaultNamespace,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$Namespace)
{
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$namespace = $namespace + ".DesignModel"
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
	$primaryKeyName = $foundModelType.Name+"Id";
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

$modelTypeNamePlural = [string](Get-PluralizedWord $foundModelType.Name);
if(!$modelTypeNamePlural)
{
	$modelTypeNamePlural = $foundModelType.Name + "s";
}

if(!$DesignModelName)
{
	$designModelName = "Design" + $modelTypeNamePlural
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace+"DomainContext";
}

if(!$OutputFolder)
{
	$outputFolder = "DesignModel"
}

$outputPath = $outputFolder+ "\" + $designModelName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template DesignModelTemplate `
	-Model @{ 
			ModelType = $foundModelType; 
			PrimaryKeyName = $primaryKeyName;
			DesignModelName = $designModelName;
			Namespace = $namespace;
			DefaultNamespace = $defaultNamespace;
			DomainContextName = $domainContextName; 
			ModelTypeNamePlural = $modelTypeNamePlural; 
			ExampleValue = "Hello, world!"; 
			RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added DesignModel output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force