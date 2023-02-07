Shader "Unlit/Xray"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_playerPos("ppos", float)=(0,0,0)
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" "DisableBatching" = "True" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work

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
				float4 worldPos: TEXCOORD1;
				float inside : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _playerPos;

			v2f vert(appdata v)
			{
				float3 vertVector = float3(v.vertex.x - _playerPos.x, v.vertex.y - _playerPos.y, v.vertex.z - _playerPos.z);
				float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y + vertVector.z * vertVector.z);
				v2f o;
				if (dist<1) {
					o.inside = 1;
				}
				else
				{
					o.inside = 0;
				}
				o.worldPos = v.vertex;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(1,0,0,1);
				float temp = i.worldPos.z;
				if (temp>0) {
					col = fixed4(1, 1, 1, 1);
				}
				float3 vertVector = float3(i.worldPos.x - _playerPos.x, i.worldPos.y - _playerPos.y, i.worldPos.z - _playerPos.z);//calc vector to curret fragment worldCOORDS
				float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y + vertVector.z * vertVector.z);//calc length of vector
				float illuminated;
				if (dist < 1.5) {
					illuminated = 1;
				}
				else
				{
					illuminated = 0;
				}
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				col *= illuminated;
				return col;
		}
		ENDCG
	}
	}
}
