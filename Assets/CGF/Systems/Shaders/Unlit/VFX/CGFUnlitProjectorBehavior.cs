///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 02/03/2019
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Behavior that allows to the attatched gameobject manage a material with shader Unlit/Projector.
///

using UnityEngine;
using CGF.Systems.Shaders;

namespace CGF.Systems.Shaders.Unlit.VFX
{

    /// \english
    /// <summary>
    /// Behavior that allows to the attatched gameobject manage a material with shader Unlit/Projector.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Comportamiento que permite al gameobject asociado gestionar un material con el shader Unlit/Projector.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public class CGFUnlitProjectorBehavior : CGFShaderBehavior
    {

        #region Public Variables

        public float BlendType
        {
            get { return _blendType; }
            set
            {
                _blendType = value;
                SetFloat("_BlendType", _blendType);
            }
        }

        public float BlendSource
        {
            get { return _blendSource; }
            set
            {
                _blendSource = value;
                SetFloat("_SrcBlendFactor", _blendSource);
            }
        }

        public float BlendDestination
        {
            get { return _blendDestination; }
            set
            {
                _blendDestination = value;
                SetFloat("_DstBlendFactor", _blendDestination);
            }
        }

        public float Cutoff
        {
            get { return _cutoff; }
            set
            {
                _cutoff = value;
                SetFloat("_Cutoff", _cutoff);
            }
        }

        public Color CookieColor
        {
            get { return _cookieColor; }
            set
            {
                _cookieColor = value;
                SetColor("_Tint", _cookieColor);
            }
        }

        public Texture ShadowTex
        {
            get { return _shadowTex; }
            set
            {
                _shadowTex = value;
                SetTexture("_ShadowTex", _shadowTex);
            }
        }

        public Vector2 ShadowTexTiling
        {
            get { return _shadowTexTiling; }
            set
            {
                _shadowTexTiling = value;
                SetVector("_ShadowTexTiling", _shadowTexTiling);
            }
        }

        public Vector2 ShadowTexOffset
        {
            get { return _shadowTexOffset; }
            set
            {
                _shadowTexOffset = value;
                SetVector("_ShadowTexOffset", _shadowTexOffset);
            }
        }

        public bool UvScrollAnimation
        {
            get { return _uvScrollAnimation; }
            set
            {
                _uvScrollAnimation = value;
                SetFloat("_UVScrollAnimation", _uvScrollAnimation);
            }
        }

        public float ShadowLevel
        {
            get { return _shadowLevel; }
            set
            {
                _shadowLevel = value;
                SetFloat("_ShadowLevel", _shadowLevel);
            }
        }

        public Vector2 UvScrollSpeed
        {
            get { return _uvScrollSpeed; }
            set
            {
                _uvScrollSpeed = value;
                SetVector("_UVScrollSpeed", _uvScrollSpeed);
            }
        }

        public bool ScrollByTexel
        {
            get { return _scrollByTexel; }
            set
            {
                _scrollByTexel = value;
                SetFloat("_ScrollByTexel", _scrollByTexel);
            }
        }

        public Texture FalloffMap
        {
            get { return _falloffMap; }
            set
            {
                _falloffMap = value;
                SetTexture("_FalloffMap", _falloffMap);
            }
        }

        public bool BackfaceCulling
        {
            get { return _backfaceCulling; }
            set
            {
                _backfaceCulling = value;
                SetFloat("_BackfaceCulling", _backfaceCulling);
            }
        }

        public bool UseVertexPosition
        {
            get { return _useVertexPosition; }
            set
            {
                _useVertexPosition = value;
                SetFloat("_UseVertexPosition", _useVertexPosition);
            }
        }

        public bool FlipUVVertical
        {
            get { return _flipUVVertical; }
            set
            {
                _flipUVVertical = value;
                SetFloat("_FlipUVVertical", _flipUVVertical);
            }
        }

        public bool FlipUVHorizontal
        {
            get { return _flipUVHorizontal; }
            set
            {
                _flipUVHorizontal = value;
                SetFloat("_FlipUVHorizontal", _flipUVHorizontal);
            }
        }

        public bool UvScroll
        {
            get { return _uvScroll; }
            set
            {
                _uvScroll = value;
                SetFloat("_UVScroll", _uvScroll);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private float _blendType;

        [SerializeField]
        private float _blendSource;

        [SerializeField]
        private float _blendDestination;

        [SerializeField]
        private float _cutoff;

        [SerializeField]
        private Color _cookieColor;

        [SerializeField]
        private Texture _shadowTex;

        [SerializeField]
        private Vector2 _shadowTexTiling;

        [SerializeField]
        private Vector2 _shadowTexOffset;

        [SerializeField]
        private bool _uvScrollAnimation;

        [SerializeField]
        private float _shadowLevel;

        [SerializeField]
        private Vector2 _uvScrollSpeed;

        [SerializeField]
        private bool _scrollByTexel;

        [SerializeField]
        private Texture _falloffMap;

        [SerializeField]
        private bool _backfaceCulling;

        [SerializeField]
        private bool _useVertexPosition;

        [SerializeField]
        private bool _flipUVVertical;

        [SerializeField]
        private bool _flipUVHorizontal;

        [SerializeField]
        private bool _uvScroll;

        #endregion


        #region Main Methods

        protected override void Awake()
        {

            base.Awake();

            _shaderName = "CG Framework/Unlit/Unlit Projector";

        }

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            Cutoff = GetFloat("_Cutoff"); ;

            CookieColor = GetColor("_Tint"); ;

            ShadowTex = GetTexture("_ShadowTex"); ;

            ShadowTexOffset = GetVector("_ShadowTexOffset"); ;

            ShadowTexTiling = GetVector("_ShadowTexTiling"); ;

            UvScrollAnimation = GetBoolean("_UVScrollAnimation"); ;

            ShadowLevel = GetFloat("_ShadowLevel"); ;

            UvScrollSpeed = GetVector("_UVScrollSpeed"); ;

            ScrollByTexel = GetBoolean("_ScrollByTexel"); ;

            FalloffMap = GetTexture("_FalloffMap"); ;

            BackfaceCulling = GetBoolean("_BackfaceCulling"); ;

            UseVertexPosition = GetBoolean("_UseVertexPosition"); ;

            BlendType = GetFloat("_BlendType"); ;

            BlendSource = GetFloat("_SrcBlendFactor"); ;

            BlendDestination = GetFloat("_DstBlendFactor"); ;

            FlipUVVertical = GetBoolean("_FlipUVVertical"); ;

            FlipUVHorizontal = GetBoolean("_FlipUVHorizontal"); ;

            UvScroll = GetBoolean("_UVScroll"); ;

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            Cutoff = _cutoff;

            CookieColor = _cookieColor;

            ShadowTex = _shadowTex;

            ShadowTexOffset = _shadowTexOffset;

            ShadowTexTiling = _shadowTexTiling;

            UvScrollAnimation = _uvScrollAnimation;

            ShadowLevel = _shadowLevel;

            UvScrollSpeed = _uvScrollSpeed;

            ScrollByTexel = _scrollByTexel;

            FalloffMap = _falloffMap;

            BackfaceCulling = _backfaceCulling;

            UseVertexPosition = _useVertexPosition;

            BlendType = _blendType;

            BlendSource = _blendSource;

            BlendDestination = _blendDestination;

            FlipUVVertical = _flipUVVertical;

            FlipUVHorizontal = _flipUVHorizontal;

            UvScroll = _uvScroll;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}
