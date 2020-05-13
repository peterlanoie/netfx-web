using System;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;


namespace Common.Web
{
    /// <summary>
    /// Class for doing rss feed-related presentations
    /// </summary>
    [Serializable]
    public class RssFeed
    {
        private List<string> _errors = new List<string>();

        /// <summary>
        /// Setting for the writer
        /// </summary>
        private XmlWriterSettings _settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = " ",
            OmitXmlDeclaration = true,
            Encoding = new UTF8Encoding(false),
        };

        /// <summary>
        /// Link to the feed article (from the feed item)
        /// </summary>
        public string FeedHref { get; set; }

        /// <summary>
        /// Unique id for the feed (used in caching)
        /// </summary>
        public string FeedId { get; set; }

        /// <summary>
        /// The feed itself
        /// </summary>
        private SyndicationFeed _feed { get; set; }

        /// <summary>
        /// How long should the teaser text be?
        /// </summary>
        public int SnippetLength { get; set; }

        /// <summary>
        /// The URI of the feed
        /// </summary>
        public string FeedUri { get; set; }

        /// <summary>
        /// Title of the feed
        /// </summary>
        public string FeedTitle { get; set; }

        /// <summary>
        /// Date of the feed publish
        /// </summary>
        public DateTime FeedPublishDate { get; set; }

        /// <summary>
        /// storage for last error 
        /// </summary>
        public List<string> Errors { get { return _errors; } }

        /// <summary>
        /// Snippet from the feed
        /// </summary>
        public string FeedSnippet
        {
            get
            {
                if (!string.IsNullOrEmpty(FeedContent))
                {
                    FeedContent = FeedContent.Substring(0, SnippetLength > FeedContent.Length ? FeedContent.Length : SnippetLength);
                    int IndexOfLastWhiteSpace = FeedContent.LastIndexOf(" ", FeedContent.Length);
                    FeedContent = FeedContent.Substring(0, IndexOfLastWhiteSpace) + "...";

                    /*					
                    string FeedRegex = ConfigurationManager.AppSettings["FeedRegex"];

                    if (string.IsNullOrEmpty(FeedRegex))
                    {
                        FeedRegex = "(By[^<]+?)(<br /><br />)(.+)$";
                    }

                    Match Found = Regex.Match(FeedContent, FeedRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
                    if (Found.Success)
                    {
                        try
                        {
                            string Result = Found.Groups[3].Captures[0].Value;
                            Result = Result.Substring(0, SnippetLength > Result.Length ? Result.Length : SnippetLength);
                            int IndexOfLastWhiteSpace = Result.LastIndexOf(" ", Result.Length);
                            Result = Result.Substring(0, IndexOfLastWhiteSpace) + "...";
                            return Result;
                        }
                        catch (Exception)
                        {
                            return FeedContent;
                        }
                    }
                    */
                }

                return FeedContent;
            }
        }

        /// <summary>
        /// ByLine of the Feed
        /// </summary>
        public string FeedByLine
        {
            get
            {
                if (!string.IsNullOrEmpty(FeedContent))
                {
                    Match Found = Regex.Match(FeedContent, "(By[^<]+?)<", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    if (Found.Success)
                    {
                        try
                        {
                            return Found.Groups[1].Captures[0].Value;
                        }
                        catch (Exception)
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Full content of the feed
        /// </summary>
        public string FeedContent { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Id">Unique id for the feed</param>
        /// <param name="URI">Feed URI</param>
        /// <param name="FeedSnippetLength">How much text in the teaser?</param>
        public RssFeed(string Id, string URI, int FeedSnippetLength)
        {
            FeedId = Id;
            FeedUri = URI;
            SnippetLength = FeedSnippetLength;
            Initialize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RssFeed() { }

        /// <summary>
        /// Create and set props for this object; sets up the latest item in this feed ONLY; 
        /// multiple feed items are not currently supported
        /// </summary>
        public void Initialize()
		{
			string strError = string.Empty;

			try
			{
				new Uri(FeedUri);
			}
			catch (Exception e)
			{
				throw new ApplicationException(string.Format("FeedUri {0} is not a valid URI. Exception message: {1}", FeedUri, e.ToString()), e);
			}

			try
			{
				using (XmlReader Reader = XmlReader.Create(FeedUri))
				{
					if ((!Reader.EOF) && !System.Diagnostics.Debugger.IsAttached)
					{
						_feed = SyndicationFeed.Load(Reader);

						foreach (var Item in _feed.Items)
						{
							if (Item.Title != null && !string.IsNullOrEmpty(Item.Title.Text))
							{
								FeedTitle = Item.Title.Text;
							}

							try
							{
								FeedPublishDate = Item.PublishDate.LocalDateTime;
							}
							catch (Exception)
							{
								FeedPublishDate = DateTime.Now;
							}

							if (Item.Summary != null && !string.IsNullOrEmpty(Item.Summary.Text))
							{
								FeedContent = Item.Summary.Text;
							}

							if (Item.Links != null && Item.Links.Count > 0)
							{
								try
								{
									FeedHref = Item.Links[0].GetAbsoluteUri().ToString();
								}
								catch (Exception)
								{
									FeedHref = FeedUri;
								}
							}

							return; //just do the first (latest one) for now, since this is how comm server is doing things
						}
					}
				}
			} catch (Exception e) {
				Errors.Add(e.Message);
			}
		}
    }
}
