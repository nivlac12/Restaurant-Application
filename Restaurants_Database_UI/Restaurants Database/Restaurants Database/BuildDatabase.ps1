Param(
   [string] $Server = "mssql.cs.ksu.edu",
   [string] $Database = "nivlac12"
)

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

# Tables
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Create Schema.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Organization.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Organization.sql"
Write-Host "Done"
