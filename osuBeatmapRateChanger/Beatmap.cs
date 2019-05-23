using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuBeatmapRateChanger
{
    public class Beatmap
    {
        public String Title;
        public String TitleUnicode;
        public String Artist;
        public String ArtistUnicode;
        public String Creator;
        public String Version;
        public String Source;
        public String Tags;

        public String AudioFileName;
        public String AudioLeadIn;
        public String PreviewTime;
        public String Countdown;
        public String SampleSet;
        public String StackLeniency;
        public String Mode;
        public String LetterboxInBreaks;
        public String WidescreenStoryboard;
        
        public List<int> Bookmarks = new List<int>();
        public String DistanceSpacing;
        public String BeatDivisor;
        public String GridSize;
        public String TimelineZoom;

        public String Events_bg;
        public List<int[]> Events_break = new List<int[]>();

        public Boolean isEditorExist = false;

        public float CS;
        public float AR;
        public float OD;
        public float HP;
        public float SV;
        public float ST; //SliderTickRate

        public float mainBPM;

        public double SR;

        public List<double[]> diffSection = new List<double[]>();

        public double ARms;
        public double ODms;

        public Mods mods;

        public int ObjectsCount = 0;
        public List<double[]> TimingPoints = new List<double[]>();
        public List<HitObject> HitObjects = new List<HitObject>();
    }
}
