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

        protected Color tint;
        public Color Tint {
            get {
                return tint;
            }
        }

        public Sprite(Texture2D pTexture, Vector2 pPosition, Color pTint) {
            position = pPosition;
            texture = pTexture;
            tint = pTint;
        }

        public void SetTint(Color pTint) {
            tint = pTint;
        }

        public void SetPosition(Vector2 pPosition) {
            position = pPosition;
        }

        public void SetPosition(float pX, float pY) {
            position = new Vector2(pX, pY);
        }

        public void Update(GameTime pGameTime) {
        }

        public virtual void Draw(SpriteBatch pSpriteBatch, float pScale = 1f) {
            pSpriteBatch.Draw(texture, position, null, null, null, 0, new Vector2(pScale, pScale), tint, SpriteEffects.None, 0);
        }
    }
}
