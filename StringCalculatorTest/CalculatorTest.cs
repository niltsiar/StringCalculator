using System;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test(Description = "Sumar cadena vacía")]
        public void ItAddsEmptyString()
        {
            var result = _calculator.Add(string.Empty);

            Assert.AreEqual(0, result);
        }

        [Test(Description = "Realizar la suma con un número")]
        public void ItAddsOneNumber()
        {
            var result = _calculator.Add("1");

            Assert.AreEqual(1, result);
        }

        [Test(Description = "Sumar un caracter no numérico")]
        public void ItThrowsAnExceptionWhenOneNonNumericalCharacterAsEntry(
            [Values("a", "b", "ñ", "A", "?", "@")] string character)
        {
            Assert.Throws<FormatException>(() => _calculator.Add(character));
        }

        [Test(Description = "Sumar dos números separados por ,")]
        public void ItAddsTwoNumbers()
        {
            var result = _calculator.Add("1,2");

            Assert.AreEqual(3, result);
        }

        [Test(Description = "Sumar multiples con caracteres raros separados por ,")]
        public void ItThrowsAnExceptionWhenTwoNonNumericalCharacterAsEntry(
            [Values("a", "b", "ñ", "A", "?", "@")] string character1,
            [Values("a", "b", "ñ", "A", "?", "@")] string character2)
        {
            Assert.Throws<FormatException>(() => _calculator.Add(character1 + ',' + character2));
        }

        [Test(Description = "Sumar varios números separados por ,")]
        public void ItAddsMultipleNumbers()
        {
            var result = _calculator.Add("1,2,3,4,5,6,7,8");

            Assert.AreEqual(36, result);
        }

        [Test(Description = "Sumar dos números con delimitador , y \n")]
        public void ItAddsTwoNumersSeparatedByCommaOrCrLf()
        {
            var result = _calculator.Add("1\n2,3");

            Assert.AreEqual(6, result);
        }

        [Test(Description = "Entrada errónea dos separadores juntos")]
        public void ItThowsAnExceptionWithTwoSeparatorsTogether(
            [Values(',', '\n')] char delimiter1,
            [Values(',', '\n')] char delimiter2)
        {
            Assert.Throws<DelimiterException>(() => _calculator.Add("1" + delimiter1 + delimiter2));
        }

        [Test(Description = "Entrada con delimitador al principio")]
        public void ItAddTwoNumbersWithInitialDelimiter()
        {
            var result = _calculator.Add("//;\n1;2");

            Assert.AreEqual(3, result);
        }

        [Test(Description = "Usar un delimitador distinto al declarado al inicio"), Sequential]
        public void ItThrowsAnExceptionWhenUseOneDelimiterDifferentFromDeclaredInitially(
            [Values(';', ',')] char delimiter1,
            [Values(',', ';')] char delimiter2)
        {
            var expression = "//" + delimiter1 + "\n1" + delimiter2 + "2";
            Assert.Throws<DelimiterException>(() => _calculator.Add(expression));
        }
    }
}
