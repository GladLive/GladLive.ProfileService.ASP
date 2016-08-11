using GladLive.Payload.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.ProfileService.ASP
{
	/// <summary>
	/// Object model for a <see cref="GladLive"/> profile.
	/// </summary>
	public class GladLiveProfileModel
	{
		/// <summary>
		/// Primary ID for the profile on this server.
		/// (IDs are not the same for users across services)
		/// </summary>
		[Key]
		public int ServiceId { get; private set; }

		/// <summary>
		/// Color of the GladLive profile.
		/// </summary>
		public ProfileColorModel Color { get; set; }

		/// <summary>
		/// Tag of the GladLive profile.
		/// </summary>
		public ProfileTagModel Tag { get; set; }

		/// <summary>
		/// Username of the GladLive profile.
		/// </summary>
		public string UserName { get; set; }

		//This Model will be expanded as needed.
	}
}
