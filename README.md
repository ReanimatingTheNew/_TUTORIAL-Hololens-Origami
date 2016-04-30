# Hololens Tutorial - Origami
Description: Origami Hololens Project

Source:
<a href="https://developer.microsoft.com/en-us/windows/holographic/holograms_101e">
  Origami Hololens Tutorial
</a>

###Chapter 1 - Holo World
<i>In this chapter you will set up Unity for holographic development, make a hologram, and see the hologram you made.</i>

<strong>Step 1:</strong>
Launch Unity and open Origami Project.
<br/>
<strong>Step 2:</strong>
Go to File > Scene Save As and name “Origami” inside the Assets Folder
<br/><br/>
<strong><i>Set up the main camera</i></strong>
<br/>
<strong>Step 3:</strong>
Click on the Main Camera and change y and z positions in the inspector to 0
<br/>
<strong>Step 4:</strong>
Go to clear flags set to Solid Color as Black (RGB #00000000)
<br/><br/>
<strong><i>Set up the scene</i></strong>
<br/>
<strong>Step 5:</strong>
Inside the Hierarchy Panel Click Create > Create Empty to create a new game object renamed “Origami Collection”
<br/>
<strong>Step 6:</strong>
From the Holograms folder in the Project Panel drag the Stage, Sphere1, and Sphere2 into the Origami Collection game object so that they become children of Origami Collection
<br/>
<strong>Step 7:</strong>
Right click the Directional Light object in the Hierarchy Panel and click Delete
<br/>
<strong>Step 8:</strong>
From the Holograms folder in the Project Panel drag the Lights into an empty area of the Hierarchy Panel
<br/>
<strong>Step 9:</strong>
Select the Origami Collection game object and in the inspector transform the position to 0, -0.5, 2.0 so that the origami is now in front of the user
<br/>
<strong>Step 10:</strong>
Preview project in Unity by clicking play
<br/><br/>
<strong><i>Export the project from Unity to Visual Studio</i></strong>
<br/>
<strong>Step 11:</strong>
Go to File > Build Settings > Select Windows Store and click Switch Platform with SDK set to Universal 10 and Build Type to D3D
<br/>
<strong>Step 12:</strong>
Check Unity C# Projects and Add Open Scenes to add the Origami Scene
<br/>
<strong>Step 13:</strong>
Click Player Settings and in the inspector panel select the Windows Store Logo and then select Publishing Settings and in Capabilities select Microphone and SpatialPerception capabilities.
<br/>
<strong>Step 14:</strong>
Click Build and create a new folder called App to store the build
<br/>
<strong>Step 15:</strong>
Open Origami Solution in Visual Studio
<br/>
<strong>Step 16:</strong>
Change target from Debug to Release, change ARM to X86 and change Device to Hololens Emulator
<br/>
<strong>Step 17:</strong>
Start the emulator by clicking Debug > Start without debugging and the Origami Project is now running in the emulator






