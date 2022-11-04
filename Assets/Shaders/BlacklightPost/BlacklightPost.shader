Shader "Abcight/BlacklightPost" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _DepthDistance("Fog depth", Float) = 1
        _FogColorDark("Dark fog color", Color) = (1,1,1,1)
        _FogColorLight("Light fog color", Color) = (1,1,1,1)
        _FogOpacity("Fog opacity", Float) = 1
        _GlowBias("Glow bias", Float) = 1
        _GlowColor("Glow color", Color) = (1,1,1,1)
        _GlowColor2("Glow color secondary", Color) = (1,1,1,1)
        _GlowAmount("Glow amount", Float) = 1
        _HighlightColor("Highlight color", Color) = (1,1,1,1)
        _EdgeColor("Edge color", Color) = (1,1,1,1)
        _EdgeThreshold("Edge threshold", Float) = 0.01
        _EdgeStrength("Edge strength", Float) = 1
        _OverlayTop("Overlay top", Color) = (1,1,1,1)
        _OverlayBottom("Overlay bottom", Color) = (1,1,1,1)
        _OverlayOpacity("Overlay opacity", Float) = 0.1
        _HighlightFlip("Highglight flip", Float) = 0
		_HighlightTargetSmooth("Smooth highlight", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            ZWrite Off
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0

            sampler2D _CameraDepthTexture;
            sampler2D _CameraDepthNormalsTexture;
            uniform sampler2D _MainTex;

            uniform float _DepthDistance;
            uniform float4 _FogColorDark;
            uniform float4 _FogColorLight;
            uniform float _FogOpacity;

            uniform float _GlowBias;
            uniform float4 _GlowColor;
            uniform float4 _GlowColor2;
            uniform float _GlowAmount;

            uniform float4 _HighlightTargets[100];
            uniform float _HighlightTargetThresholds[100];
			uniform float4 _HighlightColors[100];
            uniform float _SmoothColorInterpolations[100];
            uniform int _HighlightTargetsLength; // Optimization
            uniform float4 _HighlightColor;
			uniform float _HighlightTargetSmooth;

            uniform float4 _OverlayTop;
            uniform float4 _OverlayBottom;
            uniform float _OverlayOpacity;

            uniform float _HighlightFlip;

            uniform float4 _EdgeColor;
            uniform float _EdgeThreshold;
            uniform float _EdgeStrength;
            float4 _MainTex_TexelSize;

            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float3 rgbHsv(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }
            float3 hsvRgb(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }
            float4 pixelDepthValue(in float2 uv)
            {
                half3 normal;
                float depth;
                DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, uv), depth, normal);
                return fixed4(normal, depth);
            }
            float3 edgeDetect(fixed4 col, float2 uv)
            {
                fixed4 pVal = pixelDepthValue(uv);
                float2 offsets[8] = {
                    float2(-1, -1),
                    float2(-1, 0),
                    float2(-1, 1),
                    float2(0, -1),
                    float2(0, 1),
                    float2(1, -1),
                    float2(1, 0),
                    float2(1, 1)
                };
                fixed4 sampled = fixed4(0, 0, 0, 0);
                for (int j = 0; j < 8; j++)
                {
                    sampled += pixelDepthValue(uv + offsets[j] * _MainTex_TexelSize.xy);
                }
                // Remember to divide by the length of offsets!
                sampled /= 8;

                return lerp(fixed4(0,0,0,0), _EdgeColor, step(_EdgeThreshold, length(pVal - sampled))).xyz;
            }
            float4 frag(VertexOutput i) : COLOR{

                fixed4 pixel = tex2D(_MainTex, i.uv0);
                fixed4 col = lerp(pixel, 1 - pixel, _HighlightFlip);

                // Get desaturated glowmask
                float3 hsv = rgbHsv(col.xyz);
                float3 desaturated = hsvRgb(float3(hsv.x, 0, hsv.b));
                float3 glowMask = pow(desaturated, _GlowBias) * _GlowBias;

                // Get desaturated normals
                float3 normal = rgbHsv(tex2D(_CameraDepthNormalsTexture, i.uv0).xyz);
                normal = hsvRgb(float3(normal.x, 0, normal.z));

                // Grab the depth
                float depth = saturate(tex2D(_CameraDepthTexture, i.uv0).r * _DepthDistance);
                float depthSaturated = saturate((desaturated * depth).r);

                // Create fog based on depth
                float3 fog = lerp(_FogColorDark, _FogColorLight, depthSaturated);
                float3 coloredFog = lerp(col.xyz, fog, _FogOpacity);

                // Get edges from scene normals
                float3 edge = edgeDetect(col, i.uv0);

                // Calculate glow
                float3 glow = glowMask * depth * lerp(_GlowColor, _GlowColor2, normal.r);

                // Overlay
                float3 overlay = lerp(_OverlayBottom, _OverlayTop, i.uv0.y) * _OverlayOpacity;

                // Targetted highlighting
                float3 highlightMap = float3(0, 0, 0);
                for (int i = 0; i < _HighlightTargetsLength; i++)
                {
                    float3 color = _HighlightTargets[i].xyz;

					// Smooth calculation
					float dist = (-distance(pixel.xyz, color)+1-_HighlightTargetThresholds[i]);

					// Step calculation 
                    float3 contrib = step(_HighlightTargetThresholds[i], 1-distance(pixel.xyz, color));;
                    float3 contribInterp = distance(normalize(pixel.xyz), normalize(color));

                    // Add to highlight map
					highlightMap += lerp( lerp(contrib, dist, _HighlightTargetSmooth) * _HighlightColors[i], 
                                          lerp(_HighlightColors[i], pixel.xyz, contribInterp), 
                                          _SmoothColorInterpolations[i]);
                }

                float3 highlight = saturate(highlightMap);//saturate(highlightMap) * _HighlightColor;

                return fixed4(saturate(edge * depth * _EdgeStrength + (glow * _GlowAmount) + coloredFog + highlight) + overlay, 1);
            }
            ENDCG
        }
    }
}
