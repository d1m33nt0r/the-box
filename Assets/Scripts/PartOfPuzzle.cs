using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PartOfPuzzle : MonoBehaviour
    {
        public event Action collisionDetected;
        
        private Vector3 rotationEulerAngles;
        private RotationParam rotationParam;
        private GameObject targetPuzzleGameObject;
        private GameObject targetGameObject;
        
        private void Start()
        {
            targetPuzzleGameObject = transform.parent.GetComponent<BoxSideController>().targetPuzzleGameObject;
            targetGameObject = transform.parent.GetComponent<BoxSideController>().targetGameObject;
            
            rotationEulerAngles = transform.rotation.eulerAngles;
            rotationParam = transform.parent.GetComponent<BoxSideController>().rotationParam;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (rotationParam.axis)
            {
                case AxisOfRotation.X:
                    if (other.gameObject != targetPuzzleGameObject && other.gameObject != targetGameObject)
                    {
                        collisionDetected?.Invoke();
                    }
                    break;
                
                case AxisOfRotation.Z:
                    if (other.gameObject != targetPuzzleGameObject && other.gameObject != targetGameObject)
                    {
                        collisionDetected?.Invoke();
                    }
                    break;
            }
        }
    }
}