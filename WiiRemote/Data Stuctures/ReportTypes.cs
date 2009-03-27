namespace WiiRemoteLib
{

    /// <summary>
    /// An enumeration of the possible incoming reports from the Wii Remote
    /// </summary>
    public enum InputReportType : byte
    {
        /// <summary>
        /// Information about the Wii remotes battery levels, etc.
        /// </summary>
        StatusInformation = 0x20,
        /// <summary>
        /// Data that was requested from EEPROM memory or a register
        /// </summary>
        MemRegData = 0x21,
        /// <summary>
        /// An Acknowledgement of a report that requires no response to inform us that the report was received in-tact
        /// </summary>
        Acknowledgement = 0x22,
        /// <summary>
        /// Information about the buttons on the Wii Remote
        /// </summary>
        CoreButtons = 0x30,
        /// <summary>
        /// Information about the buttons and accelerometer on the Wii Remote
        /// </summary>
        CoreButtonsAccel = 0x31,
        /// <summary>
        /// Information about the buttons and basic information from an attached extension controller on the Wii Remote
        /// </summary>
        CoreButtonsExten8 = 0x32,
        /// <summary>
        /// Information about the buttons and accelerometer, as well as 12 bytes describing the data from the IR sensor
        /// </summary>
        CoreButtonsAccelIR12 = 0x33,
        /// <summary>
        /// Information about the buttons, as well as 19 bytes of data from an extension controller attached to the Wii Remote
        /// </summary>
        CoreButtonsExten19 = 0x34,
        /// <summary> 
        /// Information about the buttons and accelerometer, as well as 16 bytes of data from the extention controller attached to the Wii Remote
        /// </summary>
        CoreButtonsAccelExten16 = 0x35,
        /// <summary>
        /// Information about the buttons, as well as 10 bytes describing the state of the IR sensor, and 9 bytes of data from the extension controller attached to the Wii Remote
        /// </summary>
        CoreButtonsIR10Exten9 = 0x36,
        /// <summary>
        /// Information about the buttons and accelerometer, as well as 10 bytes describing the state of the IR sensor, and 6 bytes of data from the extension controller attached to the Wii Remote
        /// </summary>
        CoreButtonsAccelIR10Exten6 = 0x37,
        /// <summary>
        /// 21 bytes of information from an extension controller attached to the Wii Remote
        /// </summary>
        Exten21 = 0x3D,
        /// <summary>
        /// Part one of a two packet report containing information about the buttons and accelerometer on the Wii remote, as well as 36 bytes detailing the information sent from the IR sensor
        /// </summary>
        CoreButtonsAccellIR36_1 = 0x3E,
        /// <summary>
        /// Part one of a two packet report containing information about the buttons and accelerometer on the Wii remote, as well as 36 bytes detailing the information sent from the IR sensor
        /// </summary>
        CoreButtonsAccellIR36_2 = 0x3F,
    }

    /// <summary>
    /// Describes the possible reports that can be sent to the Wii Remote
    /// </summary>
    public enum OutputReportType : byte
    {
        /// <summary>
        /// Tells the Wii Remote that the following report contains data for setting the LEDs on the hardware
        /// </summary>
        SetRemoteLEDs = 0x11,
        /// <summary>
        /// Tells the Wii Remote that the following report contains data for setting the output report mode
        /// </summary>
        SetDataReportingMode = 0x12,
        /// <summary>
        /// Tells the Wii Remote to enable or disable the IR camera based on what has been requested
        /// </summary>
        EnableIRCamera_1 = 0x13,
        EnableSpeaker = 0x14,
        StatusInfoRequest = 0x15,
        WriteMemReg = 0x16,
        ReadMemReg = 0x17,
        SendSpeakerData = 0x18,
        MuteSpeaker = 0x19,
        EnableIRCamera_2 = 0x1A

    }
}
