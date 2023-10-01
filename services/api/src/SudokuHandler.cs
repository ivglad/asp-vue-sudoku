namespace api;

public class SudokuHandler
{
    private const int SIZE = 5; // Размер судоку
    private static int count = 0; // Количество найденных решений

    public static async Task<int[,]> HandleFile(string filePath)
    {
      // int[,] grid = createGridFromFile(filePath);

      int[,] grid = new int[5,5];
      // {
      //     {5, 0, 4, 0, 0},
      //     {2, 0, 0, 0, 3},
      //     {3, 0, 2, 0, 4},
      //     {4, 0, 0, 3, 0},
      //     {0, 2, 0, 4, 0}
      // };

      using (StreamReader reader = new StreamReader(filePath))
      {
          string? line;
          int index = 0;
          while ((line = await reader.ReadLineAsync()) != null)
          {
            int[] charsArray = new int[5];
            for(int i = 0; i < 5; i++)
            {
              int number;
              if (Int32.TryParse(line[i].ToString(), out number))
              {
                  // charsArray[i] = number;
                  grid[index, i] = number;
              }
              else
              {
                  // charsArray[i] = 0;
                  grid[index, i] = 0;
              }
            }
            // grid[index] = charsArray;
            index++;
          }
      }

      // // Solve(grid);

      // Console.WriteLine("Всего решений: " + count);
      return grid;
    }

    // private static async Task<int[,]> createGridFromFile(string filePath)
    // {
    //   int[,] grid = new [,]{};

    //   using (StreamReader reader = new StreamReader(filePath))
    //   {
    //       string? line;
    //       while ((line = await reader.ReadLineAsync()) != null)
    //       {
    //         string[] charsArray = new string[line.Length];
    //         for(int i = 0; i < line.Length; i++)
    //         {
    //           if (line[i].ToString() == "*") {
    //             charsArray[i] = 0;
    //           } else {
    //             charsArray[i] = line[i].ToString();
    //           }
    //         }
    //           Console.WriteLine(line);
    //       }
    //   }
    //   return grid;
    // }
}