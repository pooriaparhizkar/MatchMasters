using Medrick.ComponentSystem.Core;
using System;
using System.Collections.Generic;

using Position = UnityEngine.Vector2;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public class TileStackComponentCache : ComponentCache
    {
        public LockState lockState;

        public void TryCache(Component component)
        {
            switch (component)
            {
                case LockState lockState:
                    this.lockState = lockState;
                    break;
            }
        }
    }

    public class TileStack : CacheableBasicEntity<TileStackComponentCache>
    {
        public event Action OnDesturction = delegate { };
        Position position;

        Stack<Tile> stack = new Stack<Tile>();
        CellStack parent;

        public TileStack() : base (new TileStackComponentCache())
        {
        }

        public void SetParent(CellStack parent)
        {
            this.parent = parent;
        }

        public void Push(Tile tile)
        {
            stack.Push(tile);
            tile.SetParent(this);
        }

        public void Pop()
        {
            stack.Pop();
        }

        public void Destroy()
        {
            parent.DetachTileStack();
            OnDesturction.Invoke();
            parent = null;
        }

        public Tile Top()
        {
            return stack.Peek();
        }

        public bool IsDepleted()
        {
            return stack.Count == 0;
        }

        public Stack<Tile> Stack()
        {
            return stack;
        }

        public CellStack Parent()
        {
            return parent;
        }

        public void SetPosition(Position pos)
        {
            this.position = pos;
        }

        public Position Position()
        {
            return position;
        }

    }
}