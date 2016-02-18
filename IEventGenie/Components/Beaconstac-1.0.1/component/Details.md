# Beaconstac details

This component binds the Beaconstac SDK for iOS so that it may be used in Xamarin.iOS projects to scan for iBeacons and add proximity enabled content delivery and notifications to your apps.

The system requirements are iOS 7+ and Bluetooth Low Energy. In your app’s ViewController class, inside ViewDidLoad method, initialize Beaconstac:

	Beaconstac bstac = Beaconstac.SharedInstance (2xx,"ffdxxxxxxxxxxxxx3e8");


**On iOS 8**, you must specify <code>NSLocationAlwaysUsageDescription</code> in your info.plist file with a description that will be prompted to users. On first app launch, users will be prompted to allow the app to use their location for detecting beacons.

The Beaconstac class is the primary means of setting up beacon region of interest and getting callbacks on detecting beacons or when rules are triggered. The Beaconstac instance created above is used to start the beacon ranging process. The messages are delivered by callback methods.

The following code shows an example of how to use the Beaconstac to range for beacons.

    public override void ViewDidLoad ()
    	{
        	base.ViewDidLoad ();
        	Beaconstac bstac = Beaconstac.SharedInstance (2xx,"aadxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx428");
        	bstac.StartRangingBeacons ("B9407F30-F5F8-466E-AFF9-25556B57FE6D","MobXam",null);
    		bstac.BeaconsRanged += (object sender, BeaconstacBeaconsRangedEventArgs e) => {
    		Console.WriteLine ("Ranged" + e.BeaconsDictionary.Description);
    		};
    	}

