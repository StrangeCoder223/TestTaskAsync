using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path : MonoBehaviour
{
    public IReadOnlyList<Point> Points => _points;
    
    [SerializeField] private List<Point> _points;
    [SerializeField] private bool _focusOnNewPoint;

    private void OnEnable()
    {
        if (_points == null) 
            _points = new List<Point>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            Point currentPoint = _points[i];
            Point nextPoint = _points[Mathf.Clamp(i + 1, 0, _points.Count - 1)];

            if (currentPoint == null)
            {
                RemovePoint(currentPoint);
                continue;
            }

            if (nextPoint == null)
            {
                RemovePoint(nextPoint);
                continue;
            }
            
            if (i >= _points.Count - 1) return;

            Gizmos.DrawLine(currentPoint.transform.position, nextPoint.transform.position);
        }
    }

    public void AddPointTo(Vector3 position)
    {
        Point point = CreatePoint(position);
        _points.Add(point);
    }

    public void AddPoint()
    {
        if (_points.Count > 0)
        {
            Vector3 lastPosition = _points[^1].Position;
            _points.Add(CreatePoint(lastPosition));
            return;
        }
        
        _points.Add(CreatePoint(transform.position));
    }

    public void RemovePoint(Point point)
    {
        _points.Remove(point);
        DestroyImmediate(point.gameObject);

        UpdateNames();
    }

    private Point CreatePoint(Vector3 position)
    {
        GameObject pointGameObject = new GameObject(_points.Count.ToString());
        pointGameObject.transform.position = position;
        pointGameObject.transform.rotation = transform.rotation;
        pointGameObject.transform.SetParent(transform);

#if UNITY_EDITOR
        if (_focusOnNewPoint)
            Selection.activeGameObject = pointGameObject;
#endif
        
        return pointGameObject.AddComponent<Point>();
    }

    private void UpdateNames()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            _points[i].name = i.ToString();
        }
    }
}