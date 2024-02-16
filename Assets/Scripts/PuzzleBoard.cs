using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    public Vector2 currentPosition;
    private RectTransform rectTransform;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            currentPosition = rectTransform.anchoredPosition;
        }
    }
    [SerializeField] private BoardID boardID;

    public void CorrectPuzzle(GameObject puzzle, Vector2 originalPosition)
    {
        RectTransform puzzleRectTransform = puzzle.GetComponent<RectTransform>();
        
        if (puzzleRectTransform != null)
        {
            Vector2 adjustment = originalPosition - currentPosition;
            puzzleRectTransform.anchoredPosition = adjustment;
            
            puzzleRectTransform.SetParent(this.transform, false);
        }

        Debug.Log($"{puzzle.name} is correctly placed on board {boardID}");
    }


    public static PuzzleBoard FindBoardByBoardID(BoardID id)
    {
        PuzzleBoard[] boards = FindObjectsOfType<PuzzleBoard>();
        foreach (var board in boards)
        {
            if (board.boardID == id)
                return board;
        }
        return null;
    }
}
