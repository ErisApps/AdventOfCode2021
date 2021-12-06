// See https://aka.ms/new-console-template for more information

using System.Numerics;
using Day2.Models;

Console.WriteLine("Preparing input data");
var instructions = File
	.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day2.txt"))
	.Select(instruction =>
	{
		var instructionSet = instruction.Split();
		return (commandType: Enum.Parse<CommandTypes>(instructionSet[0], true), value: int.Parse(instructionSet[1]));
	})
	.ToList();

Console.WriteLine("Hello Day 2-1!");

var currentPosition = Vector2.Zero;
foreach (var (commandType, value) in instructions)
{
	currentPosition = commandType switch
	{
		CommandTypes.Forward => new Vector2(currentPosition.X + value, currentPosition.Y),
		CommandTypes.Up => new Vector2(currentPosition.X, currentPosition.Y - value),
		CommandTypes.Down => new Vector2(currentPosition.X, currentPosition.Y + value),
		_ => throw new NotSupportedException()
	};
}

Console.WriteLine($"Day 2-1 final location {currentPosition} => {currentPosition.X * currentPosition.Y}");

Console.WriteLine("Hello Day 2-2!");

(int X, long Y, int Aim) currentPos = (X: 0, Y: 0, Aim: 0);
foreach (var (commandType, value) in instructions)
{
	currentPos = commandType switch
	{
		CommandTypes.Forward => (currentPos.X + value, currentPos.Y + value * currentPos.Aim, currentPos.Aim),
		CommandTypes.Up => (currentPos.X, currentPos.Y, currentPos.Aim - value),
		CommandTypes.Down => (currentPos.X, currentPos.Y, currentPos.Aim + value),
		_ => throw new NotSupportedException()
	};

	Console.WriteLine($"Current instruction: {commandType}-{value} => new result: {currentPos}");
}

Console.WriteLine($"Day 2-2 final location {currentPos} => {currentPos.X * currentPos.Y}");