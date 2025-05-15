# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5044
ENV ASPNETCORE_URLS=http://+:5044

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmployeeManagementSystem.csproj", "."]
RUN dotnet restore "./EmployeeManagementSystem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EmployeeManagementSystem.csproj" -c Release -o /app/build

# Add this line to install EF tools
RUN dotnet tool install --global dotnet-ef --version 8.0.*

FROM build AS publish
RUN dotnet publish "EmployeeManagementSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.dll"]