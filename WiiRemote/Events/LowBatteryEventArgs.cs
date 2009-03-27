using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// The event arguments for when the battery level on the Wii remote is lower than the threshold
    /// </summary>
    public class LowBatteryEventArgs : EventArgs
    {
        //==================================================================================
        #region Private Variables

        private byte bLevel;  //The battery level

        #endregion
        
        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the battery level when this event was raised
        /// </summary>
        public byte BatteryLevel    
        {
            get { return bLevel; }
        }

        #endregion

        //==================================================================================
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="BattLevel">The battery level at the time this event was raised</param>
        public LowBatteryEventArgs(byte BattLevel)
            : base()
        {
            this.bLevel = BattLevel;
        }

        #endregion
    }
}
