# katalyst-battleships-csharp
[Original Battleships kata from Codurance](https://www.codurance.com/katas/battleships)
[Original Battleships rules](hasbro.com/common/instruct/battleship.pdf)

##  Introduction
This version of the classic game has three ships:

- Carrier: 4 cells  represented on a board with 'c'
- Destroyer: 3 cells  represented on a board with 'd'
- Gun Ship: 1 cell  represented on a board with 'g'

Create a program that allows the user to specify commands for playing battleship. The commands available are:

- addPlayer: Creates a player for the game.
- start: Starts a new game with a fleet of ships placed at user's defined (x,y) coordinates.
- endTurn: Ends the player's turn.
- print: This command will print out the game board (Exhibit A):

![image](https://github.com/felixeu31/katalyst-battleships-csharp/assets/22452588/88b9e3b1-760a-4d8c-ac61-6a0c9639adc9)

- fire: Launches a torpedo at the given (x,y) coordinates.
  - If the (x,y) coordinate is sea then the position will be marked with 'o'.
  - If the (x,y) coordinate is a ship then the position will be marked with 'x'.
  - If a ship has all cells hit then a message should print notifying the player the ship has sunk.


## Rules
When all ships have been sunk the game ends
when the game is finished the game should display a battle report the number of shots fired by each player, including hit/miss ship sunk.
Ships sunk should show the lowest possible coordinate for the given ship, for example:
A horizontal destroyer on grid reference (2,3), (3,3) and (4,3), but when reporting the sinking of the ship, you only need to reference the first coordinate.
A vertical destroyer on ref (5,5), (5,6) and (5,7) but you'll only need to reference (5,5) when reporting.
Using Exhibit A above, here is a battle report based on the ship positions:

![image](https://github.com/felixeu31/katalyst-battleships-csharp/assets/22452588/86dc3d67-8d89-47b5-b419-fa220150bf08)

Sunk ships have all their coordinates marked with an uppercase X and hit cells have a lower case x where they were not sunk.

## Restrictions
- Complete using outside-in
- Each player has maximum:
  - 1 Carrier
  - 2 Destroyers
  - 4 Gunships
- Grid is 10 x 10 to keep it simple (but the design should be open for enhancements)

## Hardcore enhancement (optional)
Implement an AI player


