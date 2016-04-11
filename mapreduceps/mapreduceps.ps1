Get-AzureRmHDInsightJobOutput `
    -Clustername $clusterName `
    -JobId $mrJob.JobId `
    -DefaultContainer $container `
    -DefaultStorageAccountName $storageAccountName `
    -DefaultStorageAccountKey $storageAccountKey `
    -HttpCredential $cred

# Print the output of the WordCount job.
Write-Host "Display the standard output ..." -ForegroundColor Green
Get-AzureRmHDInsightJobOutput `
        -Clustername $clusterName `
        -JobId $mrJob.JobId `
        -DefaultContainer $container `
        -DefaultStorageAccountName $storageAccountName `
        -DefaultStorageAccountKey $storageAccountKey `
        -HttpCredential $cred `
        -DisplayOutputType StandardError