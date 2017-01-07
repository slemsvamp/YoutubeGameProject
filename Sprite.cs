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

        public Sprite(Texture2D pTexture, Vector2 pPosition) {
            Position = pPosition;
            Texture = pTexture;
        }

        public void Draw(SpriteBatch pSpriteBatch) {
            pSpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
