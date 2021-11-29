using Medrick.ComponentSystem.Core;
using System.Collections.Generic;

using Position = UnityEngine.Vector2Int;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public class CellStackComponentCache : ComponentCache
    {
        public LockState lockState;

        public void TryCache(Component component)
        {
            switch(component)
            {
                case LockState lockState:
                    this.lockState = lockState;
                    break;
            }
        }
    }

    // TODO: Define a container for attachments.
    public class CellStack : CacheableBasicEntity<CellStackComponentCache>
    {
        Position position;
        Stack<Cell> stack = new Stack<Cell>();
        List<CellAttachment> attachments = new List<CellAttachment>();

        TileStack currentTileStack;

        public CellStack(int xPos, int yPos) : base(new CellStackComponentCache())
        {
            position = new Position(xPos, yPos);
        }

        public void Push(Cell cell)
        {
            stack.Push(cell);
            cell.SetParent(this);
        }

        public Cell Top()
        {
            return stack.Peek();
        }

        public void Pop()
        {
            stack.Pop();
        }

        public Position Position()
        {
            return position;
        }

        public bool HasTileStack()
        {
            return currentTileStack != null;
        }

        public TileStack CurrentTileStack()
        {
            return currentTileStack;
        }

        public void SetCurrnetTileStack(TileStack stack)
        {
            this.currentTileStack = stack;
            stack.SetParent(this);
        }

        public void DetachTileStack()
        {
            this.currentTileStack = null;
        }

        public Stack<Cell> Stack()
        {
            return stack;
        }

        public void Attach(CellAttachment attachment)
        {
            attachment.SetOwner(this);
            attachments.Add(attachment);
        }

        public IEnumerable<CellAttachment> Attachments()
        {
            return attachments;
        }

        public bool HasAttachment<T>() where T : CellAttachment
        {
            for (int i = 0; i < attachments.Count; ++i)
                if (attachments[i] is T)
                    return true;
            return false;
        }

        public T GetAttachment<T>() where T : CellAttachment
        {
            for (int i = 0; i < attachments.Count; ++i)
                if (attachments[i] is T)
                    return (T)attachments[i];
            return default(T);
        }

        public void GetAttachments<T>(ref List<T> container) where T : CellAttachment
        {
            var count = attachments.Count;
            for (int i = 0; i < count; ++i)
                if (attachments[i] is T)
                    container.Add((T)attachments[i]);
        }

        public void RemoveAttachment(CellAttachment attachment) 
        {
            if (attachment.Owner() == this)
                attachment.SetOwner(null);

            attachments.Remove(attachment);
        }
    }
}