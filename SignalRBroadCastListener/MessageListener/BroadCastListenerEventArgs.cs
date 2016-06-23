using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBroadCastListener.MessageListner
{
    public class BroadCastListenerEventArgs : EventArgs
    {
        public string Message { get; internal set; }
    }
}
