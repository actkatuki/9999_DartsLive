Shader "Custom/Wroker Dither" {
	Properties {
		_MainTex  ("Albedo Tex", 2D         ) = "white" {}
		_BumpMap  ("Normal Tex", 2D         ) = "bump" {}
		_Color    ("Color"     , Color      ) = (1, 1, 1, 1)
		_Gloss    ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic"  , Range(0, 1)) = 0.0
		_Cutoff   ("Cutoff"    , Range(0, 1)) = 0.5
		_Alpha    ("Alpha"     , Range(0, 1)) = 1.0

		_Emission("Emission",Range(0,1)) = 0
	}

	SubShader {
		Tags {
			"Queue"      = "AlphaTest"
			"RenderType" = "TransparentCutout"
		}

		Cull Back
		
		CGPROGRAM
			#pragma target 5.0
			#pragma surface surf Standard fullforwardshadow alphatest:_Cutoff

			sampler3D _DitherMaskLOD;

			sampler2D _MainTex;
			sampler2D _BumpMap;
			half3 _Color;
			half _Gloss;
			half _Metallic;
			half _Alpha;
			half _Emission;

			struct Input {
				float2 uv_MainTex;
				float4 screenPos;
			};

			void surf (Input IN, inout SurfaceOutputStandard o) {
				half alpha = tex3D(_DitherMaskLOD, float3(IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy * 0.25, _Alpha * 0.9375)).a;
				clip(alpha - 0.01);

				fixed3 mainTex   = tex2D(_MainTex, IN.uv_MainTex).rgb;
				fixed4 normalTex = tex2D(_BumpMap, IN.uv_MainTex);

				o.Albedo     = mainTex.rgb * _Color;
				o.Metallic   = _Metallic;
				o.Smoothness = _Gloss;
				o.Normal     = UnpackNormal(normalTex);
				o.Alpha      = 1;
				o.Emission =mainTex.rgb * _Emission;
			}
		ENDCG

		Cull Front
		
		CGPROGRAM
			#pragma target 5.0
			#pragma surface surf Lambert fullforwardshadow alphatest:_Cutoff

			sampler3D _DitherMaskLOD;

			sampler2D _MainTex;
			half3 _Color;
			half _Alpha;

			struct Input {
				float2 uv_MainTex;
				float4 screenPos;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				half alpha = tex3D(_DitherMaskLOD, float3(IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy * 0.25, _Alpha * 0.9375)).a;
				clip(alpha - 0.01);

				fixed3 mainTex   = tex2D(_MainTex, IN.uv_MainTex).rgb;

				o.Albedo = mainTex * _Color * 0.1;
				o.Alpha  = 1;
			}
		ENDCG
	}

	FallBack "Standard"
}
