using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadFileController : ControllerBase
{
    private readonly ILogger<UploadFileController> _logger;

    public UploadFileController(ILogger<UploadFileController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GetUploadedFile")]
    public async Task Get()
    {
        Response.ContentType = "text/html;charset=utf-8";
        IFormFileCollection files = Request.Form.Files;
        if (files.Count == 0) {
            await Response.WriteAsync("Вы не отправили файл");
        }

        string uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        var file = files[0];
        string fileName = System.Guid.NewGuid().ToString();
        string fileExtension = Path.GetExtension(file.FileName);
        string fileNameWithExt = fileName + fileExtension;
        string fullPath = $"{uploadPath}/{fileNameWithExt}";
        
        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var sudoku = SudokuHandler.HandleFile(fullPath);
        
        string json = JsonConvert.SerializeObject(sudoku, Formatting.Indented);
        // string json = JsonSerializer.Serialize(sudoku);
        // await Response.WriteAsync(fileNameWithExt);
        await Response.WriteAsync(json);
    }
}
