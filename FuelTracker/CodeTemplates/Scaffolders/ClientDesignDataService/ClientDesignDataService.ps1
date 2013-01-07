[T4Scaffolding.Scaffolder(Description = "Generates DesignDataService class for DesignTime data")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ClientDesignDataServiceName,
	[string]$Namespace,
	[Array]$RelatedEntities,
	[string]$DefaultNamespace,
	[string]$OutputFolder,
	[string]$DomainContextName,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$Namespace)
{
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$namespace = $namespace + ".DesignServices"
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

if(!$ClientDesignDataServiceName)
{
	$clientDesignDataServiceName = "Design" + $foundModelType.Name + "DataService"
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace+"DomainContext";
}

if(!$OutputFolder)
{
	$outputFolder = "DesignServices"
}

$outputPath = $outputFolder+ "\"+$clientDesignDataServiceName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template ClientDesignDataServiceTemplate `
	-Model @{ 
		ModelType = $foundModelType;
		PrimaryKeyName = $primaryKeyName;
		ClientDesignDataServiceName = $clientDesignDataServiceName; 
		Namespace = $namespace; 
		DefaultNamespace = $defaultNamespace;
		DomainContextName = $domainContextName; 
		ModelTypeNamePlural = $modelTypeNamePlural; 
		ExampleValue = "Hello, world!"; 
		RelatedEntities = $relatedEntities
		} `
	-SuccessMessage "Added ClientDesignDataService output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

#TO DO Message
$clientDataServiceName = $foundModelType.Name + "DataService" # Implicit DataServiceName value
$clientDataServiceInterfaceName = "I"+$clientDataServiceName # Implicit DataServiceInterfaceName value

Write-Host "TO DO: " -ForegroundColor DarkBlue -BackgroundColor Gray
Write-Host "Add the DesignDataService property to DesignServiceProvider" -ForegroundColor DarkBlue -BackgroundColor Gray
Write-Host $clientDataServiceName " = new " $clientDesignDataServiceName "();" -ForegroundColor DarkGreen -BackgroundColor Gray