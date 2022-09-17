using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Accord.Video.FFMPEG;
using System.IO;
namespace RecorderVideo
{
    class ScreenRecorder
    {
        // Video Variables:
        private Rectangle bounds;
        private string outputPath = "";
        private string tempPath = "";
        private int fileCount = 1;
        private List<string> inputImageSequance = new List<string>();

        // Fille Variables:
        private string audioName = "mic.wav";
        private string videoName = "video.mp4";
        private string finalName = "finalVideo.mp4";

        //time Variables:
        Stopwatch watch = new Stopwatch();

        //Audio Variables 
        public static class NativeMethods //is going to do importing from dll file and creat a method 
        {
            [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
            public static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        }
        public ScreenRecorder(Rectangle b, string outPath)
        {
            CreateTempFolder("tempScreenshots");
            bounds = b;
            outputPath = outPath;
        }
        private void CreateTempFolder(string name)
        {
            if (Directory.Exists("E://"))
            {
                string pathName = $"E://{name}";
                Directory.CreateDirectory(pathName);
                tempPath = pathName;
            }
            else
            {
                string pathName = $"C://{name}";
                Directory.CreateDirectory(pathName);
                tempPath = pathName;
            }
        }
        private void DeletePath(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            foreach (string dir in dirs)
            {
                DeletePath(dir);
            }
            Directory.Delete(targetDir, false);

        }
        private void DeleteFillesExcept(string targetFils, string exxfile)
        {
            string[] files = Directory.GetFiles(targetFils);
            foreach (string file in files)
            {
                if (file != exxfile)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);

                }
            }
        }
        public void CleanUp()
        {
            if (Directory.Exists(tempPath))
            {
                DeletePath(tempPath);
            }
        }
        public string GetElapsed()// return recorder time
        {
            return string.Format("{0:D2}:{1:D1}:{2:D2}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds); //position 0 and two decimal places

        }
        public void RecordVideo() // start rec
        {
            watch.Start();
            using (Bitmap bitmap = new Bitmap(1526, 818))
            {
                using (Graphics g = Graphics.FromImage(bitmap)) /// creating a screenshot
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                string name = tempPath + "//screenshot-" + fileCount + ".png";
                bitmap.Save(name, ImageFormat.Png);
                inputImageSequance.Add(name);
                fileCount++;
                bitmap.Dispose(); // git rid og map after you are using it 
            }

        }
        public void RecordAudio() // call our native method class and recordaudio 
        {
            NativeMethods.record("open new Type waveaudio Alias recsound", "", 0, 0);
            NativeMethods.record("record recsound", "", 0, 0);
        }
        private void SaveVadeo(int width, int height, int framRate) //video to final output location
        {
            using (VideoFileWriter vfWriter = new VideoFileWriter()) // ffmpeg video recorder
            {
                try
                {

                vfWriter.Open(outputPath + "//" + videoName, width, height, framRate, VideoCodec.WMV1);
                //once the video is open can begin to add each individual screenshot  that take  with 
                //record video method we could begin to just add that to the video 
                foreach (string imageLoc in inputImageSequance)// name of the file 
                {
                    Bitmap imageFrame = System.Drawing.Image.FromFile(imageLoc) as Bitmap;// gonna call image frame 
                    vfWriter.WriteVideoFrame(imageFrame);     //call our VF Write  Video fram image
                    imageFrame.Dispose();
                }
                vfWriter.Close();

                //creating a mp4 video 
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("record error");
                }
            }
        }
        // creating a save audio 
        public void SaveAudio()
        {
            string audioPath = "save recsound" + outputPath + "//" + audioName;
            NativeMethods.record(audioPath, "", 0, 0);
            NativeMethods.record("close recsound", "", 0, 0);
        }

        private void CombainseVideoAudio(string video, string audio)
        {
            string command = $"/c ffmpeg -i \"{video}\" -i \"{audio}\" -shortest {finalName} ";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                FileName = "cmd.exe",
                WorkingDirectory = outputPath,
                Arguments = command
            };
            using (Process excProcces = Process.Start(startInfo))
            {
                excProcces.WaitForExit();
            }
        }
        public void stop()
        {
            watch.Stop();
            int width = bounds.Width;
            int hieght = bounds.Height;
            int framRate = 29;

            SaveAudio();
            SaveVadeo(width, hieght, framRate);
            CombainseVideoAudio(videoName, audioName);

            DeletePath(tempPath);

            //DeleteFillesExcept(outputPath, outputPath + "\\" + finalName);


        }







    }

}