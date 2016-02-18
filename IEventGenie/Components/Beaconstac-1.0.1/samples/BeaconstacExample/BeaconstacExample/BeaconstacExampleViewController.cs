﻿using System;
using System.Drawing;

using Foundation;
using UIKit;

using CoreLocation;
using Mobstac;

namespace BeaconstacExample
{
	public partial class BeaconstacExampleViewController : UIViewController
	{
		public BeaconstacExampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();			
		}

		#region View lifecycle
	
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = @"Beaconstac";

			// Initializing with a demo account
			// This account is configured to show a webpage http://beaconstac.com when in proximity of
			// a Beacon with UUID:B9407F30-F5F8-466E-AFF9-25556B57FE6D   Major:34950   Minor:53825
			// To configure your own rules and beacons, Request for an invite on https://developer.beaconstac.com/invite
			Beaconstac bstac = Beaconstac.SharedInstance (271,"77130dfaf40a035acb003eac5345db6bd50053d9");

			// Starts ranging beacons with given (Estimote beacon) UUID and a string as region identifier
			bstac.StartRangingBeacons ("B9407F30-F5F8-466E-AFF9-25556B57FE6D","MobstacRegion",null);

			// Handling event raised when device camps on to a beacon
			bstac.BeaconstacCampedOnBeacon += HandleBeaconstacCampedOnBeacon;

			// Handling event raised when a rule is triggered
			bstac.BeaconstacRuleTriggered += HandleBeaconstacRuleTriggered;

			// Beacon ranged event handling using Lambda expression 
			bstac.BeaconsRanged += (object sender, BeaconstacBeaconsRangedEventArgs e) => {
				Console.WriteLine ("Ranged" + e.BeaconsDictionary.Description);
			};
		}

		// Event is raised when a rule is triggered
		void HandleBeaconstacRuleTriggered (object sender, BeaconstacRuleTriggeredEventArgs e)
		{
			if (e.ActionArray.Length > 0) {

				// A rule can have an array of actions associated with it. This demo app handles
				// a webPage rule - opens a webpage in proximity of beacon
				foreach (MSAction action in e.ActionArray) {
					if (action.ActionType == MSActionType.Webpage) {
						NSUrl thisUrl = (NSUrl)action.Message;
						this.openWebViewController (thisUrl);
					} else {
						// If no action type is not webpage, the rule name is displayed in a popup
						UIAlertView alert = new UIAlertView ("Rule Triggered", "Rule Name : " + e.RuleName, null, "OK", null);
						alert.Show ();
					}
				}
			} 
		}

		// Event is raised when device camps on a beacon
		void HandleBeaconstacCampedOnBeacon (object sender, BeaconstacCampedOnBeaconEventArgs e)
		{
			Console.WriteLine ("Camped on" + e.Beacon.BeaconKey + " amongst available beacons " + e.BeaconsDictionary.Description);
		}

		// Transitions to a new viewController with a webpage with given URL
		public void openWebViewController(NSUrl url)
		{	
			var webVC = new webViewController (url);
			this.NavigationController.PushViewController (webVC, true);
		}
			
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

