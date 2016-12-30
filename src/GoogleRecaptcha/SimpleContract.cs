using System;

namespace GoogleRecaptcha
{
    internal class SimpleContract
    {
        public static void ThrowWhen<T>(bool condition, string message = null) where T: Exception
        {
            if (condition)
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }
    }
}