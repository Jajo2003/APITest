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
	[HttpGet("/api/getExists")]
	
	public IActionResult getValid()
	{
		string a = "10022";
		bool isValid = CheckCharacters(a);

		int ID;
		DB data = new DB();
		var status = Response.StatusCode;

		if (!isValid || !int.TryParse(a, out ID))
		{
			status = 400;
			Response.StatusCode = status;
			return BadRequest(new {Message = "Error wrong Characters ",StatusCode = "Error" , Value =a});

		}
		else if (!data.CheckExists(ID))
		{
			status = 400;
			Response.StatusCode = status;
			return BadRequest(new { Message = "Doesn't exists", StatusCode = "Error", Value = a});
		}
		else
		{
			status = 200;
			Response.StatusCode = status;
			return Ok(new { Message = $"Found",StatusCode = $"Success", Value = a });
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

