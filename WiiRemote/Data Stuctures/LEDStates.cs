using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// An enumeration of the possible states an LED on the Wii remote can exist in
    /// </summary>
    public enum LEDState : byte
    {
        /// <summary>
        /// The LED is lit up
        /// </summary>
        Illuminated = 1,
        /// <summary>
        /// The LED is not lit up
        /// </summary>
        Off = 0
    }

    /// <summary>
    /// A structure that can hold the state of each of the LEDs on the Wii Remote
    /// </summary>
    public struct LEDStates
    {
        /// <summary>
        /// The state of the player 1 LED
        /// </summary>
        public LEDState LED_1;
        /// <summary>
        /// The state of the player 2 LED
        /// </summary>
        public LEDState LED_2;
        /// <summary>
        /// The state of the player 3 LED
        /// </summary>
        public LEDState LED_3;
        /// <summary>
        /// The state of the player 4 LED
        /// </summary>
        public LEDState LED_4;
    }
}
