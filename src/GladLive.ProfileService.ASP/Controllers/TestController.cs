using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace GladLive.ProfileService.ASP
{
	[Route("api/test")]
	public class TestController : Controller
	{
		private ILogger loggingService { get; }

		public TestController(ILogger<TestController> logger)
		{
			loggingService = logger;
		}

		[Authorize]
		[HttpGet]
		public void Get()
		{
			foreach(Claim claim in User.Claims)
				loggingService.LogCritical($"Has Claim: {claim.Type}  {claim.Value}.");
		}
	}
}
