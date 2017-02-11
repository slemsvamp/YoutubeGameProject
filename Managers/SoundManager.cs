using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class SoundManager {
        private Dictionary<string, SoundFX> settings { get; set; }
        private Dictionary<string, SoundEffect> soundEffects { get; set; }
        private Dictionary<string, List<SoundEffectInstance>> soundEffectInstances { get; set; }
        private int maxSoundEffectInstances = 3;

        public SoundManager(List<SoundFX> pSettings, int pMaxSoundEffectInstances = 3) {
            settings = pSettings.ToDictionary(s => s.Key);
            soundEffects = new Dictionary<string, SoundEffect>();
            soundEffectInstances = new Dictionary<string, List<SoundEffectInstance>>();
            maxSoundEffectInstances = pMaxSoundEffectInstances;
        }

        public void LoadContent(ContentManager pContentManager) {
            soundEffects = new Dictionary<string, SoundEffect>();
            foreach (var sfx in settings) {
                SoundEffect soundEffect = pContentManager.GetSoundEffect(sfx.Value.Filename);

                if (soundEffect != null) {
                    soundEffects.Add(sfx.Key, soundEffect);
                }
            }

            soundEffectInstances = new Dictionary<string, List<SoundEffectInstance>>();

            foreach (var sfx in soundEffects) {
                List<SoundEffectInstance> sfxInstances = new List<SoundEffectInstance>();

                for (int i = 0; i < maxSoundEffectInstances; i++) {
                    SoundEffectInstance sfxInstance = sfx.Value.CreateInstance();
                    sfxInstance.Volume = settings[sfx.Key].DefaultVolume;
                    sfxInstance.Pitch = settings[sfx.Key].DefaultPitch;
                    sfxInstances.Add(sfxInstance);
                }

                soundEffectInstances.Add(sfx.Key, sfxInstances);
            }
        }

        public void PlaySound(string pKey, bool pAllowInterrupt = false) {
            if (soundEffectInstances.ContainsKey(pKey)) {
                var instances = soundEffectInstances[pKey];
                var instancesStopped = instances.Where(s => s.State == SoundState.Stopped);

                if (instancesStopped.Count() == 0 && pAllowInterrupt) {
                    var instance = instances.First();
                    instance.Stop();
                    instance.Play();
                } else if (instancesStopped.Count() > 0) {
                    instancesStopped.First().Play();
                }
            }
        }

        public void StopSound(string pKey) {
            if (soundEffectInstances.ContainsKey(pKey)) {
                var instances = soundEffectInstances[pKey];
                foreach (var instance in instances) {
                    if (instance.State != SoundState.Stopped) {
                        instance.Stop();
                    }
                }
            }
        }
    }
}
