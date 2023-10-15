using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class responsible for player run speed ( it affect all game items movement)
/// and it has a collection with collectable items whih have effect only on player. Another collectable items will be managed by another logic class
/// </summary>
public class PlayerUnitModel : IInitializable, IDisposable, ISpeedManager, IPlayerVerticalState
{
    private const float defaultSpeed = 2f; //TODO: move to config
    private const float minSpeed = 1f; //TODO: move to config

    public VerticalStateEnum VerticalState {get; private set;}
    public float Speed {get; private set;}
    
    private List<AutoDisableEffect> activeEffects = new List<AutoDisableEffect>();
    private SignalBus signalBus;

    public PlayerUnitModel(SignalBus signalBus)
    {
        this.signalBus = signalBus;
        Speed = defaultSpeed;
    }

    public void Initialize()
    {
        signalBus.Subscribe<ItemCollectedSignal>(OnItemCollected);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<ItemCollectedSignal>(OnItemCollected);
    }

    private void OnItemCollected(ItemCollectedSignal signal)
    {
        var item = signal.ItemCollected;
        activeEffects.Add(new AutoDisableEffect(OnEffectDisable, item, item.CollectableConfiguration.duration));

        var configuration = item.CollectableConfiguration;

        switch (configuration.type)
        {
            case CollectablesEnum.SpeedUp:
            case CollectablesEnum.SlowDown:
                var config = configuration as SpeedCollectableModifeer;
                Speed = Mathf.Max (minSpeed, Speed += config.speedModifier);
                break;
            case CollectablesEnum.Flight:
                VerticalState = VerticalStateEnum.Up;
                break;
        }
    }

    private void OnEffectDisable(AutoDisableEffect autoDisableEffect)
    {
        activeEffects.Remove(autoDisableEffect);
        var configuration = autoDisableEffect.CollectableItem.CollectableConfiguration;
        
        switch (autoDisableEffect.CollectableItem.CollectableConfiguration.type)
        {
            case CollectablesEnum.SpeedUp:
            case CollectablesEnum.SlowDown:
                var speedConfig = configuration as SpeedCollectableModifeer;
                Speed = Mathf.Max (minSpeed, Speed -= speedConfig.speedModifier);
                break;
            case CollectablesEnum.Flight:
                //in case we have two or more flight effects
                foreach(var effect in activeEffects)
                {
                    if(effect.CollectableItem.CollectableConfiguration.type == CollectablesEnum.Flight)
                    {
                        return;
                    }
                }

                VerticalState = VerticalStateEnum.Regular;
                break;
        }
    }
}