using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Controller;
using GameControl;

namespace Gameplay
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private Transform spawnpoint;
        [SerializeField] private Animator animator;
        [SerializeField] private new Collider collider;
        [SerializeField] private AudioSource audioSource;

        public Transform Spawnpoint => spawnpoint;
        public int Index
        {
            get => index;
            set => index = value;
        }

        public void Activation()
        {
            GameController.LastLevelVisit.checkpointID = index;
            animator.SetTrigger("Pop");
            audioSource.Play();
            collider.gameObject.SetActive(false);
        }

        public void Disable()
        {
            animator.gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        [Button("Auto Assign Index")]
        public void AutoAssignIndexes()
        {
            var checkpoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.InstanceID);
            for (int i = 0; i < checkpoints.Length; i++)
                checkpoints[i].Index = i;
        }
#endif
    }
}