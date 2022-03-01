using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PartOfPuzzle : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == targetTransform)
            {
                   
            }
        }

        private void OnCollisionEnter(Collision other)
        {
           
        }
    }
}