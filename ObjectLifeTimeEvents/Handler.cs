using System.Diagnostics;
using System.Windows;

namespace CodeSampleCsharp
{
    internal static class Handler
    {
        public static void EventInfo(string objectName, string eventName)
        {
            Debug.WriteLine($"{objectName} {eventName} event.");
        }
    }
}
