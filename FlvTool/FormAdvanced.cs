using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hdu.FlvTool.Utils;
using VideoEncoder;

using System.Threading;
using System.IO;
using Microsoft.WindowsAPICodePack.Taskbar;
//using Microsoft.WindowsAPICodePack.Shell;
//using MS.WindowsAPICodePack.Internal;
//using System.Data;
//using System.IO;

namespace FlvTool
{
    public partial class FormAdvanced : Form
    {
        //private FlvParser parser = null;
        //private FlvToolMain mainForm;
        private TaskbarManager tm = null;
        String title = "Advanced";

        public FormAdvanced()
        {
            InitializeComponent();
            //mainForm = (FlvToolMain)this.Owner;
            listViewFlv.Columns.Add("File", -2);
            listViewFlv.Columns.Add("Duration", -2);
            listViewFlv.Columns.Add("BitRate", -2);
            listViewFlv.Columns.Add("VideoRate", -2);
            listViewFlv.Columns.Add("AudioRate", -2);
            this.checkTop.Checked = true;
            this.TopMost = checkTop.Checked;

        }

        private void FormAdvanced_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void EnableUI(Boolean enable)
        {
            buttonBlack.Enabled = enable;
        }

        private void listViewFlv_DragDrop(object sender, DragEventArgs e)
        {
            string[] stringTemp = (string[])e.Data.GetData(DataFormats.FileDrop);
            //textStat.Clear();
            listViewFlv.Items.Clear();
            VideoEncoder.Encoder enc = new VideoEncoder.Encoder();
            enc.FFmpegPath = "ffmpeg.exe";

            for (int i = 0; i < stringTemp.Length; i++)
            {

                VideoFile vf = new VideoFile(stringTemp[i]);
                enc.GetVideoInfo(vf);
                TimeSpan t = vf.Duration;
                //int tt =(int)t.TotalSeconds;
                //seconds = (int)t.TotalSeconds / parts + 10;
                ListViewItem it = new ListViewItem();
                it.Text = stringTemp[i];
                it.SubItems.Add(vf.Duration.ToString().Substring(0, vf.Duration.ToString().LastIndexOf(".")));
                it.SubItems.Add(vf.BitRate.ToString());
                it.SubItems.Add(vf.VideoBitRate.ToString());
                it.SubItems.Add(vf.AudioBitRate.ToString());

                listViewFlv.Items.Add(it);
                //                 stringTemp[i] = stringTemp[i]
                //                     + " Duration:" + t.TotalSeconds
                //                     + " VideoRate:" + vf.VideoBitRate
                //                     + " AudioRate:" + vf.AudioBitRate;

            }
            listViewFlv.Columns[0].Width = -1;
            //listViewFlv.Columns[1].Width = -1;
            //listViewFlv.Columns[4].Width = -1;
        }

        private void listViewFlv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void buttonBlack_Click(object sender, EventArgs e)
        {
            EnableUI(false);
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Maximum = listViewFlv.Items.Count * 2;
            int numbers = listViewFlv.Items.Count;
            string[] files = new string[numbers];


            for (int i = 0; i < numbers; i++)
            {
                files[i] = listViewFlv.Items[i].Text;
            }
            // string s = listViewFlv.Items.

            
            new Thread(() =>
            {
                for (int i = 0; i < numbers; i++)
                {
                    double rate = (double)upDownRate.Value;
                    //string file = null;
                    //file = listViewFlv.Items[i].Text;

                    this.BeginInvoke(new MethodInvoker(() =>
                    {

                         this.Text = title + "--正在处理第" + (i + 1) + "个视频...";
                    }));

                    FileStream stream = null;
                    stream = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                    FlvParser parser = new FlvParser(stream, null);
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        progressBar.Value++;
                        ChangeTaskBar();
                    }));
                    stream.Close();
                    stream.Dispose();

                    if (parser.Rate > rate)
                    {
                        long filesize = parser.Length + 16;
                        double duration;
                        duration = filesize / 125.0 / rate; // * 8 / 1000 / rate
                        string offset = ((duration * 1000 - parser.Duration) / 1000).ToString("0.000");
                        Stream src = new FileStream(files[i], FileMode.Open);
                        string path = files[i].Substring(0, files[i].LastIndexOf(".")) + ".black.flv";

                        Stream dest = new FileStream(path, FileMode.Create);
                        FlvWrite fw = new FlvWrite(parser);
                        fw.WriteHead(dest, filesize, duration, -1, -1, -1, 1.0, 0,
                            0, parser.Tags.Count - 1, false);
                        for (int n = 1; n < parser.Tags.Count; n++)
                        {
                            src.Seek(parser.Tags[n].Offset - 11, SeekOrigin.Begin);
                            FlvParser.FlvTag tag = parser.Tags[n];
                            byte[] bs = new byte[tag.DataSize + 11];
                            // 数据
                            src.Read(bs, 0, bs.Length);
                            dest.Write(bs, 0, bs.Length);
                            // prev tag size
                            src.Read(bs, 0, 4);
                            dest.Write(bs, 0, 4);
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
                        fw.PutTime(buffer, 0x04, dur);
                        dest.Write(buffer, 0, buffer.Length);

                        dest.Flush();
                        dest.Close();
                        dest.Dispose();
                    }
                    this.BeginInvoke(new MethodInvoker(() =>
                     {
                         progressBar.Value++;
                         ChangeTaskBar();
                         if (progressBar.Value == progressBar.Maximum)
                         {
                             progressBar.Value = 0;
                             this.Text =title+ "--处理完成！";
                             EnableUI(true);
                         }
                     }));
                }


            }).Start();


        }

        private void ChangeTaskBar()
        {
            //CoreHelpers.ThrowIfNotWin7();
            if (TaskbarManager.IsPlatformSupported)
            {
                tm.SetProgressValue(progressBar.Value, progressBar.Maximum);
                if (progressBar.Value == progressBar.Maximum)
                {
                    tm.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
            //tm ;



        }

        private void FormAdvanced_Load(object sender, EventArgs e)
        {
            if (TaskbarManager.IsPlatformSupported)
            {
                tm = TaskbarManager.Instance;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkTop.Checked;
        }


    }
}
