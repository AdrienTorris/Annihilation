using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine.EntityComponent
{
    public struct TransformIndex : IEquatable<TransformIndex>
    {
        private int _value;

        public static readonly TransformIndex Invalid = -1;

        public bool Equals(TransformIndex other) => _value == other._value;
        public override bool Equals(object obj) => obj is TransformIndex && this == (TransformIndex)obj;
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();

        public static bool operator ==(TransformIndex left, TransformIndex right) => left.Equals(right);
        public static bool operator !=(TransformIndex left, TransformIndex right) => !left.Equals(right);

        public static implicit operator int(TransformIndex transform) => transform._value;
        public static implicit operator TransformIndex(int value) => new TransformIndex { _value = value };
    }

    public unsafe struct Transforms
    {
        public static readonly Transforms Null = default(Transforms);

        public int Count;
        public int Capacity;
        public byte* Buffer;

        public Entity* Entity;
        public Matrix4x4* LocalMatrix;
        public Matrix4x4* WorldMatrix;
    }

    public unsafe class TransformManager
    {
        private Transforms _transforms = Transforms.Null;
        private Dictionary<Entity, TransformIndex> _map;
        
        public void Allocate(int capacity)
        {
            Assert.IsTrue(capacity > _transforms.Count);

            int byteCount = capacity * Unsafe.SizeOf<Entity>() +
                            capacity * Unsafe.SizeOf<Matrix4x4>() +
                            capacity + Unsafe.SizeOf<Matrix4x4>();

            Transforms newTransforms = new Transforms
            {
                Count = _transforms.Count,
                Capacity = capacity,
                Buffer = (byte*)Marshal.AllocHGlobal(byteCount)
            };

            newTransforms.Entity = (Entity*)newTransforms.Buffer;
            newTransforms.LocalMatrix = (Matrix4x4*)newTransforms.Buffer + capacity;
            newTransforms.WorldMatrix = (Matrix4x4*)newTransforms.Buffer + capacity * 2;

            Memory.Copy(newTransforms.Entity, _transforms.Entity, _transforms.Count * Unsafe.SizeOf<Entity>());
            Memory.Copy(newTransforms.LocalMatrix, _transforms.LocalMatrix, _transforms.Count * Unsafe.SizeOf<Matrix4x4>());
            Memory.Copy(newTransforms.WorldMatrix, _transforms.WorldMatrix, _transforms.Count * Unsafe.SizeOf<Matrix4x4>());

            Memory.Free(_transforms.Buffer);

            _transforms = newTransforms;

            _map = new Dictionary<Entity, TransformIndex>(capacity);
        }

        public TransformIndex Add(Entity entity, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;
            matrix *= Matrix4x4.CreateTranslation(position);
            matrix = Matrix4x4.Transform(matrix, rotation);
            matrix *= Matrix4x4.CreateScale(scale);

            return Add(entity, matrix);
        }

        public TransformIndex Add(Entity entity, Matrix4x4 matrix)
        {
            Assert.IsFalse(_map.ContainsKey(entity), $"Entity {entity} already has a transform.");
            
            if (_transforms.Count == _transforms.Capacity)
            {
                Allocate(_transforms.Capacity * 2 + 1);
            }

            int last = _transforms.Count;

            _transforms.Entity[last] = entity;
            _transforms.WorldMatrix[last] = matrix;
            _transforms.LocalMatrix[last] = matrix;

            ++_transforms.Count;

            _map.Add(entity, last);

            return last;
        }

        public void Remove(TransformIndex index)
        {
            Assert.IsTrue(index < _transforms.Count);

            int last = _transforms.Count - 1;
            Entity entity = _transforms.Entity[index];
            Entity lastEntity = _transforms.Entity[last];

            _transforms.Entity[index] = _transforms.Entity[last];
            _transforms.WorldMatrix[index] = _transforms.WorldMatrix[last];
            _transforms.LocalMatrix[index] = _transforms.LocalMatrix[last];

            _map[lastEntity] = index;
            _map.Remove(entity);

            --_transforms.Count;
        }

        public bool Has(Entity entity)
        {
            return _map.ContainsKey(entity);
        }

        public void SetLocalPosition(TransformIndex index, Vector3 position)
        {
            Assert.IsTrue(index < _transforms.Count);
            _transforms.LocalMatrix[index].Translation = position;
        }

        public void SetLocalRotation(TransformIndex index, Quaternion rotation)
        {
            Assert.IsTrue(index < _transforms.Count);
            _transforms.LocalMatrix[index] = Matrix4x4.Transform(_transforms.LocalMatrix[index], rotation);
        }
    }
}