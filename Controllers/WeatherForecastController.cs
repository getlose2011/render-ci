using Microsoft.AspNetCore.Mvc;

namespace render_ci.Controllers
{
    [ApiController]
    [Route("api/todo")] // 💡 這是主路徑大門
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("weather")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // 模擬一個記憶體內的 List 集合（先當作臨時的假資料庫）
        private static readonly List<TodoItem> _todoList = new()
        {
            new TodoItem { Id = 1, Title = "學會 C# 基本功", IsCompleted = true },
            new TodoItem { Id = 2, Title = "打通 GitHub Actions 綠燈", IsCompleted = true },
            new TodoItem { Id = 3, Title = "成功部署網站到 Render", IsCompleted = true },
            new TodoItem { Id = 4, Title = "寫出人生第一個自製 API", IsCompleted = false }
        };

        // 1. 查詢所有代辦事項 (GET: /Todo)
        [HttpGet("all")]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoList;
        }

        // 2. 新增一筆代辦事項 (POST: /Todo)
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem newItem)
        {
            // 自動計算下一個 Id
            newItem.Id = _todoList.Count > 0 ? _todoList.Max(t => t.Id) + 1 : 1;
            _todoList.Add(newItem);
            return Ok(newItem);
        }
    }

    public class TodoItem
    {
        public int Id { get; set; }          // 編號
        public string Title { get; set; } = ""; // 代辦事項標題
        public bool IsCompleted { get; set; } // 是否完成
    }
}
