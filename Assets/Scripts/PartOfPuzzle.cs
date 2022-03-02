using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PartOfPuzzle : MonoBehaviour
    {
        public event Action collisionDetected;
        
        private Vector3 startLocalEulerAngles;

        private void Start()
        {
            startLocalEulerAngles = transform.localRotation.eulerAngles;
        }

        private void OnCollisionEnter(Collision other)
        {
            //if (transform.parent.GetComponent<BoxSideController>().isUsed && !transform.parent.GetComponent<BoxSideController>().isFinished)
            //{
                collisionDetected?.Invoke();
            //}
        }
    }
}