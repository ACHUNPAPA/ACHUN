// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unity Shaders Book/No.7/Single Texture"
{
    Properties
    {
        _Color("Color Tint",Color) = (1,1,1,1)
        _MainTex("Main Texture",2D) = "white"{}
        _Specular("Specular",Color) = (1,1,1,1)
        _Gloss("Gloss",Range(8.0,256)) = 20
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

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;//[Name]_ST为纹理属性值，xy为缩放，zw为位移
            fixed4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;//第一组纹理的纹理坐标
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;//语义自定义
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//法线世界坐标
                o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;//顶点世界坐标
                o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;//获得uv，先进行* xy线性变换，在+ zw位移
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 worldNormal = normalize(i.worldNormal);//法线世界坐标
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));//该点到光源的方向

                fixed3 albedo = tex2D(_MainTex,i.uv).rgb * _Color.rgb;//使用tex2D函数对纹理采样，乘以可编辑颜色值获取材质反射率
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;//获取环境光反射后的颜色
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0,dot(worldNormal,worldLightDir));//使用材质反射率计算漫反射结果

                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed3 halfDir = normalize(worldLightDir + viewDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0,dot(worldNormal,halfDir)),_Gloss);//高光计算方式不变（纹理不影响）
                return fixed4(ambient + diffuse + specular,1.0);
            }
            ENDCG
        }
    }
    FallBack "Specular"
}
