using BlazorWebAppDemo.Components.Pages;
using Bunit;

namespace BlazorTests;

public class MyButtonTests : TestContext
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test1(bool confirmed)
    {
        //Arrange
        string confMessage = "Test";
        JSInterop.Setup<bool>("confirm", confMessage).SetResult(confirmed);
        bool clicked = false;
        Action onClick = () => { clicked = true; };
        
        var cut = RenderComponent<MyButton>(p => p
            .Add(x => x.ConfirmMessage, confMessage)
            .Add(x => x.OnClick, onClick)
        );

        //Act
        cut.Find("button").Click();

        //Assert
        Assert.Equal(confirmed, clicked);

    }
}