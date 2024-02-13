using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public SpriteRenderer spriteRenderer;
    public Vector2 originalPosition;
    private Vector2 originalSize; 
    private Image puzzleImage; 

    void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        screenPosition.z = 0; 

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        // screenPosition.z = 0; 

        // float distance = Vector3.Distance(screenPosition, puzzle.transform.position);
        // if (distance <= 0.07f)
        // {
        //     PuzzleManager.Instance.InstantiatePuzzle(puzzle);
        //     this.gameObject.transform.parent.gameObject.SetActive(false); 
        // }
        // else
        // {
        //     transform.position = originalPosition;
        //     if (puzzleImage != null)
        //     {
        //         puzzleImage.rectTransform.sizeDelta = originalSize; 
        //     }
        // }
    }




    
}
