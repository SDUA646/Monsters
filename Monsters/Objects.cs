using System;

public class Person
{
    public int life = 5;
    private int x;
    private int y;
    public int Life
    {
        get
        { return life; }
        set
        { life = value; }
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

