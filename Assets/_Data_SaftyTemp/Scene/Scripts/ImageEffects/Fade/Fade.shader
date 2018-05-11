Shader "Hidden/Fade" {
	Properties {
		_MainTex ("Base (RGB)", 2D         ) = "white" {}
		_Color   ("Color"     , Color      ) = (1, 1, 1, 1)
		_Amount  ("Amount"    , Range(0, 1)) = 1
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
					
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				half4 _MainTex_ST;
				half3 _Color;
				half _Amount;

				fixed4 frag (v2f_img i) : SV_Target {
					fixed4 source = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));

					return fixed4(lerp(source, _Color, _Amount), 1);
				}
			ENDCG
		}
	}

	Fallback off
}