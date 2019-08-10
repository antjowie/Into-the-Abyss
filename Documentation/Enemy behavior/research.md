# Enemy behavior research

## ToC
- [Enemy behavior research](#Enemy-behavior-research)
	- [ToC](#ToC)
	- [Requirements](#Requirements)
	- [Approaches](#Approaches)
		- [Inheritance based](#Inheritance-based)
	- [Solution](#Solution)

## Requirements
Enemies need to have consitent behavior. To do this, their behavior will be scripted. This means that a simple FSM should suffice.  
All enemies perceive information the same way, by vision. Since the game will be room based, we can assume that the enemy is always engaging at the player. This means that most enemies only differ in their behavior (think about Katana Zero, some enemies wind up their attack).

Requirements: 
- Able to define behavior of an enemy. 

## Approaches
### Inheritance based
Every enemy has the same perception of the world. They all engage when the enemies are spawned. We can add a FSM that contains the state of the enemy. The problem with this is that the FSM needs to know about the enemy because the FSM has to move it.  
To solve this, it may be better to use inheritence. This gives us lots of freedom to define how state is managed. The parent class will contain all the functions that interact with the game, the child class will only define in what state it is and what it is supposed to do in that state.

This suggests two patterns. [Strategy](https://refactoring.guru/design-patterns/strategy) and [FSM](https://refactoring.guru/design-patterns/state). To explain the two briefly in our context.  
**Strategy** suggests that there is a context that uses algorithms. It delegates it works to any compatible algorithm. In our case, an **enemy controller** is the context. It uses an **enemy behavior** as algorithm.  
This may serve usefull but the problem with this is that our 'algorithm' needs to know about the game world to decide what state it will become. This means that we either make it a game object to allow quering, or we pass in a structure of variables. The last one makes more sense because an enemy only needs to update the behavior.

**FSM** suggests that there are a finite amount of states that a object can be in. Our **enemy behavior** can be a FSM since the state of it depends on certain conditions. It may be overkill though, since enemies mostly have one behavior which is attacking.

## Solution
These patterns seem to be overkill for our game. The strategy pattern adds more limitations which is nice but is also adds more complexity because of additional classes. The FSM is definitely overkill since the enemy is always in the same state.  
The best solution is probably to just use inheritance and overwrite the update function. 