# Etapa 1: Build da aplicação
# Use uma imagem base do SDK do .NET para compilar o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Copie os arquivos de projeto (.csproj) e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie todos os arquivos da aplicação e compile-a
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Criação da imagem final para execução
# Use uma imagem base mais leve do .NET runtime para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Copie os arquivos compilados da etapa de build para a etapa final
COPY --from=build /app/publish .

# Exponha a porta 80 para o tráfego HTTP
EXPOSE 80

# Defina o comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "OfficeRoomie.dll"]