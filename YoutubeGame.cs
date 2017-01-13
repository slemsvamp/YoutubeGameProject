using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GamescreenManager gamescreenManager;
        public GamescreenManager GamescreenManager {
            get {
                return gamescreenManager;
            }
        }

        private InputManager inputManager;
        public InputManager InputManager {
            get {
                return inputManager;
            }
        }

        public YoutubeGame() {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            inputManager = new InputManager();
            gamescreenManager = new GamescreenManager();
        }

        protected override void Initialize() {
            base.Initialize();

            gamescreenManager.Push(new StartupGamescreen());
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            inputManager.Update();

            gamescreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gamescreenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
