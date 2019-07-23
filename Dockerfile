FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY SymSpellAPI/*.csproj ./SymSpellAPI/
RUN dotnet restore

# copy everything else and build app
COPY SymSpellAPI/. ./SymSpellAPI/
WORKDIR /app/SymSpellAPI
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY SymSpellAPI/frequency_dictionary_en_82_765.txt ./
COPY --from=build /app/SymSpellAPI/out ./
ENTRYPOINT ["dotnet", "SymSpellAPI.dll"]