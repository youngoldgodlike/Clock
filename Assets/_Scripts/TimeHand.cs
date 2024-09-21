using System;
using _Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class TimeHand : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _handRectTransform;
    [SerializeField] private RectTransform _clock;

    private bool _editMode;
    private bool _isTouch;

    public event Action onRotationChange; 
    
    void Update() 
    {
        if (!_isTouch || !_editMode) return;
       
        var mousePos = Mouse.current.position.value;
        var direction = (Vector2)_clock.position - mousePos;
        
        _handRectTransform.rotation *=
            Quaternion.FromToRotation(_handRectTransform.transform.up, -direction);
    }

    public void StartChangeTime() =>
        _editMode = true;

    public void StopChangeTime() =>
        _editMode = false;

    public void OnPointerDown(PointerEventData eventData) =>
        _isTouch = true;

    public void OnPointerUp(PointerEventData eventData)
    {
        _isTouch = false;
        onRotationChange?.Invoke();
    }

    public abstract int GetTime();
    public abstract void SetTime(TimeData timeData);
}
