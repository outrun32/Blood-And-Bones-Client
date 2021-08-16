using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> { }
public class MainUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent OnDown;
    public UnityEvent OnUp;
    public UnityEvent OnPressed;
    public UnityEvent OnEnter;
    private bool _isPressed;
    [InspectorName("OnDrag")]public UnityEventVector2 OnDragEvent;
    [InspectorName("OnDragFinish")]public UnityEventVector2 OnDragFinishEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        OnDown?.Invoke();
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp");
        OnUp?.Invoke();
        
        _isPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        OnEnter?.Invoke();

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(("OnPointerEnter = {0},{1}", eventData.delta.x, eventData.delta.y));
        OnDragEvent?.Invoke(new Vector2(eventData.delta.x, eventData.delta.y));
    }

    private void FixedUpdate()
    {
        if (_isPressed)
        {
            OnPressed?.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragFinishEvent?.Invoke(eventData.pressPosition- eventData.position);
    }
}
