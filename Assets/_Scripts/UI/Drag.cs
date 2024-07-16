using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public float minDistance = 0.1f;
        private readonly List<Vector3> _pathPoints = new List<Vector3>();
        public Action<List<Vector3>> OnDragEnd;
        
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            SetupLineRenderer();
        }

        private void SetupLineRenderer()
        {
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = 0;
            _lineRenderer.startColor = Color.yellow;
            _lineRenderer.endColor = Color.yellow;
        }
        private void UpdateLineRenderer()
        {
            _lineRenderer.positionCount = _pathPoints.Count;
            _lineRenderer.SetPositions(_pathPoints.ToArray());
        }
        
        private void AddPointToPath(PointerEventData eventData)
        {
            var ray = Camera.main!.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out var hit))
            {
                var newPoint = hit.point;
                if (_pathPoints.Count == 0 || Vector3.Distance(newPoint, _pathPoints[^1]) >= minDistance)
                {
                    _pathPoints.Add(newPoint);
                    UpdateLineRenderer();
                }
            }
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _pathPoints.Clear();
            AddPointToPath(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            AddPointToPath(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _lineRenderer.positionCount = 0;
            OnDragEnd?.Invoke(_pathPoints);
        }
    }
}