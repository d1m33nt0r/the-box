using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private void Start()
        {
            transform.RotateAround(target.position, Vector3.up, 1);
        }
    }
}