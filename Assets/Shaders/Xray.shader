//Shader "Unlit/Xray"
//{
//	Properties
//	{
//		_MainTex("Texture", 2D) = "white" {}
//		_playerPos("ppos", Vector) = (0,0,0,1)
//		_objectPosition("objectPos", Vector) = (0,0,0,1)
//		_objectScale("objectScale", Vector) = (1,1,1,1)
//		_pulseLength("pulseLen", float) = 5
//	}
//		SubShader
//	{
//		Tags { "RenderType" = "Opaque" "DisableBatching" = "True" }
//		LOD 100
//
//		Pass
//		{
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			// make fog work
//
//			#include "UnityCG.cginc"
//
//			struct appdata
//			{
//				float4 vertex : POSITION;
//				float2 uv : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float2 uv : TEXCOORD0;
//				float4 vertex : SV_POSITION;
//				float4 worldPos: TEXCOORD1;
//				float inside : TEXCOORD2;
//			};
//
//			sampler2D _MainTex;
//			float4 _MainTex_ST;
//			float4 _playerPos;
//			float4 _objectPosition;
//			float4 _objectScale;
//			float _pulseLength;
//			v2f vert(appdata v)
//			{
//				
//				float3 vertVector = float3(v.vertex.x - _playerPos.x, v.vertex.y - _playerPos.y, v.vertex.z - _playerPos.z);
//				float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y + vertVector.z * vertVector.z);
//				v2f o;
//				if (dist<1) {
//					o.inside = 1;
//				}
//				else
//				{
//					o.inside = 0;
//				}
//				o.vertex = UnityObjectToClipPos(v.vertex);
//				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				v.vertex = float4(v.vertex.x * _objectScale.x, v.vertex.y * _objectScale.y, v.vertex.z * _objectScale.z, v.vertex.w);
//				o.worldPos = v.vertex+ _objectPosition;
//				return o;
//			}
//
//			fixed4 frag(v2f i) : SV_Target
//			{
//				fixed4 col = fixed4(1,0,0,1);
//				float temp = i.worldPos.z;
//				if (temp>0) {
//					col = fixed4(1, 1, 1, 1);
//				}
//				float3 vertVector = float3(i.worldPos.x - _playerPos.x, i.worldPos.y - _playerPos.y, i.worldPos.z - _playerPos.z);//calc vector to curret fragment worldCOORDS
//				float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y + vertVector.z * vertVector.z);//calc length of vector
//				float illuminated;
//				if (dist < _pulseLength&&dist>_pulseLength-4) {
//					illuminated = 1;
//				}
//				else
//				{
//					illuminated = 0;
//				}
//				// sample the texture
//				//fixed4 col = tex2D(_MainTex, i.uv);
//				col *= illuminated;
//				return col;
//		}
//		ENDCG
//	}
//	}
//}
Shader "Unlit/WireframeSimple"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _WireframeFrontColour("Wireframe front colour", color) = (1.0, 1.0, 1.0, 1.0)
        _WireframeBackColour("Wireframe back colour", color) = (1, 1, 1, 1.0)
        _WireframeWidth("Wireframe width threshold", float) = 0.05
        _playerPos("ppos", Vector) = (0,0,0,1)
        _objectPosition("objectPos", Vector) = (0,0,0,1)
        _objectScale("objectScale", Vector) = (1,1,1,1)
        _pulseLength("pulseLen", float) = 5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Transparent"}
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                // Removes the front facing triangles, this enables us to create the wireframe for those behind.
                Cull Off
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma geometry geom
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

            };

            // We add our barycentric variables to the geometry struct.
            struct g2f {
                float4 pos : SV_POSITION;
                float3 barycentric : TEXCOORD0;
                float4 worldPos: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _playerPos;
            float4 _objectPosition;
            float4 _objectScale;
            float _pulseLength;
            float4 wplz;
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                v.vertex = float4(v.vertex.x * _objectScale.x, v.vertex.y * _objectScale.y, v.vertex.z * _objectScale.z, v.vertex.w);
                o.worldPos = v.vertex+ _objectPosition;

                return o;
            }

            // This applies the barycentric coordinates to each vertex in a triangle.
            [maxvertexcount(3)]
            void geom(triangle v2f IN[3], inout TriangleStream<g2f> triStream) {
                g2f o;
                o.pos = IN[0].vertex;
                o.worldPos = IN[0].worldPos;
                o.barycentric = float3(1.0, 0.0, 0.0);
                triStream.Append(o);
                o.pos = IN[1].vertex;
                o.worldPos = IN[1].worldPos;
                o.barycentric = float3(0.0, 1.0, 0.0);
                triStream.Append(o);
                o.pos = IN[2].vertex;
                o.worldPos = IN[2].worldPos;
                o.barycentric = float3(0.0, 0.0, 1.0);
                triStream.Append(o);
            }

            fixed4 _WireframeBackColour;
            float _WireframeWidth;

            fixed4 frag(g2f i) : SV_Target
            {
                // Find the barycentric coordinate closest to the edge.
                float closest = min(i.barycentric.x, min(i.barycentric.y, i.barycentric.z));
            // Set alpha to 1 if within the threshold, else 0.
            float alpha = step(closest, _WireframeWidth);

            float2 vertVector = float2(i.worldPos.x - _playerPos.x, i.worldPos.z - _playerPos.z);//calc vector to curret fragment worldCOORDS
				float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y);//calc length of vector
				float illuminated;
				if (dist < _pulseLength&&dist>_pulseLength-8) {
					illuminated = 1;
				}
				else
				{
					illuminated = 0;
				}

            // Set to our backwards facing wireframe colour.
            fixed4 temp = fixed4(_WireframeBackColour.r, _WireframeBackColour.g, _WireframeBackColour.b, alpha);
            fixed4 temp2 = fixed4(1, 1, 1, 1);
            fixed4 temp3 = fixed4(illuminated, illuminated, illuminated, 1);
            return temp* illuminated;
        }
        ENDCG
    }

   
        }
}