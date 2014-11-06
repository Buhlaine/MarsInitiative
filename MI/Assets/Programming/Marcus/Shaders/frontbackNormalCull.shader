Shader "Custom/frontbackNormalCull" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NormalTex ( "Normal Map", 2D) = "bump" {}
	}
	SubShader {
		
			Tags {"Queue"="Transparent" "RenderType"="Transparent" }
			LOD 200
			Cull Off
			
			CGPROGRAM
			#pragma surface surf Lambert alpha

			sampler2D _MainTex;
			sampler2D _NormalTex;

			struct Input {
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				half4 c = tex2D (_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
				o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex));
			}
			ENDCG
	} 
	//FallBack "Diffuse"
}
