using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class ContentManager {
        private GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice {
            get {
                return graphicsDevice;
            }
        }

        public ContentManager() {
        }

        public void Prepare(GraphicsDevice pGraphicsDevice) {
            graphicsDevice = pGraphicsDevice;
        }

        public Texture2D GetTexture(string pFile) {
            Texture2D texture = null;

            if (File.Exists(pFile)) {
                using (var stream = File.OpenRead(pFile)) {
                    texture = Texture2D.FromStream(graphicsDevice, stream);
                }
            } else {
                throw new FileNotFoundException("Could not find file " + pFile);
            }

            return texture;
        }

        public SoundEffect GetSoundEffect(string pFile) {
            SoundEffect sfx = null;

            if (File.Exists(pFile)) {
                using (var stream = File.OpenRead(pFile)) {
                    sfx = SoundEffect.FromStream(stream);
                }
            } else {
                throw new FileNotFoundException("Could not find file " + pFile);
            }

            return sfx;
        }
    }
}
