Shader "Unlit/FadeOut"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _fadeOut("fadeOut", float) = 0
        _fadeDuration("fadeDuration",float) = 3
        _shouldFade("shouldFade", float) = 0
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
            float _fadeTimer;
            float _fadeDuration;
            float _shouldFade;
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
                fixed4 black = fixed4(0, 0, 0, 1);
                float lerpProgress=1;
                if (_fadeTimer< _fadeDuration) {
                    lerpProgress = _fadeTimer / _fadeDuration;
                }

                if(_shouldFade ==1){
                    col = lerp(col, black, lerpProgress);
                }
                return col;
            }
            ENDCG
        }
    }
}
