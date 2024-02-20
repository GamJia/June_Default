using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Vector2 currentPosition;
    private RectTransform rectTransform;
    [SerializeField] private BoardID boardID;
    [SerializeField] private int puzzleNum;
    public bool IsPuzzleComplete { get; private set; }
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            currentPosition = rectTransform.anchoredPosition;
        }
        CountPuzzle();
    }

    void Start()
    {
        
    }

    void CountPuzzle()
    {
        List<GameObject> boardList = Stage.Instance.boardStorage.GetBoard(boardID);
        if (boardList != null)
        {
            puzzleNum = boardList.Count;
        }
        else
        {
            puzzleNum = 0;
        }
    }

    public void CorrectPuzzle(GameObject puzzle, Vector2 originalPosition)
    {
        RectTransform puzzleRectTransform = puzzle.GetComponent<RectTransform>();
        
        if (puzzleRectTransform != null)
        {
            Vector2 adjustment = originalPosition - currentPosition;
            puzzleRectTransform.anchoredPosition = adjustment;
            
            puzzleRectTransform.SetParent(this.transform, false);
        }

        CheckPuzzleCompletion();

    }

    void CheckPuzzleCompletion()
    {
        IsPuzzleComplete = transform.childCount.Equals(puzzleNum);
        if(IsPuzzleComplete)
        {
            Group targetGroup = GetComponentInParent<Group>();
            if (targetGroup != null)
            {
                targetGroup.CheckBoardCompletion();
            }
        }
        
    }

    public static Board FindBoardByBoardID(BoardID id)
    {
        Board[] boards = FindObjectsOfType<Board>();
        foreach (var board in boards)
        {
            if (board.boardID.Equals(id))
            {
                return board;
            }                
        }
        return null;
    }
}
