# Slot-Machine

## Project Explanation
This is a 5x3 Slot Machine game where reels spin on clicking the **Spin** button and stop one after another with a bounce effect.

To build the game:

* I used **ScriptableObjects** to define symbols with unique IDs, names, and icons.
* Created a **symbol prefab** with icon, text, and highlight visuals.
* A **Symbol script** handles applying visuals and tracking positions.
* The **ReelManager** controls spinning by moving symbols and reusing them in a loop.
* I used **LeanTween** for smooth movement and timing the spin stop.
* A **SlotMachineManager** handles spin timing across all reels, stopping them sequentially (e.g., Reel 1 at 3s, Reel 2 at 3.3s, etc.).
* Basic match-check logic is included using nested loops.
* Added **audio effects** for a more polished experience.

This modular setup makes the system easy to expand with win detection, animations, or scoring later.

## Instruction to Run
### Play Build 
1. I have made a build for PC you can check '/Build' Folder.
2. Click Slote Machine.exe file and start playing.
3. Press Spin Button to play. 

### Play From Unity Project
1. Open Unity Project in Unity 6000.0.47f1
2. Locate /Assets/Scenes/Slote Machine.unity
3. Click Play Button in Unity
4. In Game Scene press spin button to play slot machine

## Used Assets
1. Cute GUI-Pack-Lite : For background UI
https://assetstore.unity.com/packages/2d/gui/icons/cute-gui-pack-lite-202389
2. Sweets Icon Pack : For Symbol pack
https://assetstore.unity.com/packages/2d/gui/icons/sweets-icon-pack-191548

<img src="Recordings/Movie_000.gif" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Image%20Sequence_003_0000.png" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Image%20Sequence_002_0000.png" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Image%20Sequence_005_0000.png" alt="Slot Machine Preview" width="400"/>
