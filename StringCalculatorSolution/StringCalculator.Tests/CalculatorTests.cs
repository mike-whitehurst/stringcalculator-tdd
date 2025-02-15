namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        // 1. Create a simple String calculator with a method int Add(string numbers)
        // - The method can take 0, 1 or 2 numbers, and will return their sum(for an empty string it will return 0) for example "" or "1" or "1,2"
        // - Start with the simplest test case of an empty string and move to 1 and two numbers
        // - Remember to solve things as simply as possible so that you force yourself to write tests you did not think about
        // - Remember to refactor after each passing test

        // 2. Allow the Add method to handle an unknown amount of numbers

        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("   ", 0)]
        public void Add_ReturnsZero_WhenInputIsEmpty(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        [InlineData("1,2,3", 6)]
        [InlineData("1,2,3,10", 16)]
        public void Add_ReturnsSum_WhenInputContainsNumbers(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // 3. Allow the Add method to handle new lines between numbers (instead of commas).
        // - the following input is ok: "1\n2,3" (will equal 6)
        // - the following input is NOT ok: "1,\n" (not need to prove it - just clarifying)

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("1\n2\n3\n10", 16)]
        public void Add_ReturnsSum_WhenDelimitedByNewLine(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // 4. Support different delimiters
        // - to change a delimiter, the beginning of the string will contain a separate line that looks like this: "//[delimiter]\n[numbers…]"
        // - for example "//;\n1;2" should return three where the default delimiter is ';'.
        // - the first line is optional. all existing scenarios should still be supported.

        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;2;3", 6)]
        public void Add_ReturnsSum_WhenCustomDelimeterProvided(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // 5. Calling Add with a negative number will throw an exception "negatives not allowed" - and the negative that was passed.
        //    if there are multiple negatives, show all of them in the exception message.

        [Theory]
        [InlineData("-1", "negatives not allowed: -1")]
        [InlineData("-1,-2", "negatives not allowed: -1, -2")]
        public void Add_ThrowsArgumentException_WhenAnyNumberIsNegative(string numbers, string expectedMessage)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            Action act = () => sut.Add(numbers);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }

        // 6. Numbers bigger than 1000 should be ignored, so adding 2 + 1001 = 2

        [Theory]
        [InlineData("2, 999", 1001)]
        [InlineData("2, 1000", 1002)]
        [InlineData("2, 1001", 2)]
        [InlineData("1001, 1002", 0)]
        public void Add_ReturnsSum_IgnoresLargeNumbers(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // 7. Delimiters can be of any length with the following format: "//[delimiter]\n" for example: "//[***]\n1***2***3" should return 6

        // 8. Allow multiple delimiters like this: "//[delim1][delim2]\n" for example "//[*][%]\n1*2%3" should return 6.

        // 9. Make sure you can also handle multiple delimiters with length longer than one char.

        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[*][%]\n1*2%3", 6)]
        [InlineData("//[**][%%][$$]\n1**2%%3$$10", 16)]
        public void Add_ReturnsSum_WhenUsingBracketDelimFormat(string numbers, int expectedResult)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(numbers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}