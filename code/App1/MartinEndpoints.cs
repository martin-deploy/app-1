using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App1;

public static class MartinEndpoints
{
	public static string Hello()
	{
		return "Hello, Martin!";
	}

	public static string Quote(IOptions<MartinPleaseOptions> options)
	{
		var q = options.Value.Quotes;

		if (q.Count == 0)
		{
			return "No quote today...";
		}
		else
		{
			return q[Random.Shared.Next(q.Count)];
		}
	}

	public static IResult Meta(IWebHostEnvironment env, IConfiguration configuration)
	{
		return Results.Ok(new
		{
			env = env.EnvironmentName,
			app = env.ApplicationName,
			config = configuration.AsEnumerable().ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
		});
	}
}