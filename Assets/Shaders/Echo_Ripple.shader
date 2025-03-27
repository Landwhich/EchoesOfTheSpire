Shader "Unlit/Echo_Ripple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Echo_Center", vector) = (0,0,0,0)
        _EchoDuration ("Echo_Duration", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
                float3 world_position : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _Center;
            float _EchoDuration;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.world_position = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float d = length(i.world_position.xyz - _Center.xyz);
                d = d<15 ? sin(d * 3.0 - (_Time.y * 6.0)) * 0.3 - 0.2 : 0;

                d = smoothstep(0.0, 0.35 ,d);
                return fixed4(d * 0.1, d * 0.1, d, 1)*_EchoDuration;

            }
            ENDCG
        }
    }
}
