
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Web;
using System.ServiceModel.Syndication;


namespace RssSharpSimple
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : Window
	{
		
		public Home()
		{
			InitializeComponent();
		}
		
		void btnSubmit_Click(object sender, RoutedEventArgs e)
		{
			
			String url = txtUrl.Text;
			
			//basic validation of the user input
			//more detailed exception handling takes place in the getFeed function
			if (!(url == "") && (url.EndsWith(".xml")))
			{
				//make sure that the url starts with 'http://'
				if (!(url.StartsWith("http://")))
				{
				    url = "http://" + url;
				}
				
				//get the feed from the url now that the url has been validated
				SyndicationFeed feed = getFeed(url);
				//display feed in the listbox
				lstResults.ItemsSource = feed.Items;
			}
			else
			{
				MessageBox.Show("Enter a valid url into the search box");
			}
			
			
		}
		
		SyndicationFeed getFeed(String url)
		{
			//this function will get the rss feed and put it into the SyndicationFeed object
			//all exceptions with return an empty SyndicationFeed object
			
			XmlReader r;
			SyndicationFeed feed;
			
			try
			{
				r = XmlReader.Create(url);
				feed = SyndicationFeed.Load(r);
				return feed;
			}
			catch (System.IO.FileNotFoundException ex)
			{
				MessageBox.Show("Invalid feed. Check URL." + "\n\n" + ex.ToString());
				feed = new SyndicationFeed();
			}
			catch (System.IO.DirectoryNotFoundException ex)
			{
				MessageBox.Show("Invalid feed. Check URL." + "\n\n" + ex.ToString());
				feed = new SyndicationFeed();
			}
			catch (System.Net.WebException ex)
			{
				MessageBox.Show("Invalid URL" + "\n\n" + ex.ToString());
				feed = new SyndicationFeed();
			}
			catch (UriFormatException ex)
			{
				MessageBox.Show("The url is formatted incorrectly" + "\n\n" + ex.ToString());
				feed = new SyndicationFeed();
			}
			
			return feed;
		}
	}
}