Shader "Hidden/NightVisionEffect" {
 
	Properties {
		_MainTex ("Base (RGB)", RECT) = "white" {}
	}
 
	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
 
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#include "UnityCG.cginc"
 
				uniform sampler2D _MainTex;
				uniform float4 _Luminance;
 
				float4 frag (v2f_img i) : COLOR
				{	
					float4 col = tex2D(_MainTex, i.uv);
					col = dot(col, _Luminance);
					col.r = max (col.r - 0.75, 0) * 4;
 
					return col;
				}
			ENDCG
		}
	}
 
	Fallback off
}