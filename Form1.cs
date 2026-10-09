using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExcavatorGame
{
    public partial class Form1 : Form
    {
        private readonly Timer gameTimer = new Timer();
        private readonly Random rnd = new Random();

        private int excavatorX = 60;
        private int excavatorY = 420;

        private int bucketAngle = 0;   // 铲斗角度
        private bool bucketDown = true;
        private bool bucketDigging = false;

        private int soilX = 350;
        private int soilY = 420;

        private int loadAmount = 0;
        private int missionTarget = 8;
        private int collected = 0;

        private bool isDigging = false;
        private bool hasLoaded = false;

        private readonly List<Rectangle> soilPile = new List<Rectangle>();
        private readonly List<Rectangle> clouds = new List<Rectangle>();

        public Form1()
        {
            this.Width = 1000;
            this.Height = 700;
            this.Text = "挖掘机工程工地";
            this.BackColor = Color.FromArgb(135, 206, 235);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            this.DoubleBuffered = true;

            for (int i = 0; i < 6; i++)
            {
                clouds.Add(new Rectangle(50 + i * 150, 40 + (i % 3) * 25, 60, 28));
            }

            gameTimer.Interval = 30;
            gameTimer.Tick += GameTick;
            gameTimer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    excavatorX -= 10;
                    break;
                case Keys.Right:
                    excavatorX += 10;
                    break;
                case Keys.Up:
                    if (bucketAngle > -65)
                        bucketAngle -= 5;
                    break;
                case Keys.Down:
                    if (bucketAngle < 30)
                        bucketAngle += 5;
                    break;
                case Keys.Space:
                    if (!isDigging && !hasLoaded)
                    {
                        isDigging = true;
                    }
                    break;
                case Keys.Enter:
                    if (hasLoaded)
                    {
                        // 把土倒到堆里
                        int dropX = 700;
                        int dropY = 480;

                        soilPile.Add(new Rectangle(dropX, dropY, 26, 20));
                        collected++;
                        hasLoaded = false;
                        loadAmount = 0;

                        if (collected >= missionTarget)
                        {
                            MessageBox.Show("任务完成！挖掘机小工完成了今天的工程！");
                            collected = 0;
                            missionTarget = 8 + rnd.Next(2, 6);
                        }
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isDigging = false;
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            // 限制挖掘机移动范围
            excavatorX = Math.Max(10, Math.Min(excavatorX, 760));

            // 挖土逻辑
            if (isDigging)
            {
                bucketDown = true;
                if (bucketAngle < 30)
                    bucketAngle += 4;

                if (soilX > 0 && bucketAngle >= 20 && excavatorX > 200 && excavatorX < 520)
                {
                    loadAmount++;
                    if (loadAmount > 9)
                    {
                        hasLoaded = true;
                        isDigging = false;
                    }
                }
            }
            else
            {
                if (bucketAngle > 0)
                    bucketAngle -= 3;
                else if (bucketAngle < 0)
                    bucketAngle += 3;
            }

            // 土堆
            if (collected > 0)
            {
                for (int i = 0; i < soilPile.Count; i++)
                {
                    soilPile[i] = new Rectangle(soilPile[i].X, soilPile[i].Y - 1, soilPile[i].Width, soilPile[i].Height);
                }
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 背景
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, 210, 255)), 0, 0, this.Width, this.Height);
            g.FillRectangle(new SolidBrush(Color.FromArgb(130, 210, 150)), 0, 520, this.Width, 180);

            // 云朵
            foreach (var cloud in clouds)
            {
                g.FillEllipse(Brushes.White, cloud);
                g.FillEllipse(Brushes.White, cloud.X + 18, cloud.Y - 10, 26, 26);
                g.FillEllipse(Brushes.White, cloud.X + 30, cloud.Y - 5, 26, 26);
            }

            // 地面
            g.FillRectangle(new SolidBrush(Color.FromArgb(130, 90, 60)), 0, 500, this.Width, 200);

            // 挖掘机底盘
            g.FillRectangle(Brushes.Gray, excavatorX, 430, 180, 30);
            g.FillRectangle(Brushes.DarkGray, excavatorX + 25, 390, 120, 45);

            // 轮子
            for (int i = 0; i < 4; i++)
            {
                int wx = excavatorX + 25 + i * 40;
                g.FillEllipse(Brushes.Black, wx, 460, 25, 25);
            }

            // 驾驶舱
            g.FillRectangle(Brushes.SkyBlue, excavatorX + 45, 350, 80, 40);

            // 臂架
            Pen armPen = new Pen(Color.DarkSlateGray, 10);
            g.DrawLine(armPen, excavatorX + 95, 390, excavatorX + 130, 320);

            // 铲斗
            RectangleF bucketRect = new RectangleF(excavatorX + 119, 296, 54, 38);
            GraphicsState gs = g.Save();
            g.TranslateTransform(bucketRect.X + bucketRect.Width / 2, bucketRect.Y + bucketRect.Height / 2);
            g.RotateTransform(bucketAngle);
            g.TranslateTransform(-bucketRect.Width / 2, -bucketRect.Height / 2);
            g.FillRectangle(Brushes.Orange, bucketRect);
            g.DrawRectangle(Pens.Brown, Rectangle.Round(bucketRect));
            g.Restore(gs);

            // 斗齿
            for (int i = 0; i < 3; i++)
            {
                int bx = (int)(excavatorX + 125 + i * 14);
                int by = (int)(310 + (bucketAngle < 0 ? 4 : 0));
                g.FillRectangle(Brushes.Brown, bx, by + 26, 8, 10);
            }

            // 挖土位置
            if (hasLoaded)
            {
                g.FillEllipse(Brushes.SaddleBrown, excavatorX + 150, 300, 26, 26);
            }

            // 工程任务区
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 245, 200)), 760, 30, 220, 220);
            g.DrawRectangle(Pens.Brown, 760, 30, 220, 220);

            g.DrawString("🏗 工程任务", new Font("微软雅黑", 16, FontStyle.Bold), Brushes.Brown, 780, 50);
            g.DrawString("━━━━━━━━━━━━", new Font("微软雅黑", 12), Brushes.Brown, 780, 75);
            g.DrawString("目标: " + missionTarget + " 次", new Font("微软雅黑", 13), Brushes.Black, 780, 95);
            g.DrawString("已运土: " + collected, new Font("微软雅黑", 13), new SolidBrush(Color.Green), 780, 125);

            g.DrawString("📋 操作说明：", new Font("微软雅黑", 12, FontStyle.Bold), Brushes.Brown, 780, 160);
            g.DrawString("← → 移动挖掘机", new Font("微软雅黑", 11), Brushes.Black, 780, 185);
            g.DrawString("↑ ↓ 调节铲斗角度", new Font("微软雅黑", 11), Brushes.Black, 780, 210);
            g.DrawString("空格 开始挖土", new Font("微软雅黑", 11), Brushes.Black, 780, 235);
            g.DrawString("回车 倒土到堆里", new Font("微软雅黑", 11), Brushes.Black, 780, 260);

            // 土堆
            foreach (var pile in soilPile)
            {
                g.FillRectangle(Brushes.SaddleBrown, pile);
                g.FillRectangle(Brushes.SandyBrown, pile.X + 4, pile.Y + 8, pile.Width - 8, 7);
            }

            // 车身小提示
            g.DrawString("🎮 小朋友的工程工地", new Font("微软雅黑", 14, FontStyle.Bold), Brushes.White, 20, 20);

            // 如果挖掘机有装满土，则显示土袋
            if (hasLoaded)
            {
                g.FillEllipse(Brushes.SaddleBrown, excavatorX + 145, 330, 28, 28);
            }

            // 让土堆有层次感
            g.FillRectangle(Brushes.DarkOliveGreen, 0, 500, this.Width, 6);
        }
    }

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
    }
}
