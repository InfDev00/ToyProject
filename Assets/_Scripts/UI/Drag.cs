using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Action<Queue<Vector3>> DragEndAction = null;
        
        private Queue<Vector3> hitPositionsQueue = new Queue<Vector3>();
        private HashSet<GameObject> hitPositionsSet = new HashSet<GameObject>();
        private Camera mainCamera;
        
        private void Awake() => mainCamera = Camera.main;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Util.SetTimeScale(0.1f);
            hitPositionsQueue.Clear();
            hitPositionsSet.Clear();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var ray = mainCamera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out var hit) && hit.collider.CompareTag(Tags.ENEMY))
            {
                if (hitPositionsSet.Add(hit.collider.gameObject)) hitPositionsQueue.Enqueue(hit.collider.transform.position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Util.SetTimeScale(1f);
            DragEndAction?.Invoke(new Queue<Vector3>(hitPositionsQueue));
        }
    }
}