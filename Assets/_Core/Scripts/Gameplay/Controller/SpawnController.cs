using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Controller
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Transform defaultSpawnPoint;

        Dictionary<int, CheckPoint> checkPointsDict = new Dictionary<int, CheckPoint>();

        int lastCheckpoint = -1;

        public void Configure(int lastCheckpoint)
        {
            var checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.InstanceID);
            checkPointsDict.Clear();

            for (int i = 0; i < checkPoints.Length; i++)
            {
                checkPointsDict[i] = checkPoints[i];
            }
            this.lastCheckpoint = lastCheckpoint;
        }

        public void SetCheckpointID(int index)
        {
            lastCheckpoint = index;
        }

        public void DisableCurrentCheckPoint()
        {
            if (lastCheckpoint >= 0)
                checkPointsDict[lastCheckpoint].Disable();
        }

        public Transform GetLastCheckPoint()
        {
            if (lastCheckpoint >= 0)
                return checkPointsDict[lastCheckpoint].Spawnpoint;
            else
                return GetDefaultSpawnPosition();
        }

        public Transform GetDefaultSpawnPosition()
        {
            return defaultSpawnPoint;
        }
    }
}