using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class SudokuController : ControllerBase
{
    private readonly ILogger<SudokuController> _logger;

    public SudokuController(ILogger<SudokuController> logger)
    {
        _logger = logger;
    }

    [Route("api/sudokuHandle")]
    [HttpPost]
    public async Task SudokuHandle()
    {
        Response.ContentType = "application/json;charset=utf-8";

        string fileName = "";
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
        {
            fileName = await reader.ReadToEndAsync();
        }

        var sudokuResult = await SudokuHandler.HandleFile(fileName);
        
        string json = JsonConvert.SerializeObject(sudokuResult, Formatting.Indented);

        await Response.WriteAsync(json);
    }
}
