Shader "Custom/AB_Vignette" 
{
	Properties 
	{
		_MainTex ("", 2D) = "white" {}
 		_bias ("bias" , Range (0,1)) = 0
 		_smoothness ("smoothness" , Range (0,1)) = 1
 		
	}
	SubShader 
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bias , _smoothness;
			uniform float4 _Col;
			int _X;
			int _Y;

			float4 frag (v2f_img i) : COLOR
			{
				//float2 ScreenParams = float2(_X , _Y);
				float2 ScreenParams = float2(600 , 338);

				float4 c = tex2D(_MainTex, i.uv);
				float2 s = (i.uv) / ScreenParams;
				s *= (1-i.uv) / ScreenParams;
				s *= 1-s.xy;

				float vig = s.x * s.y;
				vig = pow (vig , .25) * 1900;
				//vig = clamp(0,1,(vig + _bias) * _smoothness);
				vig = smoothstep (_bias-_smoothness,_bias+_smoothness,vig);


				return lerp(_Col, 1 , vig) * c;
			}
			ENDCG
		}
	}
}






