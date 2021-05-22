## Pulse Unity StreamingAssets ##

### Why? ###
The Pulse engine requires some files to run that it expects to read somewhere on the file system. To be able to package your application, those files need to be transfered directly in your application bundle. Unity enables that functionality through the [StreamingAssets](https://docs.unity3d.com/Manual/StreamingAssets.html) directory:

>Any files placed in a folder called **StreamingAssets** (case-sensitive) in a Unity project will be copied verbatim to a particular folder on the target machine. 

### How? ###

The **StreamingAssets** directory that will be used to copy files directly to the package has to be located directly inside your Assets folder, which means that you need to copy the `PulsePhysiologyEngine/StreamingAssets/PulseDataFiles` folder inside `YourProject/Assets/StreamingAssets/`.

*Note: The `PulseEngineDriver` component will dynamically ensure those files are properly located when you update the streaming assets folder. If they are not, you will see a warning in the Unity inspector indicating the exact path where you need to copy those files on your system. If they are properly located, you will be able to setup your driver with an initial state file.*