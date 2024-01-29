using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleStorage puzzleStorage;
    [SerializeField] private PuzzleID selectedPuzzleID; 
    [SerializeField] private GameObject puzzle;
    [SerializeField] private Transform puzzleTransform;
    [SerializeField] private List<GameObject> puzzleGameObjects;

    public static PuzzleManager Instance => instance;
    private static PuzzleManager instance;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        
    }

    void Start()
    {
        LoadPuzzleGameObjects(selectedPuzzleID);
    }

    void LoadPuzzleGameObjects(PuzzleID puzzleID)
    {
        if (puzzleStorage != null)
        {
            List<GameObject> puzzles = puzzleStorage.GetPuzzle(puzzleID);
            if (puzzles != null)
            {
                puzzleGameObjects = new List<GameObject>(puzzles);
                Shuffle(puzzleGameObjects); 
            }

            else
            {
                Debug.LogWarning("Puzzle not found for ID: " + puzzleID);
            }
        }
        else
        {
            Debug.LogError("PuzzleStorage not assigned in PuzzleUIManager");
        }

        AssignPuzzle();
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
        foreach (GameObject puzzleGameObject in puzzleGameObjects)
        {
            GameObject instantiatedPuzzle = Instantiate(puzzle, transform);
            
            Image puzzleImage = instantiatedPuzzle.transform.GetChild(0).GetComponent<Image>();
            SpriteRenderer puzzleSpriteRenderer = puzzleGameObject.GetComponent<SpriteRenderer>();

            if (puzzleSpriteRenderer != null && puzzleImage != null)
            {
                puzzleImage.sprite = puzzleSpriteRenderer.sprite;
                puzzleImage.SetNativeSize();

                RectTransform rectTransform = puzzleImage.rectTransform;
                float maxWidthOrHeight = 120f;
                float width = rectTransform.sizeDelta.x;
                float height = rectTransform.sizeDelta.y;

                if (width > maxWidthOrHeight || height > maxWidthOrHeight)
                {
                    float aspectRatio = width / height;
                    if (width > height)
                    {
                        rectTransform.sizeDelta = new Vector2(maxWidthOrHeight, maxWidthOrHeight / aspectRatio);
                    }
                    else
                    {
                        rectTransform.sizeDelta = new Vector2(maxWidthOrHeight * aspectRatio, maxWidthOrHeight);
                    }
                }
            }

            Puzzle puzzleUIComponent = instantiatedPuzzle.transform.GetChild(0).GetComponent<Puzzle>();
            if (puzzleUIComponent != null)
            {
                puzzleUIComponent.SetPuzzle(puzzleGameObject);
            }
        }
    }

    public void InstantiatePuzzle(GameObject puzzle)
    {
        Instantiate(puzzle,puzzleTransform);
    }




}
