
using System.Collections.Generic;

namespace Medrick.ComponentSystem.Core
{
    public interface Entity 
    {
        void AddComponent(Component component);
        T GetComponent<T>() where T : Component;

        IEnumerable<Component> AllComponents();
    }
}