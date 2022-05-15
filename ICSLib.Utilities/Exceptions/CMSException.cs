/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using System;

namespace ICSLib.Utilities.Exceptions
{
    public class CMSException : Exception
    {
        public CMSException() { 
        }

        public CMSException(string message) : base(message)
        {
        }

        public CMSException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
