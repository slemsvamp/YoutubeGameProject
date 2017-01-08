using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGameProject {
    [Flags]
    public enum Input {
        Up = 1,
        Left = 2,
        Down = 4,
        Right = 8,
        Back = 16
    }
}
