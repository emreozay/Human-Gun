using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Changes")]
    [SerializeField][Min(1)] private int currentLevel = 1;
    [Tooltip("If you want to change the level, please mark this as true.")]
    [SerializeField] private bool changeTheLevel;

    private Level level;
    private List<Level> levels;

    public static Action NewLevel;
    public static Action LevelCompleted;
    public static Action LevelFailed;

    [SerializeField]
    private GameObject finalRoadPrefab;
    private GameObject finalRoad;

    [SerializeField]
    private GameObject player;
    private Vector3 playerFirstPosition;

    private Transform objectParent;

    [SerializeField]
    private ObjectWithPrefab[] objectsWithPrefab;

    private void Awake()
    {
        if (changeTheLevel)
            PlayerPrefs.SetInt("Level", currentLevel);
        else
            currentLevel = PlayerPrefs.GetInt("Level", 1);

        NewLevel += CreateAndDestroyLevel;
    }

    void Start()
    {
        GetLevels();

        playerFirstPosition = player.transform.position;
    }

    private void InstantiateObjects(ObjectWithPosition[] objectsWithPositions)
    {
        objectParent = new GameObject("ObjectParent").transform;

        for (int i = 0; i < objectsWithPositions.Length; i++)
        {
            for (int j = 0; j < objectsWithPrefab.Length; j++)
            {
                if (objectsWithPositions[i].objectType == objectsWithPrefab[j].objectType)
                {
                    GameObject temp = objectsWithPrefab[j].objectPrefab;
                    Instantiate(temp, objectsWithPositions[i].position, Quaternion.identity, objectParent);

                    break;
                }
            }
        }
    }

    private void GetLevels()
    {
        levels = new List<Level>();

        levels = Resources.LoadAll<Level>("Levels").ToList();
        levels = levels.OrderBy(w => w.levelIndex).ToList();

        CreateAndDestroyLevel();
    }

    private void CreateAndDestroyLevel()
    {
        ResetLevel();

        currentLevel = PlayerPrefs.GetInt("Level", 1);

        if (currentLevel <= levels.Count)
            level = levels[currentLevel - 1];

        if (finalRoad != null)
            Destroy(finalRoad);

        finalRoad = Instantiate(finalRoadPrefab);

        InstantiateObjects(level.objects);
    }

    private void ResetLevel()
    {
        player.transform.position = playerFirstPosition;

        if (objectParent != null)
            Destroy(objectParent.gameObject);
    }

    private void OnDestroy()
    {
        NewLevel -= CreateAndDestroyLevel;
    }
}

public enum ObjectType
{
    LENS,
    STONE,
    HUMAN,
    BARREL,
    STONE_MONEY,
    STONE_HUMAN,
    OBSTACLE_SPIN,
    OBSTACLE_PONTOON,
    OBSTACLE_VERTICAL,
    OBSTACLE_SIMPLE,
}

[Serializable]
public struct ObjectWithPosition
{
    public ObjectType objectType;
    public Vector3 position;
}

[Serializable]
public struct ObjectWithPrefab
{
    public ObjectType objectType;
    public GameObject objectPrefab;
}