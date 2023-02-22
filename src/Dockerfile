#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
#install essential build tools
RUN apt-get update 
RUN apt-get install -y python3
RUN dotnet workload install wasm-tools
#copy files
COPY ["Api/ATCer.Api.Entry/ATCer.Api.Entry.csproj", "Api/ATCer.Api.Entry/"]
COPY ["Api/ATCer.Api.Core/ATCer.Api.Core.csproj", "Api/ATCer.Api.Core/"]
COPY ["Framework/ATCer.Application/ATCer.Application.csproj", "Framework/ATCer.Application/"]
COPY ["Framework/ATCer.Core/ATCer.Core.csproj", "Framework/ATCer.Core/"]
COPY ["Framework/ATCer/ATCer.csproj", "Framework/ATCer/"]
COPY ["Framework/ATCer.DataRecorder/ATCer.DataRecorder.csproj", "Framework/ATCer.DataRecorder/"]
COPY ["Framework/ATCer.FDE/ATCer.FDE.csproj", "Framework/ATCer.FDE/"]
COPY ["Framework/ATCer.MessageQueue/ATCer.MessageQueue.csproj", "Framework/ATCer.MessageQueue/"]
COPY ["Framework/Furion.TaskScheduler/Furion.TaskScheduler.csproj", "Framework/Furion.TaskScheduler/"]
RUN dotnet restore "Api/ATCer.Api.Entry/ATCer.Api.Entry.csproj"
COPY . .
WORKDIR "/src/Api/ATCer.Api.Entry"
RUN dotnet build "ATCer.Api.Entry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ATCer.Api.Entry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV TZ=Asia/Shanghai
WORKDIR /app
VOLUME /app/Settings
VOLUME /app/upload
VOLUME /app/logs
VOLUME /app/wwwroot/upload
COPY --from=publish /app/publish .
RUN apt-get update -y && apt-get install -y fontconfig && apt-get clean
RUN cp ./Fonts/*.* /usr/share/fonts
RUN fc-cache
RUN rm -rf ./Settings.Dev *.Development.json
ENTRYPOINT ["dotnet", "ATCer.Api.Entry.dll"]