param(
    [string] $source_path = "C:\Work\tests\yaweiwfixit\mapreduceps",
    [string] $target_path = "E:\hdp-installassets\configuration",
    [string] $files_list = "configure-cluster-property.ps1,configure-cluster-properties.ps1,configure-cluster-properties.ps1,configure-cluster-properties.txt",
    [string] $nodes_list = "hn0-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net,hn1-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net,wn0-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net,wn1-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net,wn2-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net,wn3-workfl.ao1urb3wsdeezk25kzz1hat4pb.hx.internal.cloudapp.net",
    [int] $batch_size = 4,
    [string] $cmd_word = "E:\hdp-installassets\configuration\configure-cluster-properties.ps1 -working_path 'E:\hdp-installassets\configuration' -configuration_file 'configure-cluster-properties.txt'"
)
# followed by $cmd_args
 
$cmd_args = $args  # capture this right away so can use it in functions
 
function ListSplitUnique($values)
{
    return ,@((($values -Split ",") | % {$_.Trim()} | where-object {$_ -ne ""} ) | Sort-Object -Unique)
}
 
function GetRequiredNodes($nodelistpath)
{
    $required_nodes = @()
    write-host "$nodelistpath"
    if ($nodelistpath -notlike "skip") {
        if (Test-Path -Path $nodelistpath -PathType Leaf){
            # // File exists
            (get-content $nodelistpath) | foreach-object {$required_nodes += ListSplitUnique($_)}
        } else {
            # // File does not exist
            $required_nodes = ListSplitUnique($nodelistpath)
        }
    }
 
    return $required_nodes;
}
 
function GetIPAddress($hostname)
{
    try
    {
        [System.Net.Dns]::GetHostAddresses($hostname) | ForEach-Object { if ($_.AddressFamily -eq "InterNetwork") { $_.IPAddressToString } }
    }
    catch
    {
        throw "Error resolving IPAddress for host '$hostname'"
    }
}
 
function IsSameHost(
    [string] [parameter( Position=0, Mandatory=$true )] $host1,
    [array] [parameter( Position=1, Mandatory=$false )] $host2ips = ((GetIPAddress $env:COMPUTERNAME) -as [array]))
{
    $host1ips = ((GetIPAddress $host1) -as [array])
    $heq = Compare-Object $host1ips $host2ips -ExcludeDifferent -IncludeEqual
    return ($heq -ne $null)
}
 
#convert $files_list to an array
$all_files = @()
if ($files_list -ne $null) {
    $all_files = ListSplitUnique($files_list)
}
 
#convert $nodes_list to an array
$required_nodes = GetRequiredNodes($nodes_list);
 
# define some global values
$all_nodes = @()
$nl = [Environment]::NewLine
$current_host = gc env:computername
$ips = ((GetIPAddress $env:COMPUTERNAME) -as [array])
 
 
function Check-Files 
{    
    # check for missing arguments
    if (($all_files.Count -lt 2) -or ($cmd_word = $null)) {
        write-error "Usage: push_configure_hdp_dependencies.ps1 <source_path> <target_path> <files_list> <cmd_line...>
Files_list is a string containing a comma-delimited list of simple
file names, to be found in source_path and copied to target_path on
each node.  Cmd_line is a sequence of command line tokens constituting 
a valid PowerShell execution."
        Exit -1;
    }
    
    # validate that $target_path is an absolute path including volume name
    $path_pieces = $target_path.Split(":")
    if ($path_pieces.Length -lt 2) {
        write-error "$target_path path does not include volume name with colon.  Exiting."
        Exit -1;
    }
    if ($path_pieces.Length -gt 2) {
        write-error "$target_path path has multiple colons.  Exiting."
        Exit -1;
    }
    
    # validate existence and accessibility of files in the source_path
    if (! (Test-Path $source_path)) {
        write-error "$source_path is not accessible from install master server.  Exiting."
        Exit -1;
    }
    cd "$source_path"
    write-output "$($nl)Push install files: "
    ls $all_files
    if (! $?) {
        write-error "Some requested files are missing from $source_path.  Exiting."
        Exit -1
    }
    write-output "$nl" 
}
 
function Summarize-Results($results_set)
{
    $remoteErrors = @()
    $remoteSuccessCount = 0
    $remoteFailureCount = 0
    $remoteOutputResult = 0
    
    foreach ( $result in $results_set ) {
        if ( $result -is [System.Management.Automation.ErrorRecord] ) {
            $remoteFailureCount++
        }
        ElseIf ($result -eq "Done.") {
            $remoteSuccessCount++ 
        } Else {
            $remoteOutputResult++
        }
    }
    
    return New-Object psobject -Property @{
        remoteErrors = $results_set;
        remoteSuccessCount = $remoteSuccessCount;
        remoteFailureCount = $remoteFailureCount;
        remoteOutputResult = $remoteOutputResult
    }
}
 
function Report-Results($summary_results) {
    # Report summarized results to user
    # $summary_results must be the output of Summarize-Results()    
    write-output "Summary:"
    write-output ("" + $results.remoteSuccessCount + " nodes successfully completed")
    
    if ($results.remoteFailureCount -gt 0) {
        write-output ("" + $results.remoteFailureCount + " failure messages.")
    }
    
    if ($results.remoteOutputResult -gt 0) {
        write-output ("" + $results.remoteOutputResult + " Output Lines.")
    }
}
 
function CopyInstall-Files($node)
{
    if (-not ((($node -ieq $current_host) -or (IsSameHost $node $ips)) -and ($source_path -ieq $target_path)))  {
        # convert $target_path into the corresponding admin share path, so we can push the files to the node
        $tgtDir = '\\' + ($node) + '\' +  $target_path.ToString().Replace(':', '$')
 
        # attempt to create the target install directory on the remote node if it doesn't already exist
        if(! (Test-Path "$tgtDir")) { $r = mkdir "$tgtDir" }
 
        # validate that the $tgtDir admin share exists and is accessible on the remote node
        if (! (Test-Path "$tgtDir") ) {
            write-error "$target_path on $node is not accessible by admin share.  Skipping."
            return $false
        }
 
        # push the files to each node.  Skip node if any errors.
        cd "$source_path"
        cp $all_files "$tgtDir" -Force -ErrorAction Stop
        if (! $?) {
            write-error "Some files could not be pushed to $node.  Skipping."
            return $false
        }
        
        return $true
    }
    else {
        return $true
    }
}
 
function PushInstall-Files($nodes)
{
    $copied_nodes = @()
    
    foreach ($node in $nodes) {
        # tag output with node name:
        write-output ("Copying files for node $node $nl")    
        
        # Copy the install files and build list of those needed an install
        if (CopyInstall-Files($node)) {
            if (($node -ieq $current_host) -or (IsSameHost $node $ips)){
                write-output "Skipping $node because it is current node $nl"
            }        
            else {
                write-output ("Copied files to node $node $nl")    
                $copied_nodes += $node
            }
        }
    }
    
    if ($copied_nodes.length -gt 0) {
        # invoke the install, and wait for it to complete
        write-output ("Pushing to nodes: " + ($copied_nodes -join " ") + "$nl")
        write-output "With command : $cmd_word $cmd_args"
 
        $arg_list = @("-file", "$cmd_word") + $cmd_args
        Invoke-Command -ComputerName $copied_nodes -ScriptBlock {
            # everything in this scriptblock runs local to the node    
            $node = $env:COMPUTERNAME
            
            # launch the ps1 file
            $proc_record = Start-Process powershell.exe -ArgumentList $using:arg_list -PassThru -Wait -Verb "RunAs"
            if (! $?) {
                write-error "Start-Process call failed for $node.  Error $($error[0]).  Skipping."
                return
            }
            if ($proc_record.ExitCode -ne 0) {
                        write-error "$using:cmd_word failed for $node.  Error code $($proc_record.ExitCode).  Skipping."
                        write-error "For error code meanings, see http://msdn.microsoft.com/en-us/library/windows/desktop/aa390890(v=vs.85).aspx"
                        return
                    }
            
            write-output "Installed files to node $node $nl"
            write-output "Done."  #getting to this line indicates success
 
            } 2>&1
    }    
    write-output "$nl"
}
 
function Install-HDPDependencies( )
{
    # validate the input files
    Check-Files
    
    # Do the install.
    $all_results = @()
    write-output ("Nodes to install (parallel): " + ($required_nodes -join " ") + "$nl")
    
    # bucket the nodes for parallel installation
    $required_buckets = @()
    if ($batch_size -le 1) { $batch_size = 1 }
    if ($required_nodes.length -le $batch_size) {
        $required_buckets= ,@($required_nodes)
    } else {
        $required_buckets= for($i=0; $i -lt $required_nodes.length; $i+=$batch_size) { , $required_nodes[$i..($i+$batch_size-1)]}
    }
    
    foreach ($required_bucket in $required_buckets) {
        write-output ("Pushing to nodes: " + ($required_bucket -join " ") + "...")
        $push_results = PushInstall-Files($required_bucket) 2>&1
        if ($push_results) {
            write-output "output: $push_results"
            $all_results += $push_results
            $push_results
        }
    }                
 
    write-output "$nl$nl"
    
    # parse $all_results for failure messages and alert user
    $results = Summarize-Results $all_results
    Report-Results $results
}    
 
# Invoke the functionality of the script
Install-HDPDependencies