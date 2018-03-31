Shader "Unity Shaders Book/No.8/AlphaBlendZWrite" 
{
	Properties 
    {
		_Color ("Main Tint", Color) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_AlphaScale("Alpha Scale", Range(0,1)) = 1
	}
	SubShader 
    {
		Tags 
        {
            "Queue"="AlphaTest" 
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutout"    
        }

        Pass
        {//写入深度缓存专用Pass
            ZWrite On
            ColorMask 0//该Pass不输出任何颜色
        }

		Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
            }
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _AlphaScale;

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

                fixed3 albedo = texColor.rgb * _Color.rgb;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0,dot(worldNormal,worldLightDir));
                return fixed4(ambient + diffuse,texColor.a * _AlphaScale);
            }

            ENDCG
        }
	}
	FallBack "Diffuse"
}
