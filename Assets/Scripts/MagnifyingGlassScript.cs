using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MagnifyingGlassScript : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public analyzercript myAnalyzer;
    private Transform defaultParent;
    public LayerMask UIlayer;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // defaultParent = transform.parent;
        // transform.SetParent(transform.root);
        // // transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        if (Physics2D.OverlapCircle(newPos, 1f, UIlayer))
        {
            transform.position = newPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.SetParent(defaultParent);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "invis")
        {
            myAnalyzer.FoundOne(other.gameObject);
        }
    }
}
