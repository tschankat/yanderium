
Screen Shatter Effect

Table of Content
1. Basic Usage
2. Properties
3. Demo Assets
4. Contact

1. Basic Usage
To use the screen shatter effect simply create ShatterSpawner script and attach it to a GameObject.
In order to work the ShatterSpawner must be assigned a Material to use for the shards.
The script runs automatically, creating a shatter effect infront of the camera, for more detailed 
information check out the list of parameters below or the in-editor tooltips. For an example of how 
the script can be used check the demo included.

2. Properties

UseScreenSize - If set to true automatically set the width and height of the area to the screen resolution
Width - The width of the area
Height - The height of the area
Layer - The layer to use as the temporary camera's cullMask and as the shard GameoObject's layer

Shatter Parameters
ShatterOrigin - The position where you want the crack to start
RandomizeShatterOrigin - If set to true a random point inside the Width and Height is chosen

Style Parameters
ScreenMaterial - The material you want to use for the screen
NumberOfRings - The number of rings that you want in the shatter
NumberOfRadials - The number of linear cracks you want from the origin
Jitteryness - The amount of jitteryness you want in the linear cracks
BreakQuadsChance - The chance that one of the square shards will break into two triangle shards
PhysicsScale - The width of the simulation area in meters
PushForce - The amount of force applied to the individual shards to push them away from the impact

Camera Settings
ClearCamera - Set to enable to have a solid background, if not set the camera only clears the depth buffer
BackgroundColor - The background color to use if ClearCamera is set to true
CameraDepth - The camera depth to use when creating the camera


Cleanup
DestroyGameObjectOnEnd - Destroys the object this script is attached to on cleanup when set to true
MaxPlayTime - if greater than 0 cleans up the shards after the number of seconds

3. Demo Assets

Images are provided under CC0
https://pixabay.com/en/coast-beach-rock-stones-sky-192979/
https://pixabay.com/en/blue-sky-cloud-summer-196230/
https://pixabay.com/en/motion-cloud-sunrise-1641779/
https://pixabay.com/en/mt-fuji-japan-1346096/

Glass sound effects provided under CC0
http://freesound.org/people/ngruber/sounds/204777/

3. Contact

Email: flammable.octopus@gmail.com

