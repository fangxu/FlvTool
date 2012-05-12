using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using Microsoft.CSharp;

using System.Text;
using System.Windows.Forms;
using hdu.FlvTool.Utils;
using VideoEncoder;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Taskbar;
//using System.Configuration;

namespace FlvTool
{
    public partial class FlvToolMain : Form
    {
        //private bool _changed = false;
        private string title="FlvTool";
        private FlvParser parser = null;
        private string filename = null;
        private string path = null;
        private string videoName = null;
        private string audioName = null;

        //private TaskbarManager tm = TaskbarManager.Instance;

        public FlvToolMain()
        {
            InitializeComponent();
            comboBoxPart.SelectedIndex = 0;
        }




        private void textVideo_DragDrop(object sender, DragEventArgs e)
        {
            //parser = null;
            string temp = null;
            temp = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            textVideo.Text = temp;
            videoName = temp.Substring(temp.LastIndexOf("\\") + 1, temp.Length - temp.LastIndexOf("\\") - 1);
            filename = videoName.Substring(0, videoName.LastIndexOf("."));
            path = temp.Substring(0, temp.LastIndexOf('\\') + 1);

            textFlv.Text = path + filename + ".flv";
            if (checkAudio.Checked)
            {
                audioName = filename + ".m4a";
                textAudio.Text = path + audioName;
            }
        }

        private void textVideo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void textAudio_DragDrop(object sender, DragEventArgs e)
        {
            string temp = null;
            temp = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            textAudio.Text = temp;
            videoName = temp.Substring(temp.LastIndexOf("\\") + 1, temp.Length - temp.LastIndexOf("\\") - 1);
            //path = videoName.Substring(0, videoName.LastIndexOf('.'));
        }

        private void textAudio_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void checkAudio_CheckedChanged(object sender, EventArgs e)
        {
            textAudio.Enabled = !checkAudio.Checked;
        }

        private void buttonMerger_Click(object sender, EventArgs e)
        {
            
            if (textAudio.Text == null || textVideo.Text == null) return;

            if (!File.Exists(textVideo.Text))
            {
                MessageBox.Show("video file does not exist!", "no such file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(textAudio.Text))
            {
                MessageBox.Show("audio file does not exist!", "no such file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (File.Exists(path + filename + ".flv"))
            {
                if (MessageBox.Show(this, filename + ".flv 文件已经存在，是否覆盖？", "覆盖",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                    File.Delete(path + filename + ".flv");
                else
                    return;
            }
            EnableUI(false);
            this.Text = title + " - 正在合并...";
            textStat.Clear();
            new Thread(() =>
            {
                Process p = new Process();
                string ph = Environment.CommandLine;
                ph = ph.Substring(0, ph.LastIndexOf('\\') + 1);
                if (ph[0] == '"')
                    ph = ph.Substring(1);
                //File.Delete(ph + "~tmp.flv");

                p.StartInfo.FileName = "\"" + ph + "ffmpeg.exe\"";
                p.StartInfo.Arguments = " -i " + textVideo.Text
                    + " -i " + textAudio.Text
                    + " -vcodec copy -acodec copy "
                    + path + filename + ".flv";
                p.StartInfo.RedirectStandardError = true;
                p.ErrorDataReceived += new DataReceivedEventHandler(MergerOutput);
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                //p.WaitForExit();
                p.BeginErrorReadLine();
                p.WaitForExit();
                p.Close();
                p.Dispose();
                //if (p.ExitCode != 0)
                //{
                //_filename = null;
                //_path = null;
                //MessageBox.Show(this, "转换flv失败 #" + p.ExitCode
                // + "\n" + p.StandardError.ReadToEnd(), "ffmpeg",
                // MessageBoxButtons.OK, MessageBoxIcon.Error);
                //EnableUI(true, "转码flv失败...");
                //return;
                //}
                this.BeginInvoke(new MethodInvoker(() =>
                     {
                         EnableUI(true);
                         this.Text = title+" - 合并完成!";
                     }));
            }).Start();
        }

        private void MergerOutput(object sendProcess, DataReceivedEventArgs output)
        {
            if (String.IsNullOrEmpty(output.Data)) return;
            this.Invoke(new MethodInvoker(() =>
            {
                textStat.AppendText(output.Data + "\r\n");
                //listBoxStat.
            }));

        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            if (!textFlv.Text.EndsWith(".flv"))
            {
                MessageBox.Show("It's not flv file!", "not flv", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!File.Exists(textFlv.Text))
            {
                MessageBox.Show("flv file does not exist!", "no such file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableUI(false);
            this.Text = title + " - 正在分割...";
            textStat.Clear();
            int parts = int.Parse(comboBoxPart.Text);
            int seconds = 0;
            if (parser != null)
            {
                seconds = (int)(parser.Duration / 1000) / parts + 10;
            }
            else
            {
                VideoEncoder.Encoder enc = new VideoEncoder.Encoder();
                enc.FFmpegPath = "ffmpeg.exe";
                VideoFile vf = new VideoFile(textFlv.Text);
                enc.GetVideoInfo(vf);
                TimeSpan t = vf.Duration;
                //int tt =(int)t.TotalSeconds;
                seconds = (int)t.TotalSeconds / parts + 10;
            }
            new Thread(() =>
            {
                for (int i = 0; i < parts; i++)
                {
                    Process p = new Process();
                    string ph = Environment.CommandLine;
                    ph = ph.Substring(0, ph.LastIndexOf('\\') + 1);
                    if (ph[0] == '"')
                        ph = ph.Substring(1);
                    p.StartInfo.FileName = "\"" + ph + "ffmpeg.exe\"";
                    p.StartInfo.Arguments = " -ss " + (i == 0 ? 0 : seconds * i - 10).ToString()
                        + " -i " + textFlv.Text
                        + " -vcodec copy -acodec copy "
                        + " -t " + (i == 0 ? seconds : (seconds + 10)).ToString() + " "
                        + textFlv.Text.Substring(0, textFlv.Text.LastIndexOf(".")) + (i + 1).ToString("00") + ".flv";
                    p.StartInfo.RedirectStandardError = true;
                    p.ErrorDataReceived += new DataReceivedEventHandler(SplitOutput);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.BeginErrorReadLine();
                    p.WaitForExit();
                    p.Close();
                    p.Dispose();

                }

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    EnableUI(true);
                    this.Text = title + " - 分割完成!";
                }));
            }).Start();

        }

        private void SplitOutput(object sendProcess, DataReceivedEventArgs output)
        {
            if (String.IsNullOrEmpty(output.Data)) return;
            this.Invoke(new MethodInvoker(() =>
            {
                textStat.AppendText(output.Data + "\r\n");
                //listBoxStat.
            }));

        }

        private bool AnalysVideo()
        {
            if (!File.Exists(textFlv.Text))
            {
                MessageBox.Show("flv file does not exist!", "no such file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            EnableUI(false);
            new Thread(() =>
            {
                FileStream stream = null;
                stream = new FileStream(textFlv.Text, FileMode.Open, FileAccess.Read);
                this.Invoke(new MethodInvoker(() =>
                {
                    progressBar.Maximum = (int)stream.Length;
                    progressBar.Value = 0;
                    Application.DoEvents();

                }));

                parser = new FlvParser(stream, (tag) =>
                {

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        progressBar.Value = (int)tag.Offset;
                        ChangeTaskBar();
                        if (progressBar.Value == progressBar.Maximum)
                        {
                            progressBar.Value = 0;
                        }
                    }));

                    return true;
                });
                stream.Close();
                stream.Dispose();

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    textStat.Text = "File:" + textFlv.Text + "\r\n"
                        + "Length:" + (parser.Length / 1024.0 / 1024.0).ToString("0.0") + " Mb" + "\r\n"
                        + "Duration:" + parser.Duration / 1000 + " s" + "\r\n"
                        + "Rate:" + parser.Rate + "Kbps";
                    EnableUI(true);
                }));
            }).Start();
            return true;
        }

        private void buttonAnaly_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textFlv.Text))
            {
                MessageBox.Show("flv file does not exist!", "no such file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableUI(false);
            this.Text = title + " - 正在分析...";
            new Thread(() =>
            {
                FileStream stream = null;
                stream = new FileStream(textFlv.Text, FileMode.Open, FileAccess.Read);
                this.Invoke(new MethodInvoker(() =>
                {
                    progressBar.Maximum = (int)stream.Length;
                    progressBar.Value = 0;
                    Application.DoEvents();

                }));

                parser = new FlvParser(stream, (tag) =>
                {

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        progressBar.Value = (int)tag.Offset;
                        ChangeTaskBar();
                        if (progressBar.Value == progressBar.Maximum)
                        {
                            progressBar.Value = 0;
                        }
                    }));

                    return true;
                });
                stream.Close();
                stream.Dispose();

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    textStat.Clear();
                    textStat.Text = "File:" + textFlv.Text + "\r\n"
                        + "Length:" + (parser.Length / 1024.0 / 1024.0).ToString("0.0") + " Mb" + "\r\n"
                        + "Duration:" + parser.Duration / 1000 + " s" + "\r\n"
                        + "Rate:" + parser.Rate + "Kbps";
                    EnableUI(true);
                    this.Text = title + " - 分析完成!";
                    progressBar.Value = progressBar.Minimum;
                    ChangeTaskBar();

                }));
            }).Start();

        }

        private void buttonBlack_Click(object sender, EventArgs e)
        {
            if (parser == null)
            {
                MessageBox.Show("Please analy this flv firstly!", "Analy first!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                //                 if (!AnalysVideo())
                //                 {
                //                     return;
                //                 }

            }
            double rate = (double)upDownRate.Value;
            double o_rate = parser.Rate;
            if (o_rate < rate)
            {
                // 不需要转换
                MessageBox.Show(this, "此视频不需要转换", "后黑",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            long filesize = parser.Length + 16;
            double duration;
            duration = filesize / 125.0 / rate; // * 8 / 1000 / rate
            string offset = ((duration * 1000 - parser.Duration) / 1000).ToString("0.000");
            EnableUI(false);
            this.Text = title + " - 正在后黑...";
            new Thread(() =>
            {
                Stream src = new FileStream(textFlv.Text, FileMode.Open);
                string path = textFlv.Text.Substring(0, textFlv.Text.LastIndexOf(".")) + ".black.flv";
                //DirectoryInfo di = new DirectoryInfo(toolComboFolder.Text);
                //                 if (di.Exists)
                //                 {
                //                     path = di.FullName + path.Substring(path.LastIndexOf('\\'));
                //                 }
                Stream dest = new FileStream(path, FileMode.Create);
                WriteHead(dest, filesize, duration, -1, -1, -1, 1.0, 0,
                    0, parser.Tags.Count - 1, false);
                for (int i = 1; i < parser.Tags.Count; i++)
                {
                    src.Seek(parser.Tags[i].Offset - 11, SeekOrigin.Begin);
                    FlvParser.FlvTag tag = parser.Tags[i];
                    byte[] bs = new byte[tag.DataSize + 11];
                    // 数据
                    src.Read(bs, 0, bs.Length);
                    dest.Write(bs, 0, bs.Length);
                    // prev tag size
                    src.Read(bs, 0, 4);
                    dest.Write(bs, 0, 4);

                    //                     this.BeginInvoke(new MethodInvoker(() =>
                    //                     {
                    //                         toolProgress.Value = i;
                    //                     }));
                }
                src.Close();
                src.Dispose();
                byte[] buffer = new byte[]{
                    0x09, 0, 0, 0x01, // 视频帧 1 字节
                    0, 0, 0, 0,       // 04h, timestamp & ex
                    0, 0, 0,          // stream id
                    0x17,             // InnerFrame, H.264
                    0, 0, 0, 0x0c     // 此帧长度 12 字节
                };
                uint dur = (uint)(duration * 1000);
                PutTime(buffer, 0x04, dur);
                dest.Write(buffer, 0, buffer.Length);

                dest.Flush();
                dest.Close();
                dest.Dispose();


                //                 this.Invoke(new MethodInvoker(() =>
                //                 {
                //                     MessageBox.Show(this, "处理完毕，后黑了 " + offset + " 秒！",
                //                        "后黑", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                     
                //                     EnableUI(true, "后黑处理完成: " + offset + " 秒");
                //                 }));
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MessageBox.Show("Black " + offset + " seconds!", "Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EnableUI(true);
                    this.Text = title + " - 完成后黑!";
                }));
            }).Start();
        }



        #region - Write 函数 -
        private int PutInt(byte[] dest, int pos, int val, int length)
        {
            if (length <= 0)
                return pos;
            for (int i = length - 1; i >= 0; i--)
            {
                dest[pos + i] = (byte)(val & 0xFF);
                val >>= 8;
            }
            return pos + length;
        }
        private int WriteString(byte[] dest, int pos, string str, bool type)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            if (type)
                dest[pos++] = 0x2;
            byte[] bs = Encoding.ASCII.GetBytes(str);
            pos = PutInt(dest, pos, bs.Length, 2);
            bs.CopyTo(dest, pos);
            pos += bs.Length;
            return pos;
        }
        private int WriteString(byte[] dest, int pos, string str)
        {
            return WriteString(dest, pos, str, false);
        }
        private int WriteDouble(byte[] dest, int pos, double val)
        {
            dest[pos++] = 0;
            byte[] bd = BitConverter.GetBytes(val);
            for (int i = 0; i < 8; i++)
            {
                dest[pos++] = bd[7 - i];
            }
            return pos;
        }
        private int WriteByte(byte[] dest, int pos, byte b)
        {
            dest[pos++] = 0x1;
            dest[pos++] = b;
            return pos;
        }

        private void WriteHead(Stream dest, long datasize, double duration, double vcodec, double acodec,
            double framerate, double x, uint offset_b, int f1, int f2, bool reserve)
        {
            int framecount = f2 - f1 + 1;
            if (framecount <= 0)
                throw new Exception("帧不能为空！");

            double audiosize = 0;
            double videosize = 0;
            double audiocodec = acodec;
            double videocodec = vcodec;
            double lasttimestamp = 0;
            double lastkeyframetimestamp = 0;
            double lastkeyframelocation = 0;
            List<double> filepositions = new List<double>();
            List<double> times = new List<double>();

            long first_offset = 0;
            bool res = reserve;
            for (int i = f1; i <= f2; i++)
            {
                if ((first_offset == 0) && !(parser.Tags[i] is FlvParser.ScriptTag))
                {
                    first_offset = parser.Tags[i].Offset;
                }
                FlvParser.AudioTag atag = parser.Tags[i] as FlvParser.AudioTag;
                if (atag != null)
                {
                    if (audiocodec < 0)
                        audiocodec = atag.CodecId;
                    audiosize += atag.DataSize + 11;
                    continue;
                }
                FlvParser.VideoTag vtag = parser.Tags[i] as FlvParser.VideoTag;
                if (vtag != null)
                {
                    if (videocodec < 0)
                        videocodec = vtag.CodecId;
                    videosize += vtag.DataSize + 11;
                    lasttimestamp = Math.Round((vtag.TimeStamp - offset_b) * x) / 1000.0;
                    if (vtag.FrameType == "keyframe")
                    {
                        if (res)
                        {
                            lasttimestamp = vtag.TimeStamp / 1000.0;
                            res = false;
                        }
                        lastkeyframetimestamp = lasttimestamp;
                        lastkeyframelocation = vtag.Offset - first_offset;
                        filepositions.Add(lastkeyframelocation);
                        times.Add(lastkeyframetimestamp);
                    }
                    continue;
                }
            }
            FlvParser.ScriptTag meta = parser.MetaTag;

            byte[] bhead = new byte[] {
                0x46, 0x4c, 0x56, // FLV
                0x01,             // Version 1
                0x05,             // 0000 0101, 有音频有视频
                0, 0, 0, 0x09,    // Header size, 9
                0, 0, 0, 0,       // Previous Tag Size #0
            };
            int pos = 0;
            byte[] buffer = new byte[63356];
            buffer[pos++] = 0x12; // script
            #region - 开始写 -
            for (int i = 0; i < 10; i++)
            {
                buffer[pos++] = 0;
            }
            pos = WriteString(buffer, pos, "onMetaData", true);
            buffer[pos++] = 0x08;
            pos = PutInt(buffer, pos, 26, 4);

            object o;
            double d;

            pos = WriteString(buffer, pos, "creator");
            pos = WriteString(buffer, pos, "wuyuans.com", true);

            pos = WriteString(buffer, pos, "metadatacreator");
            pos = WriteString(buffer, pos, "Metadata creator - by Wuyuan", true);

            pos = WriteString(buffer, pos, "hasKeyframes");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasVideo");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasAudio");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasMetadata");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "canSeekToEnd");
            pos = WriteByte(buffer, pos, 0);

            pos = WriteString(buffer, pos, "duration");
            pos = WriteDouble(buffer, pos, duration);
            pos = WriteString(buffer, pos, "datasize");
            pos = WriteDouble(buffer, pos, datasize);
            pos = WriteString(buffer, pos, "videosize");
            pos = WriteDouble(buffer, pos, videosize);
            pos = WriteString(buffer, pos, "videocodecid");
            pos = WriteDouble(buffer, pos, videocodec);

            pos = WriteString(buffer, pos, "width");
            d = 512.0;
            if (meta.TryGet("width", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "height");
            d = 384.0;
            if (meta.TryGet("height", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "framerate");
            d = framerate > 0 ? framerate : (framecount / duration);
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "videodatarate");
            pos = WriteDouble(buffer, pos, videosize / 125.0 / duration);

            pos = WriteString(buffer, pos, "audiosize");
            pos = WriteDouble(buffer, pos, audiosize);
            pos = WriteString(buffer, pos, "audiocodecid");
            pos = WriteDouble(buffer, pos, audiocodec);
            pos = WriteString(buffer, pos, "audiosamplerate");
            d = 44100;
            if (meta.TryGet("audiosamplerate", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);
            pos = WriteString(buffer, pos, "audiosamplesize");
            d = 16;
            if (meta.TryGet("audiosamplesize", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);
            pos = WriteString(buffer, pos, "stereo");
            byte stereo = 1;
            if (meta.TryGet("stereo", out o))
                stereo = (byte)o;
            pos = WriteByte(buffer, pos, stereo);
            pos = WriteString(buffer, pos, "audiodatarate");
            pos = WriteDouble(buffer, pos, audiosize / 125.0 / duration);

            pos = WriteString(buffer, pos, "filesize");
            int filesize_pos = pos;
            pos += 9;

            pos = WriteString(buffer, pos, "lasttimestamp");
            pos = WriteDouble(buffer, pos, lasttimestamp);
            pos = WriteString(buffer, pos, "lastkeyframetimestamp");
            pos = WriteDouble(buffer, pos, lastkeyframetimestamp);
            pos = WriteString(buffer, pos, "lastkeyframelocation");
            pos = WriteDouble(buffer, pos, lastkeyframelocation);
            #endregion
            pos = WriteString(buffer, pos, "keyframes");
            buffer[pos++] = 3; // object
            pos = WriteString(buffer, pos, "filepositions");
            int file_positions = pos;
            pos = WriteArray(buffer, pos, filepositions);
            pos = WriteString(buffer, pos, "times");
            pos = WriteArray(buffer, pos, times);
            buffer[pos++] = 0;
            buffer[pos++] = 0;
            buffer[pos++] = 9; // 结束符

            // script tag 长度
            PutInt(buffer, 1, pos - 11, 3); // script 帧的 datasize
            pos = PutInt(buffer, pos, pos, 4);
            WriteDouble(buffer, filesize_pos, datasize + pos + bhead.Length); // filesize
            WriteArray(buffer, file_positions, filepositions, pos + bhead.Length + (reserve ? 27 : 0));

            dest.Write(bhead, 0, bhead.Length);
            dest.Write(buffer, 0, pos);
        }
        private int WriteArray(byte[] dest, int pos, List<double> ds)
        {
            return WriteArray(dest, pos, ds, 0.0);
        }
        private int WriteArray(byte[] dest, int pos, List<double> ds, double offset)
        {
            dest[pos++] = 0xa;
            pos = PutInt(dest, pos, ds.Count, 4);
            for (int i = 0; i < ds.Count; i++)
            {
                pos = WriteDouble(dest, pos, ds[i] + offset);
            }
            return pos;
        }

        private void PutTime(byte[] bs, int pos, uint value)
        {
            for (int i = 2; i >= 0; i--)
            {
                bs[pos + i] = (byte)(value & 0xff);
                value >>= 8;
            }
            bs[pos + 3] = (byte)(value & 0xff);
        }
        private byte[] GetH263Frame(uint timestamp, ushort width, ushort height)
        {
            long b = (1 << 16) | width;
            b = (b << 16) | height;
            b <<= 7;
            byte[] buffer = new byte[]{
                    0x09, 0, 0, 0x0c, // 视频帧 12 字节
                    0, 0, 0, 0,       // timestamp & ex
                    0, 0, 0,          // stream id
                    0x22, 0, 0, 0x84, 0, // InnerFrame, H.263
                    0, 0, 0, 0, 0, 0x12, 0x26, // 16~20:width x height
                    0, 0, 0, 0x17     // 此帧长度 23 字节
                };
            PutTime(buffer, 4, timestamp);
            for (int i = 0; i < 5; i++)
            {
                buffer[20 - i] = (byte)(b & 0xFF);
                b >>= 8;
            }
            return buffer;
        }
        #endregion

        private void textFlv_DragDrop(object sender, DragEventArgs e)
        {

            textFlv.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            //videoName = temp.Substring(temp.LastIndexOf("\\") + 1, temp.Length - temp.LastIndexOf("\\") - 1);

        }

        private void textFlv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void checkTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkTop.Checked;
        }



        private void FlvToolMain_Load(object sender, EventArgs e)
        {
            //GetConfig();
            upDownRate.Value = Properties.Settings.Default.rate;
            checkAudio.Checked = Properties.Settings.Default.checkAudio;
            comboBoxPart.Text = Properties.Settings.Default.parts;
            checkTop.Checked = Properties.Settings.Default.alwaysTop;
            if (checkAudio.Checked)
            {
                textAudio.Enabled = false;
            }
        }

        private void FlvToolMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.rate = upDownRate.Value;
            Properties.Settings.Default.checkAudio = checkAudio.Checked;
            Properties.Settings.Default.parts = comboBoxPart.Text;
            Properties.Settings.Default.alwaysTop = checkTop.Checked;
            Properties.Settings.Default.Save();

        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            //AboutBox ab = new AboutBox();
            this.TopMost = false;
            //ab.ShowDialog();
            new AboutBox().ShowDialog();
            this.TopMost = checkTop.Checked;
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            //new FormAdvanced(this).Show();
            FormAdvanced fa = new FormAdvanced();
            //fa.Owner = this;
            fa.upDownRate.Value = this.upDownRate.Value;
            fa.Show();
        }

        private void ChangeTaskBar()
        {
            //CoreHelpers.ThrowIfNotWin7();
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressValue(progressBar.Value, progressBar.Maximum);
                if (progressBar.Value == progressBar.Maximum || progressBar.Value == progressBar.Minimum)
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
            //tm ;



        }

        private void EnableUI(Boolean enable)
        {
            buttonMerger.Enabled = enable;
            buttonAnaly.Enabled = enable;
            buttonSplit.Enabled = enable;
            buttonBlack.Enabled = enable;
            buttonAbout.Enabled = enable;
            buttonAdvanced.Enabled = enable;
        }
    }
}
