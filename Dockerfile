#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
RUN apt-get update
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
RUN dotnet tool install --global dotnet-ef --version 3.1.2
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ["SmartSaver.csproj", "."]
RUN dotnet restore "SmartSaver.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet publish "SmartSaver.csproj" -c Release -o /src/publish
EXPOSE 5000
EXPOSE 5001
WORKDIR /src/publish
ENTRYPOINT ["dotnet", "SmartSaver.dll", "--urls", "http://*:5000", "https://*:5001"]
