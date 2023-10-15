using System;
using System.Threading.Tasks;

public class AutoDisableEffect
{
    private Action<AutoDisableEffect> onDisable;
    public CollectableItem CollectableItem {get; private set;}

    public AutoDisableEffect(Action<AutoDisableEffect> onDisable, CollectableItem collectablesEnum, int duration)
    {
        this.onDisable = onDisable;
        this.CollectableItem = collectablesEnum;
        
        DisableTimr(duration);
    }

    private async Task DisableTimr(int duration)
    {
        await Task.Delay(duration);
        onDisable?.Invoke(this);
    }
}
