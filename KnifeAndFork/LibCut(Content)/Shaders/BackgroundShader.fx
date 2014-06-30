float4x4 World;
float4x4 View;
float4x4 Projection;

// TODO: add effect parameters here.

texture Texture;

sampler ScreenS = sampler_state
{
    Texture = <Texture>;    
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoord : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.

    return tex2D(ScreenS, input.TexCoord);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        //VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
