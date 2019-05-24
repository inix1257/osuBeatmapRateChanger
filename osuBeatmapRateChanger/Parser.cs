using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuBeatmapRateChanger
{
    class Parser
    {
        private const double OD0Ms = 79.5;
        private const double OD10Ms = 19.5;
        private const double AR0Ms = 1800.0;
        private const double AR5Ms = 1200.0;
        private const double AR10Ms = 450.0;

        private const double ODMsStep = (OD0Ms - OD10Ms) / 10.0;
        private const double ARMsStep1 = (AR0Ms - AR5Ms) / 5.0;
        private const double ARMsStep2 = (AR5Ms - AR10Ms) / 5.0;

        private float speedMtpr = 1f;
        private float statMtpr = 1f;
        private float CSMtpr = 1f;

        private int firstnoteOffset = 0;
        private int lastnoteOffset = 0;

        private Boolean isARExists = false;

        public Parser(Beatmap bm, StreamReader reader = null)
        {
            if (reader == null)
            {
                Console.WriteLine(bm.Title);

                List<HitObject> _hitObjects = bm.HitObjects;

                for (int i = 0; i < bm.ObjectsCount; i++)
                {
                    bm.HitObjects[i].offset = (int)(bm.HitObjects[i].offset / speedMtpr);
                }

                bm.CS *= CSMtpr;
                bm.AR *= statMtpr;
                bm.OD *= statMtpr;
                bm.HP *= statMtpr;

                bm.CS = Math.Min(10, bm.CS);
                bm.AR = Math.Min(10, bm.AR);
                bm.OD = Math.Min(10, bm.OD);
                bm.HP = Math.Min(10, bm.HP);

                return;
            }

            string line, section = null;
            while ((line = reader.ReadLine()?.Trim()) != null)
            {
                if (line.StartsWith("[") && line.Contains("]"))
                {
                    section = line.Substring(1, line.Length - 2);
                    continue;
                }

                if (line.Length <= 0) continue;

                switch (section)
                {
                    case "Metadata":
                        String key = line.Split(':')[0];
                        String value = line.Split(':')[1];
                        switch (key)
                        {
                            case "Title":
                                bm.Title = value;
                                break;
                            case "TitleUnicode":
                                bm.TitleUnicode = value;
                                break;
                            case "Artist":
                                bm.Artist = value;
                                break;
                            case "ArtistUnicode":
                                bm.ArtistUnicode = value;
                                break;
                            case "Version":
                                bm.Version = value;
                                break;
                            case "Creator":
                                bm.Creator = value;
                                break;
                        }
                        break;

                    case "General":
                        String key_gen = line.Split(':')[0];
                        String value_gen = line.Split(new string[] { ": "}, StringSplitOptions.None)[1];
                        switch (key_gen)
                        {
                            case "AudioFilename":
                                bm.AudioFileName = value_gen;
                                break;
                            case "AudioLeadIn":
                                bm.AudioLeadIn = value_gen;
                                break;
                            case "PreviewTime":
                                bm.PreviewTime = value_gen;
                                break;
                            case "Countdown":
                                bm.Countdown = value_gen;
                                break;
                            case "SampleSet":
                                bm.SampleSet = value_gen;
                                break;
                            case "StackLeniency":
                                bm.StackLeniency = value_gen;
                                break;
                            case "Mode":
                                bm.Mode = value_gen;
                                break;
                            case "LetterboxInBreaks":
                                bm.LetterboxInBreaks = value_gen;
                                break;
                            case "WidescreenStoryboard":
                                bm.WidescreenStoryboard = value_gen;
                                break;
                        }
                        break;

                    case "Editor":
                        bm.isEditorExist = true;
                        String key_edit = line.Split(':')[0];
                        String value_edit = line.Split(new string[] { ": " }, StringSplitOptions.None)[1];
                        switch (key_edit)
                        {
                            case "Bookmarks":
                                int count = value_edit.Split(',').Length;
                                for(int i=0; i<count; i++)
                                {
                                    bm.Bookmarks.Add(int.Parse(value_edit.Split(',')[i]));
                                }
                                break;

                            case "DistanceSpacing":
                                bm.DistanceSpacing = value_edit;
                                break;
                            case "BeatDivisor":
                                bm.BeatDivisor = value_edit;
                                break;
                            case "GridSize":
                                bm.GridSize = value_edit;
                                break;
                            case "TimelineZoom":
                                bm.TimelineZoom = value_edit;
                                break;
                        }
                        break;

                    case "Difficulty":
                        String key_diff = line.Split(':')[0];
                        float val_diff = float.Parse(line.Split(':')[1]);
                        switch (key_diff)
                        {
                            case "CircleSize":
                                bm.CS = val_diff;
                                break;
                            case "OverallDifficulty":
                                bm.OD = val_diff;
                                break;
                            case "ApproachRate":
                                isARExists = true;
                                bm.AR = val_diff;
                                break;
                            case "HPDrainRate":
                                bm.HP = val_diff;
                                break;
                            case "SliderMultiplier":
                                bm.SV = val_diff;
                                break;
                            case "SliderTickRate":
                                bm.ST = val_diff;
                                break;
                        }
                        break;

                    case "Events":
                        if(line.Contains(".png") || line.Contains(".jpg"))
                        {
                            bm.Events_bg = line;
                            break;
                        }
                        if (line.StartsWith("2,"))
                        {
                            int[] breaks = new int[2];
                            breaks[0] = int.Parse(line.Split(',')[1]);
                            breaks[1] = int.Parse(line.Split(',')[2]);

                            bm.Events_break.Add(breaks);
                        }
                        break;

                    case "TimingPoints":
                        double[] tp = new double[line.Split(',').Length];
                        for (int i = 0; i < line.Split(',').Length; i++)
                        {
                            tp[i] = double.Parse(line.Split(',')[i]);
                            if (i == 2)
                            {
                                tp[i] = double.Parse(line.Split(',')[i]) / speedMtpr;
                            }
                        }

                        bm.TimingPoints.Add(tp);
                        break;

                    case "HitObjects":
                        if(bm.ObjectsCount == 0)
                        {
                            firstnoteOffset = int.Parse(line.Split(',')[2]);
                        }
                        bm.ObjectsCount++;
                        String[] str = line.Split(',');
                        HitObjectType objType = (HitObjectType)int.Parse(str[3]);
                        HitObject obj = new HitObject(int.Parse(str[0]), int.Parse(str[1]), (int)(int.Parse(str[2]) / speedMtpr), objType, line.Split(new string[] { str[2] }, StringSplitOptions.None)[1], line);
                        bm.HitObjects.Add(obj);
                        break;
                }
            }

            lastnoteOffset = bm.HitObjects[bm.ObjectsCount-1].offset;

            double mainBPM = 0;
            double prevBPM = 0;
            int prevOffset = 0, offsetGap = 0;
            Boolean isSingleBPM = true;
            double currBPM = 0;
            int currOffset = 0;

            foreach(double[] tmp in bm.TimingPoints)
            {
                if (tmp[1] < 0) continue;
                currBPM = tmp[1];
                currOffset = (int)tmp[0];

                Console.WriteLine("currOffset : " + currOffset);
                Console.WriteLine("currBPM : " + currBPM);

                if(offsetGap < currOffset - prevOffset)
                {
                    if (offsetGap != 0) isSingleBPM = false;
                    offsetGap = currOffset - prevOffset;
                    
                    mainBPM = prevBPM;
                    
                    Console.WriteLine("new longest bpm! : " + mainBPM);
                }
                prevOffset = currOffset;
                prevBPM = currBPM;
            }

            if(lastnoteOffset - currOffset > offsetGap)
            {
                mainBPM = prevBPM;
            }

            if (isSingleBPM)
            {
                Console.WriteLine("single bpm detected");
                mainBPM = prevBPM;
            }
            

            bm.mainBPM = (float)Math.Round((1000d / mainBPM) * 60d);
            Console.WriteLine("mainBPM : " + bm.mainBPM);

            if (!isARExists) bm.AR = bm.OD;
           
            bm.CS *= CSMtpr;
            bm.AR *= statMtpr;
            bm.OD *= statMtpr;
            bm.HP *= statMtpr;

            bm.CS = Math.Min(10, bm.CS);
            bm.AR = Math.Min(10, bm.AR);
            bm.OD = Math.Min(10, bm.OD);
            bm.HP = Math.Min(10, bm.HP);

            bm.ARms = bm.AR < 5.0f ?
                    AR0Ms - ARMsStep1 * bm.AR
                    : AR5Ms - ARMsStep2 * (bm.AR - 5.0f);

            bm.ARms = Math.Min(AR0Ms, Math.Max(AR10Ms, bm.ARms));
            bm.ARms /= speedMtpr;

            bm.AR = (float)(
                bm.ARms > AR5Ms
                ? (AR0Ms - bm.ARms) / ARMsStep1
                : 5.0 + (AR5Ms - bm.ARms) / ARMsStep2
            );

            bm.ODms = OD0Ms - Math.Ceiling(ODMsStep * bm.OD);
            bm.ODms = Math.Min(OD0Ms, Math.Max(OD10Ms, bm.ODms));
            bm.ODms /= speedMtpr;
            bm.OD = (float)((OD0Ms - bm.ODms) / ODMsStep);

        }
    }
}
