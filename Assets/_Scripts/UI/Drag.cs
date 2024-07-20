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
        public PlayerController player;
        public Action<Queue<Vector3>, List<Enemy>> DragEndAction = null;
        
        private Queue<Vector3> hitPositionsQueue = new Queue<Vector3>();
        private HashSet<Enemy> hitPositionsSet = new HashSet<Enemy>();
        private Camera mainCamera;
        private LineRenderer lineRenderer;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Util.SetTimeScale(0.1f);
            hitPositionsQueue.Clear();
            hitPositionsSet.Clear();
            lineRenderer.positionCount = 0;
            if (player != null)
            {
                hitPositionsQueue.Enqueue(player.transform.position);
                UpdateLineRenderer(player.transform.position);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var ray = mainCamera.ScreenPointToRay(eventData.position);
            
            if (Physics.Raycast(ray, out var hit) && hit.collider.CompareTag(Tags.ENEMY))
            {
                if (hitPositionsSet.Add(hit.collider.GetComponent<Enemy>()))
                {
                    hitPositionsQueue.Enqueue(hit.collider.transform.position);
                    UpdateLineRenderer(hit.collider.transform.position);
                }
                
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEndAction?.Invoke(hitPositionsQueue, new List<Enemy>(hitPositionsSet));
            lineRenderer.positionCount = 0;
        }
        
        private void UpdateLineRenderer(Vector3 newPosition)
        {
            lineRenderer.positionCount = hitPositionsQueue.Count;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
        }
    }
}