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

# HDInsight cluster name
$defaultContainer = $hdinsightClusterName

Write-Host "Select-AzureRmSubscription -SubscriptionName $subscriptionName"
Select-AzureRmSubscription -SubscriptionName $subscriptionName

$cluster = Get-AzureRmHDInsightCluster -ResourceGroupName $resourceGroupName -ClusterName $hdinsightClusterName
$defaultStorageAccount = $cluster.DefaultStorageAccount -replace '.blob.core.windows.net'
$defaultStorageAccountKey = Get-AzureRmStorageAccountKey -ResourceGroupName $resourceGroupName -Name $defaultStorageAccount |  %{ $_.Key1 }
$defaultStorageContainer = $cluster.DefaultStorageContainer

$ctx = New-AzureStorageContext -StorageAccountName $defaultStorageAccount -StorageAccountKey $defaultStorageAccountKey

$blobs = Get-AzureStorageBlob -Blob output* -Container $defaultContainer -Context $ctx -ErrorAction SilentlyContinue 

if($blobs)
{
    $blobs | % {Remove-AzureStorageBlob -Blob $_.Name -Container $defaultContainer -Context $ctx}
}

# Define the MapReduce job
$mrJobDefinition = New-AzureRmHDInsightStreamingMapReduceJobDefinition `
                        -Files "/Mapper.exe","/Reducer.exe" `
                        -Mapper "Mapper.exe" `
                        -Reducer "Reducer.exe" `
                        -InputPath "/workflow_report_m.txt" `
                        -OutputPath "/output" `

# Submit the job and wait for job completion
$user = "admin"
$pwd = ConvertTo-SecureString –String "Password01!" –AsPlainText -Force
$cred = New-Object –TypeName System.Management.Automation.PSCredential –ArgumentList $user, $pwd


Write-Host "Start-AzureRmHDInsightJob"
Measure-Command -Expression {
$mrJob = Start-AzureRmHDInsightJob `
                    -ResourceGroupName $resourceGroupName `
                    -ClusterName $hdinsightClusterName `
                    -HttpCredential $cred `
                    -JobDefinition $mrJobDefinition 
Write-Host "Wait-AzureRmHDInsightJob Starts..."

Wait-AzureRmHDInsightJob `
    -ResourceGroupName $resourceGroupName `
    -ClusterName $clusterName `
    -HttpCredential $cred `
    -JobId $mrJob.JobId 
}

Write-Host "Wait-AzureRmHDInsightJob Finishes."