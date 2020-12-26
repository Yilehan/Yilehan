using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 背单词
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int idx = 0;//索引值

        public int displayMode = 0;//显示模式（正序、倒序、随机）

        public string[] english;//英文部分

        public string[] chinese;//释义部分

        public string filePath; //词汇文件路径
        
        public SortedDictionary<string, string> dict = new SortedDictionary<string, string>();//存储字典
     
        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            //默认显示
            tsCbSelect.SelectedIndex = 0;
            tsCbSpeed.SelectedIndex = 2;
            tsCbDisplay.SelectedIndex = 0;

            //加载词汇文件
            filePath = @".\words\College_Grade4.txt";
            ReadFile(filePath);

            //设置显示方式/刷新速度   
            this.TopMost = true;
            displayMode = 1;
            timer1.Interval = 3000;
            timer1.Enabled = true;
        }

        //读取词汇文件
        public void ReadFile(string path)
        {
            StreamReader sw = new StreamReader(path, Encoding.Default);

            string content = sw.ReadToEnd();//读取全部数据
            string[] lines = content.Split('\n');//按行拆分
            string[] words = new string[2];
            english = new string[lines.Length];
            chinese = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                int index = lines[i].IndexOf(' ');//将每一行按空格拆分成英文部分和释义部分
                if (index < 0) continue;
                
                if (index > 0 && index < lines[i].Length)
                {
                    words[0] = lines[i].Substring(0, index).Trim();
                    words[1] = lines[i].Substring(index).Trim();
                }
                if (!dict.ContainsKey(words[0]))
                {
                    dict.Add(words[0], words[1]); 
                }
                english[i] = words[0];
                chinese[i] = words[1];
            }

        }

        //自动隐藏窗体
        void AutoSideHideOrShow()
        {
            int sideThickness = 4;//边缘的厚度，窗体停靠在边缘隐藏后留出来的可见部分的厚度  

            //如果窗体最小化或最大化了则什么也不做  
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                return;
            }

            //如果鼠标在窗体内  
            if (Cursor.Position.X >= this.Left && Cursor.Position.X < this.Right && Cursor.Position.Y >= this.Top && Cursor.Position.Y < this.Bottom)
            {
                //如果窗体离屏幕边缘很近，则自动停靠在该边缘  
                if (this.Top <= sideThickness)
                {
                    this.Top = 0;
                }
                if (this.Left <= sideThickness)
                {
                    this.Left = 0;
                }
                if (this.Left >= Screen.PrimaryScreen.WorkingArea.Width - this.Width - sideThickness)
                {
                    this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                }
            }
            //当鼠标离开窗体以后  
            else
            {
                //隐藏到屏幕左边缘  
                if (this.Left == 0)
                {
                    this.Left = sideThickness - this.Width;
                }
                //隐藏到屏幕右边缘  
                else if (this.Left == Screen.PrimaryScreen.WorkingArea.Width - this.Width)
                {
                    this.Left = Screen.PrimaryScreen.WorkingArea.Width - sideThickness;
                }
                //隐藏到屏幕上边缘  
                else if (this.Top == 0 && this.Left > 0 && this.Left < Screen.PrimaryScreen.WorkingArea.Width - this.Width)
                {
                    this.Top = sideThickness - this.Height;
                }
            }
        }

        //定时显示单词
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (displayMode == 1)//正序显示
            {
                
                label1.Text = english[idx];
                label2.Text = chinese[idx];

                idx++;
                if (idx >= english.Length) idx = 0;

            }
            else if (displayMode == 2)//倒序显示
            {
                
                label1.Text = english[idx];
                label2.Text = chinese[idx];

                idx--;
                if (idx <= 0) idx = english.Length - 1;
            }
            else if (displayMode == 3)//乱序显示
            {
                Random rnd = new Random();
                idx = rnd.Next(english.Length);
                label1.Text = english[idx];
                label2.Text = chinese[idx];
            }

            


        }

        //选择词汇文件
        private void tsCbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tsCbSelect.Text)
            {
                case "四级":
                    filePath = @".\words\College_Grade4.txt";

                    break;
                case "六级":
                    filePath = @".\words\College_Grade6.txt";
                    break;
                case "考研":
                    filePath = @".\words\brace_brutalbrace.txt";
                    break;
            }

            idx = 0;
            ReadFile(filePath);

        }

        //设置刷新速度
        private void tsCbSpeed_TextChanged(object sender, EventArgs e)
        {
            int time = 0;
            bool flg = int.TryParse(tsCbSpeed.Text.Trim(),out time);
            if (flg && time >0)
            {
                timer1.Interval = time;
                //timer1.Enabled = true;
            }
            else
            {
                MessageBox.Show("输入格式错误：请输入一个数字！");
            }
             
        }
        //设置显示方式
        private void tsCbDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tsCbDisplay.Text)
            {
                case "正序显示":
                    displayMode = 1;
                    idx = 0;
                    break;
                case "倒序显示":
                    displayMode = 2;
                    idx = english.Length-1;
                    break;
                case "随机显示":
                    displayMode = 3;
                    break;
            }
        }

        //自动停靠到桌面边缘
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 200;
            AutoSideHideOrShow();
        }
    }
}
