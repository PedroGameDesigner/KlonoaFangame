///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 19/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano, David Cuenca
/// Description: Class with a utility and functionality set for the customize and build fast the inspector of materials.
///

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;


/// \english
/// <summary>
/// Class with a utility and functionality set for the customize and build fast the inspector of materials.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Clase con un conjunto de utilidades y funcionalidades para personalizar y construir rápido el inspector de los materiales.
/// </summary>
/// \endspanish
public class CGFMaterialEditorUtilitiesClass : ShaderGUI
{

    #region Public Variables

        /// \english
        /// <summary>
        /// Texture propery data.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Datos de la propiedad de tipo textura.
        /// </summary>
        /// \endspanish
        public struct TextureStruct
        {

            /// \english
            /// <summary>
            /// Texture.
            /// </summary>
            /// \endenglish
            /// \spanish
            /// <summary>
            /// Textura.
            /// </summary>
            /// \endspanish
            public Texture texture;

            /// \english
            /// <summary>
            /// Rect.
            /// </summary>
            /// \endenglish
            /// \spanish
            /// <summary>
            /// Rect.
            /// </summary>
            /// \endspanish
            public Rect rect;

        }

        /// \english
        /// <summary>
        /// Rendering modes. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public enum RenderMode
        {
            Opaque,

            Cutout,

            Transparent,
        }

        /// \english
        /// <summary>
        /// Selected rendering mode. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public static RenderMode renderMode;

        /// \english
        /// <summary>
        /// Rendering mode names. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado. Opaque, Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeNames = GetEnumNames(renderMode);

         /// \english
        /// <summary>
        /// Rendering modes. Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado. Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public enum RenderModeTransparent
        {
            Cutout,

            Transparent
        }

        /// \english
        /// <summary>
        /// Selected rendering mode. Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado. Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public static RenderModeTransparent renderModeTransparent;

        /// \english
        /// <summary>
        /// Rendering mode names. Transparent, Cutout.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado. Transparent, Cutout.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeTransparentNames = GetEnumNames(renderModeTransparent);

        /// \english
        /// <summary>
        /// Rendering modes. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public enum RenderModeStandard
        {

            Opaque,

            Cutout,

            Fade,

            Transparent

        }

        /// \english
        /// <summary>
        /// Selected rendering mode. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public static RenderModeStandard renderModeStandard;

        /// \english
        /// <summary>
        /// Rendering mode names. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeStandardNames = GetEnumNames(renderModeStandard);

        /// \english
        /// <summary>
        /// Rendering modes. Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado. Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public enum RenderModeStandardTransparent
        {

            Cutout,

            Fade,

            Transparent

        }

        /// \english
        /// <summary>
        /// Selected rendering mode. Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado. Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public static RenderModeStandardTransparent renderModeStandardTransparent;

        /// \english
        /// <summary>
        /// Rendering mode names. Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado. Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeStandardTransparentNames = GetEnumNames(renderModeStandardTransparent);

        /// \english
        /// <summary>
        /// Rendering modes. Opaque, Transparent. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado. Opaque, Transparent. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// \endspanish
        public enum RenderModeLite
        {

            Opaque,

            Transparent,

        }

        /// \english
        /// <summary>
        /// Selected rendering mode. Opaque, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado. Opaque, Transparent.
        /// </summary>
        /// \endspanish
        public static RenderModeLite renderModeLite;

        /// \english
        /// <summary>
        /// Rendering mode names. Opaque, Transparent.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado. Opaque, Transparent.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeLiteNames = GetEnumNames(renderModeLite);

        /// \english
        /// <summary>
        /// Blending modes.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendMode
        {

            Normal,

            Darken,

            Multiply,

            ColorBurn,

            LinearBurn,

            DarkerColor,

            Lighten,

            Screen,

            ColorDodge,

            LinearDodgeOrAdditive,

            LighterColor,

            Overlay,

            SoftLight,

            HardLight,

            VividLight,

            LinearLight,

            PinLight,

            HardMix,

            Difference,

            Exclusion,

            Subtract,

            Divide,

            Hue,

            Saturation,

            Color,

            Luminosity

        }

        /// \english
        /// <summary>
        /// Selected blending mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de fusión seleccionado.
        /// </summary>
        /// \endspanish
        public static BlendMode blendMode;

        /// \english
        /// <summary>
        /// Blending mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de fusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] blendModeNames = GetEnumNames(blendMode);

        /// \english
        /// <summary>
        /// Blending factors.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Factores de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendFactor
        {

            Zero,

            One,

            DstColor,

            SrcColor,

            OneMinusDstColor,

            SrcAlpha,

            OneMinusSrcColor,

            DstAlpha,

            OneMinusDstAlpha,

            SrcAlphaSaturate,

            OneMinusSrcAlpha,

        }

        /// \english
        /// <summary>
        /// Selected blending factor.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Factor de fusión seleccionado.
        /// </summary>
        /// \endspanish
        public static BlendFactor blendFactor;

        /// \english
        /// <summary>
        /// Blending factor names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los factores de fusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] blendFactorNames = GetEnumNames(blendFactor);

        /// \english
        /// <summary>
        /// Blending operations.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operacions de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendOperation
        {
            Add,

            Subtract,

            ReverseSubtract,

            Min,

            Max,

            LogicalClear,

            LogicalSet,

            LogicalCopy,

            LogicalCopyInverted,

            LogicalNoop,

            LogicalInvert,

            LogicalAnd,

            LogicalNand,

            LogicalOr,

            LogicalNor,

            LogicalXor,

            LogicalEquivalence,

            LogicalAndReverse,

            LogicalAndInverted,

            LogicalOrReverse,

            LogicalOrInverted,

            Multiply,

            Screen,

            Overlay,

            Darken,

            Lighten,

            ColorDodge,

            ColorBurn,

            HardLight,

            SoftLight,

            Difference,

            Exclusion,

            HSLHue,

            HSLSaturation,

            HSLColor,

            HSLLuminosity
        }

        /// \english
        /// <summary>
        /// Selected blending operations.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación de fusión seleccionado.
        /// </summary>
        /// \endspanish
        public static BlendOperation blendOperation;

        /// \english
        /// <summary>
        /// Blending operation names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de las operaciones de fusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] blendOperationNames = GetEnumNames(blendOperation);

        /// \english
        /// <summary>
        /// Blending type.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Tipos de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendType
        {

            Custom,
            
            AlphaBlend,

            Premultiplied,

            AdditiveLinearDodge,

            SoftAdditive,

            Multiplicative,

            DoubleMultiplicative,

            ParticleBlend,

            Darken,

            Lighten,

            Screen,

            LinearBurn
        }

        /// \english
        /// <summary>
        /// Selected blending type.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Tipo de fusión seleccionado.
        /// </summary>
        /// \endspanish
        public static BlendType blendType;

        /// \english
        /// <summary>
        /// Blending type names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los tipos de fusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] blendTypeNames = GetEnumNames(blendType);
		
		/// \english
        /// <summary>
        /// Cull mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de oclusión.
        /// </summary>
        /// \endspanish
        public enum CullMode
        {

            Off,
            
            Front,

            Back

        }

        /// \english
        /// <summary>
        /// Selected cull mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de oclusión seleccionado.
        /// </summary>
        /// \endspanish
        public static CullMode cullMode;

        /// \english
        /// <summary>
        /// Cull mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de oclusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] cullModeNames = GetEnumNames(cullMode);

        /// \english
        /// <summary>
        /// Depth mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de profundidad.
        /// </summary>
        /// \endspanish
        public enum DepthMode
        {

            On,

            Off,

        }

        /// \english
        /// <summary>
        /// Selected depth mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de profundidad seleccionado.
        /// </summary>
        /// \endspanish
        public static DepthMode depthMode;

        /// \english
        /// <summary>
        /// Depth mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de profundidad.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] depthModeNames = GetEnumNames(depthMode);

        /// \english
        /// <summary>
        /// Stencil compare functions.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Funciones de comparación del stencil.
        /// </summary>
        /// \endspanish
        public enum StencilCompareFunction
        {

            Disabled,

            Never,

            Less,

            Equal,

            LessEqual,

            Greater,

            NotEqual,

            GreaterEqual,

            Always

        }

        /// \english
        /// <summary>
        /// Selected stencil compare function.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Funciones de comparación del stencil seleccionada.
        /// </summary>
        /// \endspanish
        public static StencilCompareFunction stencilCompareFunction;

        /// \english
        /// <summary>
        /// Stencil compare function names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de las funciones de comparación del stencil.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] stencilCompareFunctionNames = GetEnumNames(stencilCompareFunction);

        /// \english
        /// <summary>
        /// Stencil pass operations.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del pass del stencil.
        /// </summary>
        /// \endspanish
        public enum StencilPassOperation
        {

            Keep,

            Zero,

            Replace,

            IncrementSaturate,

            DecrementSaturate,

            Invert,

            IncrementWrap,

            DecrementWrap

        }

        /// \english
        /// <summary>
        /// Selected stencil pass operation.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del pass del stencil seleccionada.
        /// </summary>
        /// \endspanish
        public static StencilPassOperation stencilPassOperation;

        /// \english
        /// <summary>
        /// Stencil pass operation names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de las operaciones del pass del stencil.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] stencilPassOperationNames = GetEnumNames(stencilPassOperation);

        /// \english
        /// <summary>
        /// Stencil fail operations.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del fail del stencil.
        /// </summary>
        /// \endspanish
        public enum StencilFailOperation
        {

            Keep,

            Zero,

            Replace,

            IncrementSaturate,

            DecrementSaturate,

            Invert,

            IncrementWrap,

            DecrementWrap

        }

        /// \english
        /// <summary>
        /// Selected stencil fail operation.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del fail del stencil seleccionada.
        /// </summary>
        /// \endspanish
        public static StencilFailOperation stencilFailOperation;

        /// \english
        /// <summary>
        /// Stencil fail operation names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de las operaciones del fail del stencil.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] stencilFailOperationNames = GetEnumNames(stencilFailOperation);

        /// \english
        /// <summary>
        /// Stencil zfail operations.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del zfail del stencil.
        /// </summary>
        /// \endspanish
        public enum StencilZFailOperation
        {

            Keep,

            Zero,

            Replace,

            IncrementSaturate,

            DecrementSaturate,

            Invert,

            IncrementWrap,

            DecrementWrap

        }

        /// \english
        /// <summary>
        /// Selected stencil zfail operation.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Operación del zfail del stencil seleccionada.
        /// </summary>
        /// \endspanish
        public static StencilZFailOperation stencilZFailOperation;

        /// \english
        /// <summary>
        /// Stencil zfail operation names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de las operaciones del zfail del stencil.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] stencilZFailOperationNames = GetEnumNames(stencilZFailOperation);

        /// \english
        /// <summary>
        /// Surface rendering modes.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado surface.
        /// </summary>
        /// \endspanish
        public enum SurfaceRenderMode
        {

            Opaque,

            Cutout,

            Fade,

            Transparent

        }

        /// \english
        /// <summary>
        /// Selected rendering mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado.
        /// </summary>
        /// \endspanish
        public static SurfaceRenderMode surfaceRenderMode;

        /// \english
        /// <summary>
        /// Surface rendering mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado surface.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] surfaceRenderModeNames = GetEnumNames(surfaceRenderMode);

        /// \english
        /// <summary>
        /// Array to store the options of a enum of keyword.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Array para almacenar las opciones de un enumeración de una keyword.
        /// </summary>
        /// \endspanish
        public static string[] keywordEnumOptions = new string[]{};

        /// \english
        /// <summary>
        /// Selected keyword enum option.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Opción seleccionada de un enumeración de una keyword.
        /// </summary>
        /// \endspanish
        public static float keywordEnumIndex = 0;

        /// \english
        /// <summary>
        /// Array to store the options of a enum.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Array para almacenar las opciones de un enumeración.
        /// </summary>
        /// \endspanish
        public static string[] enumOptions = new string[]{};

        /// \english
        /// <summary>
        /// Selected enum option.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Opción seleccionada de un enumeración.
        /// </summary>
        /// \endspanish
        public static float enumIndex = 0;

        /// \english
        /// <summary>
        /// Copied Component.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Componente copiado.
        /// </summary>
        /// \endspanish
        private static string _copiedComponent;

    #endregion

    #region Utilities

        /// \english
        /// <summary>
        /// Get the names of all elements of a enumeration.
        /// </summary>
        /// <param name="enumeration">Enumeration with the names.</param>
        /// <returns>Names of the elements of the enumeration.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene los nombres de los elementos de una enumeración.
        /// </summary>
        /// <param name="enumeration">Enumeración con los nombres.</param>
        /// <returns>Nombres de los elementos de la enumeración.</returns>
        /// \endspanish 
        protected static GUIContent[] GetEnumNames(Enum enumeration) {        

            string[] names = Enum.GetNames(enumeration.GetType());

            List<GUIContent> content = new List<GUIContent>(); ;

            for (int i = 0; i < names.Length; i++)
            {

                content.Add(new GUIContent(names[i]));

            }

            return content.ToArray();

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword according to its parameters.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="enable">Property float value used to enable o disable the keyword.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="enable">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// \endspanish 
        protected static void SetKeyword(MaterialProperty property, float enable, bool mode)
        {

            bool value = Convert.ToBoolean(enable);

            if (mode)
            {

                SetKeywordInternal(property, value, "_ON");
                
            }
            else
            {

                SetKeywordInternal(property, !value, "_OFF");
                
            }

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="on">Property float value used to enable o disable the keyword.</param>
        /// <param name="defaultKeywordSuffix">Keyword suffix.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="on">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="defaultKeywordSuffix">Sufijo dela keyword.</param>
        /// \endspanish 
        protected static void SetKeywordInternal(MaterialProperty property, bool on, string defaultKeywordSuffix)
        {

            string keyword = property.name.ToUpperInvariant() + defaultKeywordSuffix;
        
            foreach (Material target in property.targets)
            {
                if (on)
                {

                    target.EnableKeyword(keyword);

                }
                else
                {

                    target.DisableKeyword(keyword);

                }

            }

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword according to its parameters.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="enable">Property float value used to enable o disable the keyword.</param>
        /// <param name="name">Name of the keyword.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="enable">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="name">Nombre de la keyword.</param>
        /// \endspanish 
        protected static void SetKeywordWithName(MaterialProperty property, float enable, string name)
        {
            bool value = Convert.ToBoolean(enable);

            SetKeywordInternalWithName(property, value, name);  
        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="on">Property float value used to enable o disable the keyword.</param>
        /// <param name="defaultKeywordSuffix">Keyword suffix.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="on">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="defaultKeywordSuffix">Sufijo dela keyword.</param>
        /// \endspanish 
        protected static void SetKeywordInternalWithName(MaterialProperty property, bool on, string name)
        {
        
            foreach (Material target in property.targets)
            {
                if (on)
                {

                    target.EnableKeyword(name);

                }
                else
                {

                    target.DisableKeyword(name);

                }

            }

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword according to its parameters from a popup.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="options">An array with the options shown in the popup.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="options">Array con las opciones a mostrar en el popup.</param>
        /// \endspanish 
        protected static void SetKeywordEnum(MaterialProperty property, string[] options)
        {

            SetKeywordInternalEnum(property, options);

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword from a popup.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="options">Array con las opciones a mostrar en el popup.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="options">Array con las opciones a mostrar en el popup.</param>
        /// \endspanish 
        protected static void SetKeywordInternalEnum(MaterialProperty property, string[] options)
        {

            for (int index = 0; index < options.Length; ++index)
            {
                string selectedKeyword = (string)options.GetValue(index);

                string keyword = property.name.ToUpperInvariant() + "_" + selectedKeyword.ToUpperInvariant().Replace(" ","");
            
                foreach (Material target in property.targets)
                {
                    
                    if (property.floatValue == index)
                    {

                        target.EnableKeyword(keyword);

                    }

                    else
                    {

                        target.DisableKeyword(keyword);

                    }      

                }

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderMode">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderMode">Modo de renderizado inicial.</param>
        /// \endspanish 
        public static void SetInitialRenderMode(Material material, string renderMode)
        {

            if(material.GetTag("RenderType", false) == "Custom")    
            {

                switch (renderMode)
                {

                    case "opaque":

                        material.SetOverrideTag("RenderType", "Opaque");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                        material.SetInt("_ZWrite", 1);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                        material.DisableKeyword("_ALPHATEST_ON");

                        material.DisableKeyword("_ALPHABLEND_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                        material.SetOverrideTag("IgnoreProjector", "False");

                        break;

                    case "transparent":

                        material.SetOverrideTag("RenderType", "Transparent");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                        material.SetInt("_ZWrite", 0);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                        material.DisableKeyword("_ALPHATEST_ON");

                        material.EnableKeyword("_ALPHABLEND_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                        material.SetOverrideTag("IgnoreProjector", "True");

                        break;

                    case "cutout":

                        material.SetOverrideTag("RenderType", "TransparentCutout");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                        material.SetInt("_ZWrite", 1);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                        material.EnableKeyword("_ALPHATEST_ON");

                        material.DisableKeyword("_ALPHABLEND_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                        material.SetOverrideTag("IgnoreProjector", "False");

                        break;
                }

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeStandard">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeStandard">Modo de renderizado inicial.</param>
        /// \endspanish 
        public static void SetInitialRenderModeStandard(Material material, string renderModeStandard)
        {

            if(material.GetTag("RenderType", false) == "Custom")    
            {

                switch (renderModeStandard)
                {

                    case "opaque":

                        material.SetOverrideTag("RenderType", "Opaque");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                        material.SetInt("_ZWrite", 1);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                        material.DisableKeyword("_ALPHATEST_ON");

                        material.DisableKeyword("_ALPHABLEND_ON");

                        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                        material.SetOverrideTag("IgnoreProjector", "False");

                        break;

                    case "cutout":

                        material.SetOverrideTag("RenderType", "TransparentCutout");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                        material.SetInt("_ZWrite", 1);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                        material.EnableKeyword("_ALPHATEST_ON");

                        material.DisableKeyword("_ALPHABLEND_ON");

                        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                        material.SetOverrideTag("IgnoreProjector", "False");

                        break;

                    case "fade":

                        material.SetOverrideTag("RenderType", "Transparent");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                        material.SetInt("_ZWrite", 0);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                        material.DisableKeyword("_ALPHATEST_ON");

                        material.EnableKeyword("_ALPHABLEND_ON");

                        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                        material.SetOverrideTag("IgnoreProjector", "True");

                        break;

                    case "transparent":

                        material.SetOverrideTag("RenderType", "Transparent");

                        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                        material.SetInt("_ZWrite", 0);

                        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                        material.DisableKeyword("_ALPHATEST_ON");

                        material.DisableKeyword("_ALPHABLEND_ON");

                        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");

                        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                        material.SetOverrideTag("IgnoreProjector", "True");

                        break;

                }

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderMode">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderMode">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderMode(Material material, RenderMode renderMode)
        {

            switch (renderMode)
            {

                case RenderMode.Opaque:

                    material.SetOverrideTag("RenderType", "Opaque");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderMode.Transparent:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.EnableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderMode.Cutout:

                    material.SetOverrideTag("RenderType", "TransparentCutout");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    material.EnableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;
            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderMode">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Transparent, Cutout.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderMode">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeTransparent(Material material, RenderModeTransparent renderModeTransparent)
        {

            switch (renderModeTransparent)
            {

                case RenderModeTransparent.Transparent:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.EnableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderModeTransparent.Cutout:

                    material.SetOverrideTag("RenderType", "TransparentCutout");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    material.EnableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeStandard">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeStandard">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeFull(Material material, RenderModeStandard renderModeStandard)
        {
            switch (renderModeStandard)
            {
                case RenderModeStandard.Opaque:
                    material.SetOverrideTag("RenderType", "Opaque");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandard.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandard.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderModeStandard.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;
            }
        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeStandard">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeStandard">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeFullTransparent(Material material, RenderModeStandardTransparent renderModeStandardTransparent)
        {
            switch (renderModeStandardTransparent)
            {
                case RenderModeStandardTransparent.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandardTransparent.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderModeStandardTransparent.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;
            }
        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeStandard">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeStandard">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeStandard(Material material, RenderModeStandard renderModeStandard)
        {
            switch (renderModeStandard)
            {
                case RenderModeStandard.Opaque:
                    material.SetOverrideTag("RenderType", "Opaque");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandard.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandard.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderModeStandard.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;
            }
        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeStandard">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeStandard">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeStandardTransparent(Material material, RenderModeStandardTransparent renderModeStandardTransparent)
        {
            switch (renderModeStandardTransparent)
            {
                case RenderModeStandardTransparent.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

                case RenderModeStandardTransparent.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case RenderModeStandardTransparent.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;
            }
        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material. Opaque, Transparent.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderModeLite">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material. Opaque, Transparent.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderModeLite">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderModeLite(Material material, RenderModeLite renderModeLite)
        {

            switch (renderModeLite)
            {

                case RenderModeLite.Opaque:

                    material.SetOverrideTag("RenderType", "Opaque");

                    material.SetOverrideTag("IgnoreProjector", "False");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                    break;

                case RenderModeLite.Transparent:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetOverrideTag("IgnoreProjector", "True");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.EnableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material to transparent to cutout.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="transparentCutoffMode">Control value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material a transparente o cutout.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="transparentCutoffMode">Valor de control.</param>
        /// \endspanish 
        public static void SetTransparentCutoffMode(Material material, float transparentCutoffMode)
        {

            switch ((int)transparentCutoffMode)
            {

                case 0:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.EnableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    material.SetOverrideTag("IgnoreProjector", "True");

                    break;

                case 1:

                    material.SetOverrideTag("RenderType", "TransparentCutout");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    material.EnableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    material.SetOverrideTag("IgnoreProjector", "False");

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the blending mode of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="blendMode">Blend modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de fusión del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="blendMode">Blend de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetBlendMode(Material material, BlendMode blendMode)
        {

            switch (blendMode)
            {

                case BlendMode.Normal:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_NORMAL");

                    break;

                case BlendMode.Darken:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");
					
                    material.EnableKeyword("_BLENDMODE_DARKEN");

                    break;

                case BlendMode.Multiply:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_MULTIPLY");

                    break;

                case BlendMode.ColorBurn:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLORBURN");

                    break;

                case BlendMode.LinearBurn:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARBURN");

                    break;

                case BlendMode.DarkerColor:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DARKERCOLOR");

                    break;

                case BlendMode.Lighten:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LIGHTEN");

                    break;

                case BlendMode.Screen:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SCREEN");

                    break;

                case BlendMode.ColorDodge:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLORDODGE");

                    break;

                case BlendMode.LinearDodgeOrAdditive:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARDODGE");

                    break;

                case BlendMode.LighterColor:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LIGHTERCOLOR");

                    break;

                case BlendMode.Overlay:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_OVERLAY");

                    break;

                case BlendMode.SoftLight:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SOFTLIGHT");

                    break;

                case BlendMode.HardLight:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HARDLIGHT");

                    break;

                case BlendMode.VividLight:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_VIVIDLIGHT");

                    break;

                case BlendMode.LinearLight:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARLIGHT");

                    break;

                case BlendMode.PinLight:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_PINLIGHT");

                    break;

                case BlendMode.HardMix:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HARDMIX");

                    break;

                case BlendMode.Difference:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DIFFERENCE");

                    break;

                case BlendMode.Exclusion:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_EXCLUSION");

                    break;

                case BlendMode.Subtract:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SUBTRACT");

                    break;

                case BlendMode.Divide:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DIVIDE");

                    break;

                case BlendMode.Hue:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HUE");

                    break;

                case BlendMode.Saturation:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SATURATION");

                    break;

                case BlendMode.Color:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLOR");

                    break;

                case BlendMode.Luminosity:

                    material.DisableKeyword("_BLENDMODE_NORMAL");
                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LUMINOSITY");

                    break;

            }

        }


        /// \english
        /// <summary>
        /// Set the blending type of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="blendType">Blend types enumeration.</param>
        /// <param name="blendSource">Blend factor of source enumeration.</param>
        /// <param name="blendDestination">Blend factor of destination enumeration.</param>
        /// <param name="blendOperation">Blend operation.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el tipo de fusión del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="blendType">Enumeración de todos los tipos de fusión.</param>
        /// <param name="blendSource">Enumeración de los factores de fusión de la fuente.</param>
        /// <param name="blendDestination">Enumeración de los factores de fusión del destino.</param>
        /// <param name="blendOperation">Operación de fusión.</param>
        /// \endspanish
        public static void SetBlendType(Material material, BlendType blendType, BlendFactor blendSource, BlendFactor blendDestination, BlendOp blendOperation)
        {

            switch (blendType)
            {

                case BlendType.Custom:

                    material.SetInt("_SrcBlendFactor", (int)blendSource);
                    material.SetInt("_SrcBlend", (int)blendSource);

                    material.SetInt("_DstBlendFactor", (int)blendDestination);
                    material.SetInt("_DstBlend", (int)blendSource);

                    material.SetInt("_BlendOperation", (int)blendOperation);

                    break;

                case BlendType.AlphaBlend:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.Premultiplied:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.AdditiveLinearDodge:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.SoftAdditive:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.Multiplicative:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.DoubleMultiplicative:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcColor);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.SrcColor);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.ParticleBlend:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.Darken:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Min);

                    break;

                case BlendType.Lighten:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Max);

                    break;

                case BlendType.Screen:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcColor);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcColor);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;

                case BlendType.LinearBurn:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_BlendOperation", (int)UnityEngine.Rendering.BlendOp.Add);

                    break;
            }
        }
		
		/// \english
        /// <summary>
        /// Set the cull mode of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="cullMode">Cull modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de oclusión del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="cullMode">Enumeracion de todos los modos de oclusión.</param>
        /// \endspanish 
        public static void SetCullMode(Material material, CullMode cullMode)
        {

            switch (cullMode)
            {

                case CullMode.Off:
				
                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    break;

                case CullMode.Front:

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Front);

                    break;

                case CullMode.Back:

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the depth mode of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="depthMode">Depth modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de profundidad del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="depthMode">Enumeracion de todos los modos de profundidad.</param>
        /// \endspanish 
        public static void SetDepthMode(Material material, DepthMode depthMode)
        {

            switch (depthMode)
            {

                case DepthMode.On:

                    material.SetInt("_ZWrite", 1);

                    break;

                case DepthMode.Off:

                    material.SetInt("_ZWrite", 0);

                break;

            }

        }

        /// \english
        /// <summary>
        /// Set the stencil compare function of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="stencilCompareFunction">Stencil compare function enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura la función de comparación del stencil del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="stencilCompareFunction">Enumeracion de todos las funciones de comparación del stencil.</param>
        /// \endspanish 
        public static void SetStencilCompareFunction(Material material, StencilCompareFunction stencilCompareFunction)
        {

            switch (stencilCompareFunction)
            {

                case StencilCompareFunction.Disabled:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Disabled);

                    break;

                case StencilCompareFunction.Never:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Never);

                    break;

                case StencilCompareFunction.Less:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Less);

                    break;

                case StencilCompareFunction.Equal:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Equal);

                    break;

                case StencilCompareFunction.LessEqual:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.LessEqual);

                    break;

                case StencilCompareFunction.Greater:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Greater);

                    break;

                case StencilCompareFunction.NotEqual:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.NotEqual);

                    break;

                case StencilCompareFunction.GreaterEqual:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.GreaterEqual);

                    break;

                case StencilCompareFunction.Always:

                    material.SetInt("_StencilComparisonFunction", (int)UnityEngine.Rendering.CompareFunction.Always);

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the stencil pass operation of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="stencilOperation">Stencil pass operation enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura la operación de pass del stencil del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="stencilOperation">Enumeracion de todas las operaciones de pass del stencil.</param>
        /// \endspanish 
        public static void SetStencilPassOperation(Material material, StencilPassOperation stencilPassOperation)
        {

            switch (stencilPassOperation)
            {

                case StencilPassOperation.Keep:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.Keep);

                    break;

                case StencilPassOperation.Zero:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.Zero);

                    break;

                case StencilPassOperation.Replace:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.Replace);

                    break;

                case StencilPassOperation.IncrementSaturate:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.IncrementSaturate);

                    break;

                case StencilPassOperation.DecrementSaturate:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.DecrementSaturate);

                    break;

                case StencilPassOperation.Invert:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.Invert);

                    break;

                case StencilPassOperation.IncrementWrap:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.IncrementWrap);

                    break;

                case StencilPassOperation.DecrementWrap:

                    material.SetInt("_StencilPassOperation", (int)UnityEngine.Rendering.StencilOp.DecrementWrap);

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the stencil fail operation of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="stencilOperation">Stencil fail operation enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura la operación de fail del stencil del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="stencilOperation">Enumeracion de todas las operaciones de fail del stencil.</param>
        /// \endspanish 
        public static void SetStencilFailOperation(Material material, StencilFailOperation stencilFailOperation)
        {

            switch (stencilFailOperation)
            {

                case StencilFailOperation.Keep:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.Keep);

                    break;

                case StencilFailOperation.Zero:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.Zero);

                    break;

                case StencilFailOperation.Replace:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.Replace);

                    break;

                case StencilFailOperation.IncrementSaturate:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.IncrementSaturate);

                    break;

                case StencilFailOperation.DecrementSaturate:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.DecrementSaturate);

                    break;

                case StencilFailOperation.Invert:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.Invert);

                    break;

                case StencilFailOperation.IncrementWrap:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.IncrementWrap);

                    break;

                case StencilFailOperation.DecrementWrap:

                    material.SetInt("_StencilFailOperation", (int)UnityEngine.Rendering.StencilOp.DecrementWrap);

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the stencil fail operation of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="stencilOperation">Stencil fail operation enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura la operación de fail del stencil del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="stencilOperation">Enumeracion de todas las operaciones de fail del stencil.</param>
        /// \endspanish 
        public static void SetStencilZFailOperation(Material material, StencilZFailOperation stencilZFailOperation)
        {

            switch (stencilZFailOperation)
            {

                case StencilZFailOperation.Keep:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.Keep);

                    break;

                case StencilZFailOperation.Zero:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.Zero);

                    break;

                case StencilZFailOperation.Replace:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.Replace);

                    break;

                case StencilZFailOperation.IncrementSaturate:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.IncrementSaturate);

                    break;

                case StencilZFailOperation.DecrementSaturate:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.DecrementSaturate);

                    break;

                case StencilZFailOperation.Invert:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.Invert);

                    break;

                case StencilZFailOperation.IncrementWrap:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.IncrementWrap);

                    break;

                case StencilZFailOperation.DecrementWrap:

                    material.SetInt("_StencilZFailOperation", (int)UnityEngine.Rendering.StencilOp.DecrementWrap);

                    break;

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the surface material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderMode">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material surface.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderMode">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetSurfaceRenderMode(Material material, SurfaceRenderMode surfaceRenderMode)
        {

            switch (surfaceRenderMode)
            {

                case SurfaceRenderMode.Opaque:

                    material.SetOverrideTag("RenderType", "Opaque");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                    break;

                case SurfaceRenderMode.Cutout:

                    material.SetOverrideTag("RenderType", "TransparentCutout");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    //material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    material.EnableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    break;

                case SurfaceRenderMode.Fade:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    //material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.EnableKeyword("_ALPHABLEND_ON");

                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    break;

                case SurfaceRenderMode.Transparent:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.DisableKeyword("_ALPHABLEND_ON");

                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    break;

            }

        }

    #endregion

    #region Value Types

        /// \english
        /// <summary>
        /// Float property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloat(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo float con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloat(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float positive property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float positivo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float positivo introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatPositive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), (property.floatValue < 0) ? property.floatValue = 0f : property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float positive property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float positivo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float positivo introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatPositive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), (property.floatValue < 0) ? property.floatValue = 0f : property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float negative property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float negativo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float negativo introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatNegative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), (property.floatValue > 0) ? property.floatValue = 0f : property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float negative property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float negativo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float negativo introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatNegative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), (property.floatValue > 0) ? property.floatValue = 0f : property.floatValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRound(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round(property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad float redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRound(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;


            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round(property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float positive round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float positivo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRoundPositive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round((property.floatValue < 0) ? 0f : property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float positive round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo float positivo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRoundPositive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round((property.floatValue < 0) ? 0f : property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;         

        }

        /// \english
        /// <summary>
        /// Float negative round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float negativo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRoundNegative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round((property.floatValue > 0) ? 0f : property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float negative round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo float negativo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildFloatRoundNegative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), Mathf.Round((property.floatValue > 0) ? 0f : property.floatValue));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Vector2 property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2 (property.vectorValue.x, property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector2 con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(property.vectorValue.x, property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 positive property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 positivo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Positive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 positive property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector2 positivo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Positive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 negative property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 negativo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 negativo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Negative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 negative property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector 2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector2 negativo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 negativo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Negative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Round(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector2 redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2Round(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 positive round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 positivo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 positive round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector2 positivo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 negative round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector2 negativo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector2 negative round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector2 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector2 negativo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector2 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector2 BuildVector2RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector2 value = EditorGUILayout.Vector2Field(new GUIContent(propertyName, propertyDescription), new Vector2(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(property.vectorValue.x, property.vectorValue.y, property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector3 con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(property.vectorValue.x, property.vectorValue.y, property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 positive property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 positivo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Positive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y, (property.vectorValue.z < 0) ? 0f : property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 positive property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector3 positivo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Positive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y, (property.vectorValue.z < 0) ? 0f : property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 negative property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 negativo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 negativo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Negative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y, (property.vectorValue.z > 0) ? 0f : property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 negative property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector3 negativo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 negativo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Negative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y, (property.vectorValue.z > 0) ? 0f : property.vectorValue.z));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Round(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y), Mathf.Round(property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector3 redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3Round(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y), Mathf.Round(property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 positive round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 positivo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z < 0) ? 0f : property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 positive round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector3 positivo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z < 0) ? 0f : property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 negative round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector3 negativo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z > 0) ? 0f : property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector3 negative round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector3 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector3 negativo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector3 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector3 BuildVector3RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector3 value = EditorGUILayout.Vector3Field(new GUIContent(propertyName, propertyDescription), new Vector3(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z > 0) ? 0f : property.vectorValue.z)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), property.vectorValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }


        /// \english
        /// <summary>
        /// Vector4 property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector4 con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), property.vectorValue);

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 positive property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 positivo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Positive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y, (property.vectorValue.z < 0) ? 0f : property.vectorValue.z, (property.vectorValue.w < 0) ? 0f : property.vectorValue.w));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 positive property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Positive vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector4 positivo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 positivo introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Positive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4((property.vectorValue.x < 0) ? 0f : property.vectorValue.x, (property.vectorValue.y < 0) ? 0f : property.vectorValue.y, (property.vectorValue.z < 0) ? 0f : property.vectorValue.z, (property.vectorValue.w < 0) ? 0f : property.vectorValue.w));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 negative property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 negativo con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 negative introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Negative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y, (property.vectorValue.z > 0) ? 0f : property.vectorValue.z, (property.vectorValue.w > 0) ? 0f : property.vectorValue.w));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 negative property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Negative vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector4 negativo con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 negative introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Negative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4((property.vectorValue.x > 0) ? 0f : property.vectorValue.x, (property.vectorValue.y > 0) ? 0f : property.vectorValue.y, (property.vectorValue.z > 0) ? 0f : property.vectorValue.z, (property.vectorValue.w > 0) ? 0f : property.vectorValue.w));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Round(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y), Mathf.Round(property.vectorValue.z), Mathf.Round(property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propuedad vector4 redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4Round(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round(property.vectorValue.x), Mathf.Round(property.vectorValue.y), Mathf.Round(property.vectorValue.z), Mathf.Round(property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 positive round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 positivo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z < 0) ? 0f : property.vectorValue.z), Mathf.Round((property.vectorValue.w < 0) ? 0f : property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 positive round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round positive vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector4 positivo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 positivo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4RoundPositive(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round((property.vectorValue.x < 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y < 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z < 0) ? 0f : property.vectorValue.z), Mathf.Round((property.vectorValue.w < 0) ? 0f : property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 negative round property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo vector4 negativo redondeado con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z > 0) ? 0f : property.vectorValue.z), Mathf.Round((property.vectorValue.w > 0) ? 0f : property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Vector4 negative round property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round negative vector4 value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un propiedad de tipo vector4 negativo redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor vector4 negativo redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static Vector4 BuildVector4RoundNegative(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            Vector4 value = EditorGUILayout.Vector4Field(new GUIContent(propertyName, propertyDescription), new Vector4(Mathf.Round((property.vectorValue.x > 0) ? 0f : property.vectorValue.x), Mathf.Round((property.vectorValue.y > 0) ? 0f : property.vectorValue.y), Mathf.Round((property.vectorValue.z > 0) ? 0f : property.vectorValue.z), Mathf.Round((property.vectorValue.w > 0) ? 0f : property.vectorValue.w)));

            EditorGUI.showMixedValue = false;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.vectorValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.vectorValue;

        }

        /// \english
        /// <summary>
        /// Float slider property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float slider con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildSlider(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 0.0f;

            float value = EditorGUILayout.Slider(new GUIContent(propertyName, propertyDescription), property.floatValue, property.rangeLimits.x, property.rangeLimits.y);

            EditorGUI.showMixedValue = false;

            EditorGUIUtility.labelWidth = labelWidth;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float slider property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float slider redondeado con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildSlider(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 0.0f;

            float value = EditorGUILayout.Slider(new GUIContent(propertyName, propertyDescription), property.floatValue, property.rangeLimits.x, property.rangeLimits.y);

            EditorGUI.showMixedValue = false;

            EditorGUIUtility.labelWidth = labelWidth;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float slider property builder with units.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float slider con unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildSliderRound(string propertyName, string propertyDescription, MaterialProperty property, string units = null)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 0.0f;

            float value = EditorGUILayout.Slider(new GUIContent(propertyName, propertyDescription), Mathf.Round(property.floatValue), property.rangeLimits.x, property.rangeLimits.y);

            EditorGUI.showMixedValue = false;

            EditorGUIUtility.labelWidth = labelWidth;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Float slider property builder with units and locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <param name="units">Property units.</param>
        /// <returns>Round float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo float slider con unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <returns>Valor float redondeado introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildSliderRound(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, string units = null)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 0.0f;

            float value = EditorGUILayout.Slider(new GUIContent(propertyName, propertyDescription), Mathf.Round(property.floatValue), property.rangeLimits.x, property.rangeLimits.y);

            EditorGUI.showMixedValue = false;

            EditorGUIUtility.labelWidth = labelWidth;

            if (units != null)
                GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Color property builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <returns>Color value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo color.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <returns>Valor color introducido por el usuario.</returns>
        /// \endspanish
        public static Color BuildColor(string propertyName, string propertyDescription, MaterialProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            bool hdr = (property.flags & MaterialProperty.PropFlags.HDR) != MaterialProperty.PropFlags.None;

            bool showAlpha = true;

            Color color = EditorGUILayout.ColorField(new GUIContent(propertyName, propertyDescription), property.colorValue, true, showAlpha, hdr, (ColorPickerHDRConfig)null);

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.colorValue = color;

            EditorGUILayout.EndHorizontal();

            return property.colorValue;

        }

        /// \english
        /// <summary>
        /// Color property builder with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <returns>Color value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo color con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <returns>Valor color introducido por el usuario.</returns>
        /// \endspanish
        public static Color BuildColor(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            bool hdr = (property.flags & MaterialProperty.PropFlags.HDR) != MaterialProperty.PropFlags.None;

            bool showAlpha = true;

            Color color = EditorGUILayout.ColorField(new GUIContent(propertyName, propertyDescription), property.colorValue, true, showAlpha, hdr, (ColorPickerHDRConfig)null);

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.colorValue = color;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.colorValue;

        }

        /// \english
        /// <summary>
        /// Color HDR property builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <returns>Color value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo color HDR.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <returns>Valor color introducido por el usuario.</returns>
        /// \endspanish
        public static Color BuildColorHDR(string propertyName, string propertyDescription, MaterialProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            bool showAlpha = true;

            Color color = EditorGUILayout.ColorField(new GUIContent(propertyName, propertyDescription), property.colorValue, true, showAlpha, true, new ColorPickerHDRConfig(0f, 99f, 1f / 99f, 3f));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.colorValue = color;

            EditorGUILayout.EndHorizontal();

            return property.colorValue;

        }

        /// \english
        /// <summary>
        /// Color HDR property builder with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <returns>Color value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo color HDR con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <returns>Valor color introducido por el usuario.</returns>
        /// \endspanish
        public static Color BuildColorHDR(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            bool showAlpha = true;

            Color color = EditorGUILayout.ColorField(new GUIContent(propertyName, propertyDescription), property.colorValue, true, showAlpha, true, new ColorPickerHDRConfig(0f, 99f, 1f / 99f, 3f));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.colorValue = color;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.colorValue;

        }

        /// \english
        /// <summary>
        /// Texture property builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="scaleOffset">If enabled, shows the tiling and offset properties.</param>
        /// <param name="compactMode">Float that manages de compact mode.</param>
        /// <returns>Texture entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo textura.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="scaleOffset">Si está activo, se muestra la propiedad de tiling y offset.</param>
        /// <param name="compactMode">Float que gestiona el modo compacto.</param>
        /// <returns>Textura introducido por el usuario.</returns>
        /// \endspanish
        public static TextureStruct BuildTexture(string propertyName, string propertyDescription, MaterialProperty property, MaterialEditor materialEditor, bool scaleOffset, bool compactMode = false)
        {

            TextureStruct textureStruct = new TextureStruct();

            if (compactMode == false)
            {

                textureStruct.texture = materialEditor.TextureProperty(EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 35), property, propertyName, propertyDescription, scaleOffset);

            return textureStruct;

            }
            else
            {

                textureStruct.rect = materialEditor.TexturePropertySingleLine(new GUIContent(propertyName, propertyDescription), property);

                return textureStruct;

            }

        }

        /// \english
        /// <summary>
        /// Texture property builder with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="scaleOffset">If enabled, shows the tiling and offset properties.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <param name="compactMode">Float that manages de compact mode.</param>
        /// <returns>Texture entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo textura con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="scaleOffset">Si está activo, se muestra la propiedad de tiling y offset.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="compactMode">Float que gestiona el modo compacto.</param>
        /// <returns>Textura introducido por el usuario.</returns>
        /// \endspanish
        public static TextureStruct BuildTexture(string propertyName, string propertyDescription, MaterialProperty property, MaterialEditor materialEditor, bool scaleOffset, float toggleLock, bool compactMode = false)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            TextureStruct textureStruct = new TextureStruct();

            if (compactMode == false)
            {

                textureStruct.texture = materialEditor.TextureProperty(EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 35), property, propertyName, propertyDescription, scaleOffset);

                GUI.enabled = true;

                return textureStruct;

            }
            else
            {

                textureStruct.rect = materialEditor.TexturePropertySingleLine(new GUIContent(property.displayName), property);

                GUI.enabled = true;

                return textureStruct;

            }

        }

        /// \english
        /// <summary>
        /// Texture vertical property builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <returns>Texture entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo textura vertical.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <returns>Textura introducido por el usuario.</returns>
        /// \endspanish
        public static TextureStruct BuildTextureVertical(string propertyName, string propertyDescription, MaterialProperty property, bool scaleOffset, MaterialEditor materialEditor)
        {

            BuildLabel(propertyName, propertyDescription);

            GUILayout.Space(5);

            TextureStruct textureStruct = new TextureStruct();

            textureStruct.rect = materialEditor.TexturePropertySingleLine(new GUIContent(""), property);

            //Preview texture.
            Rect rectPreview = EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 8);
            var previewRect = new Rect(rectPreview.x + 35, rectPreview.y - 20, rectPreview.width - 40, rectPreview.height + 1);
            /*if(property.hasMixedValue || (property.textureValue = null))
            {
                var col = GUI.color;
                GUI.color = EditorGUIUtility.isProSkin ? new Color(.25f, .25f, .25f) : new Color(.85f, .85f, .85f);
                EditorGUI.DrawPreviewTexture(previewRect, Texture2D.whiteTexture);
                GUI.color = col;
            }*/
            if(property.textureValue != null)
            {
                EditorGUI.DrawPreviewTexture(previewRect, property.textureValue);
            }

            if (scaleOffset == true)
            {
                Rect rectScaleOffset = EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 20);
                var scaleOffsetRect = new Rect(rectPreview.x, rectScaleOffset.y - 20, rectScaleOffset.width, rectScaleOffset.height);
                materialEditor.TextureScaleOffsetProperty(scaleOffsetRect, property); 
            }

            return textureStruct;
        }

        /// \english
        /// <summary>
        /// Texture vertical property builder with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="scaleOffset">If enabled, shows the tiling and offset properties.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="toggleLock">Float that locks the property.</param>
        /// <returns>Texture entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo textura vertical con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="scaleOffset">Si está activo, se muestra la propiedad de tiling y offset.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <returns>Textura introducido por el usuario.</returns>
        /// \endspanish
        public static TextureStruct BuildTextureVertical(string propertyName, string propertyDescription, MaterialProperty property, bool scaleOffset, MaterialEditor materialEditor, float toggleLock)
        {

            BuildLabel(propertyName, propertyDescription);

            GUILayout.Space(5);

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            TextureStruct textureStruct = new TextureStruct();

            textureStruct.rect = materialEditor.TexturePropertySingleLine(new GUIContent(""), property);

            //Preview texture.
            Rect rectPreview = EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 8);
            var previewRect = new Rect(rectPreview.x + 35, rectPreview.y - 20, rectPreview.width - 40, rectPreview.height + 1);
            /*if(property.hasMixedValue || (property.textureValue = null))
            {
                var col = GUI.color;
                GUI.color = EditorGUIUtility.isProSkin ? new Color(.25f, .25f, .25f) : new Color(.85f, .85f, .85f);
                EditorGUI.DrawPreviewTexture(previewRect, Texture2D.whiteTexture);
                GUI.color = col;
            }*/
            if(property.textureValue != null)
            {
                EditorGUI.DrawPreviewTexture(previewRect, property.textureValue);
            }

            if (scaleOffset == true)
            {
                Rect rectScaleOffset = EditorGUILayout.GetControlRect(true, EditorGUIUtility.standardVerticalSpacing * 20);
                var scaleOffsetRect = new Rect(rectPreview.x, rectScaleOffset.y - 20, rectScaleOffset.width, rectScaleOffset.height);
                materialEditor.TextureScaleOffsetProperty(scaleOffsetRect, property); 
            }
            
            GUI.enabled = true;

            return textureStruct;
        }

        /// \english
        /// <summary>
        /// Toggle float property builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description-</param>
        /// <param name="property">Property to build.</param>
        /// <param name="multiplier">Multiplies the boolean value.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="multiplier">Multiplica el valor del booleno.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildToggleFloat(string propertyName, string propertyDescription, MaterialProperty property, float multiplier = 1.0f)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue))) * multiplier;

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Toggle float property builder with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <param name="multiplier">Multiplies the boolean value.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="multiplier">Multiplica el valor del booleno.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildToggleFloat(string propertyName, string propertyDescription, MaterialProperty property, float toggleLock, float multiplier = 1.0f)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue))) * multiplier;

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return property.floatValue;

        }


        /// \english
        /// <summary>
        /// Toggle float property builder that manages a keyword.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description-</param>
        /// <param name="property">Property to build.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float que controla una keyword.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildKeyword(string propertyName, string propertyDescription, MaterialProperty property, bool mode)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue)));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            SetKeyword(property, property.floatValue, mode);

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Toggle float property builder that manages a keyword with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float que controla una keyword con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildKeyword(string propertyName, string propertyDescription, MaterialProperty property, bool mode, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue)));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            SetKeyword(property, property.floatValue, mode);

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Toggle float property builder that manages a keyword.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description-</param>
        /// <param name="property">Property to build.</param>
        /// <param name="name">The name of the keyword.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float que controla una keyword.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="name">El nombre de la keyword.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildKeywordWithName(string propertyName, string propertyDescription, MaterialProperty property, string name)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue)));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            SetKeywordWithName(property, property.floatValue, name);

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Toggle float property builder that manages a keyword with locking.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="property">Property to build.</param>
        /// <param name="name">The name of the keyword.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float que controla una keyword con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="name">El nombre de la keyword.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildKeywordWithName(string propertyName, string propertyDescription, MaterialProperty property, string name, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue)));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUILayout.EndHorizontal();

            SetKeywordWithName(property, property.floatValue, name);

            GUI.enabled = true;

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Popup builder to manage a keyword enum.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="options">Name of the elementos of the enumeration.</param>
        /// <param name="property">Popup property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup para gestionar una enumeración de keyword.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="options">Nombre de los elementos de la enumeración.</param>
        /// <param name="property">Propiedad de tipo popup a construir.</param>
        /// \endspanish
        public static void BuildPopup(string propertyName, string propertyDescription, string[] options, MaterialProperty property)
        {

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(propertyName, propertyDescription), style, GUILayout.Height(14));

            EditorGUI.showMixedValue = property.hasMixedValue;

            keywordEnumIndex = property.floatValue;
            
            keywordEnumOptions = options;

            keywordEnumIndex = EditorGUILayout.Popup((int)keywordEnumIndex, keywordEnumOptions);            
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = keywordEnumIndex;

            }

            GUILayout.EndHorizontal();

            SetKeywordEnum(property, keywordEnumOptions);

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Popup builder to manage a keyword enum.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="options">Name of the elementos of the enumeration.</param>
        /// <param name="property">Popup property to build.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup para gestionar una enumeración de keyword.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="options">Nombre de los elementos de la enumeración.</param>
        /// <param name="property">Propiedad de tipo popup a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildPopup(string propertyName, string propertyDescription, string[] options, MaterialProperty property, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(propertyName, propertyDescription), style, GUILayout.Height(14));

            EditorGUI.showMixedValue = property.hasMixedValue;

            keywordEnumIndex = property.floatValue;

            keywordEnumOptions = options;

            keywordEnumIndex = EditorGUILayout.Popup((int)keywordEnumIndex, keywordEnumOptions);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = keywordEnumIndex;

            }

            GUILayout.EndHorizontal();

            SetKeywordEnum(property, keywordEnumOptions);

            EditorGUI.showMixedValue = false;

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Popup builder to manage a float enum.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="options">Name of the elementos of the enumeration.</param>
        /// <param name="property">Popup property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup para gestionar una enumeración de floats.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="options">Nombre de los elementos de la enumeración.</param>
        /// <param name="property">Propiedad de tipo popup a construir.</param>
        /// \endspanish
        public static void BuildPopupFloat(string propertyName, string propertyDescription, string[] options, MaterialProperty property)
        {

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(propertyName, propertyDescription), style, GUILayout.Height(14));

            EditorGUI.showMixedValue = property.hasMixedValue;

            enumIndex = property.floatValue;
            
            enumOptions = options;

            enumIndex = EditorGUILayout.Popup((int)enumIndex, enumOptions);            
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = enumIndex;

            }

            GUILayout.EndHorizontal();

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Popup builder to manage a float enum.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description.</param>
        /// <param name="options">Name of the elementos of the enumeration.</param>
        /// <param name="property">Popup property to build.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup para gestionar una enumeración de floats.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="options">Nombre de los elementos de la enumeración.</param>
        /// <param name="property">Propiedad de tipo popup a construir.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildPopupFloat(string propertyName, string propertyDescription, string[] options, MaterialProperty property, float toggleLock)
        {

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(propertyName, propertyDescription), style, GUILayout.Height(14));

            EditorGUI.showMixedValue = property.hasMixedValue;

            enumIndex = property.floatValue;

            enumOptions = options;

            enumIndex = EditorGUILayout.Popup((int)enumIndex, enumOptions);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = enumIndex;

            }

            GUILayout.EndHorizontal();

            EditorGUI.showMixedValue = false;

            GUI.enabled = true;

        }


    #endregion

    #region Utility Methods

        /// <summary>
        /// Copia y pega los valores de un material.
        /// </summary>
        /// <param name="materialEditor">Editor del material.</param>
        public static void ManageMaterialValues(MaterialEditor materialEditor)
        {

            Material mat = materialEditor.target as Material;

            if (mat == null)
                return;

            GUILayout.BeginHorizontal("Toolbar");

            GUILayout.Space(10.0f);

            GUILayout.Label("Copied: " + (_copiedComponent ?? "Nothing"));

            GUILayout.FlexibleSpace();

            if (GUILayout.RepeatButton(new GUIContent("Copy", "Copy component values"), "toolbarButton"))
            {

                Shader shader = mat.shader;

                int propertyCount = UnityEditor.ShaderUtil.GetPropertyCount(shader);

                string allProperties = string.Empty;

                for (int i = 0; i < propertyCount; i++)
                {

                    UnityEditor.ShaderUtil.ShaderPropertyType type = UnityEditor.ShaderUtil.GetPropertyType(shader, i);

                    string propertyName = UnityEditor.ShaderUtil.GetPropertyName(shader, i);

                    string valueStr = string.Empty;

                    switch (type)
                    {
                        case UnityEditor.ShaderUtil.ShaderPropertyType.Color:
                            {

                                Color value = mat.GetColor(propertyName);

                                valueStr = value.r.ToString() + '|' +
                                            value.g.ToString() + '|' +
                                            value.b.ToString() + '|' +
                                            value.a.ToString();

                            }
                            break;
                        case UnityEditor.ShaderUtil.ShaderPropertyType.Vector:
                            {

                                Vector4 value = mat.GetVector(propertyName);

                                valueStr = value.x.ToString() + '|' +
                                            value.y.ToString() + '|' +
                                            value.z.ToString() + '|' +
                                            value.w.ToString();

                            }
                            break;
                        case UnityEditor.ShaderUtil.ShaderPropertyType.Float:
                            {

                                float value = mat.GetFloat(propertyName);

                                valueStr = value.ToString();
                            }

                            break;
                        case UnityEditor.ShaderUtil.ShaderPropertyType.Range:
                            {

                                float value = mat.GetFloat(propertyName);

                                valueStr = value.ToString();

                            }

                            break;
                        case UnityEditor.ShaderUtil.ShaderPropertyType.TexEnv:
                            {

                                Texture value = mat.GetTexture(propertyName);

                                valueStr = AssetDatabase.GetAssetPath(value);

                            }

                            break;
                    }

                    allProperties += propertyName + ';' + type + ';' + valueStr;

                    if (i < (propertyCount - 1))
                    {

                        allProperties += '\n';

                    }

                }

                EditorPrefs.SetString("CGFMATCLIPBRDID", allProperties);

                _copiedComponent = materialEditor.target.name;

            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Paste", "Paste component values"), "toolbarButton"))
            {

                string propertiesStr = EditorPrefs.GetString("CGFMATCLIPBRDID", string.Empty);

                if (!string.IsNullOrEmpty(propertiesStr))
                {

                    string[] propertyArr = propertiesStr.Split('\n');

                    try
                    {

                        for (int i = 0; i < propertyArr.Length; i++)
                        {

                            string[] valuesArr = propertyArr[i].Split(';');

                            if (valuesArr.Length != 3)
                            {

                                break;

                            }                            
                            else if (mat.HasProperty(valuesArr[0]))
                            {

                                UnityEditor.ShaderUtil.ShaderPropertyType type = (UnityEditor.ShaderUtil.ShaderPropertyType)Enum.Parse( typeof(UnityEditor.ShaderUtil.ShaderPropertyType), valuesArr[1]);

                                switch (type)
                                {
                                    case UnityEditor.ShaderUtil.ShaderPropertyType.Color:
                                        {

                                            string[] colorVals = valuesArr[2].Split('|');

                                            if (colorVals.Length != 4)
                                            {

                                                break;

                                            }
                                            else
                                            {

                                                mat.SetColor(valuesArr[0], new Color(Convert.ToSingle(colorVals[0]),
                                                    Convert.ToSingle(colorVals[1]),
                                                    Convert.ToSingle(colorVals[2]),
                                                    Convert.ToSingle(colorVals[3])));

                                            }
                                        }
                                        break;
                                    case UnityEditor.ShaderUtil.ShaderPropertyType.Vector:
                                        {

                                            string[] vectorVals = valuesArr[2].Split('|');

                                            if (vectorVals.Length != 4)
                                            {

                                                break;

                                            }
                                            else
                                            {

                                                mat.SetVector(valuesArr[0], new Vector4(Convert.ToSingle(vectorVals[0]),
                                                    Convert.ToSingle(vectorVals[1]),
                                                    Convert.ToSingle(vectorVals[2]),
                                                    Convert.ToSingle(vectorVals[3])));

                                            }

                                        }

                                        break;

                                    case UnityEditor.ShaderUtil.ShaderPropertyType.Float:
                                        {

                                            mat.SetFloat(valuesArr[0], Convert.ToSingle(valuesArr[2]));

                                        }

                                        break;

                                    case UnityEditor.ShaderUtil.ShaderPropertyType.Range:
                                        {

                                            mat.SetFloat(valuesArr[0], Convert.ToSingle(valuesArr[2]));

                                        }

                                        break;
                                    case UnityEditor.ShaderUtil.ShaderPropertyType.TexEnv:
                                        {

                                            mat.SetTexture(valuesArr[0], AssetDatabase.LoadAssetAtPath<Texture>(valuesArr[2]));

                                        }

                                        break;
                                }

                            }

                        }

                    }
                    catch (Exception e)
                    {

                        Debug.LogException(e);

                    }

                }

            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

        }

        /// \english
        /// <summary>
        /// Label builder.
        /// </summary>
        /// <param name="text">Label text.</param>
        /// <param name="description">Label description.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una etiqueta de texto.
        /// </summary>
        /// <param name="text">Texto de la etiqueta de texto.</param>
        /// <param name="description">Descripción de la etiqueta de texto.</param>
        /// \endspanish
        public static void BuildLabel(string text, string description)
        {
            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.fontStyle = FontStyle.Normal;

            style.alignment = TextAnchor.MiddleLeft;

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(text, description), style, GUILayout.Height(14));

        }

        /// \english
        /// <summary>
        /// Header builder.
        /// </summary>
        /// <param name="text">Header text.</param>
        /// <param name="description">Header description.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un título.
        /// </summary>
        /// <param name="text">Texto del título.</param>
        /// <param name="description">Descripción del título.</param>
        /// \endspanish
        public static void BuildHeader(string text, string description)
        {
            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.fontStyle = FontStyle.Bold;

            style.alignment = TextAnchor.MiddleLeft;

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(text, description), style, GUILayout.Height(14));

        }

        /// \english
        /// <summary>
        /// Header builder with float toggle.
        /// </summary>
        /// <param name="text">Header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un título.
        /// </summary>
        /// <param name="text">Texto del título</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// \endspanish
        public static float BuildHeaderWithToggleFloat(string text, string description, MaterialProperty property)
        {

            GUILayout.BeginHorizontal();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.fontStyle = FontStyle.Bold;

            style.alignment = TextAnchor.MiddleLeft;

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(text, description), style, GUILayout.Height(14));

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            bool value = GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable");

            if (value)
            {

                property.floatValue = 1;

            }
            else
            {

                property.floatValue = 0;

            }
            
            GUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Header builder with float toggle to manage a keyword.
        /// </summary>
        /// <param name="text">Header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un título con un toggle float para gestionar una keyword.
        /// </summary>
        /// <param name="text">Texto del título</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// \endspanish
        public static float BuildHeaderWithKeyword(string text, string description, MaterialProperty property, bool mode)
        {

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.fontStyle = FontStyle.Bold;

            style.alignment = TextAnchor.MiddleLeft;

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(text, description), style, GUILayout.Height(14));

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            float value = Convert.ToInt16(GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable"));

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = value;

            }           

            GUILayout.EndHorizontal();

            SetKeyword(property, property.floatValue, mode);

            return property.floatValue;

        }


        /// \english
        /// <summary>
        /// Header builder with popup to manage a keyword enum.
        /// </summary>
        /// <param name="text">Header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="options">Name of the elementos of the enumeration.</param>
        /// <param name="property">Popup property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un título con un popup para gestionar una enumeración de keyword.
        /// </summary>
        /// <param name="text">Texto del título</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="options">Nombre de los elementos de la enumeración.</param>
        /// <param name="property">Propiedad de tipo popup a construir.</param>
        /// \endspanish
        public static void BuildHeaderWithPopup(string text, string description, string[] options, MaterialProperty property)
        {

            GUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            GUIStyle style = new GUIStyle();

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            style.fontStyle = FontStyle.Bold;

            style.alignment = TextAnchor.MiddleLeft;

            style.contentOffset = new Vector2(2, 0);

            EditorGUILayout.LabelField(new GUIContent(text, description), style, GUILayout.Height(14));

            EditorGUI.showMixedValue = property.hasMixedValue;

            keywordEnumIndex = property.floatValue;

            keywordEnumOptions  = options;

            keywordEnumIndex = EditorGUILayout.Popup((int)keywordEnumIndex, keywordEnumOptions);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = keywordEnumIndex;

            }

            GUILayout.EndHorizontal();

            SetKeywordEnum(property, keywordEnumOptions);

            EditorGUI.showMixedValue = false;

        }


        /// \english
        /// <summary>
        /// FoldOut header group builder.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout de grupo con título.
        /// </summary>
        /// <param name="text">Texto del título del foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static bool BuildFoldOutHeaderGroup(string text, string description, bool bold, bool isUnfold, string playerPrefKeyName)
        {
            string uniqueIdentifier = Regex.Replace(text, @"\s+", String.Empty);

            if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
            {
                PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
                PlayerPrefs.Save();
            }
            
            isUnfold = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);
            
            GUIStyle style = EditorStyles.foldoutHeader;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.BeginFoldoutHeaderGroup(isUnfold, new GUIContent(text, description), style);
            EditorGUILayout.EndFoldoutHeaderGroup();

            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
            PlayerPrefs.Save();

            return isUnfold;

        }

        /// \english
        /// <summary>
        /// FoldOut header group with toggle float builder.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout de grupo con título con toggle float.
        /// </summary>
        /// <param name="text">Texto del título del foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static bool BuildFoldOutHeaderGroupWithToggleFloat(string text, string description, MaterialProperty property, bool bold, bool isUnfold, string playerPrefKeyName)
        {
            
            string uniqueIdentifier = Regex.Replace(text, @"\s+", String.Empty);

            if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
            {
                PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
                PlayerPrefs.Save();
            }
            
            isUnfold = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);
            

            GUILayout.BeginHorizontal();
            
            GUIStyle style = EditorStyles.foldoutHeader;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.BeginFoldoutHeaderGroup(isUnfold, new GUIContent(text, description), style);

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            bool value = GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable");

            if (value)
            {

                property.floatValue = 1;

            }
            else
            {

                property.floatValue = 0;

            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
            PlayerPrefs.Save();

            GUILayout.EndHorizontal();

            return isUnfold;

        }

        /// \english
        /// <summary>
        /// FoldOut header group builder with float toggle to manage a keyword.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout con un toggle float para gestionar una keyword.
        /// </summary>
        /// <param name="text">Texto del título del foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static bool BuildFoldOutHeaderGroupWithKeyword(string text, string description, MaterialProperty property, bool bold, bool isUnfold, bool mode, string playerPrefKeyName)
        {
            
            string uniqueIdentifier = Regex.Replace(text, @"\s+", String.Empty);

            if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
            {
                PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
                PlayerPrefs.Save();
            }
            
            isUnfold = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);
            

            GUILayout.BeginHorizontal();
            
            GUIStyle style = EditorStyles.foldoutHeader;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.BeginFoldoutHeaderGroup(isUnfold, new GUIContent(text, description), style);

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            bool value = GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable");

            if (value)
            {

                property.floatValue = 1;

            }
            else
            {

                property.floatValue = 0;

            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (isUnfold ? 1 : 0));
            PlayerPrefs.Save();

            GUILayout.EndHorizontal();

            SetKeyword(property, property.floatValue, mode);

            return isUnfold;

        }

        /// \english
        /// <summary>
        /// FoldOut builder.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout.
        /// </summary>
        /// <param name="text">Texto del título del foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// \endspanish
        public static bool BuildFoldOut(string text, string description, bool bold, bool isUnfold)
        {

            GUIStyle style = EditorStyles.foldout;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.Foldout(isUnfold, new GUIContent(text, description), style);

            return isUnfold;

        }

        /// \english
        /// <summary>
        /// FoldOut builder with float toggle.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout con un toggle float.
        /// </summary>
        /// <param name="text">Texto del título del foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// \endspanish
        public static bool BuildFoldOutWithToggleFloat(string text, string description, MaterialProperty property, bool bold, bool isUnfold)
        {

            GUILayout.BeginHorizontal();

            GUIStyle style = EditorStyles.foldout;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.Foldout(isUnfold, new GUIContent(text, description), style);

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            bool value = GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable");

            if (value)
            {

                property.floatValue = 1;

            }
            else
            {

                property.floatValue = 0;

            }

            GUILayout.EndHorizontal();

            return isUnfold;

        }

        /// \english
        /// <summary>
        /// FoldOut builder with float toggle to manage a keyword.
        /// </summary>
        /// <param name="text">Foldout header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// <param name="bold">If true, the text has a bold style.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un foldout con un toggle float para gestionar una keyword.
        /// </summary>
        /// <param name="text">Texto del título dle foldout.</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// <param name="bold">Si está activo, el texto tiene un estilo en negrita.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// \endspanish
        public static bool BuildFoldOutWithKeyword(string text, string description, MaterialProperty property, bool bold, bool isUnfold, bool mode)
        {

            GUILayout.BeginHorizontal();

            GUIStyle style = EditorStyles.foldout;

            if (EditorGUIUtility.isProSkin)
            {

                style.normal.textColor = new Color(0.70f, 0.70f, 0.70f);

            }

            if (bold)
            {

                style.fontStyle = FontStyle.Bold;

            }

            style.alignment = TextAnchor.MiddleLeft;

            isUnfold = EditorGUILayout.Foldout(isUnfold, new GUIContent(text, description), style);

            Rect rect = GUILayoutUtility.GetRect(55f, 20f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

            bool value = GUI.Toggle(rect, Convert.ToBoolean(property.floatValue), "Enable");

            if (value)
            {

                property.floatValue = 1;

            }
            else
            {

                property.floatValue = 0;

            }

            GUILayout.EndHorizontal();

            SetKeyword(property, property.floatValue, mode);

            return isUnfold;

        }
        
        /// \english
        /// <summary>
        /// Documentation link tool builder.
        /// </summary>
        /// <param name="documentationURL">Documentation URL.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la herramienta del enlace de la documentación.
        /// </summary>
        /// <param name="documentationURL">URL de la documentación.</param>
        /// \endspanish
        public static void BuildMaterialTools(string documentationURL)
        {
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Documentation", EditorStyles.miniLabel))
            {

                Application.OpenURL(documentationURL);

            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

        }

        /// \english
        /// <summary>
        /// Build that shows a information message to user to explains that the user can use a script to manage the material.
        /// </summary>
        /// <param name="component">Type of the script that manages the material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor que muestra un mensaje de aviso al usuario de que puede utilizar un script para gestionar el material.
        /// </summary>
        /// <param name="component">Tipo del script que gestiona el material.</param>
        /// \endspanish
        public static void BuildMaterialComponent(Type component)
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

                if (Selection.activeGameObject == null)
                {

                    GUILayout.BeginHorizontal();

                    EditorGUILayout.HelpBox("You can use " + component.Name + " component to manage this material.", MessageType.Info, true);

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    GUILayout.FlexibleSpace();

                }
                else
                {

                    GUILayout.BeginHorizontal();

                    EditorGUILayout.HelpBox("You can use " + component.Name + " component to manage this material.", MessageType.Info, true);

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Add Material Component"))
                    {

                        Selection.activeGameObject.AddComponent(component);

                    }

                }

                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

            }

        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Opaque, Transparent, Cutout, Background.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Opaque, Transparent, Cutout, Background.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeEnum(MaterialProperty property, MaterialEditor materialEditor, bool showTitle = true)
        {
            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderMode renderMode = (RenderMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            if(showTitle == true)
            {
                BuildHeader("Render Mode", "Rendering mode.");
            }

            renderMode = (RenderMode)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderMode, renderModeNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = (float)renderMode;

                SetRenderMode(material, renderMode);
            }

            EditorGUI.showMixedValue = false;
        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Opaque, Transparent, Cutout, Background header group.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="showGizmo">Show gizmo</param>
	    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Opaque, Transparent, Cutout, Background grupo.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="showGizmo">Muestra el gizmo.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static void BuildRenderModeEnum(MaterialProperty property, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
        {
            isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Render Mode", "Rendering mode.", true, isUnfold, playerPrefKeyName);

            if (isUnfold)
            {
                EditorGUI.showMixedValue = property.hasMixedValue;

                RenderMode renderMode = (RenderMode)property.floatValue;

                Material material = materialEditor.target as Material;

                EditorGUI.BeginChangeCheck();

                renderMode = (RenderMode)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderMode, renderModeNames);
                
                if (EditorGUI.EndChangeCheck())
                {
                    property.floatValue = (float)renderMode;

                    SetRenderMode(material, renderMode);
                }

                EditorGUI.showMixedValue = false;
            }
        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Transparent, Cutout.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Transparent, Cutout.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeTransparentEnum(MaterialProperty property, MaterialEditor materialEditor, bool showHeader = true)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeTransparent renderModeTransparent = (RenderModeTransparent)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            if (showHeader == true)
            {
                BuildHeader("Render Mode", "Rendering mode.");
            }

            renderModeTransparent = (RenderModeTransparent)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeTransparent, renderModeTransparentNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeTransparent;

                SetRenderModeTransparent(material, renderModeTransparent);

            }

            EditorGUI.showMixedValue = false;
        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Transparent, Cutout header group.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
	    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Transparent, Cutout grupo.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static void BuildRenderModeTransparentEnum(MaterialProperty property, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
        {
            isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Render Mode", "Rendering mode.", true, isUnfold, playerPrefKeyName);

            if (isUnfold)
            {
                EditorGUI.showMixedValue = property.hasMixedValue;

                RenderModeTransparent renderModeTransparent = (RenderModeTransparent)property.floatValue;

                Material material = materialEditor.target as Material;

                EditorGUI.BeginChangeCheck();

                renderModeTransparent = (RenderModeTransparent)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeTransparent, renderModeTransparentNames);
                
                if (EditorGUI.EndChangeCheck())
                {
                    
                    property.floatValue = (float)renderModeTransparent;

                    SetRenderModeTransparent(material, renderModeTransparent);

                }

                EditorGUI.showMixedValue = false;
            }
        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeFullEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeStandard renderModeStandard = (RenderModeStandard)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Render Mode", "Rendering mode.");

            renderModeStandard = (RenderModeStandard)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeStandard, renderModeStandardNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeStandard;

                SetRenderModeStandard(material, renderModeStandard);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeFullTransparentEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeStandardTransparent renderModeStandardTransparent = (RenderModeStandardTransparent)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Render Mode", "Rendering mode.");

            renderModeStandardTransparent = (RenderModeStandardTransparent)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeStandardTransparent, renderModeStandardTransparentNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeStandardTransparent;

                SetRenderModeStandardTransparent(material, renderModeStandardTransparent);

            }

            EditorGUI.showMixedValue = false;

        }


        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Opaque, Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeStandardEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeStandard renderModeStandard = (RenderModeStandard)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Render Mode", "Rendering mode.");

            renderModeStandard = (RenderModeStandard)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeStandard, renderModeStandardNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeStandard;

                SetRenderModeStandard(material, renderModeStandard);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Cutout, Fade, Transparent.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeStandardTransparentEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeStandardTransparent renderModeStandardTransparent = (RenderModeStandardTransparent)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Render Mode", "Rendering mode.");

            renderModeStandardTransparent = (RenderModeStandardTransparent)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeStandardTransparent, renderModeStandardTransparentNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeStandardTransparent;

                SetRenderModeStandardTransparent(material, renderModeStandardTransparent);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder. Opaque, Transparent.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado. Opaque, Transparent.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeLiteEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            RenderModeLite renderModeLite = (RenderModeLite)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Render Mode", "Rendering mode.");

            renderModeLite = (RenderModeLite)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderModeLite, renderModeLiteNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)renderModeLite;

                SetRenderModeLite(material, renderModeLite);

            }

            EditorGUI.showMixedValue = false;

        }
        
        /// \english
        /// <summary>
        /// Toggle float property builder to change the render mode to transparent or cutout.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description-</param>
        /// <param name="property">Property to build.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="multiplier">Multiplies the boolean value.</param>
        /// <returns>Float value entered by the user.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de una propiedad de tipo toggle float para cambiar el modo de renderizado a transparente o cutout.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="multiplier">Multiplica el valor del booleno.</param>
        /// <returns>Valor float introducido por el usuario.</returns>
        /// \endspanish
        public static float BuildTransparentCutoffModeToggle(string propertyName, string propertyDescription, MaterialProperty property, MaterialEditor materialEditor, float multiplier = 1.0f)
        {
            
            Material material = materialEditor.target as Material;

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMixedValue;

            float value = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue))) * multiplier;

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = value;

                SetTransparentCutoffMode(material, property.floatValue);

            }

            EditorGUILayout.EndHorizontal();

            return property.floatValue;

        }

        /// \english
        /// <summary>
        /// Build the render face popup.
        /// </summary>
        /// <param name="cull">Cull property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor del popup de renerizado de caras.
        /// </summary>
        /// <param name="cull">Propiedad cull.</param>
        /// \endspanish
        public static void BuildRenderFace(MaterialProperty cull)
        {
            CGFMaterialEditorUtilitiesClass.BuildPopupFloat("Render Face", "Determine which sides of mesh to render.", new string[]{"Booth", "Back", "Front"}, cull);
        }

        /// \english
        /// <summary>
        /// Blending mode enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the blending mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de fusión.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de fusión.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildBlendModeEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            BlendMode blendMode = (BlendMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Blend Mode", "Blending mode.");

            blendMode = (BlendMode)EditorGUILayout.Popup(new GUIContent("Blend Mode", "Blending mode."), (int)blendMode, blendModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)blendMode;

                SetBlendMode(material, blendMode);

                EditorUtility.SetDirty(material);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Blending mode enumeration builder header group.
        /// </summary>
        /// <param name="property">Property that manages the blending mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
	    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de fusión grupo.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de fusión.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static void BuildBlendModeEnum(MaterialProperty property, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
        {
            isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Blend Mode", "Blending mode.", true, isUnfold, playerPrefKeyName);

            if (isUnfold)
            {
                EditorGUI.showMixedValue = property.hasMixedValue;

                BlendMode blendMode = (BlendMode)property.floatValue;

                Material material = materialEditor.target as Material;

                EditorGUI.BeginChangeCheck();

                blendMode = (BlendMode)EditorGUILayout.Popup(new GUIContent("Blend Mode", "Blending mode."), (int)blendMode, blendModeNames);

                if (EditorGUI.EndChangeCheck())
                {

                    property.floatValue = (float)blendMode;

                    SetBlendMode(material, blendMode);

                    EditorUtility.SetDirty(material);

                }

                EditorGUI.showMixedValue = false;
            }
        }

        /// \english
        /// <summary>
        /// Blending mode enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the blending mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de fusión.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de fusión.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildBlendModeEnumWithoutKeyword(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            BlendMode blendMode = (BlendMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Blend Mode", "Blending mode.");

            blendMode = (BlendMode)EditorGUILayout.Popup(new GUIContent("Blend Mode", "Blending mode."), (int)blendMode, blendModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)blendMode;

                EditorUtility.SetDirty(material);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Blending type enumeration builder.
        /// </summary>
        /// <param name="propertyBlendType">Property that manages the blending type.</param>
        /// <param name="propertyBlendSource">Property that manages the blending source.</param>
        /// <param name="propertyBlendDestination">Property that manages the blending destination.</param>
        /// <param name="propertyBlendOperation">Property that manages the blending operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de tipo de fusión.
        /// </summary>
        /// <param name="propertyBlendType">Propiedad que gestiona el tipo de fusión.</param>
        /// <param name="propertyBlendSource">Propiedad que gestiona la fuente de la fusión.</param>
        /// <param name="propertyBlendDestination">Propiedad que gestiona el destinación de la fusión.</param>
        /// <param name="propertyBlendOperation">Propiedad que gestiona la operación de fusión</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildBlendTypeEnum(MaterialProperty propertyBlendType, MaterialProperty propertyBlendSource, MaterialProperty propertyBlendDestination, MaterialProperty propertyBlendOperation, MaterialEditor materialEditor)
        {
            EditorGUI.showMixedValue = propertyBlendType.hasMixedValue;
            EditorGUI.showMixedValue = propertyBlendSource.hasMixedValue;
            EditorGUI.showMixedValue = propertyBlendDestination.hasMixedValue;
            EditorGUI.showMixedValue = propertyBlendOperation.hasMixedValue;

            BlendType blendType = (BlendType)propertyBlendType.floatValue;
            BlendFactor blendFactorSource = (BlendFactor)propertyBlendSource.floatValue;
            BlendFactor blendFactorDestination = (BlendFactor)propertyBlendDestination.floatValue;
            BlendOp blendOperation = (BlendOp)propertyBlendOperation.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Blend Type", "Blending type.");

            blendType = (BlendType)EditorGUILayout.Popup(new GUIContent("Blend Type", "Blending type."), (int)blendType, blendTypeNames);

            if (blendType == 0)
            {
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
            }

            blendFactorSource = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Source Factor", "Blending source factor."), (int)blendFactorSource, blendFactorNames);

            blendFactorDestination = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Destination Factor", "Blending destination factor."), (int)blendFactorDestination, blendFactorNames);

            blendOperation = (BlendOp)EditorGUILayout.Popup(new GUIContent("Blend Operation", "Blending operation."), (int)blendOperation, blendOperationNames);

            if (EditorGUI.EndChangeCheck())
            {

                propertyBlendType.floatValue = (float)blendType;
                propertyBlendSource.floatValue = (float)blendFactorSource;
                propertyBlendDestination.floatValue = (float)blendFactorDestination;
                propertyBlendOperation.floatValue = (float)blendOperation;

                SetBlendType(material, blendType, blendFactorSource, blendFactorDestination, blendOperation);

                EditorUtility.SetDirty(material);

            }

            EditorGUI.showMixedValue = false;

            GUI.enabled = true;
        }
        
        

        /// \english
        /// <summary>
        /// Blending type enumeration builder header group.
        /// </summary>
        /// <param name="propertyBlendType">Property that manages the blending type.</param>
        /// <param name="propertyBlendSource">Property that manages the blending source.</param>
        /// <param name="propertyBlendDestination">Property that manages the blending destination.</param>
        /// <param name="propertyBlendOperation">Property that manages the blending operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
        /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de tipo de fusión grupo.
        /// </summary>
        /// <param name="propertyBlendType">Propiedad que gestiona el tipo de fusión.</param>
        /// <param name="propertyBlendSource">Propiedad que gestiona la fuente de la fusión.</param>
        /// <param name="propertyBlendDestination">Propiedad que gestiona el destinación de la fusión.</param>
        /// <param name="propertyBlendOperation">Propiedad que gestiona la operación de fusión</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static void BuildBlendTypeEnum(MaterialProperty propertyBlendType, MaterialProperty propertyBlendSource, MaterialProperty propertyBlendDestination, MaterialProperty propertyBlendOperation, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
        {
            isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Blend Type", "Blending type.", true, isUnfold, playerPrefKeyName);

            if (isUnfold)
            {
                EditorGUI.showMixedValue = propertyBlendType.hasMixedValue;
                EditorGUI.showMixedValue = propertyBlendSource.hasMixedValue;
                EditorGUI.showMixedValue = propertyBlendDestination.hasMixedValue;
                EditorGUI.showMixedValue = propertyBlendOperation.hasMixedValue;

                BlendType blendType = (BlendType)propertyBlendType.floatValue;
                BlendFactor blendFactorSource = (BlendFactor)propertyBlendSource.floatValue;
                BlendFactor blendFactorDestination = (BlendFactor)propertyBlendDestination.floatValue;
                BlendOp blendOperation = (BlendOp)propertyBlendOperation.floatValue;

                Material material = materialEditor.target as Material;

                EditorGUI.BeginChangeCheck();

                blendType = (BlendType)EditorGUILayout.Popup(new GUIContent("Blend Type", "Blending type."), (int)blendType, blendTypeNames);

                if (blendType == 0)
                {
                    GUI.enabled = true;
                }
                else
                {
                    GUI.enabled = false;
                }

                blendFactorSource = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Source Factor", "Blending source factor."), (int)blendFactorSource, blendFactorNames);

                blendFactorDestination = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Destination Factor", "Blending destination factor."), (int)blendFactorDestination, blendFactorNames);

                blendOperation = (BlendOp)EditorGUILayout.Popup(new GUIContent("Blend Operation", "Blending operation."), (int)blendOperation, blendOperationNames);

                if (EditorGUI.EndChangeCheck())
                {

                    propertyBlendType.floatValue = (float)blendType;
                    propertyBlendSource.floatValue = (float)blendFactorSource;
                    propertyBlendDestination.floatValue = (float)blendFactorDestination;
                    propertyBlendOperation.floatValue = (float)blendOperation;

                    SetBlendType(material, blendType, blendFactorSource, blendFactorDestination, blendOperation);

                    EditorUtility.SetDirty(material);

                }

                EditorGUI.showMixedValue = false;

                GUI.enabled = true;
            } 
        }

        /// \english
        /// <summary>
        /// Cull mode enumeration builder. The default value is setted in the shader property.
        /// </summary>
        /// <param name="property">Property that manages the cull mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de oclusión. El valor por defecto se establece en la propiedad del shader.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de oclusión.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildCullModeEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            CullMode cullMode = (CullMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Cull Mode", "Culling mode.");

            cullMode = (CullMode)EditorGUILayout.Popup(new GUIContent("Cull Mode", "Culling mode."), (int)cullMode, cullModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)cullMode;

                SetCullMode(material, cullMode);

                EditorUtility.SetDirty(material);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Dpeth mode enumeration builder. The default value is setted in the shader property.
        /// </summary>
        /// <param name="property">Property that manages the depth mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de profundidad. El valor por defecto se establece en la propiedad del shader.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de profundidad.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildDepthModeEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            DepthMode depthMode = (DepthMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Depth Mode", "Controls whether pixels from this object are written to the depth buffer.");

            depthMode = (DepthMode)EditorGUILayout.Popup(new GUIContent("Depth Mode", "Controls whether pixels from this object are written to the depth buffer."), (int)depthMode, depthModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)depthMode;

                SetDepthMode(material, depthMode);

                EditorUtility.SetDirty(material);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Stencil compare operation enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the stencil compare operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de la operación de comparación del stencil.
        /// </summary>
        /// <param name="property">Propiedad que gestiona la operación de comparación del stencil.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildStencilCompareFunction(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            StencilCompareFunction stencilCompareFunction = (StencilCompareFunction)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            stencilCompareFunction = (StencilCompareFunction)EditorGUILayout.Popup(new GUIContent("Stencil Compare Function", "The function used to compare the reference value to the current contents of the buffer. Default: always."), (int)stencilCompareFunction, stencilCompareFunctionNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)stencilCompareFunction;

                SetStencilCompareFunction(material, stencilCompareFunction);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Stencil pass compare operation enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the stencil pass compare operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de la operación de comparación del pass del stencil.
        /// </summary>
        /// <param name="property">Propiedad que gestiona la operación de comparación del pass del stencil.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildStencilPassOperation(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            StencilPassOperation stencilPassOperation = (StencilPassOperation)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            stencilPassOperation = (StencilPassOperation)EditorGUILayout.Popup(new GUIContent("Stencil Pass Operation", "What to do with the contents of the buffer if the stencil test (and the depth test) passes. Default: keep."), (int)stencilPassOperation, stencilPassOperationNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)stencilPassOperation;

                SetStencilPassOperation(material, stencilPassOperation);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Stencil fail compare operation enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the stencil fail compare operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de la operación de comparación del fail del stencil.
        /// </summary>
        /// <param name="property">Propiedad que gestiona la operación de comparación del fail del stencil.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildStencilFailOperation(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            StencilFailOperation stencilFailOperation = (StencilFailOperation)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            stencilFailOperation = (StencilFailOperation)EditorGUILayout.Popup(new GUIContent("Stencil Fail Operation", "What to do with the contents of the buffer if the stencil test fails. Default: keep."), (int)stencilFailOperation, stencilFailOperationNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)stencilFailOperation;

                SetStencilFailOperation(material, stencilFailOperation);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Stencil zfail compare operation enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the stencil zfail compare operation.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de la operación de comparación del zfail del stencil.
        /// </summary>
        /// <param name="property">Propiedad que gestiona la operación de comparación del zfail del stencil.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildStencilZFailOperation(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            StencilZFailOperation stencilZFailOperation = (StencilZFailOperation)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            stencilZFailOperation = (StencilZFailOperation)EditorGUILayout.Popup(new GUIContent("Stencil ZFail Operation", "What to do with the contents of the buffer if the stencil test passes, but the depth test fails. Default: keep."), (int)stencilZFailOperation, stencilZFailOperationNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)stencilZFailOperation;

                SetStencilZFailOperation(material, stencilZFailOperation);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Surface rendering mode enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de modo de renderizado surface.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildSurfaceRenderModeEnum(MaterialProperty property, MaterialEditor materialEditor)
        {

            EditorGUI.showMixedValue = property.hasMixedValue;

            SurfaceRenderMode surfaceRenderMode = (SurfaceRenderMode)property.floatValue;

            Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            BuildHeader("Surface Render Mode", "Surface rendering mode.");

            surfaceRenderMode = (SurfaceRenderMode)EditorGUILayout.Popup(new GUIContent("Surface Render Mode", "Surface rendering mode."), (int)surfaceRenderMode, surfaceRenderModeNames);
            
            if (EditorGUI.EndChangeCheck())
            {
                
                property.floatValue = (float)surfaceRenderMode;

                SetSurfaceRenderMode(material, surfaceRenderMode);

            }

            EditorGUI.showMixedValue = false;

        }

        /// \english
        /// <summary>
        /// Build of the toggle to manage the texture compact mode layout.
        /// </summary>
        /// <param name="enable">Initial status.</param>
        /// <returns>Compact mode status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor del toggle que gestiona la disposición compacta de las texturas.
        /// </summary>
        /// <param name="enable">Estado inicial.</param>
        /// <returns>Estado del modo compacto.</returns>
        /// \endspanish
        public static bool BuildTextureCompactMode (bool enable)
        {
            enable = EditorGUILayout.Toggle(new GUIContent("Compact Mode", "Use a compact layout to show texture properties."), enable);

            return enable;
        }

        /// \english
        /// <summary>
        /// Build of the toggle to manage the texture compact mode layout with player pref.
        /// </summary>
        /// <param name="enable">Initial status.</param>
        /// <returns>Compact mode status.</returns>
        /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor del toggle que gestiona la disposición compacta de las texturas con player pref.
        /// </summary>
        /// <param name="enable">Estado inicial.</param>
        /// <returns>Estado del modo compacto.</returns>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static bool BuildTextureCompactMode (bool enable, string playerPrefKeyName)
        {

            string uniqueIdentifier = Regex.Replace("CompactMode", @"\s+", String.Empty);

            if(!PlayerPrefs.HasKey($"{playerPrefKeyName}.{uniqueIdentifier}"))
            {
                PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (enable ? 1 : 0));
                PlayerPrefs.Save();
            }
            
            enable = (PlayerPrefs.GetInt($"{playerPrefKeyName}.{uniqueIdentifier}") != 0);


            enable = EditorGUILayout.Toggle(new GUIContent("Compact Mode", "Use a compact layout to show texture properties."), enable);


            PlayerPrefs.SetInt($"{playerPrefKeyName}.{uniqueIdentifier}", (enable ? 1 : 0));
            PlayerPrefs.Save();

            return enable;
        }

        /// \english
        /// <summary>
        /// Other settings builder. Includes render queue, enable instancing and lightmap emission properties.
        /// </summary>
        /// <param name="renderQueue">If enabled, shows render queue property.</param>
        /// <param name="enableInstancing">If enabled, shows enable instancing property.</param>
        /// <param name="lightmapEmission">If enabled, shows enable lightmap emission property.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de las configuraciones adicionales. Incluye las propiedades de render queue, enable instancing y lightmap emission.
        /// </summary>
        /// <param name="renderQueue">Si está activo, muestra la propiedad de render queue.</param>
        /// <param name="enableInstancing">Si está activo, muestra la propiedad de instancing.</param>
        /// <param name="lightmapEmission">Si está activo, muestra la propiedad de lightmap emission.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildOtherSettings(bool renderQueue, bool enableInstancing, bool lightmapEmission, bool doubleSidedGI, MaterialEditor materialEditor)
        {
            BuildHeader("Other Settings", "");

            if (renderQueue)
            {
                materialEditor.RenderQueueField();
            }

            if (enableInstancing)
            {
                materialEditor.EnableInstancingField();
            }

            if (lightmapEmission)
            {
                materialEditor.LightmapEmissionProperty();
            }

            if (doubleSidedGI)
            {
                materialEditor.DoubleSidedGIField();
            }
        }


        /// \english
        /// <summary>
        /// Other settings builder. Includes render queue, enable instancing and lightmap emission properties header group.
        /// </summary>
        /// <param name="renderQueue">If enabled, shows render queue property.</param>
        /// <param name="enableInstancing">If enabled, shows enable instancing property.</param>
        /// <param name="lightmapEmission">If enabled, shows enable lightmap emission property.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// <param name="isUnfold">Manage de foldout status.</param>
	    /// <param name="playerPrefKeyName">Name of the player pref key.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de las configuraciones adicionales. Incluye las propiedades de render queue, enable instancing y lightmap emission grupo.
        /// </summary>
        /// <param name="renderQueue">Si está activo, muestra la propiedad de render queue.</param>
        /// <param name="enableInstancing">Si está activo, muestra la propiedad de instancing.</param>
        /// <param name="lightmapEmission">Si está activo, muestra la propiedad de lightmap emission.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// <param name="isUnfold">Controla el estado del foldout.</param>
        /// <param name="playerPrefKeyName">Nombre de la clave del player pref.</param>
        /// \endspanish
        public static void BuildOtherSettings(bool renderQueue, bool enableInstancing, bool lightmapEmission, bool doubleSidedGI, MaterialEditor materialEditor, bool isUnfold, string playerPrefKeyName)
        {
            isUnfold = CGFMaterialEditorUtilitiesClass.BuildFoldOutHeaderGroup("Other Settings", "", true, isUnfold, playerPrefKeyName);

            if (isUnfold)
            {
                if (renderQueue)
                {
                    materialEditor.RenderQueueField();
                }

                if (enableInstancing)
                {
                    materialEditor.EnableInstancingField();
                }

                if (lightmapEmission)
                {
                    materialEditor.LightmapEmissionProperty();
                }

                if (doubleSidedGI)
                {
                    materialEditor.DoubleSidedGIField();
                }
            }
        }

    #endregion

}
