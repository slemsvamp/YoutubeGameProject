using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace YoutubeGameProject {
    public class StartupGamescreen : Gamescreen {
        Texture2D spaceIslandTexture;
        Sprite tinyMaleSprite;
        Font smallFont;

        public StartupGamescreen() {
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent(ContentManager pContentManager) {
            base.LoadContent(pContentManager);

            spaceIslandTexture = pContentManager.GetTexture("Content/Sprites/space-island.png");
            Texture2D tinyMaleTexture = pContentManager.GetTexture("Content/Sprites/tiny-male.png");
            tinyMaleSprite = new Sprite(tinyMaleTexture, new Vector2(100, 100), Color.White, true);

            Texture2D fontSpriteTexture = pContentManager.GetTexture("Content/Fonts/small-font.png");
            FramedSprite fontSprite = new FramedSprite(8, 6, 0, fontSpriteTexture, Vector2.Zero, Color.White);

            var mapping = pContentManager.GetFontMapping("Content/Fonts/small-font.fontmapping");

            smallFont = new Font(fontSprite, mapping, 0, 1, Color.Red);
        }

        public override void Update(GameTime pGameTime) {
            base.Update(pGameTime);

            if (YoutubeGame.Instance.InputManager.Pressed(Input.Back)) {
                quit = true;
            }

            tinyMaleSprite.Update(pGameTime);
        }

        public override void Draw(SpriteBatch pSpriteBatch) {
            base.Draw(pSpriteBatch);

            pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            pSpriteBatch.Draw(spaceIslandTexture, Vector2.Zero, Color.White);
            tinyMaleSprite.Draw(pSpriteBatch);

            smallFont.DrawString(pSpriteBatch, "Hello World, I really like to make long strings to write!", new Vector2(100, 100));
            pSpriteBatch.End();

        }
    }
}
