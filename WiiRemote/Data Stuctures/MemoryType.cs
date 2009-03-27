using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// Enumerates the two different types of memory in the Wii Remote
    /// </summary>
    internal enum MemoryType : byte
    {
        EEPROM_Memory = 0x00,
        Register = 0x04
    }
}
