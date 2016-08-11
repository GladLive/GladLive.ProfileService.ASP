using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// Repository service for <see cref="GladLiveProfileModel"/> model objects.
	/// </summary>
	public interface IProfileRepository : IRepository<GladLiveProfileModel>
	{
		/// <summary>
		/// Queries for the profile by the <see cref="string"/>
		/// name of the profile.
		/// </summary>
		/// <param name="userName">User name of the profile.</param>
		/// <returns>The profile with the matching <paramref name="userName"/> or null.</returns>
		GladLiveProfileModel GetByProfileName(string userName);
	}

	/// <summary>
	/// Repository service for <see cref="GladLiveProfileModel"/> model objects.
	/// </summary>
	public interface IProfileRepositoryAsync : IRepositoryAsync<GladLiveProfileModel>
	{
		/// <summary>
		/// Asyncronously queries for the profile by the <see cref="string"/>
		/// name of the profile.
		/// </summary>
		/// <param name="userName">User name of the profile.</param>
		/// <returns>A future of the profile with the matching <paramref name="userName"/> or null.</returns>
		Task<GladLiveProfileModel> GetByProfileNameAsync(string userName);
	}
}
