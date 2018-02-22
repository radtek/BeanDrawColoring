Shader "Custom/Test" {
    Properties {
        _MainTex("Texture", 2D) = "white" { }  
    }
 
    SubShader{ 
 
    Pass{
 
    Cull Back
 
    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
 
    #include "UnityCG.cginc"
 
    sampler2D _MainTex;
    float4x4 _MATRIX_MVP;
 
    struct v2f{
        float4  pos : SV_POSITION;
        float2  uv : TEXCOORD0;
    };
 
    v2f vert(appdata_base v){
 
        v2f o;
        float2 screenSpacePos;
        float4 clipPos;
 
        //Convert position from world space to clip space.
        //Only the UV coordinates should be frozen, so use a different matrix
        clipPos = mul(_MATRIX_MVP, v.vertex);
 
        //Convert position from clip space to screen space.
        //Screen space has range x=-1 to x=1
        screenSpacePos.x = clipPos.x / clipPos.w;
        screenSpacePos.y = clipPos.y / clipPos.w;
 
        //the screen space range (-1 to 1) has to be converted to
        //the UV range 0 to 1
        o.uv.x = (0.5f*screenSpacePos.x) + 0.5f;
        o.uv.y = (0.5f*screenSpacePos.y) + 0.5f;
 
        //The position of the vertex should not be frozen, so use
        //the standard UNITY_MATRIX_MVP matrix
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
 
        return o;
    }
 
    half4 frag(v2f i) : COLOR{
 
        half4 texcol = tex2D(_MainTex, i.uv);
        return texcol;
    }
 
    ENDCG
 
    }
    }
} 