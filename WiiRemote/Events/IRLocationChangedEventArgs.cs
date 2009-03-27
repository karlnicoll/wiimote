using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    public class IRLocationChangedEventArgs : EventArgs
    {
        //==================================================================================
        #region Private Variables

        private System.Drawing.PointF location;  //The battery level

        #endregion
        
        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the battery level when this event was raised
        /// </summary>
        public System.Drawing.PointF Location    
        {
            get { return location; }
        }

        #endregion

        //==================================================================================
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Loc">The new location of the pointer</param>
        public IRLocationChangedEventArgs(System.Drawing.PointF Loc)
            : base()
        {
            this.location = Loc;
        }

        #endregion
    }
}
