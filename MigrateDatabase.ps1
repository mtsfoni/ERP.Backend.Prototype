#PostgreSQL
$ENV:DATABASE_TYPE = "PostgreSQL"
dotnet ef migrations add INITIAL --startup-project ./ERP.Backend.REST --project ./Migrations/ERP.Backend.Postgres

#SQLite
$ENV:DATABASE_TYPE = "SQLite"
dotnet ef migrations add INITIAL --startup-project ./ERP.Backend.REST --project ./Migrations/ERP.Backend.SQLite