using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingWebSocket
{
    public class MicrosoftWebSockets: WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();
        private string name;
        public override void OnOpen()
        {
            //base.OnOpen();
            name = this.WebSocketContext.QueryString["chatName"];
            clients.Add(this);
            clients.Broadcast(name + " has connected!");
        }
        public override void OnMessage(string message)
        {
            // base.OnMessage(message);
            clients.Broadcast(string.Format("{0} said {1}", name, message));
        }
        public override void OnClose()
        {
            //base.OnClose();
            clients.Remove(this);
            clients.Broadcast(string.Format("{0} has gone away", name));
        }
    }
}