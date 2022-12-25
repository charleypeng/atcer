// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.MessageCenter.Services;
using Microsoft.AspNetCore.SignalR;

namespace ATCer.UserCenter.Hubs
{
    public abstract class BaseHub : Hub
    {
        protected readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        public virtual async Task SendMsgAsync(string message)
        {
            await Clients.All.SendAsync("SendMessage", message);
        }
        /// <summary>
        /// Override of basehub, to add groups and users into the  connection database
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Groups.AddToGroupAsync(Context.ConnectionId, "client");
            string name = Context.User.Identity.Name;
            //to ensure the identity user name is not a null string
            var _name = name ?? "UNKNOWN";

            _connections.Add(_name, Context.ConnectionId);

            Console.WriteLine($"{_name} && {_connections.Count} has been added");

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            string name = Context.User.Identity.Name;
            //to ensure the identity user name is not a null string
            var _name = name ?? "UNKNOWN";
            _connections.Remove(_name, Context.ConnectionId);

            Console.WriteLine($"{name} disconnected");
        }
    }

    public class BaseHub<T> : Hub<T> where T : class
    {
        protected readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        /// <summary>
        /// Override of basehub, to add groups and users into the  connection database
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Groups.AddToGroupAsync(Context.ConnectionId, "client");
            string name = Context.User.Identity.Name;
            //to ensure the identity user name is not a null string
            var _name = name ?? "UNKNOWN";

            _connections.Add(_name, Context.ConnectionId);

            Console.WriteLine($"{_name} && {_connections.Count} has been added");

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            string name = Context.User.Identity.Name;
            //to ensure the identity user name is not a null string
            var _name = name ?? "UNKNOWN";
            _connections.Remove(_name, Context.ConnectionId);

            Console.WriteLine($"{name} disconnected");
        }
    }
}
