# Brightest ScreenPlay Package
A lightweight ScreenPlay Package
## ScreenPlay Design Pattern
The Screenplay Design Pattern, introduced by Serenity BDD framework, is a high-level, user-centered approach to test automation. 
It provides a way to describe tests in a way that closely resembles how end-users interact with the system. 
This pattern focuses on modeling the user’s journey through the application and representing it as a series of tasks. 
The main benefit of using this pattern is that it makes tests easy to understand, even for non-technical stakeholders, as the tests are described in terms of user actions.
## Getting Started and how to use
In the ScreenPlay pattern, the Actors are the ones doing things. In the package, there are 2 Actors defined that can be used in your test solution:
- CallAnApi
- OpenAWebPage
### Create an actor 
One of the first things that you need to do when you want to use this package is to define an Actor class that can do things
This is an example class that uses the CallAnAPI actor class:
```csharp
using Bright.ScreenPlay.Abilities;
using ScreenPlayExample.Responses;

namespace x.y.z
{
	public class SomeApi : CallAnAPi
	{
		public SomeApi(){
			Settings.BaseUrl = "SomeUrl"
		}
	}
}
```
The package also has some extension methods that allow you to write something like this:
```csharp
var coBRHATypeZorgverleners = await restponse.Content.ReadAsJsonAsync<List<CoBRHATypeZorgverlener>>();
````
This is an example class that uses the OpenAWebPage Actor class
```csharp
using Bright.ScreenPlay.Abilities;
using OpenQA.Selenium;

namespace x.y.z
{
	public class SomePage: OpenAWebPage
	{
			private IWebDriver webDriver;
		public SomePage(){
			Settings.BaseUrl = "SomeUrl"
		}
		public void OpenMainPage(IWebDriver webDriver)
		{
    			WebDriver = webDriver;
			this.webDriver = WebDriver;
			webDriver.Navigate().GoToUrl(Settings.BaseUrl);
			WaitUntilElmentVisableByXpath("//a[@id='menuRegisterLink']");
		}
	}
}
```
In the OpenAWebpage class, there are some methods that can be used to interact with the webpage.
### Create a question
To ensure the actor can do things, you need to create Question classes.
This is an example of an implemented Question Class:
```csharp
using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using ScreenPlayExample.Abilities;

namespace x.y.z
{
	public TheQuestion : Question<List<string>>
	{
		public static async Task<List<string>> PerformGetCodeTableAs(IPerformer actor, string table)
		{
    			return await actor.GetAbility<CodeTableAPI>().GetCodeTable(table);
		}	
	}
	public override List<string> PerformAs(IPerformer actor)
	{
    		return new List<string>();
	}
}
```
As you can see the Question class requires you to think about the response and also requires you to implement the PerformAs method.

### Usage in the tests
This is an example of how you can use the ScreenPlay package in your tests:
```csharp
using Bright.ScreenPlay.Actors;
using ScreenPlayExample.Abilities;
using ScreenPlayExample.Questions;

namespace ScreenPlayExample.StepDefinitions
{
    [Binding]
    public class CodeTableStepDefinitions
    {
        private Actor joe;
        private string _table;
        private List<string> _results;
        [Given(@"I want to search the (.*)")]
        public void GivenIWantToSearchTheTable(string table)
        {
            _table = table;
            joe = new Actor("Joe");
            joe.IsAbleTo<CodeTableAPI>();
        }

        [When(@"I send the request to search the code table")]
        public async Task WhenISendTheRequestToSearchTheCodeTable()
        {
            _results = await TheCodeTable.PerformGetCodeTableAs(joe, _table);
        }

        [Then(@"The results of that table should contain data")]
        public void ThenTheResultsOfThatTableShouldContainData()
        {
            _results.Should().NotBeEmpty();
            _results.Count.Should().BeGreaterThan(1);
        }
    }
}
```