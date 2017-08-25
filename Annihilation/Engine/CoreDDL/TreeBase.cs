namespace ODDL
{
    public abstract class TreeBase
    {
        public TreeBase PreviousNode;
        public TreeBase NextNode;
        public TreeBase SuperNode;
        public TreeBase FirstSubnode;
        public TreeBase LastSubnode;

        protected TreeBase() { }

        protected virtual void Destroy()
        {
            PurgeSubtree();
            SuperNode?.RemoveSubnode(this);
        }

        public int GetSubnodeCount()
        {
            long count = 0;
            TreeBase subnode = FirstSubnode;
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
            TreeBase subnode = FirstSubnode;
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
            TreeBase element = this;
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
            TreeBase element = this;
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
            TreeBase subnode = FirstSubnode;
            while (subnode != null)
            {
                TreeBase next = subnode.NextNode;
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

        protected TreeBase GetRootNode()
        {
            TreeBase root = this;
            while (true)
            {
                TreeBase node = root.SuperNode;
                if (node == null)
                {
                    break;
                }
                root = node;
            }
            return root;
        }

        protected bool Sucessor(TreeBase node)
        {
            TreeBase super = node.SuperNode;
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

        protected TreeBase GetLeftmostNode()
        {
            TreeBase node = this;
            while (true)
            {
                TreeBase subnode = node.FirstSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected TreeBase GetRightmostNode()
        {

            TreeBase node = this;
            while (true)
            {
                TreeBase subnode = node.LastSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected TreeBase GetNextNode(TreeBase node)
        {
            TreeBase next = node.FirstSubnode;
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

        protected TreeBase GetPreviousNode(TreeBase node)
        {
            if (node == this)
            {
                return null;
            }

            TreeBase prev = node.PreviousNode;
            if (prev == null)
            {
                return node.SuperNode;
            }
            return prev.GetRightmostNode();
        }

        protected TreeBase GetNextLevelNode(TreeBase node)
        {
            TreeBase next = null;
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

        protected TreeBase GetPreviousLevelNode(TreeBase node)
        {
            TreeBase prev = null;
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

        protected void AppendSubnode(TreeBase node)
        {
            TreeBase tree = node.SuperNode;
            if (tree != null)
            {
                TreeBase prev = node.PreviousNode;
                TreeBase next = node.NextNode;

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

        protected void PrependSubnode(TreeBase node)
        {
            TreeBase tree = node.SuperNode;
            if (tree != null)
            {
                TreeBase prev = node.PreviousNode;
                TreeBase next = node.NextNode;

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

        protected void InsertSubnodeBefore(TreeBase node, TreeBase before)
        {
            TreeBase tree = node.SuperNode;
            if (tree != null)
            {
                TreeBase prev = node.PreviousNode;
                TreeBase next = node.NextNode;

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
                TreeBase after = before.PreviousNode;
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
                TreeBase after = LastSubnode;
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

        protected void InsertSubnodeAfter(TreeBase node, TreeBase after)
        {
            TreeBase tree = node.SuperNode;
            if (tree != null)
            {
                TreeBase prev = node.PreviousNode;
                TreeBase next = node.NextNode;

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
                TreeBase before = after.NextNode;
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
                TreeBase before = FirstSubnode;
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

        protected void RemoveSubnode(TreeBase subnode)
        {
            TreeBase prev = subnode.PreviousNode;
            TreeBase next = subnode.NextNode;

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