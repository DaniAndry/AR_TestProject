using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FingerTouch : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;

    [SerializeField] private ObjectSpawner spawner;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void OnEnable()
    {
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void OnFingerDown(UnityEngine.InputSystem.EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        Vector2 screenPosition = finger.currentTouch.screenPosition;

        if (IsPointerOverUI(screenPosition))
        {
            return;
        }

        HandleTouch(screenPosition);
    }

   // ¬вод дл€ работы в PLayMode
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 screenPosition = Mouse.current.position.ReadValue();

    //        if (IsPointerOverUI(screenPosition))
    //        {
    //            return;
    //        }

    //        HandleTouch(screenPosition);
    //    }
    //}

    private void HandleTouch(Vector2 screenPosition)
    {
        if (screenPosition.x < 0 || screenPosition.y < 0 ||
            screenPosition.x > Screen.width || screenPosition.y > Screen.height)
        {
            return;
        }

        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log($"Raycast hit {hits.Count} plane(s).");

            ARRaycastHit hit = hits[0];
            Pose hitPose = hit.pose;

            if (spawner.TrySpawnObject(hitPose.position, hitPose.up))
            {
                Debug.Log($"Object spawned at position: {hitPose.position}, rotation: {hitPose.rotation}");
            }
            else
            {
                Debug.LogWarning("Failed to spawn object.");
            }
        }
    }


    private bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}
