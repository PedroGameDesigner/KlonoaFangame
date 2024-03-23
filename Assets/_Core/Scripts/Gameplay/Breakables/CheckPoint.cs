using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform spawnpoint;
        [SerializeField] private Animator animator;
        [SerializeField] private new Collider collider;

        public void Activation()
        {
            animator.SetTrigger("Pop");
            collider.gameObject.SetActive(false);
        }

        public void Disable()
        {
            animator.gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
        }
    }
}