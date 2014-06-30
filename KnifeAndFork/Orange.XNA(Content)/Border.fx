float4x4 World;
float4x4 View;
float4x4 Projection;

float Width = 0.05f;
float4 Border = float4(0.5f, 0.5f, 0.5f, 1.0f);
float4 Fill = float4(1.0f, 1.0f, 1.0f, 1.0f);

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoords : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	if (input.TexCoords.x < Width)
	{
		return Border;
	}
	if (input.TexCoords.y < Width)
	{
		return Border;
	}
	if (input.TexCoords.x > 1.0f - Width)
	{
		return Border;
	}
	if (input.TexCoords.y > 1.0f - Width)
	{
		return Border;
	}

    return Fill;
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
