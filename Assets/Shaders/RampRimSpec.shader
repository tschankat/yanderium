//This is a sexy little shader I made for cartoon styled graphics.
//This is roughly based on the Toon/Lighted shader that comes with unity.
//I added in some extra options:
//Saturation - Saturate or desaturate the colors of a texture
//Hue Shift - Shift the hue of an object (rotate the color wheel)
//Specular - BlinnPhong specular highlights
//Rim Lighting - Adds an emissive lighting effect on the 'edges' of a model

Shader "RampRimSpec" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_SpecColor("Spec Color", Color) = (0.5,0.5,0.5,1)
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		
		_Saturation ("Saturation", Range(0, 5)) = 1.0 
		_HueShift ("Hue Shift", Range(0, 1)) = 0.0 //color at 0 is same at 1
		
		_Shininess ("Shininess", Range(0.001,5.0)) = .075 //Increased range on shininess
		_RimPower ("Rim Power", Range(0.1,8.0)) = 3.0 //Lower number = more effect
		
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
			#include "UnityCG.cginc"
			//Colorspace conversions from RGB->HSV and HSV->RGB 
			#include "./hsv.cginc"
			#pragma surface surf ToonBlinnPhong
			sampler2D _MainTex;
			float4 _Color;
			float4 _RimColor;
			float _RimPower;
			half  _Shininess;
				
			#ifdef HSV_INCLUDED
			float _Saturation;
			float _HueShift;
			#endif
			
			sampler2D _Ramp;

			// custom lighting function that uses a texture ramp based
			// on angle between light direction and normal
			#pragma lighting ToonBlinnPhong exclude_path:prepass
			inline half4 LightingToonBlinnPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten) {
				half3 h = normalize(lightDir + viewDir);
				#ifndef USING_DIRECTIONAL_LIGHT
				lightDir = normalize(lightDir);
				#endif
				
				//Sample Ramp for Diffuse power
				half d = dot(s.Normal, lightDir)*0.5 + 0.5;
				half3 diff = tex2D(_Ramp, float2(d,d)).rgb;
				
				//BlinnPhong specular calculation
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0) * s.Gloss;
				
				fixed4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2);
				c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
				
				return c;
			}
			
			struct Input {
				float2 uv_MainTex : TEXCOORD0;
				float3 viewDir;
			};
			
			void surf (Input IN, inout SurfaceOutput o) {
				half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				
				#ifdef HSV_INCLUDED
				float3 hsv = toHSV(c.rgb);
				if (hsv.g > 0) {
					hsv.r = frac(hsv.r + _HueShift);
					hsv.g *= _Saturation;
					c.rgb = saturate(toRGB(hsv));
				}
				#endif
				
				o.Albedo = c.rgb;
				
				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				float3 emis = _RimColor.rgb * pow(rim, _RimPower);
				
				o.Emission = emis;
				o.Alpha = c.a * _Color.a;
				o.Gloss = c.a;
				o.Specular = _Shininess;
			}
			
		ENDCG

	}

	Fallback "Diffuse"
}
