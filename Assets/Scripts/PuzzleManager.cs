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
        List<GameObject> puzzles = puzzleStorage.GetPuzzle(puzzleID);
        if (puzzles != null)
        {
            // 기존의 리스트에 새로운 퍼즐 리스트를 추가합니다.
            puzzleGameObjects.AddRange(puzzles);
            // 전체 리스트를 섞습니다.
            Shuffle(puzzleGameObjects); 
        }
        else
        {
            Debug.LogWarning("Puzzle not found for ID: " + puzzleID);
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
            GameObject puzzleParentInstance = Instantiate(puzzle, this.transform.position, Quaternion.identity, this.transform);
            // 퍼즐 게임 오브젝트의 복사본을 생성하고, 위치와 회전을 원본과 동일하게 설정
            GameObject puzzleInstance = Instantiate(puzzleGameObject, puzzleGameObject.transform.position, puzzleGameObject.transform.rotation);

            // 생성된 퍼즐 인스턴스를 'puzzleParentInstance'의 자식으로 설정
            puzzleInstance.transform.SetParent(puzzleParentInstance.transform, false);

            // 필요에 따라 puzzleInstance의 localPosition, localScale 등을 조정
            puzzleInstance.transform.localPosition = Vector3.zero; 
            // puzzleInstance.transform.localScale = Vector3.one; 등으로 설정 가능
        }
    }
        




}
