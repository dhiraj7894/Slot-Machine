# Slot-Machine

## Project Explanation
This is a 5x3 Slot Machine game where clicking the Spin button triggers the reels to spin for a specific duration. Once the spin time ends, each reel stops one by one with a scale bounce effect for visual feedback.

1. Symbol Setup using ScriptableObject : I started by creating a ScriptableObject for each symbol. This included a unique ID, name, and icon. These fields help identify and display the correct symbol visuals during gameplay.

2. Prefab Creation for Symbols : Before setting up the reels, I made a prefab that includes the symbol icon, a highlight effect, and a label (text). This prefab forms the base for every symbol in the reel.

3. Symbol Script & Visual Handling : I created a Symbol script and attached it to the prefab. This script uses the ScriptableObject to fetch and apply the correct visuals. It also keeps track of the symbol’s currentPos and nextPos for animation purposes.

4. Reel Movement – ReelManager : I built a ReelManager class to control the spinning logic. It handles symbol movement by looping through each one and shifting its position from currentPos to nextPos. Once a symbol goes off-screen, it's reused and sent back to the top, creating a seamless spinning effect.

5. Symbol Refresh & Position Logic : The ReelManager also includes methods to refresh symbols on each spin, calculate the next position in the sequence, and handle other reel-specific tasks.

6. Spin Animation & Speed Tweaks : To create a more dynamic spinning effect, I used LeanTween to control movement speed. A small "jump" or acceleration is added at the start to make the spin feel more responsive.

7. Timed Spin Stop : Using LeanTween’s delayed call, I managed to stop each reel after a specific duration. This ensures consistent timing for reel stop animations.

8. SlotMachineManager – Reel Control : Another script, SlotMachineManager, is responsible for starting the spin and coordinating the stopping sequence. Each reel stops with a slight delay from the previous one—for example, the first reel stops at 3s, the second at 3.3s, the third at 3.6s, and so on.

9. Symbol Match Check : I implemented basic matching logic to check if adjacent symbols on the same row match based on their ScriptableObject. This is currently handled using a nested for-loop.

10. Audio Integration : For added feedback, I included audio effects that play during the spin and when reels stop.

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

<img src="Recordings/Image%20Sequence_003_0000.png" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Image%20Sequence_002_0000.png" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Image%20Sequence_005_0000.png" alt="Slot Machine Preview" width="400"/>
<img src="Recordings/Movie_000.gif" alt="Slot Machine Preview" width="400"/>
//![Image 2](/Recordings/Image%20Sequence_002_0000.png)
//![Image 3](/Recordings/Image%20Sequence_005_0000.png)
//![Giff](/Recordings/Movie_000.gif)
