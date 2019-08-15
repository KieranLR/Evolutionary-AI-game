Shader "Sprites/Water"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_OffsetTex("Refraction Map", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
	[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0

		// Add values to determine if outlining is enabled and outline color.
		[PerRendererData] _Outline("Outline", Float) = 0
		[PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
		[PerRendererData] _OutlineSize("Outline Size", int) = 1
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
		"DisableBatching" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex SpriteVert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_instancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		half4 color : COLOR;
	};

	struct v2g
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 screenuv : TEXCOORD1;
		half4 color : COLOR;
	};

	v2g vert(appdata v)
	{
		v2g o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		o.screenuv = ((o.vertex.xy / o.vertex.w) + 1) * 0.5;
		o.color = v.color;
		return o;
	}

	float2 safemul(float4x4 M, float4 v)
	{
		float2 r;

		r.x = dot(M._m00_m01_m02, v);
		r.y = dot(M._m10_m11_m12, v);

		return r;
	}

	//sampler2D _MainTex;
	sampler2D _OffsetTex;
	//sampler2D _AmbientTex;

	//uniform sampler2D _GlobalRefractionTex;
	uniform float _GlobalVisibility;
	uniform float _GlobalRefractionMag;

	float4 frag(v2g i) : SV_Target
	{
		float4 color = tex2D(_MainTex, i.uv) * i.color;
		float2 offset = safemul(unity_ObjectToWorld, tex2D(_OffsetTex, i.uv) * 2 - 1);
		//float4 ambient = tex2D(_AmbientTex, (i.screenuv + offset * _GlobalRefractionMag * 5) * 2);
		//float4 worldRefl = tex2D(_GlobalRefractionTex, i.screenuv + offset.xy * _GlobalRefractionMag);

		color.rgb = (color.rgb) * (1.0 - _GlobalVisibility)
			+  _GlobalVisibility;

		return color;
	}
		ENDCG
	}
	}
}
