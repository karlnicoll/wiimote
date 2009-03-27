namespace WiiRemoteLib
{
    /// <summary>
    /// The format in which infrared data is sent.
    /// NOTE: This data format must match that of the reporting mode set. 
    /// <list type="bullets">
    /// <item>IRMode.Off requires that a reporting type be used that does not transmit IR data at all</item>
    /// <item>IRMode.Basic requires a reporting type that sends 10 bytes of IR data</item>
    /// <item>IRMode.Extended requires a reporting type that sends 12 bytes of IR data</item>
    /// <item>IRMode.Full requires a reporting type that sends 36 bytes of IR data (only available with InputReportType.CoreButtonsAccellIR36_x where "x" is a 1 or a 2</item>
    /// </list>
    /// </summary>
    public enum IRMode : byte
    {
        /// <summary>
        /// IR sensor off
        /// </summary>
        Off = 0x00,
        /// <summary>
        /// Basic mode
        /// </summary>
        Basic = 0x01,
        /// <summary>
        /// Extended mode
        /// </summary>
        Extended = 0x03,
        /// <summary>
        /// Full mode
        /// </summary>
        Full = 0x05,
    }

    /// <summary>
    /// Enumerates the possible sensitivity settings for the IR sensor
    /// </summary>
    public enum IRSensitivity : byte
    {
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5
    }
}
