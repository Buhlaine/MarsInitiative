Shader "Shaders/cutoffSecondaryShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SecondaryTex ("Cutoff Texture", 2D) = "white"{}
		_Cutoff ("alpha Cutoff", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff

		sampler2D _MainTex;
		sampler2D _SecondaryTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondaryTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 secondary = tex2D (_SecondaryTex, IN.uv_SecondaryTex);
			o.Albedo = c.rgb;
			o.Alpha = secondary.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
