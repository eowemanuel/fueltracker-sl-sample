[T4Scaffolding.Scaffolder(Description = "Generates RiaServices Entity Metadata")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$MetadataClassName,
	[string]$DefaultNamespace,
	[string]$Project,
	[string]$OutputFolder,
	[string]$CodeLanguage,
	[Array]$RelatedEntities,
	[switch]$IncludeRelatedEntities = $false,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$DefaultNamespace)
{
	$DefaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
}

if ($ModelType)
{
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

if($IncludeRelatedEntities)
{
	if ($foundModelType) 
	{ 
		if(!$relatedEntities)
		{
			$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) 
		}
	}
}
if (!$relatedEntities) { $relatedEntities = @() }

if(!$MetadataClassName)
{
	$MetadataClassName = $foundModelType.Name+"Metadata"
}

if(!$OutputFolder)
{
	$outputFolder = "Models"
}

$outputPath = $outputFolder+ "\"+$MetadataClassName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template EntityMetadataTemplate `
	-Model @{ 
	ModelType = $foundModelType; 
	MetadataClassName = $MetadataClassName; 
	Namespace = $DefaultNamespace; 
	RelatedEntities = $relatedEntities
	} `
	-SuccessMessage "Added EntityMetadata output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force