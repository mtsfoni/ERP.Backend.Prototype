# Define a parameter to hold the migration name
param(
    [Parameter(Mandatory = $true)]
    [string]
    $MigrationName
)

#PostgreSQL
$ENV:DATABASE_TYPE = "PostgreSQL"
dotnet ef migrations add $MigrationName --startup-project ./ERP.Backend.REST --project ./Migrations/ERP.Backend.PostgreSQL

#SQLite
$ENV:DATABASE_TYPE = "SQLite"
dotnet ef migrations add $MigrationName --startup-project ./ERP.Backend.REST --project ./Migrations/ERP.Backend.SQLite