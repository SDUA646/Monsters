using System;

public class Map
{
	Random rand = new Random();
	int total = 3;
	public Map()
	{
	}
	public void getMap(ref Buttons[,] buttons)
	{
		int type = rand.Next(total - 1);
		switch (type)
		{
			case 0:
				//石头
				for (int i = 6; i < 19; i++)
				{
					for (int j = 10; j < 24; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = 6; i < 26; i++)
				{
					for (int j = 24; j < 37; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = 19; i < 33; i++)
				{
					for (int j = 61; j < 70; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				//横线
				for (int i = 6, j = 0; j < 70; j++)
				{
					if (j >= 24 && j <= 48) buttons[i, j].Type = 10;
				}
				for (int i = 13, j = 0; j < 70; j++)
				{
					if (j >= 48 && j <= 69) buttons[i, j].Type = 10;
				}
				for (int i = 19, j = 0; j < 70; j++)
				{
					if (j >= 0 && j <= 24) buttons[i, j].Type = 10;
					if (j >= 36 && j <= 61) buttons[i, j].Type = 10;
				}
				for (int i = 26, j = 0; j < 70; j++)
				{
					if (j >= 48 && j <= 61) buttons[i, j].Type = 10;
				}
				for (int i = 33, j = 0; j < 70; j++)
				{
					if (j >= 10 && j <= 36) buttons[i, j].Type = 10;
				}
				//竖线
				for (int j = 10, i = 0; i < 40; i++)
				{
					if (i >= 0 && i <= 13) buttons[i, j].Type = 10;
					if (i >= 26 && i <= 33) buttons[i, j].Type = 10;
				}
				for (int j = 24, i = 0; i < 40; i++)
				{
					if (i >= 6 && i <= 26) buttons[i, j].Type = 10;
				}
				for (int j = 36, i = 0; i < 40; i++)
				{
					if (i >= 6 && i <= 13) buttons[i, j].Type = 10;
					if (i >= 19 && i <= 33) buttons[i, j].Type = 10;
				}
				for (int j = 48, i = 0; i < 40; i++)
				{
					if (i >= 13 && i <= 19) buttons[i, j].Type = 10;
					if (i >= 26 && i <= 39) buttons[i, j].Type = 10;
				}
				break;
			case 1:
				//石头
				for (int i = 6; i < 13; i++)
				{
					for (int j = 10; j < 24; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = 6; i < 19; i++)
				{
					for (int j = 24; j < 36; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = 19; i < 26; i++)
				{
					for (int j = 36; j < 48; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				//横线
				for (int i = 6, j = 0; j < 70; j++)
				{
					if (j >= 10 && j <= 48) buttons[i, j].Type = 10;
				}
				for (int i = 13, j = 0; j < 70; j++)
				{
					if (j >= 36 && j <= 61) buttons[i, j].Type = 10;
				}
				for (int i = 19, j = 0; j < 70; j++)
				{
					if (j >= 24 && j <= 36) buttons[i, j].Type = 10;
					if (j >= 48 && j <= 69) buttons[i, j].Type = 10;
				}
				for (int i = 26, j = 0; j < 70; j++)
				{
					if (j >= 0 && j <= 24) buttons[i, j].Type = 10;
				}
				for (int i = 33, j = 0; j < 70; j++)
				{
					if (j >= 36 && j <= 61) buttons[i, j].Type = 10;
				}
				//竖线
				for (int j = 10, i = 0; i < 40; i++)
				{
					if (i >= 13 && i <= 26) buttons[i, j].Type = 10;
					if (i >= 33 && i <= 39) buttons[i, j].Type = 10;
				}
				for (int j = 24, i = 0; i < 40; i++)
				{
					if (i >= 6 && i <= 19) buttons[i, j].Type = 10;
					if (i >= 26 && i <= 33) buttons[i, j].Type = 10;
				}
				for (int j = 36, i = 0; i < 40; i++)
				{

					if (i >= 26 && i <= 39) buttons[i, j].Type = 10;
				}
				for (int j = 48, i = 0; i < 40; i++)
				{

					if (i >= 19 && i <= 26) buttons[i, j].Type = 10;
				}
				for (int j = 48, i = 0; i < 40; i++)
				{
					if (i >= 0 && i <= 13) buttons[i, j].Type = 10;
					if (i >= 26 && i <= 33) buttons[i, j].Type = 10;
				}
				break;
			case 2:
				//石头
				for (int i = 6; i < 26; i++)
				{
					for (int j = 24; j < 48; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = 19; i < 34; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				//横线
				for (int i = 33, j = 0; j < 70; j++)
				{
					if (j >= 10 && j <= 61) buttons[i, j].Type = 10;
				}
				for (int i = 0, j = 0; j < 70; j++)
				{
					if (j >= 10 && j <= 61) buttons[i, j].Type = 10;
				}
				//竖线
				for (int j = 10, i = 0; i < 40; i++)
				{
					if (i >= 0 && i <= 13) buttons[i, j].Type = 10;
					if (i >= 19 && i <= 33) buttons[i, j].Type = 10;
				}
				for (int j = 61, i = 0; i < 40; i++)
				{
					if (i >= 0 && i <= 33) buttons[i, j].Type = 10;
				}
				break;
			default:
				break;
		}

	}
}