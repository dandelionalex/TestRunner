A brief info to Runner assignment.

For this project, I decided to use Zenject because it is the most popular DI container.

Despite of all movement is fake ( the bottom panel just changes texture offset and items moved to the player), I decided to delegate speed responsibility to the Player class. To untie them even more, I created the ISpeedManager interface  - later, speed responsibility can move everywhere.

Coins implemented via Collectable type with CollectableEnum. 
After a collision with the player, the Signal is fired - so every Logic class can subscribe to this Signal on the event bus and do some logic.
For now, it is implemented only in the PlayerUnitModel, but I planned that it can be not only a player effect ( maybe just coin or die/loose game).



Things which I’ll improve:
- Add resource manager and load Collectables from resources.
- Move increase/decrease speed value to config. It can be fields on the prefabs of the collectible items.
- Refactor CollectableSpawner class.
- Add Cancelation toten to the AutoDisableEffect class. Refactor it.
- Add effector class and run collect effect there.