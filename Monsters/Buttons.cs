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
    private int moveonterrain = 0;
    //记录该个控件下的object，0为空，1为人
    //2为心，3为怪，4为地，5为终点，6为加速器,7为钥匙，8为绿巨人
    //10为障碍物
    //14为石头，12为石头心，13为石头怪,11为石头钥匙，18为石头绿巨人
    private int type;
    FindingPath findingpath = new FindingPath();
    public Buttons()
    {
        Tag = 0;     ///0表示未翻开，1表示翻开
        Size = new System.Drawing.Size(22, 22);
    }
    public int Moveonterrain
    {
        get
        { return moveonterrain; }
        set
        { moveonterrain = value; }
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

    public bool MovePerson(int mouseX, int mouseY, Person person, Buttons[,] button,int row,int column)//传入新的坐标
    {
      
        bool success = false;
        
        
        int x = mouseX - person.X, y = mouseY - person.Y;
        if (x < row - 2 && y < column - 2)
        {
            if ((x > 0 && y >= 0 && x >= y) || (x > 0 && y < 0 && x >= -y))
            {

                if (button[person.X + 2, person.Y].Type != 10 && button[person.X + 2, person.Y + 1].Type != 10)
                {
                    if (findingpath.canMove(button[person.X + 1, person.Y]))
                    {
                        person.Move(person.X + 1, person.Y);
                        success = true;
                        person.condition++;
                    }
                }
            }
            else if ((x >= 0 && y > 0 && x < y) || (x < 0 && y >= 0 && y >= -x))
            {
                if (button[person.X, person.Y + 2].Type != 10 && button[person.X + 1, person.Y + 2].Type != 10)
                {
                    if (findingpath.canMove(button[person.X, person.Y + 1]))
                    {
                        person.Move(person.X, person.Y + 1);
                        success = true;
                        person.condition++;
                    }

                }
            }
            else if ((x <= 0 && y > 0 && -x >= y) || (x < 0 && y <= 0 && x <= y))
            {
                if (button[person.X - 1, person.Y].Type != 10 && button[person.X - 1, person.Y + 1].Type != 10)
                {
                    if (findingpath.canMove(button[person.X - 1, person.Y]))
                    {
                        person.Move(person.X - 1, person.Y);
                        success = true;
                        person.condition++;
                    }
                }
            }
            else if ((x > 0 && y <= 0 && x < -y) || (x <= 0 && y <= 0 && x > y))
            {
                if (button[person.X, person.Y - 1].Type != 10 && button[person.X + 1, person.Y - 1].Type != 10)
                {
                    if (findingpath.canMove(button[person.X, person.Y - 1]))
                    {
                        person.Move(person.X, person.Y - 1);
                        success = true;
                        person.condition++;
                    }
                }
            }
        }
        return success;

    }

    public void showImage()
    {
        if(Tag.Equals(1))
        {
            switch(type)
            {
                case 2:
                    BackgroundImage = Image.FromFile(pictures.hearts1);
                    break;
                case 12:
                    BackgroundImage = Image.FromFile(pictures.hearts01);
                    break;
                case 13:
                    BackgroundImage = Image.FromFile(pictures.monsters01);
                    break;
                case 3:
                    BackgroundImage = Image.FromFile(pictures.monsters1);
                    break;
                case 4:
                    BackgroundImage = Image.FromFile(pictures.ground);
                    break;
                case 20:
                    BackgroundImage = Image.FromFile(pictures.victory1);
                    break;
                case 21:
                    BackgroundImage = Image.FromFile(pictures.victory2);
                    break;
                case 22:
                    BackgroundImage = Image.FromFile(pictures.victory3);
                    break;
                case 23:
                    BackgroundImage = Image.FromFile(pictures.victory4);
                    break;
                case 6:
                    BackgroundImage = Image.FromFile(pictures.booster);
                    break;
                case 7:
                    BackgroundImage = Image.FromFile(pictures.key);
                    break;
                case 8:
                    BackgroundImage = Image.FromFile(pictures.monsters2);
                    break;
                case 18:
                    BackgroundImage = Image.FromFile(pictures.monsters02);
                    break;
                case 10:
                    BackgroundImage = Image.FromFile(pictures.block);
                    break;
                case 11:
                    BackgroundImage = Image.FromFile(pictures.key_1);
                    break;
                case 14:
                    BackgroundImage = Image.FromFile(pictures.rock);
                    break;
            }
        }
    }
}