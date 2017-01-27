using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class Sprite {
        protected Vector2 position;
        public Vector2 Position {
            get {
                return position;
            }
        }

        protected Texture2D texture;
        public Texture2D Texture {
            get {
                return texture;
            }
        }

        private bool isPlayerControlled;
        public bool IsPlayerControlled {
            get {
                return isPlayerControlled;
            }
        }

        protected Color tint;
        public Color Tint {
            get {
                return tint;
            }
        }

        public Sprite(Texture2D pTexture, Vector2 pPosition, Color pTint, bool pIsPlayerControlled = false) {
            position = pPosition;
            texture = pTexture;
            tint = pTint;
            isPlayerControlled = pIsPlayerControlled;
        }

        public void SetTint(Color pTint) {
            tint = pTint;
        }

        public void Update(GameTime pGameTime) {
            if (IsPlayerControlled) {
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Up)) {
                    position.Y -= 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Down)) {
                    position.Y += 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Left)) {
                    position.X -= 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Right)) {
                    position.X += 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
            }
        }

        public virtual void Draw(SpriteBatch pSpriteBatch) {
            pSpriteBatch.Draw(texture, position, tint);
        }
    }
}
