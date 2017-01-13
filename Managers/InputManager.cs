using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class InputManager {

        public Dictionary<Keys, Input> KeyBindingsKeyboard;
        public Dictionary<Buttons, Input> KeyBindingsGamepad;

        private bool isUsingKeyboard;

        private int playerInput;

        public InputManager(bool pIsUsingKeyboard = true) {
            isUsingKeyboard = pIsUsingKeyboard;

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

        public void Update(PlayerIndex pPlayer = PlayerIndex.One) {
            playerInput = 0;

            if (isUsingKeyboard) {
                Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

                foreach (Keys key in pressedKeys) {
                    if (KeyBindingsKeyboard.ContainsKey(key)) {
                        playerInput |= (int)KeyBindingsKeyboard[key];
                    }
                }
            } else {
                var gamepadState = GamePad.GetState(pPlayer);
                foreach (var kvp in KeyBindingsGamepad) {
                    if (gamepadState.IsButtonDown(kvp.Key)) {
                        playerInput |= (int)kvp.Value;
                    }
                }
            }
        }

        public bool Pressed(params Input[] pInputs) {
            int n = 0;
            foreach (var pi in pInputs) {
                n |= (int)pi;
            }

            return playerInput == n;
        }
    }
}
