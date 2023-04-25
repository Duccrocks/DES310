Shader "Unlit/ColourBlindGL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColourBlindVector("colourBlindVect", vector) = (1,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ColourBlindVector;

            float3 RGBtoLMS(float3 input) {
                float3 lms = 0;
                lms[0] = (17.8824 * input.r) + (43.5161 * input.g) + (4.11935 * input.b);
                lms[1] = (3.45565 * input.r) + (27.1554 * input.g) + (3.86714 * input.b);
                lms[2] = (0.0299566 * input.r) + (0.184309 * input.g) + (1.46709 * input.b);
                return lms;
            }

            float3 Correction(float3 input, float3 dlms) {
                float3 err = 0;
                err.r = (0.0809444479 * dlms[0]) + (-0.130504409 * dlms[1]) + (0.116721066 * dlms[2]);
                err.g = (-0.0102485335 * dlms[0]) + (0.0540193266 * dlms[1]) + (-0.113614708 * dlms[2]);
                err.b = (-0.000365296938 * dlms[0]) + (-0.00412161469 * dlms[1]) + (0.693511405 * dlms[2]);
                err = (input - err);
                float3 correction = 0;
                correction.g = (err.r * 0.7) + (err.g * 1.0);
                correction.b = (err.r * 0.7) + (err.b * 1.0);
                return input + correction;
            }

            float3 Protanopia(float3 lms) {
                float3 dlms = 0;
                dlms[0] = 0.0 * lms[0] + 2.02344 * lms[1] + -2.52581 * lms[2];
                dlms[1] = 0.0 * lms[0] + 1.0 * lms[1] + 0.0 * lms[2];
                dlms[2] = 0.0 * lms[0] + 0.0 * lms[1] + 1.0 * lms[2];
                return dlms;
            }

            float3 Deuteranopia(float3 lms) {
                float3 dlms = 0;
                dlms[0] = 1.0 * lms[0] + 0.0 * lms[1] + 0.0 * lms[2];
                dlms[1] = 0.494207 * lms[0] + 0.0 * lms[1] + 1.24827 * lms[2];
                dlms[2] = 0.0 * lms[0] + 0.0 * lms[1] + 1.0 * lms[2];
                return dlms;
            }

            float3 Tritanopia(float3 lms) {
                float3 dlms = 0;
                dlms[0] = 1.0 * lms[0] + 0.0 * lms[1] + 0.0 * lms[2];
                dlms[1] = 0.0 * lms[0] + 1.0 * lms[1] + 0.0 * lms[2];
                dlms[2] = -0.395913 * lms[0] + 0.801109 * lms[1] + 0.0 * lms[2];
                return dlms;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 lms = RGBtoLMS(float3(col.r,col.b,col.g));
                //return col;
                float3 trit = Tritanopia(lms);
                float3 deut = Deuteranopia(lms);
                float3 prot = Protanopia(lms);

                float3 dlms = col.rbg * _ColourBlindVector.x + trit * _ColourBlindVector.y + deut * _ColourBlindVector.z + prot * _ColourBlindVector.w;
                if (_ColourBlindVector.x==0) {
                    return fixed4(Correction(col, dlms), col.a);
                }
                else
                {
                    return col;
                }
                
            }
            ENDCG
        }
    }
}
