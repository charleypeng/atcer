// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageCenter.Dtos;
using ATCer.MessageQueue.Dtos;
using ATCer.MessageQueue.Enums;
using Furion.InstantMessaging;
using ATCer.Authentication.Core;
using ATCer.Authentication.Dtos;
using ATCer.Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using System.Linq;

namespace ATCer.MessageQueue.Core
{
    /// <summary>
    /// MQ service hub
    /// </summary>
    [MapHub("api/ws/mq")]
    [Authorize(AuthenticationSchemes = $"{nameof(IdentityType.User)},{nameof(IdentityType.Client)}")]
    public class MQServiceHub : BaseHub
    {
        private readonly ICapPublisher publisher;
        private readonly IIdentityService identityService;
        private readonly IMQService mQService;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="publisher"></param>
        /// <param name="identityService"></param>
        /// <param name="mQService"></param>
        public MQServiceHub(ICapPublisher publisher,
                            IIdentityService identityService,
                            IMQService mQService)
        {
            this.publisher = publisher;
            this.mQService = mQService;
            this.identityService = identityService;
        }
        
        /// <summary>
        /// 客户端发过来的通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task Send(MQData data)
        {
            //收到客户端信息
            if (data == null)
            {
                return;
            }
            Identity identity = identityService.GetIdentity();
            data.Identity = identity;
            data.Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4();
            if(string.IsNullOrEmpty(data.MQTopic))
            {
                //if not topic then send as default topic("SendInvoke")
                await publisher.PublishAsync(Topic.MQCenter.MQSendInvoke, data, data.CallBack);
            }
            else
            {
                //send to subscribers
                await publisher.PublishAsync(data.MQTopic, data, data.CallBack);
            }
        }
        /// <summary>
        /// 用户连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var notifyDto = new UserStatusNotifyDto()
            {
                OnlineStatus = UserOnlineStatus.Online
            };

            var identity = identityService.GetIdentity();
            var ipAddress = Context.GetHttpContext().GetRemoteIpAddressToIPv4();
            //notify when user online
            if (identity != null)
            {
                notifyDto.UserId = identity.Id;
                notifyDto.UserName = identity.GivenName;
                notifyDto.IpAddress = ipAddress;
                notifyDto.ClientId = identity.LoginId;
            }
            await publisher.PublishAsync(Topic.MQCenter.MQUserStatusChanged, notifyDto);
            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var notifyDto = new UserStatusNotifyDto()
            {
                OnlineStatus = UserOnlineStatus.Offline
            };
            var identity = identityService.GetIdentity();
            var ipAddress = Context.GetHttpContext().GetRemoteIpAddressToIPv4();
            //notify when user offline
            if (identity != null)
            {
                notifyDto.UserId = identity.Id;
                notifyDto.UserName = identity.GivenName;
                notifyDto.IpAddress = ipAddress;
                notifyDto.ClientId = identity.LoginId;
            }
            await publisher.PublishAsync(Topic.MQCenter.MQUserStatusChanged, notifyDto);
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        public static void HttpConnectionDispatcherOptionsSettings(HttpConnectionDispatcherOptions options)
        {
            // 配置
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public static void HubEndpointConventionBuilderSettings(HubEndpointConventionBuilder builder)
        {
            // 配置
            var options = App.GetService<IOptions<MessageQueueOptions>>().Value;
            if (options == null)
                throw new ArgumentNullException("没有messagequeue的配置");

            var origins = options.Origins;

            if (origins == null || origins.Count() == 0)
                throw new ArgumentNullException("请至少配置一个域");

            builder.RequireCors(cpb =>
            {
                cpb.WithOrigins(origins)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .Build();
            });
        }
    }
}
