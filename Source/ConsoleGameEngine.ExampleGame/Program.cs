// See https://aka.ms/new-console-template for more information
using ConsoleGameEngine;
using ConsoleGameEngine.ExampleGame;

using var game = new Game();
game.Scene = new TestScene(game);
game.Start();