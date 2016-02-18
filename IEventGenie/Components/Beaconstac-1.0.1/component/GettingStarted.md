Getting Started with Beaconstac
===================


##Beaconstac basics:

**Beacons** are small Bluetooth Low Energy based transmitters that can help with detecting proximity and manage messaging in an indoor environment. Beacons are usually identified using three fields: UUID string, Major number and Minor number.

A **Rule** allows you to define a set of conditions that must be satisfied in the proximity of a beacon, for actions to be performed inside the app.

An **Action** is what you want Beaconstac to do when the conditions of a rule are met. Think of it as 'if conditions then action'

##Create a Beaconstac account

Go to https://developer.beaconstac.com/invite and request an invite for the Beaconstac platform. Once you receive an invitation, sign up at the link provided in the invitation and create a new account. You can get your organization-id and Developer-token from the account page in the portal, which will be used to initialize the sdk. For the convenience of the developers, a demo account has been created and its credentials pre-filled in the Example app.

## Initializing Beaconstac

In your app’s ViewController class, inside ViewDidLoad method, initialize Beaconstac sharedInstance using the credentials(organization-id and developer-token) from the portal. As a first step, we will start by discovering beacons in the vicinity of the device, commonly referred to as Ranging Beacons. When beacons are ranged, the SDK generates an event “BeaconsRanged” which gives you a list of all the beacons along with their identifiers and RSSI values. You can stop ranging beacons at any point by calling stopRangingBeacons method on Beaconstac. The following code accomplishes this.

    using Mobstac;
    
    public async override void ViewDidLoad ()
    	{
    		base.ViewDidLoad ();
    	    this.Title = "Select Beacon";
    	    Beaconstac bstac = Beaconstac.SharedInstance (2xx,"aadxxxxxxxxxxxx428");
    		NSDictionary opt = new NSDictionary();
    	
    // Replace the arguments with the UUID of the beacon you wish to interact with, and a unique identifier string for the region you are going to monitor, and an optional key-value 
    		bstac.StartRangingBeacons("B9407F30-F5F8-466E-AFF9-25556B57FE6D", "BeaconRegion", opt);
    }

**On iOS 8**, you must specify <code>NSLocationAlwaysUsageDescription</code> in your info.plist file with a description that will be prompted to your users. On first app launch, user will be prompted to allow the app to use your location for detecting beacons.

##Event callbacks
It is possible that there are multiple beacons around the device. However, rules are usually configured to respond to proximity of a single beacon. Beaconstac SDK is smart enough to figure out which is the beacon of interest in such cases. It generates an event called “CampedOnBeacon” which returns the beacon object of interest along with all the other beacons that are ranged at that time. 

If you have created Rules on the portal and all the conditions for that Rule are satisfied, the SDK generates an event “RuleTriggered” providing the RuleName and an array of Actions associated with the Rule. 

Use the code below to implement the generation of events from the SDK:

    public override void ViewDidLoad ()
    	{
    		..
    		// Handling event raised when device camps on to a beacon
    		bstac.BeaconstacCampedOnBeacon += HandleBeaconstacCampedOnBeacon;
    			
	    // Handling event raised when a rule is triggered
    		bstac.BeaconstacRuleTriggered += HandleBeaconstacRuleTriggered;
    
	    // Beacon ranged event handling using Lambda expression 
    		bstac.BeaconsRanged += (object sender, BeaconstacBeaconsRangedEventArgs e) => {
    			Console.WriteLine ("Ranged" + e.BeaconsDictionary.Description);
    		};
    		...
    	}
    
    	// Event is raised when a rule is triggered
    	void HandleBeaconstacRuleTriggered (object sender, BeaconstacRuleTriggeredEventArgs e)
    	{
    		Console.WriteLine ("Rule triggered " + e.RuleName);
    	}
    
    	// Event is raised when device camps on a beacon
    	void HandleBeaconstacCampedOnBeacon (object sender, BeaconstacCampedOnBeaconEventArgs e)
    	{
    		Console.WriteLine ("Camped on" + e.Beacon.BeaconKey + " amongst available beacons " + e.BeaconsDictionary.Description);
    	}

> Please not that you cannot test Beacon enabled app in iOS simulator