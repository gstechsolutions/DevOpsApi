namespace DevOpsApi.core.api.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var num1 = 10;
            var num2 = 20;

            var total = num1 + num2;

            Assert.Equal(30, total);
        }
    }
}