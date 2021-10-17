using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public class Buttons : Button
{
    private int x;
    private int y;
    private Pictures pictures = new Pictures();
    //记录该个控件下的object，0为空，1为人
    //2为心，3为怪
    private int type;
    public Buttons()
    {
        Tag = 0;     ///0表示未翻开，1表示翻开
        Size = new System.Drawing.Size(20, 20);
    }
    public int X
    {
        get
        { return x; }
        set
        { x = value; }
    }
    public int Y
    {
        get
        { return y; }
        set
        { y = value; }
    }
    public int Type
    {
        get
        { return type; }
        set
        { type = value; }
    }

    public bool MovePerson(int mouseX, int mouseY, Person person)//传入新的坐标
    {
        //如果传入的新坐标在原坐标周围，则执行移动
        //bool condition1 = (x + y == person.X + person.Y + 1) && (x - person.X == 1 || x - person.X == 0);
        //bool condition2 = (x + y == person.X + person.Y - 1) && (x - person.X == -1 || x - person.X == 0);

        bool success = false;
        //if (condition1 || condition2)
        //{
        //  //  type = 4;
        //     person.Move(x, y);//人物移动到新地址
        // //   BackgroundImage = Image.FromFile(pictures.ground); ;
        //    success = true;
        //}
        //return success;
        int x = mouseX - person.X, y = mouseY - person.Y;
        if((x > 0 && y >= 0 && x >= y)||(x > 0 && y < 0 && x >= - y))
        {
            person.Move(person.X + 1, person.Y);
            success = true;
        }
        else if(( x >= 0 && y > 0 && x < y)||(x < 0 && y >= 0 && y >= -x))
        {
            person.Move(person.X, person.Y + 1);
            success = true;

        }
        else if(( x <= 0 && y > 0 && -x >=  y)|| (x < 0 && y < 0 && x <= y))
        {
            person.Move(person.X - 1, person.Y);
            success = true;
        }
        else if ((x > 0 && y <= 0 && x < -y)|| (x < 0 && y <= 0 && x > y))
        {
            person.Move(person.X, person.Y -1 );
            success = true;
        }
        return success;

    }

    public void MoveMonsters(ref int x, ref int y, Person person)
    {

    }

    public void showImage()
    {
        if(Tag.Equals(1))
        {
            switch(type)
            {
                case 2: 
                    BackgroundImage = Image.FromFile(pictures.hearts);
                    break;
                case 3:
                    BackgroundImage = Image.FromFile(pictures.monsters);
                    break;
                case 4:
                    BackgroundImage = Image.FromFile(pictures.ground);
                    break;
                case 5:
                    BackgroundImage = Image.FromFile(pictures.victory);
                    break;
            }
        }
    }
}