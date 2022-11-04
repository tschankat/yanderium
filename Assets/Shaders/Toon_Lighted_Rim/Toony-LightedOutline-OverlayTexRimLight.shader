Shader "Toon/Lighted Outline Overlay RimLight" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_OverlayTex ("Overlay Texture (RGBA)", 2D) = "white" {}
        _BlendAmount("Blend Amount", Range(0.0, 1.0)) = 0.0
        
        _RimLightIntencity ("RimLightIntencity", Range (0.0, 5)) = 1.3
		_RimCrisp ("RimCrisp", Range (0.0, .7)) = .3
        _RimAdditive ("RimAdditive", Range (0.0, .5)) = .15
		_RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1)
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
CGPROGRAM
#pragma surface surf ToonRamp

sampler2D _Ramp;

// custom lighting function that uses a texture ramp based
// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
{
	#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
	#endif
	
	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
	c.a = 0;
	return c;
}


sampler2D _MainTex;
float4 _Color;
sampler2D _OverlayTex;
fixed _BlendAmount;


half _RimLightIntencity;
half _RimCrisp;

half _RimAdditive;
half3 _RimColor;


struct Input {
	float2 uv_MainTex : TEXCOORD0;
    float2 uv_OverlayTex;
	float3 viewDir;
};

void surf (Input IN, inout SurfaceOutput o) {
	
	//_RimLightIntencity = 1.3;
	//_RimCrisp = .3;
	//_RimAdditive = .15;
	
	half fresnel = 1 - saturate(dot(normalize(IN.viewDir), normalize(o.Normal)) + _RimCrisp);
	fresnel = saturate(fresnel * _RimLightIntencity);

    fixed4 overlay = tex2D (_OverlayTex, IN.uv_OverlayTex);
	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	
	c = half4(lerp(c.rgb, overlay.rgb, overlay.a * _BlendAmount),0);
	
	half3 DiffRim = c.rgb + _RimAdditive * _RimColor;
	
	o.Albedo = lerp(c.rgb, DiffRim, fresnel);
	o.Alpha = c.a;
}
ENDCG
UsePass "Toon/Basic Outline/OUTLINE"
	} 
	Fallback "Toon/Lighted"
}
