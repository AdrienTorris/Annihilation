/*
	OpenDDL Library Software License
	==================================

	OpenDDL Library, version 1.1
	Copyright 2014-2015, Eric Lengyel
	All rights reserved.

	The OpenDDL Library is free software published on the following website:

		http://openddl.org/

	Redistribution and use in source and binary forms, with or without modification,
	are permitted provided that the following conditions are met:

	1. Redistributions of source code must retain the entire text of this license,
	comprising the above copyright notice, this list of conditions, and the following
	disclaimer.
	
	2. Redistributions of any modified source code files must contain a prominent
	notice immediately following this license stating that the contents have been
	modified from their original form.

	3. Redistributions in binary form must include attribution to the author in any
	listing of credits provided with the distribution. If there is no listing of
	credits, then attribution must be included in the documentation and/or other
	materials provided with the distribution. The attribution must be exactly the
	statement "This software contains the OpenDDL Library by Eric Lengyel" (without
	quotes) in the case that the distribution contains the original, unmodified
	OpenDDL Library, or it must be exactly the statement "This software contains a
	modified version of the OpenDDL Library by Eric Lengyel" (without quotes) in the
	case that the distribution contains a modified version of the OpenDDL Library.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
	ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
	WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
	IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
	INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
	NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
	PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
	WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
	ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
	POSSIBILITY OF SUCH DAMAGE.
*/

namespace Engine.Core
{
    public class Tree<T> : Tree where T : Tree
    {
        public T Previous => (T)PreviousNode;
        public T Next => (T)NextNode;
        public new T SuperNode => (T)base.SuperNode;
        public new T FirstSubnode => (T)base.FirstSubnode;
        public new T LastSubnode => (T)base.LastSubnode;
        public T Root => (T)GetRootNode();
        public T LeftmostNode => (T)GetLeftmostNode();
        public T RightmostNode => (T)GetRightmostNode();

        public bool IsSuccessor(Tree<T> node) => IsSucessor(node);
        public T GetNextNode(Tree<T> node) => (T)base.GetNextNode(node);
        public T GetPreviousNode(Tree<T> node) => (T)base.GetPreviousNode(node);
        public T GetPreviousLevelNode(Tree<T> node) => (T)base.GetPreviousLevelNode(node);

        public void AppendSubnode(T node) => base.AppendSubnode(node);
        public void PrependSubnode(T node) => base.PrependSubnode(node);
        public void InsertSubnodeBefore(T node, T before) => base.InsertSubnodeBefore(node, before);
        public void InsertSubnodeAfter(T node, T after) => base.InsertSubnodeAfter(node, after);
        public void RemoveSubnode(T node) => base.RemoveSubnode(node);
    }

    public abstract class Tree
    { 
        public Tree PreviousNode;
        public Tree NextNode;
        public Tree SuperNode;
        public Tree FirstSubnode;
        public Tree LastSubnode;

        protected Tree() { }

        protected virtual void Destroy()
        {
            PurgeSubtree();
            SuperNode?.RemoveSubnode(this);
        }
        
        public int GetSubnodeCount()
        {
            long count = 0;
            Tree subnode = FirstSubnode;
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
            Tree subnode = FirstSubnode;
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
            Tree element = this;
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
            Tree element = this;
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
            Tree subnode = FirstSubnode;
            while (subnode != null)
            {
                Tree next = subnode.NextNode;
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

        protected Tree GetRootNode()
        {
            Tree root = this;
            while (true)
            {
                Tree node = root.SuperNode;
                if (node == null)
                {
                    break;
                }
                root = node;
            }
            return root;
        }

        protected bool IsSucessor(Tree node)
        {
            Tree super = node.SuperNode;
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

        protected Tree GetLeftmostNode()
        {
            Tree node = this;
            while (true)
            {
                Tree subnode = node.FirstSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected Tree GetRightmostNode()
        {
            Tree node = this;
            while (true)
            {
                Tree subnode = node.LastSubnode;
                if (subnode == null)
                {
                    break;
                }
                node = subnode;
            }
            return node;
        }

        protected Tree GetNextNode(Tree node)
        {
            Tree next = node.FirstSubnode;
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

        protected Tree GetPreviousNode(Tree node)
        {
            if (node == this)
            {
                return null;
            }

            Tree prev = node.PreviousNode;
            if (prev == null)
            {
                return node.SuperNode;
            }
            return prev.GetRightmostNode();
        }

        protected Tree GetNextLevelNode(Tree node)
        {
            Tree next = null;
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

        protected Tree GetPreviousLevelNode(Tree node)
        {
            Tree prev = null;
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

        protected void AppendSubnode(Tree node)
        {
            Tree tree = node.SuperNode;
            if (tree != null)
            {
                Tree prev = node.PreviousNode;
                Tree next = node.NextNode;

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
            node.SuperNode = (this);
        }

        protected void PrependSubnode(Tree node)
        {
            Tree tree = node.SuperNode;
            if (tree != null)
            {
                Tree prev = node.PreviousNode;
                Tree next = node.NextNode;

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

            node.SuperNode = (this);
        }

        protected void InsertSubnodeBefore(Tree node, Tree before)
        {
            Tree tree = node.SuperNode;
            if (tree != null)
            {
                Tree prev = node.PreviousNode;
                Tree next = node.NextNode;

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

            node.SuperNode = (this);
            node.NextNode = before;

            if (before != null)
            {
                Tree after = before.PreviousNode;
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
                Tree after = LastSubnode;
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

        protected void InsertSubnodeAfter(Tree node, Tree after)
        {
            Tree tree = node.SuperNode;
            if (tree != null)
            {
                Tree prev = node.PreviousNode;
                Tree next = node.NextNode;

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

            node.SuperNode = (this);
            node.PreviousNode = after;

            if (after != null)
            {
                Tree before = after.NextNode;
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
                Tree before = FirstSubnode;
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

        protected void RemoveSubnode(Tree subnode)
        {
            Tree prev = subnode.PreviousNode;
            Tree next = subnode.NextNode;

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