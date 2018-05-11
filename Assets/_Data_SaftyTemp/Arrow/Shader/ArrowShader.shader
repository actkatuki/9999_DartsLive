// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ArrowShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color0("Color 0", Color) = (0,0.8344827,1,0)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			fixed filler;
		};

		uniform float4 _Color0;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( _Color0 * 10.0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
2567;226;1266;948;728.1047;499.4944;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;13;-256.7549,-2.608917;Float;False;Constant;_Float0;Float 0;0;0;10;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;7;-393,-278;Float;False;Property;_Color0;Color 0;0;0;0,0.8344827,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;35.24506,-133.6089;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;207,-26;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;ArrowShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;7;0
WireConnection;12;1;13;0
WireConnection;24;2;12;0
ASEEND*/
//CHKSM=83E95F4C5D2987F153045E2B695D36183785805D