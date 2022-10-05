using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PictureViewerDE.Utilities
{
    internal static class Debug
    {
        private const string _PREFIX = "DEBUG.TRACE";
        private const string _S = "|";  // separator
        private const string _B = "";  // begining
        private const string _E = "";  // end
        public static bool Enable{ get; set; } // is toggle possible?

        // Does not return correct calling method name
        //public static void Trace()
        //{
        //    Trace("");
        //}

        public static void Trace(string message, [CallerMemberName] string callerName = "")
        {
            if (Enable)
            {
                string currentTime = DateTime.Now.ToString("HH:mm:ss.fff");
                if (message != "")
                {
                    message = "  ->  " + "[\'" + message + "\']";
                }
                Console.WriteLine($"{_B}{_S}{_PREFIX}{_S}{currentTime}{_S}{NameOfCallingClass()}.{callerName}{_S}{_E}{message}");
                /* align test
                Console.Write($"{_B}{_S}{_PREFIX}{_S}{currentTime}{_S}{NameOfCallingClass()}.{callerName}{_S}{_E}");
                Console.CursorLeft = Console.BufferWidth - (message.Length + 4);
                Console.WriteLine($"{message}");
                */
            }
        }

        public static string NameOfCallingClass() /// https: //stackoverflow.com/questions/48570573/how-to-get-class-name-that-is-calling-my-method
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }
    }
}



/*
System.Reflection.MethodBase.GetCurrentMethod().Name
//---------
MethodBase method = new StackFrame(2, false).GetMethod();
Type declaringType = method.DeclaringType;
*/
