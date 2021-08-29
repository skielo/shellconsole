using System;
namespace appui.Runtime
{
    public class QuitException : Exception
    {
        public QuitException(string message) : base(message)
        {
        }
    }
}
