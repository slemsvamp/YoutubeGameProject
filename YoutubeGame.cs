using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace YoutubeGameProject {
    public class YoutubeGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spaceIslandTexture;
        Sprite tinyMaleSprite;

        public YoutubeGame() {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spaceIslandTexture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/space-island.png"));
            Texture2D tinyMaleTexture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/tiny-male.png"));
            tinyMaleSprite = new Sprite(tinyMaleTexture, new Vector2(100, 100));
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W)) {
                tinyMaleSprite.Position.Y -= 10 * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            if (keyboardState.IsKeyDown(Keys.S)) {
                tinyMaleSprite.Position.Y += 10 * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            if (keyboardState.IsKeyDown(Keys.A)) {
                tinyMaleSprite.Position.X -= 30 * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            if (keyboardState.IsKeyDown(Keys.D)) {
                tinyMaleSprite.Position.X += 30 * gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            spriteBatch.Draw(spaceIslandTexture, Vector2.Zero, Color.White);
            tinyMaleSprite.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
