public class ItemCollectedSignal 
{
    public CollectablesEnum ItemCollected { get; private set; }

    public ItemCollectedSignal(CollectablesEnum itemCollected)
    {
        this.ItemCollected = itemCollected;
    }
}
