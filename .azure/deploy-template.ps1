param (
    [Parameter(Mandatory)]
    [ValidatePattern('^[-\w\._\(\)]+$')]
    $deploymentName,
    [Parameter(Mandatory)]
    [ValidateSet('prod', 'test')]
    $environment,
    [Parameter(Mandatory = $false)]
    $location = 'norwayeast',
    [Parameter(Mandatory = $false)]
    [ValidateScript({ Test-Path $_ -PathType Leaf })]
    $templateFile = "$PSScriptRoot\deployment.bicep",
    [Parameter(Mandatory = $false)]
    $subscription = '0e39107c-468f-41a4-a1ce-231f97cf4944'
)
$appName = 'net6confval'
$resourceGroup = "$appName-$environment"

$ErrorActionPreference = 'stop'

az group create --name $resourceGroup --location $location
az deployment group create -g $resourceGroup -n $deploymentName --template-file $templateFile `
    --parameters "env=$environment" --subscription $subscription -c
