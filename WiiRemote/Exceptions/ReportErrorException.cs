using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// Thrown when a method in the WiiRemote class receives a report that it was not expecting
    /// </summary>
    class ReportErrorException : ApplicationException
    {
        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants

        private const string MESSAGE_DEFAULT = "The report contained an error code.";

        #endregion

        //==================================================================================
        #region Events



        #endregion

        //==================================================================================
        #region Private Variables

        private byte errNo;

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ReportErrorException()
            : this(MESSAGE_DEFAULT)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        public ReportErrorException(string Message)
            : this(Message, 0x00)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="InnerException">Any exception that led to this exception</param>
        public ReportErrorException(string Message, Exception InnerException)
            : this(Message, 0x00, InnerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Err">The error that was received</param>
        public ReportErrorException(byte Err)
            : this(MESSAGE_DEFAULT, Err)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="Err">The error that was received</param>
        public ReportErrorException(string Message, byte Err)
            : this(Message, Err, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="Err">The error that was received</param>
        /// <param name="InnerException">Any exception that led to this exception</param>
        public ReportErrorException(string Message, byte Err, Exception InnerException)
            : base(Message, InnerException)
        {
            errNo = Err;
        }

        #endregion

        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the report type that was mistakenly received
        /// </summary>
        public byte ErrorNumber
        {
            get { return errNo; }
        }

        #endregion

        //==================================================================================
        #region Public Methods


        #endregion

        //==================================================================================
        #region Private/Protected Methods



        #endregion
    }
}
