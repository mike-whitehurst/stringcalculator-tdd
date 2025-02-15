namespace StringCalculator
{
    public class Calculator
    {
        private readonly List<string> _defaultDelimeters = [",", "\n"];
        private readonly int _maxValue = 1000;

        public int Add(string input)
        {
            var delims = GetDelims(input);
            var numbers = GetNumbers(input, delims);

            ThrowIfAnyNegativeNumbers(numbers);

            return numbers.Sum();
        }

        private List<string> GetDelims(string input)
        {
            var delims = new List<string>();

            if (ContainsCustomDelims(input))
            {
                var parts = GetParts(input);
                var delimPart = parts[0].Substring(2);

                if (DelimsUseBracketFormat(delimPart))
                {
                    delims.AddRange(GetBracketFormatDelims(delimPart));
                }
                else
                {
                    delims.Add(delimPart);
                }
            }
            else
            {
                delims.AddRange(_defaultDelimeters);
            }

            return delims;
        }

        private static bool ContainsCustomDelims(string input)
        {
            return input.StartsWith("//");
        }

        private static string[] GetParts(string input)
        {
            return input.Split('\n', 2);
        }

        private static bool DelimsUseBracketFormat(string delimPart)
        {
            return delimPart.StartsWith('[') && delimPart.EndsWith(']');
        }

        private static IEnumerable<string> GetBracketFormatDelims(string delimPart)
        {
            return delimPart
                .Split('[', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(delim => delim.TrimEnd(']'));
        }

        private IEnumerable<int> GetNumbers(string input, List<string> delims)
        {
            var numbersPart = GetNumbersPart(input);

            var numbers = numbersPart
                .Split(delims.ToArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .Where(number => number <= _maxValue);

            return numbers;
        }

        private static string GetNumbersPart(string input)
        {
            if (ContainsCustomDelims(input))
            {
                var parts = GetParts(input);
                return parts[1];
            }
            else
            {
                return input;
            }
        }

        private static void ThrowIfAnyNegativeNumbers(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(number => number < 0);

            if (negatives.Any())
            {
                throw new ArgumentException(string.Format("negatives not allowed: {0}", string.Join(", ", negatives)));
            }
        }
    }
}
