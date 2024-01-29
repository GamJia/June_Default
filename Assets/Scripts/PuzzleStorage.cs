using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PuzzleID
{
    Puzzle_0,
    Puzzle_1,

}

[CreateAssetMenu]
public class PuzzleStorage : ScriptableObject
{
    public static PuzzleStorage Instance => instance;
    private static PuzzleStorage instance;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        GenerateDictionary(); 
    }
    [SerializeField] PuzzleArray[] puzzleArray;

    Dictionary<PuzzleID, List<GameObject>> puzzleDictionary = new Dictionary<PuzzleID, List<GameObject>>();

    void GenerateDictionary()
    {
        for (int i = 0; i < puzzleArray.Length; i++)
        {
            puzzleDictionary.Add(puzzleArray[i].puzzleID, puzzleArray[i].puzzleList);
        }
    }

    public List<GameObject> GetPuzzle(PuzzleID id)
    {
        Debug.Assert(puzzleArray.Length > 0, "No Puzzle!!");

        if (puzzleDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (puzzleDictionary.ContainsKey(id))
        {
            return puzzleDictionary[id];
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public struct PuzzleArray
{
    [SerializeField] PuzzleID _puzzleID;
    [SerializeField] List<GameObject> _puzzleList;

    public PuzzleID puzzleID { get { return _puzzleID; } }
    public List<GameObject> puzzleList { get { return _puzzleList; } }
}

