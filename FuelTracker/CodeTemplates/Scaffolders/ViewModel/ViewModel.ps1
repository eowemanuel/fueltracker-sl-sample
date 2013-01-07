[T4Scaffolding.Scaffolder(Description = "Enter a description of ViewModel here")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$ViewModelName,
	[string]$Namespace,
	[string]$Project,
	[string]$CodeLanguage,
	[string]$OutputFolder,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$Namespace)
{
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
}

if ($ModelType) {
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

if ($foundModelType) { $relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) }
if (!$relatedEntities) { $relatedEntities = @() }

if($ViewModelName)
{
	$viewModelName = $ViewModelName
}
else
{
	$viewModelName = $foundModelType.Name+"ViewModel"
}

if($OutputFolder)
{
	$outputFolder = $OutputFolder
}
else
{
	$outputFolder = "Services"
}

$outputPath = $outputFolder+ "\"+$viewModelName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template ViewModelTemplate `
	-Model @{ ModelType = $foundModelType; ViewModelName = $viewModelName; Namespace = $namespace; ExampleValue = "Hello, world!"; RelatedEntities = $relatedEntities} `
	-SuccessMessage "Added ViewModel output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force