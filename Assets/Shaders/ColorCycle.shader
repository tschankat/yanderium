Shader "Custom/ColorCycle" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    colorMap ("Base (RGB)", 2D) = "white" {}
    _CycleSpeed ("Cycle Speed", Float) = 2.0

}
SubShader {
    Pass {

        CGPROGRAM
        #pragma exclude_renderers gles

		#pragma vertex vert_img
        #pragma fragment frag

        #include "UnityCG.cginc"

        // Two textures, one for the base texture, another for the color map
        sampler2D _MainTex, colorMap;

        float _CycleSpeed;

        float4 frag(v2f_img i) : COLOR 
        {
            // First get the color of the base texture for this fragment
            // i.uv is the unity provided texture coordinate
            half4 grayscale = tex2D (_MainTex, i.uv);

            // Use one of the components of the grayscale color to calculate 
            // an index into the colorMap. Adding time will shift the index 
            // over time. 
            // Textures wrap automatically so we don't need to worry about 
            // keeping the index between 0 and 1.
            // _Time is a unity provided variable that is useful for animating things
            half index = grayscale.r + _Time[0]*_CycleSpeed; 

            // Get the mapped color from the colorMap texture. The second component of the 
            // texture coordinate is always 0 since colorMap is a 1-dimensional texture.
            half4 c  = tex2D (colorMap, float2(index, 0));

            return c;
        }
        ENDCG
    }
} 
FallBack "Diffuse"
}