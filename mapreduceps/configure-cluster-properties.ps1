param(
    [string] $working_path,
    [string] $configuration_file
)
 
$hadoop_config_dir = $env:HADOOP_CONF_DIR
$configurationFile = "$working_path\$configuration_file";
 
function Get-PropertiesTable {
 
    $properties = @(); 
 
    if (Test-Path -Path $configurationFile -PathType Leaf) {    
        $propertylines = Get-Content $configurationFile
        foreach ($propertyline in $propertylines)
        {
            $propertyline = $propertyline.Trim()
            if (($propertyline) -and (-not $propertyline.StartsWith("#")))
            {
                $propertyline_values = ,@($propertyline -Split ",|\t") | % {$_.Trim()}
                if ($propertyline_values.length -eq 3) {
                    $hdp_file = $propertyline_values[0]
                    $properties += New-Object PSObject ¨CProperty @{ConfigFile="$hadoop_config_dir\$hdp_file"; PropertyName=$propertyline_values[1]; PropertyValue=$propertyline_values[2]}
                }
            }
        }            
    } else {
        Write-Error "Configuration File $configurationFile cannot be found..."
    }
 
    return $properties;
}
 
Get-PropertiesTable | % {. $working_path\configureclusterproperty.ps1 -config_file_name $_.ConfigFile -config_property_name $_.PropertyName -config_property_value $_.PropertyValue }