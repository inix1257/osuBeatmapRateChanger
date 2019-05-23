using osuBeatmapRateChanger.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osuBeatmapRateChanger
{
    public partial class Form1 : Form
    {
        private const double OD0Ms = 79.5;
        private const double OD10Ms = 19.5;
        private const double AR0Ms = 1800.0;
        private const double AR5Ms = 1200.0;
        private const double AR10Ms = 450.0;

        private const double ODMsStep = (OD0Ms - OD10Ms) / 10.0;
        private const double ARMsStep1 = (AR0Ms - AR5Ms) / 5.0;
        private const double ARMsStep2 = (AR5Ms - AR10Ms) / 5.0;

        int intQuality;
        Process prcFFMPEG = new Process();
        bool adjustmapstats;
        float CS, AR, OD, HP;
        Beatmap bm;
        string filename, dirname, safename;

        float rate;

        public Form1()
        {
            InitializeComponent();
        }

        private void percentTXT_Click(object sender, EventArgs e)
        {

        }

        private void bpminput_TextChanged(object sender, EventArgs e)
        {
            rateCalc();
        }

        private void bpmoutput_TextChanged(object sender, EventArgs e)
        {
            rateCalc();
        }

        private void rateCalc()
        {
            if (bpminput.Text == "" || bpmoutput.Text == "") return;
            float bpm1 = float.Parse(bpminput.Text);
            float bpm2 = float.Parse(bpmoutput.Text);
            ratebox.Value = (decimal)(Math.Max(Math.Min(bpm2 / bpm1, 10), 0.1));
        }

        private void ratebox_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void convert_btn_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            percentTXT.Text = "0%";
            rate = (float)ratebox.Value;
            try
            {

                adjustmapstats = adjustCheckBox.Checked;
                string strFFMPEGOut;
                ProcessStartInfo psiProcInfo = new ProcessStartInfo();
                TimeSpan estimatedTime = TimeSpan.MaxValue;

                StreamReader srFFMPEG;
                string strFFMPEGCmd = " -i \"" + dirname + bm.AudioFileName + "\" -filter:a \"atempo=" + rate + "\" -y \"" + dirname + bm.AudioFileName.Replace(".mp3", "") + " x" + rate + ".mp3\"";
                Console.WriteLine(strFFMPEGCmd);

                psiProcInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
                psiProcInfo.Arguments = strFFMPEGCmd;
                psiProcInfo.UseShellExecute = false;
                psiProcInfo.WindowStyle = ProcessWindowStyle.Hidden;
                psiProcInfo.RedirectStandardError = true;
                psiProcInfo.RedirectStandardOutput = true;
                psiProcInfo.CreateNoWindow = true;

                prcFFMPEG.StartInfo = psiProcInfo;

                prcFFMPEG.Start();

                srFFMPEG = prcFFMPEG.StandardError;

                try
                {
                    do
                    {
                        strFFMPEGOut = srFFMPEG.ReadLine();

                        if (strFFMPEGOut == null)
                        {
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                progressBar1.Value = 100;
                                percentTXT.Text = "done!";
                            }));
                            
                            break;
                        }

                        string duration = "Duration";
                        if (strFFMPEGOut.TrimStart().IndexOf(duration) == 0)
                        {
                            try
                            {
                                string text = strFFMPEGOut.TrimStart().Substring(duration.Length + 2);
                                int pos = text.IndexOf(",");
                                string estimated = text.Substring(0, pos);

                                estimatedTime = TimeSpan.Parse(estimated);
                            }
                            catch
                            {
                            }
                        }

                        if (estimatedTime != TimeSpan.MaxValue)
                        {
                            string time = "time=";
                            int startPos = strFFMPEGOut.IndexOf(time);
                            if (startPos != -1)
                            {
                                string text = strFFMPEGOut.Substring(startPos + time.Length);
                                int pos = text.IndexOf(" ");
                                string current = text.Substring(0, pos);

                                TimeSpan currentTime = TimeSpan.Parse(current);

                                int progresss = (int)(currentTime.TotalMilliseconds * 100 / estimatedTime.TotalMilliseconds);
                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    progressBar1.Value = Math.Min(progresss, 100);
                                    percentTXT.Text = progresss + "%";
                                }));
                            }

                        }

                        Console.WriteLine(strFFMPEGOut);
                    } while (prcFFMPEG.HasExited == false);
                }catch(Exception er)
                {
                    Console.WriteLine("312321" + er.ToString());
                    progressBar1.Value = 0;
                    percentTXT.Text = "done!";
                }

            }
            catch (Exception ee)
            {
            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    this.Text = "Done!";
                    Console.WriteLine("ffmpeg done");
                }));
            }

            try
            {
                String str = "osu file format v14\n\n";
                str += "[General]\n";
                str += "AudioFilename: " + bm.AudioFileName.Replace(".mp3", "") + " x" + rate + ".mp3\n";
                str += "AudioLeadIn: " + bm.AudioLeadIn + "\n";
                str += "PreviewTime: " + (int)(float.Parse(bm.PreviewTime)/rate) + "\n";
                str += "Countdown: " + bm.Countdown + "\n";
                str += "SampleSet: " + bm.SampleSet + "\n";
                str += "StackLeniency: " + bm.StackLeniency + "\n";
                str += "Mode: " + bm.Mode + "\n";
                str += "LetterboxInBreaks: " + bm.LetterboxInBreaks + "\n";
                if(bm.WidescreenStoryboard != null) str += "WidescreenStoryboard: " + bm.WidescreenStoryboard + "\n\n";
                if (bm.isEditorExist)
                {
                    str += "[Editor]\n";
                    str += "Bookmarks: ";
                    for (int i = 0; i < bm.Bookmarks.Count; i++)
                    {
                        if (i != 0) str += ",";
                        str += (int)((bm.Bookmarks[i]) / rate);
                    }
                    str += "\n";
                    str += "DistanceSpacing: " + bm.DistanceSpacing + "\n";
                    str += "BeatDivisor: " + bm.BeatDivisor + "\n";
                    str += "GridSize: " + bm.GridSize + "\n";
                    if (bm.TimelineZoom != null) str += "TimelineZoom: " + bm.TimelineZoom + "\n\n";
                }
                str += "[Metadata]\n";
                str += "Title:" + bm.Title + "\n";
                str += "TitleUnicode:" + bm.TitleUnicode + "\n";
                str += "Artist:" + bm.Artist + "\n";
                str += "ArtistUnicode:" + bm.ArtistUnicode + "\n";
                str += "Creator:" + bm.Creator + "\n";
                str += "Version:" + bm.Version + " x" + rate + "\n";
                str += "Source:" + bm.Source + "\n";
                str += "Tags:" + bm.Tags + "\n";
                str += "BeatmapID:0\n";
                str += "BeatmapSetID:-1\n\n";
                str += "[Difficulty]\n";
                str += "HPDrainRate:" + bm.HP + "\n";
                str += "CircleSize:" + bm.CS + "\n";
                if (adjustmapstats)
                {
                    bm.ODms /= rate;
                    bm.OD = (float)((OD0Ms - bm.ODms) / ODMsStep);
                    bm.ARms /= rate;

                    bm.AR = (float)(
                        bm.ARms > AR5Ms
                        ? (AR0Ms - bm.ARms) / ARMsStep1
                        : 5.0 + (AR5Ms - bm.ARms) / ARMsStep2
                    );
                    str += "OverallDifficulty:" + bm.OD + "\n";
                    str += "ApproachRate:" + bm.AR + "\n";
                }
                else
                {
                    str += "OverallDifficulty:" + bm.OD + "\n";
                    str += "ApproachRate:" + bm.AR + "\n";
                }
                
                str += "SliderMultiplier:" + bm.SV + "\n";
                str += "SliderTickRate:" + bm.ST + "\n\n";
                str += "[Events]\n";
                str += bm.Events_bg + "\n";
                for(int i=0; i<bm.Events_break.Count; i++)
                {
                    float b1 = bm.Events_break[i][0] / rate;
                    float b2 = bm.Events_break[i][1] / rate;
                    str += "2," + (int)b1 + "," + (int)b2 + "\n";
                }
                str += "\n";
                str += "[TimingPoints]\n";
                for(int i=0; i<bm.TimingPoints.Count; i++)
                {
                    for (int j = 0; j < bm.TimingPoints[i].Length; j++)
                    {
                        if (j == 0){
                            str += (int)(bm.TimingPoints[i][0] / rate) + ",";
                            continue;
                        }
                        if (j == 1 && bm.TimingPoints[i][1] > 0)
                        {
                            str += bm.TimingPoints[i][1] / rate + ",";
                            continue;
                        }
                        if (j != 7) {
                            str += bm.TimingPoints[i][j] + ",";
                        }
                        else
                        {
                            str += bm.TimingPoints[i][j];
                        }
                        
                    }
                    str += "\n";
                }
                str += "[HitObjects]\n";
                for(int i=0; i<bm.HitObjects.Count; i++)
                {
                    String[] s = bm.HitObjects[i].orig.Split(',');

                    if (bm.Mode == "3" && s[3].Equals("128"))
                    {
                        str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," + (int)((float)bm.HitObjects[i].offset / rate) + "," + s[3] + "," + s[4] + "," + (int)(float.Parse(s[5].Split(':')[0]) / rate) + ":" + s[5].Substring(s[5].IndexOf(s[5].Split(':')[0])) + "\n";
                        continue;
                    }
                    if(bm.Mode == "1")
                    {
                        if (s[3].Equals("2") || s[3].Equals("6"))
                        {
                            str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," + (int)((float)bm.HitObjects[i].offset / rate) + "," + s[3] + "," + s[4] + "," + s[5] +","+s[6]+","+s[7]+ "\n";
                            continue;
                        }else if (s[3].Equals("12"))
                        {
                            str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," + (int)((float)bm.HitObjects[i].offset / rate) + "," + s[3] + "," + s[4] + "," + (int)(float.Parse(s[5]) / rate) + "\n";
                            continue;
                        }

                        str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," + (int)((float)bm.HitObjects[i].offset / rate) + "," + s[3] + "," + s[4] + "\n";
                        continue;
                    }
                    if(bm.Mode == "0" && s[3].Equals("12"))
                    {
                        str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," + (int)((float)bm.HitObjects[i].offset / rate) + "," + s[3] +","+ s[4] +","+ (int)(float.Parse(s[5]) / rate) +","+ s[6] + "\n";
                        continue;
                    }
                    str += bm.HitObjects[i].pos_x + "," + bm.HitObjects[i].pos_y + "," +(int)( (float)bm.HitObjects[i].offset/rate) + bm.HitObjects[i].etc + "\n";
                }
                
                
                string[] chars = new string[] {"/", "?", "#", "*", ":", "<", ">", "|", "\"" };
                for (int i = 0; i < chars.Length; i++)
                {
                        bm.Artist = bm.Artist.Replace(chars[i], "");
                        bm.Title = bm.Title.Replace(chars[i], "");
                        bm.Creator = bm.Creator.Replace(chars[i], "");
                        bm.Version = bm.Version.Replace(chars[i], "");
                    
                }
                
                System.IO.File.WriteAllText(dirname + bm.Artist + " - " + bm.Title + " (" + bm.Creator + ") [" + bm.Version + " x" + rate + "].osu", str, Encoding.Unicode);
            }catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = ".osu";
            openFile.Filter = "osu Beatmap Files(*.osu)|*.osu";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                filename = openFile.FileName;
                dirname = filename.Replace(openFile.SafeFileName, "");
                safename = openFile.SafeFileName;

                bm = new Beatmap();

                StreamReader file = new StreamReader(filename);
                new Parser(bm, file);
                Console.WriteLine(string.Format("{0:F2} - {1:F2} [{2:F2}] / {3:F2}", bm.Artist, bm.Title, bm.Version, dirname));

                txtSource.Text = filename;
                txtArtist.Text = bm.Artist;
                txtTitle.Text = bm.Title;
                txtCreator.Text = bm.Creator;
                txtDiff.Text = bm.Version;

                bpminput.Text = bm.mainBPM.ToString();
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
