using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Media;
using System.Timers;
using NAudio.Wave;
using System.IO;

namespace Graphics_4._1
{
    public partial class MainForm : Form
    {

        //Variables-----------------------------------------------------------------------------------
        private System.Timers.Timer timer;

        private Bitmap drawingSurface = new Bitmap(1000, 1000);
        private readonly Bitmap crossHairMap = new Bitmap(20, 20);

        private readonly SoundPlayer Gun_Shot = new SoundPlayer(Properties.Resources.Gun_Shot);
        private readonly SoundPlayer Button_Click = new SoundPlayer(Properties.Resources.Button_Click_2);
        private readonly SoundPlayer Gate_Open = new SoundPlayer(Properties.Resources.Gate_Open);
        private readonly WaveOutEvent Music_Output = new WaveOutEvent();

        private int width = 200;
        private int height = 400;
        private int offset = 0;
        private int x = 0;
        private int y = 0;

        private bool GameNotStart = true;
        private bool CanEnterGate = false;
        private bool EnteredGate = false;
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Form Builder, Contains all of the settings for the form;
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Cursor.Hide();
            this.KeyPreview = true;
            this.TopMost = true;
            this.MinimumSize = new Size(800, 600);
            this.DoubleBuffered = true; //Reduce Lag;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.Manual;
            drawingSurface = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Screen.Size = new Size(drawingSurface.Width, drawingSurface.Height);
            Screen.Location = new Point(0, 0);
            Crosshair.Size = new Size(crossHairMap.Width, crossHairMap.Height);
            Crosshair.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            Crosshair.Enabled = false;
            Crosshair.WaitOnLoad = true;
            x = drawingSurface.Width / 2 + offset - width / 2;
            y = drawingSurface.Height / 2;

        }

        //--------------------------------------------------------------------------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            Main_Menu();
        }

        //Level Env Renderers------------------------------------------------------------------------------
        /// <summary>
        /// Renders the Menu of the game;
        /// </summary>
        private void Main_Menu()
        {
            Crosshair.Enabled = false;
            Crosshair.Visible = false;
            Graphics GFX = Graphics.FromImage(drawingSurface);
            GFX.Clear(Color.Black);
            string title = "Thy Hell";
            string desc = "press [SPACE] to begin...";
            GFX.DrawString(title, new Font("castellar", 40), new SolidBrush(Color.HotPink), drawingSurface.Width / 2 - (title.Length * 15), 200);
            GFX.DrawString(desc, new Font("castellar", 20), new SolidBrush(Color.HotPink), drawingSurface.Width / 2 - (desc.Length * 6), 500);
            Screen.Image = drawingSurface;
            GFX.Dispose();
        }

        /// <summary>
        /// Renders the Gate To Hell;
        /// </summary>
        private void Hell_Gate()
        {
            Graphics GFX = Graphics.FromImage(drawingSurface);
            GFX.Clear(Color.Black);
            x = drawingSurface.Width / 2 + offset - width / 2;
            if (x < drawingSurface.Width / 2 - width / 2)
            {
                x = drawingSurface.Width / 2 + offset - width;
                CanEnterGate = false;
            }
            else if (x > drawingSurface.Width / 2 - width / 2)
            {
                x = drawingSurface.Width / 2 + offset;
                CanEnterGate = false;
            }
            y = drawingSurface.Height / 2;
            GFX.DrawRectangle(new Pen(Color.White), x, y - height / 5, width, height);
            GFX.DrawString("Enter Thy Hell", new Font("castellar", width / 13),
                new SolidBrush(Color.HotPink), x, y - height / 5);
            GFX.DrawLine(new Pen(Color.White), x, y, x + width, y);
            Screen.Image = drawingSurface;
            GFX.Dispose();
        }
        /// <summary>
        /// Renders Level 1 of the game;
        /// </summary>
        private void Level_1()
        {
            Graphics GFX = Graphics.FromImage(drawingSurface);
            GFX.Clear(Color.Black);
            x = drawingSurface.Width / 2 + offset - width / 2;
            if (x < drawingSurface.Width / 2 - width / 2)
            {
                x = drawingSurface.Width / 2 + offset - width;
                CanEnterGate = false;
            }
            else if (x > drawingSurface.Width / 2 - width / 2)
            {
                x = drawingSurface.Width / 2 + offset;
                CanEnterGate = false;
            }
            int y = drawingSurface.Height / 2;
            GFX.FillRectangle(new SolidBrush(Color.Aquamarine), x, y, width, height);
            Screen.Image = drawingSurface;
            GFX.Dispose();
        }
        //--------------------------------------------------------------------------------------------

        //Input Handlers------------------------------------------------------------------------------
        /// <summary>
        /// Makes Crosshair follow the mouse;
        /// </summary>
        private void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            Crosshair.Location = new Point(e.X, e.Y);
        }
        /// <summary>
        /// In Charge of Object Detection and Shooting sounds on MouseClick;
        /// </summary>
        private void Crosshair_MouseClick(object sender, MouseEventArgs e)
        {
            Gun_Shot.Play();
            if (EnteredGate && Crosshair.Location.X < x + width + offset && Crosshair.Location.X > x - width / 2 - offset &&
                Crosshair.Location.Y > y && Crosshair.Location.Y < y + height)
            {
                Graphics GFX = Graphics.FromImage(drawingSurface);
                GFX.Clear(Color.Black);
                Screen.Image = drawingSurface;
                GFX.Dispose();
            }
        }

        /// <summary>
        /// Handles the Keyboard input from the player;
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space && GameNotStart)
            {
                Button_Click.Play();
                GameNotStart = false;
                Crosshair.Enabled = true;
                Crosshair.Visible = true;
                Graphics GFX = Graphics.FromImage(drawingSurface);
                GFX.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Graphics CrossGFX = Graphics.FromImage(crossHairMap);
                CrossGFX.FillRectangle(new SolidBrush(Color.HotPink), 0, 0, crossHairMap.Width, crossHairMap.Height);
                Crosshair.Image = crossHairMap;
                Hell_Gate();
            }

            if (keyData == Keys.Space && !GameNotStart && CanEnterGate && !EnteredGate)
            {
                EnteredGate = true;
                CanEnterGate = false;
                Gate_Open.Play();
                timer = new System.Timers.Timer();
                timer.Interval = 2000;
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = false;
                timer.Enabled = true;
                width = 200;
                height = 400;
                Level_1();
            }
            if (!GameNotStart && !EnteredGate)
            {
                if (keyData == Keys.W)
                {
                    if (height + 10 < drawingSurface.Height)
                    {
                        width += 15;
                        height += 30;
                    }
                    else
                    {
                        CanEnterGate = true;
                    }
                    Hell_Gate();
                }

                if (keyData == Keys.S)
                {
                    if (width - 10 > 10 && height - 10 > 20)
                    {
                        width -= 15;
                        height -= 30;
                    }
                    Hell_Gate();
                }

                if (keyData == Keys.D)
                {
                    offset -= 20;
                    Hell_Gate();
                }

                if (keyData == Keys.A)
                {
                    offset += 20;
                    Hell_Gate();
                }
            }

            if (keyData == Keys.Escape)
            {
                this.Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        //--------------------------------------------------------------------------------------------

        //Game Adapters-------------------------------------------------------------------------------
        /// <summary>
        /// Incase you want a Sizable window;
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            drawingSurface = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Crosshair.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            Screen.Size = new Size(drawingSurface.Width, drawingSurface.Height);
            Hell_Gate();
        }

        /// <summary>
        /// Lag Reduce;
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        //--------------------------------------------------------------------------------------------

        //Audio & Music handlers----------------------------------------------------------------------
        /// <summary>
        /// Plays Music Once a time limit is Over;
        /// </summary>
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            UnmanagedMemoryStream sound1 = Properties.Resources.Cycle_Sort_1;

            byte[] b = ReadToEnd(sound1);

            WaveStream wav = new RawSourceWaveStream(new MemoryStream(b), new WaveFormat(44100, 16, 2));
            Music_Output.Init(wav);
            Music_Output.Play();
            Music_Output.PlaybackStopped += Output_PlaybackStopped;
        }
        /// <summary>
        /// Loops the music;
        /// </summary>
        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            UnmanagedMemoryStream sound1 = Properties.Resources.Cycle_Sort_1;

            byte[] b = ReadToEnd(sound1);

            WaveStream wav = new RawSourceWaveStream(new MemoryStream(b), new WaveFormat(44100, 16, 2));
            Music_Output.Init(wav);
            Music_Output.Play();
        }
        /// <summary>
        /// Streams the Music;
        /// </summary>
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        //--------------------------------------------------------------------------------------------

    }
}
