using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;
    private Vector2 originalSize; 
    private Vector2 currentSize;
    private RectTransform rectTransform;
    [SerializeField] private BoardID boardID; 
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
            originalSize = rectTransform.sizeDelta;
        }
    }


    void Start()
    {
        currentSize = rectTransform.sizeDelta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.sizeDelta = originalSize;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        screenPosition.z = 0; 

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        Vector3 adjustedMousePosition = new Vector3(mouseWorldPosition.x * 100, mouseWorldPosition.y * 100, 0);

        float distance = Vector3.Distance(adjustedMousePosition, originalPosition);

        if (distance <= 7f)
        {
            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            Board targetBoard = Board.FindBoardByBoardID(boardID);
            if (targetBoard != null)
            {                
                targetBoard.CorrectPuzzle(this.gameObject, originalPosition);
            }
            
        }
        else
        {
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = currentSize;
                rectTransform.anchoredPosition = Vector2.zero; 
            }
        }

    }
    


    
}
