using GladLive.Payload.Profile;
using GladNet.ASP.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet.Payload;
using GladLive.Payload.Profile.Enums.Payload;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// Controller that services <see cref="GetMinimumProfilesRequestPayload"/> requests.
	/// No authentication is required to service.
	/// </summary>
	[PayloadRoute(typeof(GetMinimumProfilesRequestPayload))]
	public class MinimumProfileRequestController : RequestController<GetMinimumProfilesRequestPayload>
	{
		/// <summary>
		/// Async profile data repository service.
		/// </summary>
		private IProfileRepositoryAsync profileRepository { get; }

		/// <summary>
		/// ASP will create this controller when a <see cref="GetMinimumProfilesRequestPayload"/> is recieved.
		/// </summary>
		/// <param name="modelRepo"><see cref="GladLiveProfileModel"/> repository service.</param>
		public MinimumProfileRequestController(IProfileRepositoryAsync modelRepo)
		{
			profileRepository = modelRepo;
		}

		/// <summary>
		/// Called internally by the GladNet <see cref="RequestController{TPayloadType}"/>.
		/// </summary>
		/// <param name="payloadInstance">The incoming GladNet payload.</param>
		/// <returns>Returns a response payload.</returns>
		public override async Task<PacketPayload> HandlePost(GetMinimumProfilesRequestPayload payloadInstance)
		{
			//Validate the payload; if it's invalid send invalid response
			if (payloadInstance.ProfileNames == null || payloadInstance.ProfileNames.Count() == 0)
				return new GetMinimumProfilesResponsePayload(MinimumProfilesResponseCode.Unknown);

			//Get the profiles by the provided names
			IEnumerable<GladLiveProfileModel> models = await GetProfiles(payloadInstance.ProfileNames);

			//Map to the network type
			List<MinimumProfileDataModel> wireReadyModels = models
				.Select(m => new MinimumProfileDataModel(m.Color, m.Tag, m.UserName)).ToList();

			//Return the response with the models and the appropriate code.
			return new GetMinimumProfilesResponsePayload(ComputeResponseCode(payloadInstance.ProfileNames, wireReadyModels), wireReadyModels);
		}

		/// <summary>
		/// Queries the <see cref="IRepository{TModelType}"/> service for the profiles requested.
		/// </summary>
		/// <param name="profiles">Desired profiles.</param>
		/// <returns>Returns a Future containing the profile models.</returns>
		private async Task<IEnumerable<GladLiveProfileModel>> GetProfiles(IEnumerable<string> profiles)
		{
			return await profileRepository.GetByProfilesNameAsync(profiles);
		}

		/// <summary>
		/// Computes the <see cref="MinimumProfilesResponseCode"/> for the request.
		/// </summary>
		/// <param name="profilesRequested">The profiles the user requested asked for.</param>
		/// <param name="resultModels">The resulting models from the query.</param>
		/// <returns>The computed result code.</returns>
		private MinimumProfilesResponseCode ComputeResponseCode(IEnumerable<string> profilesRequested, IEnumerable<IMinimumProfileDataModel> resultModels)
		{
			//Failed for an unknown reason
			if (resultModels.Count() == 0)
				return MinimumProfilesResponseCode.Unknown;

			if (resultModels.Count() < profilesRequested.Count())
				return MinimumProfilesResponseCode.PartialSuccess;

			//If the above passes then it was a successful query.
			return MinimumProfilesResponseCode.Success;
		}
	}
}
