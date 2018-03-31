// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unity Shaders Book/No.6/Diffuse Pixel-Level"
{
    Properties
    {
        _Diffuse("Diffuse",Color) = (1,1,1,1)
        _A("A",Range(0,1)) = 0.5
        _B("B",Range(0,1)) = 0.5
    }

    SubShader
    {
        Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            fixed4 _Diffuse;
            fixed _A;
            fixed _B;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = mul(v.normal,(float3x3)unity_WorldToObject);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;//环境光颜色
                fixed3 worldNormal = normalize(i.worldNormal);//法线向量世界坐标
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);//光源向量
                //fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal,worldLightDir));//兰伯特模型
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * (dot(worldNormal,worldLightDir) * _A + _B);//半兰伯特模型
                fixed3 color = ambient + diffuse;
                return fixed4(color,1.0);
            }
            ENDCG
        }
    }
}