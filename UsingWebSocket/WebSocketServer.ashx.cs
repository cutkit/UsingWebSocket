using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace UsingWebSocket
{
    public class WebSocketServer : IHttpHandler
    {
        public bool IsReusable// => throw new NotImplementedException();
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //throw new NotImplementedException();
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessSocket);
            }
        }
        public async Task ProcessSocket(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    string userMessage = Encoding.UTF8.GetString(buffer.Array,0,result.Count);
                    userMessage = "You send:" + userMessage + "at" + DateTime.Now.ToLongDateString();
                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMessage));
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    break;
                }
            }

        }
    }
}