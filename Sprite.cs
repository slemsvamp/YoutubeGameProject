using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class Sprite {
        public Vector2 Position;
        public Texture2D Texture;
        public bool IsPlayerControlled;

        public Sprite(Texture2D pTexture, Vector2 pPosition, bool pIsPlayerControlled = false) {
            Position = pPosition;
            Texture = pTexture;
            IsPlayerControlled = pIsPlayerControlled;
        }

        public void Update(GameTime pGameTime) {
            if (IsPlayerControlled) {
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Up)) {
                    Position.Y -= 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Down)) {
                    Position.Y += 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Left)) {
                    Position.X -= 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
                if (YoutubeGame.Instance.InputManager.Pressed(Input.Right)) {
                    Position.X += 30 * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
                }
            }
        }

        public void Draw(SpriteBatch pSpriteBatch) {
            pSpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
