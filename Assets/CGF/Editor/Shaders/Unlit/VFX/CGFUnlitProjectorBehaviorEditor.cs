///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 13/03/2019
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Behavior that allows to the attatched gameobject manage a material with shader CG Framework/Unlit/Unlit Projector.
///

using UnityEditor;
using UnityEngine;
using CGF.Editor;
using CGF.Systems.Shaders.Unlit.VFX;

/// \english
/// <summary>
/// Behavior that allows to the attatched gameobject manage a material with shader Unlit/Projector.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor de los comportamientos del material del shader CG Framework/Unlit/Unlit Projector.
/// </summary>
/// \endspanish
[CustomEditor(typeof(CGFUnlitProjectorBehavior))]
[CanEditMultipleObjects]
public class CGFUnlitProjectorBehaviorEditor : Editor
{

    private SerializedProperty _blendType;

    private SerializedProperty _blendSource;

    private SerializedProperty _blendDestination;
    
    private SerializedProperty _blendOperation;

    private SerializedProperty _cutoff;

    private SerializedProperty _cookieColor;

    private SerializedProperty _shadowTex;

    private SerializedProperty _shadowTexOffset;

    private SerializedProperty _shadowTexTiling;

    private SerializedProperty _uvScrollAnimation;

    private SerializedProperty _shadowLevel;

    private SerializedProperty _uvScrollSpeed;

    private SerializedProperty _falloffMap;

    private SerializedProperty _backfaceCulling;

    private SerializedProperty _useVertexPosition;

    private SerializedProperty _flipUVVertical;

    private SerializedProperty _flipUVHorizontal;

    private SerializedProperty _uvScroll;

    protected SerializedProperty myMaterial;

    protected virtual void OnEnable()
    {

        myMaterial = serializedObject.FindProperty("_myMaterial");

        _cutoff = serializedObject.FindProperty("_cutoff");

        _cookieColor = serializedObject.FindProperty("_cookieColor");

        _shadowTex = serializedObject.FindProperty("_shadowTex");

        _shadowTexOffset = serializedObject.FindProperty("_shadowTexOffset");

        _shadowTexTiling = serializedObject.FindProperty("_shadowTexTiling");

        _uvScrollAnimation = serializedObject.FindProperty("_uvScrollAnimation");

        _shadowLevel = serializedObject.FindProperty("_shadowLevel");

        _uvScrollSpeed = serializedObject.FindProperty("_uvScrollSpeed");

        _falloffMap = serializedObject.FindProperty("_falloffMap");

        _backfaceCulling = serializedObject.FindProperty("_backfaceCulling");

        _useVertexPosition = serializedObject.FindProperty("_useVertexPosition");

        _blendType = serializedObject.FindProperty("_blendType");

        _blendSource = serializedObject.FindProperty("_blendSource");
    
        _blendDestination = serializedObject.FindProperty("_blendDestination");

        _blendOperation = serializedObject.FindProperty("_blendOperation");

        _flipUVVertical = serializedObject.FindProperty("_flipUVVertical");

        _flipUVHorizontal = serializedObject.FindProperty("_flipUVHorizontal");

        _uvScroll = serializedObject.FindProperty("_uvScroll");

    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        CGFEditorUtilitiesClass.BuildComponentTools("http://chloroplastgames.com/cg-framework-user-manual/", serializedObject);

        CGFEditorUtilitiesClass.ManageComponentValues<CGFUnlitProjectorBehavior>();

        CGFEditorUtilitiesClass.BackUpComponentValues<CGFUnlitProjectorBehavior>(serializedObject);

        EditorGUILayout.LabelField(new GUIContent("Blend Type", "Description"), EditorStyles.boldLabel);

        CGFEditorUtilitiesExtendedClass.BuildBlendTypeEnum(_blendType, _blendSource, _blendDestination, _blendOperation, myMaterial);

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Projector", "Description"), EditorStyles.boldLabel);

        CGFEditorUtilitiesClass.BuildTexture("Cookie (RGB)", "Description", _shadowTex, _shadowTexTiling, _shadowTexOffset);

        CGFEditorUtilitiesClass.BuildColor("Cookie Color (RGB)", "Description", _cookieColor);

        CGFEditorUtilitiesClass.BuildFloatPositive("Cookie Level", "Description", _shadowLevel);

        CGFEditorUtilitiesClass.BuildTexture("Falloff Map (RGB)", "Description", _falloffMap);

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        CGFEditorUtilitiesClass.BuildBoolean("Backface Culling", "Description", _backfaceCulling);

        EditorGUI.BeginDisabledGroup(_backfaceCulling.boolValue == false);

        CGFEditorUtilitiesClass.BuildBoolean("Use Vertex Position", "Description", _useVertexPosition);

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        CGFEditorUtilitiesExtendedClass.BuildUVScrollSimple(_uvScroll, _flipUVHorizontal, _flipUVVertical, _uvScrollAnimation, _uvScrollSpeed);

        if (serializedObject.targetObject != null)
        {

            serializedObject.ApplyModifiedProperties();

        }

    }

}