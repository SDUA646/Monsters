using System;
public class FindingPath
{
    private const int INFINITY = 65535;
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
                terrain[i, j] = INFINITY;
                nextpath[i, j] = INFINITY;
                shortestpath[i, j] = INFINITY;
            }
        }
        final[personX, personY] = 1;
        shortestpath[personX, personY] = 0;
        for(int a = 0; a < Row * Column; a++)
        {
            int min = INFINITY;
            int minX = INFINITY;
            int minY = INFINITY;
            //寻找最小值
            for(int i = 0; i < Row; i++)
            {
                for(int j = 0; j < Column; j++)
                {
                    if(shortestpath[i, j] < min)
                    {
                        min = shortestpath[i, j];
                        minX = i;
                        minY = j;
                    }
                }
            }
            if (min == INFINITY)
                break;
            if(minX > 0 && final[minX - 1, minY] == 0)
            {
                if (shortestpath[minX - 1, minY] > shortestpath[minX, minY] + terrain[minX - 1, minY])
                {
                    nextpath[minX - 1, minY] = minY * Column + minX;
                    shortestpath[minX - 1, minY] = shortestpath[minX, minY] + terrain[minX - 1, minY];
                }
            }else if(minX < Row - 1 && final[minX + 1, minY] == 0)
            {
                if (shortestpath[minX + 1, minY] > shortestpath[minX, minY] + terrain[minX + 1, minY])
                {
                    nextpath[minX + 1, minY] = minY * Column + minX;
                    shortestpath[minX + 1, minY] = shortestpath[minX, minY] + terrain[minX + 1, minY];
                }
            }else if(minY > 0 && final[minX, minY - 1] == 0)
            {
                if (shortestpath[minX, minY - 1] > shortestpath[minX, minY] + terrain[minX, minY - 1])
                {
                    nextpath[minX, minY - 1] = minY * Column + minX;
                    shortestpath[minX, minY - 1] = shortestpath[minX, minY] + terrain[minX, minY - 1];
                }
            }else if (minY < Column - 1 && final[minX, minY + 1] == 0)
            {
                if (shortestpath[minX, minY + 1] > shortestpath[minX, minY] + terrain[minX, minY + 1])
                {
                    nextpath[minX, minY + 1] = minY * Column + minX;
                    shortestpath[minX, minY + 1] = shortestpath[minX, minY] + terrain[minX, minY + 1];
                }
            }
        }
        int monsterX;
        int monsterY;
        for(int i = 0; i < monstersnum; i++)
        {
            monsterX = monstersX[i];
            monsterY = monstersY[i];
            monstersX[i] = nextpath[monsterX, monsterY] % Column;
            monstersY[i] = nextpath[monsterX, monsterY] / Column;
        }
    }
}
