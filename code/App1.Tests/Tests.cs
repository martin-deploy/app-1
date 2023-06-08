using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App1.Tests;

public class Tests
{
	private WebApplicationFactory<Program> _factory;

	[SetUp]
	public void SetUp()
	{
		_factory = new WebApplicationFactory<Program>();
	}

	[Test]
	public async Task Test_root()
	{
		using var client = _factory.CreateClient();

		var response = await client.GetAsync("/");

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		Assert.That(await response.Content.ReadAsStringAsync(), Is.EqualTo("Hello, Martin!"));
	}

	[Test]
	public async Task Test_quote_with_default_quote_list()
	{
		string[] configuredQuotes = _factory.Services.GetRequiredService<IOptions<MartinPleaseOptions>>().Value.Quotes.ToArray();

		using var client = _factory.CreateClient();

		var response = await client.GetAsync("/quote");

		Assert.Multiple(async () =>
		{
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(await response.Content.ReadAsStringAsync(), Is.AnyOf(configuredQuotes));
		});
	}

	[Test]
	public async Task Test_quote_with_configured_quote_list()
	{
		using var client = _factory
			.WithWebHostBuilder(builder =>
			{
				builder.ConfigureTestServices(services =>
				{
					services.Configure<MartinPleaseOptions>(options =>
					{
						options.Quotes.Clear();
						options.Quotes.Add("Test 1");
						options.Quotes.Add("Test 2");
						options.Quotes.Add("Test 3");
					});
				});
			})
			.CreateClient();

		var response = await client.GetAsync("/quote");

		Assert.Multiple(async () =>
		{
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(await response.Content.ReadAsStringAsync(), Is.AnyOf("Test 1", "Test 2", "Test 3"));
		});
	}

	[Test]
	public async Task Test_quote_with_empty_quote_list()
	{
		using var client = _factory
			.WithWebHostBuilder(builder =>
			{
				builder.ConfigureTestServices(services =>
				{
					services.Configure<MartinPleaseOptions>(options =>
					{
						options.Quotes.Clear();
					});
				});
			})
			.CreateClient();

		var response = await client.GetAsync("/quote");

		Assert.Multiple(async () =>
		{
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(await response.Content.ReadAsStringAsync(), Is.EqualTo("No quote today..."));
		});
	}
}