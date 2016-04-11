param(
    [string] $config_file_name,
    [string] $config_property_name,
    [string] $config_property_value
)
 
if(Test-Path -Path $config_file_name -PathType Leaf) {
    $conf_doc = [System.Xml.XmlDocument](Get-Content $config_file_name)
 
    $property_node = ($conf_doc.DocumentElement.property | Where-Object {$_.name -eq $config_property_name})
    If ($property_node) {
        # Element found so ensure the property is correctly set
        write-host "$config_property_name Element Found, so updating value to $config_property_value..."
        $property_node.Value = $config_property_value
    } else {
        # No Element found so add a new one to the document
        write-host "$config_property_name Element Not Present, adding new element..."
        $property_element = $conf_doc.CreateElement("property")
 
        $property_element_name = $conf_doc.CreateElement("name")
        $property_element_name.AppendChild($conf_doc.CreateTextNode($config_property_name))
        $property_element.AppendChild($property_element_name)
 
        $property_element_value = $conf_doc.CreateElement("value")
        $property_element_value.AppendChild($conf_doc.CreateTextNode($config_property_value))
        $property_element.AppendChild($property_element_value)
 
        $conf_doc.DocumentElement.AppendChild($property_element)
    }
 
    $conf_doc.Save($config_file_name)
} else {
    Write-Error "Configuration File $config_file_name cannot be found"
}