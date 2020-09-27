using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public static class Logger
    {
        //public delegate void WriteMessageDlgt(string message);
        public static Action<string> WriteMessage;
        public static void LogMessage(string msg)
        {
            //WriteMessage(msg);
            WriteMessage?.Invoke(msg);  //  Only call if the value of the delegate is not null
        }
    }

}
