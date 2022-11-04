Shader "Abcight/PlanarHiganbana" {
    Properties {
        _MainTex("Sprite", 2D) = "white" {}
        _Width("Flower width", Float) = 0.14
        _Height("Flower height", Float) = 0.5
        _Randomness("Randomness", Range(0,1)) = 0.5
        _TessellationUniform("Tessellation", Range(1, 64)) = 1
        _WindSpeed("Wind speed", Float) = 1
        _WindStrength("Wind strength", Float) = 0.001
        _ClipmaskHeight("Clipmask height", Float) = 0.045
        _ClipmaskRadius("Clipmask radius", Float) = 0.02
    }
    SubShader {
        Cull off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off

        Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "LightMode" = "ForwardBase"
        }

        // Clipping mask
        Pass {
            ZWrite on
            Colormask 0
            LOD 200

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #pragma hull hull
            #pragma domain domain

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #pragma target 5.0

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

            TessellationFactors patchConstantFunction(InputPatch<vertexInput, 3> patch)
            {
                TessellationFactors f;
                f.edge[0] = _TessellationUniform;
                f.edge[1] = _TessellationUniform;
                f.edge[2] = _TessellationUniform;
                f.inside = _TessellationUniform;
                return f;
            }

            [UNITY_domain("tri")]
            [UNITY_outputcontrolpoints(3)]
            [UNITY_outputtopology("triangle_cw")]
            [UNITY_partitioning("integer")]
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

            uniform sampler2D _MainTex;
            uniform float _Height;
            uniform float _Width;
            uniform float _Randomness;

            uniform float _WindSpeed;
            uniform float _WindStrength;

            uniform float _ClipmaskHeight;
            uniform float _ClipmaskRadius;

            struct geometryOutput
            {
                float4 pos : SV_POSITION;
            };

            float4 frag(geometryOutput i) : COLOR
            {
                return float4(1,1,1,1);
            }

            geometryOutput VertexOutput(float3 pos)
            {
                geometryOutput o;
                o.pos = UnityObjectToClipPos(pos);
                return o;
            }

            float random(float3 pos)
            {
                return frac(sin(dot(pos.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
            }

            float rad(float deg)
            {
                return (deg / 180) * UNITY_PI;
            }

            float deg(float rad)
            {
                return rad * 180 / UNITY_PI;
            }

            float3 rY(float3 center, float3 vertex, float degree)
            {
                float angle = rad(degree); // Convert to radians

                float rotatedX = cos(angle) * (vertex.x - center.x) - sin(angle) * (vertex.z - center.z) + center.x;
                float rotatedZ = sin(angle) * (vertex.x - center.x) + cos(angle) * (vertex.z - center.z) + center.z;

                return float3(rotatedX, vertex.y, rotatedZ);
            }

            [maxvertexcount(30)]
            void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> geoStream)
            {
                float3 pos = IN[0].vertex;
                float3 vNormal = IN[0].normal;
                float4 vTangent = IN[0].tangent;
                float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;

                // Calculate blade size
                float height = _Height / 2;
                float width = _Width / 2;

                // Offset randomly
                float3 rand = random(pos) * _Randomness;
                rand.y = 0;
                pos += rand;

                // Wind
                float value = sin(distance(pos, float3(0, 0, 0) + _Time.x * _WindSpeed));
                float3 wind = float3(value, 0, value) * _WindStrength;
                wind.y = 0;

                float3 viewDir = UNITY_MATRIX_IT_MV[2].xyz;
                float3 clipmaskCenter = pos + float3(0, height - _ClipmaskHeight, 0) - viewDir/100;

                for (int i = 0; i < 5; i++)
                {
                    float angle = rad((360 / 5)) * i;
                    float nextAngle = rad((360 / 5)) * (i+1);

                    float3 p = clipmaskCenter + float3(cos(angle), sin(angle), 0) * _ClipmaskRadius;
                    float3 p2 = clipmaskCenter + float3(cos(nextAngle), sin(nextAngle), 0) * _ClipmaskRadius;
                    geoStream.Append(VertexOutput(p + wind));
                    geoStream.Append(VertexOutput(p2 + wind));
                    geoStream.Append(VertexOutput(clipmaskCenter + wind));

                    geoStream.Append(VertexOutput(rY(clipmaskCenter, p, 90) + wind));
                    geoStream.Append(VertexOutput(rY(clipmaskCenter, p2, 90) + wind));
                    geoStream.Append(VertexOutput(clipmaskCenter + wind));
                }
            }

            ENDCG
        }

        // Flower
        Pass {

            ZTest lequal
            LOD 200

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #pragma hull hull
            #pragma domain domain

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #pragma target 5.0

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

            TessellationFactors patchConstantFunction(InputPatch<vertexInput, 3> patch)
            {
                TessellationFactors f;
                f.edge[0] = _TessellationUniform;
                f.edge[1] = _TessellationUniform;
                f.edge[2] = _TessellationUniform;
                f.inside = _TessellationUniform;
                return f;
            }

            [UNITY_domain("tri")]
            [UNITY_outputcontrolpoints(3)]
            [UNITY_outputtopology("triangle_cw")]
            [UNITY_partitioning("integer")]
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

            uniform sampler2D _MainTex;
            uniform float _Height;
            uniform float _Width;
            uniform float _Randomness;

            uniform float _WindStrength;
            uniform float _WindSpeed;

            struct geometryOutput
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                unityShadowCoord4 _ShadowCoord : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };

            float4 frag(geometryOutput i) : COLOR
            {
                float4 col = tex2D(_MainTex, i.uv);
                col.w = saturate(floor(col.w*1.5f));

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

            float random(float3 pos)
            {
                return frac(sin(dot(pos.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
            }

            float rad(float deg)
            {
                return (deg / 180) * UNITY_PI;
            }

            float deg(float rad)
            {
                return rad * 180 / UNITY_PI;
            }

            float3 rY(float3 center, float3 vertex, float degree)
            {
                float angle = rad(degree); // Convert to radians

                float rotatedX = cos(angle) * (vertex.x - center.x) - sin(angle) * (vertex.z - center.z) + center.x;
                float rotatedZ = sin(angle) * (vertex.x - center.x) + cos(angle) * (vertex.z - center.z) + center.z;

                return float3(rotatedX, vertex.y, rotatedZ);
            }

            [maxvertexcount(12)]
            void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> geoStream)
            {
                float3 pos = IN[0].vertex;
                float3 vNormal = IN[0].normal;

                // Calculate blade size
                float height = _Height / 2;
                float width = _Width / 2;

                // Offset randomly
                float3 rand = random(pos) * _Randomness;
                rand.y = 0;
                pos += rand;

                // Wind
                float value = sin(distance(pos, float3(0, 0, 0) + _Time.x * _WindSpeed));
                float3 wind = float3(value, 0, value) * _WindStrength;
                wind.y = 0;

                // First quad
                geoStream.Append(VertexOutput( rY(pos, pos + float3(-width, 0, 0), 0),              float2(0, 0), vNormal));
                geoStream.Append(VertexOutput( rY(pos, pos + float3(-width, height, 0) + wind, 0),  float2(0, 1), vNormal));
                geoStream.Append(VertexOutput( rY(pos, pos + float3(width, height, 0) + wind, 0),   float2(1, 1), vNormal));
                geoStream.Append(VertexOutput( rY(pos, pos + float3(-width, 0, 0), 0),              float2(0, 0), vNormal) );
                geoStream.Append(VertexOutput( rY(pos, pos + float3(width, 0, 0), 0),               float2(1, 0), vNormal) );
                geoStream.Append(VertexOutput( rY(pos, pos + float3(width, height, 0) + wind, 0),   float2(1, 1), vNormal) );

                geoStream.RestartStrip();

                // Second quad
                geoStream.Append(VertexOutput(rY(pos, pos + float3(-width, 0, 0), 90),              float2(0, 0), vNormal));
                geoStream.Append(VertexOutput(rY(pos, pos + float3(-width, height, 0), 90) + wind,  float2(0, 1), vNormal));
                geoStream.Append(VertexOutput(rY(pos, pos + float3(width, height, 0), 90) + wind,   float2(1, 1), vNormal));
                geoStream.Append(VertexOutput(rY(pos, pos + float3(-width, 0, 0), 90),              float2(0, 0), vNormal));
                geoStream.Append(VertexOutput(rY(pos, pos + float3(width, 0, 0), 90),               float2(1, 0), vNormal));
                geoStream.Append(VertexOutput(rY(pos, pos + float3(width, height, 0), 90) + wind,   float2(1, 1), vNormal));
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
