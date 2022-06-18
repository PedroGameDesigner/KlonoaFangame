///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 05/09/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Editor of material with shader Unlit/VFX/Projector.
///

using UnityEngine;
using UnityEditor;
using CGF.Systems.Shaders.Unlit.VFX;

/// \english
/// <summary>
/// Editor of material with shader Unlit/VFX/Projector.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material con el shader Unlit/VFX/Projector.
/// </summary>
/// \endspanish
public class CGFUnlitProjectorMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    private bool _compactMode;

    private bool _blendTypeHeaderGroup = true;

    private bool _projectorHeaderGroup = true;

    private bool _uvScrollHeaderGroup;

    // Projector
    MaterialProperty _ShadowTex;
    MaterialProperty _ShadowTexTiling;
    MaterialProperty _ShadowTexOffset;
    MaterialProperty _Tint;
    MaterialProperty _ShadowLevel;
    MaterialProperty _FalloffMap;
    MaterialProperty _BackfaceCulling;
    MaterialProperty _UseVertexPosition;

    // UV Scroll
    MaterialProperty _UVScroll;
    MaterialProperty _FlipUVHorizontal;
    MaterialProperty _FlipUVVertical;
    MaterialProperty _UVScrollAnimation;
    MaterialProperty _UVScrollSpeed;

    // Blend Type
    MaterialProperty _BlendType;
    MaterialProperty _BlendFactorSource;
    MaterialProperty _BlendFactorDestination;
    MaterialProperty _BlendOperation;

    #endregion


    #region Main Methods

    protected override void GetProperties()
    {

        // Projector
        _ShadowTex = FindProperty("_ShadowTex");
        _ShadowTexTiling = FindProperty("_ShadowTexTiling");
        _ShadowTexOffset = FindProperty("_ShadowTexOffset");
        _Tint = FindProperty("_Tint");
        _ShadowLevel = FindProperty("_ShadowLevel");
        _FalloffMap = FindProperty("_FalloffMap");
        _BackfaceCulling = FindProperty("_BackfaceCulling");
        _UseVertexPosition = FindProperty("_UseVertexPosition");

        // UV Scroll
        _UVScroll = FindProperty("_UVScroll");
        _FlipUVHorizontal = FindProperty("_FlipUVHorizontal");
        _FlipUVVertical = FindProperty("_FlipUVVertical");
        _UVScrollAnimation = FindProperty("_UVScrollAnimation");
        _UVScrollSpeed = FindProperty("_UVScrollSpeed");

        // Blend Type
        _BlendType = FindProperty("_BlendType");
        _BlendFactorSource = FindProperty("_SrcBlendFactor");
        _BlendFactorDestination = FindProperty("_DstBlendFactor");
        _BlendOperation = FindProperty("_BlendOperation");

    }

    protected override void InspectorGUI()
    {
        CGFMaterialEditorUtilitiesClass.BuildMaterialComponent(typeof(CGFUnlitProjectorBehavior));

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("http://chloroplastgames.com/cg-framework-user-manual/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        _compactMode = CGFMaterialEditorUtilitiesClass.BuildTextureCompactMode(_compactMode, m_HeaderStateKey);

        GUILayout.Space(5);

        CGFMaterialEditorUtilitiesClass.BuildBlendTypeEnum(_BlendType, _BlendFactorSource, _BlendFactorDestination, _BlendOperation, this, _blendTypeHeaderGroup, m_HeaderStateKey);

        GUILayout.Space(5);

        // Projector
        CGFMaterialEditorUtilitiesExtendedClass.BuildProjector(_ShadowTex, _ShadowTexTiling, _ShadowTexOffset, _Tint, _ShadowLevel, _FalloffMap, _BackfaceCulling, _UseVertexPosition, this, _projectorHeaderGroup, m_HeaderStateKey, _compactMode);

        GUILayout.Space(5);

        // UV Scroll
        CGFMaterialEditorUtilitiesExtendedClass.BuildUVScrollSimple(_UVScroll, _FlipUVHorizontal, _FlipUVVertical, _UVScrollAnimation, _UVScrollSpeed, _uvScrollHeaderGroup, m_HeaderStateKey);

    }
    #endregion
}