using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace UsingWebSocket
{
    public class WebSocketServer2 : IHttpHandler
    {
        public bool IsReusable //=> throw new NotImplementedException();
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            //throw new NotImplementedException();
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new MicrosoftWebSockets());
            }
        }
    }
}