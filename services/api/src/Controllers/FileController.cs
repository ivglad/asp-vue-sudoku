using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    [Route("api/uploadFile")]
    [HttpPost]
    public async Task UploadFile()
    {
        Response.ContentType = "text/html;charset=utf-8";

        IFormFileCollection files = Request.Form.Files;
        if (files.Count == 0)
        {
            await Response.WriteAsync("Вы не отправили файл");
        }

        string fileName = await FilesHandler.Save(files);

        await Response.WriteAsync(fileName);
    }

    [Route("api/showFileGrid")]
    [HttpPost]
    public async Task ShowFileGrid()
    {
        Response.ContentType = "application/json;charset=utf-8";
        
        string filePath = "";
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
        {
            filePath = await reader.ReadToEndAsync();
        }
        int[,] fileData = await FilesHandler.GetGrid(filePath);

        string json = JsonConvert.SerializeObject(fileData, Formatting.Indented);

        await Response.WriteAsync(json);
    }
}
