using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{

    /// <summary>
    /// Contains details about a report that has been sent to the Wii remote hardware requesting information about either A) register or B) EEPROM data
    /// </summary>
    public struct DataRequestDetails
    {
        /// <summary>
        /// The register (or Location in EEPROM memory) from where the data is being retrieved, as specified in the report
        /// </summary>
        public int RequestedDataAddress;
        /// <summary>
        /// The size of the data that will be returned
        /// </summary>
        public int RequestedDataSize;
    }
}
