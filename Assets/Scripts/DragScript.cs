using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Evenement drag");
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    // Start is called before the first frame update

    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    protected CanvasGroup CanvasGroup;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBegin event");
        CanvasGroup.blocksRaycasts = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEnd event");
        CanvasGroup.blocksRaycasts = true;
    }
}
