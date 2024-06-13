﻿using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Actors;
using Bright.UnitTest.Tasks;
using Bright.UnitTest.Question;
using FluentAssertions;

namespace Bright.UnitTest
{
    [TestClass]
    public class ActorTest
    {
        [TestMethod]
        public void Actor_WhenCreated_ShouldHaveName()
        {
            // Arrange
            var actor = new Actor("John");
            // Act
            var name = actor.Name;
            // Assert
            name.Should().Be("John");
        }
        [TestMethod]
        public void Actor_WithAbility_Then_The_Type_Matches()
        {
            // Arrange
            var actor = new Actor("John");
            // Act
            actor.IsAbleToDoOrUse<CallAnApi>();
            var ability = actor.GetAbility<CallAnApi>();
            // Assert
            ability.Should().NotBeNull();
            ability.Should().BeOfType<CallAnApi>();
        }
        [TestMethod]
        public void Actor_WithTasks_Can_Perfrom_The_Task()
        {
            // Arrange
            var actor = new Actor("John");
            actor.IsAbleToDoOrUse<CallAnApi>();
            // Act
            actor.Perform<TestTask>();
            var ability = actor.GetAbility<CallAnApi>();
            // Assert
            ability.Settings.BaseUrl.Should().Be("Test");
        }   
        [TestMethod]
        public void Actor_WithQuestions_Can_Perfrom_The_Question()
        {
            // Arrange
            var actor = new Actor("John");
            actor.IsAbleToDoOrUse<CallAnApi>();
            // Act
            var result = actor.Perform<string>(new TestQuestion());
            // Assert
            result.Should().Be("Test");
        }
    }
}
