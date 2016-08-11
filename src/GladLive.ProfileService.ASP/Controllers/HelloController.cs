using GladLive.Payload.Common;
using GladNet.ASP.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// Implementation of the <see cref="HelloRequestPayload"/> controller from <see cref="GladLive.Payload.Common"/>
	/// </summary>
	[PayloadRoute(typeof(HelloRequestPayload))]
	public class HelloController : AuthenticatedRequestController<HelloRequestPayload>
	{
		private ILogger loggingService { get; }

		public HelloController(ILogger<HelloController> logger)
		{
			loggingService = logger;
		}

		public override async Task<PacketPayload> HandlePost(HelloRequestPayload payloadInstance)
		{
			//TODO: We should probably do some DB stuff here to check if the user is in the DB
			//If not we should do a query to add them
			loggingService.LogCritical($"Hello: {GladLiveUserName}");

			return new HelloResponsePayload(HelloResponseCode.HelloSuccess);
		}
	}
}
