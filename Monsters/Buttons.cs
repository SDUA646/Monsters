using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class Buttons : Button
{
    private int x;
    private int y;
    //记录该个控件下的object，0为空，1为人
    //2为心，3为怪
    private int type;
    public Buttons()
    {
        Tag = 0;     ///0表示未翻开，1表示翻开
        Size = new System.Drawing.Size(30, 30);
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
}