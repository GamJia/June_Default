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
        // puzzle의 RectTransform 컴포넌트를 가져옵니다.
        RectTransform puzzleRectTransform = puzzle.GetComponent<RectTransform>();
        
        if (puzzleRectTransform != null)
        {
            // 올바른 위치로 이동하기 위해 anchoredPosition을 사용합니다.
            Vector2 adjustment = originalPosition - currentPosition;
            puzzleRectTransform.anchoredPosition = adjustment;
            
            // puzzle을 현재 PuzzleBoard 객체의 자식으로 설정합니다.
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
