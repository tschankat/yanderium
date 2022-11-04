Shader "Abcight/PlanarGrassBlades" {
    Properties {
        _BladeTopColor("Blade top color", Color) = (1,1,1,1)
        _BladeBottomColor("Blade bottom color", Color) = (1,1,1,1)
        _BladeBend("Blade bend", Range(0, 1)) = 0.2
        _BladeWidth("Blade width", Float) = 0.05
        _BladeWidthRandom("Blade width random", Float) = 0.02
        _BladeHeight("Blade height", Float) = 0.5
        _BladeHeightRandom("Blade height random", Float) = 0.3
        _TessellationUniform("Tessellation", Range(1, 64)) = 1
        _TessellationFalloffStart("Tessellation falloff start", Float) = 10
        _TessellationFalloffLength("Tesellation falloff length", Float) = 20
        _WindMap("Wind Map", 2D) = "white" {}
        _WindFrequency("Wind Frequency", Vector) = (0.05, 0.05, 0, 0)
        _WindStrength("Wind Strength", Float) = 1
    }
    SubShader {
        Cull Off

        Tags {
            "RenderType"="Opaque"
        }

        // Geometry
        Pass {
            Tags {
                "RenderType" = "Opaque"
                "LightMode" = "ForwardBase"
            }

            LOD 300

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #pragma hull hull
            #pragma domain domain

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #pragma target 4.6

            #include "UnityCG.cginc"
            #include "Autolight.cginc"
            #include "Lighting.cginc"
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct vertexOutput
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            vertexInput vert(vertexInput v)
            {
                return v;
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct TessellationFactors
            {
                float edge[3] : SV_TessFactor;
                float inside : SV_InsideTessFactor;
            };

            vertexOutput tessVert(vertexInput v)
            {
                vertexOutput o;
                o.vertex = v.vertex;
                o.normal = v.normal;
                o.tangent = v.tangent;
                return o;
            }

            uniform float _TessellationUniform;
            uniform float _TessellationFalloffStart;
            uniform float _TessellationFalloffLength;

            struct TessellationControlPoint
            {
                float4 vertex : INTERNALTESSPOS;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
            };

            float invlerp(float a, float b, float v)
            {
                return saturate((v - a) / (b - a));
            }

            float remap(float imin, float imax, float omin, float omax, float v)
            {
                float t = invlerp(imin, imax, v);
                return saturate(lerp(omin, omax, t));
            }

            TessellationFactors patchConstantFunction(InputPatch<vertexInput, 3> patch)
            {
                float3 p0 = mul(unity_ObjectToWorld, float4(patch[1].vertex.xyz, 1)).xyz;
                float3 p1 = mul(unity_ObjectToWorld, float4(patch[2].vertex.xyz, 1)).xyz;
                float3 center = (p0 + p1) * 0.5;
                float dist = distance(center, _WorldSpaceCameraPos);
                float inverse = remap(_TessellationFalloffStart, _TessellationFalloffStart + _TessellationFalloffLength, 1, 0, dist);

                TessellationFactors f;
                f.edge[0] = _TessellationUniform * inverse;
                f.edge[1] = _TessellationUniform * inverse;
                f.edge[2] = _TessellationUniform * inverse;
                f.inside = _TessellationUniform * inverse;
                return f;
            }

            [UNITY_domain("tri")]
            [UNITY_outputcontrolpoints(3)]
            [UNITY_outputtopology("triangle_cw")]
            [UNITY_partitioning("fractional_even")]
            [UNITY_patchconstantfunc("patchConstantFunction")]
            vertexInput hull(InputPatch<vertexInput, 3> patch, uint id : SV_OutputControlPointID)
            {
                return patch[id];
            }

            [UNITY_domain("tri")]
            vertexOutput domain(TessellationFactors factors, OutputPatch<vertexInput, 3> patch, float3 barycentricCoordinates : SV_DomainLocation)
            {
                vertexInput v;

                #define MY_DOMAIN_PROGRAM_INTERPOLATE(fieldName) v.fieldName = \
		            patch[0].fieldName * barycentricCoordinates.x + \
		            patch[1].fieldName * barycentricCoordinates.y + \
		            patch[2].fieldName * barycentricCoordinates.z;

                MY_DOMAIN_PROGRAM_INTERPOLATE(vertex)
                MY_DOMAIN_PROGRAM_INTERPOLATE(normal)
                MY_DOMAIN_PROGRAM_INTERPOLATE(tangent)

                return tessVert(v);
            }

            uniform float4 _BladeTopColor;
            uniform float4 _BladeBottomColor;
            uniform float _BladeBend;
            uniform float _BladeHeight;
            uniform float _BladeHeightRandom;
            uniform float _BladeWidth;
            uniform float _BladeWidthRandom;

            uniform sampler2D _WindMap;
            uniform float4 _WindMap_ST;
            uniform float2 _WindFrequency;
            uniform float _WindStrength;

            struct geometryOutput
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                unityShadowCoord4 _ShadowCoord : TEXCOORD1;
                UNITY_FOG_COORDS(3)
            };

            // Fake random function
            float random(float3 co)
            {
                return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
            }

            float3x3 angleM3(float angle, float3 axis)
            {
                float c, s;
                sincos(angle, s, c);

                float t = 1 - c;
                float x = axis.x;
                float y = axis.y;
                float z = axis.z;

                return float3x3(
                    t * x * x + c, t * x * y - s * z, t * x * z + s * y,
                    t * x * y + s * z, t * y * y + c, t * y * z - s * x,
                    t * x * z - s * y, t * y * z + s * x, t * z * z + c
                );
            }

            float4 frag(geometryOutput i, fixed facing : VFACE) : SV_Target
            {
                float4 col = lerp(_BladeBottomColor , _BladeTopColor, i.uv.y);
                col.w = saturate(floor(col.w * 1.5f));

                float shadow = SHADOW_ATTENUATION(i);
                float NdotL = saturate(saturate(dot(i.normal, _WorldSpaceLightPos0))) * shadow;

                float3 ambient = ShadeSH9(float4(i.normal, 1));
                float4 lightIntensity = NdotL * _LightColor0 + float4(ambient, 1);

                col = float4(col.xyz * lightIntensity.xyz, col.w);

                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }


            geometryOutput VertexOutput(float3 pos, float2 uv, float3 normal)
            {
                geometryOutput o;
                o.pos = UnityObjectToClipPos(pos);
                o.uv = uv;
                o._ShadowCoord = ComputeScreenPos(o.pos);
                o.normal = UnityObjectToWorldNormal(normal);

                UNITY_TRANSFER_FOG(o, o.pos);

                return o;
            }

            [maxvertexcount(3)]
            void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> geoStream)
            {
                float3 pos = IN[0].vertex;
                float3 vNormal = IN[0].normal;
                float4 vTangent = IN[0].tangent;
                float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;

                // Local tangents
                float3x3 tangentToLocal = float3x3(
                    vTangent.x, vBinormal.x, vNormal.x,
                    vTangent.y, vBinormal.y, vNormal.y,
                    vTangent.z, vBinormal.z, vNormal.z
                );

                // Blade transformation
                float3x3 facingRotationMatrix = angleM3(random(pos) * UNITY_TWO_PI, float3(0, 0, 1));
                float3x3 bendRotationMatrix = angleM3(random(pos.zzx) * _BladeBend * UNITY_PI * 0.5, float3(-1, 0, 0));
                float2 uv = pos.xz * _WindMap_ST.xy + _WindMap_ST.zw + _WindFrequency * _Time.y;
                float2 windSample = (tex2Dlod(_WindMap, float4(uv, 0, 0)).xy * 2 - 1) * _WindStrength;
                float3 wind = normalize(float3(windSample.x, windSample.y, 0));
                float3x3 windRotation = angleM3(UNITY_PI * windSample, wind);
                float3x3 transformationMatrix = mul(mul(mul(tangentToLocal, windRotation), facingRotationMatrix), bendRotationMatrix);
                float3x3 transformationMatrixFacing = mul(tangentToLocal, facingRotationMatrix);

                // Calculate blade size
                float height = (random(pos.zyx) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
                float width = (random(pos.xzy) * 2 - 1) * _BladeWidthRandom + _BladeWidth;

                // Append blade to the geometry stream
                geoStream.Append(VertexOutput( pos + mul(transformationMatrixFacing, float3(width, 0, 0)), float2(0, 0), vNormal));
                geoStream.Append(VertexOutput( pos + mul(transformationMatrix, float3(-width, 0, 0)), float2(1, 0), vNormal));
                geoStream.Append(VertexOutput( pos + mul(transformationMatrix, float3(0, 0, abs(height))), float2(0.5, 1), vNormal));
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
