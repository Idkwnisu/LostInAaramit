Shader "Custom/LightShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Transparency("Transparency", Range(0.0,1)) = 0.8
		_LightVariance("Light Variance", Range(0.0,1)) = 0.4
		_Cube ("Cubemap", CUBE) = "" {}

	}
		SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

			LOD 200

			CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert alpha:fade 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
			float3 worldRefl;

			fixed x;
			fixed z;
		};

		float _Transparency;
		float _LightVariance;
		half _Glossiness;
		fixed4 _Color;
		samplerCUBE _Cube;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.x = abs(v.vertex.x);
			o.z = abs(v.vertex.z);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			
			o.Albedo = c.rgb;
			o.Emission = texCUBE (_Cube, IN.worldRefl).rgb;
			fixed ray = sqrt(IN.x*IN.x + IN.z*IN.z);
			o.Alpha = _Transparency + _LightVariance * abs(sin(_Time[0]*50));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
