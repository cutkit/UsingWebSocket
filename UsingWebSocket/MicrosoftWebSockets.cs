using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace UsingWebSocket
{
    public class MicrosoftWebSockets: WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();
        private string name;
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            clients.Broadcast("hello");
        }
        public override void OnOpen()
        {
            //base.OnOpen();
            name = this.WebSocketContext.QueryString["chatName"];
            clients.Add(this);
            clients.Broadcast(name + " has connected!");

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
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