using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    public class AnimationJob {
        public FramedSprite Sprite;
        public AnimationToken[] Tokens;
        public AnimationState State;

        public int CurrentStep;
        public int ElapsedMsInStep;
    }
}
