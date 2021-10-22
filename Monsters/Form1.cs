using System;
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
        private Map map = new Map();



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
                    if ((button[i, j].Type == 3 && (int)button[i, j].Tag == 1) || (button[i, j].Type == 13 && (int)button[i, j].Tag == 1))
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
                    else if((button[i, j].Type == 14 && (int)button[i, j].Tag == 1) || (button[i, j].Type == 13 && (int)button[i, j].Tag == 1))
                    {
                        terrain[i, j] = 2;
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
                        findingpath.transType(ref button[localmonstersX[i], localmonstersY[i]], FindingPath.Transtype.monstertoground, pictures);
                        person.life -= 1;
                        showPersonLife();
                    }
                    else if ((button[monstersX[i], monstersY[i]].Type == 4) || (button[monstersX[i], monstersY[i]].Type == 14))
                    {
                        findingpath.transType(ref button[monstersX[i], monstersY[i]], FindingPath.Transtype.groundtomonster, pictures);
                        findingpath.transType(ref button[localmonstersX[i], localmonstersY[i]], FindingPath.Transtype.monstertoground, pictures);
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
            button[1, 0].BackgroundImage = Image.FromFile(pictures.person4);
            button[0, 1].BackgroundImage = Image.FromFile(pictures.person2);
            button[1, 1].BackgroundImage = Image.FromFile(pictures.person3);
            button[0, 0].Type = 4;
            button[0, 1].Type = 4;
            button[1, 0].Type = 4;
            button[1, 1].Type = 4;
            getView(0, 0);
        }

        Buttons b;


        private void bt_MouseUp(object sender, MouseEventArgs e)
        {
           
            //获取被点击的Button按钮
            Buttons b1 = (Buttons)sender;
            b = b1;
            

            //设置计时器，每隔一秒调用一次执行人物移动的函数
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;    //配置文件中配置的秒数
            aTimer.Enabled = true;      
        
        }
        public void OnTimedEvent(object source, ElapsedEventArgs es)
        {
            Control.CheckForIllegalCrossThreadCalls = false;


              if (!personmoving)
              {
                    //游戏通关，停止运行
                    if(over == true)
                    {
                        MessageBox.Show("你真牛逼！", "游戏通关");

                    }
                   
                    if (b.MovePerson(b.X, b.Y, person))
                    {
                    if (person.X == row - 1)
                        person.X -= 1;
                    if (person.Y == column - 1)
                        person.Y -= 1;
                    //吃心，生命值++

                    checkHerts(person.X, person.Y);



                    //开视野，探索地图
                    getView(person.X, person.Y);

                    //展示新的人物形象
                    button[person.X, person.Y].BackgroundImage = Image.FromFile(pictures.person1);
                    button[person.X + 1, person.Y].BackgroundImage = Image.FromFile(pictures.person4);
                    button[person.X, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person2);
                    button[person.X + 1, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person3);


                    //通关判断


                    if (button[person.X, person.Y].Type == 5)
                    {                    
                        over = true;
                    }
                    personmoving = true;
                }
            }
        }

        //吃心函数，检测按钮的type，如果是心，生命值++
        private void checkHerts(int x,int y)
        {
            if (button[x + 1, y + 1].Type == 2)
            {
                person.Life++;
                button[x + 1, y].Type = 4;

                showPersonLife();
            }
            if (button[x, y].Type == 2)
            {
                person.Life++;
                button[x, y + 1].Type = 4;

                showPersonLife();
            }

            if (button[x + 1, y].Type == 2)
            {
                person.Life++;
                button[x + 1, y].Type = 4;

                showPersonLife();
            }
            if (button[x, y].Type == 2)
            {
                person.Life++;
                button[person.X, person.Y].Type = 4;
                showPersonLife();
            }
            if (button[x + 1, person.Y + 1].Type == 12)
                    {
                        person.Life++;
                        button[x + 1, y + 1].Type = 14;

                        showPersonLife();
                    }
                    if (button[x, y + 1].Type == 12)
                    {
                        person.Life++;
                        button[x, y + 1].Type = 14;

                        showPersonLife();
                    }

                    if (button[x + 1, y].Type == 12)
                    {
                        person.Life++;
                        button[x + 1, y].Type = 14;

                        showPersonLife();
                    }
                    if (button[x, y].Type == 12)
                    {
                        person.Life++;
                        button[x, y].Type = 14;
                        showPersonLife();
                    }
        }

        //开视野，10/22，新调视野
        private void getView(int x, int y)
        {
            //8格视野
            getImage(x - 1, y - 1);
            getImage(x - 1, y);
            getImage(x - 1, y + 1);
            getImage(x - 1, y + 2);

            getImage(x - 2, y);
            getImage(x -2, y + 1);

            getImage(x, y - 2);
            getImage(x, y - 1);
            getImage(x, y + 2);
            getImage(x, y + 3);

          
            getImage(x + 1, y - 2);
            getImage(x + 1, y - 1);
            getImage(x + 1, y + 2);
            getImage(x + 1, y + 3);

            getImage(x + 2, y - 1);
            getImage(x + 2, y );
            getImage(x + 2, y + 1);
            getImage(x + 2, y + 2);

            getImage(x + 3, y);
            getImage(x + 3, y + 1);


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

            button[row - 2, column - 2].Type = 5;
            map.getMap(ref button, row, column);
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
                else if(button[position_x, position_y].Type == 14)
                {
                    button[position_x, position_y].Type = 12;
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
                else if (button[position_x, position_y].Type == 14)
                {
                    button[position_x, position_y].Type = 13;
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
