[T4Scaffolding.Scaffolder(Description = "Generates simple DTO class ")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$SimpleDtoName,
	[string]$DefaultNamespace,
	[string]$Project,
	[string]$OutputFolder,
	[string]$CodeLanguage,
	[Array]$RelatedEntities,
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

if ($foundModelType) 
{ 
	if(!$relatedEntities)
	{
		$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) 
	}
}
if (!$relatedEntities) { $relatedEntities = @() }

if(!$SimpleDtoName)
{
	$simpleDtoName = $foundModelType.Name+"SimpleDto"
}

if(!$OutputFolder)
{
	$outputFolder = "Models"
}

$outputPath = $outputFolder+ "\"+$simpleDtoName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template SimpleDtoTemplate `
	-Model @{ 
	ModelType = $foundModelType; 
	SimpleDtoName = $simpleDtoName; 
	Namespace = $DefaultNamespace; 
	RelatedEntities = $relatedEntities
	} `
	-SuccessMessage "Added SimpleDto output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force