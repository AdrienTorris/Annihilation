using System.Collections;
using System.Collections.Generic;

namespace Engine.Core
{
    public sealed class MapElement<TKey, TValue>
    {
        public MapElement<TKey, TValue> Parent;
        public MapElement<TKey, TValue> Left;
        public MapElement<TKey, TValue> Right;
        public TKey Key;
        public TValue Value;
        public int Balance;
    }

    public sealed class MapElementEnumerator<TKey, TValue> : IEnumerator<MapElement<TKey, TValue>>
    {
        enum Action
        {
            Parent,
            Right,
            End
        }

        private Action _action;
        private MapElement<TKey, TValue> _root;
        private MapElement<TKey, TValue> _right;

        public MapElement<TKey, TValue> Current { get; private set; }

        object IEnumerator.Current => Current;

        public MapElementEnumerator(MapElement<TKey, TValue> root)
        {
            _right = _root = root;
            _action = _root == null ? Action.End : Action.Right;
        }

        public bool MoveNext()
        {
            switch (_action)
            {
                case Action.Right:
                    Current = _right;

                    while (Current.Left != null)
                    {
                        Current = Current.Left;
                    }

                    _right = Current.Right;
                    _action = _right != null ? Action.Right : Action.Parent;

                    return true;

                case Action.Parent:
                    while (Current.Parent != null)
                    {
                        MapElement<TKey, TValue> previous = Current;

                        Current = Current.Parent;

                        if (Current.Left == previous)
                        {
                            _right = Current.Right;
                            _action = _right != null ? Action.Right : Action.Parent;

                            return true;
                        }
                    }

                    _action = Action.End;

                    return false;

                default:
                    return false;
            }
        }

        public void Reset()
        {
            _right = _root;
            _action = _root == null ? Action.End : Action.Right;
        }

        public void Dispose() { }
    }

    public class Map<TKey, TValue> : IEnumerable<MapElement<TKey, TValue>>
    {
        private IComparer<TKey> _comparer;

        public MapElement<TKey, TValue> Root { get; private set; }

        public Map(IComparer<TKey> comparer)
        {
            _comparer = comparer;
        }

        public Map() : this(Comparer<TKey>.Default) { }

        public IEnumerator<MapElement<TKey, TValue>> GetEnumerator()
        {
            return new MapElementEnumerator<TKey, TValue>(Root);
        }

        public bool Search(TKey key, out TValue value)
        {
            MapElement<TKey, TValue> node = Root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;

                    return true;
                }
            }

            value = default(TValue);

            return false;
        }

        public bool Insert(TKey key, TValue value)
        {
            MapElement<TKey, TValue> node = Root;

            while (node != null)
            {
                int compare = _comparer.Compare(key, node.Key);

                if (compare < 0)
                {
                    MapElement<TKey, TValue> left = node.Left;

                    if (left == null)
                    {
                        node.Left = new MapElement<TKey, TValue> { Key = key, Value = value, Parent = node };

                        InsertBalance(node, 1);

                        return true;
                    }
                    else
                    {
                        node = left;
                    }
                }
                else if (compare > 0)
                {
                    MapElement<TKey, TValue> right = node.Right;

                    if (right == null)
                    {
                        node.Right = new MapElement<TKey, TValue> { Key = key, Value = value, Parent = node };

                        InsertBalance(node, -1);

                        return true;
                    }
                    else
                    {
                        node = right;
                    }
                }
                else
                {
                    node.Value = value;

                    return false;
                }
            }

            Root = new MapElement<TKey, TValue> { Key = key, Value = value };

            return true;
        }

        private void InsertBalance(MapElement<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }

                    return;
                }

                MapElement<TKey, TValue> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        private MapElement<TKey, TValue> RotateLeft(MapElement<TKey, TValue> node)
        {
            MapElement<TKey, TValue> right = node.Right;
            MapElement<TKey, TValue> rightLeft = right.Left;
            MapElement<TKey, TValue> parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == Root)
            {
                Root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private MapElement<TKey, TValue> RotateRight(MapElement<TKey, TValue> node)
        {
            MapElement<TKey, TValue> left = node.Left;
            MapElement<TKey, TValue> leftRight = left.Right;
            MapElement<TKey, TValue> parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if (leftRight != null)
            {
                leftRight.Parent = node;
            }

            if (node == Root)
            {
                Root = left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;
        }

        private MapElement<TKey, TValue> RotateLeftRight(MapElement<TKey, TValue> node)
        {
            MapElement<TKey, TValue> left = node.Left;
            MapElement<TKey, TValue> leftRight = left.Right;
            MapElement<TKey, TValue> parent = node.Parent;
            MapElement<TKey, TValue> leftRightRight = leftRight.Right;
            MapElement<TKey, TValue> leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }

            if (node == Root)
            {
                Root = leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;
        }

        private MapElement<TKey, TValue> RotateRightLeft(MapElement<TKey, TValue> node)
        {
            MapElement<TKey, TValue> right = node.Right;
            MapElement<TKey, TValue> rightLeft = right.Left;
            MapElement<TKey, TValue> parent = node.Parent;
            MapElement<TKey, TValue> rightLeftLeft = rightLeft.Left;
            MapElement<TKey, TValue> rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if (node == Root)
            {
                Root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        public bool Delete(TKey key)
        {
            MapElement<TKey, TValue> node = Root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    MapElement<TKey, TValue> left = node.Left;
                    MapElement<TKey, TValue> right = node.Right;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == Root)
                            {
                                Root = null;
                            }
                            else
                            {
                                MapElement<TKey, TValue> parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        MapElement<TKey, TValue> successor = right;

                        if (successor.Left == null)
                        {
                            MapElement<TKey, TValue> parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            left.Parent = successor;

                            if (node == Root)
                            {
                                Root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            MapElement<TKey, TValue> parent = node.Parent;
                            MapElement<TKey, TValue> successorParent = successor.Parent;
                            MapElement<TKey, TValue> successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;
                            left.Parent = successor;

                            if (node == Root)
                            {
                                Root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successorParent, -1);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private void DeleteBalance(MapElement<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                MapElement<TKey, TValue> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private static void Replace(MapElement<TKey, TValue> target, MapElement<TKey, TValue> source)
        {
            MapElement<TKey, TValue> left = source.Left;
            MapElement<TKey, TValue> right = source.Right;

            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}