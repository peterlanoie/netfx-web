using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Common.Web.Modules
{
	/// <summary>
	/// Defines the configuration section for the request delay HTTP module.
	/// </summary>
	public class RequestDelaySection : ConfigurationSection
	{
		/// <summary>
		/// The delay type to use.
		/// </summary>
		[ConfigurationProperty("type", DefaultValue = RequestDelayType.None, IsRequired = true)]
		public RequestDelayType DelayType { get { return (RequestDelayType)this["type"]; } }

		/// <summary>
		/// Gets the fixed delay time in milliseconds.
		/// Used for fixed delay mode only.
		/// </summary>
		[ConfigurationProperty("fixeddelaytime", DefaultValue = 2000, IsRequired = false)]
		public int FixedDelayTime { get { return (int)this["fixeddelaytime"]; } }

		/// <summary>
		/// Gets the minimum delay in millseconds.
		/// Used for random delay mode only.
		/// </summary>
		[ConfigurationProperty("mindelaytime", DefaultValue = 1000, IsRequired = false)]
		public int MinDelayTime { get { return (int)this["mindelaytime"]; } }

		/// <summary>
		/// Gets the maximum random delay time.
		/// Used for random delay mode only.
		/// </summary>
		[ConfigurationProperty("maxdelaytime", DefaultValue = 5000, IsRequired = false)]
		public int MaxDelayTime { get { return (int)this["maxdelaytime"]; } }
	}

	/// <summary>
	/// The request delay types.
	/// </summary>
	public enum RequestDelayType
	{
		/// <summary>
		/// No request delay.
		/// </summary>
		None,

		/// <summary>
		/// Fixed request delay, uses a fixed time value.
		/// </summary>
		Fixed,

		/// <summary>
		/// Random request delay between a defined or default minimum and maximum.
		/// </summary>
		Random
	}
}
