using Medrick.ComponentSystem.Core;
using System;


namespace Medrick.Match3CoreSystem.Game
{
    public interface KeyType { };

    public class LockState : Component 
    {
        Type keyType = null;
        long lastReleasedTick; 

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

        public void LockBy<T>() where  T : KeyType
        {
            this.keyType = typeof(T);
        }

        public void Release()
        {
            lastReleasedTick = DateTime.UtcNow.Ticks;
            this.keyType = null;
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