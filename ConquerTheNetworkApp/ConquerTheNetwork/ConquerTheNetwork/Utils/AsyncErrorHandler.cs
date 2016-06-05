using System;
using System.Diagnostics;

namespace ConquerTheNetwork.Utils
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Debug.WriteLine($"AsyncErrorHandler caught this exception: {exception.Message}");

            // TODO: handle the exception in your own way, e.g. reporting it to
            // an online application crash analytics service
        }
    }
}
