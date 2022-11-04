Shader "TOZ/Object/Fx/Electric" {
	Properties {
		_Color("Electric Color", Color) = (1, 0.5, 0.5, 1)
		_NoiseTex("Noise Texture", 2D) = "white" { }
		_Distance("Sample Distance", Range(0, 5)) = 0.02
		_Speed("Speed", Range(0, 10)) = 0.25
		_Noise("Noise Amount", Range(-10, 10)) = 0.1
		_Height("Wave Height", Range(0, 1)) = 0.1
		_Glow("Glow Amount", Float) = 0.1
		_GlowHeight("Glow Height", Float) = 15
		_GlowFallOff("Glow Falloff", Float) = 0.05
		_GlowPower("Glow Power", Float) = 150
		_UvXScale("Uv X Scale", Range(-1, 2)) = 1.0
	}

	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		LOD 100

		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma target 2.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile_instancing

			//UNITY_INSTANCING_BUFFER_START(MyProperties)
			//	UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			//	#define _Color_arr MyProperties
			//UNITY_INSTANCING_BUFFER_END(MyProperties)

			fixed4 _Color;
			sampler2D _NoiseTex;
			half _Distance;
			fixed _Speed, _Noise, _Height, _Glow, _GlowHeight;
			fixed _GlowFallOff, _GlowPower;
			fixed _UvXScale;

			struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 xs : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			v2f vert(a2v v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = (v.texcoord.xy - 0.5);
				o.xs.x = o.uv.x - _Distance;
				o.xs.y = o.uv.x;
				o.xs.z = o.uv.x + _Distance;
				o.xs *= _UvXScale;
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				float t = _Speed * _Time.y - _Noise * abs(i.uv.y);
				fixed n0 = tex2D(_NoiseTex, float2(i.xs.x, t)).r;
				fixed n1 = tex2D(_NoiseTex, float2(i.xs.y, t)).r;
				fixed n2 = tex2D(_NoiseTex, float2(i.xs.z, t)).r;
				half m0 = _Height * (n0 * 2.0 - 1.0) * (1.0 - i.xs.x * i.xs.x);
				half m1 = _Height * (n1 * 2.0 - 1.0) * (1.0 - i.xs.y * i.xs.y);
				half m2 = _Height * (n2 * 2.0 - 1.0) * (1.0 - i.xs.z * i.xs.z);
				half d0 = abs(i.uv.y - m0);
				half d1 = abs(i.uv.y - m1);
				half d2 = abs(i.uv.y - m2);
				half glow = 1.0 - pow((d0 + d1 + d2), _GlowFallOff);
				half amb = _Glow * (1.0 - i.uv.y * i.uv.y) * (1.0 - abs(_GlowHeight * i.uv.y));
				fixed4 result =  (_GlowPower * glow * glow + amb);
				//result *= UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
				result *= _Color;
				UNITY_APPLY_FOG(i.fogCoord, result);
				return result;
			}
			ENDCG
		}
	}

	FallBack Off
}