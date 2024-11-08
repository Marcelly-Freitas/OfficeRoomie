# Remover o arquivo do banco de dados SQLite, se existir
$databasePath = "officeroomie.db"
if (Test-Path $databasePath) {
    Remove-Item $databasePath -ErrorAction SilentlyContinue
    Write-Host "Arquivo do banco de dados removido: $databasePath"
} else {
    Write-Host "Arquivo do banco de dados não encontrado."
}

# Remover as migrações atuais (se estiverem em uma pasta chamada Migrations)
$migrationsPath = "./Migrations"
if (Test-Path $migrationsPath) {
    Remove-Item -Recurse -Force $migrationsPath
    Write-Host "Migrações removidas do diretório: $migrationsPath"
} else {
    Write-Host "Diretório de migrações não encontrado."
}

# Criar nova migração chamada "InitialMigration"
dotnet ef migrations add InitialMigration
Write-Host "Nova migração 'InitialMigration' criada."

# Atualizar o banco de dados
dotnet ef database update
Write-Host "Banco de dados atualizado."