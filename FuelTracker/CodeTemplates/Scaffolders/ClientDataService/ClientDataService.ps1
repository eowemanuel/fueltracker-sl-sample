[T4Scaffolding.Scaffolder(Description = "Generates client DataService for data transfers with the server")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ClientDataServiceName,
	[string]$DomainContextName,
	[string]$Namespace,
	[Array]$RelatedEntities,
	[string]$DefaultNamespace,
	[string]$OutputFolder,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if(!$Namespace)
{
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$namespace = $namespace + ".Services"
}

if(!$DefaultNamespace)
{
	$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value;
}

if ($ModelType) {
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	#$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
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
}

$modelTypeNamePlural = [string](Get-PluralizedWord $foundModelType.Name);
if(!$modelTypeNamePlural)
{
	$modelTypeNamePlural = $foundModelType.Name + "s";
}

if(!$ClientDataServiceName)
{
	$clientDataServiceName = $foundModelType.Name+"DataService"
}

if(!$DomainContextName)
{
	$domainContextName = $defaultNamespace + "DomainContext";
}

if(!$OutputFolder)
{
	$outputFolder = "Services"
}

$outputPath = $outputFolder+ "\" + $clientDataServiceName  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive

Add-ProjectItemViaTemplate $outputPath -Template ClientDataServiceTemplate `
	-Model @{ 
		ModelType = $foundModelType; 
		PrimaryKeyName = $primaryKeyName;
		ClientDataServiceName = $clientDataServiceName; 
		Namespace = $namespace; 
		DefaultNamespace = $defaultNamespace;
		DomainContextName = $domainContextName; 
		ModelTypeNamePlural = $modelTypeNamePlural; 
		ExampleValue = "Hello, world!"; 
		RelatedEntities = $relatedEntities
		} `
	-SuccessMessage "Added ClientDataService output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$clientDataServiceInterfaceName = "I"+$clientDataServiceName

Write-Host "TO DO:" -ForegroundColor DarkBlue -BackgroundColor Gray
Write-Host "Add to ServiceProviderBase" -ForegroundColor DarkBlue -BackgroundColor Gray
Write-Host "public virtual " $clientDataServiceInterfaceName $clientDataServiceName " { get; protected set; }" -ForegroundColor DarkGreen -BackgroundColor Gray
Write-Host "" -ForegroundColor DarkGreen -BackgroundColor Gray
Write-Host "Add to ServiceProvider" -ForegroundColor DarkBlue -BackgroundColor Gray
Write-Host "    public override " $clientDataServiceInterfaceName " " $clientDataServiceName -ForegroundColor DarkGreen -BackgroundColor Gray
Write-Host "    {" -ForegroundColor DarkGreen -BackgroundColor Gray
Write-Host "        get { return new " $clientDataServiceName "(); }" -ForegroundColor DarkGreen -BackgroundColor Gray
Write-Host "    }" -ForegroundColor DarkGreen -BackgroundColor Gray