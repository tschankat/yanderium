using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFilterScript : MonoBehaviour
{
	CameraFilterPack_3D_Anomaly Anomaly;
	CameraFilterPack_3D_Binary Binary;
	CameraFilterPack_3D_BlackHole BlackHole3D;
	CameraFilterPack_3D_Computer Computer;
	CameraFilterPack_3D_Distortion Distortion;
	CameraFilterPack_3D_Fog_Smoke FogSmoke;
	CameraFilterPack_3D_Ghost Ghost;
	CameraFilterPack_3D_Inverse Inverse;
	CameraFilterPack_3D_Matrix Matrix;
	CameraFilterPack_3D_Mirror Mirror3D;
	CameraFilterPack_3D_Myst Myst;
	CameraFilterPack_3D_Scan_Scene ScanScene;
	CameraFilterPack_3D_Shield Shield;
	CameraFilterPack_3D_Snow Snow;
	CameraFilterPack_AAA_Blood AAABlood;
	CameraFilterPack_AAA_BloodOnScreen AAABloodOnScreen;
	CameraFilterPack_AAA_Blood_Hit AAABloodHit;
	CameraFilterPack_AAA_Blood_Plus AAABloodPlus;
	CameraFilterPack_AAA_SuperComputer SuperComputer;
	CameraFilterPack_AAA_SuperHexagon SuperHexagon;
	CameraFilterPack_AAA_WaterDrop WaterDrop;
	CameraFilterPack_AAA_WaterDropPro WaterDropPro;
	CameraFilterPack_Alien_Vision AlienVision;
	CameraFilterPack_Antialiasing_FXAA FXAA;
	CameraFilterPack_Atmosphere_Fog Fog;
	CameraFilterPack_Atmosphere_Rain Rain;
	CameraFilterPack_Atmosphere_Rain_Pro RainPro;
	CameraFilterPack_Atmosphere_Rain_Pro_3D RainPro3D;
	CameraFilterPack_Atmosphere_Snow_8bits Snow8bits;
	CameraFilterPack_Blend2Camera_Blend Blend;
	CameraFilterPack_Blend2Camera_BlueScreen BlueScreen;
	CameraFilterPack_Blend2Camera_Color Color;
	CameraFilterPack_Blend2Camera_ColorBurn ColorBurn;
	CameraFilterPack_Blend2Camera_ColorDodge ColorDodge;
	CameraFilterPack_Blend2Camera_ColorKey ColorKey;
	CameraFilterPack_Blend2Camera_Darken Darken;
	CameraFilterPack_Blend2Camera_DarkerColor DarkerColor;
	CameraFilterPack_Blend2Camera_Difference Difference;
	CameraFilterPack_Blend2Camera_Divide Divide;
	CameraFilterPack_Blend2Camera_Exclusion Exclusion;
	CameraFilterPack_Blend2Camera_GreenScreen GreenScreen;
	CameraFilterPack_Blend2Camera_HardLight HardLight;
	CameraFilterPack_Blend2Camera_HardMix HardMix;
	CameraFilterPack_Blend2Camera_Hue Blend2CameraHue;
	CameraFilterPack_Blend2Camera_Lighten Lighten;
	CameraFilterPack_Blend2Camera_LighterColor LighterColor;
	CameraFilterPack_Blend2Camera_LinearBurn LinearBurn;
	CameraFilterPack_Blend2Camera_LinearDodge LinearDodge;
	CameraFilterPack_Blend2Camera_LinearLight LinearLight;
	CameraFilterPack_Blend2Camera_Luminosity Luminosity;
	CameraFilterPack_Blend2Camera_Multiply Multiply;
	CameraFilterPack_Blend2Camera_Overlay Overlay;
	CameraFilterPack_Blend2Camera_PhotoshopFilters PhotoshopFilters;
	CameraFilterPack_Blend2Camera_PinLight PinLight;
	CameraFilterPack_Blend2Camera_Saturation Saturation;
	CameraFilterPack_Blend2Camera_Screen Screen;
	CameraFilterPack_Blend2Camera_SoftLight SoftLight;
	CameraFilterPack_Blend2Camera_SplitScreen SplitScreen;
	CameraFilterPack_Blend2Camera_SplitScreen3D SplitScreen3D;
	CameraFilterPack_Blend2Camera_Subtract Subtract;
	CameraFilterPack_Blend2Camera_VividLight VividLight;
	CameraFilterPack_Blizzard Blizzard;
	CameraFilterPack_Blur_Bloom Bloom;
	CameraFilterPack_Blur_BlurHole BlurHole;
	CameraFilterPack_Blur_Blurry Blurry;
	CameraFilterPack_Blur_Dithering2x2 Dithering2x2;
	CameraFilterPack_Blur_DitherOffset DitherOffset;
	CameraFilterPack_Blur_Focus Focus;
	CameraFilterPack_Blur_GaussianBlur GaussianBlur;
	CameraFilterPack_Blur_Movie Movie;
	CameraFilterPack_Blur_Noise BlurNoise;
	CameraFilterPack_Blur_Radial Radial;
	CameraFilterPack_Blur_Radial_Fast RadialFast;
	CameraFilterPack_Blur_Regular Regular;
	CameraFilterPack_Blur_Steam Steam;
	CameraFilterPack_Blur_Tilt_Shift TiltShift;
	CameraFilterPack_Blur_Tilt_Shift_Hole TiltShiftHole;
	CameraFilterPack_Blur_Tilt_Shift_V TiltShiftV;
	CameraFilterPack_Broken_Screen BrokenScreen;
	CameraFilterPack_Broken_Simple BrokenSimple;
	CameraFilterPack_Broken_Spliter Spliter;
	CameraFilterPack_Classic_ThermalVision ThermalVision;
	CameraFilterPack_Colors_Adjust_ColorRGB AdjustColorRGB;
	CameraFilterPack_Colors_Adjust_FullColors AdjustFullColors;
	CameraFilterPack_Colors_Adjust_PreFilters AdjustPreFilters;
	CameraFilterPack_Colors_BleachBypass BleachBypass;
	CameraFilterPack_Colors_Brightness Brightness;
	CameraFilterPack_Colors_DarkColor DarkColor;
	CameraFilterPack_Colors_HSV HSV;
	CameraFilterPack_Colors_HUE_Rotate HUERotate;
	CameraFilterPack_Colors_NewPosterize NewPosterize;
	CameraFilterPack_Colors_RgbClamp RgbClamp;
	CameraFilterPack_Colors_Threshold Threshold;
	CameraFilterPack_Color_Adjust_Levels AdjustLevels;
	CameraFilterPack_Color_BrightContrastSaturation BrightContrastSaturation;
	CameraFilterPack_Color_Chromatic_Aberration ChromaticAberration;
	CameraFilterPack_Color_Chromatic_Plus ChromaticPlus;
	CameraFilterPack_Color_Contrast Contrast;
	CameraFilterPack_Color_GrayScale GrayScale;
	CameraFilterPack_Color_Invert Invert;
	CameraFilterPack_Color_Noise ColorNoise;
	CameraFilterPack_Color_RGB ColorRGB;
	CameraFilterPack_Color_Sepia Sepia;
	CameraFilterPack_Color_Switching Switching;
	CameraFilterPack_Color_YUV YUV;
	CameraFilterPack_Convert_Normal Normal;
	CameraFilterPack_Distortion_Aspiration Aspiration;
	CameraFilterPack_Distortion_BigFace BigFace;
	CameraFilterPack_Distortion_BlackHole BlackHole;
	CameraFilterPack_Distortion_Dissipation Dissipation;
	CameraFilterPack_Distortion_Dream Dream;
	CameraFilterPack_Distortion_Dream2 Dream2;
	CameraFilterPack_Distortion_FishEye FishEye;
	CameraFilterPack_Distortion_Flag Flag;
	CameraFilterPack_Distortion_Flush Flush;
	CameraFilterPack_Distortion_Half_Sphere HalfSphere;
	CameraFilterPack_Distortion_Heat Heat;
	CameraFilterPack_Distortion_Lens Lens;
	CameraFilterPack_Distortion_Noise DistortionNoise;
	CameraFilterPack_Distortion_ShockWave ShockWave;
	CameraFilterPack_Distortion_ShockWaveManual ShockWaveManual;
	CameraFilterPack_Distortion_Twist Twist;
	CameraFilterPack_Distortion_Twist_Square TwistSquare;
	CameraFilterPack_Distortion_Water_Drop DistortionWaterDrop;
	CameraFilterPack_Distortion_Wave_Horizontal WaveHorizontal;
	CameraFilterPack_Drawing_BluePrint BluePrint;
	CameraFilterPack_Drawing_CellShading CellShading;
	CameraFilterPack_Drawing_CellShading2 CellShading2;
	CameraFilterPack_Drawing_Comics Comics;
	CameraFilterPack_Drawing_Crosshatch Crosshatch;
	CameraFilterPack_Drawing_Curve Curve;
	CameraFilterPack_Drawing_EnhancedComics EnhancedComics;
	CameraFilterPack_Drawing_Halftone Halftone;
	CameraFilterPack_Drawing_Laplacian Laplacian;
	CameraFilterPack_Drawing_Lines Lines;
	CameraFilterPack_Drawing_Manga Manga;
	CameraFilterPack_Drawing_Manga2 Manga2;
	CameraFilterPack_Drawing_Manga3 Manga3;
	CameraFilterPack_Drawing_Manga4 Manga4;
	CameraFilterPack_Drawing_Manga5 Manga5;
	CameraFilterPack_Drawing_Manga_Color MangaColor;
	CameraFilterPack_Drawing_Manga_Flash MangaFlash;
	CameraFilterPack_Drawing_Manga_FlashWhite MangaFlashWhite;
	CameraFilterPack_Drawing_Manga_Flash_Color MangaFlashColor;
	CameraFilterPack_Drawing_NewCellShading NewCellShading;
	CameraFilterPack_Drawing_Paper Paper;
	CameraFilterPack_Drawing_Paper2 Paper2;
	CameraFilterPack_Drawing_Paper3 Paper3;
	CameraFilterPack_Drawing_Toon Toon;
	CameraFilterPack_Edge_BlackLine BlackLine;
	CameraFilterPack_Edge_Edge_filter Edgefilter;
	CameraFilterPack_Edge_Golden Golden;
	CameraFilterPack_Edge_Neon Neon;
	CameraFilterPack_Edge_Sigmoid Sigmoid;
	CameraFilterPack_Edge_Sobel Sobel;
	CameraFilterPack_EXTRA_Rotation Rotation;
	CameraFilterPack_EXTRA_SHOWFPS SHOWFPS;
	CameraFilterPack_EyesVision_1 EyesVision1;
	CameraFilterPack_EyesVision_2 EyesVision2;
	CameraFilterPack_Film_ColorPerfection ColorPerfection;
	CameraFilterPack_Film_Grain Grain;
	CameraFilterPack_Fly_Vision FlyVision;
	CameraFilterPack_FX_8bits FX8bits;
	CameraFilterPack_FX_8bits_gb FX8bitsgb;
	CameraFilterPack_FX_Ascii Ascii;
	CameraFilterPack_FX_DarkMatter DarkMatter;
	CameraFilterPack_FX_DigitalMatrix DigitalMatrix;
	CameraFilterPack_FX_DigitalMatrixDistortion DigitalMatrixDistortion;
	CameraFilterPack_FX_Dot_Circle DotCircle;
	CameraFilterPack_FX_Drunk Drunk;
	CameraFilterPack_FX_Drunk2 Drunk2;
	CameraFilterPack_FX_EarthQuake EarthQuake;
	CameraFilterPack_FX_Funk Funk;
	CameraFilterPack_FX_Glitch1 Glitch1;
	CameraFilterPack_FX_Glitch2 Glitch2;
	CameraFilterPack_FX_Glitch3 Glitch3;
	CameraFilterPack_FX_Grid Grid;
	CameraFilterPack_FX_Hexagon Hexagon;
	CameraFilterPack_FX_Hexagon_Black HexagonBlack;
	CameraFilterPack_FX_Hypno Hypno;
	CameraFilterPack_FX_InverChromiLum InverChromiLum;
	CameraFilterPack_FX_Mirror FXMirror;
	CameraFilterPack_FX_Plasma FXPlasma;
	CameraFilterPack_FX_Psycho FXPsycho;
	CameraFilterPack_FX_Scan Scan;
	CameraFilterPack_FX_Screens Screens;
	CameraFilterPack_FX_Spot Spot;
	CameraFilterPack_FX_superDot superDot;
	CameraFilterPack_FX_ZebraColor ZebraColor;
	CameraFilterPack_Glasses_On GlassesOn;
	CameraFilterPack_Glasses_On_2 GlassesOn2;
	CameraFilterPack_Glasses_On_3 GlassesOn3;
	CameraFilterPack_Glasses_On_4 GlassesOn4;
	CameraFilterPack_Glasses_On_5 GlassesOn5;
	CameraFilterPack_Glasses_On_6 GlassesOn6;
	CameraFilterPack_Glitch_Mozaic Mozaic;
	CameraFilterPack_Glow_Glow Glow;
	CameraFilterPack_Glow_Glow_Color GlowColor;
	CameraFilterPack_Gradients_Ansi Ansi;
	CameraFilterPack_Gradients_Desert Desert;
	CameraFilterPack_Gradients_ElectricGradient ElectricGradient;
	CameraFilterPack_Gradients_FireGradient FireGradient;
	CameraFilterPack_Gradients_Hue GradientsHue;
	CameraFilterPack_Gradients_NeonGradient NeonGradient;
	CameraFilterPack_Gradients_Rainbow GradientsRainbow;
	CameraFilterPack_Gradients_Stripe Stripe;
	CameraFilterPack_Gradients_Tech Tech;
	CameraFilterPack_Gradients_Therma Therma;
	CameraFilterPack_Light_Rainbow LightRainbow;
	CameraFilterPack_Light_Rainbow2 LightRainbow2;
	CameraFilterPack_Light_Water Water;
	CameraFilterPack_Light_Water2 Water2;
	CameraFilterPack_Lut_2_Lut Lut;
	CameraFilterPack_Lut_2_Lut_Extra LutExtra;
	CameraFilterPack_Lut_Mask Mask;
	CameraFilterPack_Lut_PlayWith PlayWith;
	CameraFilterPack_Lut_Plus Plus;
	CameraFilterPack_Lut_Simple LutSimple;
	CameraFilterPack_Lut_TestMode TestMode;
	CameraFilterPack_NewGlitch1 NewGlitch1;
	CameraFilterPack_NewGlitch2 NewGlitch2;
	CameraFilterPack_NewGlitch3 NewGlitch3;
	CameraFilterPack_NewGlitch4 NewGlitch4;
	CameraFilterPack_NewGlitch5 NewGlitch5;
	CameraFilterPack_NewGlitch6 NewGlitch6;
	CameraFilterPack_NewGlitch7 NewGlitch7;
	CameraFilterPack_NightVisionFX NightVisionFX;
	CameraFilterPack_NightVision_4 NightVision4;
	CameraFilterPack_Noise_TV TV;
	CameraFilterPack_Noise_TV_2 TV2;
	CameraFilterPack_Noise_TV_3 TV3;
	CameraFilterPack_Oculus_NightVision1 NightVision1;
	CameraFilterPack_Oculus_NightVision2 NightVision2;
	CameraFilterPack_Oculus_NightVision3 NightVision3;
	CameraFilterPack_Oculus_NightVision5 NightVision5;
	CameraFilterPack_Oculus_ThermaVision ThermaVision;
	CameraFilterPack_OldFilm_Cutting1 Cutting1;
	CameraFilterPack_OldFilm_Cutting2 Cutting2;
	CameraFilterPack_Pixelisation_DeepOilPaintHQ DeepOilPaintHQ;
	CameraFilterPack_Pixelisation_Dot Dot;
	CameraFilterPack_Pixelisation_OilPaint OilPaint;
	CameraFilterPack_Pixelisation_OilPaintHQ OilPaintHQ;
	CameraFilterPack_Pixelisation_Sweater Sweater;
	CameraFilterPack_Pixel_Pixelisation Pixelisation;
	CameraFilterPack_Rain_RainFX RainFX;
	CameraFilterPack_Real_VHS RealVHS;
	CameraFilterPack_Retro_Loading Loading;
	CameraFilterPack_Sharpen_Sharpen Sharpen;
	CameraFilterPack_Special_Bubble Bubble;
	CameraFilterPack_TV_50 TV50;
	CameraFilterPack_TV_80 TV80;
	CameraFilterPack_TV_ARCADE ARCADE;
	CameraFilterPack_TV_ARCADE_2 ARCADE2;
	CameraFilterPack_TV_ARCADE_Fast ARCADEFast;
	CameraFilterPack_TV_Artefact Artefact;
	CameraFilterPack_TV_BrokenGlass BrokenGlass;
	CameraFilterPack_TV_BrokenGlass2 BrokenGlass2;
	CameraFilterPack_TV_Chromatical Chromatical;
	CameraFilterPack_TV_Chromatical2 Chromatical2;
	CameraFilterPack_TV_CompressionFX CompressionFX;
	CameraFilterPack_TV_Distorted Distorted;
	CameraFilterPack_TV_Horror Horror;
	CameraFilterPack_TV_LED LED;
	CameraFilterPack_TV_MovieNoise MovieNoise;
	CameraFilterPack_TV_Noise TVNoise;
	CameraFilterPack_TV_Old Old;
	CameraFilterPack_TV_Old_Movie OldMovie;
	CameraFilterPack_TV_Old_Movie_2 OldMovie2;
	CameraFilterPack_TV_PlanetMars PlanetMars;
	CameraFilterPack_TV_Posterize Posterize;
	CameraFilterPack_TV_Rgb TVRgb;
	CameraFilterPack_TV_Tiles Tiles;
	CameraFilterPack_TV_Vcr Vcr;
	CameraFilterPack_TV_VHS TVVHS;
	CameraFilterPack_TV_VHS_Rewind VHSRewind;
	CameraFilterPack_TV_Video3D Video3D;
	CameraFilterPack_TV_Videoflip Videoflip;
	CameraFilterPack_TV_Vignetting Vignetting;
	CameraFilterPack_TV_Vintage Vintage;
	CameraFilterPack_TV_WideScreenCircle WideScreenCircle;
	CameraFilterPack_TV_WideScreenHorizontal WideScreenHorizontal;
	CameraFilterPack_TV_WideScreenHV WideScreenHV;
	CameraFilterPack_TV_WideScreenVertical WideScreenVertical;
	CameraFilterPack_VHS_Tracking Tracking;
	CameraFilterPack_Vision_Aura Aura;
	CameraFilterPack_Vision_AuraDistortion AuraDistortion;
	CameraFilterPack_Vision_Blood VisionBlood;
	CameraFilterPack_Vision_Blood_Fast VisionBloodFast;
	CameraFilterPack_Vision_Crystal Crystal;
	CameraFilterPack_Vision_Drost Drost;
	CameraFilterPack_Vision_Hell_Blood VisionHellBlood;
	CameraFilterPack_Vision_Plasma VisionPlasma;
	CameraFilterPack_Vision_Psycho VisionPsycho;
	CameraFilterPack_Vision_Rainbow VisionRainbow;
	CameraFilterPack_Vision_SniperScore SniperScore;
	CameraFilterPack_Vision_Tunnel Tunnel;
	CameraFilterPack_Vision_Warp Warp;
	CameraFilterPack_Vision_Warp2 Warp2;

	public UILabel NameLabel;

	public float DisplayTimer;
	public float Speed;

	public int FilterMax;
	int FilterID;

	public string[] FilterNames;

	public bool[] FilterSkips;

	void Start()
	{
		Anomaly = gameObject.AddComponent<CameraFilterPack_3D_Anomaly>();Anomaly.enabled = false;
		Binary = gameObject.AddComponent<CameraFilterPack_3D_Binary>();Binary.enabled = false;
		BlackHole3D = gameObject.AddComponent<CameraFilterPack_3D_BlackHole>();BlackHole3D.enabled = false;
		Computer = gameObject.AddComponent<CameraFilterPack_3D_Computer>();Computer.enabled = false;
		Distortion = gameObject.AddComponent<CameraFilterPack_3D_Distortion>();Distortion.enabled = false;
		FogSmoke = gameObject.AddComponent<CameraFilterPack_3D_Fog_Smoke>();FogSmoke.enabled = false;
		Ghost = gameObject.AddComponent<CameraFilterPack_3D_Ghost>();Ghost.enabled = false;
		Inverse = gameObject.AddComponent<CameraFilterPack_3D_Inverse>();Inverse.enabled = false;
		Matrix = gameObject.AddComponent<CameraFilterPack_3D_Matrix>();Matrix.enabled = false;
		Mirror3D = gameObject.AddComponent<CameraFilterPack_3D_Mirror>();Mirror3D.enabled = false;
		Myst = gameObject.AddComponent<CameraFilterPack_3D_Myst>();Myst.enabled = false;
		ScanScene = gameObject.AddComponent<CameraFilterPack_3D_Scan_Scene>();ScanScene.enabled = false;
		Shield = gameObject.AddComponent<CameraFilterPack_3D_Shield>();Shield.enabled = false;
		Snow = gameObject.AddComponent<CameraFilterPack_3D_Snow>();Snow.enabled = false;
		AAABlood = gameObject.AddComponent<CameraFilterPack_AAA_Blood>();AAABlood.enabled = false;
		AAABloodOnScreen = gameObject.AddComponent<CameraFilterPack_AAA_BloodOnScreen>();AAABloodOnScreen.enabled = false;
		AAABloodHit = gameObject.AddComponent<CameraFilterPack_AAA_Blood_Hit>();AAABloodHit.enabled = false;
		AAABloodPlus = gameObject.AddComponent<CameraFilterPack_AAA_Blood_Plus>();AAABloodPlus.enabled = false;
		SuperComputer = gameObject.AddComponent<CameraFilterPack_AAA_SuperComputer>();SuperComputer.enabled = false;
		SuperHexagon = gameObject.AddComponent<CameraFilterPack_AAA_SuperHexagon>();SuperHexagon.enabled = false;
		WaterDrop = gameObject.AddComponent<CameraFilterPack_AAA_WaterDrop>();WaterDrop.enabled = false;
		WaterDropPro = gameObject.AddComponent<CameraFilterPack_AAA_WaterDropPro>();WaterDropPro.enabled = false;
		AlienVision = gameObject.AddComponent<CameraFilterPack_Alien_Vision>();AlienVision.enabled = false;
		FXAA = gameObject.AddComponent<CameraFilterPack_Antialiasing_FXAA>();FXAA.enabled = false;
		Fog = gameObject.AddComponent<CameraFilterPack_Atmosphere_Fog>();Fog.enabled = false;
		Rain = gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain>();Rain.enabled = false;
		RainPro = gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro>();RainPro.enabled = false;
		RainPro3D = gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D>();RainPro3D.enabled = false;
		Snow8bits = gameObject.AddComponent<CameraFilterPack_Atmosphere_Snow_8bits>();Snow8bits.enabled = false;
		Blend = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Blend>();Blend.enabled = false;
		BlueScreen = gameObject.AddComponent<CameraFilterPack_Blend2Camera_BlueScreen>();BlueScreen.enabled = false;
		Color = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Color>();Color.enabled = false;
		ColorBurn = gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorBurn>();ColorBurn.enabled = false;
		ColorDodge = gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorDodge>();ColorDodge.enabled = false;
		ColorKey = gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorKey>();ColorKey.enabled = false;
		Darken = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Darken>();Darken.enabled = false;
		DarkerColor = gameObject.AddComponent<CameraFilterPack_Blend2Camera_DarkerColor>();DarkerColor.enabled = false;
		Difference = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Difference>();Difference.enabled = false;
		Divide = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Divide>();Divide.enabled = false;
		Exclusion = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Exclusion>();Exclusion.enabled = false;
		GreenScreen = gameObject.AddComponent<CameraFilterPack_Blend2Camera_GreenScreen>();GreenScreen.enabled = false;
		HardLight = gameObject.AddComponent<CameraFilterPack_Blend2Camera_HardLight>();HardLight.enabled = false;
		HardMix = gameObject.AddComponent<CameraFilterPack_Blend2Camera_HardMix>();HardMix.enabled = false;
		Blend2CameraHue = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Hue>();Blend2CameraHue.enabled = false;
		Lighten = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Lighten>();Lighten.enabled = false;
		LighterColor = gameObject.AddComponent<CameraFilterPack_Blend2Camera_LighterColor>();LighterColor.enabled = false;
		LinearBurn = gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearBurn>();LinearBurn.enabled = false;
		LinearDodge = gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearDodge>();LinearDodge.enabled = false;
		LinearLight = gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearLight>();LinearLight.enabled = false;
		Luminosity = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Luminosity>();Luminosity.enabled = false;
		Multiply = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Multiply>();Multiply.enabled = false;
		Overlay = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Overlay>();Overlay.enabled = false;
		PhotoshopFilters = gameObject.AddComponent<CameraFilterPack_Blend2Camera_PhotoshopFilters>();PhotoshopFilters.enabled = false;
		PinLight = gameObject.AddComponent<CameraFilterPack_Blend2Camera_PinLight>();PinLight.enabled = false;
		Saturation = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Saturation>();Saturation.enabled = false;
		Screen = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Screen>();Screen.enabled = false;
		SoftLight = gameObject.AddComponent<CameraFilterPack_Blend2Camera_SoftLight>();SoftLight.enabled = false;
		SplitScreen = gameObject.AddComponent<CameraFilterPack_Blend2Camera_SplitScreen>();SplitScreen.enabled = false;
		SplitScreen3D = gameObject.AddComponent<CameraFilterPack_Blend2Camera_SplitScreen3D>();SplitScreen3D.enabled = false;
		Subtract = gameObject.AddComponent<CameraFilterPack_Blend2Camera_Subtract>();Subtract.enabled = false;
		VividLight = gameObject.AddComponent<CameraFilterPack_Blend2Camera_VividLight>();VividLight.enabled = false;
		Blizzard = gameObject.AddComponent<CameraFilterPack_Blizzard>();Blizzard.enabled = false;
		Bloom = gameObject.AddComponent<CameraFilterPack_Blur_Bloom>();Bloom.enabled = false;
		BlurHole = gameObject.AddComponent<CameraFilterPack_Blur_BlurHole>();BlurHole.enabled = false;
		Blurry = gameObject.AddComponent<CameraFilterPack_Blur_Blurry>();Blurry.enabled = false;
		Dithering2x2 = gameObject.AddComponent<CameraFilterPack_Blur_Dithering2x2>();Dithering2x2.enabled = false;
		DitherOffset = gameObject.AddComponent<CameraFilterPack_Blur_DitherOffset>();DitherOffset.enabled = false;
		Focus = gameObject.AddComponent<CameraFilterPack_Blur_Focus>();Focus.enabled = false;
		GaussianBlur = gameObject.AddComponent<CameraFilterPack_Blur_GaussianBlur>();GaussianBlur.enabled = false;
		Movie = gameObject.AddComponent<CameraFilterPack_Blur_Movie>();Movie.enabled = false;
		BlurNoise = gameObject.AddComponent<CameraFilterPack_Blur_Noise>();BlurNoise.enabled = false;
		Radial = gameObject.AddComponent<CameraFilterPack_Blur_Radial>();Radial.enabled = false;
		RadialFast = gameObject.AddComponent<CameraFilterPack_Blur_Radial_Fast>();RadialFast.enabled = false;
		Regular = gameObject.AddComponent<CameraFilterPack_Blur_Regular>();Regular.enabled = false;
		Steam = gameObject.AddComponent<CameraFilterPack_Blur_Steam>();Steam.enabled = false;
		TiltShift = gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift>();TiltShift.enabled = false;
		TiltShiftHole = gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift_Hole>();TiltShiftHole.enabled = false;
		TiltShiftV = gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift_V>();TiltShiftV.enabled = false;
		BrokenScreen = gameObject.AddComponent<CameraFilterPack_Broken_Screen>();BrokenScreen.enabled = false;
		BrokenSimple = gameObject.AddComponent<CameraFilterPack_Broken_Simple>();BrokenSimple.enabled = false;
		Spliter = gameObject.AddComponent<CameraFilterPack_Broken_Spliter>();Spliter.enabled = false;
		ThermalVision = gameObject.AddComponent<CameraFilterPack_Classic_ThermalVision>();ThermalVision.enabled = false;
		AdjustColorRGB = gameObject.AddComponent<CameraFilterPack_Colors_Adjust_ColorRGB>();AdjustColorRGB.enabled = false;
		AdjustFullColors = gameObject.AddComponent<CameraFilterPack_Colors_Adjust_FullColors>();AdjustFullColors.enabled = false;
		AdjustPreFilters = gameObject.AddComponent<CameraFilterPack_Colors_Adjust_PreFilters>();AdjustPreFilters.enabled = false;
		BleachBypass = gameObject.AddComponent<CameraFilterPack_Colors_BleachBypass>();BleachBypass.enabled = false;
		Brightness = gameObject.AddComponent<CameraFilterPack_Colors_Brightness>();Brightness.enabled = false;
		DarkColor = gameObject.AddComponent<CameraFilterPack_Colors_DarkColor>();DarkColor.enabled = false;
		HSV = gameObject.AddComponent<CameraFilterPack_Colors_HSV>();HSV.enabled = false;
		HUERotate = gameObject.AddComponent<CameraFilterPack_Colors_HUE_Rotate>();HUERotate.enabled = false;
		NewPosterize = gameObject.AddComponent<CameraFilterPack_Colors_NewPosterize>();NewPosterize.enabled = false;
		RgbClamp = gameObject.AddComponent<CameraFilterPack_Colors_RgbClamp>();RgbClamp.enabled = false;
		Threshold = gameObject.AddComponent<CameraFilterPack_Colors_Threshold>();Threshold.enabled = false;
		AdjustLevels = gameObject.AddComponent<CameraFilterPack_Color_Adjust_Levels>();AdjustLevels.enabled = false;
		BrightContrastSaturation = gameObject.AddComponent<CameraFilterPack_Color_BrightContrastSaturation>();BrightContrastSaturation.enabled = false;
		ChromaticAberration = gameObject.AddComponent<CameraFilterPack_Color_Chromatic_Aberration>();ChromaticAberration.enabled = false;
		ChromaticPlus = gameObject.AddComponent<CameraFilterPack_Color_Chromatic_Plus>();ChromaticPlus.enabled = false;
		Contrast = gameObject.AddComponent<CameraFilterPack_Color_Contrast>();Contrast.enabled = false;
		GrayScale = gameObject.AddComponent<CameraFilterPack_Color_GrayScale>();GrayScale.enabled = false;
		Invert = gameObject.AddComponent<CameraFilterPack_Color_Invert>();Invert.enabled = false;
		ColorNoise = gameObject.AddComponent<CameraFilterPack_Color_Noise>();ColorNoise.enabled = false;
		ColorRGB = gameObject.AddComponent<CameraFilterPack_Color_RGB>();ColorRGB.enabled = false;
		Sepia = gameObject.AddComponent<CameraFilterPack_Color_Sepia>();Sepia.enabled = false;
		Switching = gameObject.AddComponent<CameraFilterPack_Color_Switching>();Switching.enabled = false;
		YUV = gameObject.AddComponent<CameraFilterPack_Color_YUV>();YUV.enabled = false;
		Normal = gameObject.AddComponent<CameraFilterPack_Convert_Normal>();Normal.enabled = false;
		Aspiration = gameObject.AddComponent<CameraFilterPack_Distortion_Aspiration>();Aspiration.enabled = false;
		BigFace = gameObject.AddComponent<CameraFilterPack_Distortion_BigFace>();BigFace.enabled = false;
		BlackHole = gameObject.AddComponent<CameraFilterPack_Distortion_BlackHole>();BlackHole.enabled = false;
		Dissipation = gameObject.AddComponent<CameraFilterPack_Distortion_Dissipation>();Dissipation.enabled = false;
		Dream = gameObject.AddComponent<CameraFilterPack_Distortion_Dream>();Dream.enabled = false;
		Dream2 = gameObject.AddComponent<CameraFilterPack_Distortion_Dream2>();Dream2.enabled = false;
		FishEye = gameObject.AddComponent<CameraFilterPack_Distortion_FishEye>();FishEye.enabled = false;
		Flag = gameObject.AddComponent<CameraFilterPack_Distortion_Flag>();Flag.enabled = false;
		Flush = gameObject.AddComponent<CameraFilterPack_Distortion_Flush>();Flush.enabled = false;
		HalfSphere = gameObject.AddComponent<CameraFilterPack_Distortion_Half_Sphere>();HalfSphere.enabled = false;
		Heat = gameObject.AddComponent<CameraFilterPack_Distortion_Heat>();Heat.enabled = false;
		Lens = gameObject.AddComponent<CameraFilterPack_Distortion_Lens>();Lens.enabled = false;
		DistortionNoise = gameObject.AddComponent<CameraFilterPack_Distortion_Noise>();DistortionNoise.enabled = false;
		ShockWave = gameObject.AddComponent<CameraFilterPack_Distortion_ShockWave>();ShockWave.enabled = false;
		ShockWaveManual = gameObject.AddComponent<CameraFilterPack_Distortion_ShockWaveManual>();ShockWaveManual.enabled = false;
		Twist = gameObject.AddComponent<CameraFilterPack_Distortion_Twist>();Twist.enabled = false;
		TwistSquare = gameObject.AddComponent<CameraFilterPack_Distortion_Twist_Square>();TwistSquare.enabled = false;
		DistortionWaterDrop = gameObject.AddComponent<CameraFilterPack_Distortion_Water_Drop>();DistortionWaterDrop.enabled = false;
		WaveHorizontal = gameObject.AddComponent<CameraFilterPack_Distortion_Wave_Horizontal>();WaveHorizontal.enabled = false;
		BluePrint = gameObject.AddComponent<CameraFilterPack_Drawing_BluePrint>();BluePrint.enabled = false;
		CellShading = gameObject.AddComponent<CameraFilterPack_Drawing_CellShading>();CellShading.enabled = false;
		CellShading2 = gameObject.AddComponent<CameraFilterPack_Drawing_CellShading2>();CellShading2.enabled = false;
		Comics = gameObject.AddComponent<CameraFilterPack_Drawing_Comics>();Comics.enabled = false;
		Crosshatch = gameObject.AddComponent<CameraFilterPack_Drawing_Crosshatch>();Crosshatch.enabled = false;
		Curve = gameObject.AddComponent<CameraFilterPack_Drawing_Curve>();Curve.enabled = false;
		EnhancedComics = gameObject.AddComponent<CameraFilterPack_Drawing_EnhancedComics>();EnhancedComics.enabled = false;
		Halftone = gameObject.AddComponent<CameraFilterPack_Drawing_Halftone>();Halftone.enabled = false;
		Laplacian = gameObject.AddComponent<CameraFilterPack_Drawing_Laplacian>();Laplacian.enabled = false;
		Lines = gameObject.AddComponent<CameraFilterPack_Drawing_Lines>();Lines.enabled = false;
		Manga = gameObject.AddComponent<CameraFilterPack_Drawing_Manga>();Manga.enabled = false;
		Manga2 = gameObject.AddComponent<CameraFilterPack_Drawing_Manga2>();Manga2.enabled = false;
		Manga3 = gameObject.AddComponent<CameraFilterPack_Drawing_Manga3>();Manga3.enabled = false;
		Manga4 = gameObject.AddComponent<CameraFilterPack_Drawing_Manga4>();Manga4.enabled = false;
		Manga5 = gameObject.AddComponent<CameraFilterPack_Drawing_Manga5>();Manga5.enabled = false;
		MangaColor = gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Color>();MangaColor.enabled = false;
		MangaFlash = gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Flash>();MangaFlash.enabled = false;
		MangaFlashWhite = gameObject.AddComponent<CameraFilterPack_Drawing_Manga_FlashWhite>();MangaFlashWhite.enabled = false;
		MangaFlashColor = gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Flash_Color>();MangaFlashColor.enabled = false;
		NewCellShading = gameObject.AddComponent<CameraFilterPack_Drawing_NewCellShading>();NewCellShading.enabled = false;
		Paper = gameObject.AddComponent<CameraFilterPack_Drawing_Paper>();Paper.enabled = false;
		Paper2 = gameObject.AddComponent<CameraFilterPack_Drawing_Paper2>();Paper2.enabled = false;
		Paper3 = gameObject.AddComponent<CameraFilterPack_Drawing_Paper3>();Paper3.enabled = false;
		Toon = gameObject.AddComponent<CameraFilterPack_Drawing_Toon>();Toon.enabled = false;
		BlackLine = gameObject.AddComponent<CameraFilterPack_Edge_BlackLine>();BlackLine.enabled = false;
		Edgefilter = gameObject.AddComponent<CameraFilterPack_Edge_Edge_filter>();Edgefilter.enabled = false;
		Golden = gameObject.AddComponent<CameraFilterPack_Edge_Golden>();Golden.enabled = false;
		Neon = gameObject.AddComponent<CameraFilterPack_Edge_Neon>();Neon.enabled = false;
		Sigmoid = gameObject.AddComponent<CameraFilterPack_Edge_Sigmoid>();Sigmoid.enabled = false;
		Sobel = gameObject.AddComponent<CameraFilterPack_Edge_Sobel>();Sobel.enabled = false;
		Rotation = gameObject.AddComponent<CameraFilterPack_EXTRA_Rotation>();Rotation.enabled = false;
		SHOWFPS = gameObject.AddComponent<CameraFilterPack_EXTRA_SHOWFPS>();SHOWFPS.enabled = false;
		EyesVision1 = gameObject.AddComponent<CameraFilterPack_EyesVision_1>();EyesVision1.enabled = false;
		EyesVision2 = gameObject.AddComponent<CameraFilterPack_EyesVision_2>();EyesVision2.enabled = false;
		ColorPerfection = gameObject.AddComponent<CameraFilterPack_Film_ColorPerfection>();ColorPerfection.enabled = false;
		Grain = gameObject.AddComponent<CameraFilterPack_Film_Grain>();Grain.enabled = false;
		FlyVision = gameObject.AddComponent<CameraFilterPack_Fly_Vision>();FlyVision.enabled = false;
		FX8bits = gameObject.AddComponent<CameraFilterPack_FX_8bits>();FX8bits.enabled = false;
		FX8bitsgb = gameObject.AddComponent<CameraFilterPack_FX_8bits_gb>();FX8bitsgb.enabled = false;
		Ascii = gameObject.AddComponent<CameraFilterPack_FX_Ascii>();Ascii.enabled = false;
		DarkMatter = gameObject.AddComponent<CameraFilterPack_FX_DarkMatter>();DarkMatter.enabled = false;
		DigitalMatrix = gameObject.AddComponent<CameraFilterPack_FX_DigitalMatrix>();DigitalMatrix.enabled = false;
		DigitalMatrixDistortion = gameObject.AddComponent<CameraFilterPack_FX_DigitalMatrixDistortion>();DigitalMatrixDistortion.enabled = false;
		DotCircle = gameObject.AddComponent<CameraFilterPack_FX_Dot_Circle>();DotCircle.enabled = false;
		Drunk = gameObject.AddComponent<CameraFilterPack_FX_Drunk>();Drunk.enabled = false;
		Drunk2 = gameObject.AddComponent<CameraFilterPack_FX_Drunk2>();Drunk2.enabled = false;
		EarthQuake = gameObject.AddComponent<CameraFilterPack_FX_EarthQuake>();EarthQuake.enabled = false;
		Funk = gameObject.AddComponent<CameraFilterPack_FX_Funk>();Funk.enabled = false;
		Glitch1 = gameObject.AddComponent<CameraFilterPack_FX_Glitch1>();Glitch1.enabled = false;
		Glitch2 = gameObject.AddComponent<CameraFilterPack_FX_Glitch2>();Glitch2.enabled = false;
		Glitch3 = gameObject.AddComponent<CameraFilterPack_FX_Glitch3>();Glitch3.enabled = false;
		Grid = gameObject.AddComponent<CameraFilterPack_FX_Grid>();Grid.enabled = false;
		Hexagon = gameObject.AddComponent<CameraFilterPack_FX_Hexagon>();Hexagon.enabled = false;
		HexagonBlack = gameObject.AddComponent<CameraFilterPack_FX_Hexagon_Black>();HexagonBlack.enabled = false;
		Hypno = gameObject.AddComponent<CameraFilterPack_FX_Hypno>();Hypno.enabled = false;
		InverChromiLum = gameObject.AddComponent<CameraFilterPack_FX_InverChromiLum>();InverChromiLum.enabled = false;
		FXMirror = gameObject.AddComponent<CameraFilterPack_FX_Mirror>();FXMirror.enabled = false;
		FXPlasma = gameObject.AddComponent<CameraFilterPack_FX_Plasma>();FXPlasma.enabled = false;
		FXPsycho = gameObject.AddComponent<CameraFilterPack_FX_Psycho>();FXPsycho.enabled = false;
		Scan = gameObject.AddComponent<CameraFilterPack_FX_Scan>();Scan.enabled = false;
		Screens = gameObject.AddComponent<CameraFilterPack_FX_Screens>();Screens.enabled = false;
		Spot = gameObject.AddComponent<CameraFilterPack_FX_Spot>();Spot.enabled = false;
		superDot = gameObject.AddComponent<CameraFilterPack_FX_superDot>();superDot.enabled = false;
		ZebraColor = gameObject.AddComponent<CameraFilterPack_FX_ZebraColor>();ZebraColor.enabled = false;
		GlassesOn = gameObject.AddComponent<CameraFilterPack_Glasses_On>();GlassesOn.enabled = false;
		GlassesOn2 = gameObject.AddComponent<CameraFilterPack_Glasses_On_2>();GlassesOn2.enabled = false;
		GlassesOn3 = gameObject.AddComponent<CameraFilterPack_Glasses_On_3>();GlassesOn3.enabled = false;
		GlassesOn4 = gameObject.AddComponent<CameraFilterPack_Glasses_On_4>();GlassesOn4.enabled = false;
		GlassesOn5 = gameObject.AddComponent<CameraFilterPack_Glasses_On_5>();GlassesOn5.enabled = false;
		GlassesOn6 = gameObject.AddComponent<CameraFilterPack_Glasses_On_6>();GlassesOn6.enabled = false;
		Mozaic = gameObject.AddComponent<CameraFilterPack_Glitch_Mozaic>();Mozaic.enabled = false;
		Glow = gameObject.AddComponent<CameraFilterPack_Glow_Glow>();Glow.enabled = false;
		GlowColor = gameObject.AddComponent<CameraFilterPack_Glow_Glow_Color>();GlowColor.enabled = false;
		Ansi = gameObject.AddComponent<CameraFilterPack_Gradients_Ansi>();Ansi.enabled = false;
		Desert = gameObject.AddComponent<CameraFilterPack_Gradients_Desert>();Desert.enabled = false;
		ElectricGradient = gameObject.AddComponent<CameraFilterPack_Gradients_ElectricGradient>();ElectricGradient.enabled = false;
		FireGradient = gameObject.AddComponent<CameraFilterPack_Gradients_FireGradient>();FireGradient.enabled = false;
		GradientsHue = gameObject.AddComponent<CameraFilterPack_Gradients_Hue>();GradientsHue.enabled = false;
		NeonGradient = gameObject.AddComponent<CameraFilterPack_Gradients_NeonGradient>();NeonGradient.enabled = false;
		GradientsRainbow = gameObject.AddComponent<CameraFilterPack_Gradients_Rainbow>();GradientsRainbow.enabled = false;
		Stripe = gameObject.AddComponent<CameraFilterPack_Gradients_Stripe>();Stripe.enabled = false;
		Tech = gameObject.AddComponent<CameraFilterPack_Gradients_Tech>();Tech.enabled = false;
		Therma = gameObject.AddComponent<CameraFilterPack_Gradients_Therma>();Therma.enabled = false;
		LightRainbow = gameObject.AddComponent<CameraFilterPack_Light_Rainbow>();LightRainbow.enabled = false;
		LightRainbow2 = gameObject.AddComponent<CameraFilterPack_Light_Rainbow2>();LightRainbow2.enabled = false;
		Water = gameObject.AddComponent<CameraFilterPack_Light_Water>();Water.enabled = false;
		Water2 = gameObject.AddComponent<CameraFilterPack_Light_Water2>();Water2.enabled = false;
		Lut = gameObject.AddComponent<CameraFilterPack_Lut_2_Lut>();Lut.enabled = false;
		LutExtra = gameObject.AddComponent<CameraFilterPack_Lut_2_Lut_Extra>();LutExtra.enabled = false;
		Mask = gameObject.AddComponent<CameraFilterPack_Lut_Mask>();Mask.enabled = false;
		PlayWith = gameObject.AddComponent<CameraFilterPack_Lut_PlayWith>();PlayWith.enabled = false;
		Plus = gameObject.AddComponent<CameraFilterPack_Lut_Plus>();Plus.enabled = false;
		LutSimple = gameObject.AddComponent<CameraFilterPack_Lut_Simple>();LutSimple.enabled = false;
		TestMode = gameObject.AddComponent<CameraFilterPack_Lut_TestMode>();TestMode.enabled = false;
		NewGlitch1 = gameObject.AddComponent<CameraFilterPack_NewGlitch1>();NewGlitch1.enabled = false;
		NewGlitch2 = gameObject.AddComponent<CameraFilterPack_NewGlitch2>();NewGlitch2.enabled = false;
		NewGlitch3 = gameObject.AddComponent<CameraFilterPack_NewGlitch3>();NewGlitch3.enabled = false;
		NewGlitch4 = gameObject.AddComponent<CameraFilterPack_NewGlitch4>();NewGlitch4.enabled = false;
		NewGlitch5 = gameObject.AddComponent<CameraFilterPack_NewGlitch5>();NewGlitch5.enabled = false;
		NewGlitch6 = gameObject.AddComponent<CameraFilterPack_NewGlitch6>();NewGlitch6.enabled = false;
		NewGlitch7 = gameObject.AddComponent<CameraFilterPack_NewGlitch7>();NewGlitch7.enabled = false;
		NightVisionFX = gameObject.AddComponent<CameraFilterPack_NightVisionFX>();NightVisionFX.enabled = false;
		NightVision4 = gameObject.AddComponent<CameraFilterPack_NightVision_4>();NightVision4.enabled = false;
		TV = gameObject.AddComponent<CameraFilterPack_Noise_TV>();TV.enabled = false;
		TV2 = gameObject.AddComponent<CameraFilterPack_Noise_TV_2>();TV2.enabled = false;
		TV3 = gameObject.AddComponent<CameraFilterPack_Noise_TV_3>();TV3.enabled = false;
		NightVision1 = gameObject.AddComponent<CameraFilterPack_Oculus_NightVision1>();NightVision1.enabled = false;
		NightVision2 = gameObject.AddComponent<CameraFilterPack_Oculus_NightVision2>();NightVision2.enabled = false;
		NightVision3 = gameObject.AddComponent<CameraFilterPack_Oculus_NightVision3>();NightVision3.enabled = false;
		NightVision5 = gameObject.AddComponent<CameraFilterPack_Oculus_NightVision5>();NightVision5.enabled = false;
		ThermaVision = gameObject.AddComponent<CameraFilterPack_Oculus_ThermaVision>();ThermaVision.enabled = false;
		Cutting1 = gameObject.AddComponent<CameraFilterPack_OldFilm_Cutting1>();Cutting1.enabled = false;
		Cutting2 = gameObject.AddComponent<CameraFilterPack_OldFilm_Cutting2>();Cutting2.enabled = false;
		DeepOilPaintHQ = gameObject.AddComponent<CameraFilterPack_Pixelisation_DeepOilPaintHQ>();DeepOilPaintHQ.enabled = false;
		Dot = gameObject.AddComponent<CameraFilterPack_Pixelisation_Dot>();Dot.enabled = false;
		OilPaint = gameObject.AddComponent<CameraFilterPack_Pixelisation_OilPaint>();OilPaint.enabled = false;
		OilPaintHQ = gameObject.AddComponent<CameraFilterPack_Pixelisation_OilPaintHQ>();OilPaintHQ.enabled = false;
		Sweater = gameObject.AddComponent<CameraFilterPack_Pixelisation_Sweater>();Sweater.enabled = false;
		Pixelisation = gameObject.AddComponent<CameraFilterPack_Pixel_Pixelisation>();Pixelisation.enabled = false;
		RainFX = gameObject.AddComponent<CameraFilterPack_Rain_RainFX>();RainFX.enabled = false;
		RealVHS = gameObject.AddComponent<CameraFilterPack_Real_VHS>();RealVHS.enabled = false;
		Loading = gameObject.AddComponent<CameraFilterPack_Retro_Loading>();Loading.enabled = false;
		Sharpen = gameObject.AddComponent<CameraFilterPack_Sharpen_Sharpen>();Sharpen.enabled = false;
		Bubble = gameObject.AddComponent<CameraFilterPack_Special_Bubble>();Bubble.enabled = false;
		TV50 = gameObject.AddComponent<CameraFilterPack_TV_50>();TV50.enabled = false;
		TV80 = gameObject.AddComponent<CameraFilterPack_TV_80>();TV80.enabled = false;
		ARCADE = gameObject.AddComponent<CameraFilterPack_TV_ARCADE>();ARCADE.enabled = false;
		ARCADE2 = gameObject.AddComponent<CameraFilterPack_TV_ARCADE_2>();ARCADE2.enabled = false;
		ARCADEFast = gameObject.AddComponent<CameraFilterPack_TV_ARCADE_Fast>();ARCADEFast.enabled = false;
		Artefact = gameObject.AddComponent<CameraFilterPack_TV_Artefact>();Artefact.enabled = false;
		BrokenGlass = gameObject.AddComponent<CameraFilterPack_TV_BrokenGlass>();BrokenGlass.enabled = false;
		BrokenGlass2 = gameObject.AddComponent<CameraFilterPack_TV_BrokenGlass2>();BrokenGlass2.enabled = false;
		Chromatical = gameObject.AddComponent<CameraFilterPack_TV_Chromatical>();Chromatical.enabled = false;
		Chromatical2 = gameObject.AddComponent<CameraFilterPack_TV_Chromatical2>();Chromatical2.enabled = false;
		CompressionFX = gameObject.AddComponent<CameraFilterPack_TV_CompressionFX>();CompressionFX.enabled = false;
		Distorted = gameObject.AddComponent<CameraFilterPack_TV_Distorted>();Distorted.enabled = false;
		Horror = gameObject.AddComponent<CameraFilterPack_TV_Horror>();Horror.enabled = false;
		LED = gameObject.AddComponent<CameraFilterPack_TV_LED>();LED.enabled = false;
		MovieNoise = gameObject.AddComponent<CameraFilterPack_TV_MovieNoise>();MovieNoise.enabled = false;
		TVNoise = gameObject.AddComponent<CameraFilterPack_TV_Noise>();TVNoise.enabled = false;
		Old = gameObject.AddComponent<CameraFilterPack_TV_Old>();Old.enabled = false;
		OldMovie = gameObject.AddComponent<CameraFilterPack_TV_Old_Movie>();OldMovie.enabled = false;
		OldMovie2 = gameObject.AddComponent<CameraFilterPack_TV_Old_Movie_2>();OldMovie2.enabled = false;
		PlanetMars = gameObject.AddComponent<CameraFilterPack_TV_PlanetMars>();PlanetMars.enabled = false;
		Posterize = gameObject.AddComponent<CameraFilterPack_TV_Posterize>();Posterize.enabled = false;
		TVRgb = gameObject.AddComponent<CameraFilterPack_TV_Rgb>();TVRgb.enabled = false;
		Tiles = gameObject.AddComponent<CameraFilterPack_TV_Tiles>();Tiles.enabled = false;
		Vcr = gameObject.AddComponent<CameraFilterPack_TV_Vcr>();Vcr.enabled = false;
		TVVHS = gameObject.AddComponent<CameraFilterPack_TV_VHS>();TVVHS.enabled = false;
		VHSRewind = gameObject.AddComponent<CameraFilterPack_TV_VHS_Rewind>();VHSRewind.enabled = false;
		Video3D = gameObject.AddComponent<CameraFilterPack_TV_Video3D>();Video3D.enabled = false;
		Videoflip = gameObject.AddComponent<CameraFilterPack_TV_Videoflip>();Videoflip.enabled = false;
		Vignetting = gameObject.AddComponent<CameraFilterPack_TV_Vignetting>();Vignetting.enabled = false;
		Vintage = gameObject.AddComponent<CameraFilterPack_TV_Vintage>();Vintage.enabled = false;
		WideScreenCircle = gameObject.AddComponent<CameraFilterPack_TV_WideScreenCircle>();WideScreenCircle.enabled = false;
		WideScreenHorizontal = gameObject.AddComponent<CameraFilterPack_TV_WideScreenHorizontal>();WideScreenHorizontal.enabled = false;
		WideScreenHV = gameObject.AddComponent<CameraFilterPack_TV_WideScreenHV>();WideScreenHV.enabled = false;
		WideScreenVertical = gameObject.AddComponent<CameraFilterPack_TV_WideScreenVertical>();WideScreenVertical.enabled = false;
		Tracking = gameObject.AddComponent<CameraFilterPack_VHS_Tracking>();Tracking.enabled = false;
		Aura = gameObject.AddComponent<CameraFilterPack_Vision_Aura>();Aura.enabled = false;
		AuraDistortion = gameObject.AddComponent<CameraFilterPack_Vision_AuraDistortion>();AuraDistortion.enabled = false;
		VisionBlood = gameObject.AddComponent<CameraFilterPack_Vision_Blood>();VisionBlood.enabled = false;
		VisionBloodFast = gameObject.AddComponent<CameraFilterPack_Vision_Blood_Fast>();VisionBloodFast.enabled = false;
		Crystal = gameObject.AddComponent<CameraFilterPack_Vision_Crystal>();Crystal.enabled = false;
		Drost = gameObject.AddComponent<CameraFilterPack_Vision_Drost>();Drost.enabled = false;
		VisionHellBlood = gameObject.AddComponent<CameraFilterPack_Vision_Hell_Blood>();VisionHellBlood.enabled = false;
		VisionPlasma = gameObject.AddComponent<CameraFilterPack_Vision_Plasma>();VisionPlasma.enabled = false;
		VisionPsycho = gameObject.AddComponent<CameraFilterPack_Vision_Psycho>();VisionPsycho.enabled = false;
		VisionRainbow = gameObject.AddComponent<CameraFilterPack_Vision_Rainbow>();VisionRainbow.enabled = false;
		SniperScore = gameObject.AddComponent<CameraFilterPack_Vision_SniperScore>();SniperScore.enabled = false;
		Tunnel = gameObject.AddComponent<CameraFilterPack_Vision_Tunnel>();Tunnel.enabled = false;
		Warp = gameObject.AddComponent<CameraFilterPack_Vision_Warp>();Warp.enabled = false;
		Warp2 = gameObject.AddComponent<CameraFilterPack_Vision_Warp2>();Warp2.enabled = false;

		ScanScene.AutoAnimatedNear = true;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyDown("z"))
			{
				FilterID -= 10;
				UpdateFilter();
			}

			if (Input.GetKeyDown("x"))
			{
				FilterID += 10;
				UpdateFilter();
			}
		}
		else
		{
			if (Input.GetKeyDown("z"))
			{
				FilterID--;
				UpdateFilter();
			}

			if (Input.GetKeyDown("x"))
			{
				FilterID++;
				UpdateFilter();
			}
		}
			
		if (DisplayTimer < 2)
		{
			DisplayTimer += Time.deltaTime;

			NameLabel.transform.localPosition = Vector3.Lerp (NameLabel.transform.localPosition, new Vector3 (
				1245,
				0,
				0),
				Time.deltaTime * 10);
		}
		else
		{
			Speed += Time.deltaTime * 10;

			NameLabel.transform.localPosition = Vector3.MoveTowards(NameLabel.transform.localPosition, Vector3.zero, Speed);
		}
	}

	void UpdateFilter()
	{
		NameLabel.transform.localPosition = Vector3.zero;
		DisplayTimer = 0;
		Speed = 0;

		Anomaly.enabled = false;
		Binary.enabled = false;
		BlackHole3D.enabled = false;
		Computer.enabled = false;
		Distortion.enabled = false;
		FogSmoke.enabled = false;
		Ghost.enabled = false;
		Inverse.enabled = false;
		Matrix.enabled = false;
		Mirror3D.enabled = false;
		Myst.enabled = false;
		ScanScene.enabled = false;
		Shield.enabled = false;
		Snow.enabled = false;
		AAABlood.enabled = false;
		AAABloodOnScreen.enabled = false;
		AAABloodHit.enabled = false;
		AAABloodPlus.enabled = false;
		SuperComputer.enabled = false;
		SuperHexagon.enabled = false;
		WaterDrop.enabled = false;
		WaterDropPro.enabled = false;
		AlienVision.enabled = false;
		FXAA.enabled = false;
		Fog.enabled = false;
		Rain.enabled = false;
		RainPro.enabled = false;
		RainPro3D.enabled = false;
		Snow8bits.enabled = false;
		Blend.enabled = false;
		BlueScreen.enabled = false;
		Color.enabled = false;
		ColorBurn.enabled = false;
		ColorDodge.enabled = false;
		ColorKey.enabled = false;
		Darken.enabled = false;
		DarkerColor.enabled = false;
		Difference.enabled = false;
		Divide.enabled = false;
		Exclusion.enabled = false;
		GreenScreen.enabled = false;
		HardLight.enabled = false;
		HardMix.enabled = false;
		Blend2CameraHue.enabled = false;
		Lighten.enabled = false;
		LighterColor.enabled = false;
		LinearBurn.enabled = false;
		LinearDodge.enabled = false;
		LinearLight.enabled = false;
		Luminosity.enabled = false;
		Multiply.enabled = false;
		Overlay.enabled = false;
		PhotoshopFilters.enabled = false;
		PinLight.enabled = false;
		Saturation.enabled = false;
		Screen.enabled = false;
		SoftLight.enabled = false;
		SplitScreen.enabled = false;
		SplitScreen3D.enabled = false;
		Subtract.enabled = false;
		VividLight.enabled = false;
		Blizzard.enabled = false;
		Bloom.enabled = false;
		BlurHole.enabled = false;
		Blurry.enabled = false;
		Dithering2x2.enabled = false;
		DitherOffset.enabled = false;
		Focus.enabled = false;
		GaussianBlur.enabled = false;
		Movie.enabled = false;
		BlurNoise.enabled = false;
		Radial.enabled = false;
		RadialFast.enabled = false;
		Regular.enabled = false;
		Steam.enabled = false;
		TiltShift.enabled = false;
		TiltShiftHole.enabled = false;
		TiltShiftV.enabled = false;
		BrokenScreen.enabled = false;
		BrokenSimple.enabled = false;
		Spliter.enabled = false;
		ThermalVision.enabled = false;
		AdjustColorRGB.enabled = false;
		AdjustFullColors.enabled = false;
		AdjustPreFilters.enabled = false;
		BleachBypass.enabled = false;
		Brightness.enabled = false;
		DarkColor.enabled = false;
		HSV.enabled = false;
		HUERotate.enabled = false;
		NewPosterize.enabled = false;
		RgbClamp.enabled = false;
		Threshold.enabled = false;
		AdjustLevels.enabled = false;
		BrightContrastSaturation.enabled = false;
		ChromaticAberration.enabled = false;
		ChromaticPlus.enabled = false;
		Contrast.enabled = false;
		GrayScale.enabled = false;
		Invert.enabled = false;
		ColorNoise.enabled = false;
		ColorRGB.enabled = false;
		Sepia.enabled = false;
		Switching.enabled = false;
		YUV.enabled = false;
		Normal.enabled = false;
		Aspiration.enabled = false;
		BigFace.enabled = false;
		BlackHole.enabled = false;
		Dissipation.enabled = false;
		Dream.enabled = false;
		Dream2.enabled = false;
		FishEye.enabled = false;
		Flag.enabled = false;
		Flush.enabled = false;
		HalfSphere.enabled = false;
		Heat.enabled = false;
		Lens.enabled = false;
		DistortionNoise.enabled = false;
		ShockWave.enabled = false;
		ShockWaveManual.enabled = false;
		Twist.enabled = false;
		TwistSquare.enabled = false;
		DistortionWaterDrop.enabled = false;
		WaveHorizontal.enabled = false;
		BluePrint.enabled = false;
		CellShading.enabled = false;
		CellShading2.enabled = false;
		Comics.enabled = false;
		Crosshatch.enabled = false;
		Curve.enabled = false;
		EnhancedComics.enabled = false;
		Halftone.enabled = false;
		Laplacian.enabled = false;
		Lines.enabled = false;
		Manga.enabled = false;
		Manga2.enabled = false;
		Manga3.enabled = false;
		Manga4.enabled = false;
		Manga5.enabled = false;
		MangaColor.enabled = false;
		MangaFlash.enabled = false;
		MangaFlashWhite.enabled = false;
		MangaFlashColor.enabled = false;
		NewCellShading.enabled = false;
		Paper.enabled = false;
		Paper2.enabled = false;
		Paper3.enabled = false;
		Toon.enabled = false;
		BlackLine.enabled = false;
		Edgefilter.enabled = false;
		Golden.enabled = false;
		Neon.enabled = false;
		Sigmoid.enabled = false;
		Sobel.enabled = false;
		Rotation.enabled = false;
		SHOWFPS.enabled = false;
		EyesVision1.enabled = false;
		EyesVision2.enabled = false;
		ColorPerfection.enabled = false;
		Grain.enabled = false;
		FlyVision.enabled = false;
		FX8bits.enabled = false;
		FX8bitsgb.enabled = false;
		Ascii.enabled = false;
		DarkMatter.enabled = false;
		DigitalMatrix.enabled = false;
		DigitalMatrixDistortion.enabled = false;
		DotCircle.enabled = false;
		Drunk.enabled = false;
		Drunk2.enabled = false;
		EarthQuake.enabled = false;
		Funk.enabled = false;
		Glitch1.enabled = false;
		Glitch2.enabled = false;
		Glitch3.enabled = false;
		Grid.enabled = false;
		Hexagon.enabled = false;
		HexagonBlack.enabled = false;
		Hypno.enabled = false;
		InverChromiLum.enabled = false;
		FXMirror.enabled = false;
		FXPlasma.enabled = false;
		FXPsycho.enabled = false;
		Scan.enabled = false;
		Screens.enabled = false;
		Spot.enabled = false;
		superDot.enabled = false;
		ZebraColor.enabled = false;
		GlassesOn.enabled = false;
		GlassesOn2.enabled = false;
		GlassesOn3.enabled = false;
		GlassesOn4.enabled = false;
		GlassesOn5.enabled = false;
		GlassesOn6.enabled = false;
		Mozaic.enabled = false;
		Glow.enabled = false;
		GlowColor.enabled = false;
		Ansi.enabled = false;
		Desert.enabled = false;
		ElectricGradient.enabled = false;
		FireGradient.enabled = false;
		GradientsHue.enabled = false;
		NeonGradient.enabled = false;
		GradientsRainbow.enabled = false;
		Stripe.enabled = false;
		Tech.enabled = false;
		Therma.enabled = false;
		LightRainbow.enabled = false;
		LightRainbow2.enabled = false;
		Water.enabled = false;
		Water2.enabled = false;
		Lut.enabled = false;
		LutExtra.enabled = false;
		Mask.enabled = false;
		PlayWith.enabled = false;
		Plus.enabled = false;
		LutSimple.enabled = false;
		TestMode.enabled = false;
		NewGlitch1.enabled = false;
		NewGlitch2.enabled = false;
		NewGlitch3.enabled = false;
		NewGlitch4.enabled = false;
		NewGlitch5.enabled = false;
		NewGlitch6.enabled = false;
		NewGlitch7.enabled = false;
		NightVisionFX.enabled = false;
		NightVision4.enabled = false;
		TV.enabled = false;
		TV2.enabled = false;
		TV3.enabled = false;
		NightVision1.enabled = false;
		NightVision2.enabled = false;
		NightVision3.enabled = false;
		NightVision5.enabled = false;
		ThermaVision.enabled = false;
		Cutting1.enabled = false;
		Cutting2.enabled = false;
		DeepOilPaintHQ.enabled = false;
		Dot.enabled = false;
		OilPaint.enabled = false;
		OilPaintHQ.enabled = false;
		Sweater.enabled = false;
		Pixelisation.enabled = false;
		RainFX.enabled = false;
		RealVHS.enabled = false;
		Loading.enabled = false;
		Sharpen.enabled = false;
		Bubble.enabled = false;
		TV50.enabled = false;
		TV80.enabled = false;
		ARCADE.enabled = false;
		ARCADE2.enabled = false;
		ARCADEFast.enabled = false;
		Artefact.enabled = false;
		BrokenGlass.enabled = false;
		BrokenGlass2.enabled = false;
		Chromatical.enabled = false;
		Chromatical2.enabled = false;
		CompressionFX.enabled = false;
		Distorted.enabled = false;
		Horror.enabled = false;
		LED.enabled = false;
		MovieNoise.enabled = false;
		TVNoise.enabled = false;
		Old.enabled = false;
		OldMovie.enabled = false;
		OldMovie2.enabled = false;
		PlanetMars.enabled = false;
		Posterize.enabled = false;
		TVRgb.enabled = false;
		Tiles.enabled = false;
		Vcr.enabled = false;
		TVVHS.enabled = false;
		VHSRewind.enabled = false;
		Video3D.enabled = false;
		Videoflip.enabled = false;
		Vignetting.enabled = false;
		Vintage.enabled = false;
		WideScreenCircle.enabled = false;
		WideScreenHorizontal.enabled = false;
		WideScreenHV.enabled = false;
		WideScreenVertical.enabled = false;
		Tracking.enabled = false;
		Aura.enabled = false;
		AuraDistortion.enabled = false;
		VisionBlood.enabled = false;
		VisionBloodFast.enabled = false;
		Crystal.enabled = false;
		Drost.enabled = false;
		VisionHellBlood.enabled = false;
		VisionPlasma.enabled = false;
		VisionPsycho.enabled = false;
		VisionRainbow.enabled = false;
		SniperScore.enabled = false;
		Tunnel.enabled = false;
		Warp.enabled = false;
		Warp2.enabled = false;

		//Debug.Log ("FilterID is: " + FilterID);

		if (FilterID > FilterMax)
		{
			FilterID = 0;
		}

		if (FilterID < 0)
		{
			FilterID = FilterMax;
		}
			
		while (FilterSkips[FilterID])
		{
			if (Input.GetKeyDown("z"))
			{
				FilterID--;
			}
			else
			{
				FilterID++;
			}
		}

		NameLabel.text = "#" + FilterID + " - " + FilterNames[FilterID];

		switch (FilterID)
		{
			case 1:
				Anomaly.enabled = true;
				break;

			case 2:
				Binary.enabled = true;
				break;

			case 3:
				BlackHole3D.enabled = true;
				break;

			case 4:
				Computer.enabled = true;
				break;

			case 5:
				Distortion.enabled = true;
				break;
				
			case 6:
				FogSmoke.enabled = true;
				break;

			case 7:
				Ghost.enabled = true;
				break;

			case 8:
				Inverse.enabled = true;
				break;

			case 9:
				Matrix.enabled = true;
				break;

			case 10:
				Mirror3D.enabled = true;
				break;
				
			case 11:
				Myst.enabled = true;
				break;

			case 12:
				ScanScene.enabled = true;
				break;

			case 13:
				Shield.enabled = true;
				break;

			case 14:
				Snow.enabled = true;
				break;

			case 15:
				AAABlood.enabled = true;
				break;
				
			case 16:
				AAABloodOnScreen.enabled = true;
				break;
				
			case 17:
				AAABloodHit.enabled = true;
				break;
				
			case 18:
				AAABloodPlus.enabled = true;
				break;
				
			case 19:
				SuperComputer.enabled = true;
				break;
				
			case 20:
				SuperHexagon.enabled = true;
				break;
				
			case 21:
				WaterDrop.enabled = true;
				break;
				
			case 22:
				WaterDropPro.enabled = true;
				break;
				
			case 23:
				AlienVision.enabled = true;
				break;
				
			case 24:
				FXAA.enabled = true;
				break;
				
			case 25:
				Fog.enabled = true;
				break;
				
			case 26:
				Rain.enabled = true;
				break;
				
			case 27:
				RainPro.enabled = true;
				break;
				
			case 28:
				RainPro3D.enabled = true;
				break;
				
			case 29:
				Snow8bits.enabled = true;
				break;
				
			case 30:
				Blend.enabled = true;
				break;
				
			case 31:
				BlueScreen.enabled = true;
				break;
				
			case 32:
				Color.enabled = true;
				break;
				
			case 33:
				ColorBurn.enabled = true;
				break;
				
			case 34:
				ColorDodge.enabled = true;
				break;
				
			case 35:
				ColorKey.enabled = true;
				break;
				
			case 36:
				Darken.enabled = true;
				break;
				
			case 37:
				DarkerColor.enabled = true;
				break;
				
			case 38:
				Difference.enabled = true;
				break;
				
			case 39:
				Divide.enabled = true;
				break;
				
			case 40:
				Exclusion.enabled = true;
				break;
				
			case 41:
				GreenScreen.enabled = true;
				break;
				
			case 42:
				HardLight.enabled = true;
				break;
				
			case 43:
				HardMix.enabled = true;
				break;
				
			case 44:
				Blend2CameraHue.enabled = true;
				break;
				
			case 45:
				Lighten.enabled = true;
				break;
				
			case 46:
				LighterColor.enabled = true;
				break;
				
			case 47:
				LinearBurn.enabled = true;
				break;
				
			case 48:
				LinearDodge.enabled = true;
				break;
				
			case 49:
				LinearLight.enabled = true;
				break;
				
			case 50:
				Luminosity.enabled = true;
				break;
				
			case 51:
				Multiply.enabled = true;
				break;
				
			case 52:
				Overlay.enabled = true;
				break;
				
			case 53:
				PhotoshopFilters.enabled = true;
				break;
				
			case 54:
				PinLight.enabled = true;
				break;
				
			case 55:
				Saturation.enabled = true;
				break;
				
			case 56:
				Screen.enabled = true;
				break;
				
			case 57:
				SoftLight.enabled = true;
				break;
				
			case 58:
				SplitScreen.enabled = true;
				break;
				
			case 59:
				SplitScreen3D.enabled = true;
				break;
				
			case 60:
				Subtract.enabled = true;
				break;
								
			case 61:
				VividLight.enabled = true;
				break;
				
			case 62:
				Blizzard.enabled = true;
				break;
				
			case 63:
				Bloom.enabled = true;
				break;
				
			case 64:
				BlurHole.enabled = true;
				break;
				
			case 65:
				Blurry.enabled = true;
				break;
				
			case 66:
				Dithering2x2.enabled = true;
				break;
				
			case 67:
				DitherOffset.enabled = true;
				break;
				
			case 68:
				Focus.enabled = true;
				break;
				
			case 69:
				GaussianBlur.enabled = true;
				break;
				
			case 70:
				Movie.enabled = true;
				break;
								
			case 71:
				BlurNoise.enabled = true;
				break;
				
			case 72:
				Radial.enabled = true;
				break;
				
			case 73:
				RadialFast.enabled = true;
				break;
				
			case 74:
				Regular.enabled = true;
				break;
				
			case 75:
				Steam.enabled = true;
				break;
				
			case 76:
				TiltShift.enabled = true;
				break;
				
			case 77:
				TiltShiftHole.enabled = true;
				break;
				
			case 78:
				TiltShiftV.enabled = true;
				break;
				
			case 79:
				BrokenScreen.enabled = true;
				break;
				
			case 80:
				BrokenSimple.enabled = true;
				break;
								
			case 81:
				Spliter.enabled = true;
				break;
				
			case 82:
				ThermalVision.enabled = true;
				break;
				
			case 83:
				AdjustColorRGB.enabled = true;
				break;
				
			case 84:
				AdjustFullColors.enabled = true;
				break;
				
			case 85:
				AdjustPreFilters.enabled = true;
				break;
				
			case 86:
				BleachBypass.enabled = true;
				break;
				
			case 87:
				Brightness.enabled = true;
				break;
				
			case 88:
				DarkColor.enabled = true;
				break;
				
			case 89:
				HSV.enabled = true;
				break;
				
			case 90:
				HUERotate.enabled = true;
				break;
								
			case 91:
				NewPosterize.enabled = true;
				break;
				
			case 92:
				RgbClamp.enabled = true;
				break;
				
			case 93:
				Threshold.enabled = true;
				break;
				
			case 94:
				AdjustLevels.enabled = true;
				break;
				
			case 95:
				BrightContrastSaturation.enabled = true;
				break;
				
			case 96:
				ChromaticAberration.enabled = true;
				break;
				
			case 97:
				ChromaticPlus.enabled = true;
				break;
				
			case 98:
				Contrast.enabled = true;
				break;
				
			case 99:
				GrayScale.enabled = true;
				break;
				
			case 100:
				Invert.enabled = true;
				break;
				
			case 101:
				ColorNoise.enabled = true;
				break;
				
			case 102:
				ColorRGB.enabled = true;
				break;
				
			case 103:
				Sepia.enabled = true;
				break;
				
			case 104:
				Switching.enabled = true;
				break;
				
			case 105:
				YUV.enabled = true;
				break;
				
			case 106:
				Normal.enabled = true;
				break;
				
			case 107:
				Aspiration.enabled = true;
				break;
				
			case 108:
				BigFace.enabled = true;
				break;
				
			case 109:
				BlackHole.enabled = true;
				break;
				
			case 110:
				Dissipation.enabled = true;
				break;
								
			case 111:
				Dream.enabled = true;
				break;
				
			case 112:
				Dream2.enabled = true;
				break;
				
			case 113:
				FishEye.enabled = true;
				break;
				
			case 114:
				Flag.enabled = true;
				break;
				
			case 115:
				Flush.enabled = true;
				break;
				
			case 116:
				HalfSphere.enabled = true;
				break;
				
			case 117:
				Heat.enabled = true;
				break;
				
			case 118:
				Lens.enabled = true;
				break;
				
			case 119:
				DistortionNoise.enabled = true;
				break;
				
			case 120:
				ShockWave.enabled = true;
				break;
								
			case 121:
				ShockWaveManual.enabled = true;
				break;
				
			case 122:
				Twist.enabled = true;
				break;
				
			case 123:
				TwistSquare.enabled = true;
				break;
				
			case 124:
				DistortionWaterDrop.enabled = true;
				break;
				
			case 125:
				WaveHorizontal.enabled = true;
				break;
				
			case 126:
				BluePrint.enabled = true;
				break;
				
			case 127:
				CellShading.enabled = true;
				break;
				
			case 128:
				CellShading2.enabled = true;
				break;
				
			case 129:
				Comics.enabled = true;
				break;
				
			case 130:
				Crosshatch.enabled = true;
				break;
								
			case 131:
				Curve.enabled = true;
				break;
				
			case 132:
				EnhancedComics.enabled = true;
				break;
				
			case 133:
				Halftone.enabled = true;
				break;
				
			case 134:
				Laplacian.enabled = true;
				break;
				
			case 135:
				Lines.enabled = true;
				break;
				
			case 136:
				Manga.enabled = true;
				break;
				
			case 137:
				Manga2.enabled = true;
				break;
				
			case 138:
				Manga3.enabled = true;
				break;
				
			case 139:
				Manga4.enabled = true;
				break;
				
			case 140:
				Manga5.enabled = true;
				break;
								
			case 141:
				MangaColor.enabled = true;
				break;
				
			case 142:
				MangaFlash.enabled = true;
				break;
				
			case 143:
				MangaFlashWhite.enabled = true;
				break;
				
			case 144:
				MangaFlashColor.enabled = true;
				break;
				
			case 145:
				NewCellShading.enabled = true;
				break;
				
			case 146:
				Paper.enabled = true;
				break;
				
			case 147:
				Paper2.enabled = true;
				break;
				
			case 148:
				Paper3.enabled = true;
				break;
				
			case 149:
				Toon.enabled = true;
				break;
				
			case 150:
				BlackLine.enabled = true;
				break;

			case 151:
				Edgefilter.enabled = true;
				break;
				
			case 152:
				Golden.enabled = true;
				break;
				
			case 153:
				Neon.enabled = true;
				break;
				
			case 154:
				Sigmoid.enabled = true;
				break;
				
			case 155:
				Sobel.enabled = true;
				break;
				
			case 156:
				Rotation.enabled = true;
				break;
				
			case 157:
				SHOWFPS.enabled = true;
				break;
				
			case 158:
				EyesVision1.enabled = true;
				break;
				
			case 159:
				EyesVision2.enabled = true;
				break;
				
			case 160:
				ColorPerfection.enabled = true;
				break;

			case 161:
				Grain.enabled = true;
				break;
				
			case 162:
				FlyVision.enabled = true;
				break;
				
			case 163:
				FX8bits.enabled = true;
				break;
				
			case 164:
				FX8bitsgb.enabled = true;
				break;
				
			case 165:
				Ascii.enabled = true;
				break;
				
			case 166:
				DarkMatter.enabled = true;
				break;
				
			case 167:
				DigitalMatrix.enabled = true;
				break;
				
			case 168:
				DigitalMatrixDistortion.enabled = true;
				break;
				
			case 169:
				DotCircle.enabled = true;
				break;
				
			case 170:
				Drunk.enabled = true;
				break;

			case 171:
				Drunk2.enabled = true;
				break;
				
			case 172:
				EarthQuake.enabled = true;
				break;
				
			case 173:
				Funk.enabled = true;
				break;
				
			case 174:
				Glitch1.enabled = true;
				break;
				
			case 175:
				Glitch2.enabled = true;
				break;
				
			case 176:
				Glitch3.enabled = true;
				break;
				
			case 177:
				Grid.enabled = true;
				break;
				
			case 178:
				Hexagon.enabled = true;
				break;
				
			case 179:
				HexagonBlack.enabled = true;
				break;
				
			case 180:
				Hypno.enabled = true;
				break;

			case 181:
				InverChromiLum.enabled = true;
				break;
				
			case 182:
				FXMirror.enabled = true;
				break;
				
			case 183:
				FXPlasma.enabled = true;
				break;
				
			case 184:
				FXPsycho.enabled = true;
				break;
				
			case 185:
				Scan.enabled = true;
				break;
				
			case 186:
				Screens.enabled = true;
				break;
				
			case 187:
				Spot.enabled = true;
				break;
				
			case 188:
				superDot.enabled = true;
				break;
				
			case 189:
				ZebraColor.enabled = true;
				break;
				
			case 190:
				GlassesOn.enabled = true;
				break;

			case 191:
				GlassesOn2.enabled = true;
				break;
				
			case 192:
				GlassesOn3.enabled = true;
				break;
				
			case 193:
				GlassesOn4.enabled = true;
				break;
				
			case 194:
				GlassesOn5.enabled = true;
				break;
				
			case 195:
				GlassesOn6.enabled = true;
				break;
				
			case 196:
				Mozaic.enabled = true;
				break;
				
			case 197:
				Glow.enabled = true;
				break;
				
			case 198:
				GlowColor.enabled = true;
				break;
				
			case 199:
				Ansi.enabled = true;
				break;
				
			case 200:
				Desert.enabled = true;
				break;

			case 201:
				ElectricGradient.enabled = true;
				break;
				
			case 202:
				FireGradient.enabled = true;
				break;
				
			case 203:
				GradientsHue.enabled = true;
				break;
				
			case 204:
				NeonGradient.enabled = true;
				break;
				
			case 205:
				GradientsRainbow.enabled = true;
				break;
				
			case 206:
				Stripe.enabled = true;
				break;
				
			case 207:
				Tech.enabled = true;
				break;
				
			case 208:
				Therma.enabled = true;
				break;
				
			case 209:
				LightRainbow.enabled = true;
				break;
				
			case 210:
				LightRainbow2.enabled = true;
				break;

			case 211:
				Water.enabled = true;
				break;
				
			case 212:
				Water2.enabled = true;
				break;
				
			case 213:
				Lut.enabled = true;
				break;
				
			case 214:
				LutExtra.enabled = true;
				break;
				
			case 215:
				Mask.enabled = true;
				break;
				
			case 216:
				PlayWith.enabled = true;
				break;
				
			case 217:
				Plus.enabled = true;
				break;
				
			case 218:
				LutSimple.enabled = true;
				break;
				
			case 219:
				TestMode.enabled = true;
				break;
				
			case 220:
				NewGlitch1.enabled = true;
				break;

			case 221:
				NewGlitch2.enabled = true;
				break;
				
			case 222:
				NewGlitch3.enabled = true;
				break;
				
			case 223:
				NewGlitch4.enabled = true;
				break;
				
			case 224:
				NewGlitch5.enabled = true;
				break;
				
			case 225:
				NewGlitch6.enabled = true;
				break;
				
			case 226:
				NewGlitch7.enabled = true;
				break;
				
			case 227:
				NightVisionFX.enabled = true;
				break;
				
			case 228:
				NightVision4.enabled = true;
				break;
				
			case 229:
				TV.enabled = true;
				break;
				
			case 230:
				TV2.enabled = true;
				break;

			case 231:
				TV3.enabled = true;
				break;
				
			case 232:
				NightVision1.enabled = true;
				break;
				
			case 233:
				NightVision2.enabled = true;
				break;
				
			case 234:
				NightVision3.enabled = true;
				break;
				
			case 235:
				NightVision5.enabled = true;
				break;
				
			case 236:
				ThermaVision.enabled = true;
				break;
				
			case 237:
				Cutting1.enabled = true;
				break;
				
			case 238:
				Cutting2.enabled = true;
				break;
				
			case 239:
				DeepOilPaintHQ.enabled = true;
				break;
				
			case 240:
				Dot.enabled = true;
				break;

			case 241:
				OilPaint.enabled = true;
				break;
				
			case 242:
				OilPaintHQ.enabled = true;
				break;
				
			case 243:
				Sweater.enabled = true;
				break;
				
			case 244:
				Pixelisation.enabled = true;
				break;
				
			case 245:
				RainFX.enabled = true;
				break;
				
			case 246:
				RealVHS.enabled = true;
				break;
				
			case 247:
				Loading.enabled = true;
				break;
				
			case 248:
				Sharpen.enabled = true;
				break;
				
			case 249:
				Bubble.enabled = true;
				break;
				
			case 250:
				TV50.enabled = true;
				break;

			case 251:
				TV80.enabled = true;
				break;
				
			case 252:
				ARCADE.enabled = true;
				break;
				
			case 253:
				ARCADE2.enabled = true;
				break;
				
			case 254:
				ARCADEFast.enabled = true;
				break;
				
			case 255:
				Artefact.enabled = true;
				break;
				
			case 256:
				BrokenGlass.enabled = true;
				break;
				
			case 257:
				BrokenGlass2.enabled = true;
				break;
				
			case 258:
				Chromatical.enabled = true;
				break;
				
			case 259:
				Chromatical2.enabled = true;
				break;
				
			case 260:
				CompressionFX.enabled = true;
				break;

			case 261:
				Distorted.enabled = true;
				break;
				
			case 262:
				Horror.enabled = true;
				break;
				
			case 263:
				LED.enabled = true;
				break;
				
			case 264:
				MovieNoise.enabled = true;
				break;
				
			case 265:
				TVNoise.enabled = true;
				break;
				
			case 266:
				Old.enabled = true;
				break;
				
			case 267:
				OldMovie.enabled = true;
				break;
				
			case 268:
				OldMovie2.enabled = true;
				break;
				
			case 269:
				PlanetMars.enabled = true;
				break;
				
			case 270:
				Posterize.enabled = true;
				break;

			case 271:
				TVRgb.enabled = true;
				break;
				
			case 272:
				Tiles.enabled = true;
				break;
				
			case 273:
				Vcr.enabled = true;
				break;
				
			case 274:
				TVVHS.enabled = true;
				break;
				
			case 275:
				VHSRewind.enabled = true;
				break;
				
			case 276:
				Video3D.enabled = true;
				break;
				
			case 277:
				Videoflip.enabled = true;
				break;
				
			case 278:
				Vignetting.enabled = true;
				break;
				
			case 279:
				Vintage.enabled = true;
				break;
				
			case 280:
				WideScreenCircle.enabled = true;
				break;

			case 281:
				WideScreenHorizontal.enabled = true;
				break;
				
			case 282:
				WideScreenHV.enabled = true;
				break;
				
			case 283:
				WideScreenVertical.enabled = true;
				break;
				
			case 284:
				Tracking.enabled = true;
				break;
				
			case 285:
				Aura.enabled = true;
				break;
				
			case 286:
				AuraDistortion.enabled = true;
				break;
				
			case 287:
				VisionBlood.enabled = true;
				break;
				
			case 288:
				VisionBloodFast.enabled = true;
				break;
				
			case 289:
				Crystal.enabled = true;
				break;
				
			case 290:
				Drost.enabled = true;
				break;

			case 291:
				VisionHellBlood.enabled = true;
				break;
				
			case 292:
				VisionPlasma.enabled = true;
				break;
				
			case 293:
				VisionPsycho.enabled = true;
				break;
				
			case 294:
				VisionRainbow.enabled = true;
				break;
				
			case 295:
				SniperScore.enabled = true;
				break;
				
			case 296:
				Tunnel.enabled = true;
				break;
				
			case 297:
				Warp.enabled = true;
				break;
				
			case 298:
				Warp2.enabled = true;
				break;

			default:
				break;
		}
	}
}