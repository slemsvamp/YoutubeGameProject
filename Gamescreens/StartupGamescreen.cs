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

        private AnimationManager animationManager;

        FramedSprite tinyMaleSprite;
        int tinyMaleAnimationId;
        Direction lastTinyMaleWalkingDirection;

        Font smallFont;

        public StartupGamescreen() {
        }

        public override void Initialize() {
            base.Initialize();

            animationManager = YoutubeGame.Instance.AnimationManager;
        }

        public override void LoadContent(ContentManager pContentManager) {
            base.LoadContent(pContentManager);

            spaceIslandTexture = pContentManager.GetTexture("Content/Sprites/space-island.png");
            Texture2D tinyMaleTexture = pContentManager.GetTexture("Content/Sprites/tiny-male.png");
            tinyMaleSprite = new FramedSprite(8, 4, 0, tinyMaleTexture, new Vector2(100, 100), Color.White);

            Texture2D fontSpriteTexture = pContentManager.GetTexture("Content/Fonts/small-font.png");
            FramedSprite fontSprite = new FramedSprite(8, 6, 0, fontSpriteTexture, Vector2.Zero, Color.White);

            var mapping = pContentManager.GetFontMapping("Content/Fonts/small-font.fontmapping");

            smallFont = new Font(fontSprite, mapping, 0, 1, Color.PowderBlue);
        }

        public override void Update(GameTime pGameTime) {
            base.Update(pGameTime);

            if (YoutubeGame.Instance.InputManager[Input.Back].IsPressed) {
                quit = true;
            }

            Direction tinyMaleWalkingDirection = Direction.None;

            if (YoutubeGame.Instance.InputManager[Input.Up].IsHeld) {
                tinyMaleWalkingDirection = Direction.Up;
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X, tinyMaleSprite.Position.Y - 100 * pGameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            if (YoutubeGame.Instance.InputManager[Input.Down].IsHeld) {
                tinyMaleWalkingDirection = Direction.Down;
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X, tinyMaleSprite.Position.Y + 100 * pGameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            if (YoutubeGame.Instance.InputManager[Input.Left].IsHeld) {
                tinyMaleWalkingDirection = Direction.Left;
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X - 100 * pGameTime.ElapsedGameTime.Milliseconds / 1000f, tinyMaleSprite.Position.Y);
            }
            if (YoutubeGame.Instance.InputManager[Input.Right].IsHeld) {
                tinyMaleWalkingDirection = Direction.Right;
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X + 100 * pGameTime.ElapsedGameTime.Milliseconds / 1000f, tinyMaleSprite.Position.Y);
            }

            tinyMaleSprite.Update(pGameTime);

            if (tinyMaleWalkingDirection != lastTinyMaleWalkingDirection) {
                AnimationState? animationState = animationManager.GetState(tinyMaleAnimationId);

                if (animationState.HasValue) {
                    animationManager.StopAnimation(tinyMaleAnimationId);
                }

                switch (tinyMaleWalkingDirection) {
                    case Direction.Up:
                        tinyMaleAnimationId = animationManager.PlayAnimation("WalkUp8Frame", tinyMaleSprite);
                        break;
                    case Direction.Down:
                        tinyMaleAnimationId = animationManager.PlayAnimation("WalkDown8Frame", tinyMaleSprite);
                        break;
                    case Direction.Left:
                        tinyMaleAnimationId = animationManager.PlayAnimation("WalkLeft8Frame", tinyMaleSprite);
                        break;
                    case Direction.Right:
                        tinyMaleAnimationId = animationManager.PlayAnimation("WalkRight8Frame", tinyMaleSprite);
                        break;
                }
            }

            lastTinyMaleWalkingDirection = tinyMaleWalkingDirection;
        }

        public override void Draw(SpriteBatch pSpriteBatch) {
            base.Draw(pSpriteBatch);

            if (initialized) {
                pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
                pSpriteBatch.Draw(spaceIslandTexture, Vector2.Zero, Color.White);
                tinyMaleSprite.Draw(pSpriteBatch, 4f);

                smallFont.DrawString(pSpriteBatch, "Hello World!", new Vector2(20, 20), 8f);
                pSpriteBatch.End();
            }
        }
    }
}
