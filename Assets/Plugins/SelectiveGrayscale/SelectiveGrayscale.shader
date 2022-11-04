// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

  /////////////////////////////////
 //  Shader by Patrick Boelens  //
/////////////////////////////////

Shader "Custom Image Effects/Selective Grayscale" {
Properties{
	_MainTex ("", 2D) = "white" {}
	_FilterColor ("Filter Color", Color) = (1, 0, 0, 1)
	_Sensitivity ("Sensitivity", Range(0,1)) = 0.5
	_Tolerance ("Tolerance", Range(0,1)) = 0.2
	_Desaturation ("Desaturation", Range(0,1)) = 1
}
 
SubShader{
	ZTest Always Cull Off ZWrite Off Fog { Mode Off }
	
	Pass{
//		Boring stuff here, scroll down to where all the fun is!
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc" 
		
		struct v2f {
			float4 pos : POSITION;
			half2 uv : TEXCOORD0;
		};
   
		v2f vert (appdata_img v){
			v2f o;
			o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			return o; 
		}
    
		sampler2D _MainTex;
		float4 _FilterColor;
		float _Desaturation;
		float _Sensitivity;
		float _Tolerance;

//		Fun stuff starts here!
		fixed4 frag (v2f i) : COLOR{
			fixed4 oldCol = tex2D(_MainTex, i.uv);
		     
//				simple RGB average
//			float gray = (oldCol.r + oldCol.g + oldCol.b)/3f;

//				gamma/ luma corrected
			float gray = oldCol.r*0.2989f + oldCol.g*0.5870f + oldCol.b*0.1140f;
			
			fixed4 grayscaleCol = fixed4(gray, gray, gray, 1);
			
			float deltaFilterRed = _FilterColor.r - oldCol.r;
			float deltaFilterGreen = _FilterColor.g - oldCol.g;
			float deltaFilterBlue = _FilterColor.b - oldCol.b;
			float deltaFilterColor = sqrt(deltaFilterRed*deltaFilterRed + deltaFilterBlue*deltaFilterBlue + deltaFilterGreen*deltaFilterGreen);
			
//				no color filter; desaturation only
//			fixed4 newCol = lerp(oldCol, grayscaleCol, _Desaturation);
//
//				"technically correct" color filter; no tolerance
//			fixed4 newCol = lerp(oldCol, grayscaleCol, clamp(deltaFilterColor * _Sensitivity, 0, 1));
//
//				hard tolerance cut-off; no desaturation var; disable "tolerance block" below if you use this
//			fixed4 newCol = lerp(oldCol, grayscaleCol, deltaFilterColor < _Tolerance ? 0 : clamp(deltaFilterColor * _Sensitivity, 0, 1));
			
//				not "technically correct" as it "overblows" the filter color a bit, but looks best imo
			fixed4 newCol = lerp(oldCol, grayscaleCol, min(_Desaturation, deltaFilterColor * _Sensitivity));
			
//				"Tolerance Block" - this gives the tolerance that smooth falloff
			if(deltaFilterColor < _Tolerance){
				newCol = lerp(oldCol, newCol, 1 - (_Tolerance - deltaFilterColor));
			}
	     
			return newCol;
		}
		ENDCG
	}
} 
FallBack "Diffuse" //because everybody does this...
}