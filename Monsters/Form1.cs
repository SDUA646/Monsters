using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Media;

namespace Monsters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        //计时器
        private Timer timer = new Timer();
        //所用时间
        private int totaltime = 0;
        //定义怪数
        private int totalmonsters = 50;
        //定义心数
        private int totalhearts = 50;
        //游戏是否结束
        private bool over = false;
        //生成的行数
        private int row = 20;
        //生成的列数
        private int column = 20;
        //游戏过程中剩余怪的数量
        private int remainingmonsters;
        //人物是否正在移动，0为不移动，1为移动
        bool personmoving = false;

        //生成个按钮数组
        private Buttons[,] button = new Buttons[20, 20];
        private Pictures pictures = new Pictures();
        private Person person = new Person();

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.form1 = this;
            groupBox1.Location = new Point(26, 26);
            groupBox1.Text = "";
            groupBox1.Size = new System.Drawing.Size(908, 908);
            groupBox1.FlatStyle = FlatStyle.Standard;
            this.Location = new Point(20, 20);
            timer.Enabled = true;
            groundField();
            setObjects();
            this.StartPosition = FormStartPosition.Manual;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            totaltime++;
            personmoving = false;
        }

        private void groundField()
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    button[j, i] = new Buttons();
                    button[j, i].Location = new Point(3 + i * 30, 6 + j * 30);
                    button[j, i].X = j;
                    button[j, i].Y = i;
                    button[j, i].Type = 0;
                    button[j, i].Font = new System.Drawing.Font("宋体", button[j, i].Font.Size, button[j, i].Font.Style);
                    button[j, i].FlatStyle = FlatStyle.Flat;

                    groupBox1.Controls.Add(button[j, i]);
                    button[j, i].MouseUp += new MouseEventHandler(bt_MouseUp);
                }
            }
        }

        private void bt_MouseUp(object sender, MouseEventArgs e)
        {
            int x, y;
            //获取被点击的Button按钮
            Buttons b = (Buttons)sender;
            x = b.X;//x代表button数组的第一个索引
            y = b.Y;//y表示Button数组的第二个索引
            //判断按下的鼠标键是哪个
            switch (e.Button)
            {
                //按下鼠标左键
                case MouseButtons.Left:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < column; j++)
                            if (button[i, j].Type == 1)
                            {
                                button[i, j].BackgroundImage = Image.FromFile(pictures.person);
                            }
                            else if (button[i, j].Type == 2)
                            {
                                button[i, j].BackgroundImage = Image.FromFile(pictures.hearts);
                            }
                            else if (button[i, j].Type == 3)
                            {
                                button[i, j].BackgroundImage = Image.FromFile(pictures.monsters);
                            }
                    }
                    if(! personmoving)
                    {
                        if(b.MovePerson(b.X, b.Y, person))
                        {
                            b.BackgroundImage = Image.FromFile(pictures.person);
                            personmoving = true;
                        }
                    }
                    break;
            }
        }

        private void setObjects()
        {
            button[0, 0].Type = 1;
            Random rand = new Random();
            for (int i = 0; i < totalhearts; i++)
            {

                int position_x = rand.Next(row);
                int position_y = rand.Next(column);
                if (button[position_x, position_y].Type == 0)
                {
                    button[position_x, position_y].Type = 2;
                }
                else
                    i = i - 1;
            }
            for (int i = 0; i < totalmonsters; i++)
            {

                int position_x = rand.Next(row);
                int position_y = rand.Next(column);
                if (button[position_x, position_y].Type == 0)
                {
                    button[position_x, position_y].Type = 3;
                }
                else
                    i = i - 1;
            }
        }
    }
}
