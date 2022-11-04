
-------------------------------------------

GameAssetStudio
http://gameassetstudio.com/

-------------------------------------------

===========================================
Ver 1.12 Update Information
===========================================

1. Some directory names have been changed. 
2. Some file names have been changed. 
3. In Unity 4.6.2 and higher versions the Cutoff in the toon_cutoff shader wasn't working properly. This has been fixed.
4. Adjusted the light in viewer.
5. Fixed an import error with animation.fbx.
6. You can now set the Alpha cutoff of the toon_cutoff shader. 0.9 is the recommended setting.


===========================================
How to use a Particle Effects
===========================================
1.Particle data name and location
Assets > Succubus Twins Character Pack > Resources > Particles > Prefabs
- Magic Spell - Hand (Stationary)   : eff_succubus_magicspell_hand_00
- Magic Spell - Shot (Stationary)   : eff_succubus_magicspell_shot_00
- Magic Spell - Bullet (Stationary) : eff_succubus_magicspell_bullet_stationary_00
- Magic Spell - Bullet (Moves 4m)   : eff_succubus_magicspell_bullet_4m_00
- Magic Spell - Bullet (Moves 8m)   : eff_succubus_magicspell_bullet_8m_00
- Magic Spell - Bullet (Moves 20m)  : eff_succubus_magicspell_bullet_20m_00
- Damage Effect  : eff_succubus_damage_00
- Seduced Effect : eff_succubus_seduced_00
- Seduce Effect  : eff_succubus_seduce_00
- Materialization Effect : eff_succubus_materialization_00

2.Models with particle effects have been included as samples.
Assets > Succubus Twins Character Pack > Resources > Samples

- succubus_a_damage 　
- succubus_a_magicspell 0m
- succubus_a_magicspell 4m
- succubus_a_magicspell 8m
- succubus_a_magicspell 20m
- succubus_a_materialization
- succubus_a_seduce
- succubus_a_seduced

- succubus_b_damage
- succubus_b_magicspell 0m
- succubus_b_magicspell 4m
- succubus_b_magicspell 8m
- succubus_b_magicspell 20m
- succubus_b_materialization
- succubus_b_seduce
- succubus_b_seduced

3.How to use
- 1. Place the model in the Hierarchy.
- 2. Start the game.
- 3. When the screen is clicked, particle effects will appear.


===========================================
Notes on Particles
===========================================

1. The added particle effects may only be used with Succubus Twins Character Pack.
If used with other assets, there is a chance they may not function as intended.

2. Particle effects may be used with the following animations.

- Magic Spell - Hand (Stationary)   : succubus_a_magic_01 / succubus_b_magic_01 
- Magic Spell - Shot (Stationary)   : succubus_a_magic_01 / succubus_b_magic_01 
- Magic Spell - Bullet (Stationary) : succubus_a_magic_01 / succubus_b_magic_01 
- Magic Spell - Bullet (Moves 4m)   : succubus_a_magic_01 / succubus_b_magic_01 
- Magic Spell - Bullet (Moves 8m)   : succubus_a_magic_01 / succubus_b_magic_01 
- Magic Spell - Bullet (Moves 20m)  : succubus_a_magic_01 / succubus_b_magic_01 
- Damage Effect  : succubus_a_damage_l / succubus_b_damage_L
- Seduced Effect : succubus_a_death_lp / succubus_b_death_lp
- Seduce Effect  : succubus_a_charm_02 / succubus_b_charm_02
- Materialization Effect : succubus_a_apper_float / succubus_b_apper_float

When used with other animations, there is a chance they may not function as intended.

===========================================
How to use a Model
===========================================
- 1. Select a model you wish to use from project, then right-click on it.
- 2. Choose Export Package…
- 3. Export all of the assets while Include Dependencies.
- 4. Specify an arbitrary directory and a package name, then create a package file.
- 5. Open a project in which you wish to use the model.
- 6. Select Asset > Import Package > Custom Package...
- 7. Select the Package you chose the above step 4 and import it.
- 8. Once the model is imported to the project, it is ready for use.

===========================================
Succubus Twins Character Pack Setting
===========================================
There are two types of floating animations.
Those with appended with "_float" were created with a determined height.
Those without "_float" will be played from the origin.

There are two types of body textures.
When using a cutoff shader with a texture appended with "_p" you'll see the fur.

To properly align the sisters in the holding hands pose, from the origin place one sister on the x axis at 0.75 and one at -0.75.

If you have to adjust the skinned mesh renderer bounds, the model is located in the Prefab folder in the Asset folder.
If it is not culling the camera, try adjusting the skinned mesh renderer bounds.

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
- 1. Choose Edit>Project>Settings>Quality.
- 2. In the Inspector tab, choose Other>Blend Weights and set that to "4 Bones."
- 3. With this method there is no need to adjust model configurations each time they are brought into the scene.

Method 3
- 1. You can now set the Alpha cutoff of the toon_cutoff shader. 0.9 is the recommended setting.


===========================================
Facial BlendShapes Setting
===========================================
Facial BlendShapes *Hi-model Only

- Arum
Settings:
succubus_a_h > succubus > succubus_a_h_face > Skinned Mesh Renderer > BlendShapes
succubus_a_face.facial01_succubus_a_[FacialName] - succubus_a_face.facial16_succubus_a_[FacialName](0-100)

- Asphodel
Settings:
succubus_b_h > succubus > succubus_b_h_face > Skinned Mesh Renderer > BlendShapes
succubus_b_face.facial01_succubus_b_[FacialName] - succubus_b_face.facial16_succubus_b_[FacialName](0-100)


===========================================
Changing the facial expression of M and L sized models
===========================================
1. Select Assets > Succubus Twins Character Pack > Resources > Arum > Materials and then succubus_a_face_m or succubus_a_face_l
2. In the Inspector tab where material information is displayed, select Texture. 
3. By selecting "facial01_succubus_a_xxx" in the texture overview, the facial expression can be changed.
* xxx will be replaced by the corresponding facial expression's file name
* The above description is for the Arum character



===========================================
How to use a Asset Viewer
===========================================

http://gameassetstudio.com/asset/chara003/viewer/index.html


===========================================
Unity Program Setting
===========================================
Asset Viewer ( WebPlayer )
Only able to use legacy animations

SceneFile:
Succubus Twins Character Pack > Viewer > succbus_a_viewer.unity
Succubus Twins Character Pack > Viewer > succbus_b_viewer.unity

Recommended Settings:
- PlayerSettings > Setting for Web Player > Resolution and Presentation
- Default Screen Width 960
- Default Screen Width 745

===========================================
 Description of directory
===========================================
[Succubus Twins Character Pack]
  [Resources]
  : The animations and models that make up the main asset are stored here. Please use them as you see fit.
      [Arum]           : Animations are included with Arum's model.
        [Animations Legacy]  : Legacy Animation Data
        [Animations Mecanim] : Mecanim Animation Data
        [Materials]          : Material Data
        [Models Legacy]      : Legacy Model Data
          [Prefab]           : Legacy Model Data
        [Models Mecanim]     : Mecanim Model Data
          [Prefab]           : Mecanim Model Data
        [Textures]           : Texture Data
      [Asphodel]       : Animations are included with Asphodel's model.
        [Animations Legacy]  : Legacy Animation Data
        [Animations Mecanim] : Mecanim Animation Data
        [Materials]          : Material Data
        [Models Legacy]      : Legacy Model Data
          [Prefab]           : Legacy Model Data
        [Models Mecanim]     : Mecanim Model Data
          [Prefab]           : Mecanim Model Data
        [Textures]           : Texture Data
      [Particles]      : Particle effects are included.
        [Animations]         : Particle Animation Data
        [Materials]          : Particle Material Data
        [Prefabs]            : Particle Data
        [Textures]           : Particle Texture Data
      [Samples]
      : Sample Models with Particle Effects
        [AddScriptToParticles]    : Particle Prefabs with Scripts
        [ParticleInstancePrefab]  : Prefabs Used To Instance Particles
        [Script]                  : Scripts Used By Samples
          [Particls]              : Sample Particle Scripts
            [ArumParticles]       : Sample Particle Scripts
            [AsphodelParticles]   : Sample Particle Scripts
    [Shaders]                     : Shader Data
  [Viewer]
  : The files in the hierarchy below this are used by Viewer, so please do not move or rename any of the files.

    succbus_a_viewer.scene        : Arum Viewer Scene File
    succbus_b_viewer.scene        : Asphodel Viewer Scene File
    [GUI]                         : Viewer GUI Images
    [Resources]                   : Resources used by Viewer
      [Succubus]
          [Viewer Settings]       : Viewer Setting File
          [Viewer BackGround]     : Viewer Background Images
          [Viewer Materials]      : Face Materials used by Viewer
          [Viewer StandSet]       : Pedestal models are included.
            [Materials]           : Pedestal Material and Particle Material Data
            [Models]              : Pedestal Model Data
            [Particles]           : Pedestal Particle Data
            [Textures]            : Pedestal Texture Data
    [Scripts]                     : Viewer Scripts
      [Particls]                  : Viewer Particle Scripts
        [ArumParticls]            : Viewer Particle Scripts
        [AsphodelParticls]        : Viewer Particle Scripts
