using System;

public class Map
{
	Random rand = new Random();
	int total = 1;
	public Map()
	{
	}
	public void getMap(ref Buttons[,] buttons, int row, int column)
    {
		int type = rand.Next(total - 1);
		switch(type)
        {
			case 0:
				for(int i = row / 4 - 2; i < row * 3 / 5 + 1; i++)
                {
					for (int j = column / 3 - 2; j < column * 2 / 3 + 1; j++)
                    {
						buttons[i, j].Type = 10;
					}
				}
				buttons[row * 3 / 5, column / 3 - 2].Type = 0;
				buttons[row * 3 / 5 - 1, column / 3 - 2].Type = 0;
				for (int i = row / 4 - 1; i < row * 3 / 5; i++)
				{
					for (int j = column / 3 - 1; j < column * 2 / 3; j++)
					{
						buttons[i, j].Type = 14;
					}
				}
				for (int i = row * 3 / 7 - 1; i < row * 3 / 5; i++)
				{
					for (int j = column / 3 - 1; j < column / 2 + 2; j++)
					{
						buttons[i, j].Type = 0;
					}
				}
				break;
			default:
				break;
		}
			
	}
}
