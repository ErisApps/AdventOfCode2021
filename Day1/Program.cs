// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello Day 1-1!");

var depths = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day1_1.txt")).Select(int.Parse).ToList();
var prevDepth = depths.First();
var increaseCount = 0;
foreach (var currentDepth in depths)
{
	if (currentDepth > prevDepth)
	{
		increaseCount++;
	}

	prevDepth = currentDepth;
}

Console.WriteLine($"Day 1-1 increase count: {increaseCount}");