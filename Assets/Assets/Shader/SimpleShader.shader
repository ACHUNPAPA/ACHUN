﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Shader "Custom/SimpleShaderMat" {
//	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
		
//		CGPROGRAM
//		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows

//		// Use shader model 3.0 target, to get nicer looking lighting
//		#pragma target 3.0

//		sampler2D _MainTex;

//		struct Input {
//			float2 uv_MainTex;
//		};

//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;

//		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//		// #pragma instancing_options assumeuniformscaling
//		UNITY_INSTANCING_CBUFFER_START(Props)
//			// put more per-instance properties here
//		UNITY_INSTANCING_CBUFFER_END

//		void surf (Input IN, inout SurfaceOutputStandard o) {
//			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
//			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	}
//	FallBack "Diffuse"
//}


shader "Unity Shaders Book/No.5/Simple Shader"
{
    Properties
    {
        _Color("Color Tint",Color) = (1.0,1.0,1.0,1.0)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;

            //使用结构体定义顶点着色器的输入
            struct a2v
            {
                //POSITION语义告诉unity：用模型空间的顶点坐标填充vertex变量
                float4 vertex : POSITION;
                //NORMAL语义告诉unity：用模型空间的法线方向填充normal变量
                float3 normal : NORMAL;
                //TEXCOORD0语义告诉unity：用模型的第一套纹理坐标填充texcoord变量
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert(a2v v)
            {
                //return UnityObjectToClipPos(v.Vertex);
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.normal * 0.5 + fixed3(0.5,0.5,0.5);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 c = i.color;
                c *= _Color.rgb;
                return fixed4(c,1.0);
            }
            ENDCG
        }
    }
}