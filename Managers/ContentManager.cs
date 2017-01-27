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

        public Dictionary<int, int> GetFontMapping(string pFile) {
            Dictionary<int, int> mapping = new Dictionary<int, int>();
            if (File.Exists(pFile)) {
                using (var stream = File.OpenRead(pFile)) {
                    using (var reader = new StreamReader(stream)) {
                        while (reader.EndOfStream == false) {
                            string line = reader.ReadLine();
                            string[] parts = line.Split(new string[] { "|" }, StringSplitOptions.None);
                            byte[] bytes = Encoding.Unicode.GetBytes(parts[0]);
                            int key = BitConverter.ToInt16(bytes, 0);
                            int value = int.Parse(parts[1]);

                            mapping.Add(key, value);
                        }
                    }
                }
            } else {
                throw new FileNotFoundException("Could not find file " + pFile);
            }
            return mapping;
        }
    }
}
