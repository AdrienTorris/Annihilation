namespace ODDL
{
    public enum StructureType
    {
        Structure,
        DataList,
        DataArrayList
    }

    public class Structure
    {
        public Structure PreviousNode;
        public Structure NextNode;
        public Structure SuperNode;
        public Structure FirstSubnode;
        public Structure LastSubnode;

        protected Structure() { }

        protected virtual void Destroy()
        {
            PurgeSubtree();
            SuperNode?.RemoveSubnode(this);
        }

        public int GetSubnodeCount()
        {
            long count = 0;
            Structure subnode = FirstSubnode;
            while (subnode != null)
            {
                count++;
                subnode = subnode.NextNode;
            }
            return (int)count;
        }

        public int GetSubtreeNodeCount()
        {
            long count = 0;
            Structure subnode = FirstSubnode;
            while (subnode != null)
            {
                count++;
                subnode = GetNextNode(subnode);
            }
            return (int)count;
        }

        public int GetNodeIndex()
        {
            long index = 0;
            Structure element = this;
            while (true)
            {
                element = element.PreviousNode;
                if (element == null)
                {
                    break;
                }
                index++;
            }
            return (int)index;
        }

        public int GetNodeDepth()
        {
            long depth = 0;
            Structure element = this;
            while (true)
            {
                element = element.SuperNode;
                if (element == null)
                {
                    break;
                }
                depth++;
            }
            return (int)depth;
        }

        public void RemoveSubtree()
        {
            Structure subnode = FirstSubnode;
            while (subnode != null)
            {
                Structure next = subnode.NextNode;
                subnode.PreviousNode = null;
                subnode.NextNode = null;
                subnode.SuperNode = null;
                subnode = next;
            }
            FirstSubnode = null;
            LastSubnode = null;
        }

        public void PurgeSubtree()
        {
            while (FirstSubnode != null)
            {
                FirstSubnode = null;
            }
        }

        public virtual void Detach()
        {
            if (SuperNode != null)
            {
                SuperNode.RemoveSubnode(this);
            }
        }

        protected Structure GetRootNode()
        {
            Structure root = this;
            while (true)
            {
                Structure node = root.SuperNode;
                if (node == null)
                {
                    break;
                }
                root = node;
            }
            return root;
        }

        protected bool Sucessor(Structure node)
        {
            Structure super = node.SuperNode;
            while (super != null)
            {
                if (super == this)
                {
                    return true;
                }
                super = super.SuperNode;
            }
            return false;
        }

        protected Structure GetLeftmostNode()
        {
            Structure node = this;
            while (true)
            {
                Structure subnode = node.FirstSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected Structure GetRightmostNode()
        {

            Structure node = this;
            while (true)
            {
                Structure subnode = node.LastSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected Structure GetNextNode(Structure node)
        {
            Structure next = node.FirstSubnode;
            if (next == null)
            {
                while (true)
                {
                    if (node == this)
                    {
                        break;
                    }

                    next = node.NextNode;
                    if (next != null)
                    {
                        break;
                    }
                    node = node.SuperNode;
                }
            }
            return next;
        }

        protected Structure GetPreviousNode(Structure node)
        {
            if (node == this)
            {
                return null;
            }

            Structure prev = node.PreviousNode;
            if (prev == null)
            {
                return node.SuperNode;
            }
            return prev.GetRightmostNode();
        }

        protected Structure GetNextLevelNode(Structure node)
        {
            Structure next = null;
            while (true)
            {
                if (node == this)
                {
                    break;
                }
                next = node.NextNode;
                if (next != null)
                {
                    break;
                }
                node = node.SuperNode;
            }
            return next;
        }

        protected Structure GetPreviousLevelNode(Structure node)
        {
            Structure prev = null;
            while (true)
            {
                if (node == this)
                {
                    break;
                }
                prev = node.PreviousNode;
                if (prev != null)
                {
                    break;
                }
                node = node.SuperNode;
            }
            return prev;
        }

        protected void AppendSubnode(Structure node)
        {
            Structure tree = node.SuperNode;
            if (tree != null)
            {
                Structure prev = node.PreviousNode;
                Structure next = node.NextNode;

                if (prev != null)
                {
                    prev.NextNode = next;
                    node.PreviousNode = null;
                }

                if (next != null)
                {
                    next.PreviousNode = prev;
                    node.NextNode = null;
                }

                if (tree.FirstSubnode == node)
                {
                    tree.FirstSubnode = next;
                }

                if (tree.LastSubnode == node)
                {
                    tree.LastSubnode = prev;
                }
            }

            if (LastSubnode != null)
            {
                LastSubnode.NextNode = node;
                node.PreviousNode = LastSubnode;
                LastSubnode = node;
            }
            else
            {
                FirstSubnode = node;
                LastSubnode = node;
            }
            node.SuperNode = this;
        }

        protected void PrependSubnode(Structure node)
        {
            Structure tree = node.SuperNode;
            if (tree != null)
            {
                Structure prev = node.PreviousNode;
                Structure next = node.NextNode;

                if (prev != null)
                {
                    prev.NextNode = next;
                    node.PreviousNode = null;
                }

                if (next != null)
                {
                    next.PreviousNode = prev;
                    node.NextNode = null;
                }

                if (tree.FirstSubnode == node)
                {
                    tree.FirstSubnode = next;
                }

                if (tree.LastSubnode == node)
                {
                    tree.LastSubnode = prev;
                }
            }

            if (FirstSubnode != null)
            {
                FirstSubnode.PreviousNode = node;
                node.NextNode = FirstSubnode;
                FirstSubnode = node;
            }
            else
            {
                FirstSubnode = node;
                LastSubnode = node;
            }

            node.SuperNode = this;
        }

        protected void InsertSubnodeBefore(Structure node, Structure before)
        {
            Structure tree = node.SuperNode;
            if (tree != null)
            {
                Structure prev = node.PreviousNode;
                Structure next = node.NextNode;

                if (prev != null)
                {
                    prev.NextNode = next;
                }

                if (next != null)
                {
                    next.PreviousNode = prev;
                }

                if (tree.FirstSubnode == node)
                {
                    tree.FirstSubnode = next;
                }

                if (tree.LastSubnode == node)
                {
                    tree.LastSubnode = prev;
                }
            }

            node.SuperNode = this;
            node.NextNode = before;

            if (before != null)
            {
                Structure after = before.PreviousNode;
                node.PreviousNode = after;
                before.PreviousNode = node;

                if (after != null)
                {
                    after.NextNode = node;
                }
                else
                {
                    FirstSubnode = node;
                }
            }
            else
            {
                Structure after = LastSubnode;
                node.PreviousNode = after;

                if (after != null)
                {
                    after.NextNode = node;
                    LastSubnode = node;
                }
                else
                {
                    FirstSubnode = node;
                    LastSubnode = node;
                }
            }
        }

        protected void InsertSubnodeAfter(Structure node, Structure after)
        {
            Structure tree = node.SuperNode;
            if (tree != null)
            {
                Structure prev = node.PreviousNode;
                Structure next = node.NextNode;

                if (prev != null)
                {
                    prev.NextNode = next;
                }

                if (next != null)
                {
                    next.PreviousNode = prev;
                }

                if (tree.FirstSubnode == node)
                {
                    tree.FirstSubnode = next;
                }

                if (tree.LastSubnode == node)
                {
                    tree.LastSubnode = prev;
                }
            }

            node.SuperNode = this;
            node.PreviousNode = after;

            if (after != null)
            {
                Structure before = after.NextNode;
                node.NextNode = before;
                after.NextNode = node;

                if (before != null)
                {
                    before.PreviousNode = node;
                }
                else
                {
                    LastSubnode = node;
                }
            }
            else
            {
                Structure before = FirstSubnode;
                node.NextNode = before;

                if (before != null)
                {
                    before.PreviousNode = node;
                    FirstSubnode = node;
                }
                else
                {
                    FirstSubnode = node;
                    LastSubnode = node;
                }
            }
        }

        protected void RemoveSubnode(Structure subnode)
        {
            Structure prev = subnode.PreviousNode;
            Structure next = subnode.NextNode;

            if (prev != null)
            {
                prev.NextNode = next;
            }

            if (next != null)
            {
                next.PreviousNode = prev;
            }

            if (FirstSubnode == subnode)
            {
                FirstSubnode = next;
            }

            if (LastSubnode == subnode)
            {
                LastSubnode = prev;
            }

            subnode.PreviousNode = null;
            subnode.NextNode = null;
            subnode.SuperNode = null;
        }
    }
}