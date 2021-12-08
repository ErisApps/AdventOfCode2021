// See https://aka.ms/new-console-template for more information

Console.WriteLine("Preparing input data");
var rawBingoData = File
	.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Resources", "day4.txt"))
	.ToList();

var rawBingoNumberSequence = rawBingoData.First().Split(',');

List<List<string?>> bingoBoards;

void InitializeBingoBoards()
{
	bingoBoards = new();

	List<string?> intermediateBingoBoard = new();
	foreach (var bingoBoardLine in rawBingoData.Skip(2))
	{
		if (string.IsNullOrWhiteSpace(bingoBoardLine))
		{
			bingoBoards.Add(intermediateBingoBoard);
			intermediateBingoBoard = new(25);
		}

		intermediateBingoBoard.AddRange(bingoBoardLine.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
	}
}


Console.WriteLine("Hello Day 4-1!");

InitializeBingoBoards();
var bingoNumberSequence = new Queue<string>(rawBingoNumberSequence);

bool ValidateWinCondition(List<string?> bingoBoard)
{
	bool ValidateWinConditionInternal(IEnumerable<string?> bingoBoardInternal) => bingoBoardInternal.All(x => x == null);

	for (var i = 0; i < bingoBoard.Count; i += 5)
	{
		if (ValidateWinConditionInternal(bingoBoard.Skip(i).Take(5)))
		{
			return true;
		}
	}

	for (var i = 0; i < bingoBoard.Count / 5; i++)
	{
		if (ValidateWinConditionInternal(bingoBoard.Where((_, index) => ((index - i) % 5) == 0).ToList()))
		{
			return true;
		}
	}

	return false;
}

(List<string?> bingoBoard, string winningNumber)? FindWinningBoard()
{
	while (bingoNumberSequence.TryDequeue(out var nextBingoNumber))
	{
		foreach (var bingoBoard in bingoBoards)
		{
			for (var i = 0; i < bingoBoard.Count; i++)
			{
				if (bingoBoard[i] == nextBingoNumber)
				{
					bingoBoard[i] = null;
				}
			}

			if (ValidateWinCondition(bingoBoard))
			{
				return (bingoBoard, nextBingoNumber);
			}
		}
	}

	return null;
}

var findWinningBoard = FindWinningBoard();
var remainingNumbersScore = findWinningBoard?.bingoBoard.OfType<string>().Select(int.Parse).Sum() ?? 0;
var winningNumber = int.TryParse(findWinningBoard?.winningNumber, out var winningNumberParsed) ? winningNumberParsed : 0;

Console.WriteLine($"Day 4-1 winning bingo board final score ({remainingNumbersScore} * {winningNumber}) => {remainingNumbersScore * winningNumber}");

Console.WriteLine("Hello Day 4-2!");

InitializeBingoBoards();
bingoNumberSequence = new Queue<string>(rawBingoNumberSequence);

List<(List<string?> bingoBoard, string winningNumber)> FindWinningBoards()
{
	var winningBoards = new List<(List<string?> bingoBoard, string winningNumber)>();

	while (bingoNumberSequence.TryDequeue(out var nextBingoNumber))
	{
		var i = 0;
		while (i < bingoBoards.Count)
		{
			var bingoBoard = bingoBoards[i];
			for (var j = 0; j < bingoBoard.Count; j++)
			{
				if (bingoBoard[j] == nextBingoNumber)
				{
					bingoBoard[j] = null;
				}
			}

			if (ValidateWinCondition(bingoBoard))
			{
				winningBoards.Add((bingoBoard, nextBingoNumber));
				bingoBoards.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	return winningBoards;
}

var winningBoards = FindWinningBoards();
var lastWinningBoard = winningBoards.Last();
remainingNumbersScore = lastWinningBoard.bingoBoard.OfType<string>().Select(int.Parse).Sum();
winningNumber = int.TryParse(lastWinningBoard.winningNumber, out winningNumberParsed) ? winningNumberParsed : 0;

Console.WriteLine($"Day 4-1 last winning bingo final board score ({remainingNumbersScore} * {winningNumber}) => {remainingNumbersScore * winningNumber}");