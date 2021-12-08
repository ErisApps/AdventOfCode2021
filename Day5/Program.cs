// See https://aka.ms/new-console-template for more information

Console.WriteLine("Preparing input data");
var thermalVentLineData = File
	.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day5.txt"))
	.Select(lineEntry =>
	{
		var coordinatesRaw = lineEntry.Split(" -> ");
		var coordinateOneRaw = coordinatesRaw[0].Split(',').Select(int.Parse).ToArray();
		var coordinateTwoRaw = coordinatesRaw[1].Split(',').Select(int.Parse).ToArray();

		// Possibly swap coordinates to make further implementation easier
		if (coordinateOneRaw[0] > coordinateTwoRaw[0] || coordinateOneRaw[0] == coordinateTwoRaw[0] && coordinateOneRaw[1] > coordinateTwoRaw[1])
		{
			(coordinateOneRaw, coordinateTwoRaw) = (coordinateTwoRaw, coordinateOneRaw);
		}

		return (startCoordinate: (x: coordinateOneRaw[0], y: coordinateOneRaw[1]), endCoordinate: (x: coordinateTwoRaw[0], y: coordinateTwoRaw[1]));
	})
	.ToList();

var startXMax = thermalVentLineData.Max(x => x.startCoordinate.x);
var endXMax = thermalVentLineData.Max(x => x.endCoordinate.x);
var combinedXMax = Math.Max(startXMax, endXMax) + 1;
var startYMax = thermalVentLineData.Max(x => x.startCoordinate.y);
var endYMax = thermalVentLineData.Max(x => x.endCoordinate.y);
var combinedYMax = Math.Max(startYMax, endYMax) + 1;

List<int> CalculateOverlapField(IEnumerable<((int x, int y) startCoordinate, (int x, int y) endCoordinate)> lineData)
{
	var field = Enumerable.Repeat(0, combinedXMax * combinedYMax).ToList();

	foreach (var (startCoordinate, endCoordinate) in lineData)
	{
		var xDiff = endCoordinate.x - startCoordinate.x;
		var yDiff = endCoordinate.y - startCoordinate.y;

		if (xDiff > 0)
		{
			// Should be able to cover both horizontal and diagonal lines
			for (var i = startCoordinate.x + (startCoordinate.y * combinedXMax); i <= endCoordinate.x + (endCoordinate.y * combinedXMax); i++)
			{
				field[i]++;
				if (yDiff > 0)
				{
					i += combinedXMax;
				}
				else if (yDiff < 0)
				{
					i -= combinedXMax;
				}
			}
		}
		else
		{
			// Covers vertical lines... supposedly
			for (var i = startCoordinate.x + (startCoordinate.y * combinedXMax); i <= endCoordinate.x + (endCoordinate.y * combinedXMax); i += combinedXMax)
			{
				field[i]++;
			}
		}
	}

	return field;
}

Console.WriteLine("Hello Day 5-1!");

var filteredLineData = thermalVentLineData.Where(lineEntry => lineEntry.startCoordinate.x == lineEntry.endCoordinate.x || lineEntry.startCoordinate.y == lineEntry.endCoordinate.y).ToList();

var fieldData = CalculateOverlapField(filteredLineData);

Console.WriteLine($"Day 5-1 overlapping coordinate count {fieldData.Count(x => x >= 2)}");