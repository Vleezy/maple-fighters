﻿using Common.ComponentModel.Tests;
using Common.UnitTestsBase;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Common.ComponentModel.Generic.UnitTests
{
    public class ComponentsContainerTests
    {
        [Fact]
        public void Add_Returns_UnexposableComponent()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);

            // Act
            var component = componentsContainer.Add(new UnexposableComponent());

            // Assert
            component.ShouldNotBeNull();
        }

        [Fact]
        public void AddAndExpose_Returns_ExposableComponent()
        {
            // Arrange
            IExposableComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);

            // Act
            var component = componentsContainer.Add(new ExposableComponent());

            // Assert
            component.ShouldNotBeNull();
        }

        [Fact]
        public void Add_Throws_Exception_When_Two_Same_Components_Added()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new UnexposableComponent());

            // Act & Assert
            Should.Throw<ComponentModelException>(
                () => componentsContainer.Add(new UnexposableComponent()));
        }

        [Fact]
        public void AddAndExpose_Throws_Exception_When_Two_Same_Components_Added()
        {
            // Arrange
            IExposableComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new ExposableComponent());

            // Act & Assert
            Should.Throw<ComponentModelException>(
                () => componentsContainer.Add(new ExposableComponent()));
        }

        [Fact]
        public void Remove_Exposable_Component()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new ExposableComponent());

            // Act
            componentsContainer.Remove<ExposableComponent>();

            // Assert
            componentsContainer.Get<IExposableComponent>().ShouldBeNull();
        }

        [Fact]
        public void Remove_Unexposable_Component()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new UnexposableComponent());

            // Act
            componentsContainer.Remove<UnexposableComponent>();

            // Assert
            componentsContainer.Get<IUnexposableComponent>().ShouldBeNull();
        }

        [Fact]
        public void Get_Returns_Exposable_Component()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new ExposableComponent());

            // Act
            var exposableComponent = componentsContainer.Get<IExposableComponent>();

            // Assert
            exposableComponent.ShouldNotBeNull();
        }

        [Fact]
        public void Get_Returns_Exposable_Only_Component()
        {
            // Arrange
            IExposableComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new ExposableComponent());

            // Act
            var exposableComponent = componentsContainer.Get<IExposableComponent>();

            // Assert
            exposableComponent.ShouldNotBeNull();
        }

        [Fact]
        public void Get_Returns_Unexposable_Component()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new UnexposableComponent());

            // Act
            var unexposableComponent = componentsContainer.Get<IUnexposableComponent>();

            // Assert
            unexposableComponent.ShouldNotBeNull();
        }

        [Fact]
        public void Get_Throws_Error_When_Not_Interface()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            componentsContainer.Add(new UnexposableComponent());

            // Act && Assert
            Should.Throw<ComponentModelException>(() => 
                componentsContainer.Get<UnexposableComponent>());
        }

        [Fact]
        public void Dispose_Components()
        {
            // Arrange
            IComponentsContainer componentsContainer = new ComponentsContainer<IDummyObject>(null);
            var otherTestableComponent = componentsContainer.AddAndMock<IOtherTestableComponent>();
            componentsContainer.Add(new TestableComponent());

            // Act
            componentsContainer.Dispose();

            // Assert
            otherTestableComponent.Received().Test();
        }
    }

    public interface IDummyObject
    {
        // Left blank intentionally
    }

    public interface IUnexposableComponent
    {
        // Left blank intentionally
    }

    [ComponentSettings(ExposedState.Unexposable)]
    public class UnexposableComponent : ComponentBase<IDummyObject>, IUnexposableComponent
    {
        // Left blank intentionally
    }

    public interface IExposableComponent
    {
        // Left blank intentionally
    }

    [ComponentSettings(ExposedState.Exposable)]
    public class ExposableComponent : ComponentBase<IDummyObject>, IExposableComponent
    {
        // Left blank intentionally
    }

    [ComponentSettings(ExposedState.Unexposable)]
    public class TestableComponent : ComponentBase<IDummyObject>
    {
        protected override void OnRemoved()
        {
            base.OnRemoved();

            var otherTestableComponent = Components.Get<IOtherTestableComponent>()
                .AssertNotNull();
            otherTestableComponent.Test();
        }
    }

    public interface IOtherTestableComponent
    {
        void Test();
    }
}