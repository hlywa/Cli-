Shader "Unlit/ToonShader"
{
    Properties
    {
        _Color("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    
        _Noise ("Noise", 2D) = "white" {}
        _Distortion("Distortion", Range(0,1)) = 0
        _ScrollX("Scroll X", Range(0,10)) = 0
        _ScrollY("Scroll Y", Range(0,10)) = 0
            _FR("FrameRate", int) = 0
    }
    SubShader
    {
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            sampler2D _Noise;
            float _Distortion;
            float _ScrollX;
            float _ScrollY;

            float _FR;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                
                fixed roundedTime = floor(_Time.x * _FR)/_FR;

            float2 noiseOffset = float2(_ScrollX, _ScrollY) * roundedTime;
            fixed4 noise = tex2D(_Noise, i.uv + noiseOffset);
            fixed4 col = tex2D(_MainTex, i.uv + noise.b * _Distortion);
            col *= _Color;

                return col;
            }
            ENDCG
        }
    }
}
