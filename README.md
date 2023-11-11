# Overview

Welcome to the Chess Playground Engine, a project designed to explore the fundamental mechanics of chess through the lens of a simplified engine. This project serves as a playground for implementing basic chess piece movements, scoring systems, and the Minimax algorithm with Alpha-Beta pruning.

The Chess Playground Engine is by no means a full-fledged chess engine. It does not support all chess pieces nor complex chess situations such as 'en passant' or 'castling'. 

> Overall, I've reached my goal of understanding the very basic structure of a chess engine and its basic algorithms, i.e. how to represent pieces and their movements, generate legal moves, evaluate positions (rudimentary, though) and how to implement the Minimax algorithm with Alpha-Beta pruning. Therefore, I do not plan to continue on this journey for now (and if I would, I would invest more time to figure out state of the art approaches, optimizations, etc...)

## Interesting Packages and Files

The base package is `chess`.

- `Board.Board` contains the representation of the chess board and the pieces on it.
- Subclasses of `Board.Piece` implement basic chess piece movements for queen, king, and knight.
- `Board.Loader` parses a game position from a file `game.txt`
- `Engine.Engine` implements the core logic of the engine, including the Minimax algorithm with Alpha-Beta pruning.
- `Engine.Score` computes a score for a given board position.
- `Engine.GameState` contains multiple helper functions to determine if the game is over (checkmate, stalemate, etc.)

## Build and run

This is a standard .NET Core project. To build and run, use the following commands:

```
dotnet build
dotnet run
```

## Example

The following is an example of a game position in `game.txt` (white to move first):

```
8 . . . . . . . . 
7 . . . . . . . . 
6 . . . . . . . . 
5 . . . Q . . . . 
4 . . . . . N . . 
3 . . K . . . . . 
2 . . . . . . X . 
1 . . . . n . . k 
  a b c d e f g h 
```

and when running the program, the following output is produced:

```
8 . . . . . . . . 
7 . . . . . . . . 
6 . . . . . . . . 
5 . . . Q . . . . 
4 . . . . . . . . 
3 . . K . . . . . 
2 . . . . N . X . 
1 . . . . n . . k 
  a b c d e f g h 



8 . . . . . . . . 
7 . . . . . . . . 
6 . . . . . . . . 
5 . . . Q . . . . 
4 . . . . . . . . 
3 . . K . . . . . 
2 . . . . N . X k 
1 . . . . n . . . 
  a b c d e f g h 



8 . . . . . . . . 
7 . . . . . . . . 
6 . . . . . . . . 
5 . . . . . . . Q 
4 . . . . . . . . 
3 . . K . . . . . 
2 . . . . N . X k 
1 . . . . n . . . 
  a b c d e f g h 
Duration: 00:00:16.6558730
```
Computation ends when the engine finds a checkmate or stalemate.

### The special piece Block

A special piece used to block a square from being moved to. This allows us to create special scenarios for testing which limit the possibilities of the board and nudge piece into specific positions. The block piece is represented by the letter `X` in the game position file `game.txt`. If I ever decide to continue with the engine, the block piece will be removed, and its corresponding code will be removed as well. I've marked the respective code with a `BLOCK` comment.

