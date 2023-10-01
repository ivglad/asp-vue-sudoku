// using System;
// using System.Linq;
// using System.Security.Cryptography;

namespace api;

public class SudokuHandler
{
    // Размер сетки судоку
    private const int GRIDSIZE = 5;
    private static int[,] fileGrid = new int[GRIDSIZE, GRIDSIZE];
    private static int[,] grid = new int[GRIDSIZE, GRIDSIZE];
    private static int[] gridNumbers = new int[GRIDSIZE];

    public static async Task<int[,]> HandleFile(string fileName)
    {
        string filePath = FilesHandler.GetFilePath(fileName);

        fileGrid = await FilesHandler.GetGrid(filePath);
        grid = fileGrid;

        for (int i = 0; i < gridNumbers.Length; i++)
        {
            gridNumbers[i] = i + 1;
        }
        Random random = new Random();
        gridNumbers = gridNumbers.OrderBy(x => random.Next()).ToArray();

        FindAWayOrFadeAway(grid);

        return grid;
    }

    private static bool FindAWayOrFadeAway(int[,] grid)
    {
        int row = -1;
        int col = -1;

        // Нахождение пустой ячейки для заполнения
        for (int r = 0; r < GRIDSIZE; r++)
        {
            for (int c = 0; c < GRIDSIZE; c++)
            {
                if (grid[r, c] == 0)
                {
                    row = r;
                    col = c;
                    break;
                }
            }
            if (row != -1) break;
        }

        if (IsFoundWay())
        {
            return true;
        }
        
        foreach (int number in gridNumbers)
        {
            if (IsValidPosition(row, col, number))
            {
                grid[row, col] = number;
                if (FindAWayOrFadeAway(grid))
                {
                    return true;
                }
                grid[row, col] = 0;
            }
        }
        return false;
    }

    // Проверка на нахождение числа в строке и столбце по условиям судоку
    private static bool IsValidPosition(int row, int col, int number)
    {
        for (int i = 0; i < GRIDSIZE; i++)
        {
            if (grid[row, i] == number || grid[i, col] == number)
            {
                return false;
            }
        }
        return true;
    }

    // Проверка на пустые ячейки (нулевые)
    private static bool IsFoundWay()
    {
        for (int r = 0; r < GRIDSIZE; r++)
        {
            for (int c = 0; c < GRIDSIZE; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}