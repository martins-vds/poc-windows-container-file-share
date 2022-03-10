targetScope = 'subscription'

param appName string
param deploymentName string
param rgName string = 'poc-${appName}-${subscription().subscriptionId}'
param location string = deployment().location

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01'={
  name: rgName
  location: location
}

module infra 'modules/infra.bicep' = {
  name: deploymentName
  scope: rg
  params:{
    appName: appName
    location: rg.location
  }
}

output rg string = rg.name
output storage string = infra.outputs.storage
output acr object = infra.outputs.acr
