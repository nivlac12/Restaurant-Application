Param(
   [string] $Server = "mssql.cs.ksu.edu",
   [string] $Database = "nivlac12"
)

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

# Tables
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Create Schema.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Jobs.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Organization.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Restaurant.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Employees.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Food.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\StockItems.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Organization.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Food.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Jobs.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Restaurant.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_StockItems.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Insert_Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Organization.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Restaurant.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_StockItems.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Suppliers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Jobs.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Food.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Get_Employees.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Organizations.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Restaurant.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Food.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Jobs.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Employees.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_StockItems.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "Sql\Retrieve_Suppliers.sql"


Write-Host "Done"
