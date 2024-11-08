# Remover o arquivo do banco de dados SQLite, se existir
$databasePath = "officeroomie.db"
if (Test-Path $databasePath) {
    Remove-Item $databasePath -ErrorAction SilentlyContinue
    Write-Host "Arquivo do banco de dados removido: $databasePath"
} else {
    Write-Host "Arquivo do banco de dados n�o encontrado."
}

# Remover as migra��es atuais (se estiverem em uma pasta chamada Migrations)
$migrationsPath = "./Migrations"
if (Test-Path $migrationsPath) {
    Remove-Item -Recurse -Force $migrationsPath
    Write-Host "Migra��es removidas do diret�rio: $migrationsPath"
} else {
    Write-Host "Diret�rio de migra��es n�o encontrado."
}

# Criar nova migra��o chamada "InitialMigration"
dotnet ef migrations add InitialMigration
Write-Host "Nova migra��o 'InitialMigration' criada."

# Atualizar o banco de dados
dotnet ef database update
Write-Host "Banco de dados atualizado."