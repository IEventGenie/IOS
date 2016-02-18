using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using UIKit;

namespace BeaconstacExample
{
	public partial class webViewController : UIViewController
	{

		private NSUrl url;
		public NSUrl webUrl
		{
			get 
			{
				return this.url;
			}
			set 
			{
				this.url = value;
			}

		}

		public webViewController (IntPtr handle) : base (handle)
		{

		}

		public webViewController (NSUrl url)
		{
			this.webUrl = url;
			var webV = new UIWebView ( View.Bounds);
			View.AddSubview (webV);
			webV.LoadRequest (new NSUrlRequest (this.webUrl));
		}
			

	}
}
