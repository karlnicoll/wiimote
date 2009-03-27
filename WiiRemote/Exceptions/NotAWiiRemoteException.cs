using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// An exception that informs the application that the device path does not represent a Wii Remote
    /// </summary>
    public class NotAWiiRemoteException : ApplicationException
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

        /// <summary>
        /// The path to the device to which the connection was attempted
        /// </summary>
        private string devPath;
        private short vendorID;
        private short productID;

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Constructor
        /// </summary>
        public NotAWiiRemoteException()
            : this("An attempt was made to connect to an HID device, but the device is not a Wii Remote. Correct Device Path?", null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        public NotAWiiRemoteException(string Message)
            : this(Message, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        /// <param name="InnerException">The exception that led to this exception</param>
        public NotAWiiRemoteException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
            this.devPath = "<UNKNOWN>";
            this.vendorID = 0;
            this.productID = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Message">The message to describe what has happened</param>
        /// <param name="Remote">The Wii Remote that raised this Exception</param>
        /// <param name="InnerException">The exception that led to this exception</param>
        public NotAWiiRemoteException(string Message, WiiRemote Remote, Exception InnerException)
            : this(Message, InnerException)
        {
            devPath = Remote.DevicePath;
            vendorID = Remote.VendorID;
            productID = Remote.ProductID;
        }

        #endregion

        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the device path that the Wii remote that is not there
        /// </summary>
        public string DevicePath    
        {
            get { return devPath; }
        }

        /// <summary>
        /// Gets the vendor ID of the device that is not a Wii remote
        /// </summary>
        public short VendorID       
        {
            get { return vendorID; }
        }

        /// <summary>
        /// The Product ID of the device that is not a Wii remot
        /// </summary>
        public short ProductID      
        {
            get { return productID; }
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
