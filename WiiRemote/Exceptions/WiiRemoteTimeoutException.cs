using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// An exception that is thrown when a communication with the Wii Remote takes longer than expected.
    /// </summary>
    public class WiiRemoteTimeoutException : ApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WiiRemoteTimeoutException()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        public WiiRemoteTimeoutException(string Message)
            : base(Message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        /// <param name="InnerException">The exception that led to this exception</param>
        public WiiRemoteTimeoutException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }
    }
}
