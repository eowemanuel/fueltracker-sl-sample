[T4Scaffolding.Scaffolder(Description = "Generates View that Lists entities of a given type using DomainCollectionView")][CmdletBinding()]
param(        
	[string]$ModelType,
	[string]$PrimaryKeyName,
	[string]$ViewName,
	[string]$DomainContextName,
	[string]$ViewNamespace,
	[string]$ViewModelName,
	[switch]$NoChildRelatedEntities = $false,
	[string]$DefaultNamespace,
	[string]$OutputFolder,
	[string]$Area,
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$startTimeOverall = Get-Date 
$startTime = Get-Date

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
	$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) 
}

$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "Preparation time: " + $duration

# base classses
$startTime = Get-Date
Scaffold DesignModel -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName
Scaffold ClientDataService -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities  -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName
Scaffold ClientDesignDataService -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName
Scaffold DesignCollectionViewLoader -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName
Scaffold Messages -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -Force:$Force


$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "- Client Models and Service classes generation time: " + $duration

# List ModelType Entities
$startTime = Get-Date
Scaffold ViewModelListWithDCV -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName
Scaffold ViewListWithDCV -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName

$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "- Listing entities classes generation time: " + $duration

# Edit ModelType Entities
$startTime = Get-Date

Scaffold ViewModelEdit -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName -NoChildRelatedEntities:$NoChildRelatedEntities
Scaffold ViewEdit -ModelType $ModelType -PrimaryKeyName $PrimaryKeyName -RelatedEntities $RelatedEntities -Force:$Force -DefaultNamespace $DefaultNamespace -DomainContextName $DomainContextName -NoChildRelatedEntities:$NoChildRelatedEntities

$endTime = Get-Date
$duration = $endTime - $startTime
Write-Host "- Edit entity classes generation time: " + $duration

$endTimeOverall = Get-Date
$durationOverall = $endTimeOverall - $startTimeOverall
Write-Host "--- Overall MVVM genration: " + $durationOverall
