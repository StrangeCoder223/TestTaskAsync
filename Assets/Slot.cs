using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Image _indicator;
    
    private bool _wasSeen;


    public void ResetAll()
    {
        _wasSeen = false;
        _indicator.enabled = !_wasSeen;
    }
    
    private void OnEnable()
    {
        _indicator.enabled = !_wasSeen;
        _scrollRect.onValueChanged.AddListener(CheckSlotVisibility);
        
        CheckSlotVisibility();
    }

    private void CheckSlotVisibility(Vector2 value = default)
    {
        if (!_wasSeen)
            _wasSeen = RectTransformUtility.RectangleContainsScreenPoint(_scrollRect.viewport, transform.position);
    }

    private void OnDisable()
    {
        _scrollRect.onValueChanged.RemoveAllListeners();
    }
}
