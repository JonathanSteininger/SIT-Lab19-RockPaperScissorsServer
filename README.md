# Lab19-RockPaperScissorsServer

This project contains both the server application and the client application. Both applications are WinForm GUI apps.

<h3>ServerForm</h3>

The server application is the server that each client connects to when they run the client form. It maintains the state of multiple users through worker threads. 

When a client connects to the server it creates a new worker thread and maintains the state of that user so that other users can see the state of that user.

When the client chooses single player they will request a single player game to the server where the server will set up a single player game with a bot.
It will update the state of the player for anyone to see.

When the client chooses multi-player they will request a multi-player game to the server.
The server will set up a multiplayer game and change the players state to waiting for a player where other players are able to join the pending game.
When a player joins another players pending game they will be able to play rock papper scissors against each other.

<h3>ClientForm</h3>

The clients application when run will connect to the local server and send it some starting player such as the name of the current player. 

The user can change their name, start a single-player game, start a multi-player game, join a waiting for opponnent player's multiplayer.

The user must manually refresh the list of active players if they want to join someones game.
In order to join someones game they must double click on another player that is waitning for an opponent.

When the user has started or joined a game they can play rock papper scissors with the opponent.

<h2>Warning</h2>
I never really limit tested the client application. There are alot of cases I did not check for so you have been warned!
