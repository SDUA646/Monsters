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
        
        //定义怪数
        private int totalmonsters = 30;
        //定义心数
        private int totalhearts = 80;
        //定义加速器的数量
        private int boosters = 20;
        //定义钥匙的数量
        private int keyNum = 1;
        //定义游戏所用时间     
        private int millisecond = 0;
        private int second = 0;
        private int minute = 0;
        //定义分数
        private double score=0;
        //游戏是否结束
        private bool over = false;
        //生成的行数
        static int row = 40;
        //生成的列数
        static int column = 70;
        //人物是否正在移动，0为不移动，1为移动
        bool personmoving = false;
        //执行人物移动的函数的定时器
        private static System.Timers.Timer aTimer;
       
        //
        int monstertime = -1;
        //
        int hulktime = -1;
        //
        int persontime = 0;
        //
        int speedTime = 0;
        //
        int totalhulks = 20;

        //生成个按钮数组
        private Buttons[,] button = new Buttons[row, column];
        private Buttons b;
        private Pictures pictures = new Pictures();
        private Person person = new Person();
        private FindingPath findingpath = new FindingPath();
        private Map map = new Map();



        private void Form1_Load(object sender, EventArgs e)
        {
            
            label1.Text = (person.Life).ToString();
            label2.Text = minute.ToString() + " 分" + second.ToString() + " 秒";
            groupBox1.Location = new Point(26, 40);
            groupBox1.Text = "";
            groupBox1.Size = new System.Drawing.Size(1600, 908);
            groupBox1.FlatStyle = FlatStyle.Standard;
            this.Location = new Point(20, 20);
            groundField();
            setObjects();
            this.StartPosition = FormStartPosition.Manual;
            timer.Enabled = true;
            timer.Stop();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 10;
            
            

            //设置计时器，每隔一秒调用一次执行人物移动的函数
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 100;    //配置文件中配置的秒数
            aTimer.Enabled = true;

            b = button[0, 0];
            findingpath.initFindingPath(row, column);
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            //展示新的人物形象
            button[person.X, person.Y].BackgroundImage = Image.FromFile(pictures.person1);
            button[person.X + 1, person.Y].BackgroundImage = Image.FromFile(pictures.person4);
            button[person.X, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person2);
            button[person.X + 1, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person3);
           //输出时钟计时，且每隔一段时间开一个随即视野
            millisecond++;
            if (millisecond % 65 == 0)
            {
                second++;
                if(second%10==0)
                {
                    RandView();
                }
                if(second>=60)
                {
                    second = 0;
                    minute++;

                }
            }
            label2.Text = minute.ToString() + " 分" + second.ToString() + " 秒";
              
        }
        //生成地图
        private void groundField()
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    button[j, i] = new Buttons();
                
                    button[j, i].Location = new Point(3 + i * 21, 6 + j * 21);
                    button[j, i].X = j;
                    button[j, i].Y = i;
                    button[j, i].Type = 0;
                    button[j, i].Font = new System.Drawing.Font("宋体", button[j, i].Font.Size, button[j, i].Font.Style);
                    button[j, i].FlatStyle = FlatStyle.Flat;
                    button[j, i].Enabled = false;

                    groupBox1.Controls.Add(button[j, i]);
                    button[j, i].MouseUp += new MouseEventHandler(bt_MouseUp);
                }
            }
        }
        //开始按钮
        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    button[j, i].Enabled = true;

                }
            }
            button[0, 0].BackgroundImage = Image.FromFile(pictures.person1);
            button[1, 0].BackgroundImage = Image.FromFile(pictures.person4);
            button[0, 1].BackgroundImage = Image.FromFile(pictures.person2);
            button[1, 1].BackgroundImage = Image.FromFile(pictures.person3);
            button[0, 0].Type = 4;
            button[0, 1].Type = 4;
            button[1, 0].Type = 4;
            button[1, 1].Type = 4;
            getView(0, 0);
            getRandView(row - 2, column - 2);
            button1.Enabled = false;
        }

        


        private void bt_MouseUp(object sender, MouseEventArgs e)
        {
           
            //获取被点击的Button按钮
            Buttons b1 = (Buttons)sender;
            b = b1;
            
        }
        public void OnTimedEvent(object source, ElapsedEventArgs es)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
           
            if(person.Speed==0)
            {
                if (persontime == 9)
                {
                    personmoving = false;
                    persontime = 0;
                }
                else
                {
                    persontime++;
                }
            }
            if(person.Speed > 0)
            {
                if (persontime == 4)
                {
                    personmoving = false;
                    persontime = 0;
                    speedTime++;
                    if(speedTime%10==0)
                    {
                        person.Speed--;
                    }
                }
                else
                {
                    persontime++;
                }
            }

            monstersMoving();
            if (!personmoving)
              {
               
                
                    if (b.MovePerson(b.X, b.Y, person,button))
                    {
                    if (person.X == row - 1)
                        person.X -= 1;
                    if (person.Y == column - 1)
                        person.Y -= 1;
                    //吃心，生命值++

                    checkHearts(person.X, person.Y);

                    //得到加速器，speed++
                    checkSpeed(person.X, person.Y);

                    //得到钥匙  haveKey++
                    checkKey(person.X, person.Y);

                    //开视野，探索地图
                    getView(person.X, person.Y);

                    //展示新的人物形象
                    button[person.X, person.Y].BackgroundImage = Image.FromFile(pictures.person1);
                    button[person.X + 1, person.Y].BackgroundImage = Image.FromFile(pictures.person4);
                    button[person.X, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person2);
                    button[person.X + 1, person.Y + 1].BackgroundImage = Image.FromFile(pictures.person3);


                    //通关判断


                    if (button[person.X, person.Y].Type == 5 && person.HaveKey>0)
                    {                    
                        over = true;
                        Checkwin();
                    }
                    
                }
                personmoving = true;
            }
            
        }

        //吃心函数，检测按钮的type，如果是心，生命值++
        private void checkHearts(int x,int y)
        {
            if (button[x + 1, y + 1].Type == 2)
            {
                person.Life++;
                button[x + 1, y+1].Type = 4;

                showPersonLife();
            }
             if (button[x + 1, y].Type == 2)
            {
                person.Life++;
                button[x + 1, y].Type = 4;

                showPersonLife();
            }

            if (button[x , y+1].Type == 2)
            {
                person.Life++;
                button[x , y+1].Type = 4;

                showPersonLife();
            }
            if (button[x, y].Type == 2)
            {
                person.Life++;
                button[x, y].Type = 4;
                showPersonLife();
            }
            if (button[x + 1,y + 1].Type == 12)
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
        //得到加速器的函数，检测按钮的type，如果是加速器，speed++
        public void checkSpeed(int x,int y)
        {
            if(button[x,y].Type==6)
            {
                person.Speed++;
                button[x, y].Type = 4;
            }
            if (button[x+1, y].Type == 6)
            {
                person.Speed++;
                button[x+1, y].Type = 4;
            }
            if (button[x, y+1].Type == 6)
            {
                person.Speed++;
                button[x, y+1].Type = 4;
            }
            if (button[x+1, y+1].Type == 6)
            {
                person.Speed++;
                button[x + 1, y + 1].Type = 4;
            }
        }
        //得到钥匙的函数，检测按钮的type，如果是钥匙，haveKey++
        public void checkKey(int x,int y)
        {
            if (button[x, y].Type == 7)
            {
                person.HaveKey++;
                button[x, y].Type = 4;
            }
            if (button[x + 1, y].Type == 7)
            {
                person.HaveKey++;
                button[x + 1, y].Type = 4;
            }
            if (button[x, y + 1].Type == 7)
            {
                person.HaveKey++;
                button[x, y + 1].Type = 4;
            }
            if (button[x + 1, y + 1].Type == 7)
            {
                person.HaveKey++;
                button[x + 1, y + 1].Type = 4;
            }
            if (button[x, y].Type == 11)
            {
                person.HaveKey++;
                button[x, y].Type = 14;
            }
            if (button[x + 1, y].Type == 11)
            {
                person.HaveKey++;
                button[x + 1, y].Type = 14;
            }
            if (button[x, y + 1].Type == 11)
            {
                person.HaveKey++;
                button[x, y + 1].Type = 14;
            }
            if (button[x + 1, y + 1].Type == 11)
            {
                person.HaveKey++;
                button[x + 1, y + 1].Type = 14;
            }
        }
        //随机视野
        private void RandView()
        {
            Random rand1 = new Random();
            int position_x = rand1.Next(row - 1);
            int position_y = rand1.Next(column - 1);
            getRandView(position_x, position_y);
        }
        private void getRandView(int x,int y)
        {
            getImage(x,y);
            getImage(x + 1,y);
            getImage(x,y + 1);
            getImage(x + 1, y + 1);
            
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

        //开视野，10/22，新调视野
        private void getView(int x, int y)
        {
            //8格视野
            getImage(x - 1, y - 2);
            getImage(x - 1, y - 1);
            getImage(x - 1, y);
            getImage(x - 1, y + 1);
            getImage(x - 1, y + 2);
            getImage(x - 1, y + 3);

            getImage(x - 2, y - 1);
            getImage(x - 2, y);
            getImage(x -2, y + 1);
            getImage(x - 2, y + 2);


            getImage(x - 3, y);
            getImage(x - 3, y + 1);

            getImage(x, y - 3);
            getImage(x, y - 2);
            getImage(x, y - 1);
            getImage(x, y + 2);
            getImage(x, y + 3);
            getImage(x, y + 4);

            getImage(x + 1, y - 3);
            getImage(x + 1, y - 2);
            getImage(x + 1, y - 1);
            getImage(x + 1, y + 2);
            getImage(x + 1, y + 3);
            getImage(x + 1, y + 4);

            getImage(x + 2, y - 2);
            getImage(x + 2, y - 1);
            getImage(x + 2, y );
            getImage(x + 2, y + 1);
            getImage(x + 2, y + 2);
            getImage(x + 2, y + 3);

            getImage(x + 3, y -1);
            getImage(x + 3, y);
            getImage(x + 3, y + 1);
            getImage(x + 3, y + 2);

            getImage(x + 4, y);
            getImage(x + 4, y + 1);


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
            fail();
        }

        private void monstersMoving()
        {
            oneMonsterMoving(3, 13, 0, 1);
            oneMonsterMoving(8, 18, 1, 3);

            monstertime++;
            hulktime++;
        }
        private void oneMonsterMoving(int type0, int type1, int monstertype, int hurt)
        {
            if ((monstertime == 6 & monstertype == 0) || (hulktime == 17 && monstertype == 1))
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
                        if ((button[i, j].Type == type0 && (int)button[i, j].Tag == 1) || (button[i, j].Type == type1 && (int)button[i, j].Tag == 1))
                        {
                            visiblemonsters++;
                            monstersX[visiblemonsters] = i;
                            monstersY[visiblemonsters] = j;
                            localmonstersX[visiblemonsters] = i;
                            localmonstersY[visiblemonsters] = j;
                        }
                        if ((button[i, j].Type == 4 && (int)button[i, j].Tag == 1) || (button[i, j].Type == type0 && (int)button[i, j].Tag == 1))
                        {
                            terrain[i, j] = 1;
                        }
                        else if ((button[i, j].Type == 14 && (int)button[i, j].Tag == 1) || (button[i, j].Type == type1 && (int)button[i, j].Tag == 1))
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
                        bool condition1 = monstersX[i] >= Math.Max(person.X, 0) && monstersX[i] <= Math.Min(person.X + 1, row);
                        bool condition2 = monstersY[i] >= Math.Max(person.Y, 0) && monstersY[i] <= Math.Min(person.Y + 1, column);
                        bool condition3 = localmonstersX[i] > Math.Max(person.X - 1, 0) && localmonstersX[i] < Math.Min(person.X + 3, row);
                        bool condition4 = localmonstersY[i] > Math.Max(person.Y - 1, 0) && localmonstersY[i] < Math.Min(person.Y + 3, column);

                        if (findingpath.canMove(button[monstersX[i], monstersY[i]]))
                        {
                            if ((condition1 && condition2) || (condition3 && condition4))
                            {
                                findingpath.transType(ref button[localmonstersX[i], localmonstersY[i]], FindingPath.Transtype.monstertoground, pictures, monstertype);
                                person.life -= hurt;
                                showPersonLife();
                            }
                            else if ((button[monstersX[i], monstersY[i]].Type == 4) || (button[monstersX[i], monstersY[i]].Type == 14))
                            {
                                findingpath.transType(ref button[monstersX[i], monstersY[i]], FindingPath.Transtype.groundtomonster, pictures, monstertype);
                                findingpath.transType(ref button[localmonstersX[i], localmonstersY[i]], FindingPath.Transtype.monstertoground, pictures, monstertype);
                            }
                        }
                    }

                }
                if (monstertime == 6 && monstertype == 0)
                    monstertime = -1;
                else if(hulktime == 17 && monstertype == 1)
                    hulktime = -1;

            }
        }
        private void setObjects()
        {

            button[row - 2, column - 2].Type = 5;
            map.getMap(ref button);
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
            //布钥匙
            for(int i=0;i<keyNum;i++)
            {
                int position_x = rand.Next(row - 1);
                int position_y = rand.Next(column - 1);

                if (button[position_x, position_y].Type == 0)
                {
                    button[position_x, position_y].Type = 7;
                }
                else if (button[position_x, position_y].Type == 14)
                {
                    button[position_x, position_y].Type = 11;
                }
                else
                    i = i - 1;
            }
            //布加速器
            for (int i = 0; i < boosters; i++)
            {

                int position_x = rand.Next(row - 1);
                int position_y = rand.Next(column - 1);

                if (button[position_x, position_y].Type == 0)
                {
                    button[position_x, position_y].Type = 6;
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
            for (int i = 0; i < totalhulks; i++)
            {

                int position_x = rand.Next(row);
                int position_y = rand.Next(column);
                if (button[position_x, position_y].Type == 0 && position_x + position_y != 0)
                {
                    button[position_x, position_y].Type = 8;
                }
                else if (button[position_x, position_y].Type == 14)
                {
                    button[position_x, position_y].Type = 18;
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

        private void Checkwin()
        {
            if (over == true)
            {
                aTimer.Stop();
                timer.Stop();
                timerM.Stop();

                double degree = Exploration(out int score);
                score =score + (int)row * column / 2 + person.life * 10;
                MessageBox.Show("真是幸运的一天。你的得分是" + score  + "。你的探索度是" + degree + "%。");
               
               
                this.Dispose();
                this.Close();
                 
            }
        }

      // private void label1_TextChanged(object sender, EventArgs e)
       private void fail()
        {
            if(person.Life <= 0)
            {
                
                aTimer.Stop();
                timer.Stop();
                timerM.Stop();
                //输出分数
                double degree = Exploration(out int score);
                
                
                MessageBox.Show("你被怪物夺取了所有的心脏！！！");
                MessageBox.Show("你探索了" + degree * 100 + "%的地图。" + "你的分数是" + score + "。" );

                this.Dispose();
               
                this.Close();
            }
        }
        //返回地图探索度的函数
        private double Exploration(out int score)
        {
            int sumTag = 0;
            double sumButton = row * column;
            double degree = 0;
            for (int i = 0; i < row - 1; i++)
                for (int j = 0; j < column; j++)
                    if ((int)button[i, j].Tag==1)
                        sumTag++;
            degree = sumTag / sumButton;
            degree = Math.Round(degree, 2);
            score = sumTag;
            return degree;
        }
        private void esc_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            timer.Stop();
            timerM.Stop();
            this.Dispose();
            this.Close();
        }
    }
}
