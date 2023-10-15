using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer image;

    public CollectableBaseConfiguration CollectableConfiguration {get; private set;}

    [HideInInspector]
    public bool MarkToDestroy;
    
    public void Init(CollectableBaseConfiguration config)
    {
        CollectableConfiguration = config;
        image.sprite = config.sprite;
    }
}
