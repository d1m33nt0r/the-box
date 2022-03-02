using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class BoxSideController : MonoBehaviour
{
    [SerializeField] private PartOfPuzzle partOfPuzzle;
    public bool isUsed;
    public bool isFinished;
    private Tween myTween;
    private Sequence mySequence;
    private Vector3 localEulerAngles;
    private bool isFoldingState;
    
    private void Start()
    {
        partOfPuzzle.collisionDetected += StopRotationAndRewind;
        localEulerAngles = transform.rotation.eulerAngles;
        mySequence = DOTween.Sequence();
        isFoldingState = true;
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (!Input.GetMouseButtonUp(0) || isUsed) return;
        
        var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray2, out var hit2)) return;
        
        if (hit2.transform.name == transform.name && !isUsed && !isFoldingState)
        {
            isFoldingState = true;
            var lRot = transform.localRotation.eulerAngles;

            myTween = transform.DOLocalRotate(new Vector3(localEulerAngles.x, lRot.y, lRot.z), 0.7f);
            myTween.onComplete = () =>
            {
                GetComponent<Rigidbody>().isKinematic = false;
                isFinished = true;
                isUsed = false;
            };
            
            mySequence.Append(myTween);
            mySequence.onKill = () => mySequence.Rewind();
            isUsed = true;
        }
        
        if (hit2.transform.name == transform.name && !isUsed && isFoldingState)
        {
            isFoldingState = false;
            var lRot = transform.localRotation.eulerAngles;

            myTween = transform.DOLocalRotate(new Vector3(-180f, lRot.y, lRot.z), 0.7f);
            myTween.onComplete = () =>
            {
                GetComponent<Rigidbody>().isKinematic = true;
                isFinished = true;
                isUsed = false;
            };
            
            mySequence.Append(myTween);
            mySequence.onKill = () => mySequence.Rewind();
            isUsed = true;
        }

#endif
        
#if UNITY_ANDROID
        
        if (Input.touchCount == 0 || Input.touchCount > 1 || isUsed) return;
        
        var touch = Input.GetTouch(0);
        
        if (touch.phase != TouchPhase.Began) return;

        var ray = Camera.main.ScreenPointToRay(touch.position);

        if (!Physics.Raycast(ray, out var hit)) return;
            
        if (hit.transform.name == transform.name && !isUsed && !isFoldingState)
        {
            isFoldingState = true;
            var lRot = transform.localRotation.eulerAngles;

            myTween = transform.DOLocalRotate(new Vector3(localEulerAngles.x, lRot.y, lRot.z), 0.7f);
            myTween.onComplete = () =>
            {
                GetComponent<Rigidbody>().isKinematic = false;
                isFinished = true;
                isUsed = false;
            };
            
            mySequence.Append(myTween);
            mySequence.onKill = () => mySequence.Rewind();
            isUsed = true;
        }
        
        if (hit.transform.name == transform.name && !isUsed && isFoldingState)
        {
            isFoldingState = false;
            var lRot = transform.localRotation.eulerAngles;

            myTween = transform.DOLocalRotate(new Vector3(-180f, lRot.y, lRot.z), 0.7f);
            myTween.onComplete = () =>
            {
                GetComponent<Rigidbody>().isKinematic = true;
                isFinished = true;
                isUsed = false;
            };
            
            mySequence.Append(myTween);
            mySequence.onKill = () => mySequence.Rewind();
            isUsed = true;
        }
        
#endif
    }

    private void StopRotationAndRewind()
    {
        var lRot = transform.localRotation.eulerAngles;
        GetComponent<Rigidbody>().isKinematic = true;
        mySequence.Kill();
    }
}