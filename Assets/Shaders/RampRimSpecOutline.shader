//If shit doesn't compile- import unity's toon shading shaders.
Shader "RampRimSpec Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_SpecColor("Spec Color", Color) = (0.5,0.5,0.5,1)
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Saturation ("Saturation", Range(0, 5)) = 1.0
		_HueShift ("Hue Shift", Range(0, 1)) = 0.0
		_Shininess ("Shininess", Range(0.01,5.0)) = .075
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "RampRimSpec/FORWARD"
		UsePass "Toon/Basic Outline/OUTLINE"
	} 
	
	Fallback "Toon/Lighted"
}
