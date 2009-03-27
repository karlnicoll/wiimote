using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// An exception that is thrown when an attempt to contact the Wii remote is made when the hardware is not connected
    /// </summary>
    class WiiRemoteNotConnectedException : ApplicationException
    {
        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants


        #endregion

        //==================================================================================
        #region Events



        #endregion

        //==================================================================================
        #region Private Variables

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Constructor
        /// </summary>
        public WiiRemoteNotConnectedException()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        public WiiRemoteNotConnectedException(string Message)
            : base(Message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        /// <param name="InnerException">The exception that led to this exception</param>
        public WiiRemoteNotConnectedException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }

        #endregion

        //==================================================================================
        #region Public Properties


        #endregion

        //==================================================================================
        #region Public Methods


        #endregion

        //==================================================================================
        #region Private/Protected Methods



        #endregion
    }
}
