using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// Concrete repository for <see cref="Profile"/> / <see cref="ProfileDbContext"/>.
	/// </summary>
	public class ProfileRepository : Repository<ProfileDbContext>, IProfileRepository, IProfileRepositoryAsync
	{
		public ProfileRepository(ProfileDbContext context)
			: base(context)
		{

		}

		/// <summary>
		/// Queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>True if the model exists or false otherwise.</returns>
		public bool Exists(int id)
		{
			//Indicates if at least one account exists with that id (should be unique though)
			return databaseContext.Profiles
				.Where(x => x.ServiceId == id)
				.Count() != 0;
		}

		/// <summary>
		/// Async queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future bool of True if the model exists or false otherwise.</returns>
		public async Task<bool> ExistsAsync(int id)
		{
			//Async: indicates if at least one account exists with that id (should be unique though)
			IEnumerable<GladLiveProfileModel> accounts = await databaseContext.Profiles.ToAsyncEnumerable()
				.Where(ax => ax.ServiceId == id).ToList();

			return accounts.Count() != 0;
		}

		/// <summary>
		/// Queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>The model instance or null if not found.</returns>
		public GladLiveProfileModel Get(int id)
		{
			return databaseContext.Profiles
				.FirstOrDefault(x => x.ServiceId == id);
		}

		/// <summary>
		/// Queries for a non-lazy collection of all <see cref="Profile"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null collection of all <see cref="Profile"/>s.</returns>
		public IEnumerable<GladLiveProfileModel> GetAll()
		{
			return databaseContext.Profiles;
		}

		/// <summary>
		/// Async queries for a non-lazy collection of all <see cref="Profile"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null future collection of all <see cref="Profile"/> s.</returns>
		public async Task<IEnumerable<GladLiveProfileModel>> GetAllAsync()
		{
			return await databaseContext.Profiles.ToAsyncEnumerable().ToList();
		}

		/// <summary>
		/// Async queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future model instance or null if not found.</returns>
		public async Task<GladLiveProfileModel> GetAsync(int id)
		{
			return await databaseContext.Profiles.ToAsyncEnumerable()
				.FirstOrDefault();
		}

		/// <summary>
		/// Queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="userName">Profile name of the account.</param>
		/// <returns>The account with the matching <paramref name="userName"/> or null.</returns>
		public GladLiveProfileModel GetByProfileName(string userName)
		{
			return databaseContext.Profiles
				.Where(a => a.UserName == userName)
				.FirstOrDefault();
		}

		/// <summary>
		/// Asyncronously queries for the account by the <see cref="string"/>
		/// name of the account.
		/// </summary>
		/// <param name="userName">Profile name of the account.</param>
		/// <returns>A future of the account with the matching <paramref name="userName"/> or null.</returns>
		public async Task<GladLiveProfileModel> GetByProfileNameAsync(string userName)
		{
			return await databaseContext.Profiles
				.ToAsyncEnumerable()
				.Where(a => a.UserName == userName)
				.FirstOrDefault();
		}

		/// <summary>
		/// Asyncronously queries for the profiles by the <see cref="string"/>
		/// name of the profile.
		/// </summary>
		/// <param name="userNames">User names of the profiles.</param>
		/// <returns>A future of the profiles with the matching <paramref name="userNames"/> or null.</returns>
		public async Task<IEnumerable<GladLiveProfileModel>> GetByProfilesNameAsync(IEnumerable<string> userNames)
		{
			return await databaseContext.Profiles.ToAsyncEnumerable()
				.Where(p => userNames.Contains(p.UserName))
				.ToList();
		}
	}
}
