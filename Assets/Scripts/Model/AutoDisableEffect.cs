using System;
using System.Threading.Tasks;

public class AutoDisableEffect
{
    private Action<AutoDisableEffect> onDisable;
    public CollectablesEnum CollectablesEnum {get; private set;}

    public AutoDisableEffect(Action<AutoDisableEffect> onDisable, CollectablesEnum collectablesEnum)
    {
        this.onDisable = onDisable;
        this.CollectablesEnum = collectablesEnum;
        DisableTimr();
    }

    private async Task DisableTimr()
    {
        await Task.Delay(10000);
        onDisable?.Invoke(this);
    }
}
