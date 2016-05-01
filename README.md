# Hololens Tutorial - Origami
Description: Origami Hololens Project

Source:
<a href="https://developer.microsoft.com/en-us/windows/holographic/holograms_101e">
  Origami Hololens Tutorial
</a>

###Chapter 1 - Holo World
<i>In this chapter you will set up Unity for holographic development, make a hologram, and see the hologram you made.</i>
<br/><br/>
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
<br/><br/>
###Chapter 2 - Gaze
<i>In this chapter you will visualize your gaze using a world-locked cursor.</i>
<br/><br/>
<strong>Step 1:</strong>
Select the Hologram folder in the Project Panel
<br/>
<strong>Step 2:</strong>
Drag the Cursor object into the Hierarchy panel
<br/>
<strong>Step 3:</strong>
Double-click on the Cursor object and right-click in the Scripts Folder in the Project Panel to create a new C# Script named
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/WorldCursor.cs">
WorldCursor
</a>
<br/>
<strong>Step 4:</strong>
Select the Cursor and drag and drop the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/WorldCursor.cs">
WorldCursor 
</a>
Script into the Inspector Panel
<br/>
<strong>Step 5:</strong>
Double Click on the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/WorldCursor.cs">
WorldCursor
</a>
Script to edit the script in Visual Studio and write the following code
```C#
using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            // Display the cursor mesh.
            meshRenderer.enabled = true;

            // Move thecursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;

            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }
    }
}
```
<strong>Step 6:</strong>
Rebuild app in Unity in the same App Folder and run in emulator in Visual Studio
<br/><br/>
###Chapter 3 - Gestures
<i>In this chapter you will control your holograms with the select gesture.</i>
<br/><br/>
<strong>Step 1:</strong>
Select the Hologram folder in the Project Panel
<br/>
<strong>Step 2:</strong>
Double-click on the Origami Collection object and right-click in the Scripts Folder in the Project Panel to create a new C# Script named 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
GazeGestureManager
</a>
<br/>
<strong>Step 3:</strong>
Select the Origami Collection object and drag and drop the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
GazeGestureManager
</a>
Script into the Inspector Panel
<br/>
<strong>Step 4:</strong>
Double-click on the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
GazeGestureManager
</a>
Script to edit the script in Visual Studio and write the following code
```C#
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
```
<strong>Step 5:</strong>
Double-click on the Origami Collection object and right-click in the Scripts Folder in the Project Panel to create a new C# Script named 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
SphereCommands
</a>
<br/>
<strong>Step 6:</strong>
Select the Sphere1 object and drag and drop  the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
SphereCommands
</a>
Script into the Inspector Panel and do the same for Sphere2
<br/>
<strong>Step 7:</strong>
Double-click on the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/GazeGestureManager.cs">
SphereCommands
</a>
Script to edit the script in Visual Studio and write the following code
```C#
using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}
```
<strong>Step 8:</strong>
Rebuild app in Unity in the same App Folder and run emulator in Visual Studio and use spacebar to simulate a tap gesture in the emulator
<br/><br/>
###Chapter 4 - Voice
<i>In this chapter you will control your holograms through voice commands.</i>
<br/><br/>
<strong>Step 1:</strong>
Double-click on the Origami Collection object and right-click in the Scripts Folder in the Project Panel to create a new C# Script named 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/SpeechManager.cs">
SpeechManager
</a>
<br/>
<strong>Step 2:</strong>
Select the Origami Collection object and drag and drop the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/SpeechManager.cs">
SpeechManager 
</a>
Script into the Inspector Panel  
<br/>
<strong>Step 3:</strong>
Double-click on the 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/SpeechManager.cs">
SpeechManager
</a>
Script to edit the script in Visual Studio and write the following code
```C#
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Reset world", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");
        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnDrop");
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
```
<strong>Step 4:</strong>
Update 
<a href="https://github.com/kevincarrier/_TUTORIAL-Hololens-Origami/blob/master/Origami/Assets/Scripts/SphereCommand.cs">
SphereCommand.cs 
</a>
Script with the following code
```C#
Vector3 originalPosition;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the sphere when the app starts.
        originalPosition = this.transform.localPosition;
    }
// Called by SpeechManager when the user says the "Reset world" command
    void OnReset()
    {
        // If the sphere has a Rigidbody component, remove it to disable physics.
        var rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            DestroyImmediate(rigidbody);
        }

        // Put the sphere back into its original local position.
        this.transform.localPosition = originalPosition;
    }

    // Called by SpeechManager when the user says the "Drop sphere" command
    void OnDrop()
    {
        // Just do the same logic as a Select gesture.
        OnSelect();
    }
```
<strong>Step 5:</strong>
Rebuild app in Unity in the same App Folder and run emulator in Visual Studio




