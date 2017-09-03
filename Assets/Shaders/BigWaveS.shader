Shader "Custom/BigWaveS" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_RotationSpeed ("RotationSpeed", Range(-5,5)) = -0.17
		_Speed ("speed", Range(-5,5)) = 0.0
		_WavePos ("WavePos", Vector)  = (1,1,1,1) 
		_WhiteWaves("WhiteWaves", Range(0,1))  = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow fullforwardshadows vertex:vert nolightmap

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		sampler2D _MainTex;
		float3 _WavePos;
		float _Speed;
		float _RotationSpeed;
		float _Tess;
		float _WhiteWaves;

		struct Input {
			float2 uv_MainTex;
   			float3 pos;
		};
		struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void vert(inout appdata_full v, out Input o) {  

            _WavePos.x = _WavePos.x - _Speed;  
			half2 worldSpaceVertex = mul(unity_ObjectToWorld,_WavePos.xyz).xy;
			float dist = distance(v.vertex.xy, worldSpaceVertex);
			
            float sinX = sin ( _RotationSpeed  * 1/ dist /3);
            float cosX = cos ( _RotationSpeed  * 1/ dist *2);
            float sinY = sin ( _RotationSpeed  * 1/ dist);
            float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
            fixed2 n = mul ( v.vertex.xy , rotationMatrix ); 
            v.vertex.xy = n; 

            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.pos = v.vertex; 
			//v.vertex.y += 1/ dist ;
		}
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color + (IN.pos.y * IN.pos.y * IN.pos.y * _WhiteWaves);
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