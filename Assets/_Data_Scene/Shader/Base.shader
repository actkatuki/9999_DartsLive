// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Base"
{
	Properties
	{
		_Albedo_Base("Albedo_Base", 2D) = "white" {}
		_Albedo_Sub("Albedo_Sub", 2D) = "white" {}
		_MS("MS", 2D) = "white" {}
		_MSO_Sub("MSO_Sub", 2D) = "white" {}
		_Metal("Metal", Range( 0 , 1)) = 0
		_Smooth("Smooth", Range( 0 , 1)) = 1
		_AO("AO", Range( 0 , 1)) = 1
		_Normal("Normal", 2D) = "bump" {}
		_Normal_Sub("Normal_Sub", 2D) = "bump" {}
		_NormalMax("NormalMax", Range( 0 , 1)) = 1
		_NormalMin("NormalMin", Range( 0 , 1)) = 0
		_Substance("Substance", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord4( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv4_texcoord4;
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _Normal_Sub;
		uniform float _Substance;
		uniform float _NormalMax;
		uniform float _NormalMin;
		uniform sampler2D _Albedo_Base;
		uniform sampler2D _Albedo_Sub;
		uniform sampler2D _MSO_Sub;
		uniform float _AO;
		uniform sampler2D _MS;
		uniform float _Metal;
		uniform float _Smooth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 lerpResult30 = lerp( UnpackNormal( tex2D( _Normal, i.uv4_texcoord4 ) ) , UnpackNormal( tex2D( _Normal_Sub, i.uv_texcoord ) ) , _Substance);
			o.Normal = ( ( lerpResult30 * ( _NormalMax - _NormalMin ) ) + _NormalMin );
			float4 lerpResult24 = lerp( tex2D( _Albedo_Base, i.uv4_texcoord4 ) , tex2D( _Albedo_Sub, i.uv_texcoord ) , _Substance);
			float4 tex2DNode35 = tex2D( _MSO_Sub, i.uv_texcoord );
			float4 temp_cast_0 = (tex2DNode35.b).xxxx;
			float4 lerpResult28 = lerp( float4(1,1,1,0) , temp_cast_0 , _AO);
			o.Albedo = ( lerpResult24 * lerpResult28 ).rgb;
			float4 tex2DNode34 = tex2D( _MS, i.uv4_texcoord4 );
			float lerpResult31 = lerp( tex2DNode34.r , tex2DNode35.r , _Substance);
			o.Metallic = ( lerpResult31 * _Metal );
			float lerpResult36 = lerp( tex2DNode34.g , tex2DNode35.g , _Substance);
			o.Smoothness = ( lerpResult36 * _Smooth );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
1993;141;1220;656;2003.232;996.358;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1740.423,-781.7148;Float;False;3;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1736.718,-582.7289;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;-822.208,-105.8595;Float;False;Property;_NormalMin;NormalMin;10;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1769.24,-47.68589;Float;False;Property;_Substance;Substance;11;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1269.512,-346.5595;Float;True;Property;_Normal;Normal;7;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1278.802,-127.0915;Float;True;Property;_Normal_Sub;Normal_Sub;8;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-828.8119,-195.2919;Float;False;Property;_NormalMax;NormalMax;9;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;34;-1273.683,88.12645;Float;True;Property;_MS;MS;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;46;-522.3888,-189.376;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;30;-830.8693,-329.0302;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1267.792,-897.674;Float;False;Property;_AO;AO;6;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-1280.919,-623.9663;Float;True;Property;_Albedo_Sub;Albedo_Sub;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-1269.308,291.4667;Float;True;Property;_MSO_Sub;MSO_Sub;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;26;-1214.315,-1145.117;Float;False;Constant;_White;White;10;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1282.593,-808.3065;Float;True;Property;_Albedo_Base;Albedo_Base;0;0;Create;True;0;0;False;0;None;56c50eea30621624b8dfe4f3b3d97665;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-835.4862,488.8182;Float;False;Property;_Smooth;Smooth;5;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;36;-771.5942,311.616;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;24;-763.249,-653.2074;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-849.8958,166.8213;Float;False;Property;_Metal;Metal;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;31;-790.4178,-1.237261;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-319.3927,-283.6985;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;28;-828.056,-1134.915;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-107.1925,-273.0081;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-442.8884,46.11946;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-431.9457,257.7968;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-294.9235,-630.3493;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;106.3992,-289.6747;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/Base;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;1;10;0
WireConnection;15;1;9;0
WireConnection;34;1;10;0
WireConnection;46;0;44;0
WireConnection;46;1;45;0
WireConnection;30;0;2;0
WireConnection;30;1;15;0
WireConnection;30;2;14;0
WireConnection;8;1;9;0
WireConnection;35;1;9;0
WireConnection;1;1;10;0
WireConnection;36;0;34;2
WireConnection;36;1;35;2
WireConnection;36;2;14;0
WireConnection;24;0;1;0
WireConnection;24;1;8;0
WireConnection;24;2;14;0
WireConnection;31;0;34;1
WireConnection;31;1;35;1
WireConnection;31;2;14;0
WireConnection;47;0;30;0
WireConnection;47;1;46;0
WireConnection;28;0;26;0
WireConnection;28;1;35;3
WireConnection;28;2;7;0
WireConnection;48;0;47;0
WireConnection;48;1;45;0
WireConnection;5;0;31;0
WireConnection;5;1;32;0
WireConnection;37;0;36;0
WireConnection;37;1;4;0
WireConnection;29;0;24;0
WireConnection;29;1;28;0
WireConnection;0;0;29;0
WireConnection;0;1;48;0
WireConnection;0;3;5;0
WireConnection;0;4;37;0
ASEEND*/
//CHKSM=11DEC4D50D7A207CAB54859B0166097D2F0A3A41