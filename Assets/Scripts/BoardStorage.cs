using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BoardID
{
    Board_0,
    Board_1,
    
}

[CreateAssetMenu(fileName = "BoardStorage", menuName = "Puzzle/Board Storage")]
public class BoardStorage : ScriptableObject
{
    public static BoardStorage Instance => instance;
    private static BoardStorage instance;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        GenerateDictionary(); 
    }
    
    [SerializeField] BoardArray[] boardArray; 

    Dictionary<BoardID, List<GameObject>> boardDictionary = new Dictionary<BoardID, List<GameObject>>(); 

    void GenerateDictionary()
    {
        for (int i = 0; i < boardArray.Length; i++)
        {
            boardDictionary.Add(boardArray[i].boardID, boardArray[i].boardList);
        }
    }

    public List<GameObject> GetBoard(BoardID id) 
    {
        Debug.Assert(boardArray.Length > 0, "No Board!!"); 

        if (boardDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (boardDictionary.ContainsKey(id))
        {
            return boardDictionary[id];
        }
        else
        {
            return null;
        }
    }

    public List<GameObject> GetAllPuzzles()
    {
        List<GameObject> allPuzzles = new List<GameObject>();
        foreach (BoardArray board in boardArray)
        {
            allPuzzles.AddRange(board.boardList);
        }
        return allPuzzles;
    }
}

[Serializable]
public struct BoardArray 
{
    [SerializeField] BoardID _boardID; 
    [SerializeField] List<GameObject> _boardList; 

    public BoardID boardID { get { return _boardID; } } 
    public List<GameObject> boardList { get { return _boardList; } } 
}
