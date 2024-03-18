using System;
using System.Collections.Generic;

public class CodeGenerator
{
	private readonly List<char> _letters;
	private readonly List<char> _digits;
	private readonly List<char> _characters;
	private readonly Random _random;

	public CodeGenerator()
	{
		_letters = new List<char> { 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'K', 'L', 'M', 'N', 'P', 'R', 'T', 'X', 'Y', 'Z' };
		_digits = new List<char> { '2', '3', '4', '5', '7', '9' };
		_characters = new List<char>(_letters);
		_characters.AddRange(_digits);
		_random = new Random();
	}

	public List<string> GenerateCodes(int count)
	{
		var validCodes = new List<string>();
		while (validCodes.Count < count)
		{
			string code = GenerateRandomCode();
			if (!validCodes.Contains(code) && CheckFirstRule(code) && CheckComplexRule(code))
			{
				validCodes.Add(code);
			}
		}
		return validCodes;
	}

	private string GenerateRandomCode()
	{
		string code = "";
		for (int i = 0; i < 8; i++)
		{
			code += _characters[_random.Next(_characters.Count)];
		}
		return code;
	}

	private int GetDigitIndex(string code)
	{
		for (int i = 0; i < code.Length; i++)
		{
			char character = code[i];
			if (_digits.Contains(character))
			{
				return i;
			}
		}
		return -1;
	}

	private bool CheckFirstRule(string code)
	{
		int digitIndex = GetDigitIndex(code);
		if (digitIndex != -1)
		{
			char letter = _letters[_letters.Count - 1 - (code[digitIndex] % 8)];
			return code[code[digitIndex] % 8] == letter;
		}
		return false;
	}

	private bool CheckComplexRule(string code)
	{
		bool firstOdd = false;
		bool secondEven = false;

		for (int i = 0; i < code.Length; i++)
		{
			char character = code[i];
			int asciiValue = (int)character;
			int mod2 = asciiValue % 2;

			if (!firstOdd && mod2 == 1)
			{
				int index = asciiValue % 8;
				char firstComparisonCharacter = code[index];
				int firstComparisonMod2 = (int)firstComparisonCharacter % 2;
				if (firstComparisonMod2 == 1)
				{
					firstOdd = true;
					break;
				}
				else
				{
					break;
				}
			}
		}

		for (int i = code.Length - 1; i >= 0; i--)
		{
			char character = code[i];
			int asciiValue = (int)character;
			int mod2 = asciiValue % 2;

			if (!secondEven && mod2 == 0)
			{
				int index = asciiValue % 8;
				char secondComparisonCharacter = code[index];
				int secondComparisonMod2 = (int)secondComparisonCharacter % 2;

				if (secondComparisonMod2 == 0)
				{
					secondEven = true;
					break;
				}
				else
				{
					break;
				}

			}
		}
		return firstOdd && secondEven;
	}
}

public class Program
{
	public static void Main(string[] args)
	{
		CodeGenerator generator = new CodeGenerator();
		var validCodes = generator.GenerateCodes(100);

		Console.WriteLine("Generated Codes:");
		foreach (var code in validCodes)
		{
			Console.WriteLine(code);
		}
		Console.ReadLine();
	}
}
