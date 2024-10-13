Shader "Custom/CubeMapUnwrap" {
    Properties {
        _CubeMap ("Cube Map", Cube) = "gray" {}
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            samplerCUBE _CubeMap;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // 根据UV坐标计算对应的立方体贴图方向
                float3 direction;
                if (i.uv.x < 1.0 / 3.0) {
                    direction = float3(-1, 2 * i.uv.y - 1, 2 * i.uv.x - 1);
                } else if (i.uv.x < 2.0 / 3.0) {
                    direction = float3(2 * i.uv.x - 1, 2 * i.uv.y - 1, -1);
                } else {
                    direction = float3(2 * i.uv.x - 1, 2 * i.uv.y - 1, 1);
                }
                direction = normalize(direction);

                // 从立方体贴图中采样颜色
                fixed4 col = texCUBE(_CubeMap, direction);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}