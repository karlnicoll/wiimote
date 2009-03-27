using System;
using System.Collections.Generic;
using System.Threading;
using HIDDevices;

namespace WiiRemoteLib
{
    /// <summary>
    /// A Class representing a Nintendo Wii Remote. Inherits the HIDDevices.HIDDevice Class
    /// </summary>
    public class WiiRemote : HIDDevice
    {
        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants

        #region Accelerometer constants

        /// <summary>
        /// Holds the location in EEPROM memory of the Acceleration calibration data
        /// </summary>
        private const short ACCEL_CALIBRATION_ADDR = 0x0016;
        /// <summary>
        /// The size of the repot that should be returned containing the accelerometer data
        /// </summary>
        private const short ACCEL_CALIBRATION_SIZE = 7;

        #endregion

        #region Infrared constants

        /// <summary>
        /// The location of the register that holds whether or not the infrared camera is enabled
        /// </summary>
        private const int IR_REGISTER_LOC_ENABLE = 0x00B00030;
        /// <summary>
        /// Holds the register address of the first sensitivity setting
        /// </summary>
        private const int IR_REGISTER_LOC_SENSITIVITY1 = 0x00B00000;
        /// <summary>
        /// Holds the register address of the second sensitivity setting
        /// </summary>
        private const int IR_REGISTER_LOC_SENSITIVITY2 = 0x00B0001A;
        /// <summary>
        /// The register that holds the IR reporting mode
        /// </summary>
        private const int IR_REGISTER_LOC_MODE = 0x00B00033;

        #endregion

        #region Wii Remote Details

        /// <summary>
        /// The Product ID of a valid Wii remote
        /// </summary>
        public const short WIIMOTE_PRODUCT_ID = 0x0306;
        /// <summary>
        /// The vendor ID of a Nintendo HID device
        /// </summary>
        public const short WIIMOTE_VENDOR_ID = 0x057E;

        /// <summary>
        /// The length of the reports that are sent to the wii remote.
        /// </summary>
        protected const int WIIMOTE_REPORT_LENGTH = 22;
        #endregion

        #endregion

        //==================================================================================
        #region Events

        /// <summary>
        /// The event that is raised when the Wii Remote sends out a Low Battery Warning.
        /// </summary>
        public event EventHandler<LowBatteryEventArgs> LowBatteryWarning;

        /// <summary>
        /// Event that is raised when a report is received that refreshes the buttons in this instance
        /// </summary>
        public event EventHandler<ButtonChangedEventArgs> ButtonStateChanged;

        /// <summary>
        /// Event that is raised when a report is received that refreshes the Accelerometer reading in this instance
        /// </summary>
        public event EventHandler<AccelerationChangedEventArgs> AccelerationChanged;

        /// <summary>
        /// This event is raised when the infrared location is refreshed
        /// </summary>
        public event EventHandler<IRLocationChangedEventArgs> IRReadingChanged;

        /// <summary>
        /// Event that is raised when the battery level and LED values are refreshed.
        /// </summary>
        /// <remarks>NOTE: Be aware that if the low battery warning event is raised,
        /// this will ALSO be raised as they are raised for different (although similar) events.</remarks>
        public event EventHandler<EventArgs> StatusChanged;

        #endregion

        //==================================================================================
        #region Private Variables

        #region Remote's Controls

        /// <summary>
        /// The accelerometer data for the Wii Remote
        /// </summary>
        private Accelerometer accelData;

        /// <summary>
        /// The state of the buttons on the Wii Remote
        /// </summary>
        private WiiRemoteButtons buttons;

        /// <summary>
        /// The normalised location of the IR sensor (i.e. between 0.00 and 1.00 on each axis
        /// </summary>
        private System.Drawing.PointF irLoc;

        #endregion

        #region Data Reading

        /// <summary>
        /// A linked list containing details about the data requested from the Wiimote
        /// </summary>
        private LinkedList<DataRequestDetails> dataRequestQueue;

        /// <summary>
        /// This buffer is used to hold an incoming data read, which may be split across multiple packets
        /// </summary>
        private List<byte> readBuffer;

        /// <summary>
        /// A Manual Reset Event variable which is used to hold up a method that is waiting for a data read to complete
        /// </summary>
        ManualResetEvent readBufferWait;
        #endregion

        #region Other Controller Data 

        /// <summary>
        /// The state of the LEDs on the Wii remote
        /// </summary>
        private LEDStates leds;

        /// <summary>
        /// Whether or not the Wii remote is rumbling
        /// </summary>
        private RumbleState rumble;

        /// <summary>
        /// The battery level in the Wii Remote
        /// </summary>
        private byte battLevel;

        #endregion

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Default constructor for the Wii Remote. Inherits some functionality from the HID device constructor
        /// </summary>
        public WiiRemote() : this("")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DevPath">The path to the WiiMote Device</param>
        public WiiRemote(string DevPath) : this(DevPath, false)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DevPath">The Path to the device</param>
        /// <param name="ConnectNow">When TRUE will attempt to connect this instance to the hardware that it points to (via the device path), when FALSE this instance will not attempt to connect</param>
        public WiiRemote(string DevPath, bool ConnectNow) : base(DevPath)
        {
            this.SetDefaults(ButtonState.Released, new Accelerometer(), new LinkedList<DataRequestDetails>(), new System.Drawing.PointF(0F, 0F), new LEDStates(), RumbleState.Off, 0);

            //Connect if requested
            if (ConnectNow) this.Connect();

            //Link the ReportReceived event with the parse method
            this.ReportReceived +=new EventHandler<HIDDeviceReportReceivedEventargs>(WiiRemote_ReportReceived);
        }

        #endregion

        //==================================================================================
        #region Public Properties

        #region Accelerometer and Infrared

        /// <summary>
        /// Gets or Sets the Accelerometer's calibration info.
        /// <para>NOTE: This has no impact on the hardware. For safety reasons writing new Accelerometer calibration information to the Wii Remote is not supported.</para>
        /// </summary>
        public AccelerometerCalibrationInfo AccelerometerCalibration    
        {
            get { return accelData.CalibrationInfo; }
            set { accelData.CalibrationInfo = value; }
        }

        /// <summary>
        /// Gets the last reading of the Accelerometer. If no data has been read from the accelerometer, then a Vector3D of {0,0,0} is returned.
        /// </summary>
        public Vector3D AccelerometerReading                            
        {
            get { return accelData.LastReading; }
        }


        /// <summary>
        /// Gets the last reading from the Infrared sensor. The reading is normalized, therefore a value of {0, 0} represents the extreme top-left of the IR area, and a value of {1, 1} represents the extreme bottom right of the IR area.
        /// </summary>
        public System.Drawing.PointF IRCoordinates                      
        {
            get { return irLoc; }
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Gets the currently known state of the buttons on the hardware. If no button data has been received, then the buttons are all set to ButtonState.Released or ButtonState.Pressed, depending on how the class was initialized
        /// </summary>
        public WiiRemoteButtons Buttons     
        {
            get { return buttons; }
        }

        #endregion

        #region LEDs, Rumble and Battery

        /// <summary>
        /// Gets or Sets the state of the LEDs.
        /// </summary>
        public LEDStates LEDs       
        {
            get { return leds; }
            set { SetLEDs(value); }
        }

        /// <summary>
        /// Gets or sets whether or not the Wiimote is vibrating
        /// </summary>
        public RumbleState Rumble   
        {
            get { return rumble; }
            set { SetRumble(value); }
        }

        /// <summary>
        /// Gets the last known battery level for this Wii Remote
        /// </summary>
        public byte BatteryLevel    
        {
            get { return battLevel; }
        }

        #endregion

        #endregion

        //==================================================================================
        #region Public Methods

        #region Wii Remote Connection

        /// <summary>
        /// Connect to the Wii Remote.
        /// </summary>
        /// <exception cref="NotAWiiRemoteException">Thrown if the device we are connecting to does not identify itself as a Wii Remote</exception>
        /// <remarks>Overrides HIDDevice.Connect()</remarks>
        public new void Connect()   
        {
            //Get the attributes of this Wii Remote (so that we can formally identify it as a Wii Remote
            base.SetDeviceAttributes();

            if (this.ProductID != WIIMOTE_PRODUCT_ID || this.VendorID != WIIMOTE_VENDOR_ID)
            {
                throw new NotAWiiRemoteException("Could not donnect to Wii Remote. Is the device path correct?", this, null);
            }
            else
            {
                //Connect
                base.Connect();
                this.SetInputReportFormat(InputReportType.CoreButtons, false);
                this.StartReadingData(false);
                this.RequestStatus();
            }

        }

        #endregion

        #region Data Requests

        #region Status Request
        /// <summary>
        /// Sends a request to to the hardware to send it's status to us. The status will
        /// arrive as an input report which will be parsed when it arrives
        /// </summary>
        /// <exception cref="WiiRemoteNotConnectedException">Thrown if the Wii remote is not connected, and the status can not be fetched as a result</exception>
        public void RequestStatus()                     
        {
            //Check to see if we are connected.
            if (!this.IsConnected)
            {
                throw new WiiRemoteNotConnectedException("The status of this remote cannot be gotten because the instance is not connected to the hardware. Make sure you perform the 'Connect()' method before this.");
            }
            else
            {
                //Send the report to request the status
                SendReport(new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.StatusInfoRequest, new byte[1] { GetRumbleBit() }));
            }
        }
        /// <summary>
        /// Sends a request to to the hardware to send it's status to us. The status will
        /// arrive as an input report which will be parsed when it arrives
        /// </summary>
        /// <param name="FailSilently">If this is set to TRUE, then if the status request fails for any reason, then nothing will happen. If this is set to FALSE, then if the method fails for any reason, then an exception will be raised.</param>
        /// <exception cref="WiiRemoteNotConnectedException">Thrown if the Wii remote is not connected, and the status can not be fetched as a result</exception>
        public void RequestStatus(bool FailSilently)    
        {
            //Check to see if we are connected
            if (FailSilently)
            {
                try
                {
                    RequestStatus();
                }
                catch { }
            }
            else
            {
                RequestStatus();
            }
        }
        #endregion

        #endregion

        #region Wii Remote Setup

        /// <summary>
        /// Tells the hardware how to send the data. Choosing a report mode that sends IR data will enable the IR sensor,
        /// and choosing a report mode that sends accelerometer data will calibrate the accelerometer (if required).
        /// </summary>
        /// <param name="Type">The format of the report</param>
        /// <param name="ContinuousMode">When set to TRUE, the Wii Remote will constantly send </param>
        /// <remarks>Note that the following InputReportType members are invalid for this method
        /// <list type="bullet">
        /// <item>InputReportType.StatusInformation, </item>
        /// <item>InputReportType.MemRegData,</item>
        /// <item>InputReportType.Acknowledgement</item>
        /// </list>
        /// </remarks>
        public void SetInputReportFormat(InputReportType Type, bool ContinuousMode)
        {
            if (Type == InputReportType.StatusInformation ||
                Type == InputReportType.MemRegData ||
                Type == InputReportType.Acknowledgement
               )
            {
                throw new IncorrectReportException("The chosen exception is not a valid control reporting mode", (byte)Type);
            }
            else if (!this.IsConnected)
            {
                throw new WiiRemoteNotConnectedException("The Wii remote is not connected");
            }
            else
            {
                //Send the report to change the mode
                HIDDeviceReport report = new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.SetDataReportingMode, new byte[2]);
                report.Content[0] = (byte)((ContinuousMode ? 0x04 : 0x00) | GetRumbleBit());
                report.Content[1] = (byte)Type;

                

                //Perform special tasks based on what features are going to be reporting

                //Calibrate the accelerometers if required
                if (accelData.CalibrationInfo.IsEmpty() &&
                    (Type == InputReportType.CoreButtonsAccel ||
                     Type == InputReportType.CoreButtonsAccelExten16 ||
                     Type == InputReportType.CoreButtonsAccelIR10Exten6 ||
                     Type == InputReportType.CoreButtonsAccelIR12 ||
                     Type == InputReportType.CoreButtonsAccellIR36_1 ||
                     Type == InputReportType.CoreButtonsAccellIR36_2
                    )
                   )
                {
                    PerformCalibration();
                }

                //Enable/Disable the IR sensor if required
                //----------------------------------------
                //Enable Basic IR mode (10 bytes of IR data returned)
                if (Type == InputReportType.CoreButtonsAccelIR10Exten6 ||
                    Type == InputReportType.CoreButtonsIR10Exten9
                   )
                {
                    EnableInfrared(IRMode.Basic, IRSensitivity.Level_3);
                }

                //Enable Extended IR mode (12 bytes of IR data returned)
                else if (Type == InputReportType.CoreButtonsAccelIR12)
                {
                    EnableInfrared(IRMode.Extended, IRSensitivity.Level_3);
                }

                //Enable Full IR mode (36 bytes of IR data returned in 2 separate reports)
                else if (Type == InputReportType.CoreButtonsAccellIR36_1 ||
                         Type == InputReportType.CoreButtonsAccellIR36_2
                        )
                {
                    EnableInfrared(IRMode.Full, IRSensitivity.Level_3);
                }

                //Disable Infrared
                else
                {
                    DisableInfrared();
                }

                SendReport(report);



            }
        }

        /// <summary>
        /// Calibrates the Accelerometer
        /// </summary>
        public void CalibrateAccelerometer()
        {
            PerformCalibration();
        }

        #endregion

        #endregion

        //==================================================================================
        #region Private/Protected Methods

        #region Instance Initialization

        /// <summary>
        /// Sets all the variables in the class simultaneously, used by constructors.
        /// </summary>
        /// <param name="ButtonsInitialState">The initial ButtonState of the buttons on the remote</param>
        /// <param name="InitialAccel">The initial value of the accelerometer</param>
        /// <param name="DRL">The Data Request List</param>
        /// <param name="InitialIRLoc">The initial value of the IR sensor</param>
        /// <param name="InitialLEDs">The Inital setting of the LEDs</param>
        /// <param name="InitialRumble">The initial state of the Wii Remote's rumble motor</param>
        /// <param name="BattLevel">The initial Battery Level</param>
        private void SetDefaults(ButtonState ButtonsInitialState, Accelerometer InitialAccel, LinkedList<DataRequestDetails> DRL, System.Drawing.PointF InitialIRLoc, LEDStates InitialLEDs, RumbleState InitialRumble, byte BattLevel)
        {
            this.buttons = new WiiRemoteButtons(ButtonsInitialState);
            this.accelData = InitialAccel;
            this.dataRequestQueue = DRL;
            this.irLoc = InitialIRLoc;
            this.leds = InitialLEDs;
            this.rumble = InitialRumble;
            this.battLevel = BattLevel;
            this.reportLength = WIIMOTE_REPORT_LENGTH;

        }

        #endregion

        #region Status Requesting

        /// <summary>
        /// This function is used to Request the status of the hardware when the status request timer elapses
        /// </summary>
        /// <param name="sender">The object that invoked the event that resulted in this method being executed.</param>
        /// <param name="e">The event arguments</param>
        void statusRefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)    
        {
            RequestStatus();
        }

        #endregion

        #region LED reading/setting

        /// <summary>
        /// Sets the LEDs in the class, and if the remote is connected, also illuminates the corresponding LEDs on the Wii remote
        /// </summary>
        /// <param name="value">The LED values to set</param>
        private void SetLEDs(LEDStates value)                   
        {
            //Set the LED values locally
            leds = value;

            //If we are connected, then send the state of the LEDs to the remote
            if (this.IsConnected)
            {
                SendLEDStateToHardware(value);
            }
        }

        /// <summary>
        /// Sends the chosen LED states to the Wii Remote
        /// </summary>
        /// <param name="value">How the LEDs should be set on the Wii Remote</param>
        private void SendLEDStateToHardware(LEDStates value)    
        {

            //Initialize a report to send
            HIDDeviceReport sendRep = new HIDDeviceReport(GetNextUniqueReportID());

            //Set the report type
            sendRep.Type = (byte)OutputReportType.SetRemoteLEDs;

            //Set the report contents
            sendRep.Content = new byte[1];

            //Set the values of the LEDs. This is a bitwise operation.
            /* The report format for setting LEDs is: [11][LL]
             * The first L represents the most significant bits of the second byte of the report
             * The 4 MSBs of this byte represent the 4 leds. For Example a binary represented
             * byte like this: 10110000
             * tells us that the first third and fourth LEDs should be illuminated, and the second
             * one should not. The code below formats the second report byte like this.
             *
             * NOTE:    The last bit of this line of code also sets the rumble bit, as is required! 
             */
            sendRep.Content[0] = (byte)
                                    ((value.LED_1 == LEDState.Illuminated ? 0x10 : 0x00) |
                                     (value.LED_2 == LEDState.Illuminated ? 0x20 : 0x00) |
                                     (value.LED_3 == LEDState.Illuminated ? 0x40 : 0x00) |
                                     (value.LED_4 == LEDState.Illuminated ? 0x80 : 0x00) |
                                     GetRumbleBit()
                                    );

            //Send the report to the Wii remote
            SendReport(sendRep);
            

        }

        #endregion

        #region Rumble reading / setting

        /// <summary>
        /// The rumble bit is the least significant bit on the second byte of EVERY report. The result of this function should be "or'd" with the first bit of
        /// the report being sent.
        /// </summary>
        /// <returns>0x01 if the Wii remote should be rumbling, 0x00 otherwise</returns>
        private byte GetRumbleBit() 
        {
            return (byte)rumble;
        }

        /// <summary>
        /// Sets the value of the Rumble feature of the Wii remote
        /// </summary>
        /// <param name="value">The rumble state of the Wii remote</param>
        private void SetRumble(RumbleState value)
        {
            rumble = value;

            // HACK
            /* There is no "Set Rumble" report for the Wii remote, therefore to ensure that the rumble feature
             * starts or stops NOW, we need to set the LEDs. The LEDs won't change, but we need to hijack the
             * LED setting report in order to set the rumble state on the hardware.
             * Not ideal, but we are limited by the hardware :-(
             */
            if (IsConnected) SendLEDStateToHardware(leds);
        }

        #endregion

        #region Calibration

        /// <summary>
        /// Fetches the Accelerometer data from the hardware
        /// </summary>
        protected void PerformCalibration()
        {
            //Get the raw accelerometer data
            byte[] accelCalibrationRaw = this.ReadData(ACCEL_CALIBRATION_ADDR, MemoryType.EEPROM_Memory, ACCEL_CALIBRATION_SIZE);

            //Set the calibration data
            /* NOTE:    Notice only 6 of the 7 returned bytes are used in parsing the calibration information.
             *          This is because byte 3 in the returned data doesn't seem to have a purpose in the
             *          calibration procedure, therefore we need to ignore it
             */
            this.accelData.CalibrationInfo = ParseAccelerometerCalibrationInformation(new byte[] { accelCalibrationRaw[0], accelCalibrationRaw[1], accelCalibrationRaw[2], accelCalibrationRaw[4], accelCalibrationRaw[5], accelCalibrationRaw[6] });
        }

        #endregion

        #region Data Parsing - Major (Report Parsing)

        #region Report Type Identification

        /// <summary>
        /// This catches the report that was received from the Wii remote, and begins the parsing procedure
        /// </summary>
        /// <param name="sender">The FileStream from which the report came</param>
        /// <param name="e">The Event arguments, containing the report that was received</param>
        private void  WiiRemote_ReportReceived(object sender, HIDDeviceReportReceivedEventargs e)
        {
 	        ParseIncomingReport(e.Report);
        }

        /// <summary>
        /// Parses a report that has come from the hardware, and makes the appropriate changes to this instance to reflect the changes
        /// </summary>
        /// <param name="Report">The report to parse</param>
        protected void ParseIncomingReport(HIDDeviceReport Report)
        {
            switch ((InputReportType)Report.Type)
            {
                //Do nothing for acknowledgements
                case InputReportType.Acknowledgement:
                    break;

                //Incoming button data
                case InputReportType.CoreButtons:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));

                        break;
                    }

                //Incoming buttons and accelerometer data
                case InputReportType.CoreButtonsAccel:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        this.accelData.SetLastReading(ParseAccelerometerReading(Report));

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        if (AccelerationChanged != null) AccelerationChanged(Report, new AccelerationChangedEventArgs(accelData.LastReading));

                        break;
                    }

                //Buttons, Accelerometer, and 16 extension bytes
                case InputReportType.CoreButtonsAccelExten16:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        this.accelData.SetLastReading(ParseAccelerometerReading(Report));
                        //EXTENSION PARSING HERE!

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        if (AccelerationChanged != null) AccelerationChanged(Report, new AccelerationChangedEventArgs(accelData.LastReading));
                        //EXTENSION EVENT HERE

                        break;
                    }

                //Buttons, accelerometer, 10 IR bytes, 6 Extension bytes
                case InputReportType.CoreButtonsAccelIR10Exten6:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        this.accelData.SetLastReading(ParseAccelerometerReading(Report));
                        this.irLoc = ParseIRData(Report);
                        //EXTENSION PARSING HERE

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        if (AccelerationChanged != null) AccelerationChanged(Report, new AccelerationChangedEventArgs(accelData.LastReading));
                        if (IRReadingChanged != null) IRReadingChanged(Report, new IRLocationChangedEventArgs(irLoc));
                        //EXTENSION EVENT HERE

                        break;
                    }


                //Buttons, accelerometer, 12IR bytes
                case InputReportType.CoreButtonsAccelIR12:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        this.accelData.SetLastReading(ParseAccelerometerReading(Report));
                        this.irLoc = ParseIRData(Report);

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        if (AccelerationChanged != null) AccelerationChanged(Report, new AccelerationChangedEventArgs(accelData.LastReading));
                        if (IRReadingChanged != null) IRReadingChanged(Report, new IRLocationChangedEventArgs(irLoc));

                        break;
                    }

                //Interleaved report (1)
                case InputReportType.CoreButtonsAccellIR36_1:
                    {
                        //TODO: Implement Interleaved Report parsing
                        throw new NotImplementedException("Not supported... yet.");
                        break;
                    }

                //Interleaved report (2)
                case InputReportType.CoreButtonsAccellIR36_2:
                    {
                        //TODO: Implement Interleaved Report parsing
                        throw new NotImplementedException("Not supported... yet.");
                        break;
                    }

                //Buttons, 19 Extension bytes
                case InputReportType.CoreButtonsExten19:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        //EXTENSION PARSING HERE

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        //EXTENSION EVENT HERE

                        break;
                    }

                //Buttons, 8 Extension bytes
                case InputReportType.CoreButtonsExten8:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        //EXTENSION PARSING HERE

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        //EXTENSION EVENT HERE

                        break;
                    }

                //Buttons, 10 IR bytes, 9 Extension bytes
                case InputReportType.CoreButtonsIR10Exten9:
                    {
                        WiiRemoteButtons oldButtonLayout = new WiiRemoteButtons(buttons);
                        this.buttons = ParseButtonsInfo(Report);
                        this.irLoc = ParseIRData(Report);
                        //EXTENSION PARSING HERE

                        //Throw events
                        if (ButtonStateChanged != null) ButtonStateChanged(Report, new ButtonChangedEventArgs(buttons, oldButtonLayout));
                        if (IRReadingChanged != null) IRReadingChanged(Report, new IRLocationChangedEventArgs(irLoc));
                        //EXTENSION EVENT HERE

                        break;
                    }

                //21 Extension Bytes only
                case InputReportType.Exten21:
                    {
                        //EXTENSION PARSING HERE

                        //Throw events
                        //EXTENSION EVENT HERE

                        break;
                    }

                //Data that has been requested from EEPROM memory or a register
                case InputReportType.MemRegData:
                    {
                        this.ParseDataRead(Report);
                        break;
                    }

                //Buttons, accelerometer, 10 IR bytes, 6 Extension bytes
                case InputReportType.StatusInformation:
                    {
                        this.ParseStatus(Report);

                        //Throw events
                        if (StatusChanged != null) StatusChanged(Report, EventArgs.Empty);
                        break;
                    }

                //Other reports
                default:
                    {
                        throw new IncorrectReportException("Report not recognised", Report.Type);
                        break;
                    }

            }
        }

        #endregion

        #region Specialist Parsing

        /// <summary>
        /// Parses a Status Report and sets the appropriate properties of this class to reflect
        /// any changes in the status of the Wii remote such as LED configuration and battery level.
        /// </summary>
        /// <param name="Report">The Status Report to Parse</param>
        protected void ParseStatus(HIDDeviceReport Report)      
        {
            this.buttons = ParseButtonsInfo(Report);
            this.battLevel = Report.Content[5];
            
            //Parse LEDs. The LED status is obtained by using the "AND" bitwise operator
            //to see if the LED is illuminated. If the "AND" operation returns 0, the led
            //is off, otherwise it is illuminated.
            //The format of this can be found at: http://www.wiibrew.org/wiki/Wiimote#0x20:_Status
            //NOTE: As Brian Peek said in his version of this class, there isn't really much need
            //      to do this as the LED's shouldn't be able to become unsynchronised, however the
            //      check is in place anyway.
            this.leds.LED_1 = ((Report.Content[2] & 0x10) != 0 ? LEDState.Illuminated : LEDState.Off);
            this.leds.LED_2 = ((Report.Content[2] & 0x20) != 0 ? LEDState.Illuminated : LEDState.Off);
            this.leds.LED_3 = ((Report.Content[2] & 0x40) != 0 ? LEDState.Illuminated : LEDState.Off);
            this.leds.LED_4 = ((Report.Content[2] & 0x80) != 0 ? LEDState.Illuminated : LEDState.Off);

            //Check the battery level is not low
            if ((Report.Content[2] & 0x01) == 0 && LowBatteryWarning != null)
            {
                //Raise an event to announce that the battery level is low.
                LowBatteryWarning(Report.ToRaw(), new LowBatteryEventArgs(Report.Content[5]));
            }


            //STATUS STUFF NOT DONE!
            //-- EXTENSION CONNECTED
            //-- SPEAKER ENABLED
            //-- IR CAM ENABLED

        }

        /// <summary>
        /// Parses a data report that contains data that has been requested from either a register or EEPROM memory
        /// This method will assemble the data that has been requested, and may get called more than once for a
        /// single data read. This is because a single data read may require more than 16 bytes of information to be
        /// returned (16 bytes is the most data that a data report can carry) therefore the data will be split into
        /// several data "packets". The readBuffer variable is used to assemble the report from the packets, and when
        /// the final packet is received, the readBufferWait thread blocker will be "Set" which tells any waiting threads
        /// that all the data has arrived.
        /// </summary>
        /// <param name="Report">The report containing a packet of the requested information</param>
        protected void ParseDataRead(HIDDeviceReport Report)    
        {
            //First things first, check to see if we have requested a report first!
            if (dataRequestQueue.Count > 0)
            {
                //Check to make sure we have the correct packet
                if (Report.Type != (byte)InputReportType.MemRegData)
                {
                    throw new IncorrectReportException("Report must be of type 0x21 [Read  Memory and Registers Data]");
                }
                else
                {
                    //Check for an error in the packet
                    //================================

                    /* NOTE:    Known Error Codes:
                     *          0x00:   No Error
                     *          0x07:   Attempted read from write-only register
                     *          0x08:   Attempted read from non-existant memory
                     */
                    byte errorCode = (byte)(0x0F & Report.Content[2]);
                    switch (errorCode)
                    {
                        case 7: throw new ReportErrorException("Attempted to read from write-only register", errorCode);
                            break;
                        case 8: throw new ReportErrorException("Attempted to read from non-existant memory", errorCode);
                            break;
                        default: if (errorCode != 0)
                                throw new ReportErrorException("Attempted to read from non-existant memory", errorCode);
                            break;
                    }


                    //Parse the button data
                    //=====================

                    this.buttons = ParseButtonsInfo(Report);


                    //Assemble the report
                    //===================

                    //Get size of packet data (the plus one is because the size is always reported minus 1)
                    int size = (Report.Content[2] >> 4) + 1;

                    //Parse offset data (the right side of this equation merges the two offset giving bytes together to create a short)
                    int offset = (Report.Content[3] << 8 | Report.Content[4]);

                    //Add the data items to the buffer
                    for (int i = 0; i < size; i++)
                    {
                        readBuffer.Add(Report.Content[5 + i]);
                    }

                    //Check to see if we are at the end of the expected packets by comparing the addresses
                    if (offset + size == dataRequestQueue.First.Value.RequestedDataAddress + dataRequestQueue.First.Value.RequestedDataSize)
                    {
                        //If true, then we have received all the data so we must...

                        //1) Remove the output report data from the front of the data request queue
                        dataRequestQueue.RemoveFirst();

                        //2) Set the buffer wait Manual reset event to allow the function that made the call for this data to use the data
                        readBufferWait.Set();

                    }
                }
            }
            else
            {
                readBuffer.Clear();
            }

        }

        #endregion

        #endregion

        #region Data Parsing - Minor (Parsing of report sections)

        #region Button Data Parsing

        /// <summary>
        /// Parses button data from a report that contains button data
        /// </summary>
        /// <param name="Report">The report to get the button data from</param>
        /// <returns>A WiiRemoteButtons instance that contains the status of all the buttons, as described in the report.</returns>
        protected WiiRemoteButtons ParseButtonsInfo(HIDDeviceReport Report)
        {
            return ParseButtonsInfo(Report.ToRaw());
        }

        /// <summary>
        /// Parses button data from a report that contains button data
        /// </summary>
        /// <param name="Report">The raw byte array that makes up the report</param>
        /// <returns>A WiiRemoteButtons instance that contains the status of all the buttons, as described in the report.</returns>
        /// <exception cref="IncorrectReportException">Thrown when the report received is of an unexpected type</exception>
        protected WiiRemoteButtons ParseButtonsInfo(byte[] Report)
        {

            //Variables
            WiiRemoteButtons returnedValue; //Holds the button values


            //Perform Report Validation to make sure that we have a report that contains button data
            if (Report[0] == (byte)InputReportType.CoreButtons ||
                Report[0] == (byte)InputReportType.CoreButtonsAccel ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelExten16 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelIR10Exten6 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelIR12 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccellIR36_1 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccellIR36_2 ||
                Report[0] == (byte)InputReportType.CoreButtonsExten19 ||
                Report[0] == (byte)InputReportType.CoreButtonsExten8 ||
                Report[0] == (byte)InputReportType.CoreButtonsIR10Exten9 ||
                Report[0] == (byte)InputReportType.StatusInformation ||
                Report[0] == (byte)InputReportType.MemRegData
               )
            {

                //Initialize the button class
                returnedValue = new WiiRemoteButtons(ButtonState.Released);


                /* NOTE:    The button data is always stored in bytes 1 and 2 of the report
                 *          containing byte data
                 */

                //Set the values of each of the buttons. This is done by performing an "AND" operation
                //on the appropriate byte and the bit that represents the button. All mappings can be
                //found at: http://www.wiibrew.org/wiki/Wiimote#Buttons
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_A, ((Report[2] & 0x08) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_B, ((Report[2] & 0x04) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_UP, ((Report[1] & 0x08) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_RIGHT, ((Report[1] & 0x02) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_DOWN, ((Report[1] & 0x04) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_LEFT, ((Report[1] & 0x01) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_MINUS, ((Report[2] & 0x10) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_HOME, ((Report[2] & 0x80) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_PLUS, ((Report[1] & 0x10) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_1, ((Report[2] & 0x02) != 0 ? ButtonState.Pressed : ButtonState.Released));
                returnedValue.SetButton(WiiRemoteButtons.BUTTON_2, ((Report[2] & 0x01) != 0 ? ButtonState.Pressed : ButtonState.Released));
            }
            else
            {
                returnedValue = this.Buttons;
                throw new IncorrectReportException("Wrong Report Type Received, should be one of the \"core buttons\" reports (including extended reports containing Accelerometer, IR or extension information) or a status report.", Report[0]);
            }

            //Now that we have set all the buttons, return them to the caller
            return returnedValue;
        }

        #endregion

        #region Accelerometer Data Parsing

        /// <summary>
        /// Uses some data read from the Wii remote to obtain the calibration information for the accelerometer
        /// </summary>
        /// <param name="CalibrationData"></param>
        /// <returns></returns>
        protected AccelerometerCalibrationInfo ParseAccelerometerCalibrationInformation(byte[] CalibrationData)
        {
            //Create the variable that we will return
            AccelerometerCalibrationInfo retVal = new AccelerometerCalibrationInfo();

            if (CalibrationData.Length != 6)
            {
                throw new InvalidDataException("Incorrect Data Size.\nExpected: 6 bytes\nReceived: " + CalibrationData.Length.ToString() + " bytes");
            }
            else
            {
                //Zero-Offsets
                retVal.ZeroPointX = CalibrationData[0];
                retVal.ZeroPointY = CalibrationData[1];
                retVal.ZeroPointZ = CalibrationData[2];

                //G Values
                retVal.GValueX = CalibrationData[3];
                retVal.GValueY = CalibrationData[4];
                retVal.GValueZ = CalibrationData[5];
            }

            return retVal;

        }


        /// <summary>
        /// Parses an accelerometer reading from a report
        /// </summary>
        /// <param name="Report">The report to parse the accelerometer data from</param>
        /// <returns>A 3-dimensional vector containing the speed of acceleration on the X, Y, and Z axes</returns>
        protected Vector3D ParseAccelerometerReading(HIDDeviceReport Report)
        {
            return ParseAccelerometerReading(Report.ToRaw());
        }

        /// <summary>
        /// Parses an accelerometer reading from a report
        /// </summary>
        /// <param name="Report">The report to parse the accelerometer data from</param>
        /// <returns>A 3-dimensional vector containing the speed of acceleration on the X, Y, and Z axes</returns>
        protected Vector3D ParseAccelerometerReading(byte[] Report)
        {
            //Create the value we will be returning
            Vector3D retVal = new Vector3D();

            //Check that the report contains accelerometer data 
            if (Report[0] == (byte)InputReportType.CoreButtonsAccel ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelExten16 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelIR10Exten6 ||
                Report[0] == (byte)InputReportType.CoreButtonsAccelIR12
               )
            {
                /* NOTE:    The accelerometer readings are quite complex beasts.
                 *          For the X-Axis, 8 of the bits are stored in byte 3 of the array;
                 *          however the remaining two bits (The X-Axis is a 10-bit reading)
                 *          are embedded as bits 5 and 6 in byte 1 of the report.
                 *          For the Y and Z axes, there is only one additional bit (Y/Z axes
                 *          are 9-bit readings), and these bits are stored in bits 5 and 6 respectively
                 *          inside byte 2.
                 *          For consistency purposes, all readings are considered to be 10 bytes long,
                 *          and the least significant bit of the X and Y axes are left as zeroes
                 */

                //Get X-Axis reading
                int xReading = (int)Report[3];
                int yReading = (int)Report[4];
                int zReading = (int)Report[5];

                /* NOTE:    The following code does not work. According to the WiiBrew Wii Remote page, the X, Y, and Z use
                 *          their bytes as well as 1-2 bits from unused button bits, however trying to use the unused button
                 *          bits resulted in weird readings. The above code seems to give accurate reading, so that is what sticks.
                 *          Maybe I'm reading the article wrong?
                 */
                //int xReading = (int)(((int)Report[3] << 2) | (((int)Report[1] & 0x00000060) >> 5));     //The actual X-Axis reading
                //int yReading = (int)(((int)Report[4] << 2) | (((int)Report[2] & 0x00000020) >> 4));     //The actual Y-Axis reading
                //int zReading = (int)(((int)Report[5] << 2) | (((int)Report[2] & 0x00000040) >> 5));     //The actual Z-Axis reading

                //Manipulate the readings to get the G values. method = (reading - zero_offset) / (g_value - zero_offset)
                retVal.X = (xReading - accelData.CalibrationInfo.ZeroPointX) / (accelData.CalibrationInfo.GValueX - accelData.CalibrationInfo.ZeroPointX);
                retVal.Y = (yReading - accelData.CalibrationInfo.ZeroPointY) / (accelData.CalibrationInfo.GValueY - accelData.CalibrationInfo.ZeroPointY);
                retVal.Z = (zReading - accelData.CalibrationInfo.ZeroPointZ) / (accelData.CalibrationInfo.GValueZ - accelData.CalibrationInfo.ZeroPointZ);

            }
            else
            {
                //Throw an exception if we get here because the report is not one that contains accelerometer data
                /* NOTE:    The exception is the interleaved report which DOES contain accelerometer data, but a specialized
                 *          overload of this function is used to interpret accelerometer readings coming in in interleaved
                 *          reports
                 */
                throw new IncorrectReportException("Report does not contain Acceleration data", Report[0]);
            }

            return retVal;

        }

        #endregion

        #region IR parsing

        /// <summary>
        /// Parses IR data from a report
        /// </summary>
        /// <param name="Report">The report to parse</param>
        /// <returns>A Point representing the normalised location of the remote within the bounds of the IR sensor bar</returns>
        protected System.Drawing.PointF ParseIRData(HIDDeviceReport Report)
        {
            return ParseIRData(Report.ToRaw());
        }

        /// <summary>
        /// Parses IR data from a report
        /// </summary>
        /// <param name="Report">The report to parse</param>
        /// <returns>A Point representing the normalised location of the remote within the bounds of the IR sensor bar</returns>
        protected System.Drawing.PointF ParseIRData(byte[] Report)
        {
            // TODO: The ParseIRData method must be implemented
            throw new NotImplementedException("Not Supported");
        }


        #endregion

        #region Extension Parsing

        //TODO: Implement Extension Parsing

        #endregion

        #endregion

        #region EEPROM/Register Reading/Writing

        /// <summary>
        /// Fetches some data from a register OR EEPROM memory.<br/>
        /// WARNING: This is a SYCHRONOUS function which waits for the data to arrive before continuing.
        ///          This is counter-intuitive to how packet reading is generally done.
        /// </summary>
        /// <param name="MemoryAddress">The address from where the data should be retrieved</param>
        /// <param name="Memory">Which type of memory the data should bet fetched from</param>
        /// <param name="ResultLength">How many bytes to return in the report. This should be the amount of data you want. For example, if you want 6 bytes of data from memory, set this to 6.</param>
        /// <exception cref="WiiRemoteNotConnectedException">Thrown if the Wii Remote is not connected to this instance.</exception>
        private byte[] ReadData(int MemoryAddress, MemoryType Memory, short ResultLength)
        {
            if (!this.IsConnected)
            {
                throw new WiiRemoteNotConnectedException("Wii Remote is not connected to this instance, please make sure you have used the Connect() method.");
            }
            else if (!this.IsAsyncReading)
            {
                throw new WiiRemoteNotConnectedException("Reports sent from the Wii remote are not being parsed. In order to parse reports, the StartReadingData(bool) method must be called.");
            }
            else
            {

                HIDDeviceReport report = new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.ReadMemReg, new byte[6]);

                //The first byte is the address space we are referring to
                report.Content[0] = (byte)((byte)Memory | GetRumbleBit());

                //Set the offset (the address we are getting the data from)
                //In order to set the offset, we need to break the integer into 4 bytes
                //(the Most significant byte we will not use). This is done by moving the bits
                //as shown below
                report.Content[1] = (byte)((MemoryAddress & 0x00ff0000) >> 16);
                report.Content[2] = (byte)((MemoryAddress & 0x0000ff00) >> 8);
                report.Content[3] = (byte)((MemoryAddress & 0x000000ff));
                
                //The final two bytes represent the size of the data we are retrieving
                report.Content[4] = (byte)((ResultLength & 0xff00) >> 8);
                report.Content[5] = (byte)(ResultLength & 0x00ff);

                //Initialize the data read buffer.
                /* NOTE:    Notice that there is only one data read buffer, even though multiple data read requests
                 *          can be pending at any one time. This is because normal transmission from the Wii remote
                 *          is put on hold while sending data, so that the data packets all arrive sequentially.
                 */
                readBuffer = new List<byte>(ResultLength);

                //Add this request to the request list and send the report
                DataRequestDetails dr = new DataRequestDetails();
                dr.RequestedDataAddress = MemoryAddress;
                dr.RequestedDataSize = ResultLength;
                dataRequestQueue.AddLast(dr);
                SendReport(report);

                //Wait for the read to complete or for 2 seconds, which ever comes sooner
                readBufferWait = new ManualResetEvent(false);
                if (!readBufferWait.WaitOne(5000, false))
                {
                    throw new WiiRemoteTimeoutException("Timeout while waiting for Wii remote to respond");
                }

                //Clean up the buffer
                byte[] retVal = readBuffer.ToArray();
                readBuffer.Clear();

                //Return the resulting data array
                return retVal;

            }
        }

        /// <summary>
        /// Writes data to a specfied region of EEPROM memory or register
        /// </summary>
        /// <param name="MemoryAddress">The address that the data will be written to</param>
        /// <param name="Memory">Which type of memory should be written</param>
        /// <param name="Data">The data to send. This MUST be &lt;= 16 bytes in length. If you need to send more than 16 bytes, split the data into sets of 16, and iteratively call this function for each set</param>
        /// <exception cref="WiiRemoteNotConnectedException">Thrown if the Wii Remote is not connected to this instance.</exception>
        private void WriteData(int MemoryAddress, MemoryType Memory, byte[] Data)
        {
            if (!this.IsConnected)
            {
                throw new WiiRemoteNotConnectedException("Wii Remote is not connected to this instance, please make sure you have used the Connect() method.");
            }
            else
            {

                HIDDeviceReport report = new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.WriteMemReg, new byte[reportLength - 1]);

                //The first byte is the address space we are referring to
                report.Content[0] = (byte)((byte)Memory | GetRumbleBit());

                //Set the offset (the address we are getting the data from)
                //In order to set the offset, we need to break the integer into 4 bytes
                //(the Most significant byte we will not use). This is done by moving the bits
                //as shown below
                report.Content[1] = (byte)((MemoryAddress & 0x00ff0000) >> 16);
                report.Content[2] = (byte)((MemoryAddress & 0x0000ff00) >> 8);
                report.Content[3] = (byte)((MemoryAddress & 0x000000ff));

                //The final two bytes represent the size of the data we are retrieving
                report.Content[4] = (byte)((short)Data.Length & 0xff00 >> 8);
                report.Content[5] = (byte)((short)Data.Length & 0x00ff);

                //Add the data to store to the report
                Array.Copy(Data, 0, report.Content, 6, Data.Length);

                //Send the report
                SendReport(report);
            }
        }

        #endregion

        #region Infrared Enabling/Disabling

        /// <summary>
        /// Enables the infrared sensor on the hardware
        /// </summary>
        /// <param name="Mode">The Format that data should be sent in. This must match the format of the input reports coming in from the hardware</param>
        private void EnableInfrared(IRMode Mode, IRSensitivity Sensitivity)
        {
            /* NOTE: A sleep period of 50ms is required to ensure that the IR sensor wakes up correctly.
             *       See http://www.wiibrew.org/wiki/Wiimote#Initialization for details
             */

            // 1) Enable IR
            SendReport(new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.EnableIRCamera_1, new byte[] {(byte)(0x04 | GetRumbleBit())}));
            Thread.Sleep(50);
            SendReport(new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.EnableIRCamera_2, new byte[] { (byte)(0x04 | GetRumbleBit()) }));
            Thread.Sleep(50);

            // 2) Write to Register
            WriteData(IR_REGISTER_LOC_ENABLE, MemoryType.Register, new byte[] { 0x08 });
            Thread.Sleep(50);

            // 3) Set sensitivity
            byte[] Sensitivity_Block1;
            byte[] Sensitivity_Block2;

            switch (Sensitivity)
            {
                case IRSensitivity.Level_1:
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0x64, 0x00, 0xFE };
                    Sensitivity_Block2 = new byte[] { 0xFD, 0x05 };
                    break;
                case IRSensitivity.Level_2:
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0x96, 0x00, 0xB4 };
                    Sensitivity_Block2 = new byte[] { 0xB3, 0x04 };
                    break;
                case IRSensitivity.Level_3:
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0xAA, 0x00, 0x64 };
                    Sensitivity_Block2 = new byte[] { 0x63, 0x03 };
                    break;
                case IRSensitivity.Level_4:
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0xC8, 0x00, 0x36 };
                    Sensitivity_Block2 = new byte[] { 0x35, 0x03 };
                    break;
                case IRSensitivity.Level_5:
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0x72, 0x00, 0x20 };
                    Sensitivity_Block2 = new byte[] { 0x1F, 0x03 };
                    break;
                default:    //Level 3 again
                    Sensitivity_Block1 = new byte[] { 0x02, 0x00, 0x00, 0x71, 0x01, 0x00, 0xAA, 0x00, 0x64 };
                    Sensitivity_Block2 = new byte[] { 0x63, 0x03 };
                    break;
            }

            WriteData(IR_REGISTER_LOC_SENSITIVITY1, MemoryType.Register, Sensitivity_Block1);
            Thread.Sleep(50);
            WriteData(IR_REGISTER_LOC_SENSITIVITY2, MemoryType.Register,Sensitivity_Block2);
            Thread.Sleep(50);

            // 4) Set reporting mode
            WriteData(IR_REGISTER_LOC_MODE, MemoryType.Register, new byte[] { (byte)Mode });
            Thread.Sleep(50);

            // 5) as #2
            WriteData(IR_REGISTER_LOC_ENABLE, MemoryType.Register, new byte[] { 0x08 });
            Thread.Sleep(50);

        }

        /// <summary>
        /// Disables the infrared sensor
        /// </summary>
        private void DisableInfrared()
        {
            /* NOTE: A sleep period of 50ms is required to ensure that the IR sensor wakes up correctly.
             *       See http://www.wiibrew.org/wiki/Wiimote#Initialization for details
             */

            // 1) Enable IR
            SendReport(new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.EnableIRCamera_1, new byte[] { (byte)(0x04 | GetRumbleBit()) }));
            Thread.Sleep(50);
            SendReport(new HIDDeviceReport(GetNextUniqueReportID(), (byte)OutputReportType.EnableIRCamera_2, new byte[] { (byte)(0x04 | GetRumbleBit()) }));
            Thread.Sleep(50);

            // 2) Write to Register
            WriteData(IR_REGISTER_LOC_ENABLE, MemoryType.Register, new byte[] { 0x00 });
            Thread.Sleep(50);


            // 4) Set reporting mode
            WriteData(IR_REGISTER_LOC_MODE, MemoryType.Register, new byte[] { (byte)IRMode.Off });
            Thread.Sleep(50);

            // 5) as #2
            WriteData(IR_REGISTER_LOC_ENABLE, MemoryType.Register, new byte[] { 0x00 });
            Thread.Sleep(50);

        }

        #endregion

        #endregion
    }
}
