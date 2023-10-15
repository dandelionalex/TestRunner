public class ItemCollectedSignal 
{
    public CollectableItem ItemCollected { get; private set; }

    public ItemCollectedSignal(CollectableItem itemCollected)
    {
        this.ItemCollected = itemCollected;
    }
}
