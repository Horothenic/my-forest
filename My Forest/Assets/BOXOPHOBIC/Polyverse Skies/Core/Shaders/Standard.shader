// Made with Amplify Shader Editor v1.9.8.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/Polyverse Skies/Standard"
{
	Properties
	{
		[StyledBanner(Polyverse Skies Standard)] _Banner( "< Banner >", Float ) = 1
		[StyledCategory(Background, 5, 10)] _BackgroundCat( "[ Background Cat ]", Float ) = 1
		[KeywordEnum( Colors,Cubemap,Combined )] _BackgroundMode( "Background Mode", Float ) = 0
		[Space(10)] _SkyColor( "Sky Color", Color ) = ( 0.4980392, 0.7450981, 1, 1 )
		_EquatorColor( "Equator Color", Color ) = ( 1, 0.747, 0, 1 )
		_GroundColor( "Ground Color", Color ) = ( 0.4980392, 0.497, 0, 1 )
		_EquatorHeight( "Equator Height", Range( 0, 1 ) ) = 0.5
		_EquatorSmoothness( "Equator Smoothness", Range( 0.01, 1 ) ) = 0.5
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _BackgroundCubemap( "Background Cubemap", CUBE ) = "black" {}
		[Space(10)] _BackgroundExposure( "Background Exposure", Range( 0, 8 ) ) = 1
		[StyledCategory(Pattern)] _PatternCat( "[ Pattern Cat ]", Float ) = 1
		[Toggle( _ENABLEPATTERNOVERLAY_ON )] _EnablePatternOverlay( "Enable Pattern Overlay", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _PatternCubemap( "Pattern Cubemap", CUBE ) = "black" {}
		[Space(10)] _PatternContrast( "Pattern Contrast", Range( 0, 1 ) ) = 0.5
		[StyledCategory(Stars)] _StarsCat( "[ Stars Cat ]", Float ) = 1
		[Toggle( _ENABLESTARS_ON )] _EnableStars( "Enable Stars", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _StarsCubemap( "Stars Cubemap", CUBE ) = "white" {}
		[Space(10)] _StarsIntensity( "Stars Intensity", Range( 0, 5 ) ) = 2
		[IntRange] _StarsLayer( "Stars Layers", Range( 1, 3 ) ) = 2
		_StarsSize( "Stars Size", Range( 0, 0.99 ) ) = 0.5
		_StarsSunMask( "Stars Sun Mask", Range( 0, 1 ) ) = 0
		_StarsHeightMask( "Stars Height Mask", Range( 0, 1 ) ) = 0
		[StyledToggle] _StarsBottomMask( "Stars Bottom Mask", Float ) = 0
		[Space(10)][Toggle( _ENABLESTARSTWINKLING_ON )] _EnableStarsTwinkling( "Enable Stars Twinkling", Float ) = 0
		[Space(10)] _TwinklingContrast( "Twinkling Contrast", Range( 0, 2 ) ) = 1
		_TwinklingSpeed( "Twinkling Speed", Float ) = 0.05
		[Space(10)][Toggle( _ENABLESTARSROTATION_ON )] _EnableStarsRotation( "Enable Stars Rotation", Float ) = 0
		[IntRange][Space(10)] _StarsRotation( "Stars Rotation", Range( 0, 360 ) ) = 360
		[StyledVector(18)] _StarsRotationAxis( "Stars Rotation Axis", Vector ) = ( 0, 1, 0, 0 )
		_StarsRotationSpeed( "Stars Rotation Speed", Float ) = 0.5
		[StyledCategory(Sun)] _SunCat( "[ Sun Cat ]", Float ) = 1
		[Toggle( _ENABLESUN_ON )] _EnableSun( "Enable Sun", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _SunTexture( "Sun Texture", 2D ) = "black" {}
		[Space(10)] _SunColor( "Sun Color", Color ) = ( 1, 1, 1, 1 )
		_SunSize( "Sun Size", Range( 0.1, 1 ) ) = 0.5
		_SunIntensity( "Sun Intensity", Range( 0, 10 ) ) = 1
		[Enum(Global From Script,0,Local From Material,1)] _SunDirectionMode( "Sun Direction", Float ) = 0
		[StyledVector(18)] _SunDirection( "Sun Direction", Vector ) = ( 0, 0, 1, 0 )
		[StyledCategory(Moon)] _MoonCat( "[ Moon Cat ]", Float ) = 1
		[Toggle( _ENABLEMOON_ON )] _EnableMoon( "Enable Moon", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _MoonTexture( "Moon Texture", 2D ) = "black" {}
		[Space(10)] _MoonColor( "Moon Color", Color ) = ( 1, 1, 1, 1 )
		_MoonSize( "Moon Size", Range( 0.1, 1 ) ) = 0.5
		_MoonIntensity( "Moon Intensity", Range( 0, 10 ) ) = 1
		[Enum(Global From Script,0,Local From Material,1)] _MoonDirectionMode( "Moon Direction", Float ) = 0
		[StyledVector(18)] _MoonDirection( "Moon Direction", Vector ) = ( 1, 0, 0, 0 )
		[StyledCategory(Clouds)] _CloudsCat( "[ Clouds Cat ]", Float ) = 1
		[Toggle( _ENABLECLOUDS_ON )] _EnableClouds( "Enable Clouds", Float ) = 0
		[NoScaleOffset][Space(10)][StyledTextureSingleLine] _CloudsCubemap( "Clouds Cubemap", CUBE ) = "black" {}
		[Space(10)] _CloudsHeight( "Clouds Height", Range( -0.5, 0.5 ) ) = 0
		_CloudsLightColor( "Clouds Light Color", Color ) = ( 1, 1, 1, 1 )
		_CloudsShadowColor( "Clouds Shadow Color", Color ) = ( 0.4980392, 0.7450981, 1, 1 )
		[Space(10)][Toggle( _ENABLECLOUDSROTATION_ON )] _EnableCloudsRotation( "Enable Clouds Rotation", Float ) = 0
		[Space(10)] _CloudsRotation( "Clouds Rotation", Range( 0, 360 ) ) = 1
		[StyledVector(18)] _CloudsRotationAxis( "Clouds Rotation Axis", Vector ) = ( 0, 1, 0, 0 )
		_CloudsRotationSpeed( "Clouds Rotation Speed", Float ) = 0.5
		[StyledCategory(Fog)] _FogCat( "[ Fog Cat ]", Float ) = 1
		[Toggle( _ENABLEBUILTINFOG_ON )] _EnableBuiltinFog( "Enable Fog", Float ) = 0
		[StyledMessage(Info, The fog color is controlled by the fog color set in the Lighting panel., _EnableBuiltinFog, 1, 10, 0)] _EnableFogMessage( "EnableFogMessage", Float ) = 0
		[Space(10)] _FogHeight( "Fog Height", Range( 0, 1 ) ) = 0
		_FogSmoothness( "Fog Smoothness", Range( 0.01, 1 ) ) = 0
		_FogFill( "Fog Fill", Range( 0, 1 ) ) = 0
		[StyledCategory(Skybox Settings)] _SkyboxCat( "[ Skybox Cat ]", Float ) = 1
		_SkyboxOffset( "Skybox Offset", Range( -1, 1 ) ) = 0
		_SkyboxRotation( "Skybox Rotation", Range( 0, 360 ) ) = 0
		[StyledVector(18)] _SkyboxRotationAxis( "Skybox Rotation Axis", Vector ) = ( 0, 1, 0, 0 )

	}

	SubShader
	{
		

		Tags { "RenderType"="Background" "Queue"="Background" "PreviewType"="Skybox" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		

		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			#define ASE_VERSION 19802


			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#pragma shader_feature_local _ENABLEBUILTINFOG_ON
			#pragma shader_feature_local _ENABLECLOUDS_ON
			#pragma shader_feature_local _ENABLEMOON_ON
			#pragma shader_feature_local _ENABLESUN_ON
			#pragma shader_feature_local _ENABLESTARS_ON
			#pragma shader_feature_local _ENABLEPATTERNOVERLAY_ON
			#pragma shader_feature_local _BACKGROUNDMODE_COLORS _BACKGROUNDMODE_CUBEMAP _BACKGROUNDMODE_COMBINED
			#pragma shader_feature_local _ENABLESTARSROTATION_ON
			#pragma shader_feature_local _ENABLESTARSTWINKLING_ON
			#pragma shader_feature_local _ENABLECLOUDSROTATION_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				float4 ase_texcoord7 : TEXCOORD7;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _SunCat;
			uniform half _BackgroundCat;
			uniform half _SkyboxCat;
			uniform float _EnableFogMessage;
			uniform half _PatternCat;
			uniform half _CloudsCat;
			uniform half _Banner;
			uniform half _StarsCat;
			uniform half _FogCat;
			uniform half _MoonCat;
			uniform half4 _EquatorColor;
			uniform half4 _GroundColor;
			uniform half4 _SkyColor;
			uniform float3 _SkyboxRotationAxis;
			uniform float _SkyboxOffset;
			uniform float _SkyboxRotation;
			uniform half _EquatorHeight;
			uniform half _EquatorSmoothness;
			uniform samplerCUBE _BackgroundCubemap;
			uniform half _BackgroundExposure;
			uniform samplerCUBE _PatternCubemap;
			uniform half _PatternContrast;
			uniform half3 PolyverseSunDir;
			uniform half3 _SunDirection;
			uniform half _SunDirectionMode;
			uniform half _StarsSunMask;
			uniform samplerCUBE _StarsCubemap;
			uniform float3 _StarsRotationAxis;
			uniform half _StarsRotation;
			uniform half _StarsRotationSpeed;
			uniform half _StarsLayer;
			uniform half _StarsSize;
			uniform half _TwinklingSpeed;
			uniform half _TwinklingContrast;
			uniform half _StarsHeightMask;
			uniform half _StarsIntensity;
			uniform half _StarsBottomMask;
			uniform sampler2D _SunTexture;
			uniform half _SunSize;
			uniform half4 _SunColor;
			uniform half _SunIntensity;
			uniform sampler2D _MoonTexture;
			uniform half3 PolyverseMoonDir;
			uniform half3 _MoonDirection;
			uniform half _MoonDirectionMode;
			uniform half _MoonSize;
			uniform half4 _MoonColor;
			uniform half _MoonIntensity;
			uniform half4 _CloudsShadowColor;
			uniform half4 _CloudsLightColor;
			uniform samplerCUBE _CloudsCubemap;
			uniform float3 _CloudsRotationAxis;
			uniform half _CloudsRotation;
			uniform half _CloudsRotationSpeed;
			uniform half _CloudsHeight;
			uniform half _FogHeight;
			uniform half _FogSmoothness;
			uniform half _FogFill;


			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 normalizeResult46_g15 = normalize( _SkyboxRotationAxis );
				half3 RotationAxis42_g15 = normalizeResult46_g15;
				float3 appendResult1264 = (float3(0.0 , -_SkyboxOffset , 0.0));
				half3 VertexPos40_g15 = ( v.vertex.xyz + appendResult1264 );
				float dotResult48_g15 = dot( RotationAxis42_g15 , VertexPos40_g15 );
				float3 temp_output_47_0_g15 = ( RotationAxis42_g15 * dotResult48_g15 );
				float3 CPA50_g15 = temp_output_47_0_g15;
				half Angle44_g15 = radians( _SkyboxRotation );
				float3 normalizeResult1267 = normalize( ( CPA50_g15 + ( ( VertexPos40_g15 - CPA50_g15 ) * cos( Angle44_g15 ) ) + ( cross( RotationAxis42_g15 , ( VertexPos40_g15 - CPA50_g15 ) ) * sin( Angle44_g15 ) ) ) );
				float3 vertexToFrag1237 = normalizeResult1267;
				o.ase_texcoord1.xyz = vertexToFrag1237;
				float3 normalizeResult1446 = normalize( _SunDirection );
				float3 lerpResult1443 = lerp( PolyverseSunDir , normalizeResult1446 , _SunDirectionMode);
				half3 GlobalSunDirection1005 = -lerpResult1443;
				float3 ase_positionWS = mul( unity_ObjectToWorld, float4( ( v.vertex ).xyz, 1 ) ).xyz;
				float3 ase_viewVectorWS = ( _WorldSpaceCameraPos.xyz - ase_positionWS );
				float3 ase_viewDirWS = normalize( ase_viewVectorWS );
				float dotResult732 = dot( GlobalSunDirection1005 , ase_viewDirWS );
				float temp_output_7_0_g29 = -1.0;
				#ifdef _ENABLESTARS_ON
				float staticSwitch947 = saturate( ( ( dotResult732 - temp_output_7_0_g29 ) / ( -( 1.0 - _StarsSunMask ) - temp_output_7_0_g29 ) ) );
				#else
				float staticSwitch947 = 0.0;
				#endif
				float vertexToFrag762 = staticSwitch947;
				o.ase_texcoord1.w = vertexToFrag762;
				half3 VertexPos1239 = vertexToFrag1237;
				float3 break1245 = VertexPos1239;
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float temp_output_1215_0 = ( break1245.y * CAMERA_MODE300 );
				float3 appendResult1217 = (float3(break1245.x , temp_output_1215_0 , break1245.z));
				float3 normalizeResult46_g18 = normalize( _StarsRotationAxis );
				half3 RotationAxis42_g18 = normalizeResult46_g18;
				half3 VertexPos40_g18 = appendResult1217;
				float dotResult48_g18 = dot( RotationAxis42_g18 , VertexPos40_g18 );
				float3 temp_output_47_0_g18 = ( RotationAxis42_g18 * dotResult48_g18 );
				float3 CPA50_g18 = temp_output_47_0_g18;
				half Angle44_g18 = 1.0;
				half3 VertexPos40_g21 = ( CPA50_g18 + ( ( VertexPos40_g18 - CPA50_g18 ) * cos( Angle44_g18 ) ) + ( cross( RotationAxis42_g18 , ( VertexPos40_g18 - CPA50_g18 ) ) * sin( Angle44_g18 ) ) );
				float3 appendResult74_g21 = (float3(0.0 , VertexPos40_g21.y , 0.0));
				float3 VertexPosRotationAxis50_g21 = appendResult74_g21;
				float3 break84_g21 = VertexPos40_g21;
				float3 appendResult81_g21 = (float3(break84_g21.x , 0.0 , break84_g21.z));
				float3 VertexPosOtherAxis82_g21 = appendResult81_g21;
				half Angle44_g21 = -radians( ( _StarsRotation + ( _Time.y * _StarsRotationSpeed ) ) );
				#ifdef _ENABLESTARSROTATION_ON
				float3 staticSwitch1221 = ( VertexPosRotationAxis50_g21 + ( VertexPosOtherAxis82_g21 * cos( Angle44_g21 ) ) + ( cross( float3( 0, 1, 0 ) , VertexPosOtherAxis82_g21 ) * sin( Angle44_g21 ) ) );
				#else
				float3 staticSwitch1221 = appendResult1217;
				#endif
				float3 vertexToFrag1220 = staticSwitch1221;
				o.ase_texcoord2.xyz = vertexToFrag1220;
				#ifdef _ENABLESTARS_ON
				float staticSwitch953 = saturate(  (0.1 + ( abs( v.vertex.xyz.y ) - 0.0 ) * ( 1.0 - 0.1 ) / ( _StarsHeightMask - 0.0 ) ) );
				#else
				float staticSwitch953 = 0.0;
				#endif
				float vertexToFrag856 = staticSwitch953;
				o.ase_texcoord2.w = vertexToFrag856;
				float3 temp_output_962_0 = cross( -lerpResult1443 , half3( 0, 1, 0 ) );
				float3 normalizeResult967 = normalize( temp_output_962_0 );
				float dotResult968 = dot( normalizeResult967 , v.vertex.xyz );
				float3 normalizeResult965 = normalize( cross( GlobalSunDirection1005 , temp_output_962_0 ) );
				float dotResult969 = dot( normalizeResult965 , v.vertex.xyz );
				float2 appendResult970 = (float2(dotResult968 , dotResult969));
				float2 break972 = appendResult970;
				float2 appendResult980 = (float2(break972.x , ( break972.y * CAMERA_MODE300 )));
				float lerpResult1272 = lerp( 20.0 , 2.0 , _SunSize);
				#ifdef _ENABLESUN_ON
				float2 staticSwitch940 = (( appendResult980 * lerpResult1272 )*0.5 + 0.5);
				#else
				float2 staticSwitch940 = float2( 0,0 );
				#endif
				float2 vertexToFrag993 = staticSwitch940;
				o.ase_texcoord4.xy = vertexToFrag993;
				float dotResult988 = dot( GlobalSunDirection1005 , v.vertex.xyz );
				#ifdef _ENABLESUN_ON
				float staticSwitch1027 = saturate( dotResult988 );
				#else
				float staticSwitch1027 = 0.0;
				#endif
				float vertexToFrag997 = staticSwitch1027;
				o.ase_texcoord4.z = vertexToFrag997;
				float3 normalizeResult1447 = normalize( _MoonDirection );
				float3 lerpResult1451 = lerp( PolyverseMoonDir , normalizeResult1447 , _MoonDirectionMode);
				float3 temp_output_1058_0 = cross( -lerpResult1451 , half3( 0, 1, 0 ) );
				float3 normalizeResult1039 = normalize( temp_output_1058_0 );
				float dotResult1036 = dot( normalizeResult1039 , v.vertex.xyz );
				half3 GlobalMoonDirection1073 = -lerpResult1451;
				float3 normalizeResult1064 = normalize( cross( GlobalMoonDirection1073 , temp_output_1058_0 ) );
				float dotResult1067 = dot( normalizeResult1064 , v.vertex.xyz );
				float2 appendResult1066 = (float2(dotResult1036 , dotResult1067));
				float2 break1063 = appendResult1066;
				float2 appendResult1069 = (float2(break1063.x , ( break1063.y * CAMERA_MODE300 )));
				float lerpResult1285 = lerp( 20.0 , 2.0 , _MoonSize);
				#ifdef _ENABLEMOON_ON
				float2 staticSwitch1057 = (( appendResult1069 * lerpResult1285 )*0.5 + 0.5);
				#else
				float2 staticSwitch1057 = float2( 0,0 );
				#endif
				float2 vertexToFrag1043 = staticSwitch1057;
				o.ase_texcoord5.xy = vertexToFrag1043;
				float dotResult1054 = dot( GlobalMoonDirection1073 , v.vertex.xyz );
				#ifdef _ENABLEMOON_ON
				float staticSwitch1052 = saturate( dotResult1054 );
				#else
				float staticSwitch1052 = 0.0;
				#endif
				float vertexToFrag1051 = staticSwitch1052;
				o.ase_texcoord4.w = vertexToFrag1051;
				float3 break1247 = VertexPos1239;
				float3 appendResult1129 = (float3(break1247.x , ( break1247.y * CAMERA_MODE300 ) , break1247.z));
				float3 vertexToFrag1222 = appendResult1129;
				o.ase_texcoord6.xyz = vertexToFrag1222;
				float3 normalizeResult46_g30 = normalize( _CloudsRotationAxis );
				half3 RotationAxis42_g30 = normalizeResult46_g30;
				half3 VertexPos40_g30 = appendResult1129;
				float dotResult48_g30 = dot( RotationAxis42_g30 , VertexPos40_g30 );
				float3 temp_output_47_0_g30 = ( RotationAxis42_g30 * dotResult48_g30 );
				float3 CPA50_g30 = temp_output_47_0_g30;
				half Angle44_g30 = 1.0;
				half3 VertexPos40_g31 = ( CPA50_g30 + ( ( VertexPos40_g30 - CPA50_g30 ) * cos( Angle44_g30 ) ) + ( cross( RotationAxis42_g30 , ( VertexPos40_g30 - CPA50_g30 ) ) * sin( Angle44_g30 ) ) );
				float3 appendResult74_g31 = (float3(0.0 , VertexPos40_g31.y , 0.0));
				float3 VertexPosRotationAxis50_g31 = appendResult74_g31;
				float3 break84_g31 = VertexPos40_g31;
				float3 appendResult81_g31 = (float3(break84_g31.x , 0.0 , break84_g31.z));
				float3 VertexPosOtherAxis82_g31 = appendResult81_g31;
				half Angle44_g31 = -radians( ( _CloudsRotation + ( _Time.y * _CloudsRotationSpeed ) ) );
				float3 temp_output_1378_19 = ( VertexPosRotationAxis50_g31 + ( VertexPosOtherAxis82_g31 * cos( Angle44_g31 ) ) + ( cross( float3( 0, 1, 0 ) , VertexPosOtherAxis82_g31 ) * sin( Angle44_g31 ) ) );
				float3 vertexToFrag1207 = temp_output_1378_19;
				o.ase_texcoord7.xyz = vertexToFrag1207;
				
				o.ase_texcoord3 = v.vertex;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.zw = 0;
				o.ase_texcoord6.w = 0;
				o.ase_texcoord7.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}

			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float3 vertexToFrag1237 = i.ase_texcoord1.xyz;
				half3 VertexPos1239 = vertexToFrag1237;
				float4 lerpResult180 = lerp( _GroundColor , _SkyColor , step( 0.0 , VertexPos1239.y ));
				float temp_output_7_0_g28 = 0.0;
				float4 lerpResult288 = lerp( _EquatorColor , lerpResult180 , pow( saturate( ( ( abs( VertexPos1239.y ) - temp_output_7_0_g28 ) / ( _EquatorHeight - temp_output_7_0_g28 ) ) ) , ( 1.0 - _EquatorSmoothness ) ));
				half4 SKY218 = lerpResult288;
				half3 BACKGROUND1195 = ( (texCUBE( _BackgroundCubemap, VertexPos1239 )).rgb * _BackgroundExposure );
				#if defined( _BACKGROUNDMODE_COLORS )
				float4 staticSwitch1200 = SKY218;
				#elif defined( _BACKGROUNDMODE_CUBEMAP )
				float4 staticSwitch1200 = float4( BACKGROUND1195 , 0.0 );
				#elif defined( _BACKGROUNDMODE_COMBINED )
				float4 staticSwitch1200 = ( SKY218 * float4( BACKGROUND1195 , 0.0 ) );
				#else
				float4 staticSwitch1200 = SKY218;
				#endif
				float lerpResult1380 = lerp( 0.5 , texCUBE( _PatternCubemap, VertexPos1239 ).r , _PatternContrast);
				half PATTERN513 = saturate( lerpResult1380 );
				float4 temp_cast_2 = (PATTERN513).xxxx;
				float4 blendOpSrc574 = temp_cast_2;
				float4 blendOpDest574 = staticSwitch1200;
				#ifdef _ENABLEPATTERNOVERLAY_ON
				float4 staticSwitch524 = ( saturate( (( blendOpDest574 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest574 ) * ( 1.0 - blendOpSrc574 ) ) : ( 2.0 * blendOpDest574 * blendOpSrc574 ) ) ));
				#else
				float4 staticSwitch524 = staticSwitch1200;
				#endif
				float vertexToFrag762 = i.ase_texcoord1.w;
				float3 vertexToFrag1220 = i.ase_texcoord2.xyz;
				float4 texCUBENode564 = texCUBE( _StarsCubemap, vertexToFrag1220 );
				float temp_output_537_0 =  (3.0 + ( _StarsLayer - 1.0 ) * ( 1.0 - 3.0 ) / ( 3.0 - 1.0 ) );
				float mulTime1417 = _Time.y * ( _TwinklingSpeed * 10.0 );
				float mulTime1437 = _Time.y * ( _TwinklingSpeed * 5.0 );
				float saferPower554 = abs( ( ( sin( ( ( ( i.ase_texcoord3.xyz.x + i.ase_texcoord3.xyz.y ) * 20.0 ) + mulTime1417 ) ) * sin( ( mulTime1437 + ( ( i.ase_texcoord3.xyz.x + i.ase_texcoord3.xyz.z ) * 30.0 ) ) ) ) + 0.5 ) );
				#ifdef _ENABLESTARSTWINKLING_ON
				float staticSwitch878 = saturate( pow( saferPower554 , _TwinklingContrast ) );
				#else
				float staticSwitch878 = 1.0;
				#endif
				float vertexToFrag856 = i.ase_texcoord2.w;
				float3 break1245 = VertexPos1239;
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float temp_output_1215_0 = ( break1245.y * CAMERA_MODE300 );
				half Starts_Bottom_Mask1250 = step( 0.0 , temp_output_1215_0 );
				float lerpResult1255 = lerp( 1.0 , saturate( Starts_Bottom_Mask1250 ) , _StarsBottomMask);
				half STARS630 = ( floor( ( vertexToFrag762 * ( ( texCUBENode564.r + ( texCUBENode564.g * step( temp_output_537_0 , 2.0 ) ) + ( texCUBENode564.b * step( temp_output_537_0 , 1.0 ) ) ) + ( _StarsSize * staticSwitch878 ) ) * vertexToFrag856 ) ) * _StarsIntensity * lerpResult1255 );
				#ifdef _ENABLESTARS_ON
				float4 staticSwitch918 = ( staticSwitch524 + STARS630 );
				#else
				float4 staticSwitch918 = staticSwitch524;
				#endif
				float2 vertexToFrag993 = i.ase_texcoord4.xy;
				float4 tex2DNode995 = tex2D( _SunTexture, vertexToFrag993 );
				half4 SUN1004 = ( tex2DNode995.r * _SunColor * max( _SunIntensity , 1.0 ) );
				float vertexToFrag997 = i.ase_texcoord4.z;
				half SUN_MASK1003 = ( tex2DNode995.a * saturate( _SunIntensity ) * vertexToFrag997 );
				float4 lerpResult176 = lerp( staticSwitch918 , SUN1004 , SUN_MASK1003);
				#ifdef _ENABLESUN_ON
				float4 staticSwitch919 = lerpResult176;
				#else
				float4 staticSwitch919 = staticSwitch918;
				#endif
				float2 vertexToFrag1043 = i.ase_texcoord5.xy;
				float4 tex2DNode1049 = tex2D( _MoonTexture, vertexToFrag1043 );
				half4 MOON1077 = ( tex2DNode1049.r * _MoonColor * max( _MoonIntensity , 1.0 ) );
				float vertexToFrag1051 = i.ase_texcoord4.w;
				half MOON_MASK1078 = ( tex2DNode1049.a * vertexToFrag1051 * saturate( _MoonIntensity ) );
				float4 lerpResult1114 = lerp( staticSwitch919 , MOON1077 , MOON_MASK1078);
				#ifdef _ENABLEMOON_ON
				float4 staticSwitch1113 = lerpResult1114;
				#else
				float4 staticSwitch1113 = staticSwitch919;
				#endif
				float3 vertexToFrag1222 = i.ase_texcoord6.xyz;
				float3 vertexToFrag1207 = i.ase_texcoord7.xyz;
				#ifdef _ENABLECLOUDSROTATION_ON
				float3 staticSwitch1164 = vertexToFrag1207;
				#else
				float3 staticSwitch1164 = vertexToFrag1222;
				#endif
				float3 break245 = staticSwitch1164;
				float3 appendResult246 = (float3(break245.x , ( break245.y + ( _CloudsHeight * -1.0 ) ) , break245.z));
				float4 texCUBENode41 = texCUBE( _CloudsCubemap, appendResult246 );
				float4 lerpResult101 = lerp( _CloudsShadowColor , _CloudsLightColor , texCUBENode41);
				half4 CLOUDS222 = lerpResult101;
				half CLOUDS_MASK1406 = texCUBENode41.a;
				float4 lerpResult227 = lerp( staticSwitch1113 , CLOUDS222 , CLOUDS_MASK1406);
				#ifdef _ENABLECLOUDS_ON
				float4 staticSwitch1120 = lerpResult227;
				#else
				float4 staticSwitch1120 = staticSwitch1113;
				#endif
				float temp_output_7_0_g32 = 0.0;
				float lerpResult678 = lerp( saturate( pow( ( ( abs( VertexPos1239.y ) - temp_output_7_0_g32 ) / ( _FogHeight - temp_output_7_0_g32 ) ) , ( 1.0 - _FogSmoothness ) ) ) , 0.0 , _FogFill);
				half FOG_MASK359 = lerpResult678;
				float4 lerpResult317 = lerp( float4( (unity_FogColor).rgb , 0.0 ) , staticSwitch1120 , FOG_MASK359);
				#ifdef _ENABLEBUILTINFOG_ON
				float4 staticSwitch921 = lerpResult317;
				#else
				float4 staticSwitch921 = staticSwitch1120;
				#endif
				

				finalColor = staticSwitch921;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "PolyverseSkiesAsset.MaterialGUI"
	
	Fallback Off
}
/*ASEBEGIN
Version=19802
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1265;-896,-5488;Inherit;False;Property;_SkyboxOffset;Skybox Offset;63;0;Create;True;0;0;0;False;0;False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1269;-608,-5488;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1233;-896,-5216;Inherit;False;Property;_SkyboxRotation;Skybox Rotation;64;0;Create;True;0;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1234;-896,-5632;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1264;-448,-5568;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RadiansOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1293;-608,-5216;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1262;-256,-5632;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1238;-896,-5376;Inherit;False;Property;_SkyboxRotationAxis;Skybox Rotation Axis;65;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1368;0,-5632;Inherit;False;Compute Rotation;-1;;15;d0fbe60918b2bc146b660122744f3d02;0;4;38;FLOAT3;0,0,0;False;72;FLOAT3;0,0,0;False;41;FLOAT3;0,0,0;False;43;FLOAT;0;False;1;FLOAT3;19
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1267;320,-5632;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OrthoParams, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;267;-896,-6016;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1237;512,-5632;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleDivideOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;309;-592,-6016;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1007;-448,-6016;Half;False;Constant;_Float7;Float 7;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1239;768,-5632;Half;False;VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;268;-256,-6016;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1444;-1408,32;Half;False;Property;_SunDirection;Sun Direction;37;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;300;0,-6016;Half;False;CAMERA_MODE;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1209;-256,-1280;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1208;-256,-1216;Half;False;Property;_StarsRotationSpeed;Stars Rotation Speed;29;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1244;-896,-1792;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1415;-896,-896;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;546;-896,-704;Half;False;Property;_TwinklingSpeed;Twinkling Speed;25;0;Create;True;0;0;0;False;0;False;0.05;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1426;-896,-576;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1446;-1232,32;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1445;-1408,192;Half;False;Property;_SunDirectionMode;Sun Direction;36;1;[Enum];Create;False;0;2;Global From Script;0;Local From Material;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;938;-1408,-128;Half;False;Global;PolyverseSunDir;PolyverseSunDir;38;0;Create;True;0;0;0;False;0;False;0,0,0;-0.08641268,-0.4458854,0.8909091;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1212;-896,-1648;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1245;-736,-1792;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1210;0,-1280;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1211;-256,-1376;Half;False;Property;_StarsRotation;Stars Rotation;27;1;[IntRange];Create;True;0;0;0;False;1;Space(10);False;360;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1416;-640,-896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1425;-640,-576;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1433;-640,-768;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1438;-640,-672;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1443;-1088,-128;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1448;-1408,1040;Half;False;Property;_MoonDirection;Moon Direction;45;0;Create;True;0;0;0;False;1;StyledVector(18);False;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1215;-640,-1664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1213;128,-1376;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1421;-384,-896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1417;-384,-768;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1437;-384,-672;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1424;-384,-576;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;30;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;961;-1136,208;Half;False;Constant;_Vector2;Vector 2;9;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1447;-1216,1040;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1442;-896,-128;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1449;-1408,1216;Half;False;Property;_MoonDirectionMode;Moon Direction;44;1;[Enum];Create;False;0;2;Global From Script;0;Local From Material;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1061;-1408,896;Half;False;Global;PolyverseMoonDir;PolyverseMoonDir;38;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1241;-896,-3712;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RadiansOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1216;256,-1376;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1217;-512,-1792;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;962;-576,0;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1005;-640,-128;Half;False;GlobalSunDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1228;-256,-1536;Inherit;False;Property;_StarsRotationAxis;Stars Rotation Axis;28;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,1,0;1,1,9.48;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1422;-128,-896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1427;-128,-576;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1451;-1056,896;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;964;-384,-128;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1369;128,-1664;Inherit;False;Compute Rotation;-1;;18;d0fbe60918b2bc146b660122744f3d02;0;4;38;FLOAT3;0,0,0;False;72;FLOAT3;0,0,0;False;41;FLOAT3;0,0,0;False;43;FLOAT;1;False;1;FLOAT3;19
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1218;384,-1376;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1242;-720,-3712;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1062;-896,1056;Half;False;Constant;_Vector3;Vector 3;9;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SinOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1428;0,-576;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1418;0,-896;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1450;-896,896;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1370;512,-1664;Inherit;False;Compute Rotation Y;-1;;21;848a7021c0b62984d9ca063f51e19d69;0;2;38;FLOAT3;0,0,0;False;43;FLOAT;0;False;1;FLOAT3;19
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;212;-384,-3264;Half;False;Property;_EquatorHeight;Equator Height;6;0;Create;True;0;0;0;False;0;False;0.5;0.293;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1073;-640,896;Half;False;GlobalMoonDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;733;2688,-1472;Half;False;Property;_StarsSunMask;Stars Sun Mask;20;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1058;-576,1024;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1173;-384,256;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;965;-192,-128;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;967;-384,0;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;287;-192,-3680;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1431;256,-896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;969;0,-128;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1148;2432,-1792;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1038;-384,896;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;471;-384,-3136;Half;False;Property;_EquatorSmoothness;Equator Smoothness;7;0;Create;True;0;0;0;False;0;False;0.5;0.01;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;731;2432,-1648;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1172;2432,-1152;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1011;1024,-1376;Half;False;Constant;_Float11;Float 11;47;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;738;3008,-1472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1221;768,-1792;Float;False;Property;_EnableStarsRotation;Enable Stars Rotation;26;0;Create;True;0;0;0;False;1;Space(10);False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;968;0,0;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1010;1024,-1456;Half;False;Constant;_Float10;Float 10;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;528;1024,-1536;Half;False;Property;_StarsLayer;Stars Layers;18;1;[IntRange];Create;False;0;0;0;False;0;False;2;2;1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1411;0,-3328;Inherit;False;Remap To 0-1;-1;;28;d6eb10c035ec242479a0519a8ea4fd0f;0;3;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1440;448,-896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;555;256,-640;Half;False;Property;_TwinklingContrast;Twinkling Contrast;24;0;Create;True;0;0;0;False;1;Space(10);False;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;732;2768,-1792;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;208;256,-3328;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1167;3056,-1056;Half;False;Constant;_Float0;Float 0;47;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1064;-192,896;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1013;1408,-1264;Half;False;Constant;_Float13;Float 13;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;537;1408,-1536;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;3;False;3;FLOAT;3;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1384;-896,-4864;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;828;3072,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1270;-512,-3200;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1220;1152,-1792;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;475;-64,-3136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1012;1408,-1344;Half;False;Constant;_Float12;Float 12;47;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1017;2816,-1040;Half;False;Constant;_Float17;Float 17;47;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1018;2816,-960;Half;False;Constant;_Float18;Float 18;47;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;740;3200,-1472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1246;-896,2048;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1039;-384,1024;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;260;-896,2624;Half;False;Property;_CloudsRotationSpeed;Clouds Rotation Speed;55;0;Create;True;0;0;0;False;0;False;0.5;-0.24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;831;3024,-960;Half;False;Property;_StarsHeightMask;Stars Height Mask;21;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;701;-896,2560;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1175;-384,1280;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;970;160,-128;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;554;640,-768;Inherit;False;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;303;896,-3216;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;533;1664,-1536;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1191;-640,-4864;Inherit;True;Property;_BackgroundCubemap;Background Cubemap;8;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;None;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;534;1664,-1408;Inherit;False;2;0;FLOAT;3;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1036;0,1024;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1127;-896,2192;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;470;384,-3136;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1067;0,896;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;48;-896,2464;Half;False;Property;_CloudsRotation;Clouds Rotation;53;0;Create;True;0;0;0;False;1;Space(10);False;1;360;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;564;1408,-1792;Inherit;True;Property;_StarsCubemap;Stars Cubemap;16;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;4136c4d5d7422cf49a21b885d09f4518;True;0;False;white;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;194;640,-3536;Half;False;Property;_GroundColor;Ground Color;5;0;Create;True;0;0;0;False;0;False;0.4980392,0.497,0,1;0.4980389,0.497,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;182;640,-3360;Half;False;Property;_SkyColor;Sky Color;3;0;Create;True;0;0;0;False;1;Space(10);False;0.4980392,0.7450981,1,1;0.4980389,0.7450981,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1247;-736,2048;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TFHCRemapNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;832;3328,-1152;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;972;320,-128;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;255;-640,2560;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;971;320,0;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1412;3328,-1792;Inherit;False;Remap To 0-1;-1;;29;d6eb10c035ec242479a0519a8ea4fd0f;0;3;6;FLOAT;0;False;7;FLOAT;-1;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1224;1024,-896;Half;False;Constant;_Float1;Float 1;57;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1441;1024,-768;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;529;1856,-1664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;181;640,-3712;Half;False;Property;_EquatorColor;Equator Color;4;0;Create;True;0;0;0;False;1;;False;1,0.747,0,1;1,0.747,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1196;-640,-4672;Half;False;Property;_BackgroundExposure;Background Exposure;9;0;Create;True;0;0;0;False;1;Space(10);False;1;1;0;8;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;276;-512,2464;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;619;1408,-1152;Half;False;Property;_StarsSize;Stars Size;19;0;Create;True;0;0;0;False;0;False;0.5;0.99;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;530;1856,-1536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;724;3504,-1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;417;1152,-3136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;822;3520,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1066;160,896;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1128;-640,2176;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;180;1152,-3536;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1243;-896,-2560;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;878;1408,-896;Float;False;Property;_EnableStarsTwinkling;Enable Stars Twinkling;23;0;Create;True;0;0;0;False;1;Space(10);False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1271;-480,-1664;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;976;640,-32;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1404;-320,-4864;Inherit;False;FLOAT3;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;975;0,128;Half;False;Property;_SunSize;Sun Size;34;0;Create;True;0;0;0;False;0;False;0.5;1;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;947;3712,-1744;Float;False;Property;_ENABLESTARS_ON;Enable Stars;15;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;918;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;283;-640,-2368;Half;False;Property;_PatternContrast;Pattern Contrast;13;0;Create;True;0;0;0;False;1;Space(10);False;0.5;0.35;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1250;-256,-1664;Half;False;Starts_Bottom_Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;953;3712,-1104;Float;False;Property;_ENABLESTARS_ON;Enable Stars;15;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;918;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1223;1791.023,-1013.223;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;254;-640,-2560;Inherit;True;Property;_PatternCubemap;Pattern Cubemap;12;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;2496987be02f21244bcd1e6c3bfa5aff;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1065;320,1024;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1129;-512,2048;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1376;-896,2304;Inherit;False;Property;_CloudsRotationAxis;Clouds Rotation Axis;54;0;Create;True;0;0;0;False;1;StyledVector(18);False;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;288;1536,-3712;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1197;-128,-4864;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1063;320,896;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;527;2048,-1792;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1272;640,128;Inherit;False;3;0;FLOAT;20;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;980;832,-128;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RadiansOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;47;-384,2464;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1380;-256.824,-2559.056;Inherit;False;3;0;FLOAT;0.5;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;856;3968,-1088;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;218;1792,-3712;Half;False;SKY;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1377;-256,2304;Inherit;False;Compute Rotation;-1;;30;d0fbe60918b2bc146b660122744f3d02;0;4;38;FLOAT3;0,0,0;False;72;FLOAT3;0,0,0;False;41;FLOAT3;0,0,0;False;43;FLOAT;1;False;1;FLOAT3;19
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1195;256,-4864;Half;False;BACKGROUND;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;626;2176,-1392;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;762;3968,-1728;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1046;256,1280;Half;False;Property;_MoonSize;Moon Size;42;0;Create;True;0;0;0;False;0;False;0.5;1;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1253;4480,-1024;Inherit;False;1250;Starts_Bottom_Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;985;1024,-128;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1174;2816,384;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1072;640,992;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1205;-256,2464;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1028;2816,256;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1199;-896,-6528;Inherit;False;1195;BACKGROUND;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1069;832,896;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;517;-64,-2560;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1285;640,1152;Inherit;False;3;0;FLOAT;20;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;876;4480,-1408;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;988;3072,256;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1378;0,2304;Inherit;False;Compute Rotation Y;-1;;31;848a7021c0b62984d9ca063f51e19d69;0;2;38;FLOAT3;0,0,0;False;43;FLOAT;0;False;1;FLOAT3;19
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1251;4480,-960;Half;False;Property;_StarsBottomMask;Stars Bottom Mask;22;0;Create;True;0;0;0;False;1;StyledToggle;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;220;-896,-6656;Inherit;False;218;SKY;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1273;1280,-128;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1259;4720,-1024;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1075;1024,896;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1222;256,2048;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1255;4864,-1024;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;886;4736,-1408;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1201;-672,-6528;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;994;3232,256;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;513;256,-2560;Half;False;PATTERN;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1207;256,2304;Inherit;False;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;629;4480,-1216;Half;False;Property;_StarsIntensity;Stars Intensity;17;0;Create;True;0;0;0;False;1;Space(10);False;2;5;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1176;2816,1408;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;940;1504.37,-122.7181;Float;False;Property;_ENABLESUN_ON;Enable Sun;31;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;919;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1056;2816,1280;Inherit;False;1073;GlobalMoonDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;241;1024,2384;Half;False;Property;_CloudsHeight;Clouds Height;49;0;Create;True;0;0;0;False;1;Space(10);False;0;0;-0.5;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1164;640,2240;Float;False;Property;_EnableCloudsRotation;Enable Clouds Rotation;52;0;Create;True;0;0;0;False;1;Space(10);False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;882;4928,-1408;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;996;2176,320;Half;False;Property;_SunIntensity;Sun Intensity;35;0;Create;True;0;0;0;False;0;False;1;0.84;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1027;3424,299;Float;False;Property;_ENABLESUN_ON;Enable Sun;31;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;919;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1090;1024,2480;Half;False;Constant;_Float31;Float 31;53;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;516;-384,-6912;Inherit;False;513;PATTERN;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1054;3072,1280;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1286;1280,896;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;993;1808,-64;Inherit;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1200;-512,-6656;Float;False;Property;_BackgroundMode;Background Mode;2;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;Colors;Cubemap;Combined;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1281;2544,512;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1055;3232,1280;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;245;1024,2240;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BlendOpsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;574;-192,-6912;Inherit;False;Overlay;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;278;1344,2464;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1057;1504,897;Float;False;Property;_ENABLEMOON_ON;Enable Moon;39;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1113;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;997;3680,320;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1282;2560,320;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;630;5184,-1408;Half;False;STARS;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;995;2176,-128;Inherit;True;Property;_SunTexture;Sun Texture;32;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;dd566660fc358aa4098080e42ca0db56;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;998;2176,96;Half;False;Property;_SunColor;Sun Color;33;0;Create;True;0;0;0;False;1;Space(10);False;1,1,1,1;1,1,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;632;128,-6592;Inherit;False;630;STARS;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1002;2688,-128;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1043;1793,898;Inherit;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1045;2176,1408;Half;False;Property;_MoonIntensity;Moon Intensity;43;0;Create;True;0;0;0;False;0;False;1;0.33;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1001;4096,-32;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1052;3424,1328;Float;False;Property;_ENABLEMOON_ON;Enable Moon;39;0;Create;False;0;0;0;False;0;False;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1113;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;524;128,-6912;Float;False;Property;_EnablePatternOverlay;Enable Pattern Overlay;11;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;244;1488,2368;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1248;-896,3200;Inherit;False;1239;VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1283;2688,1072;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1249;-736,3200;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;246;1664,2240;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMaxOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1284;2560,1408;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1003;4352,128;Half;False;SUN_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;631;464,-6784;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1004;4352,-128;Half;False;SUN;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexToFragmentNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1051;3712,1344;Inherit;False;False;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1048;2176,1152;Half;False;Property;_MoonColor;Moon Color;41;0;Create;True;0;0;0;False;1;Space(10);False;1,1,1,1;1,1,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1049;2176,896;Inherit;True;Property;_MoonTexture;Moon Texture;40;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;b15c2596790c6ce42980282a7e842f57;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.AbsOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;314;-512,3200;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;41;1856,2240;Inherit;True;Property;_CloudsCubemap;Clouds Cubemap;48;1;[NoScaleOffset];Create;True;0;0;0;False;2;Space(10);StyledTextureSingleLine;False;-1;None;5bf545dec997743419de1df4195b3a0d;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1112;640,-6688;Inherit;False;1004;SUN;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;325;-896,3520;Half;False;Property;_FogSmoothness;Fog Smoothness;60;0;Create;True;0;0;0;False;0;False;0;0;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;313;-896,3392;Half;False;Property;_FogHeight;Fog Height;59;0;Create;True;0;0;0;False;1;Space(10);False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1111;640,-6592;Inherit;False;1003;SUN_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;918;640,-6912;Float;False;Property;_EnableStars;Enable Stars;15;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1053;4096,992;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1059;2688,896;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;232;2688,2048;Half;False;Property;_CloudsShadowColor;Clouds Shadow Color;51;0;Create;True;0;0;0;False;0;False;0.4980392,0.7450981,1,1;1,0.1843134,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;329;-256,3520;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;176;960,-6784;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;261;2688,2208;Half;False;Property;_CloudsLightColor;Clouds Light Color;50;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1077;4352,896;Half;False;MOON;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RelayNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;407;2688,2384;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1414;-320,3200;Inherit;False;Remap To 0-1;-1;;32;d6eb10c035ec242479a0519a8ea4fd0f;0;3;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1078;4352,1152;Half;False;MOON_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1115;1152,-6688;Inherit;False;1077;MOON;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;101;3072,2048;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;919;1152,-6912;Float;False;Property;_EnableSun;Enable Sun;31;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1116;1152,-6592;Inherit;False;1078;MOON_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;677;-64,3200;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1110;128,3392;Half;False;Constant;_Float41;Float 41;55;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;316;128,3200;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;679;128,3520;Half;False;Property;_FogFill;Fog Fill;61;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1406;2176,2432;Half;False;CLOUDS_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1114;1472,-6784;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;222;3536,2048;Half;False;CLOUDS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;228;1856,-6688;Inherit;False;222;CLOUDS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;678;384,3200;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1113;1664,-6912;Float;False;Property;_EnableMoon;Enable Moon;39;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;229;1856,-6592;Inherit;False;1406;CLOUDS_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;359;768,3200;Half;False;FOG_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FogAndAmbientColorsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;312;2688,-6688;Inherit;False;unity_FogColor;0;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;227;2176,-6784;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwizzleNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1405;2944,-6688;Inherit;False;FLOAT3;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;436;2944,-6592;Inherit;False;359;FOG_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1120;2688,-6912;Float;False;Property;_EnableClouds;Enable Clouds;47;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;317;3200,-6784;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1395;305.1353,2392.873;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1185;-160,-7680;Half;False;Property;_SunCat;[ Sun Cat ];30;0;Create;True;0;0;0;True;1;StyledCategory(Sun);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1198;-736,-7680;Half;False;Property;_BackgroundCat;[ Background Cat ];1;0;Create;True;0;0;0;True;1;StyledCategory(Background, 5, 10);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1396;43.13525,2430.873;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1240;496,-7680;Half;False;Property;_SkyboxCat;[ Skybox Cat ];62;0;Create;True;0;0;0;True;1;StyledCategory(Skybox Settings);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1226;-896,3648;Inherit;False;Property;_EnableFogMessage;EnableFogMessage;58;0;Create;True;0;0;0;True;1;StyledMessage(Info, The fog color is controlled by the fog color set in the Lighting panel., _EnableBuiltinFog, 1, 10, 0);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;921;3456,-6912;Float;False;Property;_EnableBuiltinFog;Enable Fog;57;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1188;-512,-7680;Half;False;Property;_PatternCat;[ Pattern Cat ];10;0;Create;True;0;0;0;True;1;StyledCategory(Pattern);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1394;-75.42523,2091.813;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1183;160,-7680;Half;False;Property;_CloudsCat;[ Clouds Cat ];46;0;Create;True;0;0;0;True;1;StyledCategory(Clouds);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1393;-352.8311,1999.014;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1184;-896,-7680;Half;False;Property;_Banner;< Banner >;0;0;Create;True;0;0;0;True;1;StyledBanner(Polyverse Skies Standard);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1186;-336,-7680;Half;False;Property;_StarsCat;[ Stars Cat ];14;0;Create;True;0;0;0;True;1;StyledCategory(Stars);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1181;336,-7680;Half;False;Property;_FogCat;[ Fog Cat ];56;0;Create;True;0;0;0;True;1;StyledCategory(Fog);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1189;0,-7680;Half;False;Property;_MoonCat;[ Moon Cat ];38;0;Create;True;0;0;0;True;1;StyledCategory(Moon);False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1180;3792,-6912;Float;False;True;-1;2;PolyverseSkiesAsset.MaterialGUI;100;5;BOXOPHOBIC/Polyverse Skies/Standard;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;True;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;True;3;RenderType=Background=RenderType;Queue=Background=Queue=0;PreviewType=Skybox;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1030;-896,768;Inherit;False;2948.242;100;Calculate Moon Position;0;MOON;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;480;640,-3840;Inherit;False;770.3374;100;Color Gradient Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;479;-896,-3840;Inherit;False;1405.403;100;Color Gradient Calculation;0;SKY;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;952;2432,-1280;Inherit;False;1793.068;100;Stars Horizon Height Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1091;1024,1920;Inherit;False;1347.811;100;Clouds Cubemap;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1031;2176,768;Inherit;False;509.1301;100;Moon Texture, Color and Intensity;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1006;-896,-256;Inherit;False;2942.402;100;Calculate Sun Position;0;SUN;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1026;2816,128;Inherit;False;1029.934;100;Direction Negative Z Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1136;-896,1920;Inherit;False;1789.754;100;Clouds Coordinates;0;CLOUDS;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;700;-896,3072;Inherit;False;1411.941;100;Fog Coords on Screen;0;BUILT-IN FOG;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1117;-896,-7040;Inherit;False;4866.933;100;;0;FINAL COLOR;0.4980392,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1187;-896,-7808;Inherit;False;1558.748;100;Drawers;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;949;2432,-1920;Inherit;False;1751.366;100;Stars Sun Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;942;-896,-1920;Inherit;False;3070.282;100;Stars Cubemap Coords and Cubemap RGB Layer;0;STARS;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;1035;2816,1152;Inherit;False;1054.961;100;Direction Negative Z Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;639;-896,-2688;Inherit;False;1047.013;100;Pattern Overlay Texture and Contrast;0;PATTERN OVERLAY;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;431;-896,-6144;Inherit;False;770.2415;100;Switch between Perspective / Orthographic camera;0;CAMERA MODE;1,0,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;914;2688,1920;Inherit;False;1028.475;100;Cloud Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;991;2176,-256;Inherit;False;512.5464;100;Sun Texture, Color and Intensity;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;955;-896,-1024;Inherit;False;2050.233;100;Stars Twinkling ;0;;1,0,0,1;0;0
WireConnection;1269;0;1265;0
WireConnection;1264;1;1269;0
WireConnection;1293;0;1233;0
WireConnection;1262;0;1234;0
WireConnection;1262;1;1264;0
WireConnection;1368;38;1262;0
WireConnection;1368;41;1238;0
WireConnection;1368;43;1293;0
WireConnection;1267;0;1368;19
WireConnection;1237;0;1267;0
WireConnection;309;0;267;2
WireConnection;309;1;267;1
WireConnection;1239;0;1237;0
WireConnection;268;0;1007;0
WireConnection;268;1;309;0
WireConnection;268;2;267;4
WireConnection;300;0;268;0
WireConnection;1446;0;1444;0
WireConnection;1245;0;1244;0
WireConnection;1210;0;1209;0
WireConnection;1210;1;1208;0
WireConnection;1416;0;1415;1
WireConnection;1416;1;1415;2
WireConnection;1425;0;1426;1
WireConnection;1425;1;1426;3
WireConnection;1433;0;546;0
WireConnection;1438;0;546;0
WireConnection;1443;0;938;0
WireConnection;1443;1;1446;0
WireConnection;1443;2;1445;0
WireConnection;1215;0;1245;1
WireConnection;1215;1;1212;0
WireConnection;1213;0;1211;0
WireConnection;1213;1;1210;0
WireConnection;1421;0;1416;0
WireConnection;1417;0;1433;0
WireConnection;1437;0;1438;0
WireConnection;1424;0;1425;0
WireConnection;1447;0;1448;0
WireConnection;1442;0;1443;0
WireConnection;1216;0;1213;0
WireConnection;1217;0;1245;0
WireConnection;1217;1;1215;0
WireConnection;1217;2;1245;2
WireConnection;962;0;1442;0
WireConnection;962;1;961;0
WireConnection;1005;0;1442;0
WireConnection;1422;0;1421;0
WireConnection;1422;1;1417;0
WireConnection;1427;0;1437;0
WireConnection;1427;1;1424;0
WireConnection;1451;0;1061;0
WireConnection;1451;1;1447;0
WireConnection;1451;2;1449;0
WireConnection;964;0;1005;0
WireConnection;964;1;962;0
WireConnection;1369;38;1217;0
WireConnection;1369;41;1228;0
WireConnection;1218;0;1216;0
WireConnection;1242;0;1241;0
WireConnection;1428;0;1427;0
WireConnection;1418;0;1422;0
WireConnection;1450;0;1451;0
WireConnection;1370;38;1369;19
WireConnection;1370;43;1218;0
WireConnection;1073;0;1450;0
WireConnection;1058;0;1450;0
WireConnection;1058;1;1062;0
WireConnection;965;0;964;0
WireConnection;967;0;962;0
WireConnection;287;0;1242;1
WireConnection;1431;0;1418;0
WireConnection;1431;1;1428;0
WireConnection;969;0;965;0
WireConnection;969;1;1173;0
WireConnection;1038;0;1073;0
WireConnection;1038;1;1058;0
WireConnection;738;0;733;0
WireConnection;1221;1;1217;0
WireConnection;1221;0;1370;19
WireConnection;968;0;967;0
WireConnection;968;1;1173;0
WireConnection;1411;6;287;0
WireConnection;1411;8;212;0
WireConnection;1440;0;1431;0
WireConnection;732;0;1148;0
WireConnection;732;1;731;0
WireConnection;208;0;1411;0
WireConnection;1064;0;1038;0
WireConnection;537;0;528;0
WireConnection;537;1;1010;0
WireConnection;537;2;1011;0
WireConnection;537;3;1011;0
WireConnection;537;4;1010;0
WireConnection;828;0;1172;2
WireConnection;1270;1;1242;1
WireConnection;1220;0;1221;0
WireConnection;475;0;471;0
WireConnection;740;0;738;0
WireConnection;1039;0;1058;0
WireConnection;970;0;968;0
WireConnection;970;1;969;0
WireConnection;554;0;1440;0
WireConnection;554;1;555;0
WireConnection;303;0;1270;0
WireConnection;533;0;537;0
WireConnection;533;1;1012;0
WireConnection;1191;1;1384;0
WireConnection;534;0;537;0
WireConnection;534;1;1013;0
WireConnection;1036;0;1039;0
WireConnection;1036;1;1175;0
WireConnection;470;0;208;0
WireConnection;470;1;475;0
WireConnection;1067;0;1064;0
WireConnection;1067;1;1175;0
WireConnection;564;1;1220;0
WireConnection;1247;0;1246;0
WireConnection;832;0;828;0
WireConnection;832;1;1017;0
WireConnection;832;2;831;0
WireConnection;832;3;1167;0
WireConnection;832;4;1018;0
WireConnection;972;0;970;0
WireConnection;255;0;701;0
WireConnection;255;1;260;0
WireConnection;1412;6;732;0
WireConnection;1412;8;740;0
WireConnection;1441;0;554;0
WireConnection;529;0;564;2
WireConnection;529;1;533;0
WireConnection;276;0;48;0
WireConnection;276;1;255;0
WireConnection;530;0;564;3
WireConnection;530;1;534;0
WireConnection;724;0;1412;0
WireConnection;417;0;470;0
WireConnection;822;0;832;0
WireConnection;1066;0;1036;0
WireConnection;1066;1;1067;0
WireConnection;1128;0;1247;1
WireConnection;1128;1;1127;0
WireConnection;180;0;194;0
WireConnection;180;1;182;0
WireConnection;180;2;303;0
WireConnection;878;1;1224;0
WireConnection;878;0;1441;0
WireConnection;1271;1;1215;0
WireConnection;976;0;972;1
WireConnection;976;1;971;0
WireConnection;1404;0;1191;0
WireConnection;947;0;724;0
WireConnection;1250;0;1271;0
WireConnection;953;0;822;0
WireConnection;1223;0;619;0
WireConnection;1223;1;878;0
WireConnection;254;1;1243;0
WireConnection;1129;0;1247;0
WireConnection;1129;1;1128;0
WireConnection;1129;2;1247;2
WireConnection;288;0;181;0
WireConnection;288;1;180;0
WireConnection;288;2;417;0
WireConnection;1197;0;1404;0
WireConnection;1197;1;1196;0
WireConnection;1063;0;1066;0
WireConnection;527;0;564;1
WireConnection;527;1;529;0
WireConnection;527;2;530;0
WireConnection;1272;2;975;0
WireConnection;980;0;972;0
WireConnection;980;1;976;0
WireConnection;47;0;276;0
WireConnection;1380;1;254;1
WireConnection;1380;2;283;0
WireConnection;856;0;953;0
WireConnection;218;0;288;0
WireConnection;1377;38;1129;0
WireConnection;1377;41;1376;0
WireConnection;1195;0;1197;0
WireConnection;626;0;527;0
WireConnection;626;1;1223;0
WireConnection;762;0;947;0
WireConnection;985;0;980;0
WireConnection;985;1;1272;0
WireConnection;1072;0;1063;1
WireConnection;1072;1;1065;0
WireConnection;1205;0;47;0
WireConnection;1069;0;1063;0
WireConnection;1069;1;1072;0
WireConnection;517;0;1380;0
WireConnection;1285;2;1046;0
WireConnection;876;0;762;0
WireConnection;876;1;626;0
WireConnection;876;2;856;0
WireConnection;988;0;1028;0
WireConnection;988;1;1174;0
WireConnection;1378;38;1377;19
WireConnection;1378;43;1205;0
WireConnection;1273;0;985;0
WireConnection;1259;0;1253;0
WireConnection;1075;0;1069;0
WireConnection;1075;1;1285;0
WireConnection;1222;0;1129;0
WireConnection;1255;1;1259;0
WireConnection;1255;2;1251;0
WireConnection;886;0;876;0
WireConnection;1201;0;220;0
WireConnection;1201;1;1199;0
WireConnection;994;0;988;0
WireConnection;513;0;517;0
WireConnection;1207;0;1378;19
WireConnection;940;0;1273;0
WireConnection;1164;1;1222;0
WireConnection;1164;0;1207;0
WireConnection;882;0;886;0
WireConnection;882;1;629;0
WireConnection;882;2;1255;0
WireConnection;1027;0;994;0
WireConnection;1054;0;1056;0
WireConnection;1054;1;1176;0
WireConnection;1286;0;1075;0
WireConnection;993;0;940;0
WireConnection;1200;1;220;0
WireConnection;1200;0;1199;0
WireConnection;1200;2;1201;0
WireConnection;1281;0;996;0
WireConnection;1055;0;1054;0
WireConnection;245;0;1164;0
WireConnection;574;0;516;0
WireConnection;574;1;1200;0
WireConnection;278;0;241;0
WireConnection;278;1;1090;0
WireConnection;1057;0;1286;0
WireConnection;997;0;1027;0
WireConnection;1282;0;996;0
WireConnection;630;0;882;0
WireConnection;995;1;993;0
WireConnection;1002;0;995;1
WireConnection;1002;1;998;0
WireConnection;1002;2;1282;0
WireConnection;1043;0;1057;0
WireConnection;1001;0;995;4
WireConnection;1001;1;1281;0
WireConnection;1001;2;997;0
WireConnection;1052;0;1055;0
WireConnection;524;1;1200;0
WireConnection;524;0;574;0
WireConnection;244;0;245;1
WireConnection;244;1;278;0
WireConnection;1283;0;1045;0
WireConnection;1249;0;1248;0
WireConnection;246;0;245;0
WireConnection;246;1;244;0
WireConnection;246;2;245;2
WireConnection;1284;0;1045;0
WireConnection;1003;0;1001;0
WireConnection;631;0;524;0
WireConnection;631;1;632;0
WireConnection;1004;0;1002;0
WireConnection;1051;0;1052;0
WireConnection;1049;1;1043;0
WireConnection;314;0;1249;1
WireConnection;41;1;246;0
WireConnection;918;1;524;0
WireConnection;918;0;631;0
WireConnection;1053;0;1049;4
WireConnection;1053;1;1051;0
WireConnection;1053;2;1283;0
WireConnection;1059;0;1049;1
WireConnection;1059;1;1048;0
WireConnection;1059;2;1284;0
WireConnection;329;0;325;0
WireConnection;176;0;918;0
WireConnection;176;1;1112;0
WireConnection;176;2;1111;0
WireConnection;1077;0;1059;0
WireConnection;407;0;41;0
WireConnection;1414;6;314;0
WireConnection;1414;8;313;0
WireConnection;1078;0;1053;0
WireConnection;101;0;232;0
WireConnection;101;1;261;0
WireConnection;101;2;407;0
WireConnection;919;1;918;0
WireConnection;919;0;176;0
WireConnection;677;0;1414;0
WireConnection;677;1;329;0
WireConnection;316;0;677;0
WireConnection;1406;0;41;4
WireConnection;1114;0;919;0
WireConnection;1114;1;1115;0
WireConnection;1114;2;1116;0
WireConnection;222;0;101;0
WireConnection;678;0;316;0
WireConnection;678;1;1110;0
WireConnection;678;2;679;0
WireConnection;1113;1;919;0
WireConnection;1113;0;1114;0
WireConnection;359;0;678;0
WireConnection;227;0;1113;0
WireConnection;227;1;228;0
WireConnection;227;2;229;0
WireConnection;1405;0;312;0
WireConnection;1120;1;1113;0
WireConnection;1120;0;227;0
WireConnection;317;0;1405;0
WireConnection;317;1;1120;0
WireConnection;317;2;436;0
WireConnection;1395;0;1378;19
WireConnection;1395;1;1396;0
WireConnection;921;1;1120;0
WireConnection;921;0;317;0
WireConnection;1394;0;1393;0
WireConnection;1394;1;1129;0
WireConnection;1180;0;921;0
ASEEND*/
//CHKSM=1FEEFAFF1BCD9F39B081F559C85D0BF22C75889A