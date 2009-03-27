using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// Thrown when a method in the WiiRemote class receives a report that it was not expecting
    /// </summary>
    class IncorrectReportException : ApplicationException
    {
        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants

        private const string MESSAGE_DEFAULT = "The report that was received was not of a report type that was expected, and the report cannot be reliably parsed";

        #endregion

        //==================================================================================
        #region Events



        #endregion

        //==================================================================================
        #region Private Variables

        private byte reportType;

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IncorrectReportException()
            : this(MESSAGE_DEFAULT)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        public IncorrectReportException(string Message)
            : this(Message, 0x00)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="InnerException">Any exception that led to this exception</param>
        public IncorrectReportException(string Message, Exception InnerException)
            : this(Message, 0x00, InnerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="RType">The type of report that was received</param>
        public IncorrectReportException(byte RType)
            : this(MESSAGE_DEFAULT, RType)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="RType">The type of report that was received</param>
        public IncorrectReportException(string Message, byte RType)
            : this(Message, RType, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to explain why the exception has occurred</param>
        /// <param name="RType">The type of report that was received</param>
        /// <param name="InnerException">Any exception that led to this exception</param>
        public IncorrectReportException(string Message, byte RType, Exception InnerException)
            : base(Message, InnerException)
        {
            reportType = RType;
        }

        #endregion

        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the report type that was mistakenly received
        /// </summary>
        public byte ReportType
        {
            get { return reportType; }
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
