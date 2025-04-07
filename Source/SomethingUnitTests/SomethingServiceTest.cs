namespace SomethingUnitTests
{
    public class SomethingServiceTest
    {

        [Fact]
        public void Constructor_WithValidCodeAndValue_CreatesInstance()
        {
            const int expectedCode = 42;
            const string expectedValue = "Test Value";

            var codeValue = new CodeValue(expectedCode, expectedValue);
            codeValue.Code.Should().Be(expectedCode);
            codeValue.Value.Should().Be(expectedValue);
        }
    }
}