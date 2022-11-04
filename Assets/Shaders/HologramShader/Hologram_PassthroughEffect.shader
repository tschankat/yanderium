// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:7564,x:32952,y:32689,varname:node_7564,prsc:2|emission-3301-OUT,alpha-8662-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:7297,x:30777,y:32880,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_7297,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b49d1c8e292e35f4380e859daf8874bf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6670,x:31064,y:32845,varname:node_6670,prsc:2,tex:b49d1c8e292e35f4380e859daf8874bf,ntxv:0,isnm:False|TEX-7297-TEX;n:type:ShaderForge.SFN_Multiply,id:8076,x:31348,y:32847,varname:node_8076,prsc:2|A-6670-RGB,B-6172-OUT;n:type:ShaderForge.SFN_Vector1,id:2089,x:31053,y:32745,varname:node_2089,prsc:2,v1:3;n:type:ShaderForge.SFN_DepthBlend,id:474,x:31158,y:33269,varname:node_474,prsc:2|DIST-9305-OUT;n:type:ShaderForge.SFN_Slider,id:9305,x:30730,y:33266,ptovrint:False,ptlb:PassthroughHLArea,ptin:_PassthroughHLArea,varname:node_9305,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.5;n:type:ShaderForge.SFN_Lerp,id:5750,x:31590,y:32777,varname:node_5750,prsc:2|A-4912-OUT,B-8076-OUT,T-474-OUT;n:type:ShaderForge.SFN_Vector3,id:7940,x:31073,y:32463,varname:node_7940,prsc:2,v1:0.03676468,v2:1,v3:2;n:type:ShaderForge.SFN_Multiply,id:4912,x:31183,y:32658,varname:node_4912,prsc:2|A-9792-RGB,B-7161-OUT;n:type:ShaderForge.SFN_Slider,id:7161,x:30599,y:32709,ptovrint:False,ptlb:PassthroughHLIntencity,ptin:_PassthroughHLIntencity,varname:_PassthroughHLArea_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:20;n:type:ShaderForge.SFN_Color,id:9792,x:30731,y:32510,ptovrint:False,ptlb:PassthroughBloomColor,ptin:_PassthroughBloomColor,varname:node_9792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.397,c2:1,c3:0.9172413,c4:1;n:type:ShaderForge.SFN_Lerp,id:6547,x:31814,y:32809,varname:node_6547,prsc:2|A-5750-OUT,B-8076-OUT,T-8888-R;n:type:ShaderForge.SFN_Tex2d,id:8888,x:31464,y:32995,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_8888,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c2aabcf278ac9db479c3e8cdf4bbb8b0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_LightColor,id:7460,x:31935,y:32951,varname:node_7460,prsc:2;n:type:ShaderForge.SFN_Slider,id:5962,x:31857,y:33126,ptovrint:False,ptlb:LightColorModulation,ptin:_LightColorModulation,varname:node_5962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:1360,x:32168,y:32873,varname:node_1360,prsc:2|A-3626-OUT,B-7460-RGB,T-5962-OUT;n:type:ShaderForge.SFN_Vector1,id:3626,x:31960,y:32890,varname:node_3626,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:4262,x:32317,y:32811,varname:node_4262,prsc:2|A-6547-OUT,B-1360-OUT;n:type:ShaderForge.SFN_Tex2d,id:8104,x:32044,y:33303,ptovrint:False,ptlb:HoloOverlay,ptin:_HoloOverlay,varname:node_8104,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:204bbf1fbb846d5429e2aadfedbb35fb,ntxv:0,isnm:False|UVIN-345-OUT;n:type:ShaderForge.SFN_Blend,id:8401,x:32303,y:33092,varname:node_8401,prsc:2,blmd:10,clmp:True|SRC-1358-OUT,DST-4262-OUT;n:type:ShaderForge.SFN_ScreenPos,id:5527,x:31609,y:33213,varname:node_5527,prsc:2,sctp:2;n:type:ShaderForge.SFN_Multiply,id:1358,x:32303,y:33284,varname:node_1358,prsc:2|A-8104-RGB,B-5899-OUT;n:type:ShaderForge.SFN_Vector1,id:3181,x:32180,y:33413,varname:node_3181,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Slider,id:5899,x:31992,y:33544,ptovrint:False,ptlb:HoloOverlayIntensity,ptin:_HoloOverlayIntensity,varname:node_5899,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3504274,max:2;n:type:ShaderForge.SFN_Add,id:3301,x:32589,y:32845,varname:node_3301,prsc:2|A-4262-OUT,B-1358-OUT;n:type:ShaderForge.SFN_Slider,id:6172,x:30820,y:33086,ptovrint:False,ptlb:HoloTextureIntencity,ptin:_HoloTextureIntencity,varname:node_6172,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:3;n:type:ShaderForge.SFN_Add,id:345,x:31857,y:33303,varname:node_345,prsc:2|A-5527-UVOUT,B-4210-T;n:type:ShaderForge.SFN_Time,id:4210,x:31550,y:33415,varname:node_4210,prsc:2;n:type:ShaderForge.SFN_Depth,id:4310,x:30811,y:33750,varname:node_4310,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8662,x:32603,y:32992,varname:node_8662,prsc:2|A-6670-A,B-3633-OUT;n:type:ShaderForge.SFN_Vector1,id:3633,x:32284,y:33026,varname:node_3633,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:9745,x:31025,y:33750,varname:node_9745,prsc:2|A-4310-OUT,B-5447-OUT;n:type:ShaderForge.SFN_Vector1,id:5447,x:30821,y:33877,varname:node_5447,prsc:2,v1:0.15;n:type:ShaderForge.SFN_Clamp01,id:686,x:31224,y:33750,varname:node_686,prsc:2|IN-9745-OUT;n:type:ShaderForge.SFN_OneMinus,id:4574,x:31401,y:33777,varname:node_4574,prsc:2|IN-686-OUT;proporder:7297-9305-7161-9792-8888-5962-8104-5899-6172;pass:END;sub:END;*/

Shader "Unlit/Hologram_PassthroughEffect" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _PassthroughHLArea ("PassthroughHLArea", Range(0, 0.5)) = 0
        _PassthroughHLIntencity ("PassthroughHLIntencity", Range(0, 20)) = 2
        _PassthroughBloomColor ("PassthroughBloomColor", Color) = (0.397,1,0.9172413,1)
        _Mask ("Mask", 2D) = "white" {}
        _LightColorModulation ("LightColorModulation", Range(0, 1)) = 0
        _HoloOverlay ("HoloOverlay", 2D) = "white" {}
        _HoloOverlayIntensity ("HoloOverlayIntensity", Range(0, 2)) = 0.3504274
        _HoloTextureIntencity ("HoloTextureIntencity", Range(0, 3)) = 3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _PassthroughHLArea;
            uniform float _PassthroughHLIntencity;
            uniform float4 _PassthroughBloomColor;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _LightColorModulation;
            uniform sampler2D _HoloOverlay; uniform float4 _HoloOverlay_ST;
            uniform float _HoloOverlayIntensity;
            uniform float _HoloTextureIntencity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
////// Emissive:
                float4 node_6670 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_8076 = (node_6670.rgb*_HoloTextureIntencity);
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float node_3626 = 1.0;
                float3 node_4262 = (lerp(lerp((_PassthroughBloomColor.rgb*_PassthroughHLIntencity),node_8076,saturate((sceneZ-partZ)/_PassthroughHLArea)),node_8076,_Mask_var.r)*lerp(float3(node_3626,node_3626,node_3626),_LightColor0.rgb,_LightColorModulation));
                float4 node_4210 = _Time;
                float2 node_345 = (sceneUVs.rg+node_4210.g);
                float4 _HoloOverlay_var = tex2D(_HoloOverlay,TRANSFORM_TEX(node_345, _HoloOverlay));
                float3 node_1358 = (_HoloOverlay_var.rgb*_HoloOverlayIntensity);
                float3 emissive = (node_4262+node_1358);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_6670.a*1.0));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _PassthroughHLArea;
            uniform float _PassthroughHLIntencity;
            uniform float4 _PassthroughBloomColor;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _LightColorModulation;
            uniform sampler2D _HoloOverlay; uniform float4 _HoloOverlay_ST;
            uniform float _HoloOverlayIntensity;
            uniform float _HoloTextureIntencity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float3 finalColor = 0;
                float4 node_6670 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                fixed4 finalRGBA = fixed4(finalColor * (node_6670.a*1.0),0);
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
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
