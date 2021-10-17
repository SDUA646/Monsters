using System;

public class Person
{
    public int life = 1;
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

public class Monster
{
    private int x;
    private int y;
    //怪的状态，0为未翻开，1为翻开，2为死亡
    private int status = 0;
    public int Status
    {
        get
        { return status; }
        set
        { status = value; }
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
    public void lossLife()
    {

    }
    public void Move(int row, int column)
    {

    }
}

public class Heart
{
    private int flag = 1;
    private int x;
    private int y;
    //心的状态，0为未翻开，1为翻开，2为取得
    private int status = 0;
    public int Status
    {
        get
        { return status; }
        set
        { status = value; }
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
    public void getHeart()
    {

    }
}
