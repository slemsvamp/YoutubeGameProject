using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace YoutubeGameProject {
    public class YoutubeGame : Game {

        private static YoutubeGame instance;
        public static YoutubeGame Instance {
            get {
                if (instance == null) {
                    instance = new YoutubeGame();
                }
                return instance;
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spaceIslandTexture;
        Sprite tinyMaleSprite;

        InputManager inputManager;
        public InputManager InputManager {
            get {
                return inputManager;
            }
        }

        public YoutubeGame() {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            inputManager = new InputManager(false);
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spaceIslandTexture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/space-island.png"));
            Texture2D tinyMaleTexture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/tiny-male.png"));
            tinyMaleSprite = new Sprite(tinyMaleTexture, new Vector2(100, 100), true);
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            inputManager.Update();

            if (inputManager.Pressed(Input.Back)) {
                Exit();
            }

            tinyMaleSprite.Update(gameTime);

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
