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

        public FramedSprite(int pFramesX, int pFramesY, int pBorderSize, Texture2D pTexture, Vector2 pPosition, Color pTint, bool pIsPlayerControlled = false) : base(pTexture, pPosition, pTint, pIsPlayerControlled) {
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

        public override void Draw(SpriteBatch pSpriteBatch) {
            Draw(pSpriteBatch, position);
        }

        public void Draw(SpriteBatch pSpriteBatch, Vector2 pPosition) {
            Rectangle sourceRectangle = GetSourceRectangle();
            pSpriteBatch.Draw(texture, pPosition, sourceRectangle, tint);
        }
    }
}
