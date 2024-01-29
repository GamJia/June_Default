using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject puzzle;
    private Vector2 originalPosition;
    private Vector2 originalSize; 
    private Image puzzleImage; 

    void Start()
    {
        puzzleImage = GetComponent<Image>(); 
        if (puzzleImage != null)
        {
            originalSize = puzzleImage.rectTransform.sizeDelta; 
        }
    }

    public void SetPuzzle(GameObject newPuzzle)
    {
        puzzle = newPuzzle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        if (puzzleImage != null)
        {
            puzzleImage.SetNativeSize(); 
            puzzleImage.rectTransform.localScale = new Vector3(0.85f, 0.85f, 0.85f); 
        }
       
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        screenPosition.z = 0; 

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
        screenPosition.z = 0; 

        float distance = Vector3.Distance(screenPosition, puzzle.transform.position);
        if (distance <= 0.07f)
        {
            PuzzleManager.Instance.InstantiatePuzzle(puzzle);
            this.gameObject.transform.parent.gameObject.SetActive(false); 
        }
        else
        {
            transform.position = originalPosition;
            if (puzzleImage != null)
            {
                puzzleImage.rectTransform.sizeDelta = originalSize; 
            }
        }
    }




    
}
