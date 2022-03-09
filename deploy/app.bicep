param appName string
param acr string
param fileShareMountPath string
param storageAccountName string

param location string = resourceGroup().location
param appServicePlanName string = 'plan-${appName}-${uniqueString(resourceGroup().id)}'
param appServiceName string = 'app-${appName}-${uniqueString(resourceGroup().id)}'
param imageTag string = 'latest'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'P1v3'
    tier: 'PremiumV3'
    size: 'P1v3'
    family: 'Pv3'
    capacity: 1
  }
  kind: 'windows'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: true
    hyperV: true
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

resource appService 'Microsoft.Web/sites@2021-03-01' = {
  name: appServiceName
  location: location
  kind: 'app,container,windows'
  properties: {
    enabled: true
    serverFarmId: appServicePlan.id
    siteConfig: {
      numberOfWorkers: 1
      windowsFxVersion: 'DOCKER|${acr}/${appName}:latest'
      acrUseManagedIdentityCreds: false
      alwaysOn: true
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    clientCertMode: 'Required'
    httpsOnly: true
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}

resource appServiceConfig 'Microsoft.Web/sites/config@2021-03-01' = {
  parent: appService
  name: 'web'
  properties: {
    windowsFxVersion: 'DOCKER|${acr}/${appName}:${imageTag}'
    publishingUsername: '$${appName}'
    alwaysOn: true
    azureStorageAccounts: {
      containerFiles: {
        type: 'AzureFiles'
        accountName: storageAccountName
        shareName: storageAccountName
        mountPath: fileShareMountPath
      }
    }
  }
}
