# Chapter 2 : The West World Project

_A practical example on how to create and use Finite State Machines (FSM)_

This is a game environment in which agents inhabit an old western style gold mining town, named West World. This digital world is implemented as a text based console application, which will show any state change or output from the agents.

The first agent that will be implemented is Bob, a miner.

There are four distinct locations in this world: 
- A gold mine, where Bob the miner will work.
- A bank, where Bob will deposit his gold nuggets.
- A saloon, where Bob will rest and drink.
- A home, where Bob sleeps until he has to work again.

Bob should change state depending on variables like thirst, fatigue, and how much gold he has mined.

Later, the wife will be added to the world.


----

## Documentation

### State Design Pattern

Represent each state in its own class. Each state will encapsulate its transition logic. It's advisable that each state has _Enter()_ and _Exit()_ methods for entering or exiting from state transitions, that are executed once.

How the state transition happens:
1. The agent's _ChangeState()_ method first calls the _Exit()_ method of its current state.
2. Then it assigns a new state to its current state.
3. Finishes by calling the _Enter()_ method of its new current state.

_This design pattern is also useful to structure other elements of the same that have states, such as menu state, save state, options state, etc._

### _BaseGameEntity_ class

All entities in this world inherit from this class. It only stores an ID and has a virtual method _Update()_. This method should be implemented in all subclasses and will be used to update their internal state machine.

### _State_ class

The state class. Each concrete state will inherit from this one, and it will implement a singleton pattern, meaning that the actors will have to share it. By using the Singleton pattern, they will not be able to persist agent-specific data within them, and thus, it has to be saved externally. Uses generics to abstract some of the parameters to the methods.

### _Miner_ class

Derived from _BaseGameEntity_ and contains members and attributes the Miner will posses, such as Health, Fatigue, Position, etc. Its _Update()_ method is simple enough, it would update the thirst value. 

Miner states:
- **EnterMineAndDigForNugget** : If the miner is not located at the gold mine, change location. If already at the gold mine, dig out gold nuggets. When pockets are full, change state to _VisitBankAndDepositGold_, and if while digging he finds himself thirsty, stop and change state to _QuenchThirst_.
- **VisitBankAndDepositGold** : Miner walks to the bank and deposits his gold nuggets. If he considers himself wealthy enough, change state to _GoHomeAndSleepUntilRested_. Otherwise, goes back to _EnterMineAndDigForNugget_.
- **GoHomeAndSleepUntilRested** : The miner returns to his home and sleeps until his fatigue level drops below a threshold. Then he changes state to _EnterMineAndDigForNugget_.
- **QuenchThirst** : When the Miner's thirst level falls below a threshold, he'll enter this state. When the thirst is quenched, changes state back to _EnterMineAndDigForNugget_.

![miner fsm diagram](minerfsm.png "FSM diagram")

![miner uml diagram](mineruml.png "miner UML")

### _EnterMineAndDigForNuggetState_ class

 In this state, if the miner is not located at the gold mine, change location to the gold mine. Once there, dig out gold nuggets until the pockets are full, when he should change state to _VisitBankAndDepositGold_, and if while digging he finds himself thirsty, stop and change state to _QuenchThirst_.
 
### _VisitBankAndDepositGoldState_ class

This is the state the miner should enter after mining enough nuggets. He will walk to the bank and deposit them. If he considers himself wealthy enough, change state to _GoHomeAndSleepUntilRested_. Otherwise, goes back to _EnterMineAndDigForNugget_.
 
### _GoHomeAndSleepUntilRestedState_ class

The miner starts with this state. It will return to this state once he's wealthy enough. The miner returns and sleeps until his fatigue level drops below a threshold. Then he changes state to _EnterMineAndDigForNugget_.
 
### _QuenchThirstState_ class

When the Miner's thirst level falls below a threshold, he'll enter this state. When the thirst is quenched, changes state back to _EnterMineAndDigForNugget_.

### Global States and State Blips

Global States are states that agents could enter at any given time, irrespective of conditions, or repeated in several parts of the same FSM. Like in The Sims, when a Sim needs to go to the bathroom, no matter what action it was doing at the time.

State Blips are states that agents will enter but when exiting them, will always return to the previous state they were.