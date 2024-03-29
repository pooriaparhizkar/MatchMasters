﻿using System.Collections.Generic;

namespace Medrick.ComponentSystem.Core
{
    public class BasicSpecializedEntity<T> : SpecializedEntity<T> where T : Component
    {
        private readonly BasicEntity basicEntity = new BasicEntity();

        private readonly List<T> compList = new List<T>();

        public void AddComponent(T component)
        {
            basicEntity.AddComponent(component);
            compList.Add(component);
        }

        public List<T> AllComponents()
        {
            return compList;
        }

        public U GetComponent<U>() where U : T
        {
            return basicEntity.GetComponent<U>();
        }
    }
}