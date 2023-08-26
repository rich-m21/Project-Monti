Shader "XD Paint/Depth To World Position"
{
    Properties { }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ SOFTPARTICLES_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
            float4x4 _Matrix_IVP;
            float2 _ScreenUV;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
#ifdef SOFTPARTICLES_ON
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, _ScreenUV);

                #if UNITY_REVERSED_Z
                    if (depth < 0.0001)
                        return 0;
                #else
                    if (depth > 0.9999)
                        return 0;
                #endif
                
                float4 positionCS = float4(_ScreenUV * 2.0 - 1.0, depth, 1.0);
                
                #if UNITY_UV_STARTS_AT_TOP
                // positionCS.y = -positionCS.y;
                #endif

                float4 hpositionWS = mul(_Matrix_IVP, positionCS);
                float3 worldPos = hpositionWS.xyz / hpositionWS.w;
                float sceneZ = LinearEyeDepth(depth);
                return float4(worldPos, sceneZ);
#else
                return 0;
#endif
            }

            ENDCG
        }
    }
}