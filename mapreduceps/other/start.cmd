set working_path=%~dp0
if %working_path:~-1%==\ set working_path=%working_path:~0,-1%
 
set script_path=%working_path%\push_configure_cluster_properties.ps1
set source_path=%working_path%
 
set target_root=E:\hdp-installassets
set target_path=%target_root%\configuration
 
set files_list=configure-cluster-property.ps1,configure-cluster-properties.ps1,configure-cluster-properties.ps1,configure-cluster-properties.txt
set nodes_list=%working_path%\clusternodes.txt
 
set configuration_file=configure-cluster-properties.txt
 
set cmd_word=%target_path%\configure-cluster-properties.ps1
set cmd_args=-working_path '%target_path%' -configuration_file '%configuration_file%'
 
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '%script_path%' -source_path '%source_path%' -target_path '%target_path%' -files_list '%files_list%' -nodes_list '%nodes_list%' -batch_size 4 -cmd_word '%cmd_word%' %cmd_args%"
 
pause