# Vicmap: The Race against Time

## Developed by

- Niki D'Arcy (102106269)
- Dulara Prasad Rathnamalala Rathnamalala Bandaralage (105518249)
- Jericho Gonzalez
- Kieran O'Brien (105179073)

## Game Description

*Vicmap: The Race against Time* is a 2D top-down game set during a fictional storm over the City of Boroondara. You play a new emergency dispatcher who gets thrown in the deep end as the seasoned dispatchers just need someone to handle the overflow, working from a control room that has seen better days. As the storm worsens across three stages, the phone rings with calls from residents reporting blocked roads, flooding and a place for potential shelter.

Your job is to work out what each call means and mark it on the in-game map by placing road block and flood hazards where you think the problem really is. However, the information you receive is not always reliable. A caller can be calm when something is wrong, or sure of a location that doesn't match the map. You can check the map, the security camera feeds and your pin-board before you commit to an answer. Once you press Submit, your answer is locked in.

After the final stage, an end screen reviews every call you handled, shows whether you got it right, and explains what kind of information problem was involved.

## Connection to Supplied Challenge

The challenge from the Department of Transport and Planning asks for a game where accurate, reliable spatial information helps a player understand an emergency and make better decisions.

- **Fictional emergency:** a three-stage storm hitting Boroondara. The storm progress bar at the top of the screen tracks it.
- **Features of Interest:** the locations in calls (hospitals, fire and ambulance stations, schools and more... to be added) come from the supplied Boroondara dataset. The supplied data is kept as the reference layer.
- **Unreliable information:** each call asset records a data issue (none, missing, misclassified, outdated or miss-located), kept separate from the reference data as the fictional game layer. Callers can describe the wrong place, report with undeserved confidence or be unsure of where they actually are.
- **Decisions depend on checking:** the player can cross-check the caller against the map, camera feeds and pin-board, then commit to their choice by pressing Submit.
- **Data quality and consequences:** the storm waits while a call is being handled, so the player isn't punished for checking. The end screen then links each missed call to the type of information problem behind it.
- **Key messages:** knowing what is where matters, and so does being able to trust that information.

## Controls

| Input | Action |
| --- | --- |
| **W / A / S / D** | Move |
| **E** | Interact with a highlighted object (phone, map table, pin-board, camera desk) |
| **Mouse** | Press UI buttons (Continue, Submit and others) Can also be used to access the map table |

### Map view

- Click **Road Blocks** or **Flooding** to create a hazard block.
- Drag a block to position it on the map. Drag it onto the bin to delete it.
- Use the scroll wheel to zoom.

### Pin-board view

- How does the pin-board work

### Camera desk view

- How does the camera work

## How to Play

1. Start the game from the main menu.
2. Wait for the phone to ring, walk to it and press **E** to answer.
3. Read the call, then press **Continue**. The storm is paused from the moment the phone rings until you submit your answer.
4. Use the map table, camera desk and pin-board to work out what's going on. Open the map and place a hazard where the call says the problem is. If the report is described as all clear and you deem it to be so, leave that location clear.
5. Press **Submit** to lock in your answer. The next call will come if there is one.
6. Repeat through all three storm stages.
7. The end screen reviews each call.
Only road block, flood and all-clear calls are scored at the moment (see Known Issues).

## How to Run the Build

### From itch.io

1. In the itch.io browser [itch.io](https://orchidofthemacabre.itch.io/vicmap-the-race-against-time)

### From the Repository

1. Download the build from our [Git repository](https://github.com/Kieran-obi/Vicmap-The-Race-Against-Time).
2. Unzip the folder and run **[executable name].exe**.
3. Choose **Play** on the main menu.

**To run from the Unity project:** open the project folder with Unity Hub using Unity **6000.3.21f1**, open `Assets/Scenes/Main_Menu`, and press Play.

## Key Programming Systems

**Call system** (`CallData`, `LocationData`, `CallManager`, `DialogueTemplates`)
Every call is a ScriptableObject asset holding its storm stage, caller location, actual location, claim type, template variables and data issue type. Dialogue isn't written per call. Each claim type maps to a template sentence with placeholders, and `DialogueTemplates.Build` fills them from the calls variables. New calls can be created in the Uity Editor.

**Call flow, resolution and end screen** (`CallManager`, `PhoneInteractable`, `MapSubmitButton`)
A storm event queues the next call and the phone rings. Answering shows the dialogue box, then Continue lets the player move again to interact with the map/cameras/pinboard, then the Submit button on the map will score the call. Scoring compares the placed hazards with the expected hazard type and the location's map position, within a tolerance (which needs fine tuning). Each result is recorded and shown on the end screen with a lesson based on the call's issue.

**Weather and storm progress** (`WeatherManager`, `WeatherEvent`, `StormStageTracker`, `StormProgressUI`, `StormProgressPersistence`, `WeatherManagerPersistence`)
`WeatherEvent` stores a storm stage's name, intensity and chance. `WeatherManager` keeps a timeline of scheduled events and its own clock, which only runs while a game is in progress and isn't paused. On each tick (5 seconds by default) it fires every event whose time has passed, subject to its chance, through a static event that `CallManager` listens to when queueing the next call. The clock freezes when a call rings and resumes after the player submits on the map. It starts when the game scene loads and resets when the main menu loads, so a replay starts fresh. `StormStageTracker` counts the stages fired, and `StormProgressUI` fills the progress bar and glides the cloud marker along it.`StormProgressPersistence` and `WeatherManagerPersistence` keep the bar and the weather manager alive between scenes without creating duplicates.

**View and camera switching** (`GameManager`, `SceneTransitionInteractable`, `SceneSwitcher`, `EventMaskSwitcher`)
GameManager handles the scene type and cameras. The game has to run asynchronously due to the CRT camera render textures.
Async allows for a live feed of the scenes to the cameras. SceneSwitcher is attached to objects with a 2D Colliders and switches out
the cameras on click. EventMaskSwitcher sets the event mask of the 2D Raycaster component on the main camera. Depending on the
scene, there are multiple 2d raycasters and in their case they have to be toggled on and the main toggled off so the click interactions
work. SceneTransitionInteractable allows for scene switching on key press (E).

**Hazard placement** (`HazardManager`, `HazardDraggable`, `HazardSpawner`, `MapPanZoom`)
[Jericho]

**Interaction and movement** (`InteractableObject`, `PlayerController`)
[Jericho?]

**Audio** (`AudioManager`)
A persistent manager with separate sources for music, storm ambience, sound effects and the phone ring.

**Pinboard** (`PinBoard`)
Handles the PinBoard scene, allowing the player to create and type on notes. This is intended to memorising what is happening in the environment.

**Settings** (`Settings_Menu`)
A script to handle the settings in the Settings scene. Features a resolution switcher.

**Pause Menu** (`PauseMenu`)
A simple menu that the player can access with the ESC button. Consists of functions for pausing and resuming the game, which is attached to the "Menu" canvas
in the VicmapRoom scene.


## Team Contributions

| Member | Contribution |
| --- | --- |
| Niki D'Arcy | Pause menu, settings menu, main menu and game scenes blocking/visuals, camera switching mechanics including cullings masks and layer handling, sprite 
and map  visual editing in Clip Studio Paint, `GameManager`, `SceneSwitcher`, `EventMaskSwitcher`, `PinBoard`, `Settings` and unused scripts 
`MapGrid`, `DijkstraBehaviour`, `CivilianBehaviour`. Made 2 sprite animations for a cat and dog sprite I downloaded. Set up shaders. |
| Dulara Prasad Rathnamalala Rathnamalala Bandaralage | Weather system: `WeatherEvent` and `WeatherManager`, which schedule the three storm stages on a timeline (with a chance value) and broadcast each stage as an event that other systems listen to. Storm progress bar HUD: `StormProgressUI` (fill bar and gliding cloud marker) and `StormStageTracker` (counts fired stages). `StormProgressPersistence` and `WeatherManagerPersistence`: keep both systems alive across scenes without duplicates. |
| Jericho Gonzalez | **Write here** |
| Kieran O'Brien | Caller scenario design and the call data (call and location assets, including map positions). Dialogue templates. `CallManager`: stage call queues, dialogue box, call resolution and the end screen. `AudioManager`: background music, storm ambience and the phone ring. |

## Known Issues

- **Restarting a run:** the weather timeline, storm progress bar and audio persist between scenes. Returning to the main menu and playing again without closing the game won't reset them. Closing and reopening the game is the way to restart currently.
- **Saved hazards:** hazard blocks are saved to a file between sessions, so blocks from a previous run can still be on the map. Delete them by dragging them to the bin before you start. Or just use the same placements and you'll get the same score, there is only three calls for the prototype.
- **Calls not yet scored:** people trapped and uncertain calls are not scored or included right now, and won't appear on the end screen.
- **Placement tolerance:** a block counts as correct if it is within a set distance of the location's icon. Blocks placed beside an icon, and not pretty much on top of it, can still count as a miss.
- **Pathing:** the grid and path-finding groundwork (`MapGrid`, `DijkstraBehaviour`, `CivilianBehaviour`) is not connected to gameplay. Some or all of these will likely be used for assignment 3.
- **Notes:** getting stuck in the middle-top-left of the screen, making it undraggable or hard to drag.
