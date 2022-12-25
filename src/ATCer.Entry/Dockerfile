#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gardener.Entry/Gardener.Entry.csproj", "Gardener.Entry/"]
COPY ["Api/Gardener.Api.Core/Gardener.Api.Core.csproj", "Api/Gardener.Api.Core/"]
COPY ["ATCer/Framework/ATCer.Core/ATCer.Core.csproj", "ATCer/Framework/ATCer.Core/"]
COPY ["Modules/UserCenter/Gardener.UserCenter.Impl/Gardener.UserCenter.Impl.csproj", "Modules/UserCenter/Gardener.UserCenter.Impl/"]
COPY ["Modules/Framework/Gardener.Base/Gardener.Base.csproj", "Modules/Framework/Gardener.Base/"]
COPY ["Modules/Framework/Gardener.EntityFramwork/Gardener.EntityFramwork.csproj", "Modules/Framework/Gardener.EntityFramwork/"]
COPY ["Common/Gardener.Common/Gardener.Common.csproj", "Common/Gardener.Common/"]
COPY ["Common/Gardener.Enums/Gardener.Enums.csproj", "Common/Gardener.Enums/"]
COPY ["Common/Gardener.Attributes/Gardener.Attributes.csproj", "Common/Gardener.Attributes/"]
COPY ["Modules/Framework/Gardener/Gardener.csproj", "Modules/Framework/Gardener/"]
COPY ["Modules/Framework/Gardener.SystemManager/Gardener.SystemManager.csproj", "Modules/Framework/Gardener.SystemManager/"]
COPY ["Modules/UserCenter/Gardener.UserCenter/Gardener.UserCenter.csproj", "Modules/UserCenter/Gardener.UserCenter/"]
COPY ["ATCer/Framework/ATCer.DataRecorder/ATCer.DataRecorder.csproj", "ATCer/Framework/ATCer.DataRecorder/"]
COPY ["ATCer/Framework/ATCer.ElasticSearch/ATCer.ElasticSearch.csproj", "ATCer/Framework/ATCer.ElasticSearch/"]
COPY ["ATCer/Framework/ATCer.Base/ATCer.Base.csproj", "ATCer/Framework/ATCer.Base/"]
COPY ["ATCer/Framework/ATCer.FDE/ATCer.FDE.csproj", "ATCer/Framework/ATCer.FDE/"]
COPY ["ATCer/Framework/ATCer/ATCer.csproj", "ATCer/Framework/ATCer/"]
COPY ["ATCer/Framework/ATCer.MessageQueue/ATCer.MessageQueue.csproj", "ATCer/Framework/ATCer.MessageQueue/"]
COPY ["Modules/Framework/Gardener.Attachment/Gardener.Attachment.csproj", "Modules/Framework/Gardener.Attachment/"]
COPY ["Modules/Framework/Gardener.Audit/Gardener.Audit.csproj", "Modules/Framework/Gardener.Audit/"]
COPY ["Modules/Framework/Gardener.Authentication/Gardener.Authentication.csproj", "Modules/Framework/Gardener.Authentication/"]
COPY ["Modules/Framework/Gardener.Authorization/Gardener.Authorization.csproj", "Modules/Framework/Gardener.Authorization/"]
COPY ["Modules/Framework/Gardener.Cache/Gardener.Cache.csproj", "Modules/Framework/Gardener.Cache/"]
COPY ["Modules/Framework/Gardener.CodeGeneration/Gardener.CodeGeneration.csproj", "Modules/Framework/Gardener.CodeGeneration/"]
COPY ["Modules/Framework/Gardener.Email/Gardener.Email.csproj", "Modules/Framework/Gardener.Email/"]
COPY ["Modules/Framework/Gardener.EventBus/Gardener.EventBus.csproj", "Modules/Framework/Gardener.EventBus/"]
COPY ["Modules/Framework/Gardener.FileStore/Gardener.FileStore.csproj", "Modules/Framework/Gardener.FileStore/"]
COPY ["Modules/Framework/Gardener.NotificationSystem/Gardener.NotificationSystem.csproj", "Modules/Framework/Gardener.NotificationSystem/"]
COPY ["Modules/Framework/Gardener.Swagger/Gardener.Swagger.csproj", "Modules/Framework/Gardener.Swagger/"]
COPY ["Modules/Framework/Gardener.VerifyCode/Gardener.VerifyCode.csproj", "Modules/Framework/Gardener.VerifyCode/"]
COPY ["Modules/Framework/Gardener.VerifyCode.CacheStore/Gardener.VerifyCode.CacheStore.csproj", "Modules/Framework/Gardener.VerifyCode.CacheStore/"]
COPY ["Modules/Framework/Gardener.VerifyCode.DbStore/Gardener.VerifyCode.DbStore.csproj", "Modules/Framework/Gardener.VerifyCode.DbStore/"]
COPY ["Modules/SysTimer/Gardener.SysTimer.Impl/Gardener.SysTimer.Impl.csproj", "Modules/SysTimer/Gardener.SysTimer.Impl/"]
COPY ["Modules/SysTimer/Gardener.SysTimer/Gardener.SysTimer.csproj", "Modules/SysTimer/Gardener.SysTimer/"]
COPY ["Client/Gardener.Client.Entry/Gardener.Client.Entry.csproj", "Client/Gardener.Client.Entry/"]
COPY ["Client/Gardener.Client.Core/Gardener.Client.Core.csproj", "Client/Gardener.Client.Core/"]
COPY ["ATCer/Client/ATCer.Client.Core/ATCer.Client.Core.csproj", "ATCer/Client/ATCer.Client.Core/"]
COPY ["ATCer/HRCenter/ATCer.HRCenter.Client/ATCer.HRCenter.Client.csproj", "ATCer/HRCenter/ATCer.HRCenter.Client/"]
COPY ["ATCer/Client/ATCer.Client.Base/ATCer.Client.Base.csproj", "ATCer/Client/ATCer.Client.Base/"]
COPY ["Client/Gardener.Client.Base/Gardener.Client.Base.csproj", "Client/Gardener.Client.Base/"]
COPY ["ATCer/Framework/ATCer.MessageQueue.Client/ATCer.MessageQueue.Client.csproj", "ATCer/Framework/ATCer.MessageQueue.Client/"]
COPY ["ATCer/LTFATCenter/ATCer.LTFATCenter.Client/ATCer.LTFATCenter.Client.csproj", "ATCer/LTFATCenter/ATCer.LTFATCenter.Client/"]
COPY ["ATCer/MessageCenter/ATCer.MessageCenter.Client/ATCer.MessageCenter.Client.csproj", "ATCer/MessageCenter/ATCer.MessageCenter.Client/"]
COPY ["Modules/Framework/Gardener.Attachment.Client/Gardener.Attachment.Client.csproj", "Modules/Framework/Gardener.Attachment.Client/"]
COPY ["Modules/Framework/Gardener.Audit.Client/Gardener.Audit.Client.csproj", "Modules/Framework/Gardener.Audit.Client/"]
COPY ["Modules/Framework/Gardener.Authentication.Client/Gardener.Authentication.Client.csproj", "Modules/Framework/Gardener.Authentication.Client/"]
COPY ["Modules/Framework/Gardener.CodeGeneration.Client/Gardener.CodeGeneration.Client.csproj", "Modules/Framework/Gardener.CodeGeneration.Client/"]
COPY ["Modules/Framework/Gardener.Email.Client/Gardener.Email.Client.csproj", "Modules/Framework/Gardener.Email.Client/"]
COPY ["Modules/Framework/Gardener.NotificationSystem.Client/Gardener.NotificationSystem.Client.csproj", "Modules/Framework/Gardener.NotificationSystem.Client/"]
COPY ["Modules/Framework/Gardener.Swagger.Client/Gardener.Swagger.Client.csproj", "Modules/Framework/Gardener.Swagger.Client/"]
COPY ["Modules/Framework/Gardener.SystemManager.Client/Gardener.SystemManager.Client.csproj", "Modules/Framework/Gardener.SystemManager.Client/"]
COPY ["Modules/Framework/Gardener.VerifyCode.Client/Gardener.VerifyCode.Client.csproj", "Modules/Framework/Gardener.VerifyCode.Client/"]
COPY ["Modules/SysTimer/Gardener.SysTimer.Client/Gardener.SysTimer.Client.csproj", "Modules/SysTimer/Gardener.SysTimer.Client/"]
COPY ["Modules/UserCenter/Gardener.UserCenter.Client/Gardener.UserCenter.Client.csproj", "Modules/UserCenter/Gardener.UserCenter.Client/"]
RUN dotnet restore "Gardener.Entry/Gardener.Entry.csproj"
COPY . .
WORKDIR "/src/Gardener.Entry"
RUN dotnet build "Gardener.Entry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gardener.Entry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gardener.Entry.dll"]