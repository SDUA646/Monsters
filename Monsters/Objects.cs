using System;
using System.Diagnostics;

public class Person
{
    public int life = 5;
    //记录人物当前状态，偶数静止，奇数跑动
    public int condition = 0;
    private int x;
    private int y;
    private int speed = 0;
    private int haveKey = 0;
    public int Life
    {
        get
        { return life; }
        set
        { life = value;}
    }
    public int X
    {
        get
        { return x; }
        set
        { x = value;}
    }
    public int Y
    {
        get
        { return y; }
        set
        { y = value; }
    }
    public int Speed
    {
        get
        { return speed; }
        set
        { speed = value; }
    }
    public int HaveKey
    {
        get { return haveKey; }
        set { haveKey = value; }
    }


    public void Move(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void getLife()
    {

    }

    public void getView()
    {
       
        
    }
}

