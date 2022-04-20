// See https://aka.ms/new-console-template for more information
using ConsoleGameEngine;
using ConsoleGameEngine.ExampleGame;

using var game = new Game();
var scene = new TestScene(game);
game.Start(scene);