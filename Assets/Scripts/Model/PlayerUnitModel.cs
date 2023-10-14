using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class responsible for player run speed ( it affect all game items movement)
/// and it has a stuck with collectable items whih have effect on player. Another collectable items will be managed by another logic class
/// </summary>
public class PlayerUnitModel : IInitializable, IDisposable, ISpeedManager, IPlayerVerticalState
{
    private const float defaultSpeed = 2f;
    private const float minSpeed = 1f;

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
        activeEffects.Add(new AutoDisableEffect(OnEffectDisable, signal.ItemCollected));

        switch (signal.ItemCollected)
        {
            case CollectablesEnum.SpeedUp:
                Speed += 1.5f;
                break;
            case CollectablesEnum.SlowDown:
                Speed = Mathf.Max (minSpeed, Speed -= 1); // TODO: move it to collectables config
                break;
            case CollectablesEnum.Flight:
                VerticalState = VerticalStateEnum.Up;
                break;
        }
    }

    private void OnEffectDisable(AutoDisableEffect collectablesEnum)
    {
        activeEffects.Remove(collectablesEnum);
        switch (collectablesEnum.CollectablesEnum)
        {
            case CollectablesEnum.SpeedUp:
                Speed = Mathf.Max (minSpeed, Speed -= 1.5f); // TODO: move it to collectables config
                break;
            case CollectablesEnum.SlowDown:
                Speed += 1;
                break;
            case CollectablesEnum.Flight:
                //in case we have two or more flight effects
                foreach(var effect in activeEffects)
                {
                    if(effect.CollectablesEnum == CollectablesEnum.Flight)
                    {
                        return;
                    }
                }

                VerticalState = VerticalStateEnum.Regular;
                break;
        }
    }
}