using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuBeatmapRateChanger
{
    public class HitObject
    {
        public int pos_x;
        public int pos_y;
        public int offset;
        public HitObjectType noteType;
        public string etc;

        public string orig;
            



        public HitObject(int _x, int _y, int _offset, HitObjectType _noteType = HitObjectType.Circle, string _etc = "", string _orig = "")
        {
            pos_x = _x;
            pos_y = _y;
            offset = _offset;
            noteType = _noteType;
            etc = _etc;
            orig = _orig;
        }
    }
}
