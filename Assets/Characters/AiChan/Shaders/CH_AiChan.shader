Shader "Custom/CH_AiChan" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.0005, 0.002)) = .002
		
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		
CGPROGRAM
#pragma surface surf ToonRamp


struct ToonSurfaceOutput
{
	half3 Albedo;
	half3 Normal;
	half3 Emission;
	half3 Gloss;
	half Specular;
	half Alpha;
	half4 Color;
};


// custom lighting function that uses a texture ramp based
// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
inline half4 LightingToonRamp (ToonSurfaceOutput s, half3 lightDir, half atten)
{
	#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
	#endif
	
	float3 Diff = s.Albedo;
	
	half4 c;
	c.rgb = Diff * _LightColor0.rgb * .5 * atten;
	
	c.a = 0;
	return c;
}



sampler2D _MainTex;
float4 _Color;

struct Input {
	float2 uv_MainTex : TEXCOORD0;
	float3 viewDir;
};

void surf (Input IN, inout ToonSurfaceOutput o) {
	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	
	half3 normS = normalize(mul((float3x3)UNITY_MATRIX_MV, o.Normal));

	half3 Diff = c.rgb;
	half fresnel = dot(normalize(IN.viewDir), normalize(o.Normal));

	o.Albedo = Diff;
	//o.Albedo = frShadeLight;
	o.Alpha = c.a;
	
}
ENDCG
	UsePass "Toon/Basic Outline/OUTLINE"
	} 

	Fallback "Toon/Basic"
}
