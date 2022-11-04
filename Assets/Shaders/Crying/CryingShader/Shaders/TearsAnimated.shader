Shader "TearsAnimated" {
	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
		_NoiseTex ("Noise (RGB)", 2D) = "black" {}
		
		_TearReveal ("TearReveal", Range (0.0, 1)) = .35	// from .35 to 1
		_TearOpacity ("TearOpacity", float) = 1.0
		
		_AnimSpeed ("AnimSpeed", float) = 1.0
		_AnimFreq ("AnimFreq", float) = 1.0
		_AnimPower ("AnimPower", float) = 1.0
		
		_ShrinkSize ("ShrinkSize", Range (0.0, 0.003)) = .000
	}
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf CustomLighting vertex:vert alpha:blend

		inline half4 LightingCustomLighting (SurfaceOutput s, half3 lightDir, half atten)
		{
			// TODO lighting 
			
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb;
			c.a = s.Alpha	;
			return c;
		}
		
		float _ShrinkSize;
		
		float _AnimSpeed;
		float _AnimFreq;
		float _AnimPower;
		
		void vert( inout appdata_full v) 
         {
			float AnimSpeed = 500 * _AnimSpeed;
			float AnimFreq = 500 * _AnimFreq;
			
			float3 animOffset = float3(1, 1, 1) * v.vertex.xyz;
			//float3 animPower = float3(.001, .001, .001);
			float3 animPower = .0002 * _AnimPower;
			float4 newPos = v.vertex;
			
			float3 vertAnimation = sin(_Time.x * AnimSpeed + (animOffset.x + animOffset.y + animOffset.z) * AnimFreq) * animPower.xyz;
			float3 vertAnimation2 = sin(_Time.x * -AnimSpeed * .3 + (animOffset.x + animOffset.y + animOffset.z) * AnimFreq * .5) * animPower.xyz * 2;
			
			float3 animatedPos = newPos.xyz + v.color.x * vertAnimation + v.color.r * vertAnimation2;
			
			newPos.xyz = animatedPos;
			//newPos.xyz = newPos.xyz + sin(_Time.x * 10);
			
			v.vertex.xyz = newPos.xyz;
			
			//size mod
			float3 norm   = v.normal;
			v.vertex.xyz -= norm * _ShrinkSize;

         }
		
		

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		
		
		half _TearReveal;
		half _TearOpacity;
		

		struct Input {
			float2 uv_MainTex;
			
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			
			half _tearSpeed = 5;
			
			float2 noiseUV = float2( IN.uv_MainTex.y * 1 + _Time.x * _tearSpeed, IN.uv_MainTex.x * .5);
			half4 noiseTex = tex2D (_NoiseTex, noiseUV);
			
			//_TearReveal = .35;	// temp
			//_TearOpacity = 1;
			
			half TearRevealGrad = saturate(saturate(IN.uv_MainTex.y - (1-_TearReveal)) * 10 );
			
			half fresnel = 1 - saturate(dot(normalize(IN.viewDir), normalize(o.Normal)));
			
			float AlphaMult = .4;
		
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb + noiseTex.rgb;
			
			//o.Albedo = c.rgb;
			//o.Alpha = TearRevealGrad;
			o.Alpha = _TearOpacity * TearRevealGrad * (saturate(fresnel * .5) + c.a * AlphaMult + pow(noiseTex.rgb, 5) * .3);
			//o.Alpha = 0.5;
		}
		ENDCG
	} 
	Fallback "Toon/Basic"
}
