Shader "Toon/Lighted RimLight" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		
		_RimLightIntencity ("RimLightIntencity", Range (0.0, 3)) = 1.0
		_RimCrisp ("RimCrisp", Range (0.0, .5)) = .1
		_RimAdditive ("RimAdditive", Range (0.0, .5)) = .2
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
	
	half d = dot (s.Normal, lightDir)*0.5 + 0.5 ;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
	c.a = 0;
	return c;
}


sampler2D _MainTex;
float4 _Color;

half _RimLightIntencity;
half _RimCrisp;

half _RimAdditive;
half3 _RimColor;

struct Input {
	float2 uv_MainTex : TEXCOORD0;
	float3 viewDir;
};

void surf (Input IN, inout SurfaceOutput o) {

	half fresnel = 1 - saturate(dot(normalize(IN.viewDir), normalize(o.Normal)) + _RimCrisp);
	fresnel = saturate(fresnel * _RimLightIntencity);
	
	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	//o.Albedo = c.rgb;
	
	half3 DiffRim = c.rgb + _RimAdditive * _RimColor;
	
	o.Albedo = lerp(c.rgb, DiffRim, fresnel);
	o.Alpha = c.a;
}
ENDCG

	} 

	Fallback "Diffuse"
}
