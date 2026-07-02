# Stage 1: Build and publish using the .NET 9.0 SDK
FROM ://microsoft.com AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime image using .NET 9.0 ASP.NET Core Runtime
FROM ://microsoft.com
WORKDIR /app
COPY --from=build-env /app/out .

# Configure environment variables and ports
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "render-ci.dll"]
