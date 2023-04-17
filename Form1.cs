using System;
using System.Drawing;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(400, 400);

            lHour = new Label();
            lHour.Text = "HOURS";
            lHour.TextAlign = ContentAlignment.TopCenter;
            lHour.Size = new Size(300, 20);
            lHour.Location = new Point(50, 10);
            this.Controls.Add(lHour);

            trBarHour = new TrackBar();
            trBarHour.Maximum = 23;
            trBarHour.Minimum = 0;
            trBarHour.Size = new Size(300, 50);
            trBarHour.Location = new Point(50, 30);
            trBarHour.TickStyle = TickStyle.Both;
            trBarHour.TickFrequency = 10;
            trBarHour.ValueChanged += TrBarHour_ValueChanged;
            this.Controls.Add(trBarHour);

            lMinute = new Label();
            lMinute.Text = "MINUTES";
            lMinute.TextAlign = ContentAlignment.TopCenter;
            lMinute.Size = new Size(300, 20);
            lMinute.Location = new Point(50, 90);
            this.Controls.Add(lMinute);

            trBarMinute = new TrackBar();
            trBarMinute.Maximum = 59;
            trBarMinute.Minimum = 0;
            trBarMinute.Size = new Size(300, 50);
            trBarMinute.Location = new Point(50, 110);
            trBarMinute.TickStyle = TickStyle.Both;
            trBarMinute.TickFrequency = 10;
            trBarMinute.ValueChanged += TrBarMinute_ValueChanged;
            this.Controls.Add(trBarMinute);

            lSecond = new Label();
            lSecond.Text = "SECONDS";
            lSecond.TextAlign = ContentAlignment.TopCenter;
            lSecond.Size = new Size(300, 20);
            lSecond.Location = new Point(50, 170);
            this.Controls.Add(lSecond);

            trBarSecond = new TrackBar();
            trBarSecond.Maximum = 60;
            trBarSecond.Minimum = 0;
            trBarSecond.Size = new Size(300, 50);
            trBarSecond.Location = new Point(50, 190);
            trBarSecond.TickStyle = TickStyle.Both;
            trBarSecond.TickFrequency = 10;
            trBarSecond.ValueChanged += TrBarSecond_ValueChanged;
            this.Controls.Add(trBarSecond);

            lTimer = new Label();
            lTimer.Text = $"TIMER: 00:00:00";
            lTimer.TextAlign = ContentAlignment.TopCenter;
            lTimer.Size = new Size(300, 30);
            lTimer.Font = new Font("Times new roman", 20);
            lTimer.Location = new Point(50, 240);
            this.Controls.Add(lTimer);

            prBarTimer = new ProgressBar();
            prBarTimer.Minimum = 0;
            prBarTimer.Size = new Size(300, 20);
            prBarTimer.Location = new Point(50, 290);
            this.Controls.Add(prBarTimer);


            btnStart = new Button();
            btnStart.Text = "START TIMER";
            btnStart.ForeColor = Color.Black;
            btnStart.Size = new Size(120, 30);
            btnStart.Location = new Point(140, 350);
            this.Controls.Add(btnStart);
            btnStart.Click += BtnStart_Click;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private void TrBarSecond_ValueChanged(object sender, EventArgs e)
        {
            ShowTimer(trBarHour.Value, trBarMinute.Value, trBarSecond.Value);
        }

        private void TrBarMinute_ValueChanged(object sender, EventArgs e)
        {
            ShowTimer(trBarHour.Value, trBarMinute.Value, trBarSecond.Value);
        }

        private void TrBarHour_ValueChanged(object sender, EventArgs e)
        {
            ShowTimer(trBarHour.Value, trBarMinute.Value, trBarSecond.Value);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!startGo)
            {
                if (btnStart.ForeColor == Color.Black && (trBarHour.Value + trBarMinute.Value + trBarSecond.Value) > 0)
                {
                    iter = (trBarHour.Value * 3600 + trBarMinute.Value * 60 + trBarSecond.Value) * 10;
                    prBarTimer.Maximum = iter / 10;
                    trBarHour.Enabled = false;
                    trBarMinute.Enabled = false;
                    trBarSecond.Enabled = false;
                    btnStart.Text = "PAUSE TIMER";
                    btnStart.ForeColor = Color.Red;
                    startGo = true;
                    timer.Start();
                }
                else if (btnStart.ForeColor == Color.Red)
                {
                    btnStart.Text = "PAUSE TIMER";
                    startGo = true;
                    timer.Start();
                }

            }
            else
            {
                if (btnStart.ForeColor == Color.Red)
                {
                    timer.Stop();
                    btnStart.Text = "START TIMER";
                    startGo = false;
                }
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (startGo)
            {
                ShowTimer((iter / 10) / 3600, (iter / 10) / 60, (iter / 10) % 60);
                prBarTimer.Value = iter / 10;
                if (iter == 0)
                {
                    startGo = false;
                    iter = 40;
                    btnStart.Enabled = false;
                }
            }
            else
            {
                if (iter % 2 == 0)
                    this.BackColor = Color.Red;
                else
                    this.BackColor = Color.Blue;
            }
            if (iter == 0)
            {
                this.BackColor = Color.White;
                timer.Stop();
                btnStart.Enabled = true;
                btnStart.Text = "START TIMER";
                btnStart.ForeColor = Color.Black;
                trBarHour.Enabled = true;
                trBarMinute.Enabled = true;
                trBarSecond.Enabled = true;
            }
            iter--;
        }

        private void ShowTimer(int hr, int min, int sec)
        {
            if (hr > 9)
            {
                if (min > 9)
                {
                    if (sec > 9)
                        lTimer.Text = $"TIMER: {hr}:{min}:{sec}";
                    else
                        lTimer.Text = $"TIMER: {hr}:{min}:0{sec}";
                }
                else
                {
                    if (sec > 9)
                        lTimer.Text = $"TIMER: {hr}:0{min}:{sec}";
                    else
                        lTimer.Text = $"TIMER: {hr}:0{min}:0{sec}";
                }
            }
            else
            {
                if (min > 9)
                {
                    if (sec > 9)
                        lTimer.Text = $"TIMER: 0{hr}:{min}:{sec}";
                    else
                        lTimer.Text = $"TIMER: 0{hr}:{min}:0{sec}";
                }
                else
                {
                    if (sec > 9)
                        lTimer.Text = $"TIMER: 0{hr}:0{min}:{sec}";
                    else
                        lTimer.Text = $"TIMER: 0{hr}:0{min}:0{sec}";
                }
            }
        }


        System.Windows.Forms.Timer timer;
        int iter;
        bool startGo = false;

        Label lHour;
        Label lMinute;
        Label lSecond;
        Label lTimer;

        TrackBar trBarHour;
        TrackBar trBarMinute;
        TrackBar trBarSecond;

        ProgressBar prBarTimer;

        Button btnStart;
    }
}
