Shader "Oxysoft/RetroPixelMax" {
	Properties {
		_ColorCount ("Color Count", Int) = 8
	 	_MainTex ("", 2D) = "white" {}
		_Threshold ("Threshold", float) = 0.1
	}

	SubShader {
		Lighting Off
		ZTest Always
		Cull Off
		ZWrite Off
		Fog { Mode Off }

	 	Pass {
	  		CGPROGRAM
			// Upgrade NOTE: excluded shader from DX11, Xbox360, OpenGL ES 2.0 because it uses unsized arrays
	  		#pragma exclude_renderers flash
	  		#pragma vertex vert_img
	  		#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	  		#include "UnityCG.cginc"

	  		uniform int _ColorCount;
			uniform fixed4 _Colors[256];
	  		uniform sampler2D _MainTex;
	  		uniform float _Threshold;

			float linearDistance(fixed3 c1, fixed3 c2)
			{
				return sqrt(
					pow(c1.r-c2.r, 2) + 
					pow(c1.g-c2.g, 2) + 
					pow(c1.b-c2.b, 2));
			}

			float redmean(float3 c1, float3 c2)
			{
				float r = (c1.r + c2.r) * 0.5;
				return sqrt(
					(2+(r/256)) * pow((c1.r-c2.r), 2) +
					4 * pow((c1.g-c2.g), 2) +
					(2+ ((255-r)/256)) * pow((c1.b-c2.b), 2));
			}

			float toCurve(float x)
			{
				return x;
			}

			fixed4 rgbToHsv(fixed4 c)
			{
				float maxC = max(max(c.r, c.g), c.b);
				float minC = min(min(c.r, c.g), c.b);
				fixed v = maxC/255;
				fixed s = (maxC > 0) ? 1 - (minC/maxC) : 0;
				fixed h = (c.b > c.g) ?
					360 - acos((c.r - 0.5*c.g - 0.5*c.b)/(sqrt(pow(c.r, 2)+pow(c.g, 2)+pow(c.b, 2)-(c.r*c.g)-(c.r*c.b)-(c.b*c.g)))) : 
					acos((c.r - 0.5*c.g - 0.5*c.b)/(sqrt(pow(c.r, 2)+pow(c.g, 2)+pow(c.b, 2)-(c.r*c.g)-(c.r*c.b)-(c.b*c.g))));

				return fixed4 (h,s,v,c.a);
			}

	  		fixed4 frag (v2f_img i) : COLOR
	  		{
	   			fixed4 original = tex2D (_MainTex, i.uv);

	   			fixed4 col = fixed4 (0,0,0,0);
	   			float dist = 10000000.0;
				bool force = false;

	   			for (int i = 0; i < _ColorCount; i++) {
					if (!force){
	   					float4 c = _Colors[i];
	   					float d = toCurve(redmean(original.rgb, c.rgb));

	   					if (d < dist) {
	   						dist = d;
	   						col = c;
	   					}

						//force = d < _Threshold;
					}
	   			}
				//if (force) col = original;

				return col;
	  		}

	  		ENDCG
	 	}
	}

	FallBack "Diffuse"
}
