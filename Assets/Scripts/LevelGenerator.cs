using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform objectParent;

    [Header("Level Changes")]
    [SerializeField][Min(1)] private int currentLevel = 1;
    [Tooltip("If you want to change the level, please mark this as true.")]
    [SerializeField] private bool changeTheLevel;

    private Level level;
    private List<Level> levels;

    private bool isFirstLevel = true;

    public static Action NewLevel;

    [SerializeField]
    private ObjectWithPrefab[] objectsWithPrefab;

    private void Awake()
    {
        if (changeTheLevel)
            PlayerPrefs.SetInt("Level", currentLevel);
        else
            currentLevel = PlayerPrefs.GetInt("Level", 1);
    }

    void Start()
    {
        //Move it later!!!
        Application.targetFrameRate = Screen.currentResolution.refreshRate; ;

        NewLevel += CreateAndDestroyLevel;

        GetLevels();
    }

    private void InstantiateObjects(ObjectWithPosition[] objectsWithPositions)
    {
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



            /*string type = objectsWithPositions[i].type.ToString()[0] + objectsWithPositions[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = objectsWithPositions[i].shape.ToString()[0] + objectsWithPositions[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, basePosition + objectsWithPositions[i].position, Quaternion.identity, collectibleObjectParent);*/
        }
    }

    private void DestroyObjects()
    {
        Destroy(objectParent.gameObject);
    }

    private void GetLevels()
    {
        levels = new List<Level>();
        //baseLevelObjects = new List<GameObject>();

        levels = Resources.LoadAll<Level>("Levels").ToList();
        levels = levels.OrderBy(w => w.levelIndex).ToList();

        CreateAndDestroyLevel();
        isFirstLevel = false;
    }

    private void CreateAndDestroyLevel()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 1);

        if (currentLevel <= levels.Count)
        {
            level = levels[currentLevel - 1];

            InstantiateObjects(level.objects);
            /*if (isFirstLevel)
                level = levels[currentLevel - 1];
            else
                level = levels[currentLevel];*/
        }
        else
        {
            //ContinueWithRandomLevel();
        }

        //InstantiateObjects(level.objects);
    }

    private void ContinueWithRandomLevel()
    {
        var random = new System.Random();
        int index = random.Next(levels.Count);

        level = levels[index];

        if (isFirstLevel)
            level.levelIndex = currentLevel;
        else
            level.levelIndex = currentLevel + 1;

        levels.Add(level);
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