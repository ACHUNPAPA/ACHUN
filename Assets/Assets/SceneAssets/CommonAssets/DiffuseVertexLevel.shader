// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unity Shaders Book/No.6/Diffuse Vertex-Level"
{
    Properties
    {
        //漫反射颜色
        _Diffuse("Diffuse",Color) = (1,1,1,1) 
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

            fixed4 _Diffuse;//由于颜色值在0~1之间，所以使用fixed4（11位浮点数，精度范围-2.0~+2.0）来存储
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;//环境光

                //计算漫反射
                fixed3 worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));//法线世界坐标
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);//光源向量
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal,worldLight));//兰伯特模型
                
                //fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * (dot(worldNormal,worldLight) * 0.5 + 0.5);//半兰伯特模型
                o.color = ambient + diffuse;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(i.color,1.0);        
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}