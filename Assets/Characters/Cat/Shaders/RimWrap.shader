Shader "CuCatMaker/5_CuCatM RimWrap shader" {
 Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _Cutoff ("Alpha Cutoff", Float) = 0.5
 _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
 _BlendPower1("Blend_Power1", Range(0, 1)) = 0
 _BlendTex1 ("Blend_01 (RGBA)", 2D) = "black" {}
 _BlendPower2("Blend_Power2", Range(0, 1)) = 0
 _BlendTex2 ("Blend_02 (RGBA)", 2D) = "black" {}
 _BlendPower3("Blend_Power3", Range(0, 1)) = 0
 _BlendTex3 ("Blend_03 (RGBA)", 2D) = "black" {}
 _RimColor ("Rim Color", Color) = (0,0,0,0.0)
 _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
 }
  
 SubShader {
 Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent" }
 LOD 250
 Cull back
  
 CGPROGRAM
 #pragma surface surf WrapLambert
 sampler2D _MainTex;
 fixed4 _Color;
 float _BlendPower1;
 float _BlendPower2;
 float _BlendPower3;
 fixed4 _RimColor;
 float _RimPower;
 fixed _Cutoff;
 sampler2D _BlendTex1;
 sampler2D _BlendTex2;
 sampler2D _BlendTex3;
 
 half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
      half NdotL = dot (s.Normal, lightDir);
      half diff = NdotL * 0.5 + 0.5;
      half4 c;
      c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);
      c.a = s.Alpha;
      return c;
 	}
  
 struct Input {
 float2 uv_MainTex;
 float2 uv_BlendTex1;
 float2 uv_BlendTex2;
 float2 uv_BlendTex3;
 float3 viewDir;
 };
  
 void surf (Input IN, inout SurfaceOutput o) {
 fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
 half4 blend1 = tex2D(_BlendTex1, IN.uv_BlendTex1);
 half4 blend2 = tex2D(_BlendTex2, IN.uv_BlendTex2);
 half4 blend3 = tex2D(_BlendTex3, IN.uv_BlendTex3);
 c.rgb = lerp (c.rgb, blend1.rgb, blend1.a * _BlendPower1);
 c.rgb = lerp (c.rgb, blend2.rgb, blend2.a * _BlendPower2);
 c.rgb = lerp (c.rgb, blend3.rgb, blend3.a * _BlendPower3);
 c *= _Color;
 
 half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
 o.Emission = _RimColor.rgb * pow (rim, _RimPower);
 
 o.Albedo = c.rgb;
 o.Alpha = c.a + blend1.a + blend2.a + blend3.a;
 clip(o.Alpha - _Cutoff);
 }
 ENDCG
 }
 
 Fallback "Diffuse"
 }
