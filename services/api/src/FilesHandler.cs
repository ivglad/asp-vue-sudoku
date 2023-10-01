namespace api;

public class FilesHandler
{
    // Размер сетки судоку
    private const int GRIDSIZE = 5;
    private static int[,] grid = new int[GRIDSIZE, GRIDSIZE];

    // Сохранение файла
    public static async Task<string> Save(IFormFileCollection files)
    {
        string uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string fileName = System.Guid.NewGuid().ToString();
        var file = files[0];
        string fileExtension = Path.GetExtension(file.FileName);
        string fileNameWithExt = fileName + fileExtension;
        string filePath = $"{uploadPath}/{fileNameWithExt}";
        
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return fileNameWithExt;
    }

    // Возвращает сетку судоку из текстового файла
    public static async Task<int[,]> GetGrid(string fileName)
    {
        string filePath = FilesHandler.GetFilePath(fileName);

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            int index = 0;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                line = line.Replace(" ", "");
                for(int i = 0; i < GRIDSIZE; i++)
                {
                    int number;
                    if (Int32.TryParse(line[i].ToString(), out number))
                    {
                        grid[index, i] = number;
                    }
                    else
                    {
                        grid[index, i] = 0;
                    }
                }
                index++;
            }
        }
        return grid;
    }

    // Возвращает путь к файлу
    public static string GetFilePath(string fileName)
    {
        string uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        return fileName.Contains(uploadPath) ? fileName : $"{uploadPath}/{fileName}"; 
    }
}