using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private DragController dragController;
        
        private void Start()
        {
            dragController.SwipeEvent += DragProcess;
        }

        private void DragProcess(DragController.SwipeType _swipeType, float _delta)
        {
            switch (_swipeType)
            {
                case DragController.SwipeType.LEFT:
                    transform.RotateAround(target.position, Vector3.up, -_delta);
                    break;
                case DragController.SwipeType.RIGHT:
                    transform.RotateAround(target.position, Vector3.up, _delta);
                    break;
            }
        }
    }
}