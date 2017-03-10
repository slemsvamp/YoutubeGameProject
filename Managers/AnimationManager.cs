using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class AnimationManager {
        private int animationId;

        public Dictionary<string, AnimationToken[]> animations;
        public Dictionary<int, AnimationJob> jobs;

        public AnimationManager() {
            animationId = 0;
            animations = new Dictionary<string, AnimationToken[]>();
            jobs = new Dictionary<int, AnimationJob>();
        }

        public void LoadContent(ContentManager pContentManager) {
            Animation[] animationArray = pContentManager.GetAnimations("Content/Animations/humanoid-animation.txt");

            foreach (Animation animation in animationArray) {
                animations.Add(animation.Name, animation.Tokens);
            }
        }

        public AnimationState? GetState(int pAnimationId) {
            if (jobs.ContainsKey(pAnimationId)) {
                return jobs[pAnimationId].State;
            }
            return null;
        }

        public int PlayAnimation(string pAnimationName, FramedSprite pSprite) {
            if (animations.ContainsKey(pAnimationName)) {
                AnimationJob job = new AnimationJob {
                    Sprite = pSprite,
                    Tokens = animations[pAnimationName],
                    State = AnimationState.Running,
                    CurrentStep = 0,
                    ElapsedMsInStep = 0
                };

                animationId++;

                if (animationId == int.MaxValue) {
                    animationId = 0;
                }

                jobs.Add(animationId, job);
                return animationId;
            }

            throw new Exception("Animation '" + pAnimationName + "' does not exist.");
        }

        public void PauseAnimation(int pAnimationId) {
            if (jobs.ContainsKey(pAnimationId)) {
                var job = jobs[pAnimationId];
                job.State = AnimationState.Paused;
            }
        }

        public void StopAnimation(int pAnimationId) {
            if (jobs.ContainsKey(pAnimationId)) {
                var job = jobs[pAnimationId];
                job.State = AnimationState.Stopped;
            }
        }

        public void Update(GameTime pGameTime) {
            List<int> animationsToRemove = new List<int>();
            foreach (KeyValuePair<int, AnimationJob> job in jobs) {
                AnimationJob animation = job.Value;

                if (animation.State == AnimationState.Running) {
                    int elapsedTimeMs = (int)pGameTime.ElapsedGameTime.TotalMilliseconds;
                    int currentStep = animation.CurrentStep;
                    int elapsedInStep = animation.ElapsedMsInStep;

                    do {
                        var token = animation.Tokens[currentStep];
                        if (token.Type == AnimationTokenType.SetFrame) {
                            animation.Sprite.SetCurrentFrame(token.Value);
                            elapsedTimeMs--;
                            currentStep++;
                        } else if (token.Type == AnimationTokenType.Wait) {
                            if (token.Value > elapsedInStep + elapsedTimeMs) {
                                elapsedInStep += elapsedTimeMs;
                                elapsedTimeMs = 0;
                            } else if (token.Value <= elapsedInStep + elapsedTimeMs) {
                                currentStep++;
                                elapsedTimeMs -= token.Value - elapsedInStep;
                                elapsedInStep = 0;
                            }
                        }

                        if (currentStep >= animation.Tokens.Length) {
                            currentStep = 0;
                        }
                    } while (elapsedTimeMs > 0);

                    animation.CurrentStep = currentStep;
                    animation.ElapsedMsInStep = elapsedInStep;
                }

                if (animation.State == AnimationState.Stopped) {
                    animationsToRemove.Add(job.Key);
                }
            }

            foreach (int id in animationsToRemove) {
                jobs.Remove(id);
            }
        }
    }
}
