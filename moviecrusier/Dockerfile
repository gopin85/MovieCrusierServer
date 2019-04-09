FROM microsoft/aspnetcore-build:latest As build-env
WORKDIR /app

#Copy cs Project
COPY *.csproj ./
RUN dotnet restore

#Copy Everything else
COPY . ./
RUN dotnet publish -c Release -o out

#Build runtime image
FROM microsoft/aspnetcore:latest
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "moviecrusier.dll"]