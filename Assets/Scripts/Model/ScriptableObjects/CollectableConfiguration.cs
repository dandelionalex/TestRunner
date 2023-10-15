using UnityEngine;

public abstract class CollectableBaseConfiguration : ScriptableObject
{
    public CollectablesEnum type;
    public Sprite sprite;

    [Tooltip("duration, while effect active. In milliseconds")]
    public int duration;
    public GameObject visualEffect;
}
