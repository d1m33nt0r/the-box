using DG.Tweening;
using UnityEngine;

public class BoxSideController : MonoBehaviour
{
    private bool isUsed = false;

    private void Update()
    {
#if UNITY_EDITOR

        if (!Input.GetMouseButtonDown(0) || isUsed) return;
        
        var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray2, out var hit2)) return;
        
        if (hit2.transform.name == transform.name)
        {
            var lRot = transform.localRotation.eulerAngles;
            
            transform.DOLocalRotate(new Vector3(-180f, lRot.y, lRot.z), 1)
                .onComplete = () => GetComponent<Rigidbody>().isKinematic = true;

            isUsed = true;
        }
        
#endif
        
#if UNITY_ANDROID
        
        if (Input.touchCount == 0 || Input.touchCount > 1 || isUsed) return;
        
        var touch = Input.GetTouch(0);
        
        if (touch.phase != TouchPhase.Began) return;

        var ray = Camera.main.ScreenPointToRay(touch.position);

        if (!Physics.Raycast(ray, out var hit)) return;
            
        if (hit.transform.name == transform.name)
        {
            var lRot = transform.localRotation.eulerAngles;
            
            transform.DOLocalRotate(new Vector3(-180f, lRot.y, lRot.z), 1)
                .onComplete = () => GetComponent<Rigidbody>().isKinematic = true;  
            
            isUsed = true;
        }
        
#endif
    }  
}