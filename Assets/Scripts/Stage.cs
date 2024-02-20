using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject puzzleGameObject;
    public BoardStorage boardStorage;
    public GroupStorage groupStorage;
    public GroupID groupID= GroupID.Group_0; 
    [SerializeField] private List<GameObject> boards = new List<GameObject>();
    [SerializeField] private List<GameObject> puzzles = new List<GameObject>();

    public static Stage Instance => instance;
    private static Stage instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    void Start()
    {
        LoadPuzzle();
    }

    void LoadPuzzle()
    {
        puzzles.Clear();
        if(groupStorage != null)
        {
            boards = groupStorage.GetBoards(groupID); 
            foreach (GameObject boardObject in boards)
            {
                Board boardComponent = boardObject.GetComponent<Board>();
                if (boardComponent != null)
                {
                    List<GameObject> boardPuzzles = boardComponent.GetPuzzles(); 
                    puzzles.AddRange(boardPuzzles);
                }

            }
            Shuffle(puzzles);
            AssignPuzzle();
        }

    }

   
    private void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void AssignPuzzle()
    {
        foreach (GameObject puzzle in puzzles)
        {
            GameObject puzzleParentInstance = Instantiate(puzzleGameObject, this.transform.position, Quaternion.identity, this.transform);
            GameObject puzzleInstance = Instantiate(puzzle, puzzle.transform.position, puzzle.transform.rotation);

            RectTransform puzzleRectTransform = puzzleInstance.GetComponent<RectTransform>();
            if (puzzleRectTransform != null)
            {
                float width = puzzleRectTransform.rect.width;
                float height = puzzleRectTransform.rect.height;

                float targetSize = 120f;

                if (width >= height)
                {
                    puzzleRectTransform.sizeDelta = new Vector2(targetSize, height * (targetSize / width));
                }
                else 
                {
                    puzzleRectTransform.sizeDelta = new Vector2(width * (targetSize / height), targetSize);
                }

                puzzleRectTransform.pivot = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchoredPosition = Vector2.zero;
            }

            puzzleInstance.transform.SetParent(puzzleParentInstance.transform, false);
        }
    }

    public void GroupCompleted()
    {
        groupID++; 
        LoadPuzzle();
    }

    public void StageCompleted()
    {

    }

    
}
