Shader "Toon/Lighted Outline Overlay Multiple Textures EyesAnim" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_OverlayTex ("Overlay Texture (RGBA)", 2D) = "white" {}
        _BlendAmount("Blend Amount", Range(0.0, 1.0)) = 0.0
		_OverlayTex1 ("Overlay Texture 1 (RGBA)", 2D) = "white" {}
        _BlendAmount1("Blend Amount 1", Range(0.0, 1.0)) = 0.0
        
        _AnimSpeed ("AnimSpeed", float) = 1.0
        _AnimPower ("AnimPower", Range(1, .994)) = .997
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
sampler2D _OverlayTex1;
fixed _BlendAmount1;

float _AnimSpeed;
float _AnimPower;

struct Input {
	float2 uv_MainTex : TEXCOORD0;
    float2 uv_OverlayTex;
	float2 uv_OverlayTex1;
};

void surf (Input IN, inout SurfaceOutput o) {
	
	
	float2 AnimUV = IN.uv_OverlayTex.xy + floor(sin(_Time.x * (600 * _AnimSpeed))) * (1.0 * _AnimPower); // .997
	
	
    fixed4 overlay = tex2D (_OverlayTex, AnimUV);
    
	fixed4 overlay1 = tex2D (_OverlayTex1, IN.uv_OverlayTex);
	
	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	
	o.Albedo = lerp(c.rgb, overlay.rgb, overlay.a * _BlendAmount);
	o.Albedo = lerp(o.Albedo.rgb, overlay1.rgb, overlay1.a * _BlendAmount1);
	o.Alpha = c.a;
}
ENDCG
UsePass "Toon/Basic Outline/OUTLINE"
	} 
	Fallback "Toon/Basic"
}
