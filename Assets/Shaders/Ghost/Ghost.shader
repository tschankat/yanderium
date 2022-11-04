// Fixed the shader ;3 ~AB

Shader "AMZE/Ghost" {
    Properties {
        _TintA ("TintA (RGB)", Color) = (1,1,1,1)
        _TintB ("TintB (RGB)", Color) = (1,0,1,1)
        _TintFade ("Tint Fade", Float ) = 0.5
        _Alpha ("Alpha", Float ) = 0.8
        _MainTexture ("MainTexture (RGBA)", 2D) = "white" {}
        _MainTextureDesaturation ("Desaturation (MainTexture)", Range(-1, 1)) = 1
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalIntensity ("Normal Intensity", Float ) = 0
        _SpecularMap ("Specular Map", 2D) = "white" {}
        _SpecularColor ("Specular Color", Color) = (0,0,0,1)
        _Gloss ("Gloss", Float ) = 0
        _RimExposure ("Rim Exposure", Range(0, 5)) = 0.6
        _RimIntensity ("Rim Intensity", Range(-5, 5)) = 1.5
        [MaterialToggle] _NoiseTextureEnabled ("NoiseTexture Enabled", Float ) = 0.5232363
        _NoiseTexture ("Noise Texture (RGB)", 2D) = "black" {}
        [MaterialToggle] _AnimateNoise ("AnimateNoise", Float ) = 0
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.2
        [HideInInspector]_EmptyMap ("Empty Map", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmptyMap; uniform float4 _EmptyMap_ST;
            uniform sampler2D _SpecularMap; uniform float4 _SpecularMap_ST;
            uniform float _RimExposure;
            uniform float _RimIntensity;
            uniform float4 _TintA;
            uniform float _NoiseIntensity;
            uniform fixed _NoiseTextureEnabled;
            uniform float _MainTextureDesaturation;
            uniform float _NormalIntensity;
            uniform fixed _AnimateNoise;
            uniform float4 _SpecularColor;
            uniform float _Gloss;
            uniform float4 _TintB;
            uniform float _TintFade;
            uniform float _Alpha;

            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _EmptyMap_var = UnpackNormal(tex2D(_EmptyMap,TRANSFORM_TEX(i.uv0, _EmptyMap)));
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float _NormalIntensity_var = _NormalIntensity;
                float3 NormalMap = lerp(_EmptyMap_var.rgb,_NormalMap_var.rgb,_NormalIntensity_var);
                float3 normalLocal = NormalMap;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);

                // Lighting
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;

                // Gloss
                float _Gloss_var = _Gloss;
                float gloss = _Gloss_var;
                float specPow = exp2( gloss * 10.0 + 1.0 );

                // Global Illumination Data
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;

                // Specular
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float4 _SpecularColor_var = _SpecularColor;
                float4 _SpecularMap_var = tex2D(_SpecularMap,TRANSFORM_TEX(i.uv0, _SpecularMap));
                float3 specularColor = (_SpecularColor_var.rgb*_SpecularMap_var.rgb);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (gi.indirect.specular)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
                
                // Emit
                float _RimExposure_var = _RimExposure;
                float _RimIntensity_var = _RimIntensity;
                float RimLight = (1.0 - (pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimExposure_var)*_RimIntensity_var));
                float4 _TintB_var = _TintB;
                float4 _TintA_var = _TintA;
                float _TintFade_var = _TintFade;
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float _MainTextureDesaturation_var = _MainTextureDesaturation;
                float4 node_7686 = _Time;
                float node_244_ang = node_7686.g;
                float node_244_spd = 1.0;
                float node_244_cos = cos(node_244_spd*node_244_ang);
                float node_244_sin = sin(node_244_spd*node_244_ang);
                float2 node_244_piv = float2(0.5,0.5);
                float2 node_244 = (mul(i.uv0-node_244_piv,float2x2( node_244_cos, -node_244_sin, node_244_sin, node_244_cos))+node_244_piv);
                float2 _AnimateNoise_var = lerp( i.uv0, node_244, _AnimateNoise);
                float2 node_9982_skew = _AnimateNoise_var + 0.2127+_AnimateNoise_var.x*0.3713*_AnimateNoise_var.y;
                float2 node_9982_rnd = 4.789*sin(489.123*(node_9982_skew));
                float node_9982 = frac(node_9982_rnd.x*node_9982_rnd.y*(1+node_9982_skew.x));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(i.uv0, _NoiseTexture));
                float3 _NoiseTextureEnabled_var = lerp( node_9982, _NoiseTexture_var.rgb, _NoiseTextureEnabled);
                float3 Noise = _NoiseTextureEnabled_var;
                float _NoiseIntensity_var = _NoiseIntensity;
                float _Alpha_var = _Alpha;
                float3 emissive = lerp(sceneColor.rgb,lerp((RimLight*lerp(_TintB_var.rgb,_TintA_var.rgb,(sceneUVs.g.r+(0.5-_TintFade_var)))*lerp(_MainTexture_var.rgb,dot(_MainTexture_var.rgb,float3(0.3,0.59,0.11)),_MainTextureDesaturation_var)),Noise,_NoiseIntensity_var),(RimLight*_Alpha_var));
                
                // Final color
                float3 finalColor = specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
