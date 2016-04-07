param(
    [string]$subscriptionName = "Microsoft Azure Internal Consumption",
    [string]$azureProfileJson = "profile.json",
    [string]$resourceGroupName = "logitemprocgroup"
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

$clusterName = "logitemproc"             # HDInsight cluster name

Write-Host "Select-AzureRmSubscription -SubscriptionName $subscriptionName"
Select-AzureRmSubscription -SubscriptionName $subscriptionName


# Define the MapReduce job
$mrJobDefinition = New-AzureRmHDInsightStreamingMapReduceJobDefinition `
                        -Files "/app/mapper.exe","/app/reducer.exe" `
                        -Mapper "mapper.exe" `
                        -Reducer "reducer.exe" `
                        -InputPath "/workflowlog/workflow_report.txt" `
                        -OutputPath "/workflowlog/output.txt"  

# Submit the job and wait for job completion
$cred = Get-Credential -Message "Enter the HDInsight cluster HTTP user credential:" 

Write-Host "Start-AzureRmHDInsightJob"
$mrJob = Start-AzureRmHDInsightJob `
                    -ResourceGroupName $resourceGroupName `
                    -ClusterName $clusterName `
                    -HttpCredential $cred `
                    -JobDefinition $mrJobDefinition 

Write-Host "Wait-AzureRmHDInsightJob Starts..."
Wait-AzureRmHDInsightJob `
    -ResourceGroupName $resourceGroupName `
    -ClusterName $clusterName `
    -HttpCredential $cred `
    -JobId $mrJob.JobId 

Write-Host "Wait-AzureRmHDInsightJob Finishes."

# Get the job output
$cluster = Get-AzureRmHDInsightCluster -ResourceGroupName $resourceGroupName -ClusterName $clusterName
$defaultStorageAccount = $cluster.DefaultStorageAccount -replace '.blob.core.windows.net'
$defaultStorageAccountKey = Get-AzureRmStorageAccountKey -ResourceGroupName $resourceGroupName -Name $defaultStorageAccount |  %{ $_.Key1 }
$defaultStorageContainer = $cluster.DefaultStorageContainer

Write-Host "Get-AzureRmHDInsightJobOutput"
Get-AzureRmHDInsightJobOutput `
    -ResourceGroupName $resourceGroupName `
    -ClusterName $clusterName `
    -HttpCredential $cred `
    -DefaultStorageAccountName $defaultStorageAccount `
    -DefaultStorageAccountKey $defaultStorageAccountKey `
    -DefaultContainer $defaultStorageContainer  `
    -JobId $mrJob.JobId `
    -DisplayOutputType StandardError

# Download the job output to the workstation
$storageContext = New-AzureStorageContext -StorageAccountName $defaultStorageAccount -StorageAccountKey $defaultStorageAccountKey 
Get-AzureStorageBlobContent -Container $defaultStorageContainer -Blob example/data/WordCountOutput/part-r-00000 -Context $storageContext -Force

# Display the output file
cat ./example/data/StreamingOutput/part-r-00000 | findstr "there"