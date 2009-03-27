using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// Holds the calibration information of a Wii Remote accelerometer
    /// </summary>
    public struct AccelerometerCalibrationInfo
    {
        /// <summary>
        /// The X-Axis Value when there is no movement in that direction
        /// </summary>
        public float ZeroPointX;
        /// <summary>
        /// The Y-Axis Value when there is no movement in that direction
        /// </summary>
        public float ZeroPointY;
        /// <summary>
        /// The Z-Axis Value when there is no movement in that direction
        /// </summary>
        public float ZeroPointZ;
        /// <summary>
        /// The value of 1m/s^2 on the X-Axis
        /// </summary>
        public float GValueX;
        /// <summary>
        /// The value of 1m/s^2 on the Y-Axis
        /// </summary>
        public float GValueY;
        /// <summary>
        /// The value of 1m/s^2 on the Z-Axis
        /// </summary>
        public float GValueZ;

        /// <summary>
        /// Informs us whether there is any calibration data in this instance
        /// </summary>
        /// <returns>TRUE if this instance contains no calibration data, FALSE if there is calibration data within this class</returns>
        public bool IsEmpty()
        {
            return (ZeroPointX == 0F && ZeroPointY == 0F && ZeroPointZ == 0F && GValueX == 0F && GValueY == 0F && GValueZ == 0F);
        }

    }

    /// <summary>
    /// Represents a direction of movement on three planes. Used to hold the direction of movement of the Wii Remote
    /// </summary>
    public struct Vector3D
    {
        public float X;
        public float Y;
        public float Z;
    }

    /// <summary>
    /// Represents the Accelerometer in the Wii Remote
    /// </summary>
    public class Accelerometer
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



        #endregion

        //==================================================================================
        #region Private Variables

        /// <summary>
        /// Holds the calibration information for this accelerometer
        /// </summary>
        private AccelerometerCalibrationInfo calInfo;
        /// <summary>
        /// Holds the last reading recorded by this accelerometer
        /// </summary>
        private Vector3D lastReading;


        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Accelerometer()
        {
            SetDefaults(new Vector3D(), new AccelerometerCalibrationInfo());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Calibration">The calibration information for this accelerometer</param>
        public Accelerometer(AccelerometerCalibrationInfo Calibration)
        {
            SetDefaults(new Vector3D(), Calibration);
        }

        #endregion

        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the last reading of this Accelerometer
        /// </summary>
        public Vector3D LastReading 
        {
            get { return lastReading; }
        }

        /// <summary>
        /// Gets or Sets the currently known calibration information of this Accelerometer
        /// </summary>
        public AccelerometerCalibrationInfo CalibrationInfo
        {
            get { return calInfo; }
            set { calInfo = value; }
        }
     
        #endregion

        //==================================================================================
        #region Public Methods


        #endregion

        //==================================================================================
        #region Private/Protected Methods

        #region Instance Initalization

        /// <summary>
        /// Initializes the variables in the class
        /// </summary>
        /// <param name="InitialReading">The initial reading that should be returned by this instance</param>
        /// <param name="InitialCalibrationInfo">The calibration data for this accelerometer</param>
        private void SetDefaults(Vector3D InitialReading, AccelerometerCalibrationInfo InitialCalibrationInfo)
        {
            //Initialize the properties of this class
            lastReading = InitialReading;
            calInfo = InitialCalibrationInfo;
        }

        #endregion

        #region Internal Use Only

        /// <summary>
        /// Sets the state of a specified button.
        /// </summary>
        /// <param name="Reading">The value to set the last reading to</param>
        /// <returns>The previous reading that has now been replaced</returns>
        internal Vector3D SetLastReading(Vector3D Reading)
        {
            //Store the old reading
            Vector3D formerReading = lastReading;

            //Set the last reading to be the new reading
            lastReading = Reading;

            //Return the old reading
            return formerReading;
        }

        #endregion

        #endregion
    }
}
