using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        static  int row = 40;
        //生成的列数
       static int column = 40;
        //游戏过程中剩余怪的数量
        private int remainingmonsters;
        //人物是否正在移动，0为不移动，1为移动
        bool personmoving = false;

        //生成个按钮数组
        private Buttons[,] button = new Buttons[row, column];
        private Pictures pictures = new Pictures();
        private Person person = new Person();

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.form1 = this;
            label1.Text = (person.Life).ToString();
            pictureBox1.Image = Image.FromFile(pictures.hearts);
            //label1.UseMnemonic = false;
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
        //生成地图
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
                    button[0, 0].BackgroundImage = Image.FromFile(pictures.person);
                    getView(0, 0);
                    //for (int i = 0; i < row; i++)
                    //{
                    //    for (int j = 0; j < column; j++)
                    //        if (button[i, j].Type == 1)
                    //        {
                    //          button[i, j].BackgroundImage = Image.FromFile(pictures.person);
                    //        }
                    //        else if (button[i, j].Type == 2)
                    //        {
                    //            button[i, j].BackgroundImage = Image.FromFile(pictures.hearts);
                    //        }
                    //        else if (button[i, j].Type == 3)
                    //        {
                    //            button[i, j].BackgroundImage = Image.FromFile(pictures.monsters);
                    //        }
                    //        else if (button[i, j].Type == 4)
                    //        {
                    //            button[i, j].BackgroundImage = Image.FromFile(pictures.ground);
                    //        }
                    //}
                    if (! personmoving)
                    {
                        if(b.MovePerson(b.X, b.Y, person))
                        {
                            button[person.X ,person.Y].BackgroundImage = Image.FromFile(pictures.ground);
                            b.Tag = 1;
                            if (button[person.X, person.Y].Type == 2)
                            {
                                person.Life++;
                                button[person.X, person.Y].Type = 4;
                                showPersonLife();
                            }
                            if (button[person.X, person.Y].Type == 3) person.Life--;
                           
                            getView(b.X,b.Y);
                            b.BackgroundImage = Image.FromFile(pictures.person);
                            personmoving = true;
                        }
                    }
                    
                    break;
            }
        }
        
        //开视野
        private void getView(int x,int y)
        {
            getImage(x - 1, y - 1);
            getImage(x - 1, y);
            getImage(x - 1, y + 1);
            getImage(x , y - 1);
            getImage(x , y + 1);
            getImage(x + 1, y - 1);
            getImage(x + 1, y);
            getImage(x + 1, y + 1);
          

        }

        private void getImage(int x,int y)
        {          
            if(x >= 0 && y >= 0 && x <= row && y <= column)
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
            Random rand = new Random();
            //布心
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
            //布雷
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

            for(int i = 0;i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    if(button[i,j].Type == 0)
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
