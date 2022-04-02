﻿using NSubstitute;
using Xunit;
using Game.Application.Components;

namespace Game.UnitTests.Components
{
    public class ComponentBaseTests
    {
        [Fact]
        public void OnAwake_Called()
        {
            // Arrange
            var otherDummyComponent = Substitute.For<OtherDummyComponent>();
            var collection = new IComponent[] { otherDummyComponent };

            // Act
            var components = new ComponentCollection(collection);

            // Assert
            otherDummyComponent.Received().Create();
        }

        [Fact]
        public void OnRemoved_Called()
        {
            // Arrange
            var otherDummyComponent = Substitute.For<OtherDummyComponent>();
            var collection = new IComponent[] { otherDummyComponent };
            var components = new ComponentCollection(collection);

            // Act
            components.Dispose();

            // Assert
            otherDummyComponent.Received().Destroy();
        }
    }

    public interface IOtherDummyComponent
    {
        void Create();

        void Destroy();
    }

    public class OtherDummyComponent : ComponentBase, IOtherDummyComponent
    {
        public void Create()
        {
            // Left blank intentionally
        }

        public void Destroy()
        {
            // Left blank intentionally
        }
    }
}