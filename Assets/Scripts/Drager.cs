using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drager : MonoBehaviour
{
    public Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.position;
    }
    public void DragHandler(BaseEventData data)
    {  
        PointerEventData poinerData = (PointerEventData)data;
        Canvas canvas = transform.parent.transform.parent.gameObject.GetComponent<Canvas>();
        RectTransform canvasRect = transform.parent.GetComponent<RectTransform>();
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, poinerData.position, Camera.main, out position);
        Vector2 min = canvasRect.rect.min;
        Vector2 max = canvasRect.rect.max;
        Vector2 worldPosition = canvas.transform.TransformPoint(position);
        worldPosition.x = Mathf.Clamp(worldPosition.x, canvas.transform.TransformPoint(min).x, canvas.transform.TransformPoint(max).x);
        worldPosition.y = Mathf.Clamp(worldPosition.y, canvas.transform.TransformPoint(min).y, canvas.transform.TransformPoint(max).y);
        transform.position = worldPosition;

       
    }
}
