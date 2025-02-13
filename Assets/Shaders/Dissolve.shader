﻿Shader "Custom/Dissolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        _DissolveTexture("Dissolve Texutre", 2D) = "white" {}
        _Amount("Amount", Range(0,1)) = 0
        _EmissionColor ("Emission color", Color) = (1,1,1,1)
        _EdgeWidth ("Edge width", Range(0, 0.1)) = 0.05
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        
        sampler2D _DissolveTexture;
        half _Amount;
        fixed4 _EmissionColor;
        half _EdgeWidth;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
            clip(dissolve_value - _Amount);
            
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            if (dissolve_value - _Amount < _EdgeWidth)
                o.Emission = _EmissionColor.rgb;
            
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}