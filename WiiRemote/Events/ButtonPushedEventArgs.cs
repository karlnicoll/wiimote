using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    public class ButtonChangedEventArgs : EventArgs
    {
        //==================================================================================
        #region Private Variables

        private WiiRemoteButtons oldButtons;
        private WiiRemoteButtons newButtons;  //The new buttons

        #endregion
        
        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the battery level when this event was raised
        /// </summary>
        public WiiRemoteButtons NewButtons    
        {
            get { return newButtons; }
        }

        /// <summary>
        /// Gets the battery level when this event was raised
        /// </summary>
        public WiiRemoteButtons OldButtons
        {
            get { return oldButtons; }
        }

        #endregion

        //==================================================================================
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="NewBtns">The new button statuses for the Wii remote</param>
        /// <param name="OldBtns">The old button statuses</param>
        public ButtonChangedEventArgs(WiiRemoteButtons NewBtns, WiiRemoteButtons OldBtns)
            : base()
        {
            this.newButtons = NewBtns;
            this.oldButtons = OldBtns;
        }

        #endregion
    }
}
