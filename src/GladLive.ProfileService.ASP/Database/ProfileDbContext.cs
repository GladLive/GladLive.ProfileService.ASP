using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// <see cref="DbContext"/> for the <see cref="GladLiveProfileModel"/>.
	/// </summary>
	public class ProfileDbContext : DbContext
	{
		/// <summary>
		/// Queryable object that contains the set of profiles.
		/// </summary>
		public DbSet<GladLiveProfileModel> Profiles { get; set; }

		public ProfileDbContext(DbContextOptions options) 
			: base(options)
		{

		}
	}
}
