using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// The possible values that the rumble 
    /// </summary>
    public enum RumbleState : byte
    {
        /// <summary>
        /// Represents that the Wii remote is vibrating
        /// </summary>
        On = 0x01,
        /// <summary>
        /// Represents that the Wii remote is NOT vibrating
        /// </summary>
        Off = 0x00
    }
}
