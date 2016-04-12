param(
    [string]$subscriptionName = "Microsoft Azure Internal Consumption",
    [string]$azureProfileJson = "profile.json",
    [string]$resourceGroupName = "rpprocessorgroup",
    [string]$hdinsightClusterName = "reportprocessor"
)

# Main
$errorActionPreference = 'Stop'

$azureEnv = Get-Module Azure
if (!$azureEnv)
{
    Write-Host "Import-Module Azure"
    Import-Module Azure
}

$azureRMEnv = Get-Module AzureRM
if (!$azureRMEnv)   
{
    Write-Host "Import-Module AzureRM"
    Import-Module AzureRM
}

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$scriptParentPath = (Get-Item $scriptPath).Parent.FullName
$azureProfileJsonPath = Join-Path $scriptPath $azureProfileJson

Write-Host "Select-AzureRmProfile -Path $azureProfileJsonPath"
Select-AzureRmProfile -Path $azureProfileJsonPath

Write-Host "Select-AzureRmSubscription -SubscriptionName $subscriptionName"
Select-AzureRmSubscription -SubscriptionName $subscriptionName

# Cluster confi info
$rpProcClusterName = $hdinsightClusterName
$rpProcClusterLocation = "East Asia"
$rpProcNumClusterNodes = 4
$rpProcHttpUserName = "admin"
$rpProcHttpPwd = "Password01!"

$rpProcSshUserName = "sshuser"
$rpProcSshPassword = "Password01!"

$rpProcHttpCreds = New-Object System.Management.Automation.PSCredential ($rpProcHttpUserName, (ConvertTo-SecureString $rpProcHttpPwd -AsPlainText -Force))

$rpProcSshCredential = New-Object System.Management.Automation.PSCredential($rpProcSshUserName,(ConvertTo-SecureString $rpProcSshPassword -AsPlainText -Force))

$resourceGroupName = $resourceGroupName

# Resource group
$grp = Get-AzureRmResourceGroup -Name $resourceGroupName -Location $rpProcClusterLocation -ErrorAction SilentlyContinue
if(!$grp)
{
    New-AzureRMResourceGroup -Name $resourceGroupName -Location $rpProcClusterLocation
}

# Primary storage account info
$storageAccountName = "rprocessorstorage"
$storageAccountKeyHt = Get-AzureRmStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName -ErrorAction SilentlyContinue
if(!$storageAccountKeyHt)
{
    New-AzureRmStorageAccount -ResourceGroupName $resourceGroupName -Name $storageAccountName -Type "Standard_GRS" -Location $rpProcClusterLocation
    $storageAccountKey = Get-AzureRmStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName | % {$_.Key1}
}
else
{
    $storageAccountKey = $storageAccountKeyHt.Key1
}

$storageContext = New-AzureStorageContext `
                     -StorageAccountName $storageAccountName `
                     -StorageAccountKey $storageAccountKey

$storageBlobContainer = $rpProcClusterName
$container = Get-AzureStorageContainer -Name $storageBlobContainer -Context $storageContext -ErrorAction SilentlyContinue
if(!$container)
{
    New-AzureStorageContainer -Name $storageBlobContainer -Context $storageContext
}

# mapred-site.xml configuration
$mapRedConfig = @{ "mapreduce.task.timeout"="1200000"; "mapreduce.map.memory.mb"="8192"; "mapreduce.reduce.memory.mb" = "8192"; "mapreduce.map.java.opts" = "-Xmx6554m"; "mapreduce.reduce.java.opts" = "-Xmx6554m"}

$config = New-AzureRmHDInsightClusterConfig `
    | Set-AzureRmHDInsightDefaultStorage `
         -StorageAccountName "$storageAccountName.blob.core.windows.net" `
         -StorageAccountKey $storageAccountKey `
    | Add-AzureRmHDInsightConfigValues `
         -MapRed $mapRedConfig 

# Create the cluster
New-AzureRmHDInsightCluster `
    -ResourceGroupName $resourceGroupName `
    -ClusterName $rpProcClusterName `
    -Location $rpProcClusterLocation `
    -ClusterSizeInNodes $rpProcNumClusterNodes `
    -ClusterType Hadoop `
    -OSType Linux `
    -Version "3.4" `
    -HttpCredential $rpProcHttpCreds `
    -SshCredential $rpProcSshCredential `
    -Config $config 

# Verification
Get-AzureRmHDInsightCluster -ClusterName $rpProcClusterName