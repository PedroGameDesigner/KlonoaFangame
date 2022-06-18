///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 19/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano, David Cuenca
/// Description: Class that extends the utility and functionality of CGFMaterialEditorUtilitiesClass.
///

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

/// \english
/// <summary>
/// Class that extends the utility and functionality of CGFMaterialEditorUtilitiesClass.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Clase que extiende las utilidades y funcionalidades de CGFMaterialEditorUtilitiesClass.
/// </summary>
/// \endspanish
public class CGFMaterialEditorUtilitiesExtendedClass : CGFMaterialEditorUtilitiesClass
{

    #region Public Variables


    #endregion


    #region Utilities

    public static string CheckRenderMode(float renderMode = 0)
    {

        string colorString = "";

        if (renderMode == 0)
        {

            colorString = "(RGB)";

        }
        else if (renderMode == 1 || renderMode == 2)
        {

            colorString = "(RGBA)";

        }

        return colorString;

    }

    public static string CheckRenderModeTransparent(float renderMode = 0)
    {

        string colorString = "(RGBA)";

        return colorString;

    }

    public static string CheckRenderModeStandard(float renderMode = 0)
    {

        string colorString = "";

        if (renderMode == 1 || renderMode == 2 || renderMode == 3)
        {

            colorString = "(RGBA)";

        }
        else if (renderMode == 0)
        {

            colorString = "(RGB)";

        }

        return colorString;

    }

    public static string CheckRenderModeStandardTransparent(float renderMode = 0)
    {

        string colorString = "(RGBA)";

        return colorString;

    }

    public static void BuildMaterialComponentLocker(Type component)
    {

        List<Type> existingComponents = new List<Type>();

        bool componentAdded = false;

        if (Selection.activeGameObject != null)
        {

            foreach (Component components in Selection.activeGameObject.GetComponents<Component>())
            {

                existingComponents.Add(components.GetType());

            }

        }

        foreach (Type existingComponent in existingComponents)
        {

            if (existingComponents.Contains(component))
            {

                componentAdded = true;

            }

        }

        if (componentAdded == false)
        {

            GUI.enabled = false;

        }
        else
        {

            GUI.enabled = true;

        }

    }

    #endregion


    #region Utility Methods

    /// \english
    /// <summary>
    /// Color gradient function builder.
    /// </summary>
    /// <param name="gradient">Gradient property.</param>
    /// <param name="topColor">Top color property.</param>
    /// <param name="center">Center property.</param>
    /// <param name="width">Width property.</param>
    /// <param name="revert">Revert property.</param>
    /// <param name="changeDirection">Change direction property.</param>
    /// <param name="rotation">Rotation property.</param>
    /// <param name="steps">Steps property.</param>
    /// <param name="renderMode">Render mode property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de degradado de color.
    /// </summary>
    /// <param name="gradient">Propiedad degradado.</param>
    /// <param name="topColor">Propiedad color superior.</param>
    /// <param name="center">Propiedad centro.</param>
    /// <param name="width">Propiedad ancho.</param>
    /// <param name="revert">Propiedad inverso.</param>
    /// <param name="changeDirection">Propiedad cambiar direccion.</param>
    /// <param name="rotation">Propiedad rotación.</param>
    /// <param name="steps">Propiedad pasos.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorGradient(MaterialProperty gradient, MaterialProperty topColor, MaterialProperty center, MaterialProperty width, MaterialProperty revert, MaterialProperty changeDirection, MaterialProperty rotation, MaterialProperty steps, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Color Gradient", "Color Gradient.", gradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Gradient Top Color " + CheckRenderMode(renderMode), "Color of the top part of the gradient.", topColor, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Center", "Gradient center.", center, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Gradient Width", "Gradient width.", width, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Revert", "Revert the ortientation of the gradient.", revert, toggleLock: gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Change Direction", "Change direction of the gradient.", changeDirection, toggleLock: gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Rotation", "Gradient rotation.", rotation, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Gradient Steps", "Gradient steps.", steps, gradient.floatValue);
    }

    /// \english
    /// <summary>
    /// Color fill functions builder.
    /// </summary>
    /// <param name="colorFill">Color fill property.</param>
    /// <param name="colorFillLevel">Color fill level property.</param>
    /// <param name="renderMode">Render mode property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de las funciones de relleno de color.
    /// </summary>
    /// <param name="colorFill">Propiedad relleno de color</param>
    /// <param name="colorFillLevel">Propiedad de nivel del relleno de color.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorFill(MaterialProperty colorFill, MaterialProperty colorFillLevel)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Color Fill", "Color Fill.", colorFill, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Color Fill Level", "Level of color fill in relation the source color.", colorFillLevel, colorFill.floatValue);
    }

    /// \english
    /// <summary>
    /// Color gradient and Color fill functions builder.
    /// </summary>
    /// <param name="gradient">Gradient property.</param>
    /// <param name="topColor">Top color property.</param>
    /// <param name="center">Center property.</param>
    /// <param name="width">Width property.</param>
    /// <param name="revert">Revert property.</param>
    /// <param name="changeDirection">Change direction property.</param>
    /// <param name="rotation">Rotation property.</param>
    /// <param name="colorFill">Color fill property.</param>
    /// <param name="colorFillLevel">Color fill level property.</param>
    /// <param name="topColorFillLevel">Top color fill level property.</param>
    /// <param name="renderMode">Render mode property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de las funciones de degradado de color y relleno de color.
    /// </summary>
    /// <param name="gradient">Propiedad degradado.</param>
    /// <param name="topColor">Propiedad color superior.</param>
    /// <param name="center">Propiedad centro.</param>
    /// <param name="width">Propiedad ancho.</param>
    /// <param name="revert">Propiedad inverso.</param>
    /// <param name="changeDirection">Propiedad cambiar direccion.</param>
    /// <param name="rotation">Propiedad rotación.</param>
    /// <param name="colorFill">Propiedad relleno de color</param>
    /// <param name="colorFillLevel">Propiedad de nivel del relleno de color.</param>
    /// <param name="topColorFillLevel">Propiedad del nivel del relleno del color superior</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorGradientAndColorFill(MaterialProperty gradient, MaterialProperty topColor, MaterialProperty center, MaterialProperty width, MaterialProperty revert, MaterialProperty changeDirection, MaterialProperty rotation, MaterialProperty colorFill, MaterialProperty colorFillLevel, MaterialProperty topColorFillLevel, float renderMode = 0)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Color Gradient", "Color Gradient.", gradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color " + CheckRenderMode(renderMode), "Color of the top part of the gradient.", topColor, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Center", "Gradient center.", center, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Width", "Gradient width.", width, gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Revert", "Revert the ortientation of the gradient.", revert, toggleLock: gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Change Direction", "Change direction of the gradient.", changeDirection, toggleLock: gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rotation", "Gradient rotation.", rotation, gradient.floatValue);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Color Fill", "Color Fill.", colorFill, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Color Fill Level", "Level of color fill in relation the source color.", colorFillLevel, colorFill.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Color Fill Level", "Level of top color fill in relation the source color.", topColorFillLevel, colorFill.floatValue * gradient.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Color mask function builder. 
    /// </summary>
    /// <param name="colorMask">Color mask property.</param>
    /// <param name="colorMaskMap">Color mask map property.</param>
    /// <param name="colorMaskColor">Color mask color property.</param>
    /// <param name="colorMaskLevel">Color mask level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de máscara de color.
    /// </summary>
    /// <param name="colorMask">Propiedad de máscara de color.</param>
    /// <param name="colorMaskMap">Propiedad del mapa de la máscara de color.</param>
    /// <param name="colorMaskColor">Propiedad del color de la máscara.</param>
    /// <param name="colorMaskLevel">Propiedad nivel de la máscara</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorMask(MaterialProperty colorMask, MaterialProperty colorMaskMap, MaterialProperty colorMaskColor, MaterialProperty colorMaskLevel, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Color Mask", "Color Fill.", colorMask, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Color Mask Map " + CheckRenderMode(renderMode), "Mask texture.", colorMaskMap, materialEditor, scaleOffset, colorMask.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildColor("Color Mask Color " + CheckRenderMode(renderMode), "Color of the mask.", colorMaskColor, colorMask.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Color Mask Level", "Level of color mask in relation the source color.", colorMaskLevel, colorMask.floatValue);
    }

    /// \english
    /// <summary>
    /// Color mask function builder without keyword. 
    /// </summary>
    /// <param name="colorMask">Color mask property.</param>
    /// <param name="colorMaskMap">Color mask map property.</param>
    /// <param name="colorMaskColor">Color mask color property.</param>
    /// <param name="colorMaskLevel">Color mask level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de máscara de color sin keyword.
    /// </summary>
    /// <param name="colorMask">Propiedad de máscara de color.</param>
    /// <param name="colorMaskMap">Propiedad del mapa de la máscara de color.</param>
    /// <param name="colorMaskColor">Propiedad del color de la máscara.</param>
    /// <param name="colorMaskLevel">Propiedad nivel de la máscara</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorMaskWithoutKeyword(MaterialProperty colorMask, MaterialProperty colorMaskMap, MaterialProperty colorMaskColor, MaterialProperty colorMaskLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Color Mask", "Color Fill.", colorMask);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Color Mask Map " + CheckRenderMode(renderMode), "Mask texture.", colorMaskMap, materialEditor, true, colorMask.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildColor("Color Mask Color " + CheckRenderMode(renderMode), "Color of the mask.", colorMaskColor, colorMask.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Color Mask Level", "Level of color mask in relation the source color.", colorMaskLevel, colorMask.floatValue);
    }

    /// \english
    /// <summary>
    /// Fluid type function builder. 
    /// </summary>
    /// <param name="fluidType">Fluid type property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de tipo de fluido.
    /// </summary>
    /// <param name="fluidType">Propiedad de tipo de fluido.</param>
    /// \endspanish
    public static void BuildFluidType(MaterialProperty fluidType)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Fluid Type", "Type of surface of the fluid.");
        CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Fluid Type", "Type of surface of the fluid.", new string[]{"Smooth", "Flat"}, fluidType);
    }


    /// \english
    /// <summary>
    /// Fluid type function builder header group.
    /// </summary>
    /// <param name="fluidType">Fluid type property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de tipo de fluido grupo.
    /// </summary>
    /// <param name="fluidType">Propiedad de tipo de fluido.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildFluidType(MaterialProperty fluidType, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Fluid Type", "Type of surface of the fluid.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Fluid Type", "Type of surface of the fluid.", new string[]{"Smooth", "Flat"}, fluidType);
        }
    }

    
    /// \english
    /// <summary>
    /// Height fog builder.
    /// </summary>
    /// <param name="heightFog">Height fog property.</param>
    /// <param name="heightFogColor">Height fog color property.</param>
    /// <param name="heightFogStartPosition">Height fog start position property.</param>
    /// <param name="fogHeight">Fog height property.</param>
    /// <param name="heightFogDensity">Height fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="localHeightFog">Local height property.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por altura.
    /// </summary>
    /// <param name="heightFog">Propiedad niebla por altura.</param>
    /// <param name="heightFogColor">Propiedad color de la niebla por altura.</param>
    /// <param name="heightFogStartPosition">Propiedad posición de inicio de la niebla por altura.</param>
    /// <param name="fogHeight">Propiedad altura de la niebla por altura.</param>
    /// <param name="heightFogDensity">Propiedad densidad de la niebla por altura</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="localHeightFog">Propiedad niebla por altura local.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildHeightFog(MaterialProperty heightFog, MaterialProperty heightFogColor, MaterialProperty heightFogStartPosition, MaterialProperty fogHeight, MaterialProperty heightFogDensity, MaterialProperty useAlphaValue, MaterialProperty localHeightFog, out bool showGizmo, string playerPrefKeyName, float useAlphaAndAlphaClip = 1)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Height Fog", "Fog by vertex height.", heightFog, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Height Fog Color (RGB)", "Color of the fog.", heightFogColor, heightFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Height Fog Start Position", "Start point of the fog.", heightFogStartPosition, heightFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Fog Height", "Height of the fog.", fogHeight, heightFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Height Fog Density", "Level of fog in relation the source color.", heightFogDensity, heightFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Alpha", "If enabledd fog doesn't affect the transparent parts of the source color.", useAlphaValue, toggleLock: heightFog.floatValue * useAlphaAndAlphaClip);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Local Height Fog", "If enabledd the fog is created based on the center of the mesh.", localHeightFog, toggleLock: heightFog.floatValue);

        showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Height Fog Gizmo", "If enabled show height fog gizmo.", heightFog.floatValue, heightFog, playerPrefKeyName);
    }

    /// \english
    /// <summary>
    /// Height fog builder header group.
    /// </summary>
    /// <param name="heightFog">Height fog property.</param>
    /// <param name="heightFogColor">Height fog color property.</param>
    /// <param name="heightFogStartPosition">Height fog start position property.</param>
    /// <param name="fogHeight">Fog height property.</param>
    /// <param name="heightFogDensity">Height fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="localHeightFog">Local height property.</param>
    /// <param name="showGizmo">Show gizmo</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por altura.
    /// </summary>
    /// <param name="heightFog">Propiedad niebla por altura.</param>
    /// <param name="heightFogColor">Propiedad color de la niebla por altura.</param>
    /// <param name="heightFogStartPosition">Propiedad posición de inicio de la niebla por altura.</param>
    /// <param name="fogHeight">Propiedad altura de la niebla por altura.</param>
    /// <param name="heightFogDensity">Propiedad densidad de la niebla por altura</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="localHeightFog">Propiedad niebla por altura local.</param>
    /// <param name="showGizmo">Muestra el gizmo.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildHeightFog(MaterialProperty heightFog, MaterialProperty heightFogColor, MaterialProperty heightFogStartPosition, MaterialProperty fogHeight, MaterialProperty heightFogDensity, MaterialProperty useAlphaValue, MaterialProperty localHeightFog, out bool showGizmo, bool isUnfold, string playerPrefKeyName, float useAlphaAndAlphaClip = 1)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Height Fog", "Fog by vertex height.", heightFog, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildColor("Height Fog Color (RGB)", "Color of the fog.", heightFogColor, heightFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Height Fog Start Position", "Start point of the fog.", heightFogStartPosition, heightFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Fog Height", "Height of the fog.", fogHeight, heightFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Height Fog Density", "Level of fog in relation the source color.", heightFogDensity, heightFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Alpha", "If enabledd fog doesn't affect the transparent parts of the source color.", useAlphaValue, toggleLock: heightFog.floatValue * useAlphaAndAlphaClip);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Local Height Fog", "If enabledd the fog is created based on the center of the mesh.", localHeightFog, toggleLock: heightFog.floatValue);
            
            showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Height Fog Gizmo", "If enabled show height fog gizmo.", heightFog.floatValue, heightFog, playerPrefKeyName);
        }
    }

    /// \english
    /// <summary>
    /// Distance fog builder.
    /// </summary>
    /// <param name="distanceFog">Distance fog property.</param>
    /// <param name="distanceFogColor">Distance fog color property.</param>
    /// <param name="distanceFogStartPosition">Distance fog start position property.</param>
    /// <param name="distanceFogLength">Distance fog length property.</param>
    /// <param name="distanceFogDensity">Distance fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="worldDistanceFog">World distance fog property.</param>
    /// <param name="worldDistanceFogPosition">World distance fog position property.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por distancia.
    /// </summary>
    /// <param name="distanceFog">Propiedad niebla por distancia.</param>
    /// <param name="distanceFogColor">Propiedad color de la niebla por distancia.</param>
    /// <param name="distanceFogStartPosition">Propiedad posición de inicio de la niebla por distancia.</param>
    /// <param name="distanceFogLength">Propiedad longitud de la niebla por altura.</param>
    /// <param name="distanceFogDensity">Propiedad densidad de la niebla por distancia.</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="worldDistanceFog">Propiedad niebla por distancia en el mundo.</param>
    /// <param name="worldDistanceFogPosition">Propiedad posición de la niebla por distancia en el mundo.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildDistanceFog(MaterialProperty distanceFog, MaterialProperty distanceFogColor, MaterialProperty distanceFogStartPosition, MaterialProperty distanceFogLength, MaterialProperty distanceFogDensity, MaterialProperty useAlpha, MaterialProperty worldDistanceFog, MaterialProperty worldDistanceFogPosition, float useAlphaAndAlphaClip = 1)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Distance Fog", "Fog by camera distance.", distanceFog, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Distance Fog Color (RGB)", "Color of the fog.", distanceFogColor, distanceFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Distance Fog Start Position", "Start point of the fog.", distanceFogStartPosition, distanceFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Distance Fog Length", "Length of the fog.", distanceFogLength, distanceFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Distance Fog Density", "Level of fog in relation the source color.", distanceFogDensity, distanceFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Alpha", "If enabledd fog doesn't affect the transparent parts of the source color.", useAlpha, toggleLock: distanceFog.floatValue * useAlphaAndAlphaClip);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("World Distance Fog", "If enabledd the fog is created based on a position of the world.", worldDistanceFog, toggleLock: distanceFog.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("World Distance Fog Position", "World position of the distance fog.", worldDistanceFogPosition, distanceFog.floatValue);
    }

    /// \english
    /// <summary>
    /// Distance fog builder header group.
    /// </summary>
    /// <param name="distanceFog">Distance fog property.</param>
    /// <param name="distanceFogColor">Distance fog color property.</param>
    /// <param name="distanceFogStartPosition">Distance fog start position property.</param>
    /// <param name="distanceFogLength">Distance fog length property.</param>
    /// <param name="distanceFogDensity">Distance fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="worldDistanceFog">World distance fog property.</param>
    /// <param name="worldDistanceFogPosition">World distance fog position property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
    /// <param name="showGizmo">Show gizmo</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por distancia grupo.
    /// </summary>
    /// <param name="distanceFog">Propiedad niebla por distancia.</param>
    /// <param name="distanceFogColor">Propiedad color de la niebla por distancia.</param>
    /// <param name="distanceFogStartPosition">Propiedad posición de inicio de la niebla por distancia.</param>
    /// <param name="distanceFogLength">Propiedad longitud de la niebla por altura.</param>
    /// <param name="distanceFogDensity">Propiedad densidad de la niebla por distancia.</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="worldDistanceFog">Propiedad niebla por distancia en el mundo.</param>
    /// <param name="worldDistanceFogPosition">Propiedad posición de la niebla por distancia en el mundo.</param>
    /// <param name="showGizmo">Muestra el gizmo.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildDistanceFog(MaterialProperty distanceFog, MaterialProperty distanceFogColor, MaterialProperty distanceFogStartPosition, MaterialProperty distanceFogLength, MaterialProperty distanceFogDensity, MaterialProperty useAlpha, MaterialProperty worldDistanceFog, MaterialProperty worldDistanceFogPosition, out bool showGizmo, bool isUnfold, string playerPrefKeyName, float useAlphaAndAlphaClip = 1)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Distance Fog", "Fog by camera distance.", distanceFog, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildColor("Distance Fog Color (RGB)", "Color of the fog.", distanceFogColor, distanceFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Distance Fog Start Position", "Start point of the fog.", distanceFogStartPosition, distanceFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Distance Fog Length", "Length of the fog.", distanceFogLength, distanceFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Distance Fog Density", "Level of fog in relation the source color.", distanceFogDensity, distanceFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Alpha", "If enabledd fog doesn't affect the transparent parts of the source color.", useAlpha, toggleLock: distanceFog.floatValue * useAlphaAndAlphaClip);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("World Distance Fog", "If enabledd the fog is created based on a position of the world.", worldDistanceFog, toggleLock: distanceFog.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildVector3("World Distance Fog Position", "World position of the distance fog.", worldDistanceFogPosition, distanceFog.floatValue);

            showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Distance Fog Gizmo", "If enabled show distance fog gizmo.", distanceFog.floatValue, distanceFog, playerPrefKeyName);
        }
    }

    /// \english
    /// <summary>
    /// Mesh intersection function builder.
    /// </summary>
    /// <param name="meshIntersection">Mesh intersection property.</param>
    /// <param name="intersectionColor">Intersection color property.</param>
    /// <param name="intersectionTexture">Intersection texture property.</param>
    /// <param name="intersectionFalloff">Intersection falloff property.</param>
    /// <param name="intersectionDistance">Intersection distance property.</param>
    /// <param name="intersectionFill">Intersection fill property.</param>
    /// <param name="intersectionHardEdge">Intersection hard edge property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la intersección de la mesh.
    /// </summary>
    /// <param name="meshIntersection">Propiedad intersección de la mesh.</param>
    /// <param name="intersectionColor">Propiedad color de la intersección.</param>
    /// <param name="intersectionTexture">Propiedad textura de la intersección.</param>
    /// <param name="intersectionFalloff">Propiedad declive de la intersección.</param>
    /// <param name="intersectionDistance">Propiedad distnacia de la intersección.</param>
    /// <param name="intersectionFill">Propiedad relleno de la intersección.</param>
    /// <param name="intersectionHardEdge">Propiedad borde afilado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildMeshIntersectionOld(MaterialProperty meshIntersection, MaterialProperty intersectionTexture, MaterialProperty intersectionColor, MaterialProperty intersectionFalloff, MaterialProperty intersectionDistance, MaterialProperty intersectionFill, MaterialProperty intersectionHardEdge, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Mesh Intersection", "Mesh intersection detection.", meshIntersection, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Intersection Color " + CheckRenderMode(renderMode), "Intersection color.", intersectionColor, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Intersection Texture " + CheckRenderMode(renderMode), "Texture applied on the intersection area.", intersectionTexture, materialEditor, true, meshIntersection.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Intersection Falloff", "Fallof value.", intersectionFalloff, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Intersection Distance", "Intersection lenght.", intersectionDistance, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Intersection Fill", "If enabled the color fill is applied.", intersectionFill, toggleLock: meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Intersection Hard Edge", "If enabled creates a hard edge between the intersection area colr and the source color.", intersectionHardEdge, toggleLock: meshIntersection.floatValue);
    }


    /// \english
    /// <summary>
    /// Mesh intersection function builder.
    /// </summary>
    /// <param name="meshIntersection">Mesh intersection property.</param>
    /// <param name="intersectionTexture">Intersection texture property.</param>
    /// <param name="intersectionTextureScroll">Intersection texture scroll property.</param>
    /// <param name="intersectionTextureSmooth">Intersection texture smooth property.</param>
    /// <param name="intersectionTextureReverse">Intersection texture reverse property.</param>
    /// <param name="intersectionColor">Intersection color property.</param>
    /// <param name="intersectionFalloff">Intersection falloff property.</param>
    /// <param name="intersectionDistance">Intersection distance property.</param>
    /// <param name="intersectionLevel">Intersection level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la intersección de la mesh.
    /// </summary>
    /// <param name="meshIntersection">Propiedad intersección de la mesh.</param>
    /// <param name="intersectionTexture">Propiedad textura de la intersección.</param>
    /// <param name="intersectionTextureScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="intersectionTextureSmooth">Propiedad del suavizado de la textura de la intersección.</param>
    /// <param name="intersectionTextureReverse">Propiedad de la inversión de la textura de la intersección.</param>
    /// <param name="intersectionColor">Propiedad color de la intersección.</param>
    /// <param name="intersectionFalloff">Propiedad declive de la intersección.</param>
    /// <param name="intersectionDistance">Propiedad distnacia de la intersección.</param>
    /// <param name="intersectionLevel">Propiedad del nivel de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildMeshIntersection(MaterialProperty meshIntersection, MaterialProperty intersectionTexture, MaterialProperty intersectionTextureScroll, MaterialProperty intersectionTextureSmooth, MaterialProperty intersectionTextureReverse, MaterialProperty intersectionColor, MaterialProperty intersectionFalloff, MaterialProperty intersectionDistance, MaterialProperty intersectionLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Mesh Intersection", "Mesh intersection detection.", meshIntersection, true);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Intersection Texture Scroll", "Scroll the intersection texture.", intersectionTextureScroll, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Intersection Texture Smooth", "Smooth of the edge of the intersection texture.", intersectionTextureSmooth, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Intersection Texture Reverse", "If enabled, reverse the intersection texture color.", intersectionTextureReverse, toggleLock: meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Intersection Color (RGBA)", "Intersection color.", intersectionColor, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Intersection Texture (RGB)", "Texture applied on the intersection area.", intersectionTexture, materialEditor, true, meshIntersection.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Intersection Falloff", "Fallof value.", intersectionFalloff, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Intersection Distance", "Intersection lenght.", intersectionDistance, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Intersection Level", "Level of the color based on the source color.", intersectionLevel, meshIntersection.floatValue);
    }


    /// \english
    /// <summary>
    /// Foam function builder.
    /// </summary>
    /// <param name="meshIntersection">Mesh intersection property.</param>
    /// <param name="intersectionTexture">Intersection texture property.</param>
    /// <param name="intersectionTextureScroll">Intersection texture scroll property.</param>
    /// <param name="intersectionTextureSmooth">Intersection texture smooth property.</param>
    /// <param name="intersectionTextureReverse">Intersection texture reverse property.</param>
    /// <param name="intersectionColor">Intersection color property.</param>
    /// <param name="intersectionFalloff">Intersection falloff property.</param>
    /// <param name="intersectionDistance">Intersection distance property.</param>
    /// <param name="intersectionLevel">Intersection level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la espuma.
    /// </summary>
    /// <param name="meshIntersection">Propiedad intersección de la mesh.</param>
    /// <param name="intersectionTexture">Propiedad textura de la intersección.</param>
    /// <param name="intersectionTextureScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="intersectionTextureSmooth">Propiedad del suavizado de la textura de la intersección.</param>
    /// <param name="intersectionTextureReverse">Propiedad de la inversión de la textura de la intersección.</param>
    /// <param name="intersectionColor">Propiedad color de la intersección.</param>
    /// <param name="intersectionFalloff">Propiedad declive de la intersección.</param>
    /// <param name="intersectionDistance">Propiedad distnacia de la intersección.</param>
    /// <param name="intersectionLevel">Propiedad del nivel de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildFoam(MaterialProperty meshIntersection, MaterialProperty intersectionTexture, MaterialProperty intersectionTextureScroll, MaterialProperty intersectionTextureSmooth, MaterialProperty intersectionTextureReverse, MaterialProperty intersectionColor, MaterialProperty intersectionFalloff, MaterialProperty intersectionDistance, MaterialProperty intersectionLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Foam", "Foam on mesh intersection.", meshIntersection, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Foam Texture (RGB)", "Texture applied on the foam area.", intersectionTexture, materialEditor, true, meshIntersection.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Foam Texture Scroll", "Scroll the foam texture.", intersectionTextureScroll, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Foam Texture Smooth", "Smooth of the edge of the foam texture.", intersectionTextureSmooth, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Foam Texture Reverse", "If enabled, reverse the foam texture color.", intersectionTextureReverse, toggleLock: meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Foam Color (RGBA)", "Foam color.", intersectionColor, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Foam Falloff", "Fallof value.", intersectionFalloff, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Foam Distance", "Foam lenght.", intersectionDistance, meshIntersection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Foam Level", "Level of the color based on the source color.", intersectionLevel, meshIntersection.floatValue);
    }

    /// \english
    /// <summary>
    /// Foam function builder with double map header group.
    /// </summary>
    /// <param name="meshIntersection">Mesh intersection property.</param>
    /// <param name="intersectionTexture">Intersection texture property.</param>
    /// <param name="intersectionTextureScroll">Intersection texture scroll property.</param>
    /// <param name="intersectionTextureLevel">Intersection texture level property.</param>
    /// <param name="intersectionTextureSmooth">Intersection texture smooth property.</param>
    /// <param name="intersectionTextureReverse">Intersection texture reverse property.</param>
    /// <param name="intersectionTextureBlending">Intersection texture blending property.</param>
    /// <param name="intersectionColor">Intersection color property.</param>
    /// <param name="intersectionFalloff">Intersection falloff property.</param>
    /// <param name="intersectionDistance">Intersection distance property.</param>
    /// <param name="intersectionLevel">Intersection level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la espuma con doble mapa grupo.
    /// </summary>
    /// <param name="meshIntersection">Propiedad intersección de la mesh.</param>
    /// <param name="intersectionTexture">Propiedad textura de la intersección.</param>
    /// <param name="intersectionTextureScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="intersectionTextureLevel">Propiedad del nivel de la textura de la intersección.</param>
    /// <param name="intersectionTextureSmooth">Propiedad del suavizado de la textura de la intersección.</param>
    /// <param name="intersectionTextureReverse">Propiedad de la inversión de la textura de la intersección.</param>
    /// <param name="intersectionTextureBlending">Propiedad de la fusión de las texturas de la intersección.</param>
    /// <param name="intersectionColor">Propiedad color de la intersección.</param>
    /// <param name="intersectionFalloff">Propiedad declive de la intersección.</param>
    /// <param name="intersectionDistance">Propiedad distancia de la intersección.</param>
    /// <param name="intersectionLevel">Propiedad del nivel de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildFoamDoubleMap(MaterialProperty meshIntersection, MaterialProperty intersectionTexture, MaterialProperty intersectionTextureScroll, MaterialProperty intersectionTextureLevel, MaterialProperty intersectionTextureSmooth, MaterialProperty intersectionTextureReverse, MaterialProperty intersectionTextureBlending, MaterialProperty intersectionColor, MaterialProperty intersectionFalloff, MaterialProperty intersectionDistance, MaterialProperty intersectionLevel, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName, bool compactMode = false, float renderMode = 0)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Foam", "Foam on mesh intersection.", meshIntersection, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("Foam Texture (RGB)", "Texture applied on the foam area.", intersectionTexture, materialEditor, true, meshIntersection.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildVector4("Foam Texture Scroll", "Scroll of the foam texture.", intersectionTextureScroll, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildVector2Positive("Foam Texture Level", "Level of the foam texture.", intersectionTextureLevel, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Foam Texture Smooth", "Smooth of the edge of the foam texture.", intersectionTextureSmooth, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Foam Texture Reverse", "If enabled, reverse the foam texture color.", intersectionTextureReverse, toggleLock: meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Foam Texture Blending", "If enabled, blend the foam texture color.", intersectionTextureBlending, toggleLock: meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildColor("Foam Color (RGBA)", "Foam color.", intersectionColor, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Foam Falloff", "Fallof value.", intersectionFalloff, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Foam Distance", "Foam lenght.", intersectionDistance, meshIntersection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Foam Level", "Level of the color based on the source color.", intersectionLevel, meshIntersection.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Water depth simple function builder.
    /// </summary>
    /// <param name="waterDepth">Water depth property.</param>
    /// <param name="shallowColor">Shallow color property.</param>
    /// <param name="shallowLight">Shallow light property.</param>
    /// <param name="useBaseColor">Use base color property.</param>
    /// <param name="waterDepthFalloff">Water depth falloff property.</param>
    /// <param name="waterDepthDistance">Water depth distance property.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la intersección de la mesh.
    /// </summary>
    /// <param name="waterDepth">Propiedad de profundidad de agua.</param>
    /// <param name="shallowColor">Propiedad del color superficial.</param>
    /// <param name="shallowLight">Propiedad de la luz superficial.</param>
    /// <param name="useBaseColor">Propiedad de uso del color base como el color superficial.</param>
    /// <param name="waterDepthFalloff">Propiedad declive de la profundidad del agua.</param>
    /// <param name="waterDepthDistance">Propiedad distancia de la profundidad del agua.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildWaterDepthSimple(MaterialProperty waterDepth, MaterialProperty shallowColor, MaterialProperty shallowLight, MaterialProperty useBaseColor, MaterialProperty waterDepthFalloff, MaterialProperty waterDepthDistance, float renderMode = 0)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Water Depth", "Water depth effect.", waterDepth, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Shallow Color " + CheckRenderMode(renderMode), "Color of the shallow part of the water.", shallowColor, waterDepth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shallow Light", "Brightness of the shallow part of the water.", shallowLight, waterDepth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Base Color", "If enabled, use the base color as a shallow color.", useBaseColor, toggleLock: waterDepth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Water Depth Falloff", "Fallof value of the depth.", waterDepthFalloff, waterDepth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Water Depth Distance", "Lenght value of the depth.", waterDepthDistance, waterDepth.floatValue);
    }

    /// \english
    /// <summary>
    /// Water depth simple function builder header group.
    /// </summary>
    /// <param name="waterDepth">Water depth property.</param>
    /// <param name="shallowColor">Shallow color property.</param>
    /// <param name="shallowLight">Shallow light property.</param>
    /// <param name="useBaseColor">Use base color property.</param>
    /// <param name="waterDepthFalloff">Water depth falloff property.</param>
    /// <param name="waterDepthDistance">Water depth distance property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la intersección de la mesh grupo.
    /// </summary>
    /// <param name="waterDepth">Propiedad de profundidad de agua.</param>
    /// <param name="shallowColor">Propiedad del color superficial.</param>
    /// <param name="shallowLight">Propiedad de la luz superficial.</param>
    /// <param name="useBaseColor">Propiedad de uso del color base como el color superficial.</param>
    /// <param name="waterDepthFalloff">Propiedad declive de la profundidad del agua.</param>
    /// <param name="waterDepthDistance">Propiedad distancia de la profundidad del agua.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildWaterDepthSimple(MaterialProperty waterDepth, MaterialProperty shallowColor, MaterialProperty shallowLight, MaterialProperty useBaseColor, MaterialProperty waterDepthFalloff, MaterialProperty waterDepthDistance, bool isUnfold, string playerPrefKeyName, float renderMode = 0)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Water Depth", "Water depth effect.", waterDepth, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildColor("Shallow Color " + CheckRenderMode(renderMode), "Color of the shallow part of the water.", shallowColor, waterDepth.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Shallow Light", "Brightness of the shallow part of the water.", shallowLight, waterDepth.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Base Color", "If enabled, use the base color as a shallow color.", useBaseColor, toggleLock: waterDepth.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Water Depth Falloff", "Fallof value of the depth.", waterDepthFalloff, waterDepth.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Water Depth Distance", "Lenght value of the depth.", waterDepthDistance, waterDepth.floatValue);
        }
    }


    /// \english
    /// <summary>
    /// Wave movement function builder.
    /// </summary>
    /// <param name="waveMovement">Wave movement depth property.</param>
    /// <param name="waveFrequency">Wave frecuency property.</param>
    /// <param name="waveAmplitude">Wave amplitude property.</param>
    /// <param name="waveDirection">Wave direction property.</param>
    /// <param name="waveSpeed">Wave speed property.</param>
    /// <param name="waveNoiseMap">Wave noise map property.</param>
    /// <param name="waveNoiseLevel">Wave noise level property.</param>
    /// <param name="waveNoiseScroll">Wave noise scroll property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función del movimiento de la onda.
    /// </summary>
    /// <param name="waveMovement">Propiedad del movimiento de la onda.</param>
    /// <param name="waveFrequency">Propiedad de la frecuencia del movimiento de la onda.</param>
    /// <param name="waveAmplitude">Propiedad de la apmplitud del movimiento de la onda.</param>
    /// <param name="waveDirection">Propiedad de la dirección del movimiento de la onda.</param>
    /// <param name="waveSpeed">Propiedad de la velocidad del movimiento de la onda.</param>
    /// <param name="waveNoiseMap">Propiedad del mapa de ruido de la profundidad del agua.</param>
    /// <param name="waveNoiseLevel">Propiedad del nivel del mapa de ruido de la profundidad del agua.</param>
    /// <param name="waveNoiseScroll">Propiedad de la velocidad de desplazamiento del mapa de ruido de la profundidad del agua.</param>
    /// \endspanish
    public static void BuildWaveMovement(MaterialProperty waveMovement, MaterialProperty waveFrequency, MaterialProperty waveAmplitude, MaterialProperty waveDirection, MaterialProperty waveSpeed, MaterialProperty waveNoiseMap, MaterialProperty waveNoiseLevel, MaterialProperty waveNoiseScroll, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Wave Movement", "Sine wave vertex movement", waveMovement, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Frequency", "Frecuency of the waves.", waveFrequency, waveMovement.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Amplitude", "Amplitude of the waves.", waveAmplitude, waveMovement.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Wave Direction", "Direction of the wave movement.", waveDirection, waveMovement.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Speed", "Speed of the wave movement.", waveSpeed, waveMovement.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Wave Noise Map (R)", "Map for an additional height displacement of the vertex.", waveNoiseMap, materialEditor, true, waveMovement.floatValue, false);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Wave Noise Level", "Level of the additional height displacement.", waveNoiseLevel, waveMovement.floatValue * (waveNoiseMap.textureValue ? 1 : 0));
        CGFMaterialEditorUtilitiesClass.BuildVector2("Wave Noise Scroll", "Scroll speed of the additional height displacement.", waveNoiseScroll, waveMovement.floatValue * (waveNoiseMap.textureValue ? 1 : 0));
    }


    /// \english
    /// <summary>
    /// Wave movement function builder header group.
    /// </summary>
    /// <param name="waveMovement">Wave movement depth property.</param>
    /// <param name="waveFrequency">Wave frecuency property.</param>
    /// <param name="waveAmplitude">Wave amplitude property.</param>
    /// <param name="waveDirection">Wave direction property.</param>
    /// <param name="waveSpeed">Wave speed property.</param>
    /// <param name="waveNoiseMap">Wave noise map property.</param>
    /// <param name="waveNoiseLevel">Wave noise level property.</param>
    /// <param name="waveNoiseScroll">Wave noise scroll property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función del movimiento de la onda grupo.
    /// </summary>
    /// <param name="waveMovement">Propiedad del movimiento de la onda.</param>
    /// <param name="waveFrequency">Propiedad de la frecuencia del movimiento de la onda.</param>
    /// <param name="waveAmplitude">Propiedad de la apmplitud del movimiento de la onda.</param>
    /// <param name="waveDirection">Propiedad de la dirección del movimiento de la onda.</param>
    /// <param name="waveSpeed">Propiedad de la velocidad del movimiento de la onda.</param>
    /// <param name="waveNoiseMap">Propiedad del mapa de ruido de la profundidad del agua.</param>
    /// <param name="waveNoiseLevel">Propiedad del nivel del mapa de ruido de la profundidad del agua.</param>
    /// <param name="waveNoiseScroll">Propiedad de la velocidad de desplazamiento del mapa de ruido de la profundidad del agua.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildWaveMovement(MaterialProperty waveMovement, MaterialProperty waveFrequency, MaterialProperty waveAmplitude, MaterialProperty waveDirection, MaterialProperty waveSpeed, MaterialProperty waveNoiseMap, MaterialProperty waveNoiseLevel, MaterialProperty waveNoiseScroll, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Wave Movement", "Sine wave vertex movement", waveMovement, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Frequency", "Frecuency of the waves.", waveFrequency, waveMovement.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Amplitude", "Amplitude of the waves.", waveAmplitude, waveMovement.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Wave Direction", "Direction of the wave movement.", waveDirection, waveMovement.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Wave Speed", "Speed of the wave movement.", waveSpeed, waveMovement.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Wave Noise Map (R)", "Map for an additional height displacement of the vertex.", waveNoiseMap, materialEditor, true, waveMovement.floatValue, false);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Wave Noise Level", "Level of the additional height displacement.", waveNoiseLevel, waveMovement.floatValue * (waveNoiseMap.textureValue ? 1 : 0));
            CGFMaterialEditorUtilitiesClass.BuildVector2("Wave Noise Scroll", "Scroll speed of the additional height displacement.", waveNoiseScroll, waveMovement.floatValue * (waveNoiseMap.textureValue ? 1 : 0));
        }
    }


    /// \english
    /// <summary>
    /// Light function builder header group.
    /// </summary>
    /// <param name="light">Light property.</param>
    /// <param name="directionalLight">Directional light property.</param>
    /// <param name="ambient">Ambient property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de ls función de luz grupo.
    /// </summary>
    /// <param name="light">Propiedad luz.</param>
    /// <param name="directionalLight">Propiedad luz direccional.</param>
    /// <param name="ambient">Propiedad ambiente.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildLight(MaterialProperty light, MaterialProperty directionalLight, MaterialProperty ambient, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Light", "Light and Ambient light.", light, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Directional Light", "If enabled the color of the main directional light affect to the source mesh.", directionalLight, toggleLock: light.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Ambient Light", "If enabledd ambient light affect to the source mesh.", ambient, toggleLock: light.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Light function builder.
    /// </summary>
    /// <param name="light">Light property.</param>
    /// <param name="directionalLight">Directional light property.</param>
    /// <param name="ambient">Ambient property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de ls función de luz.
    /// </summary>
    /// <param name="light">Propiedad luz.</param>
    /// <param name="directionalLight">Propiedad luz direccional.</param>
    /// <param name="ambient">Propiedad ambiente.</param>
    /// \endspanish
    public static void BuildLight(MaterialProperty light, MaterialProperty directionalLight, MaterialProperty ambient)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Light", "Light and Ambient light.", light, true);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Directional Light", "If enabled the color of the main directional light affect to the source mesh.", directionalLight, toggleLock: light.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Ambient Light", "If enabledd ambient light affect to the source mesh.", ambient, toggleLock: light.floatValue);
    }

    /// \english
    /// <summary>
    /// Simulated light function builder.
    /// </summary>
    /// <param name="simulatedLight">Simulated light property.</param>
    /// <param name="simulatedLightRampTexture">Simulated light ramp texture property.</param>
    /// <param name="simulatedLightLevel">Simulated light level property.</param>
    /// <param name="simulatedLightPosition">Simulated light position property.</param>
    /// <param name="simulatedLightDistance">Simulated light distance property.</param>
    /// <param name="gradientRamp">Gradient ramp property.</param>
    /// <param name="centerColor">Center color property.</param>
    /// <param name="useExternalColor">Use external color property.</param>
    /// <param name="externalColor">External color property.</param>
    /// <param name="additiveSimulatedLight">Additive simulated light property.</param>
    /// <param name="additiveSimulatedLightLevel">Additive simulated light level property.</param>
    /// <param name="posterize">Posterize property.</param>
    /// <param name="steps">Steps property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de luz simulada.
    /// </summary>
    /// <param name="simulatedLight">Propiedad luz simulada.</param>
    /// <param name="simulatedLightRampTexture">Propiedad textura de la rampa de la luz simulada.</param>
    /// <param name="simulatedLightLevel">Propiedad nivel de la luz simulada.</param>
    /// <param name="simulatedLightPosition">Propiedad posición de la luz simulada.</param>
    /// <param name="simulatedLightDistance">Propiedad distancia de la luz simulada.</param>
    /// <param name="gradientRamp">Propiedad rampa de degradado.</param>
    /// <param name="centerColor">Propiedad color central.</param>
    /// <param name="useExternalColor">Propiedad usar color externo.</param>
    /// <param name="externalColor">Propiedad color externo.</param>
    /// <param name="additiveSimulatedLight">Propiedad luz simulada aditiva.</param>
    /// <param name="additiveSimulatedLightLevel">Propiedad nivel de la luz simulada aditiva.</param>
    /// <param name="posterize">Propiedad posterizado.</param>
    /// <param name="steps">Propiedad pasos.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildSimulatedLight(MaterialProperty simulatedLight, MaterialProperty simulatedLightRampTexture, MaterialProperty simulatedLightLevel, MaterialProperty simulatedLightPosition, MaterialProperty simulatedLightDistance, MaterialProperty gradientRamp, MaterialProperty centerColor, MaterialProperty useExternalColor, MaterialProperty externalColor, MaterialProperty additiveSimulatedLight, MaterialProperty additiveSimulatedLightLevel, MaterialProperty posterize, MaterialProperty steps, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Simulated Light", "Simulated Light.", simulatedLight, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Simulated Light Ramp Texture (RGB)", "Color ramp of the simulated light based on a texture. The top part of the texture is the center of the simulated light and the bottom part is the external part of the simulated light.", simulatedLightRampTexture, materialEditor, true, simulatedLight.floatValue - gradientRamp.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Simulated Light Level", "Level of simulated light in relation the source color.", simulatedLightLevel, simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Simulated Light Position", "World position of the simulated light.", simulatedLightPosition, simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Simulated Light Distance", "Simulated light circunference diameter.", simulatedLightDistance, simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Ramp", "If enabledd uses a gradient ramp between two colors instead a ramp texture.", gradientRamp, toggleLock: simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Center Color (RGB)", "Color of the center of the simulated light if gradient ramp is enabled.", centerColor, simulatedLight.floatValue * gradientRamp.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use External Color", "If enabledd uses a color for the external part of the light instead de source color.", useExternalColor, toggleLock: simulatedLight.floatValue * gradientRamp.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("External Color (RGB)", "Color of the expernal part of the simulated light if gradient ramp is enabled.", externalColor, simulatedLight.floatValue * gradientRamp.floatValue * useExternalColor.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Additive Simulated Light", "If enabledd adds the simulated light color to the source color.", additiveSimulatedLight, toggleLock: simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Additive Simulated Light Level", "Level of simulated light addition in relation the source color.", additiveSimulatedLightLevel, additiveSimulatedLight.floatValue * simulatedLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Posterize", "If enabledd converts the ramp texture or the gradient ramp to multiple regions of fewer tones.", posterize, toggleLock: simulatedLight.floatValue * gradientRamp.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Steps", "Color steps of the posterization.", steps, simulatedLight.floatValue * posterize.floatValue * gradientRamp.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Lightmap function builder.
    /// </summary>
    /// <param name="lightmap">Lightmap property.</param>
    /// <param name="lightmapColor">Lightmap color property.</param>
    /// <param name="lightmapLevel">Lightmap level property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="multiplyLightmap">Multiply lightmap property.</param>
    /// <param name="desaturateLightColor">Desaturate light color property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de lightmap.
    /// </summary>
    /// <param name="lightmap">Propiedad lightmap.</param>
    /// <param name="lightmapColor">Propiedad color del lightmap.</param>
    /// <param name="lightmapLevel">Propiedad nivel del lightmap.</param>
    /// <param name="shadowLevel">Propiedad nivel de la sombra.</param>
    /// <param name="multiplyLightmap">Propiedad multiplicar lightmap.</param>
    /// <param name="desaturateLightColor">Propiedad desaturar color de la luz.</param>
    /// \endspanish
    public static void BuildLightmap(MaterialProperty lightmap, MaterialProperty lightmapColor, MaterialProperty lightmapLevel, MaterialProperty shadowLevel, MaterialProperty multiplyLightmap, MaterialProperty desaturateLightColor)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Lightmap", "Lightmap.", lightmap, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Lightmap Color (RGB)", "Color of the lightmap.", lightmapColor, lightmap.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Lightmap Level", "Level of light of the lightmap in relation the source color.", lightmapLevel, lightmap.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow level", "Level of shadow of the lightmap in relation the source color.", shadowLevel, lightmap.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Multiyply Lightmap", "If enabledd the lightmap color is multiplied by the source color.", multiplyLightmap, toggleLock: lightmap.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Desaturate Light Color", "If enabledd color of the light of the lightmap is desaturated to grey scale.", desaturateLightColor, toggleLock: lightmap.floatValue);
    }

    /// \english
    /// <summary>
    /// Ambient Occlusion from lightmap function builder.
    /// </summary>
    /// <param name="ambientOcclusion">Ambient Occlusion property.</param>
    /// <param name="ambientOcclusionColor">Ambient Occlusion color property.</param>
    /// <param name="ambientOcclusionLevel">Ambient Occlusion level property.</param>
    /// <param name="shadowIntensity">Shadow level property.</param>
    /// <param name="multiplyAmbientOcclusion">Multiply ambientOcclusion property.</param>
    /// <param name="desaturateAmbientOcclusionColor">Desaturate light color property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de ambient occlusion a partir de un lightmap.
    /// </summary>
    /// <param name="ambientOcclusion">Propiedad ambient occlusion.</param>
    /// <param name="ambientOcclusionColor">Propiedad color del ambient occlusion.</param>
    /// <param name="ambientOcclusionLevel">Propiedad nivel del ambien occlusion.</param>
    /// <param name="shadowIntensity">Propiedad nivel de la sombra.</param>
    /// <param name="multiplyAmbientOcclusion">Propiedad multiplicar ambient occlusion.</param>
    /// <param name="desaturateAmbientOcclusionColor">Propiedad desaturar color de la luz.</param>
    /// \endspanish
    public static void BuildLightmapAmbientOcclusion(MaterialProperty ambientOcclusion, MaterialProperty ambientOcclusionColor, MaterialProperty ambientOcclusionLevel, MaterialProperty shadowIntensity, MaterialProperty multiplyAmbientOcclusion, MaterialProperty desaturateAmbientOcclusionColor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Ambient Occlusion", "Ambient Occlusion.", ambientOcclusion, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Ambient Occlusion Color (RGB)", "Color of the ambient occlusion.", ambientOcclusionColor, ambientOcclusion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Ambient Occlusion Level", "Level of light of the ambient occlusion in relation the source color.", ambientOcclusionLevel, ambientOcclusion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow level", "Level of shadow of the ambient occlusion in relation the source color.", shadowIntensity, ambientOcclusion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Multiyply Ambient Occlusion", "If enabledd the ambient occlusion color is multiplied by the source color.", multiplyAmbientOcclusion, toggleLock: ambientOcclusion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Desaturate Light Color", "If enabledd color of the light of the ambient occlusion is desaturated to grey scale.", desaturateAmbientOcclusionColor, toggleLock: ambientOcclusion.floatValue);
    }


    /// \english
    /// <summary>
    /// Ambient Occlusion from lightmap function builder header group.
    /// </summary>
    /// <param name="ambientOcclusion">Ambient Occlusion property.</param>
    /// <param name="ambientOcclusionColor">Ambient Occlusion color property.</param>
    /// <param name="ambientOcclusionLevel">Ambient Occlusion level property.</param>
    /// <param name="shadowIntensity">Shadow level property.</param>
    /// <param name="multiplyAmbientOcclusion">Multiply ambientOcclusion property.</param>
    /// <param name="desaturateAmbientOcclusionColor">Desaturate light color property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de ambient occlusion a partir de un lightmap grupo.
    /// </summary>
    /// <param name="ambientOcclusion">Propiedad ambient occlusion.</param>
    /// <param name="ambientOcclusionColor">Propiedad color del ambient occlusion.</param>
    /// <param name="ambientOcclusionLevel">Propiedad nivel del ambien occlusion.</param>
    /// <param name="shadowIntensity">Propiedad nivel de la sombra.</param>
    /// <param name="multiplyAmbientOcclusion">Propiedad multiplicar ambient occlusion.</param>
    /// <param name="desaturateAmbientOcclusionColor">Propiedad desaturar color de la luz.</param>
    /// <param name="showGizmo">Muestra el gizmo.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildLightmapAmbientOcclusion(MaterialProperty ambientOcclusion, MaterialProperty ambientOcclusionColor, MaterialProperty ambientOcclusionLevel, MaterialProperty shadowIntensity, MaterialProperty multiplyAmbientOcclusion, MaterialProperty desaturateAmbientOcclusionColor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Ambient Occlusion", "Ambient Occlusion.", ambientOcclusion, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildColor("Ambient Occlusion Color (RGB)", "Color of the ambient occlusion.", ambientOcclusionColor, ambientOcclusion.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Ambient Occlusion Level", "Level of light of the ambient occlusion in relation the source color.", ambientOcclusionLevel, ambientOcclusion.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow level", "Level of shadow of the ambient occlusion in relation the source color.", shadowIntensity, ambientOcclusion.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Multiyply Ambient Occlusion", "If enabledd the ambient occlusion color is multiplied by the source color.", multiplyAmbientOcclusion, toggleLock: ambientOcclusion.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Desaturate Light Color", "If enabledd color of the light of the ambient occlusion is desaturated to grey scale.", desaturateAmbientOcclusionColor, toggleLock: ambientOcclusion.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Sprite outline function builder.
    /// </summary>
    /// <param name="spriteOutline">Sprite outline property.</param>
    /// <param name="outlineColor">Outline color property.</param>
    /// <param name="outlineWidth">Outline width property.</param>
    /// <param name="outlineSharp">Outline sharp property.</param>
    /// <param name="innerSharp">Inner sharp property.</param>
    /// <param name="disableTopLeftOutline">Disable top left outline property.</param>
    /// <param name="disableTopOutline">Disable top outline property.</param>
    /// <param name="disableTopRightOutline">Disable top right outline property.</param>
    /// <param name="disableRightOutline">Disable right outline property.</param>
    /// <param name="disableBottomRightOutline">Disable bottom right outline property.</param>
    /// <param name="disableBottomOutline">Disable bottom outline property.</param>
    /// <param name="disableBottomLeftOutline">Disable bottom left outline property.</param>
    /// <param name="disableLeftOutline">Disable left outline property.</param>
    /// <param name="topLeftDistance">Top left distance property.</param>
    /// <param name="topDistance">Top distance property.</param>
    /// <param name="topRightDistance">Top right distance property.</param>
    /// <param name="rightDistance">Right distance property.</param>
    /// <param name="bottomRightDistance">Bottom right distance property.</param>
    /// <param name="bottomDistance">Bottom distance property.</param>
    /// <param name="bottomLeftDistance">Bottom left distance property.</param>
    /// <param name="leftDistance">Left ditance property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de borde de sprite.
    /// </summary>
    /// <param name="spriteOutline">Propiedad borde de sprite.</param>
    /// <param name="outlineColor">Propiedad color dle borde.</param>
    /// <param name="outlineWidth">Propiedad ancho del borde.</param>
    /// <param name="outlineSharp">Propiedad borde afilado.</param>
    /// <param name="innerSharp">Propiedad interior afilado.</param>
    /// <param name="disableTopLeftOutline">Propiedad desactivar borde superior izquierdo.</param>
    /// <param name="disableTopOutline">Propiedad desactivar borde superior.</param>
    /// <param name="disableTopRightOutline">Propiedad desactivar borde superior derecho.</param>
    /// <param name="disableRightOutline">Propiedad desactivar borde derecho.</param>
    /// <param name="disableBottomRightOutline">Propiedad desactivar borde inferior derecho.</param>
    /// <param name="disableBottomOutline">Propiedad desactivar borde inferior.</param>
    /// <param name="disableBottomLeftOutline">Propiedad desactivar borde inferior izquierdo.</param>
    /// <param name="disableLeftOutline">Propiedad desactivar borde izquierdo.</param>
    /// <param name="topLeftDistance">Propiedad distancia superior izquierda.</param>
    /// <param name="topDistance">Propiedad distancia superior.</param>
    /// <param name="topRightDistance">Propiedad distancia superior derecha.</param>
    /// <param name="rightDistance">Propiedad distancia derecha.</param>
    /// <param name="bottomRightDistance">Propiedad distancia inferior derecha.</param>
    /// <param name="bottomDistance">Propiedad distancia inferior.</param>
    /// <param name="bottomLeftDistance">Propiedad distancia inferior izquierda.</param>
    /// <param name="leftDistance">Propiedad distancia izquierda.</param>
    /// \endspanish
    public static void BuildSpriteOutline(MaterialProperty spriteOutline, MaterialProperty outlineColor, MaterialProperty outlineWidth, MaterialProperty outlineSharp, MaterialProperty innerSharp, MaterialProperty disableTopLeftOutline, MaterialProperty disableTopOutline, MaterialProperty disableTopRightOutline, MaterialProperty disableRightOutline, MaterialProperty disableBottomRightOutline, MaterialProperty disableBottomOutline, MaterialProperty disableBottomLeftOutline, MaterialProperty disableLeftOutline, MaterialProperty topLeftDistance, MaterialProperty topDistance, MaterialProperty topRightDistance, MaterialProperty rightDistance, MaterialProperty bottomRightDistance, MaterialProperty bottomDistance, MaterialProperty bottomLeftDistance, MaterialProperty leftDistance)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Sprite Outline", "Outline for sprites.", spriteOutline, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Outline Color (RGBA)", "Color of the outline.", outlineColor, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Outline Width", "Width of the outline.", outlineWidth, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Outline Sharp", "Hard edge for the external part of the outline.", outlineSharp, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Inner Sharp", "Hard edge for the internal part of the outline.", innerSharp, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Top Left Outline", "If enabledd disables the top left outline.", disableTopLeftOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Top Outline", "If enabledd disables the top outline.", disableTopOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Right Outline", "If enabledd disables the right outline.", disableTopRightOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Right Outline", "If enabledd disables the bottom right outline.", disableRightOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Right Outline", "If enabledd disables the right outline.", disableBottomRightOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Outline", "If enabledd disables the bottom outline.", disableBottomOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Left Outline", "If enabledd disables the bottom left outline.", disableBottomLeftOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Left Outline", "If enabledd disables the left outline.", disableLeftOutline, toggleLock: spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Left Distance", "Distance of top left outline.", topLeftDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Distance", "Distance of top outline.", topDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Right Distance", "Distance of top right outline.", topRightDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Right Distance", "Distance of right outline.", rightDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Right Distance", "Distance of bottom right outline.", bottomRightDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Distance", "Distance of bottom outline.", bottomDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Left Distance", "Distance of bottom left outline.", bottomLeftDistance, spriteOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Left Distance", "Distance of left outline.", leftDistance, spriteOutline.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Sprite pixel outline function builder.
    /// </summary>
    /// <param name="spritePixelOutline">Sprite pixel outline property.</param>
    /// <param name="pixelOutlineColor">Pixel outline color property.</param>
    /// <param name="pixelOutlineWidth">Pixel outline width property.</param>
    /// <param name="pixelOutlineReverse">Pixel outline reverse property.</param>
    /// <param name="outerPixelOutline">Outer pixel outline property.</param>
    /// <param name="discardTransparentPixels">Discard transparent pixels property.</param>
    /// <param name="disableTopLeftPixelOutline">Disable top left pixel outline property.</param>
    /// <param name="disableTopPixelOutline">Disable top pixel outline property.</param>
    /// <param name="disableTopRightPixelOutline">Disable top right pixel outline property.</param>
    /// <param name="disableRightPixelOutline">Disable right pixel outline property.</param>
    /// <param name="disableBottomRightPixelOutline">Disable bottom right pixel outline property.</param>
    /// <param name="disableBottomPixelOutline">Disable bottom pixel outline property.</param>
    /// <param name="disableBottomLeftPixelOutline">Disable bottom left pixel outline property.</param>
    /// <param name="disableLeftPixelOutline">Disable left pixel outline property.</param>
    /// <param name="disablePixelTopLeftDistance">Disable top left pixel outline property.</param>
    /// <param name="pixelOutlineTopDistance">Pixel outline top distance property.</param>
    /// <param name="pixelOutlineTopRightDistance">Pixel outline top right distance property.</param>
    /// <param name="pixelOutlineRightDistance">Pixel outline right distance property.</param>
    /// <param name="pixelOutlineBottomRightDistance">Pixel outline bottom right distance property.</param>
    /// <param name="pixelOutlineBottomDistance">Pixel outline bottom distance property.</param>
    /// <param name="pixelOutlineBottomLeftDistance">Pixel outline bottom left distance property.</param>
    /// <param name="pixelOutlineLeftDistance">Pixel outline left distance property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de borde pixel de sprite.
    /// </summary>
    /// <param name="spritePixelOutline">Propiedad borde pixel de sprite.</param>
    /// <param name="pixelOutlineColor">Propiedad color del borde pixel.</param>
    /// <param name="pixelOutlineWidth">Propiedad ancho del borde pixel.</param>
    /// <param name="pixelOutlineReverse">Propiedad borde invertido pixel.</param>
    /// <param name="outerPixelOutline">Propiedad borde externo pixel.</param>
    /// <param name="discardTransparentPixels">Propiedad descartar píxeles transparentes</param>
    /// <param name="disableTopLeftPixelOutline">Propiedad desactivar borde pixel superior izquierdo.</param>
    /// <param name="disableTopPixelOutline">Propiedad desactivar borde pixel superior.</param>
    /// <param name="disableTopRightPixelOutline">Propiedad desactivar borde pixel superior derecho.</param>
    /// <param name="disableRightPixelOutline">Propiedad desactivar borde pixel derecho.</param>
    /// <param name="disableBottomRightPixelOutline">Propiedad desactivar borde pixel inferior derecho.</param>
    /// <param name="disableBottomPixelOutline">Propiedad desactivar borde pixel inferior.</param>
    /// <param name="disableBottomLeftPixelOutline">Propiedad desactivar borde pixel inferior izquierdo.</param>
    /// <param name="disableLeftPixelOutline">Propiedad desactivar borde pixel superior.</param>
    /// <param name="disablePixelTopLeftDistance">Propiedad desactivar borde pixel superior izquierdo.</param>
    /// 
    /// <param name="pixelOutlineTopDistance">Propiedad distancia superior.</param>
    /// <param name="pixelOutlineTopRightDistance">Propiedad distancia superior derecha.</param>
    /// <param name="pixelOutlineRightDistance">Propiedad distancia derecha.</param>
    /// <param name="pixelOutlineBottomRightDistance">Propiedad distancia inferior derecha.</param>
    /// <param name="pixelOutlineBottomDistance">Propiedad distancia inferior.</param>
    /// <param name="pixelOutlineBottomLeftDistance">Propiedad distancia inferior izquierda.</param>
    /// <param name="pixelOutlineLeftDistance">Propiedad distancia izquierda.</param>
    /// \endspanish
    public static void BuildSpritePixelOutline(MaterialProperty spritePixelOutline, MaterialProperty pixelOutlineColor, MaterialProperty pixelOutlineWidth, MaterialProperty pixelOutlineReverse, MaterialProperty outerPixelOutline, MaterialProperty discardTransparentPixels, MaterialProperty disableTopLeftPixelOutline, MaterialProperty disableTopPixelOutline, MaterialProperty disableTopRightPixelOutline, MaterialProperty disableRightPixelOutline, MaterialProperty disableBottomRightPixelOutline, MaterialProperty disableBottomPixelOutline, MaterialProperty disableBottomLeftPixelOutline, MaterialProperty disableLeftPixelOutline, MaterialProperty disablePixelTopLeftDistance, MaterialProperty pixelOutlineTopDistance, MaterialProperty pixelOutlineTopRightDistance, MaterialProperty pixelOutlineRightDistance, MaterialProperty pixelOutlineBottomRightDistance, MaterialProperty pixelOutlineBottomDistance, MaterialProperty pixelOutlineBottomLeftDistance, MaterialProperty pixelOutlineLeftDistance)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Sprite Pixel Outline", "Pixel art outline for sprites.", spritePixelOutline, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Outline Color (RGBA)", "Color of the outline.", pixelOutlineColor, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Outline Width", "Width of the outline.", pixelOutlineWidth, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Outline Reverse", "If enabledd outlines is created from center of the sprite", pixelOutlineReverse, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Outer Outline", "If enabledd outlines is created from external part of the sprite.", outerPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Discard Transparent Pixels", "If enabledd outline doesn't affect to non opaque pixels.", discardTransparentPixels, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Top Left Outline", "If enabledd disables the top left outline.", disableTopLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Top Outline", "If enabledd disables the top outline.", disableTopPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Right Outline", "If enabledd disables the right outline.", disableTopRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Right Outline", "If enabledd disables the bottom right outline.", disableRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Right Outline", "If enabledd disables the right outline.", disableBottomRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Outline", "If enabledd disables the bottom outline.", disableBottomPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Bottom Left Outline", "If enabledd disables the bottom left outline.", disableBottomLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Left Outline", "If enabledd disables the left outline.", disableLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Left Distance", "Distance of top left outline.", disablePixelTopLeftDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Distance", "Distance of top outline.", pixelOutlineTopDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Top Right Distance", "Distance of top right outline.", pixelOutlineTopRightDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Right Distance", "Distance of right outline.", pixelOutlineRightDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Right Distance", "Distance of bottom right outline.", pixelOutlineBottomRightDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Distance", "Distance of bottom outline.", pixelOutlineBottomDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Bottom Left Distance", "Distance of bottom left outline.", pixelOutlineBottomLeftDistance, spritePixelOutline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector3("Left Distance", "Distance of left outline.", pixelOutlineLeftDistance, spritePixelOutline.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Color adjustment fucntion builder.
    /// </summary>
    /// <param name="colorAdjustment">Color adjustment property.</param>
    /// <param name="hue">Hue property.</param>
    /// <param name="saturation">Saturation property.</param>
    /// <param name="value">Value property.</param>
    /// <param name="colorAdjustmentMaskMap">Color adjustment mask map property.</param>
    /// <param name="colorAdjustmentMaskLevel">Color adjustment mask level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de ajuste de color.
    /// </summary>
    /// <param name="colorAdjustment">Propiedad ajuste de color.</param>
    /// <param name="hue">Propiedad matiz.</param>
    /// <param name="saturation">Propiedad saturación.</param>
    /// <param name="value">Propiedad brillo.</param>
    /// <param name="colorAdjustmentMaskMap">Propiedad mapa de la máscara de ajuste de color.</param>
    /// <param name="colorAdjustmentMaskLevel">Propiedad nivel de la máscara de ajuste de color.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildSpriteColorAdjustment(MaterialProperty colorAdjustment, MaterialProperty hue, MaterialProperty saturation, MaterialProperty value, MaterialProperty colorAdjustmentMaskMap, MaterialProperty colorAdjustmentMaskLevel, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Sprite Color Adjustment", "Color adjustment for sprite shaders.", colorAdjustment, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Hue", "Color.", hue, colorAdjustment.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Saturation", "Color quantity.", saturation, colorAdjustment.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Value", "Brightness of the color.", value, colorAdjustment.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Color Adjustment Mask Map (RGBA)", "Mask texture.", colorAdjustmentMaskMap, materialEditor, true, colorAdjustment.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Color Adjustment Mask Level", "Level of mask effect in relation the source color.", colorAdjustmentMaskLevel, colorAdjustment.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// UV scroll function builder.
    /// </summary>
    /// <param name="uvScroll">UV scroll property.</param>
    /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
    /// <param name="flipUVVertical">Flip UV vertical property.</param>
    /// <param name="uvScrollAnimation">UV scroll animation property.</param>
    /// <param name="uvScrollSpeed">UV scroll speed property.</param>
    /// <param name="scrollByTexel">Scroll by texel property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// UV scroll constructor.
    /// </summary>
    /// <param name="uvScroll">Propiedad UV scroll.</param>
    /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
    /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
    /// <param name="uvScrollAnimation">Propiedad animación de desplazamiento de las UV.</param>
    /// <param name="uvScrollSpeed">Propiedad velocidad de desplazamiento de las UV.</param>
    /// <param name="scrollByTexel">Propiedad desplazamiento por texel.</param>
    /// \endspanish
    public static void BuildUVScroll(MaterialProperty uvScroll, MaterialProperty flipUVHorizontal, MaterialProperty flipUVVertical, MaterialProperty uvScrollAnimation, MaterialProperty uvScrollSpeed, MaterialProperty scrollByTexel)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("UV Scroll", "Scroll and Flip the UVs from a texture.", uvScroll, true);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Horizontal", "Flip UV Horizontal.", flipUVHorizontal, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Vertical", "Flip UV Vertical.", flipUVVertical, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("UV Scroll Animation", "If enabledd the UV they animated.", uvScrollAnimation, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector2("UV Scroll Speed", "UV Scroll Speed.", uvScrollSpeed, uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Scroll By Texel", "Scroll animation texel by texel.", scrollByTexel, toggleLock: uvScroll.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// UV scroll function builder sin scroll por texel.
    /// </summary>
    /// <param name="uvScroll">UV scroll property.</param>
    /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
    /// <param name="flipUVVertical">Flip UV vertical property.</param>
    /// <param name="uvScrollAnimation">UV scroll animation property.</param>
    /// <param name="uvScrollSpeed">UV scroll speed property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// UV scroll constructor without texel scroll.
    /// </summary>
    /// <param name="uvScroll">Propiedad UV scroll.</param>
    /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
    /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
    /// <param name="uvScrollAnimation">Propiedad animación de desplazamiento de las UV.</param>
    /// <param name="uvScrollSpeed">Propiedad velocidad de desplazamiento de las UV.</param>
    /// \endspanish
    public static void BuildUVScrollSimple(MaterialProperty uvScroll, MaterialProperty flipUVHorizontal, MaterialProperty flipUVVertical, MaterialProperty uvScrollAnimation, MaterialProperty uvScrollSpeed)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("UV Scroll", "Scroll and Flip the UVs from a texture.", uvScroll, true);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Horizontal", "Flip UV Horizontal.", flipUVHorizontal, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Vertical", "Flip UV Vertical.", flipUVVertical, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("UV Scroll Animation", "If enabledd the UV they animated.", uvScrollAnimation, toggleLock: uvScroll.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector2("UV Scroll Speed", "UV Scroll Speed.", uvScrollSpeed, uvScroll.floatValue);
    }

    /// \english
    /// <summary>
    /// UV scroll function builder sin scroll por texel header group.
    /// </summary>
    /// <param name="uvScroll">UV scroll property.</param>
    /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
    /// <param name="flipUVVertical">Flip UV vertical property.</param>
    /// <param name="uvScrollAnimation">UV scroll animation property.</param>
    /// <param name="uvScrollSpeed">UV scroll speed property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// UV scroll constructor without texel scroll grupo.
    /// </summary>
    /// <param name="uvScroll">Propiedad UV scroll.</param>
    /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
    /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
    /// <param name="uvScrollAnimation">Propiedad animación de desplazamiento de las UV.</param>
    /// <param name="uvScrollSpeed">Propiedad velocidad de desplazamiento de las UV.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildUVScrollSimple(MaterialProperty uvScroll, MaterialProperty flipUVHorizontal, MaterialProperty flipUVVertical, MaterialProperty uvScrollAnimation, MaterialProperty uvScrollSpeed, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("UV Scroll", "Scroll and Flip the UVs from a texture.", uvScroll, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Horizontal", "Flip UV Horizontal.", flipUVHorizontal, toggleLock: uvScroll.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Vertical", "Flip UV Vertical.", flipUVVertical, toggleLock: uvScroll.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("UV Scroll Animation", "If enabledd the UV they animated.", uvScrollAnimation, toggleLock: uvScroll.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildVector2("UV Scroll Speed", "UV Scroll Speed.", uvScrollSpeed, uvScroll.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// UV flip function builder.
    /// </summary>
    /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
    /// <param name="flipUVVertical">Flip UV vertical property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// UV flip constructor.
    /// </summary>
    /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
    /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
    /// \endspanish
    public static void BuildUVFlip(MaterialProperty flipUVHorizontal, MaterialProperty flipUVVertical)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeader("UV Flip", "Flip the UVs from a texture.");
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Horizontal", "Flip UV Horizontal.", flipUVHorizontal);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Flip UV Vertical", "Flip UV Vertical.", flipUVVertical);
  
    }

    /// \english
    /// <summary>
    /// Distortion function builder.
    /// </summary>
    /// <param name="distortion">Distortion property.</param>
    /// <param name="distortionMap">Distortion map property.</param>
    /// <param name="distortionLevel">distortion level property.</param>
    /// <param name="distortionScale">Distortion scale property.</param>
    /// <param name="distortionMask">Distortion mask property.</param>
    /// <param name="useUVScroll">Use UV scroll property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de distorsión.
    /// </summary>
    /// <param name="distortion">Propiedad distorsión.</param>
    /// <param name="distortionMap">Propiedad mapa de distorsión.</param>
    /// <param name="distortionLevel">Propiedad nivel de distorsión.</param>
    /// <param name="distortionScale">Propiedad escala de la distorsión.</param>
    /// <param name="distortionMask">Propiedad máscara de la distorsión.</param>
    /// <param name="useUVScroll">Propiedad uso del desplazamiento de UV.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildDistortion(MaterialProperty distortion, MaterialProperty distortionMap, MaterialProperty distortionLevel, MaterialProperty distortionScale, MaterialProperty distortionMask, MaterialProperty useUVScroll, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Distortion", "Distortion effect.", distortion, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Distortion Map (RG)", "Distortion Map (RG).", distortionMap, materialEditor, true, distortion.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Distortion Level", "Distortion Level.", distortionLevel, distortion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Distortion Scale", "Distortion Scale.", distortionScale, distortion.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Distortion Mask (RGB)", "Distortion Mask (RGB).", distortionMask, materialEditor, true, distortion.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use UV Scroll", "Use UV Scroll.", useUVScroll, distortion.floatValue);

        GUILayout.Space(25);
    }

    /// \english
    /// <summary>
    /// Refraction simple function builder.
    /// </summary>
    /// <param name="refraction">Distortion property.</param>
    /// <param name="refractionMap">Distortion map property.</param>
    /// <param name="refractionLevel">distortion level property.</param>
    /// <param name="refractionScroll">Intersection texture scroll property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de refracción simple.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionMap">Propiedad mapa de refracción.</param>
    /// <param name="refractionLevel">Propiedad nivel de refracción.</param>
    /// <param name="refractionScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish

    public static void BuildRefractionSimple(MaterialProperty refraction, MaterialProperty refractionMap, MaterialProperty refractionLevel, MaterialProperty refractionScroll, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Refraction", "Refraction effect.", refraction, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Map (RG)", "Refraction Map (RG).", refractionMap, materialEditor, true, refraction.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Refraction Level", "Refraction Level.", refractionLevel, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Refraction Scroll", "Scroll the refraction texture.", refractionScroll, refraction.floatValue);
    }

    /// \english
    /// <summary>
    /// Refraction simple with double normal map function builder.
    /// </summary>
    /// <param name="refraction">Distortion property.</param>
    /// <param name="refractionMap">Distortion map property.</param>
    /// <param name="refractionLevel">distortion level property.</param>
    /// <param name="refractionScroll">Intersection texture scroll property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de refracción simple con doble normal map.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionMap">Propiedad mapa de refracción.</param>
    /// <param name="refractionLevel">Propiedad nivel de refracción.</param>
    /// <param name="refractionScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish

    public static void BuildRefractionSimpleDoubleMap(MaterialProperty refraction, MaterialProperty refractionMap, MaterialProperty refractionLevel, MaterialProperty refractionScroll, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Refraction", "Refraction effect.", refraction, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Map (RG)", "Refraction Map (RG).", refractionMap, materialEditor, true, refraction.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Refraction Level", "Refraction Level. X value is for the level of the first refraction map copy and Y is for the level of the second refraction map copy.", refractionLevel, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector4("Refraction Scroll", "Scroll the refraction texture. XY value is for the scroll of the first refraction map copy and ZW is for the scroll of the second refraction map copy.", refractionScroll, refraction.floatValue);
    }

    /// \english
    /// <summary>
    /// Refraction simple function builder.
    /// </summary>
    /// <param name="refraction">Distortion property.</param>
    /// <param name="refractionMap">Distortion map property.</param>
    /// <param name="refractionLevel">distortion level property.</param>
    /// <param name="refractionScroll">Intersection texture scroll property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de refracción simple.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionMap">Propiedad mapa de refracción.</param>
    /// <param name="refractionLevel">Propiedad nivel de refracción.</param>
    /// <param name="refractionScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildRefractionSimpleWithMask(MaterialProperty refraction, MaterialProperty refractionMap, MaterialProperty refractionLevel, MaterialProperty refractionScroll, MaterialProperty refractionMask, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Refraction", "Refraction effect.", refraction, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Map (RG)", "Refraction Map (RG).", refractionMap, materialEditor, true, refraction.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Refraction Level", "Refraction Level.", refractionLevel, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Refraction Scroll", "Scroll the refraction texture.", refractionScroll, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Mask (R)", "Refraction Mask.", refractionMask, materialEditor, false, refraction.floatValue, true);
    }

    /// \english
    /// <summary>
    /// Refraction medium function builder.
    /// </summary>
    /// <param name="refraction">Distortion property.</param>
    /// <param name="refractionIndex">Distortion index property.</param>
    /// <param name="refractionMap">Distortion map property.</param>
    /// <param name="refractionLevel">Distortion level property.</param>
    /// <param name="refractionScroll">Intersection texture scroll property.</param>
    /// <param name="chromaticDispersion">Chromatic dispersion property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de refracción media.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionIndex">Propiedad índice de refracción.</param>
    /// <param name="refractionMap">Propiedad mapa de refracción.</param>
    /// <param name="refractionLevel">Propiedad nivel de refracción.</param>
    /// <param name="refractionScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="chromaticDispersion">Propiedad de la dispersión cromática.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildRefractionAdvanced(MaterialProperty refraction, MaterialProperty refractionIndex, MaterialProperty refractionMap, MaterialProperty refractionLevel, MaterialProperty refractionScroll, MaterialProperty chromaticDispersion, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Refraction", "Refraction effect.", refraction, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Refraction Index", "Index of refraction.", refractionIndex, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Map (RG)", "Refraction Map (RG).", refractionMap, materialEditor, true, refraction.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Refraction Level", "Refraction Level.", refractionLevel, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector4("Refraction Scroll", "Scroll the refraction texture.", refractionScroll, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Chromatic Dispersion", "Dispersion of the color channels.", chromaticDispersion, refraction.floatValue);
    }

    /// \english
    /// <summary>
    /// Refraction medium function builder header group.
    /// </summary>
    /// <param name="refraction">Distortion property.</param>
    /// <param name="refractionIndex">Distortion index property.</param>
    /// <param name="refractionMap">Distortion map property.</param>
    /// <param name="refractionLevel">Distortion level property.</param>
    /// <param name="refractionScroll">Intersection texture scroll property.</param>
    /// <param name="chromaticDispersion">Chromatic dispersion property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de refracción media grupo.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionIndex">Propiedad índice de refracción.</param>
    /// <param name="refractionMap">Propiedad mapa de refracción.</param>
    /// <param name="refractionLevel">Propiedad nivel de refracción.</param>
    /// <param name="refractionScroll">Propiedad del desplazamiento de la textura de la intersección.</param>
    /// <param name="chromaticDispersion">Propiedad de la dispersión cromática.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildRefractionAdvanced(MaterialProperty refraction, MaterialProperty refractionIndex, MaterialProperty refractionMap, MaterialProperty refractionLevel, MaterialProperty refractionScroll, MaterialProperty chromaticDispersion, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Refraction", "Refraction effect.", refraction, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildSlider("Refraction Index", "Index of refraction.", refractionIndex, refraction.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Refraction Map (RG)", "Refraction Map (RG).", refractionMap, materialEditor, true, refraction.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildVector2("Refraction Level", "Refraction Level.", refractionLevel, refraction.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildVector4("Refraction Scroll", "Scroll the refraction texture.", refractionScroll, refraction.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Chromatic Dispersion", "Dispersion of the color channels.", chromaticDispersion, refraction.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Projector function builder.
    /// </summary>
    /// <param name="shadowMap">Shadow map property.</param>
    /// <param name="shadowMapTiling">Shadow map tiling property.</param>
    /// <param name="shadowMapOffset">Shadow map offset property.</param>
    /// <param name="shadowColor">Sadow color property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="falloffMap">Falloff map property.</param>
    /// <param name="backfaceCulling">Backface culling property.</param>
    /// <param name="useVertexPosition">Use vertex position property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de proyector.
    /// </summary>
    /// <param name="shadowMap">Propiedad mapa de sombra.</param>
    /// <param name="shadowMapTiling">Propiedad repetición del mapa de sombra.</param>
    /// <param name="shadowMapOffset">Propiedad desplazamiento del mapa de sombra.</param>
    /// <param name="shadowColor">Propiedad color de la sombra.</param>
    /// <param name="shadowLevel">Propiedad nivel de la sombra.</param>
    /// <param name="falloffMap">Propiedad mapa de declive.</param>
    /// <param name="backfaceCulling">Propiedad oclisión de la cara trasera.</param>
    /// <param name="useVertexPosition">Propiedad uso de la posición de los vértices.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildProjector(MaterialProperty shadowMap, MaterialProperty shadowMapTiling, MaterialProperty shadowMapOffset, MaterialProperty shadowColor, MaterialProperty shadowLevel, MaterialProperty falloffMap, MaterialProperty backfaceCulling, MaterialProperty useVertexPosition, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeader("Projector", "Projector functionalities.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Cookie (RGB)", "Projection texture. Only uses the RGB channels.", shadowMap, materialEditor, false, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Cookie Tiling", "Scale of the UV of the texture of the projection.", shadowMapTiling);
        CGFMaterialEditorUtilitiesClass.BuildVector2("Cookie Offset", "Offset of the UV of the texture of the projection.", shadowMapOffset);
        CGFMaterialEditorUtilitiesClass.BuildColor("Cookie Color (RGB)", "Color of the projection. Only uses the RGB channels.", shadowColor);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Cookie Level", "Projection level intensity.", shadowLevel);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Falloff Map (RGB)", "Texture that determines the fading of the projection along its trajectory, it is a linear gradient texture. Only uses the RGB channels.", falloffMap, materialEditor, false, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Backface Culling", "If enabledd cull the projection on the backfaces of the mesh.", backfaceCulling);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Vertex Position", "If enabledd use the vertex position instead the vertex normal to cull the backfaces of the mesh.", useVertexPosition, toggleLock: backfaceCulling.floatValue);
    }


    /// \english
    /// <summary>
    /// Projector function builder header group.
    /// </summary>
    /// <param name="shadowMap">Shadow map property.</param>
    /// <param name="shadowMapTiling">Shadow map tiling property.</param>
    /// <param name="shadowMapOffset">Shadow map offset property.</param>
    /// <param name="shadowColor">Sadow color property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="falloffMap">Falloff map property.</param>
    /// <param name="backfaceCulling">Backface culling property.</param>
    /// <param name="useVertexPosition">Use vertex position property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de proyector grupo.
    /// </summary>
    /// <param name="shadowMap">Propiedad mapa de sombra.</param>
    /// <param name="shadowMapTiling">Propiedad repetición del mapa de sombra.</param>
    /// <param name="shadowMapOffset">Propiedad desplazamiento del mapa de sombra.</param>
    /// <param name="shadowColor">Propiedad color de la sombra.</param>
    /// <param name="shadowLevel">Propiedad nivel de la sombra.</param>
    /// <param name="falloffMap">Propiedad mapa de declive.</param>
    /// <param name="backfaceCulling">Propiedad oclisión de la cara trasera.</param>
    /// <param name="useVertexPosition">Propiedad uso de la posición de los vértices.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildProjector(MaterialProperty shadowMap, MaterialProperty shadowMapTiling, MaterialProperty shadowMapOffset, MaterialProperty shadowColor, MaterialProperty shadowLevel, MaterialProperty falloffMap, MaterialProperty backfaceCulling, MaterialProperty useVertexPosition, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Projector", "Projector functionalities.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("Cookie (RGB)", "Projection texture. Only uses the RGB channels.", shadowMap, materialEditor, false, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildVector2("Cookie Tiling", "Scale of the UV of the texture of the projection.", shadowMapTiling);
            CGFMaterialEditorUtilitiesClass.BuildVector2("Cookie Offset", "Offset of the UV of the texture of the projection.", shadowMapOffset);
            CGFMaterialEditorUtilitiesClass.BuildColor("Cookie Color (RGB)", "Color of the projection. Only uses the RGB channels.", shadowColor);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Cookie Level", "Projection level intensity.", shadowLevel);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Falloff Map (RGB)", "Texture that determines the fading of the projection along its trajectory, it is a linear gradient texture. Only uses the RGB channels.", falloffMap, materialEditor, false, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Backface Culling", "If enabledd cull the projection on the backfaces of the mesh.", backfaceCulling);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Vertex Position", "If enabledd use the vertex position instead the vertex normal to cull the backfaces of the mesh.", useVertexPosition, toggleLock: backfaceCulling.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Particle Unlit properties builder.
    /// </summary>
    /// <param name="texture">Texture property.</param>
    /// <param name="useAlphaClip">Use alpha clip property.</param>
    /// <param name="cutoff">Cutoff property.</param>
    /// <param name="color">Color property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de las propiedades de partícula unlit.
    /// </summary>
    /// <param name="texture">Propiedad textura.</param>
    /// <param name="useAlphaClip">Propiedad utilizar descarte por canal alpha.</param>
    /// <param name="cutoff">Propiedad descarte.</param>
    /// <param name="color">Propiedad color.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildParticleUnlit(MaterialProperty texture, MaterialProperty useAlphaClip, MaterialProperty cutoff, MaterialProperty color, MaterialEditor materialEditor)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeader("Particle Unlit", "Particle Unlit functionalities.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Particle Texture (RGBA)", "Texture of the particle.", texture, materialEditor, false);
        CGFMaterialEditorUtilitiesClass.BuildTransparentCutoffModeToggle("Use Alpha Clip", "Enables Alpha Clip.", useAlphaClip, materialEditor);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha cutoff", "Alpha Cutoff value.", cutoff, useAlphaClip.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Color (RGBA)", "Main color.", color);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Camera fading function builder.
    /// </summary>
    /// <param name="cameraDistanceFading">Camera distance fading property.</param>
    /// <param name="cameraFadingNearPoint">Camera fading near point property.</param>
    /// <param name="cameraFadingFarPoint">Camera fading far point property.</param>
    /// <param name="cameraDirectionFading">Camera direction fading property.</param>
    /// <param name="opacityClip">Opaciry clip property.</param>
    /// <param name="opacityClipThreshold">Opaciry clip threshold property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de desvanecimiento por la cámara.
    /// </summary>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por distancia de la cámara.</param>
    /// <param name="cameraFadingNearPoint">Propiedad punto inicial del desvanecimiento.</param>
    /// <param name="cameraFadingFarPoint">Propiedad punto final del desvanecimiento.</param>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por dirección de la cámara.</param>
    /// <param name="opacityClip">Propiedad descarte por opacidad.</param>
    /// <param name="opacityClipThreshold">Propiedad umbral de descarte por opacidad.</param>
    /// \endspanish
    public static void BuildCameraFading(MaterialProperty cameraDistanceFading, MaterialProperty cameraFadingNearPoint, MaterialProperty cameraFadingFarPoint, MaterialProperty cameraDirectionFading, MaterialProperty opacityClip, MaterialProperty opacityClipThreshold, float renderMode)
    {
        float transparent;

        if(renderMode >= 1)
        {
            GUI.enabled = true;
            transparent = 1;
        }
        else {
            GUI.enabled = false;
            transparent = 0;
        }

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Camera Fading", "Opacity by camera distance and direction.", cameraDistanceFading, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Near Point", "Start point of the fading. Offset to position camera.", cameraFadingNearPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Far Point", "End point of the fading.", cameraFadingFarPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Camera Direction Fading", "Opacity by camera direction.", cameraDirectionFading, true, transparent);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Opacity Clip", "If enabledd clips the mesh with less opacity than the opacity clip threshold.", opacityClip, toggleLock: (((cameraDistanceFading.floatValue == 1.0f || cameraDirectionFading.floatValue == 1.0f) ? 1.0f : 0.0f) * transparent));
        CGFMaterialEditorUtilitiesClass.BuildSlider("Opacity Clip Threshold", "Opacity clip threshold.", opacityClipThreshold, (((cameraDistanceFading.floatValue == 1.0f || cameraDirectionFading.floatValue == 1.0f) ? 1.0f : 0.0f * opacityClip.floatValue) * transparent));
        
        GUI.enabled = true;
    }
    

    /// \english
    /// <summary>
    /// Camera fading function builder.
    /// </summary>
    /// <param name="cameraDistanceFading">Camera distance fading property.</param>
    /// <param name="cameraFadingNearPoint">Camera fading near point property.</param>
    /// <param name="cameraFadingFarPoint">Camera fading far point property.</param>
    /// <param name="cameraDirectionFading">Camera direction fading property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de desvanecimiento por la cámara.
    /// </summary>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por distancia de la cámara.</param>
    /// <param name="cameraFadingNearPoint">Propiedad punto inicial del desvanecimiento.</param>
    /// <param name="cameraFadingFarPoint">Propiedad punto final del desvanecimiento.</param>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por dirección de la cámara.</param>
    /// \endspanish
    public static void BuildCameraFadingSimple(MaterialProperty cameraDistanceFading, MaterialProperty cameraFadingNearPoint, MaterialProperty cameraFadingFarPoint, MaterialProperty cameraDirectionFading, float renderMode)
    {
        float transparent;

        if(renderMode >= 1)
        {
            GUI.enabled = true;
            transparent = 1;
        }
        else {
            GUI.enabled = false;
            transparent = 0;
        }

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Camera Fading", "Opacity by camera distance and direction.", cameraDistanceFading, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Near Point", "Start point of the fading. Offset to position camera.", cameraFadingNearPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Far Point", "End point of the fading.", cameraFadingFarPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Camera Direction Fading", "Opacity by camera direction.", cameraDirectionFading, true, (cameraDistanceFading.floatValue * transparent));

        GUI.enabled = true;
    }

    /// \english
    /// <summary>
    /// Camera fading function builder.
    /// </summary>
    /// <param name="cameraDistanceFading">Camera distance fading property.</param>
    /// <param name="cameraFadingNearPoint">Camera fading near point property.</param>
    /// <param name="cameraFadingFarPoint">Camera fading far point property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de desvanecimiento por la cámara.
    /// </summary>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por distancia de la cámara.</param>
    /// <param name="cameraFadingNearPoint">Propiedad punto inicial del desvanecimiento.</param>
    /// <param name="cameraFadingFarPoint">Propiedad punto final del desvanecimiento.</param>
    /// \endspanish
    public static void BuildCameraFadingMinimum(MaterialProperty cameraDistanceFading, MaterialProperty cameraFadingNearPoint, MaterialProperty cameraFadingFarPoint, float renderMode, bool showGizmo, string playerPrefKeyName)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        float transparent;

        if(renderMode >= 1)
        {
            GUI.enabled = true;
            transparent = 1;
        }
        else {
            GUI.enabled = false;
            transparent = 0;
        }

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Camera Fading", "Opacity by camera distance and direction.", cameraDistanceFading, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Near Point", "Start point of the fading. Offset to position camera.", cameraFadingNearPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Far Point", "End point of the fading.", cameraFadingFarPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));

        showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Camera Distance Fading Gizmo", "If enabled show camera distance fading gizmo.", cameraDistanceFading.floatValue * renderMode, cameraDistanceFading, playerPrefKeyName);

        GUI.enabled = true;
    }

    /// \english
    /// <summary>
    /// Camera fading function builder header group.
    /// </summary>
    /// <param name="cameraDistanceFading">Camera distance fading property.</param>
    /// <param name="cameraFadingNearPoint">Camera fading near point property.</param>
    /// <param name="cameraFadingFarPoint">Camera fading far point property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de desvanecimiento por la cámara grupo.
    /// </summary>
    /// <param name="cameraDistanceFading">Propiedad desvanecimiento por distancia de la cámara.</param>
    /// <param name="cameraFadingNearPoint">Propiedad punto inicial del desvanecimiento.</param>
    /// <param name="cameraFadingFarPoint">Propiedad punto final del desvanecimiento.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildCameraFadingMinimum(MaterialProperty cameraDistanceFading, MaterialProperty cameraFadingNearPoint, MaterialProperty cameraFadingFarPoint, float renderMode, out bool showGizmo, bool isUnfold, string playerPrefKeyName)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Camera Fading", "Opacity by camera distance and direction.", cameraDistanceFading, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            float transparent;

            if(renderMode >= 1)
            {
                GUI.enabled = true;
                transparent = 1;
            }
            else {
                GUI.enabled = false;
                transparent = 0;
            }

            CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Near Point", "Start point of the fading. Offset to position camera.", cameraFadingNearPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
            CGFMaterialEditorUtilitiesClass.BuildFloat("Camera Fading Far Point", "End point of the fading.", cameraFadingFarPoint, toggleLock: (cameraDistanceFading.floatValue * transparent));
            
            showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Camera Distance Fading Gizmo", "If enabled show camera distance fading gizmo.", cameraDistanceFading.floatValue * renderMode, cameraDistanceFading, playerPrefKeyName);

            GUI.enabled = true;
        }
    }

    

    /// \english
    /// <summary>
    /// Soft particles function builder.
    /// </summary>
    /// <param name="softParticles">Soft particles property.</param>
    /// <param name="fadeDistance">Fade distance property.</param>
    /// <param name="fadeFalloff">Fade falloff property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de partículas suaves.
    /// </summary>
    /// <param name="softParticles">Propiedad Soft particles.</param>
    /// <param name="fadeDistance">Propiedad distancia del desvanecimiento.</param>
    /// <param name="fadeFalloff">Propiedad declive del desvanecimiento.</param>
    /// \endspanish
    public static void BuildSoftParticles(MaterialProperty softParticles, MaterialProperty fadeDistance, MaterialProperty fadeFalloff, float renderMode)
    {   
        float transparent;

        if(renderMode >= 1)
        {
            GUI.enabled = true;
            transparent = 1;
        }
        else {
            GUI.enabled = false;
            transparent = 0;
        }

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Soft Particles", "Fade out particles when they get close to the surface of objects.", softParticles, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Fade Distance", "Fade length.", fadeDistance, toggleLock: (softParticles.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildFloat("Fade Falloff", "Fallof value.", fadeFalloff, toggleLock: (softParticles.floatValue * transparent));

        GUI.enabled = true;
    }

    /// \english
    /// <summary>
    /// Soft particles function builder.
    /// </summary>
    /// <param name="softParticles">Soft particles property.</param>
    /// <param name="fadeDistance">Fade distance property.</param>
    /// <param name="fadeFalloff">Fade falloff property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de partículas suaves.
    /// </summary>
    /// <param name="softParticles">Propiedad Soft particles.</param>
    /// <param name="fadeDistance">Propiedad distancia del desvanecimiento.</param>
    /// <param name="fadeFalloff">Propiedad declive del desvanecimiento.</param>
    /// \endspanish
    public static void BuildSoftParticlesStandard(MaterialProperty softParticles, MaterialProperty fadeDistance, MaterialProperty fadeFalloff, float renderMode)
    {   
        float transparent;

        if(renderMode == 2 || renderMode == 1)
        {
            GUI.enabled = true;
            transparent = 1;
        }
        else {
            GUI.enabled = false;
            transparent = 0;
        }

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Soft Particles", "Fade out particles when they get close to the surface of objects.", softParticles, true);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Fade Distance", "Fade length.", fadeDistance, toggleLock: (softParticles.floatValue * transparent));
        CGFMaterialEditorUtilitiesClass.BuildFloat("Fade Falloff", "Fallof value.", fadeFalloff, toggleLock: (softParticles.floatValue * transparent));

        GUI.enabled = true;
    }

    /// \english
    /// <summary>
    /// Point filter function builder.
    /// </summary>
    /// <param name="pointFilter">Soft particles property.</param>
    /// <param name="sizeOffset">Size offset property.</param>
    /// <param name="customSize">Custom size property.</param>
    /// <param name="textureSize">Texture size property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de filtro point.
    /// </summary>
    /// <param name="pointFilter">Propiedad Point filter.</param>
    /// <param name="sizeOffset">Propiedad ajuste de tamaño de textura.</param>
    /// <param name="customSize">Propiedad textura personalizado.</param>
    /// <param name="textureSize">Propiedad tamaño de textura personalizado.</param>
    /// \endspanish
    public static void BuildPointFilter(MaterialProperty pointFilter, MaterialProperty sizeOffset, MaterialProperty customSize, MaterialProperty textureSize)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Point Filter", "Simulate point filter mode of a texture.", pointFilter, true);
        CGFMaterialEditorUtilitiesClass.BuildVector2Round("Size Offset", "Offset to fit the point filter effect", sizeOffset, toggleLock: pointFilter.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Custom Size", "If enabled use a custom font texture size.", customSize, toggleLock: pointFilter.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildVector2RoundPositive("Texture Size", "Custom font texure size.", textureSize, toggleLock: pointFilter.floatValue);

    }

    /// \english
    /// <summary>
    /// Emission function builder.
    /// </summary>
    /// <param name="emission">Emission property.</param>
    /// <param name="emissionMap">Emission map property.</param>
    /// <param name="emissionColor">Emission color property.</param>
    /// <param name="emissionLevel">Emission level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de emission.
    /// </summary>
    /// <param name="emission">Propiedad Emission.</param>
    /// <param name="emissionMap">Propiedad mapa de emisión.</param>
    /// <param name="emissionColor">Propiedad color de emisión.</param>
    /// <param name="emissionLevel">Propiedad nivel de emisión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildEmission(MaterialProperty emission, MaterialProperty emissionMap, MaterialProperty emissionColor, MaterialProperty emissionLevel, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Emission", "Emission color.", emission, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Emission Map (RGB)", "Texture applied to emission area", emissionMap, materialEditor, true, emission.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildColorHDR("Emission Color (RGB)", "Emission color.", emissionColor, emission.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Emission Level", "Level of emission color in relation the source color.", emissionLevel, emission.floatValue);
        
        EditorGUI.BeginChangeCheck();
            
        if (EditorGUI.EndChangeCheck())
        {
            //https://answers.unity.com/questions/1152443/lightmap-emission-with-custom-shaders.html
            //https://forum.unity.com/threads/how-to-write-emission-to-lightmap-in-vert-frag-shader.462926/
            //https://forum.unity.com/threads/emmisive.492609/#post-3206559
            foreach (Material target in materialEditor.targets)
            {
                target.globalIlluminationFlags &=
					~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
        
    }

    /// \english
    /// <summary>
    /// Emission function builder without keyword.
    /// </summary>
    /// <param name="emission">Emission property.</param>
    /// <param name="emissionMap">Emission map property.</param>
    /// <param name="emissionColor">Emission color property.</param>
    /// <param name="emissionLevel">Emission level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de emission sin keyword.
    /// </summary>
    /// <param name="emission">Propiedad Emission.</param>
    /// <param name="emissionMap">Propiedad mapa de emisión.</param>
    /// <param name="emissionColor">Propiedad color de emisión.</param>
    /// <param name="emissionLevel">Propiedad nivel de emisión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildEmissionWithoutKeyword(MaterialProperty emission, MaterialProperty emissionMap, MaterialProperty emissionColor, MaterialProperty emissionLevel, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Emission", "Emission color.", emission);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Emission Map (RGB)", "Texture applied to emission area", emissionMap, materialEditor, true, emission.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildColorHDR("Emission Color (RGB)", "Emission color.", emissionColor, emission.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Emission Level", "Level of emission color in relation the source color.", emissionLevel, emission.floatValue);

        EditorGUI.BeginChangeCheck();
            
        if (EditorGUI.EndChangeCheck())
        {
            //https://answers.unity.com/questions/1152443/lightmap-emission-with-custom-shaders.html
            //https://forum.unity.com/threads/how-to-write-emission-to-lightmap-in-vert-frag-shader.462926/
            //https://forum.unity.com/threads/emmisive.492609/#post-3206559
            foreach (Material target in materialEditor.targets)
            {
                target.globalIlluminationFlags &=
					~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
    }

    /// \english
    /// <summary>
    /// Emission function builder with embbeded emission level.
    /// </summary>
    /// <param name="emission">Emission property.</param>
    /// <param name="emissionMap">Emission map property.</param>
    /// <param name="emissionColor">Emission color property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de emission con nivel de emisión incluido.
    /// </summary>
    /// <param name="emission">Propiedad Emission.</param>
    /// <param name="emissionMap">Propiedad mapa de emisión.</param>
    /// <param name="emissionColor">Propiedad color de emisión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildEmissionStandard(MaterialProperty emission, MaterialProperty emissionMap, MaterialProperty emissionColor, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Emission", "Emission color.", emission, true);

        if (emission.floatValue == 1)
        {
            GUI.enabled = true;
        }
        else
        {
            GUI.enabled = false;
        }

        // Obsolete.
        //materialEditor.TexturePropertyWithHDRColor(new GUIContent("Emission Value", "Texture (RGB), color (RGB) and brightness level applied to emission area."), emissionMap, emissionColor, new ColorPickerHDRConfig(0f, 99f, 1f / 99f, 3f), false);
        materialEditor.TexturePropertyWithHDRColor(new GUIContent("Emission Value", "Texture (RGB), color (RGB) and brightness level applied to emission area."), emissionMap, emissionColor, false);
        
        GUI.enabled = true;

        EditorGUI.BeginChangeCheck();

        if (EditorGUI.EndChangeCheck())
        {
            //https://answers.unity.com/questions/1152443/lightmap-emission-with-custom-shaders.html
            //https://forum.unity.com/threads/how-to-write-emission-to-lightmap-in-vert-frag-shader.462926/
            //https://forum.unity.com/threads/emmisive.492609/#post-3206559
            foreach (Material target in materialEditor.targets)
            {
                target.globalIlluminationFlags &=
					~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
    }

    /// \english
    /// <summary>
    /// Emission function builder with embbeded emission level header group.
    /// </summary>
    /// <param name="emission">Emission property.</param>
    /// <param name="emissionMap">Emission map property.</param>
    /// <param name="emissionColor">Emission color property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de emission con nivel de emisión incluido grupo.
    /// </summary>
    /// <param name="emission">Propiedad Emission.</param>
    /// <param name="emissionMap">Propiedad mapa de emisión.</param>
    /// <param name="emissionColor">Propiedad color de emisión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildEmissionStandard(MaterialProperty emission, MaterialProperty emissionMap, MaterialProperty emissionColor, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Emission", "Emission color.", emission, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            if (emission.floatValue == 1)
            {
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
            }

            // Obsolete.
            //materialEditor.TexturePropertyWithHDRColor(new GUIContent("Emission Value", "Texture (RGB), color (RGB) and brightness level applied to emission area."), emissionMap, emissionColor, new ColorPickerHDRConfig(0f, 99f, 1f / 99f, 3f), false);
            materialEditor.TexturePropertyWithHDRColor(new GUIContent("Emission Value", "Texture (RGB), color (RGB) and brightness level applied to emission area."), emissionMap, emissionColor, false);

            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                //https://answers.unity.com/questions/1152443/lightmap-emission-with-custom-shaders.html
                //https://forum.unity.com/threads/how-to-write-emission-to-lightmap-in-vert-frag-shader.462926/
                //https://forum.unity.com/threads/emmisive.492609/#post-3206559
                foreach (Material target in materialEditor.targets)
                {
                    target.globalIlluminationFlags &=
                        ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                }
            }
        }
    }

    /// \english
    /// <summary>
    /// Normal function builder.
    /// </summary>
    /// <param name="normal">Normal property.</param>
    /// <param name="normalMap">Normal map property.</param>
    /// <param name="normalLevel">Normal map level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de normal.
    /// </summary>
    /// <param name="normal">Propiedad Normal.</param>
    /// <param name="normalMap">Propiedad mapa de normal.</param>
    /// <param name="normalLevel">Nivel del normal map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildNormalASE(MaterialProperty normal, MaterialProperty normalMap, MaterialProperty normalLevel, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Normal", "Normal mapping.", normal);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Normal Map (RGB)", "Normal map. Only uses the RGB channels.", normalMap, materialEditor, true, normal.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Normal Level", "Scale of the normal map.", normalLevel, normal.floatValue);

    }

    /// \english
    /// <summary>
    /// Normal function builder.
    /// </summary>
    /// <param name="normal">Normal property.</param>
    /// <param name="normalMap">Normal map property.</param>
    /// <param name="normalLevel">Normal map level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de normal.
    /// </summary>
    /// <param name="normal">Propiedad Normal.</param>
    /// <param name="normalMap">Propiedad mapa de normal.</param>
    /// <param name="normalLevel">Nivel del normal map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildNormal(MaterialProperty normal, MaterialProperty normalMap, MaterialProperty normalLevel, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Normal", "Normal mapping.", normal, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Normal Map (RGB)", "Normal map. Only uses the RGB channels.", normalMap, materialEditor, scaleOffset, normal.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Normal Level", "Scale of the normal map.", normalLevel, normal.floatValue);
    }

    /// \english
    /// <summary>
    /// Normal function builder header group.
    /// </summary>
    /// <param name="normal">Normal property.</param>
    /// <param name="normalMap">Normal map property.</param>
    /// <param name="normalLevel">Normal map level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de normal grupo.
    /// </summary>
    /// <param name="normal">Propiedad Normal.</param>
    /// <param name="normalMap">Propiedad mapa de normal.</param>
    /// <param name="normalLevel">Nivel del normal map.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildNormal(MaterialProperty normal, MaterialProperty normalMap, MaterialProperty normalLevel, MaterialEditor materialEditor, bool scaleOffset, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Normal", "Normal mapping.", normal, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("Normal Map (RGB)", "Normal map. Only uses the RGB channels.", normalMap, materialEditor, scaleOffset, normal.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Normal Level", "Scale of the normal map.", normalLevel, normal.floatValue);
        }
    }

    /// \english
    /// <summary>
    /// Normal function builder.
    /// </summary>
    /// <param name="normal">Normal property.</param>
    /// <param name="normalMap">Normal map property.</param>
    /// <param name="normalLevel">Normal map level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de normal.
    /// </summary>
    /// <param name="normal">Propiedad Normal.</param>
    /// <param name="normalMap">Propiedad mapa de normal.</param>
    /// <param name="normalLevel">Nivel del normal map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildNormalWithoutKeyword(MaterialProperty normal, MaterialProperty normalMap, MaterialProperty normalLevel, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Normal", "Normal mapping.", normal);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Normal Map (RGB)", "Normal map. Only uses the RGB channels.", normalMap, materialEditor, true, normal.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Normal Level", "Scale of the normal map.", normalLevel, normal.floatValue);
    }

    /// \english
    /// <summary>
    /// Parallax function builder.
    /// </summary>
    /// <param name="parallaxMode">Parallax mode property.</param>
    /// <param name="parallaxMap">Parallax mode property.</param>
    /// <param name="parallaxLevel">Parallax level property.</param>
    /// <param name="parallaxIterations">Parallax iterations level property.</param>
    /// <param name="parallaxRaymarchingSearchSteps">Raymarching ray lenght steps.</param>
    /// <param name="parallaxOcclusionSampleRange">Iterations that compound the height simulation.</param>
    /// <param name="parallaxOcclusionInterpolationSteps">Steps of the interpolation between iterations.</param>
    /// <param name="parallaxOcclusionClipEdge">If enabled clips the ends of the uvs to give a more 3D look at the edges of the geometry.</param>
    /// <param name="parallaxOcclusionClipSilhouette">If enabled uses the UV coordinates to clip the effect curvature in X or V axis, useful for cylinders, works best with "Clip Edges" property disabled.</param>
    /// <param name="parallaxOcclusionCurvatureU">U coordinates to clip the effect curvature.</param>
    /// <param name="parallaxOcclusionCurvatureV">V coordinates to clip the effect curvature.</param>
    /// <param name="parallaxOcclusionCurvatureBias">Adjust of the effect curvature.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función parallax.
    /// </summary>
    /// <param name="parallaxMode">Propiedad modo de parallax.</param>
    /// <param name="parallaxMap">Propiedad mapa de parallax.</param>
    /// <param name="parallaxLevel">Nivel del parallax map.</param>
    /// <param name="parallaxIterations">Iteraciones del parallax.</param>
    /// <param name="parallaxRaymarchingSearchSteps">Iteraciones de la longitud del rayo del raymarching.</param>
    /// <param name="parallaxOcclusionSampleRange">Iteraciones que componen la simulación de la altura.</param>
    /// <param name="parallaxOcclusionInterpolationSteps">Pasos de la interpolación entre iteraciones.</param>
    /// <param name="parallaxOcclusionClipEdge">Si está habilitado, recorta los extremos de los uvs para dar un aspecto más 3D a los bordes de la geometría.</param>
    /// <param name="parallaxOcclusionClipSilhouette">Si está habilitado, usa las coordenadas UV para recortar la curvatura del efecto en el eje X o V, útil para cilindros, funciona mejor con la propiedad "Clip Edges" deshabilitadoa.</param>
    /// <param name="parallaxOcclusionCurvatureU">Coordenadas U para recortar la curvatura del efecto.</param>
    /// <param name="parallaxOcclusionCurvatureV">Coordenadas V para recortar la curvatura del efecto.</param>
    /// <param name="parallaxOcclusionCurvatureBias">Ajuste de la curvatura del efecto.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildParallax(MaterialProperty parallaxMode, MaterialProperty parallaxMap, MaterialProperty parallaxLevel, MaterialProperty parallaxIterations, MaterialProperty parallaxRaymarchingSearchSteps, 
    MaterialProperty parallaxOcclusionSampleRange, MaterialProperty parallaxOcclusionInterpolationSteps, MaterialProperty parallaxOcclusionClipEdge, MaterialProperty parallaxOcclusionClipSilhouette,
    MaterialProperty parallaxOcclusionCurvatureU, MaterialProperty parallaxOcclusionCurvatureV, MaterialProperty parallaxOcclusionCurvatureBias, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithPopup("Parallax Mode", "Parallax mode.", new string[]{"None", "Parallax Offset", "Parallax Mapping", "Parallax Mapping Raymarching", "Parallax Occlusion Mapping"}, parallaxMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Height Map (R)", "Displacement texture for simulate height. Only uses the R channels.", parallaxMap, materialEditor, true, parallaxMode.floatValue > 0 ? 1 : 0, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Height Level", "Level of height simulation", parallaxLevel, parallaxMode.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Height Iterations", "Iterations that compound the height simulation.", parallaxIterations, parallaxMode.floatValue == 2 || parallaxMode.floatValue == 3 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Raymarching Search Steps", "Lenght of the raymarching ray. More steps increase the quality of the effect but increase the performance cost.", parallaxRaymarchingSearchSteps, parallaxMode.floatValue == 3 ? 1 : 0);

        CGFMaterialEditorUtilitiesClass.BuildVector2Round("Occlusion Iterations Range", "Iterations that compound the height simulation.", parallaxOcclusionSampleRange, toggleLock: parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Occlusion Interpolation Steps", "More steps increase the quality of the effect but increase the performance cost.", parallaxOcclusionInterpolationSteps, parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Clip Edge", "If enabled clips the ends of the uvs to give a more 3D look at the edges of the geometry.", parallaxOcclusionClipEdge, toggleLock : parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Clip Silhouette", "If enabled uses the UV coordinates to clip the effect curvature in X or V axis, useful for cylinders, works best with 'Clip Edges' disabled.", parallaxOcclusionClipSilhouette, toggleLock : parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Curvature U", "U coordinates to clip the effect curvature.", parallaxOcclusionCurvatureU, parallaxOcclusionClipSilhouette.floatValue == 1 && parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Curvature V", "V coordinates to clip the effect curvature.", parallaxOcclusionCurvatureV, parallaxOcclusionClipSilhouette.floatValue == 1 && parallaxMode.floatValue == 4 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Curvature Bias", "Adjust of the effect curvature.", parallaxOcclusionCurvatureBias, parallaxOcclusionClipSilhouette.floatValue == 1 && parallaxMode.floatValue == 4 ? 1 : 0);
    }

    /// \english
    /// <summary>
    /// Ambient occlusion function builder.
    /// </summary>
    /// <param name="ambientOcclusion">Ambient occlusion property.</param>
    /// <param name="ambientOcclusionMap">Ambient occlusion map property.</param>
    /// <param name="ambientOcclusionLevel">Ambient occlusion level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient occlusion.
    /// </summary>
    /// <param name="ambientOcclusion">Propiedad ambient occlusion.</param>
    /// <param name="ambientOcclusionMap">Propiedad mapa de ambient occlusion.</param>
    /// <param name="ambientOcclusionLevel">Nivel del ambient occlusion map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildAmbientOcclusionWithoutKewyword(MaterialProperty ambientOcclusion, MaterialProperty ambientOcclusionMap, MaterialProperty ambientOcclusionLevel, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Ambient Occlusion", "Ambiental occlusion static effect.", ambientOcclusion);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Ambient Occlusion (R)", "Ambient occlusion map. Only uses the R channel.", ambientOcclusionMap, materialEditor, scaleOffset, ambientOcclusion.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Ambient Occlusion Level", "Level of the ambient occlusion map color in relation the source color.", ambientOcclusionLevel, ambientOcclusion.floatValue);
    }

    /// \english
    /// <summary>
    /// Ambient occlusion function builder.
    /// </summary>
    /// <param name="ambientOcclusion">Ambient occlusion property.</param>
    /// <param name="ambientOcclusionMap">Ambient occlusion map property.</param>
    /// <param name="ambientOcclusionLevel">Ambient occlusion level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient occlusion.
    /// </summary>
    /// <param name="ambientOcclusion">Propiedad ambient occlusion.</param>
    /// <param name="ambientOcclusionMap">Propiedad mapa de ambient occlusion.</param>
    /// <param name="ambientOcclusionLevel">Nivel del ambient occlusion map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildAmbientOcclusion(MaterialProperty ambientOcclusion, MaterialProperty ambientOcclusionMap, MaterialProperty ambientOcclusionLevel, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Ambient Occlusion", "Ambiental occlusion static effect.", ambientOcclusion, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Ambient Occlusion Map (R)", "Ambient occlusion map. Only uses the R channel.", ambientOcclusionMap, materialEditor, scaleOffset, ambientOcclusion.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Ambient Occlusion Level", "Level of the ambient occlusion map color in relation the source color.", ambientOcclusionLevel, ambientOcclusion.floatValue);
    }

    /// \english
    /// <summary>
    /// Ambient occlusion function builder header group.
    /// </summary>
    /// <param name="ambientOcclusion">Ambient occlusion property.</param>
    /// <param name="ambientOcclusionMap">Ambient occlusion map property.</param>
    /// <param name="ambientOcclusionLevel">Ambient occlusion level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient occlusion grupo.
    /// </summary>
    /// <param name="ambientOcclusion">Propiedad ambient occlusion.</param>
    /// <param name="ambientOcclusionMap">Propiedad mapa de ambient occlusion.</param>
    /// <param name="ambientOcclusionLevel">Nivel del ambient occlusion map.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildAmbientOcclusion(MaterialProperty ambientOcclusion, MaterialProperty ambientOcclusionMap, MaterialProperty ambientOcclusionLevel, MaterialEditor materialEditor, bool scaleOffset, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Ambient Occlusion", "Ambiental occlusion static effect.", ambientOcclusion, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("Ambient Occlusion Map (R)", "Ambient occlusion map. Only uses the R channel.", ambientOcclusionMap, materialEditor, scaleOffset, ambientOcclusion.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Ambient Occlusion Level", "Level of the ambient occlusion map color in relation the source color.", ambientOcclusionLevel, ambientOcclusion.floatValue);
        }
    }


    /// \english
    /// <summary>
    /// Ambient light function builder.
    /// </summary>
    /// <param name="ambientLight">Ambient light property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient light.
    /// </summary>
    /// <param name="ambientLight">Propiedad ambient light.</param>
    /// \endspanish
    public static void BuildAmbientLightWithoutKewyword(MaterialProperty ambientLight)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Ambient light", "Scene ambient light features.");
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Disable Ambient Light", "If enabled ambient light doesn't affect to the source mesh.", ambientLight);
    }

    /// \english
    /// <summary>
    /// Ambient light function builder.
    /// </summary>
    /// <param name="ambientLight">Ambient light property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient light.
    /// </summary>
    /// <param name="ambientLight">Propiedad ambient light.</param>
    /// \endspanish
    public static void BuildAmbientLight(MaterialProperty ambientLight)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Ambient Light", "Scene ambient light features.");
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Enable Ambient Light", "If enabled ambient light affect to the source mesh.", ambientLight, true);
    }

    /// \english
    /// <summary>
    /// Ambient light function builder header group.
    /// </summary>
    /// <param name="ambientLight">Ambient light property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient light grupo.
    /// </summary>
    /// <param name="ambientLight">Propiedad ambient light.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildAmbientLight(MaterialProperty ambientLight, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Ambient Light", "Scene ambient light features.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Enable Ambient Light", "If enabled ambient light affect to the source mesh.", ambientLight, true);
        }
    }

    /// \english
    /// <summary>
    /// Ambient light function builder.
    /// </summary>
    /// <param name="matCapTexture">MatCap texture property.</param>
    /// <param name="desaturateMatCap">MatCap desaturation property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient light.
    /// </summary>
    /// <param name="matCapTexture">Propiedad la textura del MatCap.</param>
    /// <param name="desaturateMatCap">Propiedad desaturación de MatCap.</param>
    /// \endspanish
    public static void BuildMatCap(MaterialProperty matCapTexture, MaterialProperty desaturateMatCap, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("MatCap", "MatCap features");
        CGFMaterialEditorUtilitiesClass.BuildTexture("MatCap (RGB)", "MatCap texture.", matCapTexture, materialEditor, scaleOffset, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Desaturate MatCap", "Desaturate the MatCap texture.", desaturateMatCap);
    }

    /// \english
    /// <summary>
    /// Ambient light function builder header group.
    /// </summary>
    /// <param name="matCapTexture">MatCap texture property.</param>
    /// <param name="desaturateMatCap">MatCap desaturation property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función ambient light grupo.
    /// </summary>
    /// <param name="matCapTexture">Propiedad la textura del MatCap.</param>
    /// <param name="desaturateMatCap">Propiedad desaturación de MatCap.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildMatCap(MaterialProperty matCapTexture, MaterialProperty desaturateMatCap, MaterialEditor materialEditor, bool scaleOffset, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("MatCap", "MatCap features.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("MatCap (RGB)", "MatCap texture.", matCapTexture, materialEditor, scaleOffset, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Desaturate MatCap", "Desaturate the MatCap texture.", desaturateMatCap);
        }
    }

    // \english
    /// <summary>
    /// Metallic function builder.
    /// </summary>
    /// <param name="metallic">Metallic property.</param>
    /// <param name="metallicMap">Metallic map property.</param>
    /// <param name="metalness">Metalness property.</param>
    /// <param name="glossMode">Gloss mode property.</param>
    /// <param name="smoothnessRoughnessMap">Smoothness roughness Map property.</param>
    /// <param name="glossiness">Glossiness property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función metallic.
    /// </summary>
    /// <param name="metallic">Propiedad metallic.</param>
    /// <param name="metallicMap">Propiedad metallic map.</param>
    /// <param name="metalness">Propiedad metalness.</param>
    /// <param name="glossMode">Propiedad modo de gloss personalizado.</param>
    /// <param name="smoothnessRoughnessMap">Propiedad de mapa de suavidad o rugosidad.</param>
    /// <param name="glossiness">Propiedad de brillo.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildMetallic(MaterialProperty metallic, MaterialProperty metallicMap, MaterialProperty metalness, MaterialProperty glossMode, MaterialProperty smoothnessRoughnessMap, MaterialProperty glossiness, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Metallic", "Metallic look.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Metallic Map (RGB)", "Texture that determines the metallic look of the surface.", metallicMap, materialEditor, false, metallic.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Metalness", "Level of metallic look of the surface.", metalness, metallic.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Gloss Mode", "Use smoothnes or roughnes to determine the glossines of the surface", new string[]{"Smoothnes", "Roughness"}, glossMode, metallic.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Smoothnes or Roughness Map (RGB)", "Texture that determines the smoothnes or roughness of the surface.", smoothnessRoughnessMap, materialEditor, false, metallic.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Glossiness", "Level of smoothnes or roughness of the surface.", glossiness, metallic.floatValue);
    }


    // \english
    /// <summary>
    /// Metallic function builder header group.
    /// </summary>
    /// <param name="metallic">Metallic property.</param>
    /// <param name="metallicMap">Metallic map property.</param>
    /// <param name="metalness">Metalness property.</param>
    /// <param name="glossMode">Gloss mode property.</param>
    /// <param name="smoothnessRoughnessMap">Smoothness roughness Map property.</param>
    /// <param name="glossiness">Glossiness property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función metallic grupo.
    /// </summary>
    /// <param name="metallic">Propiedad metallic.</param>
    /// <param name="metallicMap">Propiedad metallic map.</param>
    /// <param name="metalness">Propiedad metalness.</param>
    /// <param name="glossMode">Propiedad modo de gloss personalizado.</param>
    /// <param name="smoothnessRoughnessMap">Propiedad de mapa de suavidad o rugosidad.</param>
    /// <param name="glossiness">Propiedad de brillo.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildMetallic(MaterialProperty metallic, MaterialProperty metallicMap, MaterialProperty metalness, MaterialProperty glossMode, MaterialProperty smoothnessRoughnessMap, MaterialProperty glossiness, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Metallic", "Metallic look.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildTexture("Metallic Map (RGB)", "Texture that determines the metallic look of the surface.", metallicMap, materialEditor, false, metallic.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Metalness", "Level of metallic look of the surface.", metalness, metallic.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Gloss Mode", "Use smoothnes or roughnes to determine the glossines of the surface", new string[]{"Smoothnes", "Roughness"}, glossMode, metallic.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Smoothnes or Roughness Map (RGB)", "Texture that determines the smoothnes or roughness of the surface.", smoothnessRoughnessMap, materialEditor, false, metallic.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Glossiness", "Level of smoothnes or roughness of the surface.", glossiness, metallic.floatValue);
        }
    }


    // \english
    /// <summary>
    /// Detail function builder.
    /// </summary>
    /// <param name="detailAlbedo">Detail albedo property.</param>
    /// <param name="detailTex">Detail albedo texture property.</param>
    /// <param name="detailNormal">Gloss mode property.</param>
    /// <param name="detailNormalMap">Detail normal map property.</param>
    /// <param name="detailNormalLevel">Smoothness roughness Map property.</param>
    /// <param name="detailMask">Glossiness property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función detail.
    /// </summary>
    /// <param name="detailAlbedo">Propiedad metallic.</param>
    /// <param name="detailTex">Propiedad metallic map.</param>
    /// <param name="detailNormal">Propiedad modo de gloss personalizado.</param>
    /// <param name="detailNormalMap">Propiedad metalness.</param>
    /// <param name="detailNormalLevel">Propiedad de mapa de suavidad o rugosidad.</param>
    /// <param name="detailMask">Propiedad de brillo.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildDetail(MaterialProperty detailAlbedo, MaterialProperty detailTex, MaterialProperty detailNormal, MaterialProperty detailNormalMap, MaterialProperty detailNormalLevel, MaterialProperty detailMask, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Detail", "Normal and albedo details.");
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Detail Albedo", "Albedo details.", detailAlbedo, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Detail Albedo Map (RGBA)", "Texture of the albedo details.", detailTex, materialEditor, true, detailAlbedo.floatValue, compactMode);
        
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Detail Normal", "Normal details.", detailNormal, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Detail Normal Map (RGB)", "Texture of the normal details.", detailNormalMap, materialEditor, false, detailNormal.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Detail Normal Level", "Level of the normal map.", detailNormalLevel, detailNormal.floatValue);

        CGFMaterialEditorUtilitiesClass.BuildTexture("Detail Mask (R)", "Texture that masks the detail maps.", detailMask, materialEditor, false, (detailAlbedo.floatValue == 1 || detailNormal.floatValue == 1) ? 1 : 0, compactMode);
    }


    /// \english
    /// <summary>
    /// Reflection function builder.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="reflectionColor">Reflection color property.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="reflectionCameraFading">Reflection camera fading property.</param>
    /// <param name="reflectionCameraFadingNearPoint">Reflection camera fading near point property.</param>
    /// <param name="reflectionCameraFarNearPoint">Reflection camera far near point property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="showGizmo">Show gizmo.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="reflectionColor">Propiedad color de la reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="reflectionCameraFading">Propiedad desvanecimiento por la cámara.</param>
    /// <param name="reflectionCameraFadingNearPoint">Propiedad punto inicial del desvanecimiento de la reflexión.</param>
    /// <param name="reflectionCameraFarNearPoint">Propiedad punto final del desvanecimiento de la reflexión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="showGizmo">Muestra el gizmo.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildReflection(MaterialProperty reflection, MaterialProperty reflectionColor, MaterialProperty reflectionCustom,  MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialProperty reflectionCameraFading, MaterialProperty reflectionCameraFadingNearPoint, MaterialProperty reflectionCameraFadingFarPoint, MaterialEditor materialEditor, out bool showGizmo, string playerPrefKeyName, bool compactMode = false)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Reflection", "Reflective surface.", reflection, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Reflection Color (RGB)", "Color of the reflective surface.", reflectionColor, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Camera Fading", "Reflection level by camera distance.", reflectionCameraFading, toggleLock: reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Near Point", "Start point of the reflection fading. Offset to position camera.", reflectionCameraFadingNearPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Far Point", "End point of the reflection fading.", reflectionCameraFadingFarPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
        
        showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Reflection Camera Fading Gizmo", "If enabled show reflection camera fading gizmo.", reflection.floatValue, reflection, playerPrefKeyName);
    }


    /// \english
    /// <summary>
    /// Reflection function builder header group.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="reflectionColor">Reflection color property.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="reflectionCameraFading">Reflection camera fading property.</param>
    /// <param name="reflectionCameraFadingNearPoint">Reflection camera fading near point property.</param>
    /// <param name="reflectionCameraFarNearPoint">Reflection camera far near point property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="showGizmo">Show gizmo</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection grupo.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="reflectionColor">Propiedad color de la reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="reflectionCameraFading">Propiedad desvanecimiento por la cámara.</param>
    /// <param name="reflectionCameraFadingNearPoint">Propiedad punto inicial del desvanecimiento de la reflexión.</param>
    /// <param name="reflectionCameraFarNearPoint">Propiedad punto final del desvanecimiento de la reflexión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="showGizmo">Muestra el gizmo.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildReflection(MaterialProperty reflection, MaterialProperty reflectionColor, MaterialProperty reflectionCustom,  MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialProperty reflectionCameraFading, MaterialProperty reflectionCameraFadingNearPoint, MaterialProperty reflectionCameraFadingFarPoint, MaterialEditor materialEditor, out bool showGizmo, bool isUnfold, string playerPrefKeyName, bool compactMode = false)
    {
        // Assignation of the argument with "out" keyword.
        showGizmo = false;

        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Reflection", "Reflective surface.", reflection, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildColor("Reflection Color (RGB)", "Color of the reflective surface.", reflectionColor, reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Camera Fading", "Reflection level by camera distance.", reflectionCameraFading, toggleLock: reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Near Point", "Start point of the reflection fading. Offset to position camera.", reflectionCameraFadingNearPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Far Point", "End point of the reflection fading.", reflectionCameraFadingFarPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
            
            showGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(out showGizmo, "Reflection Camera Fading Gizmo", "If enabled show reflection camera fading gizmo.", reflection.floatValue, reflection, playerPrefKeyName);
        }
    }

    // \english
    /// <summary>
    /// Reflection function builder.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="reflectionColor">Reflection color property.</param>
    /// <param name="reflectionSmooth">Level of smooth of the reflective surface.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="reflectionCameraFading">Reflection camera fading property.</param>
    /// <param name="reflectionCameraFadingNearPoint">Reflection camera fading near point property.</param>
    /// <param name="reflectionCameraFarNearPoint">Reflection camera far near point property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="reflectionColor">Propiedad color de la reflexión.</param>
    /// <param name="reflectionSmooth">Propiedad de nivel de suavidad de la superficie de reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="reflectionCameraFading">Propiedad desvanecimiento por la cámara.</param>
    /// <param name="reflectionCameraFadingNearPoint">Propiedad punto inicial del desvanecimiento de la reflexión.</param>
    /// <param name="reflectionCameraFarNearPoint">Propiedad punto final del desvanecimiento de la reflexión.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildReflectionFull(MaterialProperty reflection, MaterialProperty reflectionColor, MaterialProperty reflectionSmooth, MaterialProperty reflectionCustom, MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialProperty reflectionCameraFading, MaterialProperty reflectionCameraFadingNearPoint, MaterialProperty reflectionCameraFadingFarPoint, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Reflection", "Reflective surface.", reflection, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Reflection Color (RGB)", "Color of the reflective surface.", reflectionColor, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Reflection Smooth", "Level of smooth of the reflective surface.", reflectionSmooth, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Camera Fading", "Reflection level by camera distance.", reflectionCameraFading, toggleLock: reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Near Point", "Start point of the reflection fading. Offset to position camera.", reflectionCameraFadingNearPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Reflection Camera Fading Far Point", "End point of the reflection fading.", reflectionCameraFadingFarPoint, toggleLock: reflectionCameraFading.floatValue * reflection.floatValue);
    }

    /// \english
    /// <summary>
    /// Reflection function builder.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="reflectionColor">Reflection color property.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="reflectionColor">Propiedad color de la reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildReflectionSimple(MaterialProperty reflection, MaterialProperty reflectionColor, MaterialProperty reflectionCustom, MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Reflection", "Reflective surface.", reflection, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Reflection Color (RGB)", "Color of the reflective surface.", reflectionColor, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
    }

    // \english
    /// <summary>
    /// Reflection function builder.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="reflectionColor">Reflection color property.</param>
    /// <param name="specularMask">Specular mask map property.</param>
    /// <param name="shininessLevel">Level of smooth of the reflective surface.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="reflectionColor">Propiedad color de la reflexión.</param>
    /// <param name="specularMask">Propiedad de la textura de la máscara especular.</param>
    /// <param name="shininessLevel">Propiedad de nivel de suavidad de la superficie de reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildReflectionSimpleSpecular(MaterialProperty reflection, MaterialProperty reflectionColor, MaterialProperty specularMask, MaterialProperty shininessLevel, MaterialProperty reflectionCustom, MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Reflection", "Reflective surface.", reflection, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Reflection Color (RGB)", "Color of the reflective surface.", reflectionColor, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Specular Mask (RGB)", "Specular texture.", specularMask, materialEditor, false, reflection.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shininess Level", "Level of blur for the highlight and the relection.", shininessLevel, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, compactMode);
    }

    // \english
    /// <summary>
    /// Reflection function builder.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="shininessLevel">Level of smooth of the reflective surface.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="shininessLevel">Propiedad de nivel de suavidad de la superficie de reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildReflectionFlatLighting(MaterialProperty reflection, MaterialProperty shininessLevel, MaterialProperty reflectionCustom,  MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Reflection", "Reflective surface.", reflection, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shininess Level", "Level of blur for the highlight and the relection.", shininessLevel, reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, true);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, true);
    }

    // \english
    /// <summary>
    /// Reflection function builder header group.
    /// </summary>
    /// <param name="reflection">Reflection property.</param>
    /// <param name="shininessLevel">Level of smooth of the reflective surface.</param>
    /// <param name="reflectionCustom">Reflection custom property.</param>
    /// <param name="reflectionTexture">Reflection texure property.</param>
    /// <param name="reflectionCubemap">Reflection cube map property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función reflection grupo.
    /// </summary>
    /// <param name="reflection">Propiedad reflexión.</param>
    /// <param name="shininessLevel">Propiedad de nivel de suavidad de la superficie de reflexión.</param>
    /// <param name="reflectionCustom">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="reflectionTexture">Propiedad de textura de la reflexión personalizado.</param>
    /// <param name="reflectionCubemap">Propiedad de cubemap de la reflexión personalizado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildReflectionFlatLighting(MaterialProperty reflection, MaterialProperty shininessLevel, MaterialProperty reflectionCustom,  MaterialProperty reflectionTexture, MaterialProperty reflectionCubemap, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroupWithKeyword("Reflection", "Reflective surface.", reflection, true, isUnfold, true, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildSlider("Shininess Level", "Level of blur for the highlight and the relection.", shininessLevel, reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Reflection Custom", "If enabled the shader uses a custom reflections source.", reflectionCustom, toggleLock : reflection.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Texture", "Custom texture for the reflective surface.", reflectionTexture, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, true);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Reflection Cubemap", "Custom cubemap for the reflective surface.", reflectionCubemap, materialEditor, false, reflection.floatValue * reflectionCustom.floatValue, true);
        }
    }

    // \english
    /// <summary>
    /// Refraction function builder.
    /// </summary>
    /// <param name="refraction">Refraction property.</param>
    /// <param name="refractionIndex">Refraction index property</param>
    /// <param name="chromaticDispersion">Chromatic dispersion property.</param>
    /// <param name="chromaticDispersionOffset">Chromatic dispersion offset property.</param>
    /// <param name="chromaticDispersionSmooth">Chromatic dispersion smooth property.</param>
    /// <param name="refractionBlurLevel">Refraction blur leel cube map property.</param>
    /// <param name="glossTranslucency">Gloss translucency property.</param>
    /// <param name="glossTranslucencySamples">Gloss translucency property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función refracción.
    /// </summary>
    /// <param name="refraction">Propiedad refracción.</param>
    /// <param name="refractionIndex">Propiedad de índice de refracción.</param>
    /// <param name="chromaticDispersion">Propiedad de dispersión cromática.</param>
    /// <param name="chromaticDispersionOffset">Propiedad del desplazamiento de la dispersión cromática.</param>
    /// <param name="chromaticDispersionSmooth">Propiedad del suvizado de la dispersión cromática.</param>
    /// <param name="refractionBlurLevel">Propiedad del nivel de desenfoque de la refracción.</param>
    /// <param name="glossTranslucency">Propiedad translucidez a partir del brillo de la superficie.</param>
    /// <param name="glossTranslucencySamples">Propiedad de la calidad de la translucidez a partir del brillo de la superficie.</param>
    /// \endspanish
    public static void BuildRefraction(MaterialProperty refraction, MaterialProperty refractionIndex, MaterialProperty chromaticDispersion, MaterialProperty chromaticDispersionOffset, MaterialProperty chromaticDispersionSmooth, MaterialProperty refractionBlurLevel, MaterialProperty glossTranslucency, MaterialProperty glossTranslucencySamples)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Refraction", "Refractive surface.", refraction, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Refraction Index", "Ratio of indices of refraction at the surface.", refractionIndex, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Chromatic Dispersion", "Dissociation of color channels due the refraction effect.", chromaticDispersion, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Chromatic Dispersion Offset", "Offset of area affected by the chromatic dispersion.", chromaticDispersionOffset, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Chromatic Dispersion Smooth", "Smooth or falloff of edge of the area affected by the chromatic dispersion.", chromaticDispersionSmooth, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Refraction Blur Level", "Intensity of blur in the chromatic dispersion area.", refractionBlurLevel, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Gloss Refraction Translucency", "If enabled, glossiness value of the metallic look controls the translucency of the surface.", glossTranslucency, true, refraction.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gloss Translucency Quality", "Quality of translucency effect.", glossTranslucencySamples, refraction.floatValue * glossTranslucency.floatValue);
    }


    // \english
    /// <summary>
    /// Translucency function builder.
    /// </summary>
    /// <param name="translucency">Translucency property.</param>
    /// <param name="subSurfaceScattering">Sub surface scattering property.</param>
    /// <param name="sSSInternalColor">Internal color property.</param>
    /// <param name="sSSInternalColorLevel">Level of the internal color property.</param>
    /// <param name="sSSOffset">SSS offset property.</param>
    /// <param name="sSSSmooth">SSS smooth property.</param>
    /// <param name="sSSThicknessMap">Thickness map property.</param>
    /// <param name="nonDirectionalLightPenetration">Non directional light penetration property.</param>
    /// <param name="shadowStrength">Shadow strength property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función translucidez.
    /// </summary>
    /// <param name="translucency">Propiedad translucidez.</param>
    /// <param name="subSurfaceScattering">Propiedad de dispersión.</param>
    /// <param name="sSSInternalColor">Propiedad color interno.</param>
    /// <param name="sSSInternalColorLevel">Propiedad nivel del color interno.</param>
    /// <param name="sSSOffset">Propiedad de desplazamiento del SSS.</param>
    /// <param name="sSSSmooth">Propiedad sde suavidad del SSS.</param>
    /// <param name="sSSThicknessMap">Propiedad del mapa de grosor.</param>
    /// <param name="nonDirectionalLightPenetration">Propiedad de penetración de luces no direccionales.</param>
    /// <param name="shadowStrength">Propiedad de nivel de la sombra.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildTranslucency(MaterialProperty translucency, MaterialProperty subSurfaceScattering, MaterialProperty sSSInternalColor, MaterialProperty sSSInternalColorLevel, MaterialProperty sSSOffset, MaterialProperty sSSSmooth, MaterialProperty sSSThicknessMap, MaterialProperty nonDirectionalLightPenetration, MaterialProperty shadowStrength, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Translucency", "Translucency surface, sub surface scattering.", translucency, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Sub Surface Scattering", "Scattering of the light rays when go through the mesh.", subSurfaceScattering, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("SSS Internal Color (RGB)", "Color for the area affected by the translucency, simulates the color of internal part of the mesh.", sSSInternalColor, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("SSS Internal Color Level", "Level of the internal color based on the surface color and light color.", sSSInternalColorLevel, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("SSS Offset", "Offset of area affected by the translucency.", sSSOffset, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("SSS Smooth", "Smooth or falloff of edge of the area affected by the translucency.", sSSSmooth, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("SSS Thickness Map (R)", "Texture that contains the thickness of the mesh.", sSSThicknessMap, materialEditor, true, translucency.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Non Dir Light Penetration", "Penetration intensity through the mesh of point and spot light (non directional lights).", nonDirectionalLightPenetration, translucency.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow Strength", "Diffuse shadow level.", shadowStrength, translucency.floatValue);
    }

    /// \english
    /// <summary>
    /// LOD fade function builder.
    /// </summary>
    /// <param name="lODFadeMode">LOD fade mode property.</param>
    /// <param name="lODDitherType">LOD dither type property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función LOD fade.
    /// </summary>
    /// <param name="lODFadeMode">Propiedad modo del desvanecimiento en de los LOD.</param>
    /// <param name="lODDitherType">Propiedad tipo del dither en de los LOD.</param>
    /// \endspanish
    public static void BuildLODFade(MaterialProperty lODFadeMode, MaterialProperty lODDitherType, MaterialEditor materialEditor)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeaderWithPopup("LOD Fade Mode", "LOD fade mode.", new string[]{"None", "Cross Fade Blending", "Cross Fade Dithering"}, lODFadeMode);
        //CGFMaterialEditorUtilitiesClass.BuildPopup("LOD Dither Type", "LOD dither type.", new string[]{"Unity", "Blue Noise", "Floyd Steinberg"}, lODDitherType, (lODFadeMode.floatValue == 2) ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildPopupFloat("LOD Dither Type", "LOD dither type.", new string[]{"Unity", "Blue Noise", "Floyd Steinberg"}, lODDitherType, (lODFadeMode.floatValue == 2) ? 1 : 0);

    }

    /// \english
    /// <summary>
    /// LOD fade function builder header group.
    /// </summary>
    /// <param name="lODFadeMode">LOD fade mode property.</param>
    /// <param name="lODDitherType">LOD dither type property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función LOD fade grupo.
    /// </summary>
    /// <param name="lODFadeMode">Propiedad modo del desvanecimiento en de los LOD.</param>
    /// <param name="lODDitherType">Propiedad tipo del dither en de los LOD.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildLODFade(MaterialProperty lODFadeMode, MaterialProperty lODDitherType, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("LOD Fade Mode", "LOD fade mode.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildHeaderWithPopup("LOD Fade Mode", "LOD fade mode.", new string[]{"None", "Cross Fade Blending", "Cross Fade Dithering"}, lODFadeMode);
            //CGFMaterialEditorUtilitiesClass.BuildPopup("LOD Dither Type", "LOD dither type.", new string[]{"Unity", "Blue Noise", "Floyd Steinberg"}, lODDitherType, (lODFadeMode.floatValue == 2) ? 1 : 0);
            CGFMaterialEditorUtilitiesClass.BuildPopupFloat("LOD Dither Type", "LOD dither type.", new string[]{"Unity", "Blue Noise", "Floyd Steinberg"}, lODDitherType, (lODFadeMode.floatValue == 2) ? 1 : 0);
        }
    }

    /// \english
    /// <summary>
    /// Back light function builder.
    /// </summary>
    /// <param name="backLight">Backlight property.</param>
    /// <param name="backLightMode">Backlight mode property.</param>
    /// <param name="backLightColor">Backlight color property.</param>
    /// <param name="backLightOffset">Backlight offset property.</param>
    /// <param name="backLightSmooth">Backlight smooth property.</param>
    /// <param name="onlyAgainstLightDirection">If enabled, only applies the back light when the view direction is against the source light direction.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Back light.
    /// </summary>
    /// <param name="backLight">Propiedad del backlight.</param>
    /// <param name="backLightMode">Propiedad modo del backlight.</param>
    /// <param name="backLightColor">Propiedad color del backlight.</param>
    /// <param name="backLightOffset">Propiedad del desplazamiento del backlight.</param>
    /// <param name="backLightSmooth">Propiedad del suavizado del backlight.</param>
    /// <param name="onlyAgainstLightDirection">Si está habilitado, solo se aplica el backlight cuando la dirección de la camára está en contra de la dirección de la fuente de luz.</param>
    /// \endspanish
    public static void BuildBackLight(MaterialProperty backLight, MaterialProperty backLightMode, MaterialProperty backLightColor, MaterialProperty backLightOffset, MaterialProperty backLightSmooth, MaterialProperty onlyAgainstLightDirection)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Back Light", "Back light features.", backLight, true);
        CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Back Light Mode", "Back Light mode.", new string[]{"Only Shadow", "Only Base", "Booth"}, backLightMode, backLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Back Light Color (RGB)", "Color of the back light effect.", backLightColor, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Back Light Offset", "Back light offset across the mehs surface.", backLightOffset, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Light Smooth", "Back light smooth of the edge of the effect.", backLightSmooth, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Only Against Directional Light", "If enabled, only applies the back light when the view direction is against the source light direction.", onlyAgainstLightDirection, toggleLock : backLight.floatValue > 0 ? 1 : 0);
    }

    /// \english
    /// <summary>
    /// Back light function builder without keyword.
    /// </summary>
    /// <param name="backLight">Backlight property.</param>
    /// <param name="backLightMode">Backlight mode property.</param>
    /// <param name="backLightColor">Backlight color property.</param>
    /// <param name="backLightOffset">Backlight offset property.</param>
    /// <param name="backLightSmooth">Backlight smooth property.</param>
    /// <param name="onlyAgainstLightDirection">If enabled, only applies the back light when the view direction is against the source light direction.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Back light sin keyword.
    /// </summary>
    /// <param name="backLight">Propiedad del backlight.</param>
    /// <param name="backLightMode">Propiedad modo del backlight.</param>
    /// <param name="backLightColor">Propiedad color del backlight.</param>
    /// <param name="backLightOffset">Propiedad del desplazamiento del backlight.</param>
    /// <param name="backLightSmooth">Propiedad del suavizado del backlight.</param>
    /// <param name="onlyAgainstLightDirection">Si está habilitado, solo se aplica el backlight cuando la dirección de la camára está en contra de la dirección de la fuente de luz.</param>
    /// \endspanish
    public static void BuildBackLightWithoutKeyword(MaterialProperty backLight, MaterialProperty backLightMode, MaterialProperty backLightColor, MaterialProperty backLightOffset, MaterialProperty backLightSmooth, MaterialProperty onlyAgainstLightDirection)
    {
        //CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Back Light", "Back light features.", backLight, true);
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Back Light", "Back light features.", backLight);
        CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Back Light Mode", "Back Light mode.", new string[]{"Only Shadow", "Only Base", "Booth"}, backLightMode, backLight.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Back Light Color (RGB)", "Color of the back light effect.", backLightColor, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Back Light Offset", "Back light offset across the mehs surface.", backLightOffset, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Light Smooth", "Back light smooth of the edge of the effect.", backLightSmooth, backLight.floatValue > 0 ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Only Against Directional Light", "If enabled, only applies the back light when the view direction is against the source light direction.", onlyAgainstLightDirection, toggleLock : backLight.floatValue > 0 ? 1 : 0);
    }

    /// \english
    /// <summary>
    /// Line art function builder.
    /// </summary>
    /// <param name="lineArtMap">Line art map property.</param>
    /// <param name="lineArtColor">Line art color property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Line art.
    /// </summary>
    /// <param name="lineArtMap">Propiedad de la textura del line art.</param>
    /// <param name="lineArtColor">Propiedad color del line art.</param>
    /// \endspanish
    public static void BuildLineArt(MaterialProperty lineArtMap, MaterialProperty lineArtColor, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Line Art", "Line art features.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Line Art Map (R)", "Line art texture.", lineArtMap, materialEditor, false, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildColor("Line Art Color (RGB)", "Color of the line art.", lineArtColor, lineArtMap.textureValue ? 1 : 0);
    }

    /// \english
    /// <summary>
    /// Outline function builder.
    /// </summary>
    /// <param name="outline">Outline property.</param>
    /// <param name="outlineWidth">Outline width property.</param>
    /// <param name="outlineColor">Outline color property.</param>
    /// <param name="useTextureOpacity">Use texture opacity property.</param>
    /// <param name="dynamicOutlineWidth">Dynamic outline width property.</param>
    /// <param name="dynamicOutlineEnd">Dynamic outline end property.</param>
    /// <param name="dynamicOutlineStart">Dynamic outline start property.</param>
    /// <param name="outlineMinWidth">Outline minimum width property.</param>
    /// <param name="outlineMaxWidth">Outline maximum width property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función outline.
    /// </summary>
    /// <param name="outline">Propiedad outline.</param>
    /// <param name="outlineWidth">Propiedad del ancho del borde.</param>
    /// <param name="outlineColor">Propiedad del color del borde.</param>
    /// <param name="useTextureOpacity">Propiedad del uso de la opacidad de la textura del borde.</param>
    /// <param name="dynamicOutlineWidth">Propiedad del ancho dinámico del borde.</param>
    /// <param name="dynamicOutlineEnd">Propiedad del punto final del ancho dinámico del borde.</param>
    /// <param name="dynamicOutlineStart">Propiedad del punto inicial del ancho dinámico del borde.</param>
    /// <param name="outlineMinWidth">Propiedad del ancho mínimo del borde.</param>
    /// <param name="outlineMaxWidth">Propiedad del ancho máximo del borde.</param>
    /// \endspanish
    public static void BuildOutline(MaterialProperty outline, MaterialProperty outlineWidth, MaterialProperty outlineColor, MaterialProperty useTextureOpacity, MaterialProperty dynamicOutlineWidth, MaterialProperty dynamicOutlineEnd, MaterialProperty dynamicOutlineStart, MaterialProperty outlineMinWidth, MaterialProperty outlineMaxWidth)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Outline", "Outline effect.", outline, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Outline Width", "Width of the outline.", outlineWidth, outline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Outline Color (RGBA)", "Color of the ouline.", outlineColor, outline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Texture Opacity", "Use the opacity of the diffuse map.", useTextureOpacity, toggleLock : outline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Dynamic Outline Width", "toon effect features.", dynamicOutlineWidth, toggleLock : outline.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Dynamic Outline End", "End point of the dynamic outline width feature.", dynamicOutlineEnd, outline.floatValue * dynamicOutlineWidth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Dynamic Outline Start", "Start point of the dynamic outline width feature.", dynamicOutlineStart, outline.floatValue * dynamicOutlineWidth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Outline Min Width", "toon effect features.", outlineMinWidth, outline.floatValue * dynamicOutlineWidth.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Outline Max Width", "toon effect features.", outlineMaxWidth, outline.floatValue * dynamicOutlineWidth.floatValue);
    }

    /// \english
    /// <summary>
    /// Toon function builder.
    /// </summary>
    /// <param name="toonMode">Toon mode property.</param>
    /// <param name="toonRamp">Toon ramp texture property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Toon.
    /// </summary>
    /// <param name="toonMode">Propiedad modo del efecto toon.</param>
    /// <param name="toonRamp">Propiedad de la textura de rampa del effecto toon.</param>
    /// \endspanish
    public static void BuildToon(MaterialProperty toonMode, MaterialProperty toonRamp, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Toon", "toon effect features.");
        CGFMaterialEditorUtilitiesClass.BuildPopup("Toon Mode", "Toon mode.", new string[]{"Dynamic", "Texture Ramp"}, toonMode);
        CGFMaterialEditorUtilitiesClass.BuildTextureVertical("Toon Ramp (RGB)", "Toon texture ramp.", toonRamp, false, materialEditor, toonMode.floatValue > 0 ? 1 : 0);
    }

    /// \english
    /// <summary>
    /// Cell shadow function builder.
    /// </summary>
    /// <param name="shadowOffset">Shadow offset property.</param>
    /// <param name="shadowSmooth">Shadow smooth property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="shadowSteps">Shadow steps property.</param>
    /// <param name="shadowColor">Shadow color property.</param>
    /// <param name="shadowThresholdPattern">Shadow threshold pattern property.</param>
    /// <param name="shadowScreenSpaceTreshold">Screen space threshold property.</param>
    /// <param name="shadowSmoothedTreshold">Smoothed threshold property.</param>
    /// <param name="meshSilhouetteAgainstLightDirection">Mesh silhouette against light direction property.</param>
    /// <param name="meshSilhouetteOffset">Mesh silhouette offset property.</param>
    /// <param name="meshSilhouetteSmooth">Mesh silhouette smooth property.</param>
    /// <param name="meshSilhouetteHardEdge">Mesh silhouette hard edge property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Cell shadow.
    /// </summary>
    /// <param name="shadowOffset">Propiedad del desplazamiento de la sombra.</param>
    /// <param name="shadowSmooth">Propiedad del suavizado la sombra.</param>
    /// <param name="shadowLevel">Propiedad del nivel de la sombra.</param>
    /// <param name="shadowSteps">Propiedad de los pasos de degradado de la sombra.</param>
    /// <param name="shadowColor">Propiedad color de la sombra.</param>
    /// <param name="shadowThresholdPattern">Propiedad de la textura del patrón del umbral de la sombra.</param>
    /// <param name="shadowScreenSpaceTreshold">Propiedad de uso de coordenadas en espacio de pantalla de la textura del patrón del umbral de la sombra.</param>
    /// <param name="shadowSmoothedTreshold">Propiedad del suavidado del umbral de la sombra.</param>
    /// <param name="meshSilhouetteAgainstLightDirection">Propiedad de la silueta del modelo en contra de la dirección de la luz de la sombra.</param>
    /// <param name="meshSilhouetteOffset">Propiedad del desplazamiento de la silueta del modelo de la sombra.</param>
    /// <param name="meshSilhouetteSmooth">Propiedad del suavizado de la silueta del modelo de la sombra.</param>
    /// <param name="meshSilhouetteHardEdge">Propiedad de borde definido de la silueta del modelo de la sombra.</param>
    /// \endspanish
    public static void BuildCellShadow(MaterialProperty shadowOffset, MaterialProperty shadowSmooth, MaterialProperty shadowLevel, MaterialProperty shadowSteps, MaterialProperty shadowColor, MaterialProperty shadowThresholdPattern, MaterialProperty shadowScreenSpaceTreshold, MaterialProperty shadowSmoothedTreshold, MaterialProperty meshSilhouetteAgainstLightDirection, MaterialProperty meshSilhouetteOffset, MaterialProperty meshSilhouetteSmooth, MaterialProperty meshSilhouetteHardEdge, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Shadow", "Shadow across the mesh surface.");
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow Offset", "Shadow offset across the mesh surface.", shadowOffset);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow Smooth", "Level of the smooth of the edge of the shadow.", shadowSmooth);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Shadow Level", "Level of intensity of the shadow.", shadowLevel);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Shadow Steps", "Cell shading steps.", shadowSteps);
        CGFMaterialEditorUtilitiesClass.BuildColor("Shadow Color (RGB)", "Color of the shadow.", shadowColor);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Shadow Threshold Pattern (R)", "Threshold pattern texture applied to the shade.", shadowThresholdPattern, materialEditor, true, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Shadow Screen Space Treshold", "If enabled, the threshold pattern texture use texture coordinates in screen space.", shadowScreenSpaceTreshold, toggleLock: (shadowThresholdPattern.textureValue) ? 1 : 0);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Shadow Smoothed Treshold", "If enabled, applied a smooth offset to the texture coordinates in screen space, improve the threshold pattern effect if the object is in movement.", shadowSmoothedTreshold, toggleLock: shadowScreenSpaceTreshold.floatValue * ((shadowThresholdPattern.textureValue) ? 1 : 0));
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Mesh Silhouette Against Light Direction", "If enabled, show a silhouette mask to the shadow across the mesh surface to show the base color when the view direction is opposite to the light source direction.", meshSilhouetteAgainstLightDirection, true);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Mesh Silhouette Offset", "Mesh silhouette offset across the mesh surface.", meshSilhouetteOffset, meshSilhouetteAgainstLightDirection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Mesh Silhouette Smooth", "Level of the smooth of the edge of the mesh silhouette.", meshSilhouetteSmooth, meshSilhouetteAgainstLightDirection.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Mesh Silhouette Hard Edge", "If enabled, apply a hard endge to the mesh silhouette effect.", meshSilhouetteHardEdge, toggleLock : meshSilhouetteAgainstLightDirection.floatValue);
    }

    /// \english
    /// <summary>
    /// Cell specular function builder.
    /// </summary>
    /// <param name="specular">Specular property.</param>
    /// <param name="specularColor">Specular color property.</param>
    /// <param name="useLightColor">Use light color property.</param>
    /// <param name="specularMap">Specular map property.</param>
    /// <param name="specularOffset">Specular offset property.</param>
    /// <param name="specularSmooth">Specular smooth property.</param>
    /// <param name="specularLevel">Specular level property.</param>
    /// <param name="specularSteps">Specular steps property.</param>
    /// <param name="specularEffect">Specular effect property.</param>
    /// <param name="specularEffectSmooth">Specular effect smooth property.</param>
    /// <param name="specularScreenSpaceEffect">Specular screen space effect property.</param>
    /// <param name="specularSmoothedEffect">Specular smoothed effect property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Cell specular.
    /// </summary>
    /// <param name="specular">Propiedad del brillo especular.</param>
    /// <param name="specularColor">Propiedad del color del brillo especular.</param>
    /// <param name="useLightColor">Propiedad del uso del color de la luz brillo especular.</param>
    /// <param name="specularMap">Propiedad del mapa del brillo especular.</param>
    /// <param name="specularOffset">Propiedad del desplazamiento del brillo especular.</param>
    /// <param name="specularSmooth">Propiedad del suavizado del brillo especular.</param>
    /// <param name="specularLevel">Propiedad del nivel del brillo especular.</param>
    /// <param name="specularSteps">Propiedad de los pasos de degradado del brillo especular.</param>
    /// <param name="specularEffect">Propiedad del efecto del brillo especular.</param>
    /// <param name="specularEffectSmooth">Propiedad del suavizado del efecto brillo especular.</param>
    /// <param name="specularScreenSpaceEffect">Propiedad de uso de coordenadas en espacio de pantalla de la textura del efecto brillo especular.</param>
    /// <param name="specularSmoothedEffect">Propiedad del suavidado del efecto brillo especular.</param>
    /// \endspanish
    public static void BuildCellSpecular(MaterialProperty specular, MaterialProperty specularColor, MaterialProperty useLightColor, MaterialProperty specularMap, MaterialProperty specularOffset, MaterialProperty specularSmooth, MaterialProperty specularLevel, MaterialProperty specularSteps, MaterialProperty specularEffect, MaterialProperty specularEffectSmooth, MaterialProperty specularScreenSpaceEffect, MaterialProperty specularSmoothedEffect, MaterialEditor materialEditor, bool compactMode = false)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeaderWithKeyword("Specular", "Specular light (Blinn-Phong).", specular, true);
        //CGFMaterialEditorUtilitiesClass.BuildHeaderWithToggleFloat("Specular", "Specular light (Blinn-Phong).", specular);
        CGFMaterialEditorUtilitiesClass.BuildColor("Specular Color (RGB)", "Color of the specular light.", specularColor, specular.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Use Light Color", "If enabled, apply the color of the light on the specular light.", useLightColor, toggleLock: specular.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Specular Map (RGB)", "Specular texture.", specularMap, materialEditor, true, specular.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Specular Offset", "Specular light offset across the mesh surface.", specularOffset, specular.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Specular Smooth", "Level of the smooth of the edge of the specular light.", specularSmooth, specular.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Specular Level", "Level of intensity of the specular light.", specularLevel, specular.floatValue);            
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Specular Steps", "Cell specular steps.", specularSteps, specular.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Specular Effect (R)", "Texture applied to the specular light.", specularEffect, materialEditor, true, specular.floatValue, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Specular Effect Smooth", "Level of smooth of the texture applied to the specular light.", specularEffectSmooth, specular.floatValue * ((specularEffect.textureValue) ? 1 : 0));            
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Specular Screen Space Effect", "If enabled, applied a smooth offset to the texture coordinates in screen space, improve the specular effect if the object is in movement.", specularScreenSpaceEffect, toggleLock: specular.floatValue * ((specularEffect.textureValue) ? 1 : 0));
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Specular Smoothed Effect", "Color of the specular light.", specularSmoothedEffect, toggleLock: specular.floatValue * specularScreenSpaceEffect.floatValue * ((specularEffect.textureValue) ? 1 : 0));
    }

    /// \english
    /// <summary>
    /// Stencil options function builder.
    /// </summary>
    /// <param name="stencilReference">Stencil reference property.</param>
    /// <param name="stencilReadMask">Stencil red mask property.</param>
    /// <param name="stencilWriteMask">Stencil write mask property.</param>
    /// <param name="stencilComparisonFunction">Stencil comparison function property.</param>
    /// <param name="stencilPassOperation">Stencil pass operation property.</param>
    /// <param name="stencilFailOperation">Stencil fail operation property.</param>
    /// <param name="stencilZFailOperation">Stencil z fail operation property.</param>
    /// <param name="showStencilMask">Show stencil mask property.</param>
    /// <param name="stencilColorMask">Stencil color mask property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Stencil options.
    /// </summary>
    /// <param name="stencilReference">Propiedad del stencil reference.</param>
    /// <param name="stencilReadMask">Propiedad del stencil read mask.</param>
    /// <param name="stencilWriteMask">Propiedad del stencil write mask.</param>
    /// <param name="stencilComparisonFunction">Propiedad del stencil comparison function.</param>
    /// <param name="stencilPassOperation">Propiedad del stencil pass operation.</param>
    /// <param name="stencilFailOperation">Propiedad del stencil fail operation.</param>
    /// <param name="stencilZFailOperation">Propiedad del stencil z fail operation.</param>
    /// <param name="showStencilMask">Propiedad del show stencil mask.</param>
    /// <param name="stencilColorMask">Propiedad del stencil color mask.</param>
    /// \endspanish
    public static void BuildStencilOptionsMask(MaterialProperty stencilReference, MaterialProperty stencilReadMask, MaterialProperty stencilWriteMask, MaterialProperty stencilComparisonFunction, MaterialProperty stencilPassOperation, MaterialProperty stencilFailOperation, MaterialProperty stencilZFailOperation, MaterialProperty showStencilMask, MaterialProperty stencilColorMask, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Stencil Options", "Stencil Options.");

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Reference", "The value to be compared against (if Comp is anything else than always) and/or the value to be written to the buffer (if either Pass, Fail or ZFail is set to replace). 0–255 integer.", stencilReference);

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Read Mask", "An 8 bit mask as an 0–255 integer, used when comparing the reference value with the contents of the buffer (referenceValue & readMask) comparisonFunction (stencilBufferValue & readMask). Default: 255.", stencilReadMask);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Write Mask", "An 8 bit mask as an 0–255 integer, used when writing to the buffer. Note that, like other write masks, it specifies which bits of stencil buffer will be affected by write (i.e. WriteMask 0 means that no bits are affected and not that 0 will be written). Default: 255.", stencilWriteMask);
        
        CGFMaterialEditorUtilitiesClass.BuildStencilCompareFunction(stencilComparisonFunction, materialEditor);

        CGFMaterialEditorUtilitiesClass.BuildStencilPassOperation(stencilPassOperation, materialEditor);
        CGFMaterialEditorUtilitiesClass.BuildStencilFailOperation(stencilFailOperation, materialEditor);
        CGFMaterialEditorUtilitiesClass.BuildStencilZFailOperation(stencilZFailOperation, materialEditor);

        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Show Stencil Mask", "Should the mesh of the masking object be drawn with alpha? Set color channel writing mask. Writing ColorMask 0 turns off rendering to all color channels. Default mode is writing to all channels (RGBA), but for some special effects you might want to leave certain channels unmodified, or disable color writes completely.", showStencilMask, 15.0f);
        CGFMaterialEditorUtilitiesClass.BuildColor("Stencil Color Mask (RGBA)", "Stencil Color Mask.", stencilColorMask, (showStencilMask.floatValue > 0 ? 1 : 0));
    }

    /// \english
    /// <summary>
    /// Stencil options function builder.
    /// </summary>
    /// <param name="stencilReference">Stencil reference property.</param>
    /// <param name="stencilReadMask">Stencil red mask property.</param>
    /// <param name="stencilWriteMask">Stencil write mask property.</param>
    /// <param name="stencilComparisonFunction">Stencil comparison function property.</param>
    /// <param name="stencilPassOperation">Stencil pass operation property.</param>
    /// <param name="stencilFailOperation">Stencil fail operation property.</param>
    /// <param name="stencilZFailOperation">Stencil z fail operation property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Stencil options.
    /// </summary>
    /// <param name="stencilReference">Propiedad del stencil reference.</param>
    /// <param name="stencilReadMask">Propiedad del stencil read mask.</param>
    /// <param name="stencilWriteMask">Propiedad del stencil write mask.</param>
    /// <param name="stencilComparisonFunction">Propiedad del stencil comparison function.</param>
    /// <param name="stencilPassOperation">Propiedad del stencil pass operation.</param>
    /// <param name="stencilFailOperation">Propiedad del stencil fail operation.</param>
    /// <param name="stencilZFailOperation">Propiedad del stencil z fail operation.</param>
    /// \endspanish
    public static void BuildStencilOptions(MaterialProperty stencilReference, MaterialProperty stencilReadMask, MaterialProperty stencilWriteMask, MaterialProperty stencilComparisonFunction, MaterialProperty stencilPassOperation, MaterialProperty stencilFailOperation, MaterialProperty stencilZFailOperation, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Stencil Options", "Stencil Options.");

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Reference", "The value to be compared against (if Comp is anything else than always) and/or the value to be written to the buffer (if either Pass, Fail or ZFail is set to replace). 0–255 integer.", stencilReference);

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Read Mask", "An 8 bit mask as an 0–255 integer, used when comparing the reference value with the contents of the buffer (referenceValue & readMask) comparisonFunction (stencilBufferValue & readMask). Default: 255.", stencilReadMask);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Write Mask", "An 8 bit mask as an 0–255 integer, used when writing to the buffer. Note that, like other write masks, it specifies which bits of stencil buffer will be affected by write (i.e. WriteMask 0 means that no bits are affected and not that 0 will be written). Default: 255.", stencilWriteMask);
        
        CGFMaterialEditorUtilitiesClass.BuildStencilCompareFunction(stencilComparisonFunction, materialEditor);

        CGFMaterialEditorUtilitiesClass.BuildStencilPassOperation(stencilPassOperation, materialEditor);
        CGFMaterialEditorUtilitiesClass.BuildStencilFailOperation(stencilFailOperation, materialEditor);
        CGFMaterialEditorUtilitiesClass.BuildStencilZFailOperation(stencilZFailOperation, materialEditor);
    }

    /// \english
    /// <summary>
    /// Stencil options function builder header group.
    /// </summary>
    /// <param name="stencilReference">Stencil reference property.</param>
    /// <param name="stencilReadMask">Stencil red mask property.</param>
    /// <param name="stencilWriteMask">Stencil write mask property.</param>
    /// <param name="stencilComparisonFunction">Stencil comparison function property.</param>
    /// <param name="stencilPassOperation">Stencil pass operation property.</param>
    /// <param name="stencilFailOperation">Stencil fail operation property.</param>
    /// <param name="stencilZFailOperation">Stencil z fail operation property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Stencil options grupo.
    /// </summary>
    /// <param name="stencilReference">Propiedad del stencil reference.</param>
    /// <param name="stencilReadMask">Propiedad del stencil read mask.</param>
    /// <param name="stencilWriteMask">Propiedad del stencil write mask.</param>
    /// <param name="stencilComparisonFunction">Propiedad del stencil comparison function.</param>
    /// <param name="stencilPassOperation">Propiedad del stencil pass operation.</param>
    /// <param name="stencilFailOperation">Propiedad del stencil fail operation.</param>
    /// <param name="stencilZFailOperation">Propiedad del stencil z fail operation.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildStencilOptions(MaterialProperty stencilReference, MaterialProperty stencilReadMask, MaterialProperty stencilWriteMask, MaterialProperty stencilComparisonFunction, MaterialProperty stencilPassOperation, MaterialProperty stencilFailOperation, MaterialProperty stencilZFailOperation, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Stencil Options", "Stencil options.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Reference", "The value to be compared against (if Comp is anything else than always) and/or the value to be written to the buffer (if either Pass, Fail or ZFail is set to replace). 0–255 integer.", stencilReference);

            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Read Mask", "An 8 bit mask as an 0–255 integer, used when comparing the reference value with the contents of the buffer (referenceValue & readMask) comparisonFunction (stencilBufferValue & readMask). Default: 255.", stencilReadMask);
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Write Mask", "An 8 bit mask as an 0–255 integer, used when writing to the buffer. Note that, like other write masks, it specifies which bits of stencil buffer will be affected by write (i.e. WriteMask 0 means that no bits are affected and not that 0 will be written). Default: 255.", stencilWriteMask);
            
            CGFMaterialEditorUtilitiesClass.BuildStencilCompareFunction(stencilComparisonFunction, materialEditor);

            CGFMaterialEditorUtilitiesClass.BuildStencilPassOperation(stencilPassOperation, materialEditor);
            CGFMaterialEditorUtilitiesClass.BuildStencilFailOperation(stencilFailOperation, materialEditor);
            CGFMaterialEditorUtilitiesClass.BuildStencilZFailOperation(stencilZFailOperation, materialEditor);
        }
    }

    /// \english
    /// <summary>
    /// Stencil options function builder.
    /// </summary>
    /// <param name="stencilReference">Stencil reference property.</param>
    /// <param name="stencilReadMask">Stencil red mask property.</param>
    /// <param name="stencilWriteMask">Stencil write mask property.</param>
    /// <param name="stencilComparisonFunction">Stencil comparison function property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Stencil options.
    /// </summary>
    /// <param name="stencilReference">Propiedad del stencil reference.</param>
    /// <param name="stencilReadMask">Propiedad del stencil read mask.</param>
    /// <param name="stencilWriteMask">Propiedad del stencil write mask.</param>
    /// <param name="stencilComparisonFunction">Propiedad del stencil comparison function.</param>
    /// \endspanish
    public static void BuildStencilOptionsSimple(MaterialProperty stencilReference, MaterialProperty stencilReadMask, MaterialProperty stencilWriteMask, MaterialProperty stencilComparisonFunction, MaterialEditor materialEditor)
    {
        CGFMaterialEditorUtilitiesClass.BuildHeader("Stencil Options", "Stencil Options.");

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Reference", "The value to be compared against (if Comp is anything else than always) and/or the value to be written to the buffer (if either Pass, Fail or ZFail is set to replace). 0–255 integer.", stencilReference);

        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Read Mask", "An 8 bit mask as an 0–255 integer, used when comparing the reference value with the contents of the buffer (referenceValue & readMask) comparisonFunction (stencilBufferValue & readMask). Default: 255.", stencilReadMask);
        CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Write Mask", "An 8 bit mask as an 0–255 integer, used when writing to the buffer. Note that, like other write masks, it specifies which bits of stencil buffer will be affected by write (i.e. WriteMask 0 means that no bits are affected and not that 0 will be written). Default: 255.", stencilWriteMask);
        
        CGFMaterialEditorUtilitiesClass.BuildStencilCompareFunction(stencilComparisonFunction, materialEditor);
    }

    /// \english
    /// <summary>
    /// Stencil options function builder header group.
    /// </summary>
    /// <param name="stencilReference">Stencil reference property.</param>
    /// <param name="stencilReadMask">Stencil red mask property.</param>
    /// <param name="stencilWriteMask">Stencil write mask property.</param>
    /// <param name="stencilComparisonFunction">Stencil comparison function property.</param>
    /// <param name="isUnfold">Manage de foldout status.</param>
	/// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Stencil options grupo.
    /// </summary>
    /// <param name="stencilReference">Propiedad del stencil reference.</param>
    /// <param name="stencilReadMask">Propiedad del stencil read mask.</param>
    /// <param name="stencilWriteMask">Propiedad del stencil write mask.</param>
    /// <param name="stencilComparisonFunction">Propiedad del stencil comparison function.</param>
    /// <param name="isUnfold">Controla el estado del foldout.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// \endspanish
    public static void BuildStencilOptionsSimple(MaterialProperty stencilReference, MaterialProperty stencilReadMask, MaterialProperty stencilWriteMask, MaterialProperty stencilComparisonFunction, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
    {
        isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Stencil Options", "Stencil Options.", true, isUnfold, playerPrefKeyName);

        if (isUnfold)
        {
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Reference", "The value to be compared against (if Comp is anything else than always) and/or the value to be written to the buffer (if either Pass, Fail or ZFail is set to replace). 0–255 integer.", stencilReference);

            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Read Mask", "An 8 bit mask as an 0–255 integer, used when comparing the reference value with the contents of the buffer (referenceValue & readMask) comparisonFunction (stencilBufferValue & readMask). Default: 255.", stencilReadMask);
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Stencil Write Mask", "An 8 bit mask as an 0–255 integer, used when writing to the buffer. Note that, like other write masks, it specifies which bits of stencil buffer will be affected by write (i.e. WriteMask 0 means that no bits are affected and not that 0 will be written). Default: 255.", stencilWriteMask);
            
            CGFMaterialEditorUtilitiesClass.BuildStencilCompareFunction(stencilComparisonFunction, materialEditor);
        }
    }

    /// \english
    /// <summary>
    /// Retro 3D function builder with lighting model.
    /// </summary>
    /// <param name="vertexSnappingLevel">Amount of the vertex inaccuracy position.</param>
    /// <param name="affineTextureMappingLevel">Amount of the perspective texture correction inaccuracy.</param>
    /// <param name="zBufferLackingLevel">Polygons that are supposed to be in the back popping to the front, and vice versa.</param>
    /// <param name="textureLOD">If enabled, a LOD texture.</param>
    /// <param name="textureLODMap">Texture of the texture LOD.</param>
    /// <param name="cameraDistanceTextureLOD">Activation distance of the texture LOD based on the camera position.</param>
    /// <param name="vertexClipping">If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.</param>
    /// <param name="cameraDistanceVertexClip">Vertex discard distance based on the camera position.</param>
    /// <param name="frustrumVertexClipping">If enabled, shaders discard vertices that are outside of the frustrum camera.</param>
    /// <param name="lightingModel">Determines whether to use Gouraud or Flat shading.</param>
    /// <param name="whiteSpecular">If enabled, the color of the direct specular light is always white.</param>
    /// <param name="saturationLevel">Enhances the darkest part of color of the surface.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Retro3D con modelo de iluminación.
    /// </summary>
    /// <param name="vertexSnappingLevel">Cantidad de la imprecisión en la posición del vértice.</param>
    /// <param name="affineTextureMappingLevel">Cantidad de la imprecisión de la corrección de la perspectiva de la textura.</param>
    /// <param name="zBufferLackingLevel">Los polígonos que se supone que están en la parte posterior aparecen al frente, y viceversa.</param>
    /// <param name="textureLOD">Si está habilitado, se establece un LOD en la textura.</param>
    /// <param name="textureLODMap">Textura para el LOD de textura.</param>
    /// <param name="cameraDistanceTextureLOD">Distancia de activación del LOD de textura</param>
    /// <param name="vertexClipping">Si está habilitado, el shader descarta los vértices según la propiedad Camera Distance Vertex Clip.</param>
    /// <param name="cameraDistanceVertexClip">Distancia de descarte de vértice según la posición de la cámara.</param>
    /// <param name="frustrumVertexClipping">Si está habilitado, el shader descarta los vértices que están fuera del frustrum de la cámara.</param>
    /// <param name="lightingModel">Determina si usa el sombreado de Gouraud o plano.</param>
    /// <param name="whiteSpecular">Si está habilitado, el color de la luz especular directa es siempre blanco.</param>
    /// <param name="saturationLevel">Enfatiza la parte más oscura del color de la superficie.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildRetro3DLit(MaterialProperty vertexSnappingLevel, MaterialProperty affineTextureMappingLevel, MaterialProperty zBufferLackingLevel, MaterialProperty textureLOD, MaterialProperty textureLODMap, MaterialProperty cameraDistanceTextureLOD, MaterialProperty vertexClipping, MaterialProperty cameraDistanceVertexClip, MaterialProperty frustrumVertexClipping, MaterialProperty lightingModel, MaterialProperty whiteSpecular, MaterialProperty saturationLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
            CGFMaterialEditorUtilitiesClass.BuildHeader("Retro 3D", "Retro 3D features.");
            CGFMaterialEditorUtilitiesClass.BuildHeader("Deffects", "Deffects");
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Vertex Snapping Level", "Amount of the vertex inaccuracy position.", vertexSnappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Affine Texturing Mapping Level", "Amount of the perspective texture correction inaccuracy.", affineTextureMappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Z-Buffer Lacking Level", "Polygons that are supposed to be in the back popping to the front, and vice versa.", zBufferLackingLevel);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Texture LOD", "Texture LOD");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Texture LOD", "If enabled, a LOD texture.", textureLOD, true);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Texture LOD Map " + CheckRenderMode(renderMode), "Texture of the texture LOD.", textureLODMap, materialEditor, false,  textureLOD.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Texture LOD", "Activation distance of the texture LOD based on the camera position.", cameraDistanceTextureLOD, textureLOD.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Vertex Clipping", "Vertex Clipping");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Vertex Clipping", "If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.", vertexClipping, true);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Vertex Clip", "Vertex discard distance based on the camera position.", cameraDistanceVertexClip, vertexClipping.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Frustrum Vertex Clipping", "If enabled, shaders discard vertices that are outside of the frustrum camera.", frustrumVertexClipping, toggleLock : vertexClipping.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Lighting Model", "Lighting Model");
            CGFMaterialEditorUtilitiesClass.BuildPopup("Lighting Model", "Determines whether to use Gouraud or Flat shading.", new string[]{"Gouraud", "Flat", "Phong"}, lightingModel);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("White Specular", "If enabled, the color of the direct specular light is always white.", whiteSpecular);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Saturation Enhancement", "Enhances the darkest part of color of the surface.", saturationLevel);
    }

    /// \english
    /// <summary>
    /// Retro 3D function builder without lighting model.
    /// </summary>
    /// <param name="vertexSnappingLevel">Amount of the vertex inaccuracy position.</param>
    /// <param name="affineTextureMappingLevel">Amount of the perspective texture correction inaccuracy.</param>
    /// <param name="zBufferLackingLevel">Polygons that are supposed to be in the back popping to the front, and vice versa.</param>
    /// <param name="textureLOD">If enabled, a LOD texture.</param>
    /// <param name="textureLODMap">Texture of the texture LOD.</param>
    /// <param name="cameraDistanceTextureLOD">Activation distance of the texture LOD based on the camera position.</param>
    /// <param name="vertexClipping">If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.</param>
    /// <param name="cameraDistanceVertexClip">Vertex discard distance based on the camera position.</param>
    /// <param name="frustrumVertexClipping">If enabled, shaders discard vertices that are outside of the frustrum camera.</param>
    /// <param name="saturationLevel">Enhances the darkest part of color of the surface.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Retro3D sin modelo de iluminación.
    /// </summary>
    /// <param name="vertexSnappingLevel">Cantidad de la imprecisión en la posición del vértice.</param>
    /// <param name="affineTextureMappingLevel">Cantidad de la imprecisión de la corrección de la perspectiva de la textura.</param>
    /// <param name="zBufferLackingLevel">Los polígonos que se supone que están en la parte posterior aparecen al frente, y viceversa.</param>
    /// <param name="textureLOD">Si está habilitado, se establece un LOD en la textura.</param>
    /// <param name="textureLODMap">Textura para el LOD de textura.</param>
    /// <param name="cameraDistanceTextureLOD">Distancia de activación del LOD de textura</param>
    /// <param name="vertexClipping">Si está habilitado, el shader descarta los vértices según la propiedad Camera Distance Vertex Clip.</param>
    /// <param name="cameraDistanceVertexClip">Distancia de descarte de vértice según la posición de la cámara.</param>
    /// <param name="frustrumVertexClipping">Si está habilitado, el shader descarta los vértices que están fuera del frustrum de la cámara.</param>
    /// <param name="saturationLevel">Enfatiza la parte más oscura del color de la superficie.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildRetro3DUnlit(MaterialProperty vertexSnappingLevel, MaterialProperty affineTextureMappingLevel, MaterialProperty zBufferLackingLevel, MaterialProperty textureLOD, MaterialProperty textureLODMap, MaterialProperty cameraDistanceTextureLOD, MaterialProperty vertexClipping, MaterialProperty cameraDistanceVertexClip, MaterialProperty frustrumVertexClipping, MaterialProperty saturationLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {

            CGFMaterialEditorUtilitiesClass.BuildHeader("Retro 3D", "Retro 3D features.");
            CGFMaterialEditorUtilitiesClass.BuildHeader("Deffects", "Deffects");
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Vertex Snapping Level", "Amount of the vertex inaccuracy position.", vertexSnappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Affine Texturing Mapping Level", "Amount of the perspective texture correction inaccuracy.", affineTextureMappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Z-Buffer Lacking Level", "Polygons that are supposed to be in the back popping to the front, and vice versa.", zBufferLackingLevel);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Texture LOD", "Texture LOD");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Texture LOD", "If enabled, a LOD texture.", textureLOD, true);
            CGFMaterialEditorUtilitiesClass.BuildTexture("Texture LOD Map " + CheckRenderMode(renderMode), "Texture of the texture LOD.", textureLODMap, materialEditor, false,  textureLOD.floatValue, compactMode);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Texture LOD", "Activation distance of the texture LOD based on the camera position.", cameraDistanceTextureLOD, textureLOD.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Vertex Clipping", "Vertex Clipping");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Vertex Clipping", "If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.", vertexClipping, true);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Vertex Clip", "Vertex discard distance based on the camera position.", cameraDistanceVertexClip, vertexClipping.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Frustrum Vertex Clipping", "If enabled, shaders discard vertices that are outside of the frustrum camera.", frustrumVertexClipping, toggleLock : vertexClipping.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Color Adjustment", "Color Adjustment");
            CGFMaterialEditorUtilitiesClass.BuildSlider("Saturation Enhancement", "Enhances the darkest part of color of the surface.", saturationLevel);
    }


    /// \english
    /// <summary>
    /// Retro 3D function builder with lighting model.
    /// </summary>
    /// <param name="vertexSnappingLevel">Amount of the vertex inaccuracy position.</param>
    /// <param name="affineTextureMappingLevel">Amount of the perspective texture correction inaccuracy.</param>
    /// <param name="zBufferLackingLevel">Polygons that are supposed to be in the back popping to the front, and vice versa.</param>
    /// <param name="vertexClipping">If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.</param>
    /// <param name="cameraDistanceVertexClip">Vertex discard distance based on the camera position.</param>
    /// <param name="frustrumVertexClipping">If enabled, shaders discard vertices that are outside of the frustrum camera.</param>
    /// <param name="lightingModel">Determines whether to use Gouraud or Flat shading.</param>
    /// <param name="whiteSpecular">If enabled, the color of the direct specular light is always white.</param>
    /// <param name="saturationLevel">Enhances the darkest part of color of the surface.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Retro3D con modelo de iluminación.
    /// </summary>
    /// <param name="vertexSnappingLevel">Cantidad de la imprecisión en la posición del vértice.</param>
    /// <param name="affineTextureMappingLevel">Cantidad de la imprecisión de la corrección de la perspectiva de la textura.</param>
    /// <param name="zBufferLackingLevel">Los polígonos que se supone que están en la parte posterior aparecen al frente, y viceversa.</param>
    /// <param name="vertexClipping">Si está habilitado, el shader descarta los vértices según la propiedad Camera Distance Vertex Clip.</param>
    /// <param name="cameraDistanceVertexClip">Distancia de descarte de vértice según la posición de la cámara.</param>
    /// <param name="frustrumVertexClipping">Si está habilitado, el shader descarta los vértices que están fuera del frustrum de la cámara.</param>
    /// <param name="lightingModel">Determina si usa el sombreado de Gouraud o plano.</param>
    /// <param name="whiteSpecular">Si está habilitado, el color de la luz especular directa es siempre blanco.</param>
    /// <param name="saturationLevel">Enfatiza la parte más oscura del color de la superficie.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildRetro3DLit3DText(MaterialProperty vertexSnappingLevel, MaterialProperty affineTextureMappingLevel, MaterialProperty zBufferLackingLevel, MaterialProperty vertexClipping, MaterialProperty cameraDistanceVertexClip, MaterialProperty frustrumVertexClipping, MaterialProperty lightingModel, MaterialProperty whiteSpecular, MaterialProperty saturationLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {
            CGFMaterialEditorUtilitiesClass.BuildHeader("Retro 3D", "Retro 3D features.");
            CGFMaterialEditorUtilitiesClass.BuildHeader("Deffects", "Deffects");
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Vertex Snapping Level", "Amount of the vertex inaccuracy position.", vertexSnappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Affine Texturing Mapping Level", "Amount of the perspective texture correction inaccuracy.", affineTextureMappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Z-Buffer Lacking Level", "Polygons that are supposed to be in the back popping to the front, and vice versa.", zBufferLackingLevel);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Vertex Clipping", "Vertex Clipping");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Vertex Clipping", "If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.", vertexClipping, true);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Vertex Clip", "Vertex discard distance based on the camera position.", cameraDistanceVertexClip, vertexClipping.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Frustrum Vertex Clipping", "If enabled, shaders discard vertices that are outside of the frustrum camera.", frustrumVertexClipping, toggleLock : vertexClipping.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Lighting Model", "Lighting Model");
            CGFMaterialEditorUtilitiesClass.BuildPopup("Lighting Model", "Determines whether to use Gouraud or Flat shading.", new string[]{"Gouraud", "Flat", "Phong"}, lightingModel);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("White Specular", "If enabled, the color of the direct specular light is always white.", whiteSpecular);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Saturation Enhancement", "Enhances the darkest part of color of the surface.", saturationLevel);
    }

    /// \english
    /// <summary>
    /// Retro 3D function builder without lighting model.
    /// </summary>
    /// <param name="vertexSnappingLevel">Amount of the vertex inaccuracy position.</param>
    /// <param name="affineTextureMappingLevel">Amount of the perspective texture correction inaccuracy.</param>
    /// <param name="zBufferLackingLevel">Polygons that are supposed to be in the back popping to the front, and vice versa.</param>
    /// <param name="vertexClipping">If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.</param>
    /// <param name="cameraDistanceVertexClip">Vertex discard distance based on the camera position.</param>
    /// <param name="frustrumVertexClipping">If enabled, shaders discard vertices that are outside of the frustrum camera.</param>
    /// <param name="saturationLevel">Enhances the darkest part of color of the surface.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función Retro3D sin modelo de iluminación.
    /// </summary>
    /// <param name="vertexSnappingLevel">Cantidad de la imprecisión en la posición del vértice.</param>
    /// <param name="affineTextureMappingLevel">Cantidad de la imprecisión de la corrección de la perspectiva de la textura.</param>
    /// <param name="zBufferLackingLevel">Los polígonos que se supone que están en la parte posterior aparecen al frente, y viceversa.</param>
    /// <param name="vertexClipping">Si está habilitado, el shader descarta los vértices según la propiedad Camera Distance Vertex Clip.</param>
    /// <param name="cameraDistanceVertexClip">Distancia de descarte de vértice según la posición de la cámara.</param>
    /// <param name="frustrumVertexClipping">Si está habilitado, el shader descarta los vértices que están fuera del frustrum de la cámara.</param>
    /// <param name="saturationLevel">Enfatiza la parte más oscura del color de la superficie.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildRetro3DUnlit3DText(MaterialProperty vertexSnappingLevel, MaterialProperty affineTextureMappingLevel, MaterialProperty zBufferLackingLevel, MaterialProperty vertexClipping, MaterialProperty cameraDistanceVertexClip, MaterialProperty frustrumVertexClipping, MaterialProperty saturationLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {

            CGFMaterialEditorUtilitiesClass.BuildHeader("Retro 3D", "Retro 3D features.");
            CGFMaterialEditorUtilitiesClass.BuildHeader("Deffects", "Deffects");
            CGFMaterialEditorUtilitiesClass.BuildSliderRound("Vertex Snapping Level", "Amount of the vertex inaccuracy position.", vertexSnappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Affine Texturing Mapping Level", "Amount of the perspective texture correction inaccuracy.", affineTextureMappingLevel);
            CGFMaterialEditorUtilitiesClass.BuildSlider("Z-Buffer Lacking Level", "Polygons that are supposed to be in the back popping to the front, and vice versa.", zBufferLackingLevel);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Vertex Clipping", "Vertex Clipping");
            CGFMaterialEditorUtilitiesClass.BuildKeyword("Vertex Clipping", "If enabled, shader discards vertices based on the Camera Distance Vertex Clip property.", vertexClipping, true);
            CGFMaterialEditorUtilitiesClass.BuildFloatPositive("Camera Distance Vertex Clip", "Vertex discard distance based on the camera position.", cameraDistanceVertexClip, vertexClipping.floatValue);
            CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Frustrum Vertex Clipping", "If enabled, shaders discard vertices that are outside of the frustrum camera.", frustrumVertexClipping, toggleLock : vertexClipping.floatValue);
            GUILayout.Space(10);
            CGFMaterialEditorUtilitiesClass.BuildHeader("Color Adjustment", "Color Adjustment");
            CGFMaterialEditorUtilitiesClass.BuildSlider("Saturation Enhancement", "Enhances the darkest part of color of the surface.", saturationLevel);
    }

    /// \english
    /// <summary>
    /// Effect mask function builder.
    /// </summary>
    /// <param name="effectMask">Reflection property.</param>
    /// <param name="effectLevel">Reflection color property.</param>
    /// <param name="fitMaskToScreen">Reflection custom property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función effect mask.
    /// </summary>
    /// <param name="effectMask">Propiedad reflexión.</param>
    /// <param name="effectLevel">Propiedad color de la reflexión.</param>
    /// <param name="fitMaskToScreen">Propiedad uso de la reflexión personalizado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildEffectMask(MaterialProperty effectMask, MaterialProperty effectLevel, MaterialProperty fitMaskToScreen, MaterialEditor materialEditor, bool compactMode = false)
    {

        CGFMaterialEditorUtilitiesClass.BuildHeader("Effect Mask", "Effect masking.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Effect Mask (RGBA)", "Color of the efect mask.", effectMask, materialEditor, true, compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Effect Level", "Level of effect in relation the source color.", effectLevel);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Fit Mask To Screen", "If enabled, fits the mask texture to screen size.", fitMaskToScreen);
    
    }

    /// \english
    /// <summary>
    /// Build of the toggle to manage visibility of a gizmo.
    /// </summary>
    /// <param name="enable">Initial status.</param>
    /// <param name="text">Property text.</param>
    /// <param name="description">Property description.</param>
    /// <param name="propertyGizmo">Property that locks the property.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <returns>Compact mode status.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor del toggle que gestiona la visibilidad de un gizmo.
    /// </summary>
    /// <param name="enable">Estado inicial.</param>
    /// <param name="text">Texto de la propiedad.</param>
    /// <param name="description">Descripción de la propiedad.</param>
    /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <returns>Estado del modo compacto.</returns>
    /// \endspanish
    public static bool BuildShowGizmo(bool enable, string text, string description, MaterialProperty propertyGizmo, string playerPrefKeyName)
    {
        // Assignation of the argument with "out" keyword.
        enable = false;
        
        string uniqueIdentifier = Regex.Replace(text, @"\s+", String.Empty);

        if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
        {
            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (enable ? 1 : 0));
            PlayerPrefs.Save();
        }
        
        enable = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);

        bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);

        if (enable != showGizmo)
        {
            if (propertyGizmo.floatValue == 0)
            {
                propertyGizmo.floatValue = 1;

                propertyGizmo.floatValue = 0;
            }
            else
            {
                propertyGizmo.floatValue = 0;

                propertyGizmo.floatValue = 1;
            }
        }

        PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (showGizmo ? 1 : 0));
        PlayerPrefs.Save();


        GUILayout.Space(25);

        return showGizmo;
    }

    /// \english
    /// <summary>
    /// Build of the toggle to manage visibility of a gizmo.
    /// </summary>
    /// <param name="enable">Initial status.</param>
    /// <param name="text">Property text.</param>
    /// <param name="description">Property description.</param>
    /// <param name="toggleLock">Boolean that locks the property.</param>
    /// <param name="propertyGizmo">Property that locks the property.</param>
    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
    /// <returns>Compact mode status.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor del toggle que gestiona la visibilidad de un gizmo.
    /// </summary>
    /// <param name="enable">Estado inicial.</param>
    /// <param name="text">Texto de la propiedad.</param>
    /// <param name="description">Descripción de la propiedad.</param>
    /// <param name="toggleLock">Float que bloquea la propiedad.</param>
    /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
    /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
    /// <returns>Estado del modo compacto.</returns>
    /// \endspanish
    public static bool BuildShowGizmo(out bool enable, string text, string description, float toggleLock, MaterialProperty propertyGizmo, string playerPrefKeyName)
    {
        // Assignation of the argument with "out" keyword.
        enable = false;

        string uniqueIdentifier = Regex.Replace(text, @"\s+", String.Empty);

        if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
        {
            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (enable ? 1 : 0));
            PlayerPrefs.Save();
        }
        
        enable = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);

        
        if (toggleLock == 1)
        {

            GUI.enabled = true;

        }
        else
        {

            GUI.enabled = false;

        }

        bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);


        if (enable != showGizmo)
        {

            if (propertyGizmo.floatValue == 0)
            {

                propertyGizmo.floatValue = 1;

                propertyGizmo.floatValue = 0;

            }
            else
            {

                propertyGizmo.floatValue = 0;

                propertyGizmo.floatValue = 1;

            }

        }

        GUI.enabled = true;


        PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (showGizmo ? 1 : 0));
        PlayerPrefs.Save();


        return showGizmo;

    }

    /// \english
    /// <summary>
    /// Draw a height fog position handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="startPosition">Handle position.</param>
    /// <param name="height">Handle position of height.</param>
    /// <param name="localHeightFog">Show the handles of local height fog.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador de posición de la niebla por altura.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="startPosition">Posición del controlador.</param>
    /// <param name="height">Posición del controlador de la altura.</param>
    /// <param name="localHeightFog">Muestra los controladores de la niebla local por altura.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawHeightFogArrowHandle(MaterialProperty enableProperty, MaterialProperty startPosition, MaterialProperty height, MaterialProperty localHeightFog, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();
        {

            Vector3 startPositionHandlePosition = Vector3.zero;

            Vector3 heightHandlePosition;

            if (Selection.activeTransform != null)
            {

                Vector3 activeTransformPosition = Selection.activeTransform.position;

                Vector3 activeTransformLocalScale = Selection.activeTransform.localScale;

                if (showHandle & enableProperty.floatValue == 1)
                {

                    if (localHeightFog.floatValue == 1)
                    {

                        float localStartPosition = activeTransformPosition.y + startPosition.floatValue * activeTransformLocalScale.y;

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localStartPosition, activeTransformPosition.z), Quaternion.identity);

                        float localHeightPosition = localStartPosition + height.floatValue * activeTransformLocalScale.y;

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localHeightPosition, activeTransformPosition.z), Quaternion.identity);

                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                        if (EditorGUI.EndChangeCheck())
                        {

                            startPosition.floatValue = (startPositionHandlePosition.y - activeTransformPosition.y) / activeTransformLocalScale.y;

                            height.floatValue = (heightHandlePosition.y - localStartPosition) / activeTransformLocalScale.y;

                            editor.Repaint();

                            editor.serializedObject.ApplyModifiedProperties();

                        }

                    }
                    else
                    {

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue, activeTransformPosition.z), Quaternion.identity);

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue + height.floatValue, activeTransformPosition.z), Quaternion.identity);


                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                        if (EditorGUI.EndChangeCheck())
                        {

                            startPosition.floatValue = startPositionHandlePosition.y;

                            height.floatValue = heightHandlePosition.y - startPosition.floatValue;

                            editor.Repaint();

                            editor.serializedObject.ApplyModifiedProperties();

                        }

                    }
                }
            }
        }

    }

    /// \english
    /// <summary>
    /// Draw distance fog sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="startPosition">Handle position.</param>
    /// <param name="length">Handle radius.</param>
    /// <param name="worldDistanceFog">Show the handles of world distance fog.</param>
    /// <param name="worldDistanceFogPosition">Position of handle position of world distance fog.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico de la niebla por distancia.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="startPosition">Posición del controlador.</param>
    /// <param name="length">Radio del controlador.</param>
    /// <param name="worldDistanceFog">Muestra los contorladores de la niebla de distancia del mundo.</param>
    /// <param name="worldDistanceFogPosition">Posición del controlador de posicion de la niebla de distancia del mundo.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawDistanceFogSphereHandle(MaterialProperty enableProperty, MaterialProperty startPosition, MaterialProperty length, MaterialProperty worldDistanceFog, MaterialProperty worldDistanceFogPosition, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        Vector3 handleWorldPosition = Vector3.zero;

        float startPositionHandleRadius;

        float lengthHandleRadius;

        if (showHandle & enableProperty.floatValue == 1)
        {

            if (worldDistanceFog.floatValue == 1)
            {

                handleWorldPosition = Handles.PositionHandle(worldDistanceFogPosition.vectorValue, Quaternion.identity);

                Handles.color = Color.blue;

                startPositionHandleRadius = Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vectorValue, startPosition.floatValue);

                Handles.color = Color.red;

                lengthHandleRadius = Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vectorValue, length.floatValue);

                if (EditorGUI.EndChangeCheck())
                {

                    worldDistanceFogPosition.vectorValue = handleWorldPosition;

                    startPosition.floatValue = startPositionHandleRadius;

                    length.floatValue = lengthHandleRadius;

                    editor.Repaint();

                    editor.serializedObject.ApplyModifiedProperties();

                }

            }
            else
            {

                Handles.color = Color.blue;

                startPositionHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, startPosition.floatValue);

                Handles.color = Color.red;

                lengthHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, length.floatValue);

                if (EditorGUI.EndChangeCheck())
                {

                    startPosition.floatValue = startPositionHandleRadius;

                    length.floatValue = lengthHandleRadius;

                    editor.Repaint();

                    editor.serializedObject.ApplyModifiedProperties();

                }

            }

        }

    }

    /// \english
    /// <summary>
    /// Draw camera fading sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="nearPoint">Handle position.</param>
    /// <param name="farPoint">Handle radius.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico del desvanecimiento por la cámara.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="nearPoint">Posición del controlador.</param>
    /// <param name="farPoint">Radio del controlador.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawCameraFadingSphereHandle(MaterialProperty enableProperty, MaterialProperty nearPoint, MaterialProperty farPoint, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        float nearPointHandleRadius;

        float farPointHandleRadius;

        if (showHandle & enableProperty.floatValue == 1)
        {

            Handles.color = Color.blue;

            nearPointHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, nearPoint.floatValue);

            Handles.color = Color.red;

            farPointHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, farPoint.floatValue);

            if (EditorGUI.EndChangeCheck())
            {

                nearPoint.floatValue = nearPointHandleRadius;

                farPoint.floatValue = farPointHandleRadius;

                editor.Repaint();

                editor.serializedObject.ApplyModifiedProperties();

            }

        }

    }

    /// \english
    /// <summary>
    /// Draw a sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="position">Handle position.</param>
    /// <param name="radius">Handle radius.</param>
    /// <param name="color">Handle color.</param>
    /// <param name="showRadiusHandle">Show the radius handle.</param>
    /// <param name="showPositionHandle">Show the position handle.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="position">Posición del controlador.</param>
    /// <param name="radius">Radio del controlador</param>
    /// <param name="color">Color del controlador.</param>
    /// <param name="showRadiusHandle">Muestra el controlador de radio.</param>
    /// <param name="showPositionHandle">Muestra el controlador de posición.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawSphereHandle(MaterialProperty enableProperty, MaterialProperty position, MaterialProperty radius, MaterialProperty color, bool showRadiusHandle, bool showPositionHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        Vector3 handlePosition = Vector3.zero;

        float handleRadius;

        if (showRadiusHandle & enableProperty.floatValue == 1)
        {

            if (showPositionHandle)
            {

                handlePosition = Handles.PositionHandle(position.vectorValue, Quaternion.identity);

            }

            Handles.color = color.colorValue;

            handleRadius = Handles.RadiusHandle(Quaternion.identity, position.vectorValue, radius.floatValue / 2);

            if (EditorGUI.EndChangeCheck())
            {

                if (showPositionHandle)
                {

                    position.vectorValue = handlePosition;

                }

                if (showRadiusHandle)
                {

                    radius.floatValue = handleRadius * 2;

                }

                editor.Repaint();

            }

        }

    }


    /// \english
    /// <summary>
    /// Draw a sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="position">Handle position.</param>
    /// <param name="radius">Handle radius.</param>
    /// <param name="color">Handle color.</param>
    /// <param name="showRadiusHandle">Show the radius handle.</param>
    /// <param name="showPositionHandle">Show the position handle.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="position">Posición del controlador.</param>
    /// <param name="radius">Radio del controlador</param>
    /// <param name="color">Color del controlador.</param>
    /// <param name="showRadiusHandle">Muestra el controlador de radio.</param>
    /// <param name="showPositionHandle">Muestra el controlador de posición.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawSphereHandleSimple(MaterialProperty enableProperty, Vector3 position, MaterialProperty radius, Color color, bool showRadiusHandle, bool showPositionHandle, Editor editor)
    {
        EditorGUI.BeginChangeCheck();

        Vector3 handlePosition = Vector3.zero;

        float handleRadius;

        if (showRadiusHandle & enableProperty.floatValue == 1)
        {

            if (showPositionHandle)
            {

                handlePosition = Handles.PositionHandle(position, Quaternion.identity);

            }

            Handles.color = color;

            handleRadius = Handles.RadiusHandle(Quaternion.identity, position, radius.floatValue);

            if (EditorGUI.EndChangeCheck())
            {

                if (showPositionHandle)
                {

                    position = handlePosition;

                }

                if (showRadiusHandle)
                {

                    radius.floatValue = handleRadius;

                }

                editor.Repaint();

            }

        }

    }

    #endregion

}
