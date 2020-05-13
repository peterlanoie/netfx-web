namespace Common.Web
{
	/// <summary>
	/// Defines the basic properties of a hyperlink.
	/// </summary>
	public class HyperLink
	{
		/// <summary>
		/// Create a new empty instance.
		/// </summary>
		public HyperLink()
		{
		}

		/// <summary>
		/// Create a new instance with the url and link text.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="linkText"></param>
		public HyperLink(string url, string linkText)
		{
			Url = url;
			LinkText = linkText;
		}

		/// <summary>
		/// The link's URL. Can be fully qualified or relative.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The display text of the link.
		/// </summary>
		public string LinkText { get; set; }
	}
}