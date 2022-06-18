///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 25/02/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Class from which other custom material editor classes inherit.
///

using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

/// \english
/// <summary>
/// Class from which other custom material editor classes inherit.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Clase de la que heredan las clases del editor personalizado de los materiales.
/// </summary>
/// \endspanish
public abstract class CGFMaterialEditorClass : MaterialEditor
{

    private const string k_KeyPrefix = "Material:UI_State:";
    protected string m_HeaderStateKey;

    protected virtual MaterialProperty FindProperty(string propertyName)
    {
        
        return GetMaterialProperty(serializedObject.targetObjects, propertyName);

    }

    public override void OnEnable()
    {

        base.OnEnable();

        Material material = this.target as Material;
        
        // Foldout states
        string shaderName = Regex.Replace(material.shader.name, @"\s+", String.Empty);
        m_HeaderStateKey = k_KeyPrefix + shaderName + "." +  material.GetInstanceID(); // Create key string for editor prefs

        GetProperties();

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (isVisible && !serializedObject.isEditingMultipleObjects)
        {

            GetProperties();

            InspectorGUI();

        }
        else if (serializedObject.isEditingMultipleObjects)
        {

            EditorGUILayout.LabelField("Selected materials cannot be multi-edited.");

        }

        serializedObject.ApplyModifiedProperties();
    }

    protected abstract void GetProperties();

    protected abstract void InspectorGUI();

    protected bool HasMultipleMixedShaderValues()
    {

        bool flag = false;

        Shader shader = (this.targets[0] as Material).shader;

        for (int index = 1; index < this.targets.Length; ++index)
        {

            if (shader != (this.targets[index] as Material).shader)
            {

                flag = true;

                break;

            }

        }

        return flag;

    }

}
