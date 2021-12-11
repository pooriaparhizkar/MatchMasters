using System;
using Medrick.ComponentSystem.Core;

namespace Medrick.Match3CoreSystem.Game
{
    public interface KeyType
    {
    }

    public class LockState : Component
    {
        private Type keyType;
        private long lastReleasedTick;

        public LockState()
        {
            lastReleasedTick = DateTime.UtcNow.Ticks;
        }

        public bool IsLockedBy<T>() where T : KeyType
        {
            return keyType == typeof(T);
        }

        public bool IsLocked()
        {
            return !(keyType == null);
        }

        public void LockBy<T>() where T : KeyType
        {
            keyType = typeof(T);
        }

        public void Release()
        {
            lastReleasedTick = DateTime.UtcNow.Ticks;
            keyType = null;
        }

        public bool IsFreeOrLockedBy<T>() where T : KeyType
        {
            return IsLocked() == false || IsLockedBy<T>();
        }

        public bool IsFree()
        {
            return !IsLocked();
        }

        public long LastReleaseLock()
        {
            return lastReleasedTick;
        }

        public Type KeyType()
        {
            return keyType;
        }
    }
}