using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class GamescreenManager {
        private Stack<Gamescreen> gamescreens;
        public Stack<Gamescreen> Gamescreens {
            get {
                return gamescreens;
            }
        }

        public GamescreenManager() {
            gamescreens = new Stack<Gamescreen>();
        }

        public void Push(Gamescreen pGamescreen) {
            gamescreens.Push(pGamescreen);
        }

        public void Update(GameTime pGameTime) {
            bool gamescreenPopped = false;
            do {
                gamescreenPopped = false;

                // if there are no gamescreens in the stack, quit
                if (gamescreens.Count == 0) {
                    YoutubeGame.Instance.Exit();
                    return;
                }

                // look at the first gamescreen on the stack
                var gs = gamescreens.Peek();

                // initialize if not initialized
                if (gs.Initialized == false) {
                    gs.Initialize();
                    gs.LoadContent(YoutubeGame.Instance.ContentManager);
                }

                // update it
                gs.Update(pGameTime);

                // if quit, then pop it and do the do..while again
                if (gs.Quit) {
                    gamescreens.Pop();
                    gamescreenPopped = true;
                }
            } while (gamescreenPopped);
        }

        public void Draw(SpriteBatch pSpriteBatch) {
            gamescreens.Peek().Draw(pSpriteBatch);
        }
    }
}
