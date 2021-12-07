// See https://aka.ms/new-console-template for more information

Console.WriteLine("Preparing input data");
var diagnosticData = File
	.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day3.txt"))
	.ToArray();

Console.WriteLine("Hello Day 3-1!");

Console.WriteLine($"Dataset size: {diagnosticData.Length}");
var lineEntryLength = diagnosticData.First().Length;
Console.WriteLine($"Line entry length: {lineEntryLength}");


var aggregatedResultSet = diagnosticData.Aggregate(new int[diagnosticData.First().Length], (aggregatedResultSet, lineEntry) =>
{
	for (var i = 0; i < lineEntry.Length; i++)
	{
		if (lineEntry[i] == '1')
		{
			aggregatedResultSet[i]++;
		}
	}

	return aggregatedResultSet;
});

var gammaRateBinaryStringRepresentation = string.Join("", aggregatedResultSet.Select(bitCount => Math.Round((double) bitCount / diagnosticData.Length)));
var gammaRate = Convert.ToInt32(gammaRateBinaryStringRepresentation, 2);

var epsilonRateBinaryStringRepresentation = Convert.ToString(~gammaRate, 2)[^lineEntryLength..];
var epsilonRate = Convert.ToInt32(epsilonRateBinaryStringRepresentation, 2);

Console.WriteLine($"Day 3-1 final power consumption {gammaRateBinaryStringRepresentation} ({gammaRate}) * {epsilonRateBinaryStringRepresentation} ({epsilonRate}) => {gammaRate * epsilonRate}");