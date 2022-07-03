using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;

namespace Cameras
{
    public class CameraChangeDollyWithPath : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _cameras;
        [SerializeField] private List<PathDollyPair> _pairList;

        private CameraGroup _activeCamera;
        private CameraGroup _secondaryCamera;
        private MoverOnRails _mover;
        private PathDollyPair _currentPair;


        public delegate void CameraEvent(CinemachineVirtualCamera camera);
        public event CameraEvent CameraChangeEvent;

        private void Awake()
        {
            _activeCamera = new CameraGroup(_cameras[0]);
            _secondaryCamera = new CameraGroup(_cameras[1]);

            _mover = _activeCamera.VirtualCamera.Follow.GetComponent<MoverOnRails>();
            _mover.RailChangeEvent += OnPathChange;            
        }

        private void OnPathChange(IRail newPath)
        {
            Debug.Log("OnPathChange to " + newPath.ToString());
            
            PathDollyPair newPair = SearchPair(newPath);
            if (newPair != null &&  newPair != _currentPair)
            {
                _secondaryCamera.TrackedDolly.m_Path = newPair.DollyTrack;
                ChangeCameras();
            }
        }

        private PathDollyPair SearchPair(IRail path)
        {
            foreach(PathDollyPair pair in _pairList)
            {
                if (path == pair.Path)
                    return pair;
            }

            return null;
        }

        private void ChangeCameras()
        {
            CameraGroup proxy = _activeCamera;
            _activeCamera = _secondaryCamera;
            _secondaryCamera = proxy;
            CameraChangeEvent?.Invoke(_activeCamera.VirtualCamera);
        }

        [System.Serializable]
        private class PathDollyPair
        {
            [HorizontalGroup]
            [SerializeField] private RailBehaviour _path;
            [SerializeField] private CinemachineSmoothPath _dollyTrack;

            public IRail Path => _path;
            public CinemachineSmoothPath DollyTrack => _dollyTrack;
        }

        private class CameraGroup
        {
            public CinemachineVirtualCamera VirtualCamera { get; private set; }
            public CinemachineTrackedDolly TrackedDolly { get; private set; }

            public CameraGroup(CinemachineVirtualCamera virtualCamera)
            {
                VirtualCamera = virtualCamera;
                TrackedDolly = VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            }
        }
    }
}
