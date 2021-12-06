// See https://aka.ms/new-console-template for more information

Console.WriteLine("Preparing input data");
var depths = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day1.txt")).Select(int.Parse).ToList();

Console.WriteLine("Hello Day 1-1!");

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


Console.WriteLine("Hello Day 1-2!");

var slidingWindowQueue = new Queue<int>();
prevDepth = depths.Take(3).Sum();
increaseCount = 0;

foreach (var currentDepth in depths)
{
	if (slidingWindowQueue.Count >= 3)
	{
		slidingWindowQueue.TryDequeue(out _);
	}

	slidingWindowQueue.Enqueue(currentDepth);

	if (slidingWindowQueue.Count >= 3)
	{
		var windowedDepth = slidingWindowQueue.Sum();
		if (windowedDepth > prevDepth)
		{
			increaseCount++;
		}

		prevDepth = windowedDepth;
	}
}

Console.WriteLine($"Day 1-2 increase count: {increaseCount}");