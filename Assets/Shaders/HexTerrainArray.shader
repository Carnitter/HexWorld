Shader "Custom/HexTerrainArray"
{
  Properties
  {
    _BaseArray ("Base Terrain Array", 2DArray) = "" {}
    _OverlayArray ("Overlay Terrain Array", 2DArray) = "" {}
  }

  SubShader
  {
    Tags
    {
      "RenderPipeline"="UniversalPipeline"
      "RenderType"="Opaque"
      "Queue"="Geometry"
    }

    Pass
    {
      Name "ForwardUnlit"

      HLSLPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

      TEXTURE2D_ARRAY(_BaseArray);
      SAMPLER(sampler_BaseArray);

      TEXTURE2D_ARRAY(_OverlayArray);
      SAMPLER(sampler_OverlayArray);

      struct Attributes
      {
        float3 positionOS : POSITION;
        float2 uv : TEXCOORD0;
        float2 uv2 : TEXCOORD1;
      };

      struct Varyings
      {
        float4 positionHCS : SV_POSITION;
        float2 uv : TEXCOORD0;
        float2 idx : TEXCOORD1;
      };

      Varyings vert (Attributes v)
      {
        Varyings o;
        o.positionHCS = TransformObjectToHClip(v.positionOS);
        o.uv = v.uv;
        o.idx = v.uv2;
        return o;
      }

      half4 frag (Varyings i) : SV_Target
      {
        // TEMP: force grass (index 0)
        return SAMPLE_TEXTURE2D_ARRAY(
          _BaseArray,
          sampler_BaseArray,
          i.uv,
          0
        );
      }
      ENDHLSL
    }
  }
}
