using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HIDDevices;
using WiiRemoteLib;

namespace WiiRemoteTestApp
{
    public partial class frmMain : Form
    {

        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants


        #endregion

        //==================================================================================
        #region Events

        private delegate void StatusChangeDelegate(object sender, EventArgs e);
        private delegate void ButtonsChangedDelegate(object sender, ButtonChangedEventArgs e);
        private delegate void AccelChangedDelegate(object sender, AccelerationChangedEventArgs e);

        #endregion

        //==================================================================================
        #region Private Variables

        /// <summary>
        /// The Wii Remote
        /// </summary>
        private WiiRemote remote;

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        public frmMain()
        {
            InitializeComponent();
            AttemptToContactWiiRemote();
        } 

        #endregion

        //==================================================================================
        #region Public Properties



        #endregion

        //==================================================================================
        #region Public Methods


        #endregion

        //==================================================================================
        #region Private/Protected Methods

        #region Wii Remote Connection

        /// <summary>
        /// Attempts to find a Wii remote and bind events to it
        /// </summary>
        private bool AttemptToContactWiiRemote()
        {
            bool found = FindWiiRemote();
            if (found)
            {
                //Bind Events
                remote.StatusChanged += new EventHandler<EventArgs>(remote_StatusChanged);
                remote.ButtonStateChanged += new EventHandler<ButtonChangedEventArgs>(remote_ButtonStateChanged);
                remote.AccelerationChanged += new EventHandler<AccelerationChangedEventArgs>(remote_AccelerationChanged);
            }
            else
            {
                MessageBox.Show("Could not find a Wii remote to connect to. Are you connected via bluetooth?", "Wii Remote not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return found;
        }

        /// <summary>
        /// Finds a connected Wii Remote
        /// </summary>
        /// <returns>TRUE if a Wii remote was found, FALSE otherwise</returns>
        private bool FindWiiRemote()
        {
            //Declare the value that will be returned at the end of this function
            bool retVal = true;

            //Get the list of HID devices connected (including Wii Remotes)
            HIDDeviceCollection devs = HIDDeviceInterface.GetAllHIDDevices(true, false);

            //Search through the devices until we find one that matches the product ID
            //and vendor ID of the Wii remote
            int i = 0;
            while (remote == null && i < devs.Count)
            {
                //Get the attributes for this device
                devs[i].SetDeviceAttributes();

                //Check to see if we have a wii remote
                if (devs[i].ProductID == WiiRemote.WIIMOTE_PRODUCT_ID && devs[i].VendorID == WiiRemote.WIIMOTE_VENDOR_ID)
                {
                    remote = new WiiRemote(devs[i].DevicePath);
                }
                else
                {
                    i++;
                }
            }

            //Make sure we have a wii remote now, if not throw an exception
            if (remote == null)
            {
                retVal = false;
            }

            //return whether or not we've found a Wii remote
            return retVal;
        }

        /// <summary>
        /// Button push to connect/disconnect Wii Remote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnectDisconnect_Click(object sender, EventArgs e)
        {

            if (remote == null)
            {
                AttemptToContactWiiRemote();
            }

            if (remote != null)
            {
                if (remote.IsConnected)
                {
                    remote.Disconnect();
                    btnConnectDisconnect.Text = "Connect";
                }
                else
                {
                    //Open communication with hardware
                    remote.Connect();

                    //Set the player 1 LED
                    LEDStates leds = remote.LEDs;
                    leds.LED_1 = LEDState.Illuminated;
                    remote.LEDs = leds;

                    //Change button text
                    btnConnectDisconnect.Text = "Disconnect";

                    //Get the status of the remote
                    remote.RequestStatus();

                    //Set the initial reporting mode, which we know is the first item in the list
                    cboReportingMode.SelectedIndex = 0;
                }
            }

            
        }

        #endregion

        #region Remote Changed Event Handlers

        /// <summary>
        /// Happens when the status of the remote is changed
        /// </summary>
        void remote_StatusChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new StatusChangeDelegate(remote_StatusChanged), new object[] { sender, e });
            }
            else
            {
                UpdateLEDControls(remote.LEDs);
                SetBatteryLevel(remote.BatteryLevel);
            }
        }

        /// <summary>
        /// Executes when the remote's buttons are parsed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void remote_ButtonStateChanged(object sender, ButtonChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ButtonsChangedDelegate(remote_ButtonStateChanged), new object[] { sender, e });
            }
            else
            {
                UpdateButtons(e.NewButtons);
            }
        }

        /// <summary>
        /// Executes when the accelerometer reading has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void remote_AccelerationChanged(object sender, AccelerationChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccelChangedDelegate(remote_AccelerationChanged), new object[] { sender, e });
            }
            else
            {
                UpdateAccelData(e.Direction);
            }
        }

        /// <summary>
        /// Updates the labels on the form to accurately reflect the accelerometer reading
        /// </summary>
        /// <param name="vector3D"></param>
        private void UpdateAccelData(Vector3D Vector)
        {
            lblAccelX.Text = Vector.X.ToString();
            lblAccelY.Text = Vector.Y.ToString();
            lblAccelZ.Text = Vector.Z.ToString();
        }

        #endregion

        #region LED functions

        /// <summary>
        /// Updates the LED controls to reflect the status of the LEDs on the hardware
        /// </summary>
        /// <param name="LEDs">The status of the four LEDs</param>
        private void UpdateLEDControls(LEDStates LEDs)
        {
            chkLED1.Checked = (LEDs.LED_1 == LEDState.Illuminated);
            chkLED2.Checked = (LEDs.LED_2 == LEDState.Illuminated);
            chkLED3.Checked = (LEDs.LED_3 == LEDState.Illuminated);
            chkLED4.Checked = (LEDs.LED_4 == LEDState.Illuminated);
        }

        /// <summary>
        /// Sends the new LED configuration to the Wii remote when the one of the LED checkboxes is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLED_CheckedChanged(object sender, EventArgs e)
        {
            //Stop Chasing
            tmrChaseLEDs.Stop();

            LEDStates newLEDs = new LEDStates();

            newLEDs.LED_1 = (chkLED1.Checked ? LEDState.Illuminated : LEDState.Off);
            newLEDs.LED_2 = (chkLED2.Checked ? LEDState.Illuminated : LEDState.Off);
            newLEDs.LED_3 = (chkLED3.Checked ? LEDState.Illuminated : LEDState.Off);
            newLEDs.LED_4 = (chkLED4.Checked ? LEDState.Illuminated : LEDState.Off);

            remote.LEDs = newLEDs;
        }

        #endregion

        #region Rumble Functions

        /// <summary>
        /// Turns the rumble feature of the remote on or off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbRumbleOn_CheckedChanged(object sender, EventArgs e)
        {
                remote.Rumble = (rbRumbleOn.Checked ? RumbleState.On : RumbleState.Off);
        }

        #endregion

        #region Button Functions

        /// <summary>
        /// Updates the button representation on this form
        /// </summary>
        /// <param name="Buttons">The buttons to update</param>
        private void UpdateButtons(WiiRemoteButtons Buttons)
        {
            pnlA.Visible = (Buttons.A == WiiRemoteLib.ButtonState.Pressed);
            pnlB.Visible = (Buttons.B == WiiRemoteLib.ButtonState.Pressed);
            pnlUp.Visible = (Buttons.Up == WiiRemoteLib.ButtonState.Pressed);
            pnlRight.Visible = (Buttons.Right == WiiRemoteLib.ButtonState.Pressed);
            pnlDown.Visible = (Buttons.Down == WiiRemoteLib.ButtonState.Pressed);
            pnlLeft.Visible = (Buttons.Left == WiiRemoteLib.ButtonState.Pressed);
            pnlMinus.Visible = (Buttons.Minus == WiiRemoteLib.ButtonState.Pressed);
            pnlHome.Visible = (Buttons.Home == WiiRemoteLib.ButtonState.Pressed);
            pnlPlus.Visible = (Buttons.Plus == WiiRemoteLib.ButtonState.Pressed);
            pnl1.Visible = (Buttons.Num1 == WiiRemoteLib.ButtonState.Pressed);
            pnl2.Visible = (Buttons.Num2 == WiiRemoteLib.ButtonState.Pressed);
        }

        #endregion

        #region Battery Level Functionality

        /// <summary>
        /// Sets the battery level on screen
        /// </summary>
        /// <param name="BattLevel">The byte that represents the battery level. Has a range supposed range of 0.00-200.00, however, the upper limit of 200 is unconfirmed.</param>
        private void SetBatteryLevel(byte BattLevel)
        {
            //Set the progress bar
            if (BattLevel > 200)
            {
                pgbBatteryLevel.Value = 200;

                //Set the label
                lblBatteryLevel.Text = "~100.00%";
            }
            else
            {
                pgbBatteryLevel.Value = BattLevel;
                
                //Set the label
                lblBatteryLevel.Text = Convert.ToString((float)BattLevel / 2.00F) + "%";
            }

        }

        #endregion

        private void cboReportingMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboReportingMode.SelectedIndex)
            {
                case 0:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.CoreButtons, false);
                    break;
                case 1:
                    remote.SetInputReportFormat(InputReportType.CoreButtonsAccel, false);
                    break;
                case 2:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.CoreButtonsExten8, false);
                    break;
                case 3:
                    remote.SetInputReportFormat(InputReportType.CoreButtonsAccelIR12, false);
                    break;
                case 4:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.CoreButtonsExten19, false);
                    break;
                case 5:
                    remote.SetInputReportFormat(InputReportType.CoreButtonsAccelExten16, false);
                    break;
                case 6:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.CoreButtonsIR10Exten9, false);
                    break;
                case 7:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.CoreButtonsAccelIR10Exten6, false);
                    break;
                case 8:
                    lblAccelX.Text = "Accelerometer Disabled!";
                    lblAccelY.Text = "Accelerometer Disabled!";
                    lblAccelZ.Text = "Accelerometer Disabled!";
                    remote.SetInputReportFormat(InputReportType.Exten21, false);
                    break;
                case 9:
                    remote.SetInputReportFormat(InputReportType.CoreButtonsAccellIR36_1, false);
                    break;
            }
        }

        #endregion

        private void btnChaseLEDS_Click(object sender, EventArgs e)
        {
            tmrChaseLEDs.Interval = (int)nudChaseSpeed.Value;
            tmrChaseLEDs.Start();
        }

        private void nudChaseSpeed_ValueChanged(object sender, EventArgs e)
        {
            tmrChaseLEDs.Interval = (int)nudChaseSpeed.Value;
        }

        private void tmrChaseLEDs_Tick(object sender, EventArgs e)
        {
            LEDStates leds = new LEDStates();
            leds.LED_1 = remote.LEDs.LED_4;
            leds.LED_2 = remote.LEDs.LED_1;
            leds.LED_3 = remote.LEDs.LED_2;
            leds.LED_4 = remote.LEDs.LED_3;

            remote.LEDs = leds;
        }
    }
}
