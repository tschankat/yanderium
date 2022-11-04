Shader "Custom/DepthNormals" 
{
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float4 nz : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert(appdata_base v) 
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.nz.xyz = COMPUTE_VIEW_NORMAL;
				o.nz.w = COMPUTE_DEPTH_01;
				return o;
			}

			// These methods will be used when rotating the hue
			float3 rgb_hsv(float3 c) {
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
				float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
				float d = q.x - min(q.w, q.y);
				float e = 1.0e-10;
				return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 hsv_rgb(float3 c) {
				c = float3(c.x, clamp(c.yz, 0.0, 1.0));
				float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
				float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
				return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
			}

			float4 frag(v2f i) : COLOR
			{
				// Set up the hue adjustment
				float4 hueAdjustment;
				hueAdjustment.x = 0.4;
				hueAdjustment.y = 1;
				hueAdjustment.z = 0.1;
				hueAdjustment.w = 0.99;

				// Convert the normal color to hsv and then rotate the hue
				float3 hsv = rgb_hsv(i.nz.xyz);
				float3 rgb = hsv_rgb(hsv + hueAdjustment.xyz);
				return EncodeDepthNormal(hueAdjustment.w, rgb);
			}
			ENDCG
		}
	}

	Fallback Off
}