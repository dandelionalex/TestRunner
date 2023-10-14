using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for Player Units 
/// * animation 
/// * "flying" effect.
/// * collect collectable items and throw signal about it
/// </summary>
public class PlayerUnitView : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private ISpeedManager speedManager;
    private IPlayerVerticalState playerVerticalState;
    private SignalBus signalBus;
    private Vector3 startPosition;

    [Inject]
    private void Init(ISpeedManager speedManager, SignalBus signalBus, IPlayerVerticalState playerVerticalState)
    {
        this.speedManager = speedManager;
        this.signalBus = signalBus;
        this.playerVerticalState = playerVerticalState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Collectable")
            return;
        
        var collectableItem = collision.gameObject.GetComponent<CollectableItem>();
        if(collectableItem == null)
            return;

        collectableItem.MarkToDestroy = true;
        
        signalBus.Fire<ItemCollectedSignal>(new ItemCollectedSignal(collectableItem.Type));
    }

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if(playerVerticalState.VerticalState == VerticalStateEnum.Up)
        {
            rigidbody2D.isKinematic = true;
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }
        else
        {
            rigidbody2D.isKinematic = false;
        }
            
        animator.SetFloat("RunSpeed", speedManager.Speed );
    }
}
