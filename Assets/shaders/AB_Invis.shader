// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34388,y:32655,varname:node_3138,prsc:2|emission-4994-RGB,alpha-5028-OUT,refract-6035-OUT;n:type:ShaderForge.SFN_Distance,id:2712,x:32817,y:32811,varname:node_2712,prsc:2|A-1267-XYZ,B-4320-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:1267,x:32543,y:32725,varname:node_1267,prsc:2;n:type:ShaderForge.SFN_Vector4Property,id:4320,x:32543,y:32899,ptovrint:False,ptlb:pos,ptin:_pos,varname:node_4320,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Smoothstep,id:9616,x:33557,y:32796,varname:node_9616,prsc:2|A-1806-OUT,B-1810-OUT,V-2712-OUT;n:type:ShaderForge.SFN_Slider,id:9803,x:32817,y:32590,ptovrint:False,ptlb:Smoothness,ptin:_Smoothness,varname:node_9803,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:301,x:32817,y:32696,ptovrint:False,ptlb:Bias,ptin:_Bias,varname:_Smoothness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0.5,max:10;n:type:ShaderForge.SFN_Subtract,id:1806,x:33263,y:32556,varname:node_1806,prsc:2|A-301-OUT,B-9803-OUT;n:type:ShaderForge.SFN_Add,id:1810,x:33263,y:32704,varname:node_1810,prsc:2|A-9803-OUT,B-301-OUT;n:type:ShaderForge.SFN_OneMinus,id:6585,x:33795,y:32796,varname:node_6585,prsc:2|IN-9616-OUT;n:type:ShaderForge.SFN_Vector2,id:6035,x:34094,y:33030,varname:node_6035,prsc:2,v1:0,v2:0;n:type:ShaderForge.SFN_Color,id:4994,x:33795,y:32636,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4994,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Smoothstep,id:6810,x:33557,y:32942,varname:node_6810,prsc:2|A-1806-OUT,B-6384-OUT,V-2712-OUT;n:type:ShaderForge.SFN_Vector1,id:4673,x:32969,y:33072,varname:node_4673,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Subtract,id:6384,x:33259,y:32989,varname:node_6384,prsc:2|A-301-OUT,B-4673-OUT;n:type:ShaderForge.SFN_Multiply,id:5028,x:34094,y:32890,varname:node_5028,prsc:2|A-6585-OUT,B-6810-OUT;proporder:4320-9803-301-4994;pass:END;sub:END;*/

Shader "Shader Forge/AB_Invis" {
    Properties {
        _pos ("pos", Vector) = (0,0,0,0)
        _Smoothness ("Smoothness", Range(0, 5)) = 0
        _Bias ("Bias", Range(-10, 10)) = 0.5
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _pos;
            uniform float _Smoothness;
            uniform float _Bias;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(0,0);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                float node_1806 = (_Bias-_Smoothness);
                float node_2712 = distance(i.posWorld.rgb,_pos.rgb);
                return fixed4(lerp(sceneColor.rgb, finalColor,((1.0 - smoothstep( node_1806, (_Smoothness+_Bias), node_2712 ))*smoothstep( node_1806, (_Bias-0.6), node_2712 ))),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
