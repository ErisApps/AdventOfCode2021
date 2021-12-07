// See https://aka.ms/new-console-template for more information

Console.WriteLine("Preparing input data");
var diagnosticData = File
	.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day3.txt"))
	.ToArray();

Console.WriteLine("Hello Day 3-1!");

Console.WriteLine($"Dataset size: {diagnosticData.Length}");
var lineEntryLength = diagnosticData.First().Length;
Console.WriteLine($"Line entry length: {lineEntryLength}");


var aggregatedResultSet = diagnosticData.Aggregate(Enumerable.Repeat(0, lineEntryLength).ToArray(), (aggregatedResultSet, lineEntry) =>
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

Console.WriteLine("Hello Day 3-2!");

var oxygenRatings = diagnosticData.ToList();
var co2ScrubberRatings = diagnosticData.ToList();

for (var i = 0; i < lineEntryLength; i++)
{
	const char ZERO = '0';
	const char ONE = '1';

	List<string> FilterValues(IReadOnlyCollection<string> inputData, char primaryCharacter, char secondaryCharacter)
	{
		var count = inputData.Count(line => line[i] == ONE);
		var filterCharacter = count * 2 >= inputData.Count ? primaryCharacter : secondaryCharacter;
		return inputData.Where(line => line[i] == filterCharacter).ToList();
	}
	if (oxygenRatings.Count > 1)
	{
		oxygenRatings = FilterValues(oxygenRatings, ONE, ZERO);
	}

	if (co2ScrubberRatings.Count > 1)
	{
		co2ScrubberRatings = FilterValues(co2ScrubberRatings, ZERO, ONE);
	}
}

Console.WriteLine($"Day 3-2 final life support rating: {oxygenRatings.Single()} * {co2ScrubberRatings.Single()} => {Convert.ToInt32(oxygenRatings.Single(), 2) * Convert.ToInt32(co2ScrubberRatings.Single(), 2)}");