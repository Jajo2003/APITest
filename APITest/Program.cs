using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

class Program
{
	static void Main(string[] args)
	{

		CreateWebHostBuilder(args).Build().Run();
		
	}

	public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		WebHost.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddMvc();
			})
			.Configure(app =>
			{
				app.UseRouting();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});
			});
}

[ApiController]
public class UserControler : ControllerBase
{
	[HttpGet("/api/getCorrect")]
	
	public IActionResult getValid()
	{
		string a = "2001";
		bool isValid = CheckCharacters(a);

		int ID;
		DB data = new DB();
		var status = Response.StatusCode;
		if (!isValid || !int.TryParse(a, out ID))
		{
			status = 400;
			Response.StatusCode = status;
			return BadRequest(new {Message = "YOUR ID MUST CONTAIN ONLY DIGITS",StatusCode = "Can't ADD ID with Characters"});

		}
		else if (data.CheckExists(ID))
		{
			status = 400;
			Response.StatusCode = status;
			return BadRequest(new { Message = "THIS ID ALREADY EXISTS", StatusCode = "already Exists" });
		}
		else
		{
			status = 200;
			Response.StatusCode = status;
			return Ok(new { Message = $"Your ID({ID}) ADDED SUCCESSFULLY",StatusCode = $"Success"});
		}
		

	}
	

	private bool CheckCharacters(string a)
	{
		for(int i = 0; i < a.Length; i++)
		{
			if (a[i] < '0' || a[i] > '9')
				return false;
		}
		return true;
	}
	
}

