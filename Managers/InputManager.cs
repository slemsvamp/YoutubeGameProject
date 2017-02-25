using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class InputManager {
        private Dictionary<Input, InputData> state;
        public Dictionary<Keys, Input> KeyBindingsKeyboard;
        public Dictionary<Buttons, Input> KeyBindingsGamepad;

        private TimeSpan delay;
        private TimeSpan repeat;

        private bool isUsingKeyboard;

        public InputData this[Input input] {
            get {
                if (state.ContainsKey(input)) {
                    return state[input];
                }
                return InputData.Unpressed;
            }
        }

        public InputManager(bool pIsUsingKeyboard = true) {
            isUsingKeyboard = pIsUsingKeyboard;

            state = new Dictionary<Input, InputData>();

            delay = new TimeSpan(0, 0, 0, 0, 500);
            repeat = new TimeSpan(0, 0, 0, 0, 75);

            KeyBindingsKeyboard = new Dictionary<Keys, Input> {
                { Keys.W, Input.Up },
                { Keys.A, Input.Left },
                { Keys.S, Input.Down },
                { Keys.D, Input.Right },
                { Keys.Escape, Input.Back }
            };

            KeyBindingsGamepad = new Dictionary<Buttons, Input> {
                { Buttons.DPadUp, Input.Up },
                { Buttons.DPadLeft, Input.Left },
                { Buttons.DPadDown, Input.Down },
                { Buttons.DPadRight, Input.Right },
                { Buttons.Back, Input.Back }
            };
        }

        public void Update(GameTime pGameTime, PlayerIndex pPlayer = PlayerIndex.One) {
            double now = pGameTime.TotalGameTime.TotalMilliseconds;
            List<Input> pressedInputs = new List<Input>();

            if (isUsingKeyboard) {
                Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

                foreach (Keys key in pressedKeys) {
                    if (KeyBindingsKeyboard.ContainsKey(key)) {
                        pressedInputs.Add(KeyBindingsKeyboard[key]);
                    }
                }
            } else {
                var gamepadState = GamePad.GetState(pPlayer);
                foreach (var kvp in KeyBindingsGamepad) {
                    if (gamepadState.IsButtonDown(kvp.Key)) {
                        pressedInputs.Add(kvp.Value);
                    }
                }
            }

            foreach (Input pressedInput in pressedInputs) {
                InputData data = state.ContainsKey(pressedInput) ? state[pressedInput] : new InputData(now + delay.TotalMilliseconds);
                bool isValid = data.NextValidInput <= now;
                bool previouslyUnpressed = !data.IsHeld && !data.IsPressed;

                data.IsPressed = data.IsHeld = true;

                if (previouslyUnpressed) {
                    data.NextValidInput = now + delay.TotalMilliseconds;
                } else {
                    if (isValid) {
                        data.NextValidInput = now + repeat.TotalMilliseconds;
                    } else {
                        data.IsPressed = false;
                    }
                }

                state[pressedInput] = data;
            }

            foreach (Input input in state.Keys) {
                if (pressedInputs.Contains(input) == false) {
                    state[input].IsHeld = state[input].IsPressed = false;
                    state[input].NextValidInput = double.MinValue;
                }
            }
        }
    }
}
