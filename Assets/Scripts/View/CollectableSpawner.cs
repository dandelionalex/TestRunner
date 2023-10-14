using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Monobehaviour responsible for spawning collectable items, collectable items must have CollectableItem component on the prefab
/// </summary>
public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private CollectableItem[] elementPrefabs;

    [SerializeField]
    private float verticalPosition;

    private ISpeedManager speedManager;
    private GameCameraView gameCameraView;

    private List<CollectableItem> spawnedElements = new List<CollectableItem>();

    [Inject]
    private void Init(ISpeedManager speedManager, GameCameraView gameCameraView)
    {
        this.speedManager = speedManager;
        this.gameCameraView = gameCameraView;
    }

    private void Start()
    {
        SpawnElement();
    }

    private void Update()
    {
        MoveAllElements();
    }

    private void SpawnElement()
    {
        var randomIndex = Random.Range(0, elementPrefabs.Length);
        var element = Instantiate(elementPrefabs[randomIndex], transform);
        element.transform.position = new Vector3(gameCameraView.GameViewSize.x/2, verticalPosition, 0);

        spawnedElements.Add(element);
    }

    private List<CollectableItem> elementsToDestroy = new List<CollectableItem>();

    private void MoveAllElements()
    {
        foreach(var element in spawnedElements)
        {
            if(element.MarkToDestroy)
            {
                elementsToDestroy.Add(element);
                continue;
            }

            if(element.transform.position.x < gameCameraView.GameViewSize.x/-2)
            {
                elementsToDestroy.Add(element);
                continue;
            }

            element.transform.position -= new Vector3(speedManager.Speed * Time.deltaTime, 0, 0);
        }

        foreach(var element in elementsToDestroy)
        {
            spawnedElements.Remove(element);
            Destroy(element.gameObject);
            SpawnElement();
        }

        elementsToDestroy.Clear();
    }
}
