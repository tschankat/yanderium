Shader "TOZ/Object/Surf/Sample/Diffuse-Electric" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		/* ELECTRIC SHADER PROPERTIES */
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
		Tags { "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		/* ELECTRIC SHADER PASS */
		UsePass "TOZ/Object/Fx/Electric/BASE"
	} 

	FallBack "Diffuse"
}