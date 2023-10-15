#A brief info to Runner assignment.

For this project, I decided to use Zenject because it is the most popular DI container.

Despite of all movement is fake ( the bottom panel just changes texture offset and items moved to the player), I decided to delegate speed responsibility to the Player class. To untie them even more, I created the ISpeedManager interface  - later, speed responsibility can move everywhere.

##Collectables
Coins and other perks are implemented via Collectable type with CollectableEnum. 
Collectable configuration stored it ScriptableObject. So it is easy to duplicate it and create a slow-down item with a different speed or create a completely new behavior.
After a collision with the player, the Signal is fired - so every Logic class can subscribe to this Signal on the event bus and do some logic.
For now, it is implemented only in the PlayerUnitModel, but it is highly likely that it can be not only a player effect ( maybe just coin or die/loose game effect).
All collectable objects move using  CollectableSpawner class Update, which increase a little bit performance and makes it easy to control game speed.

##Things which I’ll improve:
- Add resource manager and load Collectables from resources.
- Refactor CollectableSpawner class.
- Add Cancelation toten to the AutoDisableEffect class. Refactor it.
- Add a pooling system to collectable items
- Add effector class and run collect effect there.