// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unity Shaders Book/No.8/AlphaTest"
{
	Properties 
    {
		_Color ("Main Tint", Color) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_Cutoff("Alpha",Range(0,1)) = 0.5//透明度深浅
	}
	SubShader 
    {
		Tags
        { 
            //"Queue"="AlphaTest" //渲染队列
            "IgnoreProjector" = "True"//Shader不受投影器影响
            "RenderType" = "TransparentCutout"//指明使用透明度测试
        }
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
            float4 _MainTex_ST;
            fixed _Cutoff;

            struct a2v
            {
                float4 vertex : POSITION;
                float normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert(a2v i)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.worldNormal = UnityObjectToWorldNormal(i.normal);
                o.worldPos = mul(unity_ObjectToWorld,i.vertex);
                o.uv = TRANSFORM_TEX(i.texcoord,_MainTex);
                return o;
            }

            fixed4 frag(v2f o) : SV_Target
            {
                fixed3 worldNormal = normalize(o.worldNormal);
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(o.worldPos));
                fixed4 texColor = tex2D(_MainTex,o.uv);

                //Alpha Test
                clip(texColor.a - _Cutoff);
                /*
                if(texColor.a - _Cutoff < 0.0)
                {
                    discard;//丢弃片元
                }
                */

                fixed3 albedo = texColor.rgb * _Color.rgb;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0,dot(worldNormal,worldLightDir));
                return fixed4(ambient + diffuse,1.0);
            }

            ENDCG
        }
	}
	FallBack "Transparent/cutout/VertexLit"
}
