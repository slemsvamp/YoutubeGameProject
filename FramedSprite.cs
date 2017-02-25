using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class FramedSprite : Sprite {
        private int framesX;
        private int framesY;
        private int borderSize;

        private Size frameSize;
        public Size FrameSize {
            get {
                return frameSize;
            }
        }

        private int currentFrame;

        public FramedSprite(int pFramesX, int pFramesY, int pBorderSize, Texture2D pTexture, Vector2 pPosition, Color pTint) : base(pTexture, pPosition, pTint) {
            framesX = pFramesX;
            framesY = pFramesY;
            borderSize = pBorderSize;
            currentFrame = 0;
            frameSize = new Size {
                Width = (texture.Width - (framesX * borderSize) - borderSize) / framesX,
                Height = (texture.Height - (framesY * borderSize) - borderSize) / framesY
            };
        }

        public void SetCurrentFrame(int pFrame) {
            currentFrame = pFrame;
        }

        public Rectangle GetSourceRectangle() {
            int frameY = currentFrame / framesX;
            int frameX = currentFrame % framesX;

            int textureX = borderSize + ((borderSize + frameSize.Width) * frameX);
            int textureY = borderSize + ((borderSize + frameSize.Height) * frameY);

            return new Rectangle(textureX, textureY, frameSize.Width, frameSize.Height);
        }

        public override void Draw(SpriteBatch pSpriteBatch, float pScale = 1f) {
            Draw(pSpriteBatch, position, pScale);
        }

        public void Draw(SpriteBatch pSpriteBatch, Vector2 pPosition, float pScale = 1f) {
            Rectangle sourceRectangle = GetSourceRectangle();
            pSpriteBatch.Draw(texture, pPosition, sourceRectangle, tint, 0f, Vector2.Zero, pScale, SpriteEffects.None, 1f);
        }
    }
}
