using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iEventGenie.iOS
{
	partial class IntroController : BaseController
	{
		public IntroController (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var url = "https://www.google.co.in/"; // NOTE: https secure request
			webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
		}



	}
}
