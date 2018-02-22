Shader "Custom/CameraTargetPatch" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float4x4 _MATRIX_MVP;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//
			float4 uvTmp;
	        // calculate new uv in camera image
	        uvTmp = mul(_MATRIX_MVP, float4(IN.uv_MainTex.x-0.5f,IN.uv_MainTex.y-0.5f,0,1));
	        normalize(uvTmp);
			//变换到区间[-0.5,0.5]以应用矩阵
	        uvTmp.x = uvTmp.x/uvTmp.w;
	        uvTmp.y = uvTmp.y/uvTmp.w;
	        uvTmp.z = uvTmp.z/uvTmp.w;
			//变换到区间[0,1]
	 		// some swap for different coordinate system
	        uvTmp.x = (uvTmp.x + 1.0f)/2.0f;
	        uvTmp.y = (uvTmp.y + 1.0f)/2.0f;
        	//


			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, uvTmp.xy) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
