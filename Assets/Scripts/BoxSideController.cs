using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class BoxSideController : MonoBehaviour
{
    [SerializeField] private PartOfPuzzle partOfPuzzle;
    
    public RotationParam rotationParam;
    public GameObject targetPuzzleGameObject;
    public GameObject targetGameObject;
    public bool isUsed;
    public bool isFinished;
    private Tween myTween;
    private Sequence mySequence;
    private bool isFoldingState;
    
    private void Start()
    {
        partOfPuzzle.collisionDetected += StopRotationAndRewind;
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (!Input.GetMouseButtonDown(0) || isUsed) return;
        
        var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray2, out var hit2)) return;
        
        if (hit2.transform.name == transform.name && !isUsed && !isFoldingState)
        {
            switch (rotationParam.axis)
            {
                case AxisOfRotation.X:
                    Fold(new Vector3(rotationParam.direction * 90, 0, 0));
                    break;
                case AxisOfRotation.Z:
                    Fold(new Vector3(0, 0, rotationParam.direction * 90));
                    break;
            }
        }
        
        if (hit2.transform.name == transform.name && !isUsed && isFoldingState)
        {
            Unfold(new Vector3(0,0,0));
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
            switch (rotationParam.axis)
            {
                case AxisOfRotation.X:
                    Fold(new Vector3(rotationParam.direction * 90, 0, 0));
                    break;
                case AxisOfRotation.Z:
                    Fold(new Vector3(0, 0, rotationParam.direction * 90));
                    break;
            }
        }
        
        if (hit.transform.name == transform.name && !isUsed && isFoldingState)
        {
            Unfold(new Vector3(0,0,0));
        }
#endif
    }

    private void Unfold(Vector3 _eulerAngles)
    {
        mySequence = DOTween.Sequence();
        isFoldingState = false;
        myTween = transform.DORotate(_eulerAngles, 0.7f);
        myTween.onComplete = () =>
        {
            isFinished = true;
            isUsed = false;
        };
        mySequence.Append(myTween);
        mySequence.onKill = () =>
        {
            mySequence.Delay();
            mySequence.Rewind();
        };
        mySequence.onRewind = () =>
        {
            isFoldingState = true;
            isUsed = false;
            isFinished = true;
        };
        isUsed = true;
    }

    private void Fold(Vector3 _eulerAngles)
    {
        mySequence = DOTween.Sequence();
        isFoldingState = true;
        myTween = transform.DORotate(_eulerAngles, 1f);
        myTween.onComplete = () =>
        {
            isFinished = true;
            isUsed = false;
        };
        mySequence.Append(myTween);
        mySequence.onKill = () =>
        {
            mySequence.Delay();
            mySequence.Rewind();
        };
        mySequence.onRewind = () =>
        {
            isFoldingState = false;
            isUsed = false;
            isFinished = true;
        };
        isUsed = true;
    }

    private void StopRotationAndRewind()
    {
        mySequence.Kill();
    }
   
}

[Serializable]
public struct RotationParam
{
    public AxisOfRotation axis;
    
    [Range(-1, 1)] public int direction;
}

[Serializable]
public enum AxisOfRotation
{
    X,
    Y,
    Z
}