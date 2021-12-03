using System;
using System.Collections.Generic;
using Medrick.ComponentSystem.Core;
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
        private readonly Stack<Tile> stack = new Stack<Tile>();
        private CellStack parent;
        private Position position;

        public TileStack() : base(new TileStackComponentCache())
        {
        }

        public event Action OnDesturction = delegate { };

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
            position = pos;
        }

        public Position Position()
        {
            return position;
        }
    }
}