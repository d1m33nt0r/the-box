﻿using UnityEngine;

namespace DefaultNamespace
{
    public class DragController : MonoBehaviour
    {
        private bool isDragging, isMobilePlatform;
        private Vector2 tapPoint, swipeDelta, prevSwipeDelta;
        private Vector2 curMousePosition, prevMousePosition = Vector2.zero;
        private bool enabledSwipeController = true;

        public delegate void OnSwipeInput(SwipeType type, float delta);

        public event OnSwipeInput SwipeEvent;

        public enum SwipeType
        {
            LEFT,
            RIGHT
        }
        
        private void EnableSwipeController()
        {
            enabledSwipeController = true;
        }

        private void DisableSwipeController()
        {
            enabledSwipeController = false;
        }

        private void Update()
        {
            if (!enabledSwipeController)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                tapPoint = Input.mousePosition;
                prevMousePosition = tapPoint;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }

            CalculateSwipe();
        }

        private void CalculateSwipe()
        {
            curMousePosition = (Vector2) Input.mousePosition;
            swipeDelta = Vector2.zero;

            if (isDragging)
            {
                if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2) Input.mousePosition - prevMousePosition;

                if (swipeDelta.x < 0 && swipeDelta != prevSwipeDelta)
                {
                    SwipeEvent?.Invoke(SwipeType.LEFT, Mathf.Abs(swipeDelta.x) / 15);
                }

                if (swipeDelta.x > 0 && swipeDelta != prevSwipeDelta)
                {
                    SwipeEvent?.Invoke(SwipeType.RIGHT, Mathf.Abs(swipeDelta.x) / 15);
                }

                prevMousePosition = curMousePosition;
            }
        }

        private void ResetSwipe()
        {
            isDragging = false;
            tapPoint = swipeDelta = prevSwipeDelta = Vector2.zero;
        }
    }
}