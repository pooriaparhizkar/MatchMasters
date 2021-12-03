using Medrick.ComponentSystem.Core;
using NUnit.Framework;

namespace Medrick.Tests.ComponentSystem.Core
{
    internal class ComponentA : Component
    {
    }

    internal class ComponentB : Component
    {
    }

    internal class ComponentC : Component
    {
    }

    internal class ComponentParent : Component
    {
    }

    internal class ComponentChild : ComponentParent
    {
    }

    public class BasicEntityTest
    {
        [Test]
        public void HasTheAddedComponents()
        {
            var entity = new BasicEntity();

            var componentA = new ComponentA();
            var componentB = new ComponentB();

            entity.AddComponent(componentA);
            entity.AddComponent(componentB);

            Assert.That(entity.GetComponent<ComponentA>(), Is.SameAs(componentA));
            Assert.That(entity.GetComponent<ComponentB>(), Is.SameAs(componentB));
        }

        [Test]
        public void ShouldFindComponentByParentType()
        {
            var entity = new BasicEntity();

            var componentChild = new ComponentChild();

            entity.AddComponent(componentChild);

            Assert.That(entity.GetComponent<ComponentParent>(), Is.SameAs(componentChild));
        }
    }
}