using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DefaultNamespace
{
    public class FieldPositionController : MonoBehaviour
    {
        private ARRaycastManager _arRaycastManager;
        private ARPlaneManager _arPlaneManager;
        private ARAnchorManager _arAnchorManager;
        
        public bool IsActive { get; set; }
        private Vector2 _position;

        private void Awake()
        {
            _arAnchorManager = FindObjectOfType<ARAnchorManager>();
            _arPlaneManager = FindObjectOfType<ARPlaneManager>();
            _arRaycastManager = FindObjectOfType<ARRaycastManager>();
            _position = new Vector2(Screen.currentResolution.width * 0.5f,
                Screen.currentResolution.height * 0.5f);
        }

        private void Update()
        {
            if (_arRaycastManager == null) return;
            if (!IsActive) return;

            List<ARRaycastHit> list = new List<ARRaycastHit>();
            if (_arRaycastManager.Raycast(_position,list,TrackableType.PlaneWithinPolygon))
            {
                Pose pose = list[0].pose;
                TrackableId hitId = list[0].trackableId;
                ARPlane arPlane = _arPlaneManager.GetPlane(hitId);
                ARAnchor arAnchor = _arAnchorManager.AttachAnchor(arPlane, pose);
                gameObject.transform.position = arAnchor.transform.position;
                gameObject.transform.rotation = pose.rotation;
            }
        }
    }
}