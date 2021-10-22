﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Media;
using System.Timers;

namespace Monsters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        private const int INFINITY = 60000;
        //计时器1,人物
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        //计时器2,怪物
        private System.Windows.Forms.Timer timerM = new System.Windows.Forms.Timer();
        //计时器
        //private System.Windows.Forms.Timer timer1= new System.Windows.Forms.Timer();

        //所用时间
        private int totaltime = 0;
        //定义怪数
        private int totalmonsters = 50;
        //定义心数
        private int totalhearts = 200;
        //游戏是否结束
        private bool over = false;
        //生成的行数
        static int row = 40;
        //生成的列数
        static int column = 70;
        //游戏过程中剩余怪的数量
        private int remainingmonsters;
        //人物是否正在移动，0为不移动，1为移动
        bool personmoving = false;
        //执行人物移动的函数的定时器
        private static System.Timers.Timer aTimer;


        //生成个按钮数组
        private Buttons[,] button = new Buttons[row, column];
        private Pictures pictures = new Pictures();
        private Person person = new Person();
        private FindingPath findingpath = new FindingPath();



        private void Form1_Load(object sender, EventArgs e)
        {
            Program.form1 = this;
            label1.Text = (person.Life).ToString();

           
            groupBox1.Location = new Point(26, 40);
            groupBox1.Text = "";
            groupBox1.Size = new System.Drawing.Size(1600, 908);
            groupBox1.FlatStyle = FlatStyle.Standard;
            this.Location = new Point(20, 20);
            timer.Enabled = true;
            groundField();
            setObjects();
            this.StartPosition = FormStartPosition.Manual;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
            timerM.Enabled = true;
            timerM.Tick += new EventHandler(timerM_Tick);
            timerM.Interval = 1000;
            findingpath.initFindingPath(row, column);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            totaltime++;
            personmoving = false;
        }
        private void timerM_Tick(object sender, EventArgs e)
        {
            int[] monstersX = new int[50];
            int[] monstersY = new int[50];
            int[] localmonstersX = new int[50];
            int[] localmonstersY = new int[50];
            int visiblemonsters = -1;
            int[,] terrain = new int[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (button[i, j].Type == 3 && (int)button[i, j].Tag == 1)
                    {
                        visiblemonsters++;
                        monstersX[visiblemonsters] = i;
                        monstersY[visiblemonsters] = j;
                        localmonstersX[visiblemonsters] = i;
                        localmonstersY[visiblemonsters] = j;
                    }
                    if ((button[i, j].Type == 4 && (int)button[i, j].Tag == 1) || (button[i, j].Type == 3 && (int)button[i, j].Tag == 1))
                    {
                        terrain[i, j] = 1;
                    }
                    else
                    {
                        terrain[i, j] = INFINITY;
                    }
                }
            }
            if (visiblemonsters > -1)
            {
                findingpath.GetNextPosition(ref monstersX, ref monstersY, visiblemonsters, person.X, person.Y, terrain);
                for (int i = 0; i < visiblemonsters + 1; i++)
                {
                    if (monstersX[i] == person.X && monstersY[i] == person.Y)
                    {
                        button[localmonstersX[i], localmonstersY[i]].BackgroundImage = Image.FromFile(pictures.ground);
                        button[localmonstersX[i], localmonstersY[i]].Type = 4;
                        person.life -= 1;
                        showPersonLife();
                    }
                    else if (button[monstersX[i], monstersY[i]].Type == 4)
                    {
                        button[monstersX[i], monstersY[i]].BackgroundImage = Image.FromFile(pictures.monsters);
                        button[monstersX[i], monstersY[i]].Type = 3;
                        button[localmonstersX[i], localmonstersY[i]].BackgroundImage = Image.FromFile(pictures.ground);
                        button[localmonstersX[i], localmonstersY[i]].Type = 4;
                    };
                }
            }
        }
        //生成地图
        private void groundField()
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    button[j, i] = new Buttons();
                    button[j, i].Location = new Point(3 + i * 20, 6 + j * 20);
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
        private void button1_Click(object sender, EventArgs e)
        {
            button[0, 0].BackgroundImage = Image.FromFile(pictures.person1);
            button[1, 0].BackgroundImage = Image.FromFile(pictures.person2);
            button[0, 1].BackgroundImage = Image.FromFile(pictures.person3);
            button[1, 1].BackgroundImage = Image.FromFile(pictures.person4);
            button[0, 0].Type = 4;
            button[0, 1].Type = 4;
            button[1, 0].Type = 4;
            button[1, 1].Type = 4;
            getView(0, 0);
        }

        Buttons b;


        private void bt_MouseUp(object sender, MouseEventArgs e)
        {
            //int x, y;
            //获取被点击的Button按钮
            Buttons b1 = (Buttons)sender;
            b = b1;
            //x = b.X;//x代表button数组的第一个索引
            //y = b.Y;//y表示Button数组的第二个索引

            //设置计时器，每隔一秒调用一次执行人物移动的函数
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;    //配置文件中配置的秒数
            aTimer.Enabled = true;
      
            if (button[person.X, person.Y].Type == 5)
            {
                MessageBox.Show("你真牛逼！", "游戏通关");
                over = true;
            }


        }
        public void OnTimedEvent(object source, ElapsedEventArgs es)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            int x, y;
            x = b.X;//x代表button数组的第一个索引
            y = b.Y;//y表示Button数组的第二个索引
            if (!personmoving)
            {
                int personX = person.X;
                int personY = person.Y;
                if (b.MovePerson(b.X, b.Y, person))
                {
                    button[personX, personY].Type = 4;
                    button[personX, personY].BackgroundImage = Image.FromFile(pictures.ground);

                    //吃心，生命值++
                    if (button[person.X+1, person.Y+1].Type == 2)
                    {
                        person.Life++;
                        button[person.X+1, person.Y+1].Type = 4;
                               
                        showPersonLife();
                    }
                    if (button[person.X , person.Y + 1].Type == 2)
                    {
                        person.Life++;
                        button[person.X , person.Y + 1].Type = 4;
                                
                        showPersonLife();
                    }
                            
                    if (button[person.X +1, person.Y ].Type == 2)
                    {
                        person.Life++;
                        button[person.X + 1, person.Y].Type = 4;
                               
                        showPersonLife();
                    }
                    if (button[person.X , person.Y].Type == 2)
                        {
                        person.Life++;
                        button[person.X, person.Y].Type = 4;
                        showPersonLife();
                        }

                    //开视野，探索地图
                    getView(person.X, person.Y);

                    button[person.X, person.Y].BackgroundImage = Image.FromFile(pictures.person1);
                    button[person.X + 1, person.Y].BackgroundImage = Image.FromFile(pictures.person4);
                    button[person.X, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person2);
                    button[person.X + 1, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person3);


                    //通关判断
                
                       
                    if (button[person.X+1, person.Y+1].Type == 5)
                    {
                        MessageBox.Show("你真牛逼！", "游戏通关");
                        over = true;
                    }
                    personmoving = true;
                             
                }
            }
        }

        //开视野
        private void getView(int x, int y)
        {
            //8格视野
            getImage(x - 1, y - 1);
            getImage(x - 1, y);
            getImage(x - 1, y + 1);
            getImage(x, y - 1);
            getImage(x, y + 1);
            getImage(x + 1, y - 1);
            getImage(x + 1, y);
            getImage(x + 1, y + 1);

            //+12格视野
            getImage(x - 1, y - 2);
            getImage(x, y - 2);
            getImage(x, y - 3);
            getImage(x + 1, y - 2);

            getImage(x - 1, y + 2);
            getImage(x, y + 2);
            getImage(x, y + 3);
            getImage(x + 1, y + 2);

            getImage(x - 2, y - 1);
            getImage(x - 2, y);
            getImage(x - 3, y);
            getImage(x - 2, y + 1);

            getImage(x + 2, y - 1);
            getImage(x + 2, y);
            getImage(x + 3, y);
            getImage(x + 2, y + 1);

            //+4视野
            getImage(x - 2, y - 2);
            getImage(x - 2, y + 2);
            getImage(x + 2, y - 2);
            getImage(x + 2, y + 2);

        }

        private void getImage(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < row && y < column)
            {
                button[x, y].Tag = 1;
                button[x, y].showImage();

            }

        }



        private void showPersonLife()
        {
            label1.Text = (person.life).ToString();

        }
        private void setObjects()
        {
            button[0, 0].Type = 1;
            button[row - 1, column - 1].Type = 5;
            Random rand = new Random();
            //布心
            for (int i = 0; i < totalhearts; i++)
            {

                int position_x = rand.Next(row-1);
                int position_y = rand.Next(column-1);
               
                if (button[position_x, position_y].Type == 0)
                {
                    button[position_x, position_y].Type = 2;
                   


                }
                else
                    i = i - 1;
            }
            //布雷
            for (int i = 0; i < totalmonsters; i++)
            {

                int position_x = rand.Next(row);
                int position_y = rand.Next(column);
                if (button[position_x, position_y].Type == 0 && position_x + position_y != 0)
                {
                    button[position_x, position_y].Type = 3;
                }
                else
                    i = i - 1;
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (button[i, j].Type == 0)
                    {
                        button[i, j].Type = 4;
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = (person.Life).ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
