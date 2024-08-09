namespace CRUDTests
{
    public class MyMathTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            MyMath mm = new MyMath();
            int input1 = 10, input2 = 20;
            int expected = input1 + input2;

            // Act
            int actual = mm.Add(input1, input2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
