using System;
public class FindingPath
{
    private const int INFINITY = 60000;
    private static int Row;
    private static int Column;
    private int[,] final;
    private int[,] nextpath;
    private int[,] shortestpath;

    public void initFindingPath(int R, int C)
    {
        Row = R;
        Column = C;
        //记录该点是否已经固定
        final = new int[Row, Column];
        //记录父节点
        nextpath = new int[Row, Column];
        //记录整体权值
        shortestpath = new int[Row, Column];
    }
    public void GetNextPosition(ref int[] monstersX, ref int[] monstersY, int monstersnum, int personX, int personY, int[,] terrain)
    {
        //初始化
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                final[i, j] = 0;
                nextpath[i, j] = INFINITY;
                shortestpath[i, j] = INFINITY;
            }
        }
        shortestpath[personX, personY] = 0;
        for(int a = 0; a < Row * Column; a++)
        {
            int min = INFINITY;
            int minX = 0;
            int minY = 0;
            //寻找最小值
            for(int i = 0; i < Row; i++)
            {
                for(int j = 0; j < Column; j++)
                {
                    if(shortestpath[i, j] < min && final[i, j] == 0)
                    {
                        min = shortestpath[i, j];
                        minX = i;
                        minY = j;
                    }
                }
            }
            final[minX, minY] = 1;
            if (min == 60000)
               break;
            if(minX > 0 && final[minX - 1, minY] == 0)
            {
                if (shortestpath[minX - 1, minY] > shortestpath[minX, minY] + terrain[minX - 1, minY] || nextpath[minX - 1, minY] == INFINITY)
                {
                    nextpath[minX - 1, minY] = minY * Column + minX;
                    shortestpath[minX - 1, minY] = shortestpath[minX, minY] + terrain[minX - 1, minY];
                }
            }
            if(minX < Row - 1 && final[minX + 1, minY] == 0)
            {
                if (shortestpath[minX + 1, minY] > shortestpath[minX, minY] + terrain[minX + 1, minY] || nextpath[minX + 1, minY] == INFINITY)
                {
                    nextpath[minX + 1, minY] = minY * Column + minX;
                    shortestpath[minX + 1, minY] = shortestpath[minX, minY] + terrain[minX + 1, minY];
                }
            }
            if(minY > 0 && final[minX, minY - 1] == 0)
            {
                if (shortestpath[minX, minY - 1] > shortestpath[minX, minY] + terrain[minX, minY - 1] || nextpath[minX, minY - 1] == INFINITY)
                {
                    nextpath[minX, minY - 1] = minY * Column + minX;
                    shortestpath[minX, minY - 1] = shortestpath[minX, minY] + terrain[minX, minY - 1];
                }
            }
            if (minY < Column - 1 && final[minX, minY + 1] == 0)
            {
                if (shortestpath[minX, minY + 1] > shortestpath[minX, minY] + terrain[minX, minY + 1] || nextpath[minX, minY + 1] == INFINITY)
                {
                    nextpath[minX, minY + 1] = minY * Column + minX;
                    shortestpath[minX, minY + 1] = shortestpath[minX, minY] + terrain[minX, minY + 1];
                }
            }
        }
        int monsterX;
        int monsterY;
        for(int i = 0; i < monstersnum + 1; i++)
        {
            monsterX = monstersX[i];
            monsterY = monstersY[i];
            if (nextpath[monsterX, monsterY] != INFINITY)
            {
                monstersX[i] = nextpath[monsterX, monsterY] % Column;
                monstersY[i] = nextpath[monsterX, monsterY] / Column;
            }
            
        }
    }
}
