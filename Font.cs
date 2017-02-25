using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class Font {
        private FramedSprite sprite;
        public FramedSprite Sprite {
            get {
                return sprite;
            }
        }

        private Size spacing;
        private Dictionary<int, int> mapping;

        public Font(FramedSprite pSprite, Dictionary<int, int> pMapping, int pHorizontalSpace, int pVerticalSpace, Color pFontColor) {
            sprite = pSprite;
            sprite.SetCurrentFrame(0);
            sprite.SetTint(pFontColor);

            mapping = pMapping;

            spacing = new Size { Width = pHorizontalSpace, Height = pVerticalSpace };
        }

        public void DrawString(SpriteBatch pSpriteBatch, string pText, Vector2 pPosition, float pScale = 1f) {
            int x = (int)pPosition.X;

            foreach (char c in pText) {
                var bytes = Encoding.Unicode.GetBytes(new char[] { c });
                int key = BitConverter.ToInt16(bytes, 0);
                int translatedValue = mapping[key];

                sprite.SetCurrentFrame(translatedValue);
                sprite.Draw(pSpriteBatch, new Vector2(x, pPosition.Y), pScale);

                x += (int)(pScale * (sprite.FrameSize.Width + spacing.Width));
            }
        }
    }
}
