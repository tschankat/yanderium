/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:0,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33230,y:32884,varname:node_2865,prsc:2|diff-7304-OUT,spec-2153-RGB,gloss-9529-R,normal-7371-OUT,emission-214-RGB,olwid-6181-OUT,olcol-5657-OUT,disp-2138-OUT,tess-4637-OUT;n:type:ShaderForge.SFN_Tex2d,id:6895,x:30680,y:31928,ptovrint:True,ptlb:AlbedoMap,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:afc1a71774c910c47a796646ef675bdc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2153,x:32212,y:32926,ptovrint:True,ptlb:SpecularMap,ptin:_SpecularMap,varname:_SpecularMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:6181,x:32827,y:33128,ptovrint:True,ptlb:OutlineWidth,ptin:_Outline,varname:_Outline,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.002;n:type:ShaderForge.SFN_Set,id:855,x:32460,y:32368,varname:Diffuse,prsc:2|IN-7957-OUT;n:type:ShaderForge.SFN_Get,id:7361,x:32748,y:32605,varname:node_7361,prsc:2|IN-855-OUT;n:type:ShaderForge.SFN_Get,id:6266,x:31459,y:34710,varname:node_6266,prsc:2|IN-855-OUT;n:type:ShaderForge.SFN_Tex2d,id:9529,x:32212,y:33162,ptovrint:True,ptlb:GlossMap,ptin:_GlossMap,varname:_GlossMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3740,x:30598,y:33289,ptovrint:True,ptlb:NormalMap,ptin:_NormalMap,varname:_NormalMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:214,x:32214,y:33760,ptovrint:True,ptlb:EmissionMap,ptin:_EmissionMap,varname:_EmissionMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1984,x:32214,y:34008,ptovrint:False,ptlb:DisplacementMap,ptin:_DisplacementMap,varname:_DisplacementMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:4637,x:32214,y:34396,ptovrint:False,ptlb:Tesselation,ptin:_Tesselation,varname:_Tesselation,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:9733,x:30606,y:32675,ptovrint:True,ptlb:OverlayMapA,ptin:_OverlayTex,varname:_OverlayTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Lerp,id:705,x:31226,y:32360,varname:node_705,prsc:2|A-9457-OUT,B-755-OUT,T-9733-A;n:type:ShaderForge.SFN_Lerp,id:7726,x:31442,y:32158,varname:node_7726,prsc:2|A-9457-OUT,B-705-OUT,T-6355-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6355,x:31226,y:32563,ptovrint:True,ptlb:BlendAmountA,ptin:_BlendAmount,varname:_BlendAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:6953,x:31440,y:32759,ptovrint:True,ptlb:OverlayMapB,ptin:_OverlayTex1,varname:_OverlayTex1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:5944,x:31960,y:32588,ptovrint:True,ptlb:BlendAmountB,ptin:_BlendAmount1,varname:_BlendAmount1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:9585,x:31960,y:32419,varname:node_9585,prsc:2|A-7726-OUT,B-2210-OUT,T-6953-A;n:type:ShaderForge.SFN_Lerp,id:7957,x:32229,y:32392,varname:node_7957,prsc:2|A-7726-OUT,B-9585-OUT,T-5944-OUT;n:type:ShaderForge.SFN_Fresnel,id:9654,x:31897,y:31829,varname:node_9654,prsc:2|NRM-5619-OUT,EXP-9051-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9051,x:31574,y:31924,ptovrint:True,ptlb:RimExpoure,ptin:_RimLightExpoure,varname:_RimLightExpoure,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_NormalVector,id:5619,x:31677,y:31739,prsc:2,pt:False;n:type:ShaderForge.SFN_Set,id:470,x:32776,y:31909,varname:Fresnel,prsc:2|IN-6954-OUT;n:type:ShaderForge.SFN_Add,id:7304,x:32966,y:32627,varname:node_7304,prsc:2|A-7361-OUT,B-869-OUT;n:type:ShaderForge.SFN_Get,id:869,x:32748,y:32674,varname:node_869,prsc:2|IN-470-OUT;n:type:ShaderForge.SFN_Multiply,id:7678,x:32104,y:31856,varname:node_7678,prsc:2|A-9654-OUT,B-978-OUT;n:type:ShaderForge.SFN_ValueProperty,id:978,x:31897,y:32050,ptovrint:True,ptlb:RimIntensity,ptin:_RimLightIntensity,varname:_RimLightIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6954,x:32301,y:31927,varname:node_6954,prsc:2|A-7678-OUT,B-8605-RGB;n:type:ShaderForge.SFN_Color,id:8605,x:32094,y:32093,ptovrint:False,ptlb:RimColor,ptin:_RimColor,varname:_RimColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:6404,x:33189,y:32367,ptovrint:False,ptlb:=== Rimlight ===,ptin:_Rimlight,varname:_Rimlight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Set,id:2117,x:33606,y:32013,varname:Headers,prsc:2|IN-7001-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8511,x:33189,y:32107,ptovrint:False,ptlb:=== Outlines ===,ptin:_Outlines,varname:_Outlines,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Add,id:7001,x:33429,y:32013,varname:node_7001,prsc:2|A-7713-OUT,B-8511-OUT,C-4322-OUT,D-6404-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7713,x:33189,y:31968,ptovrint:False,ptlb:=== Displacement ===,ptin:_Displacement,varname:_Displacement,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4322,x:33189,y:32232,ptovrint:False,ptlb:=== Overlays ===,ptin:_Overlays,varname:_Overlays,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:755,x:30816,y:32463,varname:node_755,prsc:2|A-8209-RGB,B-9733-RGB;n:type:ShaderForge.SFN_Color,id:8209,x:30606,y:32453,ptovrint:False,ptlb:OverlayColorA,ptin:_OverlayColorA,varname:_OverlayColorA,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2210,x:31621,y:32461,varname:node_2210,prsc:2|A-8558-RGB,B-6953-RGB;n:type:ShaderForge.SFN_Color,id:8558,x:31419,y:32530,ptovrint:False,ptlb:OverlayColorB,ptin:_OverlayColorB,varname:_OverlayColorB,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9457,x:31005,y:32172,varname:node_9457,prsc:2|A-6895-RGB,B-1900-RGB;n:type:ShaderForge.SFN_Color,id:1900,x:30680,y:32168,ptovrint:True,ptlb:AlbedoColor,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3519,x:31447,y:35027,ptovrint:True,ptlb:OutlineColor,ptin:_OutlineColor,varname:_OutlineColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ToggleProperty,id:8540,x:31865,y:34470,ptovrint:False,ptlb:AdaptiveColor,ptin:_AdaptiveColor,varname:_AdaptiveColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_If,id:5657,x:32307,y:34614,varname:node_5657,prsc:2|A-8540-OUT,B-1371-OUT,GT-4205-OUT,EQ-4205-OUT,LT-3519-RGB;n:type:ShaderForge.SFN_Vector1,id:1371,x:31865,y:34578,varname:node_1371,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:4205,x:31712,y:34729,varname:node_4205,prsc:2|A-6266-OUT,B-3519-RGB;n:type:ShaderForge.SFN_ValueProperty,id:5441,x:32214,y:34245,ptovrint:False,ptlb:DisplacementIntensity,ptin:_DisplacementIntensity,varname:_DisplacementIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2138,x:32425,y:34076,varname:node_2138,prsc:2|A-1984-RGB,B-5441-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5184,x:31403,y:33279,ptovrint:False,ptlb:NormalIntensity,ptin:_NormalIntensity,varname:_NormalIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ComponentMask,id:3630,x:31247,y:33135,varname:node_3630,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3740-RGB;n:type:ShaderForge.SFN_Multiply,id:6679,x:30797,y:33037,varname:node_6679,prsc:2|A-1426-OUT,B-3740-RGB;n:type:ShaderForge.SFN_Vector1,id:1426,x:30470,y:33036,varname:node_1426,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:8859,x:31151,y:32989,varname:node_8859,prsc:2|A-6679-OUT,B-3172-OUT;n:type:ShaderForge.SFN_Vector1,id:3172,x:30927,y:33094,varname:node_3172,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:4265,x:31584,y:33170,varname:node_4265,prsc:2|A-3630-OUT,B-5184-OUT;n:type:ShaderForge.SFN_Append,id:2027,x:31712,y:33385,varname:node_2027,prsc:2|A-4265-OUT,B-3740-B;n:type:ShaderForge.SFN_Normalize,id:7371,x:31982,y:33395,varname:node_7371,prsc:2|IN-2027-OUT;n:type:ShaderForge.SFN_Vector1,id:6761,x:31509,y:33356,varname:node_6761,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:6815,x:31573,y:33420,varname:node_6815,prsc:2,v1:2;proporder:6895-1900-3740-5184-2153-9529-214-7713-1984-5441-4637-8511-6181-3519-8540-4322-9733-8209-6355-6953-8558-5944-6404-8605-9051-978;pass:END;sub:END;*/

Shader "AMZE/Toon" {
    Properties {
        _MainTex ("AlbedoMap", 2D) = "white" {}
        _Color ("AlbedoColor", Color) = (1,1,1,1)
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _NormalIntensity ("NormalIntensity", Float ) = 1
        _SpecularMap ("SpecularMap", 2D) = "black" {}
        _GlossMap ("GlossMap", 2D) = "white" {}
        _EmissionMap ("EmissionMap", 2D) = "black" {}
        _Displacement ("=== Displacement ===", Float ) = 1
        _DisplacementMap ("DisplacementMap", 2D) = "black" {}
        _DisplacementIntensity ("DisplacementIntensity", Float ) = 1
        _Tesselation ("Tesselation", Float ) = 1
        _Outlines ("=== Outlines ===", Float ) = 2
        _Outline ("OutlineWidth", Float ) = 0.002
        _OutlineColor ("OutlineColor", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _AdaptiveColor ("AdaptiveColor", Float ) = 1
        _Overlays ("=== Overlays ===", Float ) = 3
        _OverlayTex ("OverlayMapA", 2D) = "black" {}
        _OverlayColorA ("OverlayColorA", Color) = (1,1,1,1)
        _BlendAmount ("BlendAmountA", Float ) = 0
        _OverlayTex1 ("OverlayMapB", 2D) = "black" {}
        _OverlayColorB ("OverlayColorB", Color) = (1,1,1,1)
        _BlendAmount1 ("BlendAmountB", Float ) = 0
        _Rimlight ("=== Rimlight ===", Float ) = 4
        _RimColor ("RimColor", Color) = (1,1,1,1)
        _RimLightExpoure ("RimExpoure", Float ) = 3
        _RimLightIntensity ("RimIntensity", Float ) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DisplacementMap; uniform float4 _DisplacementMap_ST;
            uniform sampler2D _OverlayTex; uniform float4 _OverlayTex_ST;
            uniform sampler2D _OverlayTex1; uniform float4 _OverlayTex1_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Outline)
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesselation)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount1)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorA)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorB)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OutlineColor)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _AdaptiveColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _DisplacementIntensity)
            UNITY_INSTANCING_BUFFER_END( Props )
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
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.pos = UnityObjectToClipPos(v.vertex);
                float cameraDistanceFactor = 2.0f * unity_OrthoParams.y;

                if (unity_OrthoParams.w < 0.1f)
                {
                    cameraDistanceFactor = length(WorldSpaceViewDir(v.vertex)) * 1.0f / _ProjectionParams.y; //1.0f is a magic number
                }
                float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                float2 offset = TransformViewToProjection(norm.xy);
                float _Outline_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Outline );
                o.pos.xy += (offset * o.pos.z * _Outline_var) * cameraDistanceFactor;

                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 _DisplacementMap_var = tex2Dlod(_DisplacementMap,float4(TRANSFORM_TEX(v.texcoord0, _DisplacementMap),0.0,0));
                    float _DisplacementIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DisplacementIntensity );
                    v.vertex.xyz += (_DisplacementMap_var.rgb*_DisplacementIntensity_var);
                }
                float Tessellation(TessVertex v){
                    float _Tesselation_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesselation );
                    return _Tesselation_var;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float _AdaptiveColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _AdaptiveColor );
                float node_5657_if_leA = step(_AdaptiveColor_var,1.0);
                float node_5657_if_leB = step(1.0,_AdaptiveColor_var);
                float4 _OutlineColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlineColor );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 node_9457 = (_MainTex_var.rgb*_Color_var.rgb);
                float4 _OverlayColorA_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorA );
                float4 _OverlayTex_var = tex2D(_OverlayTex,TRANSFORM_TEX(i.uv0, _OverlayTex));
                float _BlendAmount_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount );
                float3 node_7726 = lerp(node_9457,lerp(node_9457,(_OverlayColorA_var.rgb*_OverlayTex_var.rgb),_OverlayTex_var.a),_BlendAmount_var);
                float4 _OverlayColorB_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorB );
                float4 _OverlayTex1_var = tex2D(_OverlayTex1,TRANSFORM_TEX(i.uv0, _OverlayTex1));
                float _BlendAmount1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount1 );
                float3 Diffuse = lerp(node_7726,lerp(node_7726,(_OverlayColorB_var.rgb*_OverlayTex1_var.rgb),_OverlayTex1_var.a),_BlendAmount1_var);
                float3 node_4205 = (Diffuse*_OutlineColor_var.rgb);
                return fixed4(lerp((node_5657_if_leA*_OutlineColor_var.rgb)+(node_5657_if_leB*node_4205),node_4205,node_5657_if_leA*node_5657_if_leB),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _SpecularMap; uniform float4 _SpecularMap_ST;
            uniform sampler2D _GlossMap; uniform float4 _GlossMap_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform sampler2D _DisplacementMap; uniform float4 _DisplacementMap_ST;
            uniform sampler2D _OverlayTex; uniform float4 _OverlayTex_ST;
            uniform sampler2D _OverlayTex1; uniform float4 _OverlayTex1_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesselation)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount1)
                UNITY_DEFINE_INSTANCED_PROP( float, _RimLightExpoure)
                UNITY_DEFINE_INSTANCED_PROP( float, _RimLightIntensity)
                UNITY_DEFINE_INSTANCED_PROP( float4, _RimColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorA)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorB)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _DisplacementIntensity)
                UNITY_DEFINE_INSTANCED_PROP( float, _NormalIntensity)
            UNITY_INSTANCING_BUFFER_END( Props )
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
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
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
                #elif UNITY_SHOULD_SAMPLE_SH
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 _DisplacementMap_var = tex2Dlod(_DisplacementMap,float4(TRANSFORM_TEX(v.texcoord0, _DisplacementMap),0.0,0));
                    float _DisplacementIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DisplacementIntensity );
                    v.vertex.xyz += (_DisplacementMap_var.rgb*_DisplacementIntensity_var);
                }
                float Tessellation(TessVertex v){
                    float _Tesselation_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesselation );
                    return _Tesselation_var;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float _NormalIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _NormalIntensity );
                float3 normalLocal = normalize(float3((_NormalMap_var.rgb.rg*_NormalIntensity_var),_NormalMap_var.b));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _GlossMap_var = tex2D(_GlossMap,TRANSFORM_TEX(i.uv0, _GlossMap));
                float gloss = _GlossMap_var.r;
                float perceptualRoughness = 1.0 - _GlossMap_var.r;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
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
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float4 _SpecularMap_var = tex2D(_SpecularMap,TRANSFORM_TEX(i.uv0, _SpecularMap));
                float3 specularColor = _SpecularMap_var.rgb;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 node_9457 = (_MainTex_var.rgb*_Color_var.rgb);
                float4 _OverlayColorA_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorA );
                float4 _OverlayTex_var = tex2D(_OverlayTex,TRANSFORM_TEX(i.uv0, _OverlayTex));
                float _BlendAmount_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount );
                float3 node_7726 = lerp(node_9457,lerp(node_9457,(_OverlayColorA_var.rgb*_OverlayTex_var.rgb),_OverlayTex_var.a),_BlendAmount_var);
                float4 _OverlayColorB_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorB );
                float4 _OverlayTex1_var = tex2D(_OverlayTex1,TRANSFORM_TEX(i.uv0, _OverlayTex1));
                float _BlendAmount1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount1 );
                float3 Diffuse = lerp(node_7726,lerp(node_7726,(_OverlayColorB_var.rgb*_OverlayTex1_var.rgb),_OverlayTex1_var.a),_BlendAmount1_var);
                float _RimLightExpoure_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimLightExpoure );
                float _RimLightIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimLightIntensity );
                float4 _RimColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimColor );
                float3 Fresnel = ((pow(1.0-max(0,dot(i.normalDir, viewDirection)),_RimLightExpoure_var)*_RimLightIntensity_var)*_RimColor_var.rgb);
                float3 diffuseColor = (Diffuse+Fresnel); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                float3 emissive = _EmissionMap_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _DisplacementMap; uniform float4 _DisplacementMap_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesselation)
                UNITY_DEFINE_INSTANCED_PROP( float, _DisplacementIntensity)
            UNITY_INSTANCING_BUFFER_END( Props )
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
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 _DisplacementMap_var = tex2Dlod(_DisplacementMap,float4(TRANSFORM_TEX(v.texcoord0, _DisplacementMap),0.0,0));
                    float _DisplacementIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DisplacementIntensity );
                    v.vertex.xyz += (_DisplacementMap_var.rgb*_DisplacementIntensity_var);
                }
                float Tessellation(TessVertex v){
                    float _Tesselation_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesselation );
                    return _Tesselation_var;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 5.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _SpecularMap; uniform float4 _SpecularMap_ST;
            uniform sampler2D _GlossMap; uniform float4 _GlossMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform sampler2D _DisplacementMap; uniform float4 _DisplacementMap_ST;
            uniform sampler2D _OverlayTex; uniform float4 _OverlayTex_ST;
            uniform sampler2D _OverlayTex1; uniform float4 _OverlayTex1_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Tesselation)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount)
                UNITY_DEFINE_INSTANCED_PROP( float, _BlendAmount1)
                UNITY_DEFINE_INSTANCED_PROP( float, _RimLightExpoure)
                UNITY_DEFINE_INSTANCED_PROP( float, _RimLightIntensity)
                UNITY_DEFINE_INSTANCED_PROP( float4, _RimColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorA)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OverlayColorB)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _DisplacementIntensity)
            UNITY_INSTANCING_BUFFER_END( Props )
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 _DisplacementMap_var = tex2Dlod(_DisplacementMap,float4(TRANSFORM_TEX(v.texcoord0, _DisplacementMap),0.0,0));
                    float _DisplacementIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DisplacementIntensity );
                    v.vertex.xyz += (_DisplacementMap_var.rgb*_DisplacementIntensity_var);
                }
                float Tessellation(TessVertex v){
                    float _Tesselation_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tesselation );
                    return _Tesselation_var;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                o.Emission = _EmissionMap_var.rgb;
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 node_9457 = (_MainTex_var.rgb*_Color_var.rgb);
                float4 _OverlayColorA_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorA );
                float4 _OverlayTex_var = tex2D(_OverlayTex,TRANSFORM_TEX(i.uv0, _OverlayTex));
                float _BlendAmount_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount );
                float3 node_7726 = lerp(node_9457,lerp(node_9457,(_OverlayColorA_var.rgb*_OverlayTex_var.rgb),_OverlayTex_var.a),_BlendAmount_var);
                float4 _OverlayColorB_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OverlayColorB );
                float4 _OverlayTex1_var = tex2D(_OverlayTex1,TRANSFORM_TEX(i.uv0, _OverlayTex1));
                float _BlendAmount1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BlendAmount1 );
                float3 Diffuse = lerp(node_7726,lerp(node_7726,(_OverlayColorB_var.rgb*_OverlayTex1_var.rgb),_OverlayTex1_var.a),_BlendAmount1_var);
                float _RimLightExpoure_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimLightExpoure );
                float _RimLightIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimLightIntensity );
                float4 _RimColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimColor );
                float3 Fresnel = ((pow(1.0-max(0,dot(i.normalDir, viewDirection)),_RimLightExpoure_var)*_RimLightIntensity_var)*_RimColor_var.rgb);
                float3 diffColor = (Diffuse+Fresnel);
                float4 _SpecularMap_var = tex2D(_SpecularMap,TRANSFORM_TEX(i.uv0, _SpecularMap));
                float3 specColor = _SpecularMap_var.rgb;
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float4 _GlossMap_var = tex2D(_GlossMap,TRANSFORM_TEX(i.uv0, _GlossMap));
                float roughness = 1.0 - _GlossMap_var.r;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
