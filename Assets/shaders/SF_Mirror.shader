// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34065,y:32730,varname:node_3138,prsc:2|emission-6495-OUT;n:type:ShaderForge.SFN_Tex2d,id:7228,x:33519,y:32799,ptovrint:False,ptlb:node_7228,ptin:_node_7228,varname:node_7228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7344-OUT;n:type:ShaderForge.SFN_ScreenPos,id:9660,x:32449,y:32600,varname:node_9660,prsc:2,sctp:2;n:type:ShaderForge.SFN_Code,id:7344,x:32916,y:32786,varname:node_7344,prsc:2,code:cgBlAHQAdQByAG4AIABmAGwAbwBhAHQAMgAoAHUALAAxAC0AdgAqAFIAKQA7AA==,output:1,fname:Function_node_7344,width:426,height:230,input:0,input:0,input:0,input_1_label:u,input_2_label:v,input_3_label:R|A-9660-U,B-9660-V,C-3633-OUT;n:type:ShaderForge.SFN_Tex2d,id:1725,x:32313,y:32805,ptovrint:False,ptlb:Curvature_Mask,ptin:_Curvature_Mask,varname:node_1725,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:9871,x:32531,y:32921,varname:node_9871,prsc:2|A-1725-R,B-968-OUT;n:type:ShaderForge.SFN_Clamp01,id:3633,x:32704,y:32921,varname:node_3633,prsc:2|IN-9871-OUT;n:type:ShaderForge.SFN_Slider,id:968,x:32184,y:33021,ptovrint:False,ptlb:Curvature_Val,ptin:_Curvature_Val,varname:node_968,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:3624,x:33516,y:33069,ptovrint:False,ptlb:Brock_Mask,ptin:_Brock_Mask,varname:_Curvature_Mask_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6495,x:33826,y:32939,varname:node_6495,prsc:2|A-7228-RGB,B-3624-RGB;proporder:7228-1725-968-3624;pass:END;sub:END;*/

Shader "Shader Forge/SF_Mirror" {
    Properties {
        _node_7228 ("node_7228", 2D) = "white" {}
        _Curvature_Mask ("Curvature_Mask", 2D) = "white" {}
        _Curvature_Val ("Curvature_Val", Range(0, 1)) = 0
        _Brock_Mask ("Brock_Mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_7228; uniform float4 _node_7228_ST;
            float2 Function_node_7344( float u , float v , float R ){
            return float2(u,1-v*R);
            }
            
            uniform sampler2D _Curvature_Mask; uniform float4 _Curvature_Mask_ST;
            uniform float _Curvature_Val;
            uniform sampler2D _Brock_Mask; uniform float4 _Brock_Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
////// Lighting:
////// Emissive:
                float4 _Curvature_Mask_var = tex2D(_Curvature_Mask,TRANSFORM_TEX(i.uv0, _Curvature_Mask));
                float2 node_7344 = Function_node_7344( sceneUVs.r , sceneUVs.g , saturate((_Curvature_Mask_var.r+_Curvature_Val)) );
                float4 _node_7228_var = tex2D(_node_7228,TRANSFORM_TEX(node_7344, _node_7228));
                float4 _Brock_Mask_var = tex2D(_Brock_Mask,TRANSFORM_TEX(i.uv0, _Brock_Mask));
                float3 emissive = (_node_7228_var.rgb*_Brock_Mask_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
