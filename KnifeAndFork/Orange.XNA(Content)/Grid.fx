float4x4 World;
float4x4 View;
float4x4 Projection;

// TODO: add effect parameters here.
float2 Scale;
float4 Fill;
float4 FillBlank;
float4 Border;
float2 CellSize;
float2 Width;

texture ScreenTexture;    

sampler ScreenS = sampler_state
{
    Texture = <ScreenTexture>;
    MinFilter = Linear;
    MagFilter = Point;
    AddressU = Clamp;
    AddressV = Clamp;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	// Times the texcoords by the scale to get the pixels
	float2 coords = input.TexCoords * Scale;

	// If the remainder of the division by the CellSize is within the width, then draw the border color
	float remainder = coords.x % CellSize.x;
	if (remainder < Width.x)
	{
		return Border;
	}

	// Now do the same for the Y axis
	remainder = coords.y % CellSize.y;
	if (remainder < Width.y)
	{
		return Border;
	}

	// Okay so this is a square then, just draw the texture underneath it.
	return tex2D(ScreenS, input.TexCoords);
	//return float4(1.0f, 0.0f, 0.0f, 1.0f);
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
