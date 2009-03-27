using System;
using System.Collections.Generic;
using System.Text;

namespace WiiRemoteLib
{
    /// <summary>
    /// Represents the state of a button on the Wii Remote
    /// </summary>
    public enum ButtonState : int
    {
        Pressed = 0x00000001,
        Released = 0x00000000
    }

    /// <summary>
    /// A Class representing the state of the buttons on the Wii Remote
    /// </summary>
    public class WiiRemoteButtons
    {
        //==================================================================================
        #region Data Structures



        #endregion

        //==================================================================================
        #region Enumerations



        #endregion

        //==================================================================================
        #region Constants

        /// <summary>
        /// The Index of the A button in the array of buttons
        /// </summary>
        internal const int BUTTON_A = 0;
        /// <summary>
        /// The Index of the B button in the array of buttons
        /// </summary>
        internal const int BUTTON_B = 1;
        /// <summary>
        /// The Index of the D-Pad Up button in the array of buttons
        /// </summary>
        internal const int BUTTON_UP = 2;
        /// <summary>
        /// The Index of the D-Pad Right button in the array of buttons
        /// </summary>
        internal const int BUTTON_RIGHT = 3;
        /// <summary>
        /// The Index of the D-Pad Down button in the array of buttons
        /// </summary>
        internal const int BUTTON_DOWN = 4;
        /// <summary>
        /// The Index of the D-Pad Left button in the array of buttons
        /// </summary>
        internal const int BUTTON_LEFT = 5;
        /// <summary>
        /// The Index of the (-) button in the array of buttons
        /// </summary>
        internal const int BUTTON_MINUS = 6;
        /// <summary>
        /// The Index of the (Home) button in the array of buttons
        /// </summary>
        internal const int BUTTON_HOME = 7;
        /// <summary>
        /// The Index of the (+) button in the array of buttons
        /// </summary>
        internal const int BUTTON_PLUS = 8;
        /// <summary>
        /// The Index of the 1 button in the array of buttons
        /// </summary>
        internal const int BUTTON_1 = 9;
        /// <summary>
        /// The Index of the 2 button in the array of buttons
        /// </summary>
        internal const int BUTTON_2 = 10;

        #endregion

        //==================================================================================
        #region Events



        #endregion

        //==================================================================================
        #region Private Variables

        /// <summary>
        /// An array holding the status of the buttons on the Wii Remote
        /// </summary>
        private ButtonState[] buttons;

        #endregion

        //==================================================================================
        #region Constructors/Destructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WiiRemoteButtons()
        {
            SetDefaults(ButtonState.Released);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DefaultState">The initial state of all the buttons</param>
        public WiiRemoteButtons(ButtonState DefaultState)
        {
            SetDefaults(DefaultState);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Statuses">The string representation of the statuses of the buttons (See the ToString method for string format)</param>
        public WiiRemoteButtons(string Statuses)
        {
            SetDefaults(ButtonState.Released);
            for (int i = 0; i < buttons.Length; i++)
            {
                if (Statuses[i] == 'D')
                {
                    buttons[i] = ButtonState.Pressed;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ToCopy">The buttons class to clone</param>
        public WiiRemoteButtons(WiiRemoteButtons ToCopy)
        {
            SetDefaults(ButtonState.Released);
            ToCopy.buttons.CopyTo(buttons, 0);
        }

        #endregion

        //==================================================================================
        #region Public Properties

        /// <summary>
        /// Gets the state of the A button
        /// </summary>
        public ButtonState A
        {
            get { return buttons[BUTTON_A]; }
            set { buttons[BUTTON_A] = value; }
        }

        /// <summary>
        /// Gets the state of the B button
        /// </summary>
        public ButtonState B
        {
            get { return buttons[BUTTON_B]; }
            set { buttons[BUTTON_B] = value; }
        }

        /// <summary>
        /// Gets the state of the D-Pad Up button
        /// </summary>
        public ButtonState Up
        {
            get { return buttons[BUTTON_UP]; }
            set { buttons[BUTTON_UP] = value; }
        }

        /// <summary>
        /// Gets the state of the D-Pad Right button
        /// </summary>
        public ButtonState Right
        {
            get { return buttons[BUTTON_RIGHT]; }
            set { buttons[BUTTON_RIGHT] = value; }
        }

        /// <summary>
        /// Gets the state of the D-Pad down button
        /// </summary>
        public ButtonState Down
        {
            get { return buttons[BUTTON_DOWN]; }
            set { buttons[BUTTON_DOWN] = value; }
        }

        /// <summary>
        /// Gets the state of the D-Pad Left button
        /// </summary>
        public ButtonState Left
        {
            get { return buttons[BUTTON_LEFT]; }
            set { buttons[BUTTON_LEFT] = value; }
        }

        /// <summary>
        /// Gets the state of the (-) button
        /// </summary>
        public ButtonState Minus
        {
            get { return buttons[BUTTON_MINUS]; }
            set { buttons[BUTTON_MINUS] = value; }
        }

        /// <summary>
        /// Gets the state of the Home button
        /// </summary>
        public ButtonState Home
        {
            get { return buttons[BUTTON_HOME]; }
            set { buttons[BUTTON_HOME] = value; }
        }

        /// <summary>
        /// Gets the state of the (+) button
        /// </summary>
        public ButtonState Plus
        {
            get { return buttons[BUTTON_PLUS]; }
            set { buttons[BUTTON_PLUS] = value; }
        }

        /// <summary>
        /// Gets the state of the 1 button
        /// </summary>
        public ButtonState Num1
        {
            get { return buttons[BUTTON_1]; }
            set { buttons[BUTTON_1] = value; }
        }

        /// <summary>
        /// Gets the state of the 2 button
        /// </summary>
        public ButtonState Num2
        {
            get { return buttons[BUTTON_2]; }
            set { buttons[BUTTON_2] = value; }
        }

        #endregion

        //==================================================================================
        #region Public Methods

        /// <summary>
        /// Returns the string representation of all the buttons
        /// </summary>
        /// <returns>A string of the format SSSSSSSSSSS where 'S' can be either a 'U' or a
        /// 'D' depending on whether the button is up or down respectively. The string
        /// returns the buttons in the format "[A][B][UP][RIGHT][DOWN][LEFT][-][Home][+]
        /// [1][2]"</returns>
        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == ButtonState.Pressed)
                {
                    retVal.Append('D');
                }
                else
                {
                    retVal.Append('U');
                }
            }

            //Return the string
            return retVal.ToString();
        }

        #endregion

        //==================================================================================
        #region Private/Protected Methods

        #region Instance Initalization

        /// <summary>
        /// Initializes the variables in the class
        /// </summary>
        /// <param name="initialState">The initial state of all the buttons</param>
        private void SetDefaults(ButtonState initialState)
        {
            //Initialize the buttons
            buttons = new ButtonState[11];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = ButtonState.Released;
            }
        }

        #endregion

        #region Internal Use Only

        /// <summary>
        /// Sets the state of a specified button.
        /// </summary>
        /// <param name="ButtonToSet">The zero-based index of the button to change the state of. NOTE: It is recommended that you use the constants contained within this class in order to specify your button</param>
        /// <param name="State">The state to apply to the button</param>
        internal void SetButton(int ButtonToSet, ButtonState State)
        {
            buttons[ButtonToSet] = State;
        }

        #endregion

        #endregion
    }
}
