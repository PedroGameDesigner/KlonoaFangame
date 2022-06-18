///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 21/10/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: CG helper functions.
///

#ifndef CG_HELPERS_INCLUDED
#define CG_HELPERS_INCLUDED


//UNITY_SHADER_NO_UPGRADE


#include "UnityCG.cginc"
#include "UnityShaderVariables.cginc"
#include "UnityStandardUtils.cginc"

	// ------------------------------------------------------------------
	//  Color processing.

	/// \english
    /// <summary>
    /// Convert Hue values to RGB values.
    /// </summary>
    /// <param name="pqt">Hue values.</param>
	/// <returns>RGB values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en Hue a valores en RGB.
    /// </summary>
    /// <param name="pqt">Valores en Hue.</param>
	/// <returns>Valores en RGB.</returns>
    /// \endspanish
	fixed HueToRgb(fixed3 pqt)
	{

		if (pqt.z < .0) pqt.z += 1.0;

		if (pqt.z > 1.0) pqt.z -= 1.0;

		if (pqt.z < 1.0 / 6.0) return pqt.x + (pqt.y - pqt.x) * 6.0 * pqt.z;

		if (pqt.z < 1.0 / 2.0) return pqt.y;

		if (pqt.z < 2.0 / 3.0) return pqt.x + (pqt.y - pqt.x) * (2.0 / 3.0 - pqt.z) * 6.0;

		return pqt.x;

	}

	/// \english
    /// <summary>
    /// Convert HSL values to RGB values.
    /// </summary>
    /// <param name="hsl">HSL values.</param>
	/// <returns>RGB values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en HSL a valores en RGB.
    /// </summary>
    /// <param name="hsl">Valores en HSL.</param>
	/// <returns>Valores en RGB.</returns>
    /// \endspanish
	fixed3 HslToRgb (fixed3 hsl)
	{ 

		fixed3 rgb;

		fixed3 pqt;

		if (hsl.y == 0)
		{

			rgb = hsl.z;

		}
		else
		{

			pqt.y = hsl.z < .5 ? hsl.z * (1.0 + hsl.y) : hsl.z + hsl.y - hsl.z * hsl.y;

			pqt.x = 2.0 * hsl.z - pqt.y;

			rgb.r = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x + 1.0 / 3.0));

			rgb.g = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x));

			rgb.b = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x - 1.0 / 3.0));

		}

		return rgb;

	}

	/// \english
    /// <summary>
    /// Convert RGB values to HSL values.
    /// </summary>
    /// <param name="rgb">RGB values.</param>
	/// <returns>HSL values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en RGB a valores en HSL.
    /// </summary>
    /// <param name="rgb">Valores en RGB.</param>
	/// <returns>Valores en HSL.</returns>
    /// \endspanish
	fixed3 RgbToHsl(fixed3 rgb)
	{

		fixed maxC = max(rgb.r, max(rgb.g, rgb.b));

		fixed minC = min(rgb.r, min(rgb.g, rgb.b));

		fixed3 hsl;

		hsl = (maxC + minC) / 2.0;

		if (maxC == minC)
		{

			hsl.x = hsl.y = .0;

		}
		else
		{

			fixed d = maxC - minC;

			hsl.y = (hsl.z > .5) ? d / (2.0 - maxC - minC) : d / (maxC + minC);

			if (rgb.r > rgb.g && rgb.r > rgb.b)
			{

        		hsl.x = (rgb.g - rgb.b) / d + (rgb.g < rgb.b ? 6.0 : .0);

			}
			else if (rgb.g > rgb.b) 
			{

        		hsl.x = (rgb.b - rgb.r) / d + 2.0;

			}
			else
			{

        		hsl.x = (rgb.r - rgb.g) / d + 4.0;

			}

			hsl.x /= 6.0f;

		}

		return hsl;

	}

	/// \english
    /// <summary>
    /// Desaturate a color.
    /// </summary>
    /// <param name="color">Color to desaturate.</param>
	/// <param name="amount">Magnitude of the desaturation effect.</param>
	/// <returns>Desaturated color.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Desatura un color.
    /// </summary>
    /// <param name="color">Color a desaturar.</param>
	/// <param name="amount">Cantidad del efecto de desaturación.</param>
	/// <returns>Color desaturado.</returns>
    /// \endspanish
	fixed DesaturateColor(float3 color, half amount)
	{

		fixed desaturatedColor = dot(color, float3(0.299, 0.587, 0.114));

		return lerp(color, desaturatedColor, amount);

	}

	/// \english
    /// <summary>
    /// Desaturate a color.
    /// </summary>
    /// <param name="color">Color to desaturate.</param>
	/// <param name="amount">Magnitude of the desaturation effect.</param>
	/// <returns>Desaturated color.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Desatura un color.
    /// </summary>
    /// <param name="color">Color a desaturar.</param>
	/// <param name="amount">Cantidad del efecto de desaturación.</param>
	/// <returns>Color desaturado.</returns>
    /// \endspanish
	fixed DesaturateColorSimple(float3 color)
	{

		fixed desaturatedColor = dot(color, float3(0.299, 0.587, 0.114));

		return desaturatedColor;

	}

	/// \english
    /// <summary>
    /// Converts continuous gradations of tones into multiple regions of fewer tones.
    /// </summary>
    /// <param name="color">Color to posterize.</param>
	/// <param name="steps">Value between 1 and 256 which defines strength of the posterization effect. The greater the value lesser are the used tones.</param>
	/// <returns>Posterized color.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte gradaciones continuas de tonos en múltiples regiones de menos tonos.
    /// </summary>
    /// <param name="color">Color a posterizar.</param>
	/// <param name="steps">Valor entre 1 y 256 que define la fuerza del efecto de posterización. Cuanto mayor sea el valor, menores son los tonos utilizados.</param>
	/// <returns>Color posterizado.</returns>
    /// \endspanish
	float3 PosterizeColor(float3 color, float steps)
	{

		float factor = 256.0 / steps;

		fixed3 posterizedColor = floor(color * factor) / factor;

		return posterizedColor;

	}

	//
	float PosterizeValue(float value, float steps)
	{

		float factor = 256.0 / steps;

		float posterizedColor = floor(value * factor) / factor;

		return posterizedColor;

	}

	/// \english
    /// <summary>
    /// Converts continuous gradations of tones into multiple regions of fewer tones.
    /// </summary>
    /// <param name="color">Color to posterize.</param>
	/// <param name="steps">Shading steps.</param>
	/// <returns>Posterized color.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte gradaciones continuas de tonos en múltiples regiones de menos tonos.
    /// </summary>
    /// <param name="color">Color a posterizar.</param>
	/// <param name="steps">Pasos del degradado</param>
	/// <returns>Color posterizado.</returns>
    /// \endspanish
	float3 StepizeColor(float3 color, float steps)
	{
		fixed3 stepizedColor = max(0, min(1, round(color * steps) / steps));

		return stepizedColor;
	}


	//
	float LinearRgbToLuminanceUniversal(float3 color)
	{

		float luminance;

		#ifdef UNITY_COLORSPACE_GAMMA
			luminance = LinearRgbToLuminance(GammaToLinearSpace(saturate(color)));
		#else
			luminance = LinearRgbToLuminance(color);
		#endif

		return luminance;
	}


	// ------------------------------------------------------------------
	//  UV oparations.

	// UV scroll and flip.
	float2 UVScrollFlip (float2 uv, fixed flipHorizontal, fixed flipVertical, fixed scrollAnimation, fixed2 scrollSpeed, float4 texelSize, fixed texelScroll)
	{
		float2 scrolledFlippedUV = uv;

		#if defined(_UVSCROLL_ON)

			float2 flippedUV = float2(lerp(uv.x, 1.0 - uv.x, flipHorizontal), lerp(uv.y, 1.0 - uv.y, flipVertical));

			float2 scrollIncrement = scrollSpeed * _Time.y;

			float2 texelScrolling = float2(texelSize.x * round(scrollIncrement.x), texelSize.y * round(scrollIncrement.y));

			float2 scrolledUV = float2(lerp(scrollIncrement.x, texelScrolling.x, texelScroll), lerp(scrollIncrement.y, texelScrolling.y, texelScroll));

			float2 processedUV = float2(scrolledUV.x + flippedUV.x, scrolledUV.y + flippedUV.y);

			scrolledFlippedUV = lerp(flippedUV, processedUV, scrollAnimation);

		#endif

		return scrolledFlippedUV;
	}

	// UV scroll and flip without texel scrolling.
	float2 UVScrollFlipSimple (float2 uv, fixed flipHorizontal, fixed flipVertical, fixed scrollAnimation, fixed2 scrollSpeed)
	{
		float2 scrolledFlippedUV = uv;

		#if defined(_UVSCROLL_ON)

			float2 flippedUV = float2(lerp(uv.x, 1.0 - uv.x, flipHorizontal), lerp(uv.y, 1.0 - uv.y, flipVertical));

			float2 scrollIncrement = scrollSpeed * _Time.y;

			float2 scrolledUV = float2(scrollIncrement.x, scrollIncrement.y);

			float2 processedUV = float2(scrolledUV.x + flippedUV.x, scrolledUV.y + flippedUV.y);

			scrolledFlippedUV = lerp(flippedUV, processedUV, scrollAnimation);

		#endif

		return scrolledFlippedUV;
	}

	// UV scroll.
	float2 UVScroll (float2 uv, fixed scrollAnimation, fixed2 scrollSpeed, float4 texelSize, fixed texelScroll)
	{

		float2 scrolledUV = uv;

		#if defined(_UVSCROLL_ON)

			float2 scrollIncrement = scrollSpeed * _Time.y;

			float2 texelScrolling = float2(texelSize.x * round(scrollIncrement.x), texelSize.y * round(scrollIncrement.y));

			float2 scrollUV = float2(lerp(scrollIncrement.x, texelScrolling.x, texelScroll), lerp(scrollIncrement.y, texelScrolling.y, texelScroll));

			float2 processedUV = float2(scrollUV.x + uv.x, scrollUV.y + uv.y);

			scrolledUV = lerp(scrolledUV, processedUV, scrollAnimation);

		#endif

		return scrolledUV;

	}

	// UV scroll.
	float2 UVScroll (float2 uv, fixed scrollAnimation, fixed2 scrollSpeed)
	{

		float2 scrolledUV = uv;

		#if defined(_UVSCROLL_ON)

			float2 scrollIncrement = scrollSpeed * _Time.y;

			float2 scrollUV = float2(scrollIncrement.x, scrollIncrement.y);

			float2 processedUV = float2(scrollUV.x + uv.x, scrollUV.y + uv.y);

			scrolledUV = lerp(scrolledUV, processedUV, scrollAnimation);

		#endif

		return scrolledUV;

	}

	// UV flip.
	float2 UVFlip (float2 uV, fixed flipHorizontal, fixed flipVertical)
	{

		float2 flippedUV = uV;

		#if defined(_UVFLIP_ON)

			float2 processedUV = float2(lerp(uV.x, 1.0 - uV.x, flipHorizontal), lerp(uV.y, 1.0 - uV.y, flipVertical));

			flippedUV = processedUV;

		#endif

		return flippedUV;
	}

	// UV rotation.
	float2 UVRotation (float2 uv, fixed2 anchorPoint, fixed rotationAmount) {
		float2 rotatedUV = uv;

		float cosine = cos(radians(rotationAmount));
		float sine = sin(radians(rotationAmount));

		rotatedUV = mul(uv - anchorPoint, float2x2(cosine, -sine, sine, cosine)) + anchorPoint;

		return rotatedUV;
	}

	// Adjust screen UVs relative to object to prevent shower door effect
	inline void ObjSpaceUVOffset(inout float2 screenUV, in float screenRatio, float4 textureTilingAndOfset)
	{
		// UNITY_MATRIX_P._m11 = Camera FOV
		float4 objPos = float4(-UNITY_MATRIX_T_MV[3].x * screenRatio * UNITY_MATRIX_P._m11, -UNITY_MATRIX_T_MV[3].y * UNITY_MATRIX_P._m11, UNITY_MATRIX_T_MV[3].z, UNITY_MATRIX_T_MV[3].w);

		float offsetFactorX = 0.5;
		float offsetFactorY = offsetFactorX * screenRatio;
		offsetFactorX *= textureTilingAndOfset.x;
		offsetFactorY *= textureTilingAndOfset.y;

		if (unity_OrthoParams.w < 1)	//don't scale with orthographic camera
		{
			//adjust uv scale
			screenUV -= float2(offsetFactorX, offsetFactorY);
			screenUV *= objPos.z;	//scale with cam distance
			screenUV += float2(offsetFactorX, offsetFactorY);

			// sign(UNITY_MATRIX_P[1].y) is different in Scene and Game views
			screenUV.x -= objPos.x * offsetFactorX * sign(UNITY_MATRIX_P[1].y);
			screenUV.y -= objPos.y * offsetFactorY * sign(UNITY_MATRIX_P[1].y);
		}
		else
		{
			// sign(UNITY_MATRIX_P[1].y) is different in Scene and Game views
			screenUV.x += objPos.x * offsetFactorX * sign(UNITY_MATRIX_P[1].y);
			screenUV.y += objPos.y * offsetFactorY * sign(UNITY_MATRIX_P[1].y);
		}
	}

	// Screen position to UV in screen space. To use in a Vertex function.
	float2 ScreenPositionToUV (float4 vertexPosition, float4 textureTilingAndOfset, fixed objectPositionOffset)
	{
		float2 screenUV;

		float4 screenPosition = ComputeScreenPos(UnityObjectToClipPos(vertexPosition));
		screenUV = screenPosition.xy * textureTilingAndOfset.xy + textureTilingAndOfset.zw;
		screenUV = screenUV / screenPosition.w;
		float screenRatio = _ScreenParams.y / _ScreenParams.x;
		screenUV.y = screenUV.y * screenRatio;

		if (objectPositionOffset == 1)
		{
			ObjSpaceUVOffset(screenUV, screenRatio, textureTilingAndOfset);
		}

		return screenUV;
	}


	// Parallax offset.
	float2 ParallaxOffsetUV (float2 uv, sampler2D parallaxMap, fixed parallaxLevel, float3 viewDirectionTangent)
	{
		//Versión del tutorial
		/*float height = tex2D(parallaxMap, uv.).r;
		height -= 0.5;
		height *= _ParallaxLevel;
		float2 uvOffset = uv + viewDirection * height;*/

		//Versión del Unity
		float2 uvOffset = uv + ParallaxOffset(tex2D(parallaxMap, uv).r , parallaxLevel, viewDirectionTangent);

		return uvOffset;
	}

	// Parallax mapping.
	float2 ParallaxMapping(float2 uv, float2 viewDirectionTangent, sampler2D heightMap, fixed heightLevel, fixed heightSteps) {
		float2 uvOffset = uv;
		float2 surfaceHeight;

		for (int i = 1; i < heightSteps; i++)
		{
			surfaceHeight = (tex2D(heightMap, uvOffset).r - 1);
			surfaceHeight *= viewDirectionTangent;
			surfaceHeight *= heightLevel;
			uvOffset += surfaceHeight;
		}

		return uvOffset;
	}

	// Parallax mapping with raymarching.
	float2 ParallaxMappingRaymarching (float2 uv, float2 viewDirectionTangent, sampler2D heightMap, fixed heightLevel, fixed heightSteps, fixed raymarchingSearchSteps) {
		float2 uvOffset = 0;
		float stepSize = 1.0 / heightSteps;
		float2 uvDelta = viewDirectionTangent * (stepSize * heightLevel);

		float stepHeight = 1;
		float surfaceHeight = tex2D(heightMap, uv).r;

		float2 prevUVOffset = uvOffset;
		float prevStepHeight = stepHeight;
		float prevSurfaceHeight = surfaceHeight;

		for (int i = 1; i < heightSteps; i++) {
			if (stepHeight > surfaceHeight) {
				prevUVOffset = uvOffset;
				prevStepHeight = stepHeight;
				prevSurfaceHeight = surfaceHeight;
				
				uvOffset -= uvDelta;
				stepHeight -= stepSize;
				surfaceHeight = tex2D(heightMap, uv + uvOffset).r;
			}
		}

		if (raymarchingSearchSteps > 0)
			for (int j = 0; j < raymarchingSearchSteps; j++) {
				uvDelta *= 0.5;
				stepSize *= 0.5;

				if (stepHeight < surfaceHeight) {
					uvOffset += uvDelta;
					stepHeight += stepSize;
				}
				else {
					uvOffset -= uvDelta;
					stepHeight -= stepSize;
				}
				surfaceHeight = tex2D(heightMap, uv + uvOffset).r;
			}

		if (raymarchingSearchSteps == 0)
		{
			float prevDifference = prevStepHeight - prevSurfaceHeight;
			float difference = surfaceHeight - stepHeight;
			float t = prevDifference / (prevDifference + difference);
			uvOffset = prevUVOffset - uvDelta * t;
		}

		return uvOffset + uv;
	}

	// Parallax occlusion mapping.
	float2 ParallaxOcclusionMapping (sampler2D heightMap, float2 uv, float3 normalWorld, float3 viewDirectionWorld, float3 viewDirectionTangent, float2 sampleRange, int interpolationSteps, fixed heightLevel, 
	fixed clipEdge, float2 uvTiling, fixed clipSilhouette, float2 clipCurvature, fixed curvatureBias) {
		float3 result = 0;
		int stepIndex = 0;
		int numSteps = (int)lerp(sampleRange.y, sampleRange.x, saturate(dot(normalWorld, viewDirectionWorld)));
		float layerHeight = 1.0 / numSteps;
		float2 plane = heightLevel * (viewDirectionTangent.xy / viewDirectionTangent.z);
		float refPlane = 0;
		uv += refPlane * plane;
		float2 deltaTex = -plane * layerHeight;
		float2 prevTexOffset = 0;
		float prevRayZ = 1.0f;
		float prevHeight = 0.0f;
		float2 currTexOffset = deltaTex;
		float currRayZ = 1.0f - layerHeight;
		float currHeight = 0.0f;
		float intersection = 0;
		float2 finalTexOffset = 0;

		float2 derivativeX = ddx(uv);
		float2 derivativeY = ddy(uv);

		while (stepIndex < numSteps + 1)
		{
			if (clipSilhouette == 1)
			{
				result.z = dot(clipCurvature, currTexOffset * currTexOffset);
				currHeight = tex2Dgrad(heightMap, uv + currTexOffset, derivativeX, derivativeY).r * (1 - result.z);
			}
			else
			{
				currHeight = tex2Dgrad(heightMap, uv + currTexOffset, derivativeX, derivativeY).r;
			}

			if (currHeight > currRayZ)
			{
				stepIndex = numSteps + 1;
			}
			else
			{
				stepIndex++;
				prevTexOffset = currTexOffset;
				prevRayZ = currRayZ;
				prevHeight = currHeight;
				currTexOffset += deltaTex;

				if (clipSilhouette == 1)
				{	
					currRayZ -= layerHeight * (1 - result.z) * (1 + curvatureBias);
				}
				else
				{
					currRayZ -= layerHeight;
				}
			}
		}

		int sectionIndex = 0;
		float newZ = 0;
		float newHeight = 0;

		while (sectionIndex < interpolationSteps)	
		{
			intersection = (prevHeight - prevRayZ) / (prevHeight - currHeight + currRayZ - prevRayZ);
			finalTexOffset = prevTexOffset + intersection * deltaTex;
			newZ = prevRayZ - intersection * layerHeight;
			newHeight = tex2Dgrad(heightMap, uv + finalTexOffset, derivativeX, derivativeY).r;
			if (newHeight > newZ)
			{
				currTexOffset = finalTexOffset;
				currHeight = newHeight;
				currRayZ = newZ;
				deltaTex = intersection * deltaTex;
				layerHeight = intersection * layerHeight;
			}
			else
			{
				prevTexOffset = finalTexOffset;
				prevHeight = newHeight;
				prevRayZ = newZ;
				deltaTex = (1 - intersection) * deltaTex;
				layerHeight = (1 - intersection) * layerHeight;
			}
			sectionIndex++;
		}

		if (clipSilhouette == 1)
		{
			#ifdef UNITY_PASS_SHADOWCASTER
				if (unity_LightShadowBias.z == 0.0)
				{
			#endif
					if (result.z > 1)
					{
						clip(-1);
					}
			#ifdef UNITY_PASS_SHADOWCASTER
				}
			#endif
		}

		result.xy = uv + finalTexOffset;

		if (clipEdge == 1)
		{
			#ifdef UNITY_PASS_SHADOWCASTER
				if (unity_LightShadowBias.z == 0.0)
				{
			#endif
					if (result.x < 0)
					{
						clip(-1);
					}

					if (result.x > uvTiling.x)
					{
						clip(-1);
					}

					if (result.y < 0)
					{
						clip(-1);
					}

					if (result.y > uvTiling.y)
					{
						clip(-1);
					}
			#ifdef UNITY_PASS_SHADOWCASTER
				}
			#endif
		}

		return result.xy;
	}

	//
	float2 TransformTriangleVertexToUV (float2 vertex)
	{
		float2 uv = (vertex + 1.0) * 0.5;
		return uv;
	}

	// Anti Aliased Point Filtering.
		float2 AntiAliasedPointFiltering (float2 uv, float2 textureSize, half edgeSmoothing)
			{
				float2 newUV = uv * textureSize;
				float2 edgeWidth = edgeSmoothing * float2(ddx(newUV.x), ddy(newUV.y));
				float2 x = frac(newUV);
				float2 x_ = clamp(0.5 / edgeWidth * x, 0.0, 0.5) + clamp(0.5 / edgeWidth * (x - 1.0) + 0.5, 0.0, 0.5);
				float2 texCoord = (floor(newUV) + x_) / textureSize;
				return texCoord;
			}

	// ------------------------------------------------------------------
	//  Distances.

	// Distance between camera and surface calculation. Use depth buffer.
	float DistanceBetweenCameraAndSurface (float eyeDepth, half nearPoint, half farPoint)
	{
		float distanceBetween = (eyeDepth -_ProjectionParams.y - nearPoint) / farPoint;

		float clampedDistance = clamp(1.0 - distanceBetween, 0.0 , 1.0 );

		return clampedDistance;
	}

	// Distance between camera and world point. Does not use depth buffer.
	float DistanceBetweenCameraAndWorldPoint (float3 WorldCameraPosition, float3 worldPoint)
	{
		float distanceBetween = length(WorldCameraPosition - worldPoint);

		return distanceBetween;
	}

	// Distance between camera and world vertex point. Does not use depth buffer.
	float DistanceBetweenCameraAndWorldVertexPoint (float4 vertexPosition, half nearPoint, half farPoint)
	{
		float3 worldVertexPosition = mul(unity_ObjectToWorld, float4(vertexPosition.xyz, 1)).xyz;

		float distanceBetween = (distance(worldVertexPosition, _WorldSpaceCameraPos) - nearPoint) / farPoint;

		float clampedDistance = saturate(distanceBetween);

		return clampedDistance;
	}

	// Reflection by camera distance calculation.
	float ReflectionByCameraDistance (float distanceBetweenCameraAndSurface, half enableFunction)
	{

		float reflection = lerp(1.0, distanceBetweenCameraAndSurface, enableFunction);

		return reflection;
		
	}
	
	// ------------------------------------------------------------------
	//  Value and color masking.

	// Apply a mask with color to a float3.
	float3 Color3Masking (float3 baseValue, fixed interpolator, float3 maskMap, float3 colorMask)
	{

		float3 preparedMask = maskMap * colorMask + (1 - maskMap) * baseValue;

		float3 maskedValue = lerp(baseValue, preparedMask, interpolator);

		return maskedValue;

	}

	// Apply a mask with color to a float4.
	float4 Color4Masking (float4 baseValue, fixed interpolator, float4 maskMap, float4 colorMask)
	{

		float4 preparedMask = maskMap * colorMask + (1 - maskMap) * baseValue;

		float4 maskedValue = lerp(baseValue, preparedMask, interpolator);

		return maskedValue;

	}

	// Mask a float.
	float FloatMasking (float baseValue, float effect, fixed interpolator, float3 maskMap)
	{

		return lerp(baseValue, effect, lerp(0, maskMap, interpolator));

	}

	// Mask a float2.
	float2 Float2Masking (float2 baseValue, float2 effect, fixed interpolator, float2 maskMap)
	{

		return lerp(baseValue, effect, lerp(0, maskMap, interpolator));

	}

	// Mask a float3.
	float3 Float3Masking (float3 baseValue, float3 effect, fixed interpolator, float3 maskMap)
	{

		return lerp(baseValue, effect, lerp(0, maskMap, interpolator));

	}

	// Mask a float4.
	float4 Float4Masking (float4 baseValue, float4 effect, fixed interpolator, float4 maskMap)
	{

		return lerp(baseValue, effect, lerp(0, maskMap, interpolator));

	}

	// Mask a image effect.
	half4 ImageEffectMask (sampler2D screenTexture, float2 uvScreenTexture, float4 effect, sampler2D effectMask, float2 uvEffect, fixed interpolator, fixed fitToScreen, float4 screenPosition, half4 effectMaskScaleOffset, half4 effectMaskTexelSize)
	{

		float4 normalizedScreenPosition = screenPosition / screenPosition.w;
		normalizedScreenPosition.z = (UNITY_NEAR_CLIP_VALUE >= 0) ? normalizedScreenPosition.z : normalizedScreenPosition.z * 0.5 + 0.5;
		float2 uvScreenPosition = (normalizedScreenPosition * _ScreenParams * effectMaskTexelSize).xy * effectMaskScaleOffset.xy + effectMaskScaleOffset.zw;
			
		half4 maskColor = lerp(0, tex2D(effectMask, fitToScreen ? uvEffect : uvScreenPosition).r * tex2D(effectMask, fitToScreen ? uvEffect : uvScreenPosition).a, interpolator);

		fixed alphaMask = tex2D(effectMask, uvEffect).r * tex2D(effectMask, uvEffect).a;
		fixed alphaMaskScreen = tex2D(effectMask, uvScreenPosition).r * tex2D(effectMask, uvScreenPosition).a;
		float4 maskedFinalColor = lerp(tex2D(screenTexture, uvScreenTexture), effect, maskColor * (fitToScreen ? alphaMask : alphaMaskScreen));

		return maskedFinalColor;

	}



	// ------------------------------------------------------------------
	// Normals.
	// Reorient Normal Mapping if the object is rotated.
	// ref https://www.gamedev.net/topic/678043-how-to-blend-world-space-normals/#entry5287707
	// assume compositing in world space
	// Note: Using vtxNormal = real3(0, 0, 1) give the BlendNormalRNM formulation.
	// Source: ScriptableRenderPipeline/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl
	float3 CGBlendNormalWorldspaceRNM(float3 n1, float3 n2, float3 vtxNormal)
	{
		// Build the shortest-arc quaternion
		float4 q = float4(cross(vtxNormal, n2), dot(vtxNormal, n2) + 1.0) / sqrt(2.0 * (dot(vtxNormal, n2) + 1));

		// Rotate the normal
		return n1 * (q.w * q.w - dot(q.xyz, q.xyz)) + 2 * q.xyz * dot(q.xyz, n1) + 2 * q.w * cross(q.xyz, n1);
	}




	// ------------------------------------------------------------------
	//  Fresnel.

	// Standard fresnel based on a rim light.
	inline float FresnelStandard(float3 normalizedWorldNormal, float3 viewDirection, fixed fresnelBias, fixed fresnelScale, fixed fresnelPower)
	{
		float fresnel;

		float rimLight = dot(normalizedWorldNormal, viewDirection);
		fresnel = (fresnelBias + fresnelScale * pow(1.0 - rimLight, fresnelPower));

		return fresnel;
	}


	// ------------------------------------------------------------------
	//  Lighting.

	// Compute diffuse light.
	inline half CGDotClamped (half3 a, half3 b) {
		#if (SHADER_TARGET < 30 || defined(SHADER_API_PS3))
			return saturate(dot(a, b));
		#else
			return max(0.0h, dot(a, b));
	#endif
}


	// ------------------------------------------------------------------
	//  Others.

	// Projected screen position. Use tex2Dproj function.
	inline float4 CGComputeGrabScreenPos(float4 position)
	{
		#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
		#else
			float scale = 1.0;
		#endif

		float4 o = position;
		o.y = position.w * 0.5f;
		o.y = (position.y - o.y) * _ProjectionParams.x * scale + o.y;

		return o;
	}

	// Projected screen position. Supports Stereo instancing rendering, use the UNITY_SAMPLE_SCREENSPACE_TEXTURE macro.
	inline float4 CGComputeGrabScreenPosNew(float4 position)
	{
		#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
		#else
			float scale = 1.0;
		#endif

		float4 o = position;
		o.y = position.w * 0.5f;
		o.y = (position.y - o.y) * _ProjectionParams.x * scale + o.y;

		#if SHADER_API_D3D9 || SHADER_API_D3D11
			o.w += 0.00000000001;
		#endif

		o = o / o.w;

		return o;
	}

	float2 UnStereo(float2 UV) {
		#if UNITY_SINGLE_PASS_STEREO
			float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];
			UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
		#endif
		
		return UV;
	}


	// Mesh intersection calculation.
	float4 MeshIntersectionOld(float3 baseColor, float2 uv, sampler2D intersectionTexture, float4 intersectionColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed intersectionDistance, fixed intersectionFalloff, fixed intersectionFill, fixed intersectionHardEdge) 
	{
		float4 finalIntersectionColor;

		float4 intersectionColorTexture = tex2D(intersectionTexture, uv) * intersectionColor;

		float4 normalizedScreenPosition = screenPosition / screenPosition.w;
		normalizedScreenPosition.z = (UNITY_NEAR_CLIP_VALUE >= 0) ? normalizedScreenPosition.z : normalizedScreenPosition.z * 0.5 + 0.5;
		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = LinearEyeDepth(sceneZ);
		float distanceDepth = abs((screenDepth - LinearEyeDepth(normalizedScreenPosition.z)) / (intersectionDistance));

		float processedDistanceDepth = pow(distanceDepth, intersectionFalloff);
		finalIntersectionColor = lerp( lerp(float4(baseColor, 1) * intersectionColorTexture, intersectionColorTexture, intersectionFill), float4(baseColor, 1), saturate(lerp(processedDistanceDepth, round(processedDistanceDepth), intersectionHardEdge)));	
	
		return finalIntersectionColor;
	}

	// Mesh intersection calculation.
	/*float4 MeshIntersection(float4 baseColor, float2 uv, sampler2D intersectionTexture, fixed intersectionTextureSmooth, float4 intersectionColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed intersectionDistance, fixed intersectionFalloff, fixed intersectionHardEdge) 
	{
		float4 finalIntersectionColor;

		float4 intersectionColorTexture = intersectionColor;

		float4 normalizedScreenPosition = screenPosition / screenPosition.w;
		normalizedScreenPosition.z = (UNITY_NEAR_CLIP_VALUE >= 0) ? normalizedScreenPosition.z : normalizedScreenPosition.z * 0.5 + 0.5;

		float screenDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition)));
		float distanceDepth = abs((screenDepth - LinearEyeDepth(normalizedScreenPosition.z)) / (intersectionDistance));
		float processedDistanceDepth = pow(distanceDepth, intersectionFalloff);
		//float distanceDepthMask = saturate(lerp(processedDistanceDepth, round(processedDistanceDepth), intersectionHardEdge) + (1 -  tex2D(intersectionTexture, uv).r));
		float distanceDepthMask = saturate(lerp(processedDistanceDepth, round(processedDistanceDepth), intersectionHardEdge) + (1 - smoothstep(0.0, intersectionTextureSmooth, tex2D(intersectionTexture, uv).r)));

		finalIntersectionColor = lerp(intersectionColorTexture, baseColor, distanceDepthMask);	
	
		return finalIntersectionColor;
	}*/

	// Mesh intersection calculation.
	float4 MeshIntersection(float3 viewDirection, float3 normal, float4 baseColor, float2 uv, sampler2D intersectionTexture, fixed intersectionTextureSmooth, float4 intersectionColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed intersectionDistance, fixed intersectionFalloff, fixed intersectionLevel, fixed2 scrollSpeed, fixed reverseTexture) 
	{
		float4 finalIntersectionColor;

		half NdotV = saturate(dot(normal, viewDirection));

		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = 0;

		if(unity_OrthoParams.w > 0)
		{
			// Orthographic camera.
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
			screenDepth = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
		}
		else {
			// Perspective camera.
			screenDepth = LinearEyeDepth(sceneZ);
		}

		float distanceDepth = abs(screenDepth - screenPosition.z);
		
		float distanceDepth_Improved = distanceDepth * NdotV * 2;

		float distanceDepth_Distance =  saturate(distanceDepth_Improved / intersectionDistance);

		float processedDistanceDepth = pow(distanceDepth_Distance, intersectionFalloff);

		float2 uvScroll = scrollSpeed * _Time.y;
		
		float3 intersectiontexture_Raw = lerp(tex2D(intersectionTexture, uv + uvScroll).rgb, 1 - tex2D(intersectionTexture, uv + uvScroll).rgb, reverseTexture);
		float4 intersectiontexture_Color = float4(intersectiontexture_Raw, tex2D(intersectionTexture, uv + uvScroll).a);

		float intersectiontexture_Smooth = smoothstep(intersectiontexture_Color.r - intersectionTextureSmooth, intersectiontexture_Color.r + intersectionTextureSmooth, saturate(intersectionLevel - processedDistanceDepth)) * saturate(1 - processedDistanceDepth);
		float intersectiontexture_Level = intersectiontexture_Smooth * intersectionColor.a;

		finalIntersectionColor = lerp(baseColor, intersectionColor, intersectiontexture_Level * 2);	
	
		return finalIntersectionColor;
	}

	// Mesh intersection calculation with double map.
	float4 MeshIntersectionDoubleMap(float3 viewDirection, float3 normal, float4 baseColor, float2 uv, sampler2D intersectionTexture, fixed intersectionTextureSmooth, float4 intersectionColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed intersectionDistance, fixed intersectionFalloff, fixed intersectionLevel, fixed4 scrollSpeed, fixed reverseTexture, fixed2 textureLevel, fixed textureBlending) 
	{
		float4 finalIntersectionColor;

		half NdotV = saturate(dot(normal, viewDirection));

		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = 0;

		if(unity_OrthoParams.w > 0)
		{
			// Orthographic camera.
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
			screenDepth = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
		}
		else {
			// Perspective camera.
			screenDepth = LinearEyeDepth(sceneZ);
		}

		float distanceDepth = abs(screenDepth - screenPosition.z);
		
		float distanceDepth_Improved = distanceDepth * NdotV * 2;

		float distanceDepth_Distance =  saturate(distanceDepth_Improved / intersectionDistance);

		float processedDistanceDepth = pow(distanceDepth_Distance, intersectionFalloff);

		float2 uvScrollFirst = scrollSpeed.xy * _Time.y;
		float2 uvScrollSecond = scrollSpeed.zw * _Time.y;
		
		float3 intersectiontexture_RawFirst = lerp(tex2D(intersectionTexture, uv + uvScrollFirst).rgb, 1 - tex2D(intersectionTexture, uv + uvScrollFirst).rgb, reverseTexture) * textureLevel.x;
		float3 intersectiontexture_RawSecond = lerp(tex2D(intersectionTexture, uv * 0.75 + uvScrollSecond + float2(0.5, 0.5)).rgb, 1 - tex2D(intersectionTexture, uv * 0.75 + uvScrollSecond + float2(0.5, 0.5)).rgb, reverseTexture) * textureLevel.y;
		float3 intersectiontexture_ColorMax = saturate(float3(max(intersectiontexture_RawFirst, intersectiontexture_RawSecond)));
		float3 intersectiontexture_ColorMin = saturate(float3(min(intersectiontexture_RawFirst, intersectiontexture_RawSecond)));
		float4 intersectiontexture_Color = float4(lerp(intersectiontexture_ColorMin, intersectiontexture_ColorMax, textureBlending), 1);

		float intersectiontexture_Smooth = smoothstep(intersectiontexture_Color.r - intersectionTextureSmooth, intersectiontexture_Color.r + intersectionTextureSmooth, saturate(intersectionLevel - processedDistanceDepth)) * saturate(1 - processedDistanceDepth);
		float intersectiontexture_Level = intersectiontexture_Smooth * intersectionColor.a;

		finalIntersectionColor = lerp(baseColor, intersectionColor, intersectiontexture_Level * 2);	
	
		return finalIntersectionColor;
	}



	// Soft particles calculation.
	float SoftParticles(float3 viewDirection, float3 normal, float baseAlpha, float2 uv, float4 screenPosition, sampler2D cameraDepthTexture, fixed fadeDistance, fixed fadeFalloff) 
	//float SoftParticles(float3 viewDirection, float3 normal, float4 baseColor, float2 uv, sampler2D intersectionTexture, fixed intersectionTextureSmooth, float4 intersectionColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed fadeDistance, fixed fadeFalloff, fixed intersectionLevel) 
	{
		float finalFadeAlpha;

		half NdotV = saturate(dot(normal, viewDirection));

		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = 0;

		if(unity_OrthoParams.w > 0)
		{
			// Orthographic camera.
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
			screenDepth = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
		}
		else {
			// Perspective camera.
			screenDepth = LinearEyeDepth(sceneZ);
		}

		float distanceDepth = abs(screenDepth - screenPosition.z);
		
		float distanceDepth_Improved = distanceDepth * NdotV * 2;

		float distanceDepth_Distance =  saturate(distanceDepth_Improved / fadeDistance);

		float processedDistanceDepth = pow(distanceDepth_Distance, fadeFalloff);


		/*float4 intersectiontexture_Color = tex2D(intersectionTexture, uv);

		float intersectiontexture_Smooth = smoothstep(intersectiontexture_Color.r - intersectionTextureSmooth, intersectiontexture_Color.r + intersectionTextureSmooth, saturate(intersectionLevel - processedDistanceDepth)) * saturate(1 - processedDistanceDepth);
		float intersectiontexture_Level = intersectiontexture_Smooth * intersectionColor.a;*/

		//finalFadeAlpha = lerp(baseColor, intersectionColor, intersectiontexture_Level);
		finalFadeAlpha = baseAlpha * processedDistanceDepth;
	
		return finalFadeAlpha;
	}


	// Water depth calculation.
	float4 WaterDepthSimple(float3 viewDirection, float3 normal, float4 baseColor, fixed shallowLight, float4 shallowColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed depthDistance, fixed depthFalloff, fixed useBaseColor) 
	{
		float4 finalwaterDepthColor;

		half NdotV = saturate(dot(normal, viewDirection));

		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = 0;

		if(unity_OrthoParams.w > 0)
		{
			// Orthographic camera.
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
			screenDepth = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
		}
		else {
			// Perspective camera.
			screenDepth = LinearEyeDepth(sceneZ);
		}

		float distanceDepth = abs(screenDepth - screenPosition.z);
		
		float distanceDepth_Improved = distanceDepth * NdotV * 2;

		float distanceDepth_Distance =  saturate(distanceDepth_Improved / depthDistance);

		float processedDistanceDepth = pow(distanceDepth_Distance, depthFalloff);

		float4 colorInterpolation = lerp(shallowColor + shallowLight, baseColor + shallowLight, useBaseColor);

		finalwaterDepthColor = lerp(baseColor, colorInterpolation, saturate(1 - processedDistanceDepth));
	
		return finalwaterDepthColor;
	}


	// Water depth calculation.
	float4 WaterDepth(float3 viewDirection, float3 normal, float4 shallowColor, float4 depthColor, float4 screenPosition, sampler2D cameraDepthTexture, fixed depthDistance, fixed depthFalloff) 
	{
		float4 finalwaterDepthColor;

		half NdotV = saturate(dot(normal, viewDirection));

		float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(cameraDepthTexture, UNITY_PROJ_COORD(screenPosition));
		float screenDepth = 0;

		if(unity_OrthoParams.w > 0)
		{
			// Orthographic camera.
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
			screenDepth = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
		}
		else {
			// Perspective camera.
			screenDepth = LinearEyeDepth(sceneZ);
		}

		float distanceDepth = abs(screenDepth - screenPosition.z);
		
		float distanceDepth_Improved = distanceDepth * NdotV * 2;

		float distanceDepth_Distance =  saturate(distanceDepth_Improved / depthDistance);

		float processedDistanceDepth = pow(distanceDepth_Distance, depthFalloff);

		finalwaterDepthColor = lerp(depthColor, shallowColor, saturate(1 - processedDistanceDepth));
	
		return finalwaterDepthColor;
	}



	// Reflection calculation. Use depth buffer.
	float3 Reflection (float3 normal, float eyeDepth, float3 specularIndirectLight, float3 viewDirection, float3 specularMask, float3 reflectionColor, fixed reflectionCustom, float4 screenPosition, sampler2D reflectionTexture, samplerCUBE reflectionCubemap, fixed reflectionCameraFading, half reflectionCameraFadingNearPoint, half reflectionCameraFadingFarPoint, fixed shininessLevel)
	{
		float3 reflection = float3(0, 0, 0);

		// Is very important checks the definition of this macro.
		#ifdef UNITY_IMAGE_BASED_LIGHTING_INCLUDED
			float3 reflectionDirection = reflect(-viewDirection, normal);

			half perceptualRoughness = 1 - shininessLevel;
			perceptualRoughness = perceptualRoughness * (1.7 - 0.7 * perceptualRoughness);
			
			half mip = perceptualRoughnessToMipmapLevel(perceptualRoughness);

			float3 reflectionCubemapColor = texCUBElod(reflectionCubemap, float4(reflectionDirection, mip)).rgb;

			float3 reflectionTextureColor = tex2Dproj(reflectionTexture, UNITY_PROJ_COORD(screenPosition)).rgb;

			float3 reflectionSource = lerp(specularIndirectLight, reflectionTextureColor * reflectionCubemapColor, reflectionCustom);
			
			float distance = DistanceBetweenCameraAndSurface(eyeDepth, reflectionCameraFadingNearPoint, reflectionCameraFadingFarPoint);

			reflection = lerp(0.0, reflectionColor * reflectionSource * 0.25, specularMask * ReflectionByCameraDistance(distance, reflectionCameraFading));
		#endif

		return reflection;
	}

	// Reflection calculation.
	float3 ReflectionSimple (float3 normal, float3 specularIndirectLight, float3 viewDirection, float3 specularMask, float3 reflectionColor, fixed reflectionCustom, float4 screenPosition, sampler2D reflectionTexture, samplerCUBE reflectionCubemap, fixed shininessLevel)
	{
		float3 reflection = float3(0, 0, 0);

		// Is very important checks the definition of this macro.
		#ifdef UNITY_IMAGE_BASED_LIGHTING_INCLUDED
			float3 reflectionDirection = reflect(-viewDirection, normal);

			half perceptualRoughness = 1 - shininessLevel;
			perceptualRoughness = perceptualRoughness * (1.7 - 0.7 * perceptualRoughness);
			
			half mip = perceptualRoughnessToMipmapLevel(perceptualRoughness);

			float3 reflectionCubemapColor = texCUBElod(reflectionCubemap, float4(reflectionDirection, mip)).rgb;

			float3 reflectionTextureColor = tex2Dproj(reflectionTexture, UNITY_PROJ_COORD(screenPosition)).rgb;

			float3 reflectionSource = lerp(specularIndirectLight, reflectionTextureColor * reflectionCubemapColor, reflectionCustom);


			reflection = lerp(0.0, reflectionColor * reflectionSource * 0.25, specularMask);
		#endif

		return reflection;
	}

	// Reflection calculation. Use depth buffer.
	float3 ReflectionStandard (float3 normal, float eyeDepth, float3 specularIndirectLight, float3 viewDirection, float3 reflectionColor, fixed reflectionCustom, float4 screenPosition, sampler2D reflectionTexture, samplerCUBE reflectionCubemap, fixed reflectionCameraFading, half reflectionCameraFadingNearPoint, half reflectionCameraFadingFarPoint, fixed shininessLevel)
	{
		float3 reflection = float3(0, 0, 0);

		// Is very important checks the definition of this macro.
		#ifdef UNITY_IMAGE_BASED_LIGHTING_INCLUDED
			float3 reflectionDirection = reflect(-viewDirection, normal);

			half perceptualRoughness = 1 - shininessLevel;
			perceptualRoughness = perceptualRoughness * (1.7 - 0.7 * perceptualRoughness);
			half mip = perceptualRoughnessToMipmapLevel(perceptualRoughness);
			float3 reflectionCubemapColor = texCUBElod(reflectionCubemap, float4(reflectionDirection, mip)).rgb;

			float3 reflectionTextureColor = tex2Dproj(reflectionTexture, UNITY_PROJ_COORD(screenPosition)).rgb;

			float3 reflectionSource = lerp(specularIndirectLight, reflectionTextureColor * reflectionCubemapColor, reflectionCustom);
			
			float distance = DistanceBetweenCameraAndSurface(eyeDepth, reflectionCameraFadingNearPoint, reflectionCameraFadingFarPoint);

			reflection = lerp(0.0, reflectionColor * reflectionSource, ReflectionByCameraDistance(distance, reflectionCameraFading));
		#endif

		return reflection;
	}

	// Blur additional function.
	half BlurSimpleHelper(half bhqp, half x)
	{
		return exp(-(x * x) / (2.0 * bhqp * bhqp));
	}

	// Blur simple for textures.
	half4 BlurSimple(half2 uv, sampler2D source, half intensity)
	{
		int iterations = 16;
		int halfIterations = iterations / 2;
		half sigmaX = 0.1 + intensity * 0.5;
		half sigmaY = sigmaX;
		half total = 0.0;
		half4 ret = half4(0, 0, 0, 0);
		for (int iy = 0; iy < iterations; ++iy)
		{
			half fy = BlurSimpleHelper(sigmaY, half(iy) -half(halfIterations));
			half offsety = half(iy - halfIterations) * 0.00390625;
			for (int ix = 0; ix < iterations; ++ix)
			{
				half fx = BlurSimpleHelper(sigmaX, half(ix) - half(halfIterations));
				half offsetx = half(ix - halfIterations) * 0.00390625;
				total += fx * fy;
				ret += tex2D(source, uv + half2(offsetx, offsety)) * fx * fy;
			}
		}
		return ret / total;
	}

	// Blur simple for grab textures.
	half4 BlurGrabTextureSimple(half2 uv, sampler2D source, half intensity)
	{
		int iterations = 16;
		int halfIterations = iterations / 2;
		half sigmaX = 0.1 + intensity * 0.5;
		half sigmaY = sigmaX;
		half total = 0.0;
		half4 ret = half4(0, 0, 0, 0);

		for (int iy = 0; iy < iterations; ++iy)
		{
			half fy = BlurSimpleHelper(sigmaY, half(iy) -half(halfIterations));
			half offsety = half(iy - halfIterations) * 0.00390625;
			for (int ix = 0; ix < iterations; ++ix)
			{
				half fx = BlurSimpleHelper(sigmaX, half(ix) - half(halfIterations));
				half offsetx = half(ix - halfIterations) * 0.00390625;
				total += fx * fy;
				ret += UNITY_SAMPLE_SCREENSPACE_TEXTURE(source, uv + half2(offsetx, offsety)) * fx * fy;
			}
		}
		return ret / total;
	}


	float BlurMediumHelper (float2 p) {
		p = frac(p * float2(123.34, 345.45));
		p += dot(p, p + 34.345);

		return frac(p.x * p.y);
	}

	// Blur medium for grab textures.
	float4 BlurGrabTextureMedium(sampler2D source, fixed intensity, fixed samples, float4 screenPosition, float4 grabScreenPosition)
	{
		float4 finalColor = float4(0, 0, 0, 0);

		float blur = (1 - intensity) * 7;
		blur *= 0.01;

		float a = BlurMediumHelper(screenPosition) * 6.2831;

		float numSamples = samples;
		for(float i = 0; i < numSamples; i++)
		{
			float2 blurOffset = float2(sin(a), cos(a)) * blur;
			
			float d = frac(sin((i + 1) * 546) * 5424);
			d = sqrt(d);
			blurOffset *= d;
			
			finalColor += UNITY_SAMPLE_SCREENSPACE_TEXTURE(source, grabScreenPosition + blurOffset);
			
			a++;
		}

        finalColor /= numSamples;

		return finalColor;
	}

	// Blur advanced for grab textures.
	// BASARLO EN EL SHADER PluginGaussianBlurUIBlur, ShadersLabBlur o en https://www.ronja-tutorials.com/2018/08/27/postprocessing-blur.html#boxblur
	float4 BlurGrabTextureAdvanced(sampler2D source, fixed intensity, fixed samples, float4 screenPosition, float4 grabScreenPosition)
	{
		float4 finalColor = float4(0, 0, 0, 0);

		return finalColor;
	}

	// Blur Standard for grab textures.
	// BASARLO EN EL SAHDER https://www.ronja-tutorials.com/2018/08/27/postprocessing-blur.html#gaussian-blur
	float4 BlurGrabTextureStandard(sampler2D source, fixed intensity, fixed samples, float4 screenPosition, float4 grabScreenPosition)
	{
		float4 finalColor = float4(0, 0, 0, 0);

		return finalColor;
	}


	// Refraction simple calculation. Return an offset to add to the grabScreenPos. The UnpackScaleNormal is needed to called in this function not inside the InitializeFragmentNormal function.
	float2 RefractionSimple (float2 uV, sampler2D refractionMap, fixed refractionLevel, fixed2 scrollSpeed)
	{
		float2 refraction = float2(0, 0);

		float2 uvScroll = scrollSpeed * _Time.y;
		refraction = UnpackScaleNormal(tex2D(refractionMap, uV + uvScroll), refractionLevel);
		
		return refraction;
	}

	// Refraction simple calculation with double normal map. Return an offset to add to the grabScreenPos. The UnpackScaleNormal is needed to called in this function not inside the InitializeFragmentNormal function.
	float2 RefractionSimpleDoubleMap (float2 uV, sampler2D refractionMap, fixed2 refractionLevel, fixed4 scrollSpeed)
	{
		float2 refraction = float2(0, 0);

		float2 uvScrollFirst = scrollSpeed.xy * _Time.y;
		float2 uvScrollSecond = scrollSpeed.zw * _Time.y;

		float3 refractionFirst = UnpackScaleNormal(tex2D(refractionMap, uV + uvScrollFirst), refractionLevel.x);
		float3 refractionSecond = UnpackScaleNormal(tex2D(refractionMap, float2(1.0, 1.0) - uV + uvScrollSecond), refractionLevel.y);
		refraction = BlendNormals(refractionFirst, refractionSecond);

		return refraction;
	}

	// Refraction simple calculation with mask. Return an offset to add to the grabScreenPos. The UnpackScaleNormal is needed to called in this function not inside the InitializeFragmentNormal function.
	float2 RefractionSimpleMasked (float2 uV, sampler2D refractionMap, fixed refractionLevel, fixed2 scrollSpeed, sampler2D maskMap)
	{
		float2 refraction = float2(0, 0);

		float2 uvScroll = scrollSpeed * _Time.y;
		refraction = UnpackScaleNormal(tex2D(refractionMap, uV + uvScroll), refractionLevel);

		float4 mask = tex2D(maskMap, uV);

		refraction = lerp(uV, refraction, mask.xy);
		
		return refraction;
	}


	// Refraction medium calculation. Return an offset to add to the grabScreenPos. "refractionIndex" value between -1 and 1.
	float4 RefractionMedium (float3 normalTangent, float3 normalBinormal, float3 normal, float3 viewDirection, fixed refractionIndex)
	{
		float4 refraction = float4(0, 0, 0, 0);

		float3x3 normalMatrix = float3x3(normalTangent, normalBinormal, normal);
		float3 refractionVector = refract(viewDirection, normal, refractionIndex);

		float3 normalWorldToTangentDirection = mul(normalMatrix, refractionVector);

		refraction = float4(normalWorldToTangentDirection, 0);
		
		return refraction;
	}

	// Refraction advanced calculation. ASE example. Return an offset to add to the grabScreenPos. "refractionIndex" value between -3 and 4. Uses the normal of the mesh too.
	float2 RefractionAdvanced (float3 normal, float screenPosZ, float3 viewDirection, fixed refractionIndex)
	{
		float2 refraction = float2(0, 0);

		float3 refractionOffset = (refractionIndex - 1.0) * mul(UNITY_MATRIX_V, float4(normal, 0.0)) * (1.0 / (screenPosZ + 1.0)) * (1.0 - dot(normal, viewDirection));
		float2 cameraRefraction = float2(refractionOffset.x, -(refractionOffset.y * _ProjectionParams.x));
		refraction = cameraRefraction;
		
		return refraction;
	}

	// Chromatic dispersion for Refraction advanced calculation. ASE example. Return the final color of the fragment program.
	float3 ChromaticDispersionForRefractionAdvanced (sampler2D grabTexture, float2 grabScreenPosition, float2 refractionOffset, fixed chromaticDispersion, float3 finalColor, float3 grabScreenColor, float alphaValue)
	{	
		float3 finalColorWithDispersion = finalColor;

		// Apply the refraction chromatic dispersion.
		float chromaticDispersion_GreenChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition + refractionOffset * (1.0 - chromaticDispersion)).g;
		float chromaticDispersion_BlueChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition + refractionOffset * (1.0 + chromaticDispersion)).b;
		
		// Chromatic dispersion color composition.
		grabScreenColor.rgb = float3(grabScreenColor.r, chromaticDispersion_GreenChannel, chromaticDispersion_BlueChannel);
		// Adjust the brighness of the chromatic dispersion color composition.
		grabScreenColor.rgb = grabScreenColor.rgb * 0.85;
		#ifdef _BLENDMODE_NORMAL
			finalColorWithDispersion.rgb = lerp(finalColorWithDispersion.rgb * grabScreenColor.rgb, finalColorWithDispersion.rgb, alphaValue);
		#endif
		
		return finalColorWithDispersion;
	}

	// Refraction standard calculation. Return an offset to add to the grabScreenPos. "refractionIndex" value between -0.1 and 0.1. Uses the normal of the mesh too.
	float2 RefractionStandard (float3 normalTangent, float3 normalBinormal, float3 normal, float3 viewDirection, fixed refractionIndex, float2 grabScreenPosition)
	{
		float2 refraction = float2(0, 0);

		float3x3 normalMatrix = float3x3(normalTangent, normalBinormal, normal);
		float NdotV = dot(normal + normalMatrix[2], -viewDirection);

		refraction = (refractionIndex / 10) * float2(
			dot(normalMatrix[2], UNITY_MATRIX_V[0].xyz * dot(viewDirection, UNITY_MATRIX_V[2].xyz)), 
			dot(normalMatrix[2], UNITY_MATRIX_V[1].xyz * dot(normalMatrix[2], UNITY_MATRIX_V[2].xyz))
			) * (NdotV * NdotV);

		half2 dampPosition = abs(grabScreenPosition.xy * 2 - 1);
		half borderDamp = saturate(1 - max((dampPosition.x - 0.9) / (1 - 0.9), (dampPosition.y - 0.85) / (1 - 0.85)));

		refraction = refraction * borderDamp;

		return refraction;
	}

	// Chromatic dispersion for Refraction advanced calculation. Return the final color of the fragment program.
	float3 ChromaticDispersionForRefractionStandard (float3 normalTangent, float3 normalBinormal, float3 normal, float3 viewDirection, sampler2D grabTexture, float4 grabScreenPosition, float2 refractionOffset, fixed chromaticDispersion, fixed alphaValue, fixed dispersionOffset, fixed dispersionSmooth, fixed factor)
	{	
		float3x3 normalMatrix = float3x3(normalTangent, normalBinormal, normal);
		float NdotV = dot(normal + normalMatrix[2], -viewDirection);

		// Apply the refraction chromatic dispersion.
		fixed chromaticDispersion_GreenChannelOffset = lerp(1.0 - (chromaticDispersion / 10), 1, NdotV);
		fixed chromaticDispersion_BlueChannelOffset = lerp(1.0 + (chromaticDispersion / 10), 1, NdotV);


		// Without blur.
		/*float chromaticDispersion_RedChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset).r;
		float chromaticDispersion_GreenChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset * chromaticDispersion_GreenChannelOffset).g;
		float chromaticDispersion_BlueChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset * chromaticDispersion_BlueChannelOffset).b;

		// Chromatic dispersion color composition.
		float3 grabScreenColor_Dispersion = float3(chromaticDispersion_RedChannel, chromaticDispersion_GreenChannel, chromaticDispersion_BlueChannel);*/


		// With blur.
		float2 chromaticDispersion_GreenChannelUV = grabScreenPosition.xy + refractionOffset * chromaticDispersion_GreenChannelOffset;
		float2 chromaticDispersion_BlueChannelUV = grabScreenPosition.xy + refractionOffset * chromaticDispersion_BlueChannelOffset;

		float4 grabScreenColor_RedChannelBlur = BlurGrabTextureSimple(grabScreenPosition.xy + refractionOffset, grabTexture, factor);
		float4 grabScreenColor_GreenChannelBlur = BlurGrabTextureSimple(chromaticDispersion_GreenChannelUV, grabTexture, factor);
		float4 grabScreenColor_BlueChannelBlur = BlurGrabTextureSimple(chromaticDispersion_BlueChannelUV, grabTexture, factor);

		// Chromatic dispersion color composition.
		float3 grabScreenColor_Dispersion = float3(grabScreenColor_RedChannelBlur.r, grabScreenColor_GreenChannelBlur.g, grabScreenColor_BlueChannelBlur.b);

		
		// Without fresnel mask.
		//float3 chromaticDispersion_Color = grabScreenColor_Dispersion.rgb * (1 - alphaValue);

		// With fresnel mask.
		float fresnelMask = saturate(FresnelStandard(normal, viewDirection, 0, dispersionOffset, dispersionSmooth));
		float4 grabScreenColor = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition + refractionOffset);
		float3 chromaticDispersion_Color = lerp(grabScreenColor.rgb, grabScreenColor_Dispersion.rgb, fresnelMask) * (1 - alphaValue);

		return chromaticDispersion_Color;
	}

	// Chromatic dispersion for Refraction advanced calculation. Return the final color of the fragment program.
	float3 ChromaticDispersionForRefractionStandardWithGlossTranslucency (float3 normalTangent, float3 normalBinormal, float3 normal, float3 viewDirection, sampler2D grabTexture, float4 grabScreenPosition, float2 refractionOffset, fixed chromaticDispersion, fixed alphaValue, fixed dispersionOffset, fixed dispersionSmooth, fixed factor, fixed intensity, fixed samples, float4 screenPosition)
	{	
		float3x3 normalMatrix = float3x3(normalTangent, normalBinormal, normal);
		float NdotV = dot(normal + normalMatrix[2], -viewDirection);

		// Apply the refraction chromatic dispersion.
		fixed chromaticDispersion_GreenChannelOffset = lerp(1.0 - (chromaticDispersion / 10), 1, NdotV);
		fixed chromaticDispersion_BlueChannelOffset = lerp(1.0 + (chromaticDispersion / 10), 1, NdotV);


		// Without blur.
		/*float chromaticDispersion_RedChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset).r;
		float chromaticDispersion_GreenChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset * chromaticDispersion_GreenChannelOffset).g;
		float chromaticDispersion_BlueChannel = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition.xy + refractionOffset * chromaticDispersion_BlueChannelOffset).b;

		// Chromatic dispersion color composition.
		float3 grabScreenColor_Dispersion = float3(chromaticDispersion_RedChannel, chromaticDispersion_GreenChannel, chromaticDispersion_BlueChannel);*/


		// Blur for chromatic dispersion.
		float2 chromaticDispersion_GreenChannelUV = grabScreenPosition.xy + refractionOffset * chromaticDispersion_GreenChannelOffset;
		float2 chromaticDispersion_BlueChannelUV = grabScreenPosition.xy + refractionOffset * chromaticDispersion_BlueChannelOffset;

		// Gloss translucence.
		#ifdef _GLOSSTRANSLUCENCY_ON
			float4 grabScreenColor_RedChannelBlur = BlurGrabTextureMedium(grabTexture, intensity, samples, screenPosition, grabScreenPosition + float4(refractionOffset, 0, 0));
			float4 grabScreenColor_GreenChannelBlur = BlurGrabTextureMedium(grabTexture, intensity, samples, screenPosition, float4(chromaticDispersion_GreenChannelUV, 0, 0));
			float4 grabScreenColor_BlueChannelBlur = BlurGrabTextureMedium(grabTexture, intensity, samples, screenPosition, float4(chromaticDispersion_BlueChannelUV, 0, 0));
		#else
			float4 grabScreenColor_RedChannelBlur = BlurGrabTextureSimple(grabScreenPosition.xy + refractionOffset, grabTexture, factor);
			float4 grabScreenColor_GreenChannelBlur = BlurGrabTextureSimple(chromaticDispersion_GreenChannelUV, grabTexture, factor);
			float4 grabScreenColor_BlueChannelBlur = BlurGrabTextureSimple(chromaticDispersion_BlueChannelUV, grabTexture, factor);
		#endif

		// Chromatic dispersion color composition.
		float3 grabScreenColor_Dispersion = float3(grabScreenColor_RedChannelBlur.r, grabScreenColor_GreenChannelBlur.g, grabScreenColor_BlueChannelBlur.b);

		
		// Without fresnel mask.
		//float3 chromaticDispersion_Color = grabScreenColor_Dispersion.rgb * (1 - alphaValue);

		// Fresnel mask.
		fixed fresnelMask = saturate(FresnelStandard(normal, viewDirection, 0, dispersionOffset, dispersionSmooth));
		
		float4 grabScreenColor = UNITY_SAMPLE_SCREENSPACE_TEXTURE(grabTexture, grabScreenPosition + refractionOffset);

		// Gloss translucence.
		#ifdef _GLOSSTRANSLUCENCY_ON
			grabScreenColor.rgb = BlurGrabTextureMedium(grabTexture, intensity, samples, screenPosition, grabScreenPosition + float4(refractionOffset, 0, 0)).rgb;
		#endif

		float3 chromaticDispersion_Color = lerp(grabScreenColor.rgb, grabScreenColor_Dispersion.rgb, fresnelMask) * (1 - alphaValue);

		return chromaticDispersion_Color;
	}



	// Sub surface scattering without ray casting.
	// https://www.patreon.com/posts/subsurface-write-20905461
	// https://www.alanzucconi.com/2017/08/30/fast-subsurface-scattering-2/
	// https://colinbarrebrisebois.com/2011/03/07/gdc-2011-approximating-translucency-for-a-fast-cheap-and-convincing-subsurface-scattering-look/
	float3 SubSurfaceScattering (float3 lightDirection, fixed lightAttenuation, float3 viewDirection, float3 normal, fixed subSurfaceScattering, fixed SSSSmooth, fixed SSSOffset, sampler2D SSSThicknessMap, float2 uv, fixed SSSMultiplier, fixed nonDirectionalLightPenetration, fixed shadowStrength, fixed SSSInternalColorLevel, float4 SSSInternalColor, float3 lightColor, float lightIntensity){
		
		float3 subSurfaceScatteringColor = float3(0, 0, 0);
		
		float sss_LightDirection = dot(viewDirection, -(lightDirection + normal * subSurfaceScattering));

		float sss_Power;
		#if defined(POINT) || defined(POINT_COOKIE) || defined(SPOT)
			sss_Power = pow(lightAttenuation, SSSSmooth);
		#else
			sss_Power = pow(sss_LightDirection, SSSSmooth);
		#endif
		float sss_Scale = dot(sss_Power, SSSOffset);

		float sss_Mask = saturate(sss_Scale * tex2D(SSSThicknessMap, uv).r * SSSMultiplier);

		float sss_LightAttenuation;
		#if defined(POINT) || defined(POINT_COOKIE) || defined(SPOT)
			sss_LightAttenuation = sss_Mask * lightAttenuation * nonDirectionalLightPenetration;
		#else
			sss_LightAttenuation = sss_Mask * saturate(lightAttenuation + (1 - shadowStrength));
		#endif

		float sss_FaceMask = sss_LightAttenuation;
		float sss_FaceMaskPower = saturate(pow(sss_FaceMask, SSSInternalColorLevel));

		float3 sss_Color = lerp(SSSInternalColor, lightColor, sss_FaceMaskPower);
		float3 sss_ColorPower = sss_Color * lightIntensity * sss_FaceMask;

		subSurfaceScatteringColor = sss_ColorPower;

		return subSurfaceScatteringColor;
	}

	



	// ------------------------------------------------------------------
	//  Math.

	// Remap a Float.
	inline float RemapFloat(float value, float minOld, float maxOld, float minNew, float maxNew)
	{
		float4 remapedValue = minNew + (value - minOld) * (maxNew - minNew) / (maxOld - minOld);
		
		return remapedValue;
	}

	// Remap a Vector 2.
	inline float2 RemapVector2(float2 value, float2 minOld, float2 maxOld, float2 minNew, float2 maxNew)
	{
		float2 remapedValue = minNew + (value - minOld) * (maxNew - minNew) / (maxOld - minOld);
	
		return remapedValue;
	}

	// Remap a Vector 3.
	inline float3 RemapVector3(float3 value, float3 minOld, float3 maxOld, float3 minNew, float3 maxNew)
	{
		float3 remapedValue = minNew + (value - minOld) * (maxNew - minNew) / (maxOld - minOld);
	
		return remapedValue;
	}

	// Remap a Vector 4.
	inline float4 RemapVector4(float4 value, float4 minOld, float4 maxOld, float4 minNew, float4 maxNew)
	{
		float4 remapedValue = minNew + (value - minOld) * (maxNew - minNew) / (maxOld - minOld);
	
		return remapedValue;
	}



#endif