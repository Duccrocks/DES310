void disovle_float(in float3 lightColour, in float3 edgeColour, in float alpha, in float Clip, out float3 finalColour)
{
	
    if (edgeColour.r+edgeColour.g+edgeColour.b >0)
    {
        finalColour = edgeColour;
    }
    else
    {
        finalColour = lightColour;
    }
	
}