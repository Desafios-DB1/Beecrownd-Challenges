using _1000___Hello_World.Controllers;

namespace _1000___HelloWorld.Tests;

public class HelloControllerTests
{
    [Fact]
    public void Get_ReturnsHelloWorld()
    {
        var controller = new HelloController();
        
        var result = controller.Get();
        
        Assert.Equal("Hello World", result);
    }
}