# Getting Started with Kinvey

If you are starting with a fresh Xamarin project or an existing app, follow these steps to set up the Kinvey `Client`.

## Initialize a Client

The `Client.Builder` is used to build and initialize the `Client` before making any calls to the Kinvey API. 

You need to set the following arguements on your client:

 * Set your  App Key and App Secret obtained from the Kinvey console.  
 * Set a Logger delegate, so the Library can write output. 
 * Set a file path and sqlite implementation for persistating metadata.


Initializing a `Client` is usually done when your application first starts.


###iOS Client Creation
```csharp
using KinveyXamarin;

Client kinveyClient = new Client.Builder(your_app_key, your_app_secret)
			.setLogger(delegate(string msg) { Console.WriteLine(msg);})
			.setFilePath(NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User) [0].ToString())
			.setOfflinePlatform(new SQLitePlatformIOS())
			.build();

```

###Android Client Creation
```csharp
using KinveyXamarin;

Client kinveyClient = new Client.Builder(your_app_key, your_app_secret)
			.setLogger(delegate(string msg) { Console.WriteLine(msg);})
			.setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
			.setOfflinePlatform(new SQLitePlatformAndroid())
			.build();

```

###PCL Client Creation

```csharp
public Client createClient(string filePath, ISQLitePlatform platform)
{
	Client kinveyClient = new Client.Builder(your_app_key, your_app_secret)
			.setLogger(delegate(string msg) { Console.WriteLine(msg);})
			.setFilePath(filePath)
			.setOfflinePlatform(platform)
			.build();
			
	return kinveyClient;
}
```

####For more details on how to get started with Kinvey, please check out our guides in the following link:

  * <http://devcenter.kinvey.com/xamarin/guides/>

####To learn more about Kinvey, please view our tutorials:

  * <http://devcenter.kinvey.com/xamarin/tutorials/>


