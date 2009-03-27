using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    public class AccelerationChangedEventArgs : EventArgs
    {
        //==================================================================================
        #region Private Variables

        private Vector3D direction;  //The battery level

        #endregion
        
        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the battery level when this event was raised
        /// </summary>
        public Vector3D Direction    
        {
            get { return direction; }
        }

        #endregion

        //==================================================================================
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Direction">The new direction that the wii remote is travelling</param>
        public AccelerationChangedEventArgs(Vector3D Dir)
            : base()
        {
            this.direction = Dir;
        }

        #endregion
    }
}
