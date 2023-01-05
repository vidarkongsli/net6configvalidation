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
    [Parameter(Mandatory)]
    $subscription
)
$appName = 'net6confval'
$resourceGroup = "$appName-$environment"

$ErrorActionPreference = 'stop'

az group create --name $resourceGroup --location $location
az deployment group create -g $resourceGroup -n $deploymentName --template-file $templateFile `
    --parameters "env=$environment" --subscription $subscription -c
