void xray_float(in float3 lightColour, in float3 xrayColour, in float3 playerPos, in float pulseLength,in float3 worldPos, out float3 finalColour) {
	


	float2 vertVector = float2(worldPos.x - playerPos.x, worldPos.z - playerPos.z);//calc vector to curret fragment worldCOORDS
	float dist = sqrt(vertVector.x * vertVector.x + vertVector.y * vertVector.y);//calc length of vector
	float illuminated;
	if (dist < pulseLength && dist>pulseLength - 8) {
		illuminated = 1;
	}
	else
	{
		illuminated = 0;
	}
	

	float sum = xrayColour.x + xrayColour.y + xrayColour.z;
	if (sum* illuminated >0) {
		finalColour = xrayColour;
	}
	else {
		finalColour = lightColour;

	}
	
	

	
}