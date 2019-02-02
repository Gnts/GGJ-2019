Shader "Custom/ToonShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Threshold ("Threshold", Range(0,1)) = 0.5
		_Shade("Shade", Range(0,1)) = 0.7
		_OcStep("OC Step", Range(0,1)) = 0.9
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Toon fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float4 vertColor : COLOR;
        };

		struct CustomSurfaceOutput
		{
			half3 Albedo;
			half3 Normal;
			half Alpha;
			half Emission;
			half vertexOc;
		};

        half _Threshold;
		half _Shade;
		half _OcStep;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		half4 LightingToon(CustomSurfaceOutput s, half3 lightDir, half atten)
		{
			float oc = step(_OcStep, s.vertexOc);
			float NdotL = step(_Threshold, dot(s.Normal, atten));
			float toonL = min(1.0, NdotL + _Shade);
			half3 albedo = lerp(s.Albedo, s.Albedo * _LightColor0 * toonL, toonL);
			half4 c;
			c.rgb = albedo;
			// c.rgb = s.Albedo * NdotL;
			c.a = s.Alpha;
			return c;
		}

        void surf (Input IN, inout CustomSurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
			o.Emission = 0.0;
			o.vertexOc = IN.vertColor.r;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
