using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        private bool _customDelimiter;
        private char[] _delimiters;

        public Calculator()
        {
            _delimiters = new[] {',', '\n'};
        }

        public int Add(string input)
        {
            var stringNumbers = ProcessInput(input);

            var digits = ConvertToNumbers(stringNumbers);

            return digits.Sum();
        }

        private IEnumerable<int> ConvertToNumbers(IEnumerable<string> numbers)
        {
            return from c in numbers select Convert.ToInt32(c);
        }

        private bool CheckTwoDelimiterTogether(string numbers)
        {
            var sb = new StringBuilder();
            Array.ForEach(_delimiters, x => sb.Append(x));
            var regex = @"\d[" + sb + "]{2}";
            return Regex.IsMatch(numbers, regex);
        }

        private bool ExtractDelimiters(string input)
        {
            _customDelimiter = input.StartsWith("//");

            if (_customDelimiter)
                _delimiters = new[] { input[2]};

            return _customDelimiter;
        }

        private IEnumerable<string> SplitNumbers(string numbers)
        {
            if (_customDelimiter)
            {
                foreach (var delimiter in _delimiters)
                {
                    if (!numbers.Contains(delimiter)) throw new DelimiterException();
                }
            }

            if (CheckTwoDelimiterTogether(numbers)) throw new DelimiterException();

            return numbers.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<string> ProcessInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return new[] { "0" };

            if (ExtractDelimiters(input))
                input = input.Substring(4);

            return SplitNumbers(input);
        }
    }
}
