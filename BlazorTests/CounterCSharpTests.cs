using BlazorWebAppDemo.Client.Pages;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTests;

public class CounterCSharpTests : TestContext
{
    [Fact]
    public void Counter_ShouldReadZero_WhenFirstCreated()
    {
        //Arrange
        var cut = RenderComponent<Counter>();
        //Act

        //Assert
        cut.Find("p").MarkupMatches("<p role=status>Current count: 0</p>");
    }

    [Fact]
    public void Counter_ShouldIncrement_WhenButtonClicked()
    {
        //Arrange
        var cut = RenderComponent<Counter>();

        //Act
        cut.Find("button").Click();

        //Assert
        cut.Find("p").MarkupMatches("<p role=status>Current count: 1</p>");
    }
}
