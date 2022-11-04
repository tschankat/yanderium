-------------------------------------------

GameAssetStudio
http://gameassetstudio.com/

-------------------------------------------


===========================================
 Ver 1.03 Update Information
===========================================
1.The following simplified versions of the idle animations have been added.

- f05@f05_idle_01
- f05@f05_idle_11

2.Some directory names have been changed.
3.Some file names have been changed.

===========================================
How to use a Model
===========================================

1. Select a model you wish to use from project, then right-click on it.
2. Choose Export Package…
3. Export all of the assets while Include Dependencies.
4. Specify an arbitrary directory and a package name, then create a package file.
5. Open a project in which you wish to use the model.
6. Select Asset > Import Package > Custom Package...
7. Select the Package you chose the above step 4 and import it.
8. Once the model is imported to the project, it is ready for use.

===========================================
Satomi Character Pack Setting
===========================================
1. Animations with "_p" at the end of their file names have had their motion components removed and will remain at the origin.
2. Animations with "_a", "_b", or "_c" at the end of their file names divide the animation into 3 parts, respectively.
3.Some animations were created specifically for use with the Apron costume. 
These include:

- f05@liedown_04_a
- f05@liedown_05_a
- f05@punch_23_a
- f05@guard_20_a
- f05@crouch_20_a
- f05@guard_21_a
- f05@down_23_a

4. Some animations are intended for use with a pair of male or female characters.
This applies to the following animations:

- m01@hugshoulder_00　/　f05@hugshoulder_00
- m01@kiss_00　/　f05@kiss_00
- m01@hughip_00　/　f05@hughip_00
- m01@pathead_00　/　f05@pathead_00
- m01@hugshouldersit_00　/　f05@hugshouldersit_00

===========================================
Unity Asset Setting
===========================================
In order to use all of the character asset states in The Succubus Twins Character Pack,
please be sure to use the following settings.

Method 1
- 1. When dragging a model from the project folder to the scene, you must modify the number of bones to use per vertex for - skinning.
- 2. Bring the model into the scene and then double click on it.
- 3. In the Inspector Tab look for the "Quality" setting and change it from "Auto" to "4 Bones."
- 4. Now you are finished.
- 5. In addition, this procedure must be followed for each model you bring into the scene.

Method 2
- 1. Choose Edit > Project > Settings > Quality.
- 2. In the Inspector tab, choose Other > Blend Weights and set that to "4 Bones."
- 3. With this method there is no need to adjust model configurations each time they are brought into the scene.

-------------------------------------------

This product also supports Mecanim.
To use Mecanim, please use the settings below.

- 1. When you select a Mecanim animation in the project folder, the animation's details are displayed in the Inspector tab.
- 2. Choose the Rig tab.
- 3. "Source" is displayed as "f05_blazer_000_hAvatar".
Load "Avatar" of the costume saved in Asset > Satomi Character Pack > Resources > Satomi > Models Mecanim.
*The Mecanim animations in this product only correspond to the Satomi Character Pack


===========================================
Changing the facial expression of M and L sized models
===========================================
1. Select Assets > Satomi Character Pack > Resources > Satomi > Materials and then f05_face_00_m or f05_face_00_l
2. In the Inspector tab where material information is displayed, select Texture. 
3. By selecting "f05_face_xxx" in the texture overview, the facial expression can be changed.
* For facial expressions, please refer to the following correspondence table.

- Neutral
f05_face_00_m.png
f05_face_00_l.png

- Angry
f05_face_010_m.png
f05_face_010_l.png

- Sad
f05_face_020_m.png
f05_face_020_l.png

- Embarrassed
f05_face_030_m.png
f05_face_030_l.png

- Wink
f05_face_040_m.png
f05_face_040_l.png

- Kiss
f05_face_050_m.png
f05_face_050_l.png

- Eyes Closed
f05_face_060_m.png
f05_face_060_l.png

- Damage
f05_face_070_m.png
f05_face_070_l.png

===========================================
Unity Program Setting
===========================================
Asset Viewer ( WebPlayer )
Only able to use legacy animations

SceneFile:
Satomi Character Pack > Viewer > satomi_viewer.unity

Recommended Settings:
- PlayerSettings > Setting for Web Player > Resolution and Presentation
- Default Screen Width 960
- Default Screen Width 600


===========================================
Description of directory
===========================================
[SatomiCharacterPack]
  [Resources]
  : The animations and models that make up the main asset are stored here. Please use them as you see fit.
    [Satomi] 
      [Animations Legacy]  : Legacy Animation Data
      [Animations Mecanim] : Mecanim Animation Data
      [Materials]          : Material Data
      [Models Legacy]      : Legacy Model Data
      [Models Mecanim]     : Mecanim Model Data
      [Textures]           : Texture Data
    [Shaders]              : Shader Data

  [Viewer]
  : The files in the hierarchy below this are used by Viewer, so please do not move or rename any of the files.

    satomi_viewer.scene     : Viewer Scene File
    [GUI]                   : Viewer GUI Images
    [Resources]             : Resources used by Viewer
      [Satomi]
        [Viewer Settings]   : Viewer Setting File
        [Viewer BackGround] : Viewer Background Images
    [Scripts]               : Viewer Scripts