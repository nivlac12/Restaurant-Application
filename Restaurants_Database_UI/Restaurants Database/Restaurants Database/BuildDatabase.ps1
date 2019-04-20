Param(
   [string] $Server = "(localdb)\MSSQLLocalDb",
   [string] $Database = "nivlac12"
   [string] $world = "WERE ERW EIJOPA\n\n\n"
)

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

# Tables
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Organization.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Organization.sql"
Start-Sleep -s 3
Echo $world
Start-Sleep -s 3
