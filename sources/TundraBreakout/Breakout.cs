using System;
using TundraEngine;
using TundraEngine.Mathematics;
using TundraEngine.EntityComponent;

namespace Slayer
{
    public class Breakout
    {
        public void Initialize()
        {
            // Pad transform
            Transform2DComponent padTransform = new Transform2DComponent()
            {
                Position = Vector2.Zero,
                Rotation = new Angle(),
                Scale = Vector2.One
            };
            padTransform.GetBytes(out byte[] transformBytes);

            // Pad entity
            EntityResource padResource = new EntityResource()
            {
                NumEntities = 1,
                NumComponentTypes = 3,
                Components = new ComponentTypeData[]
                {
                    new ComponentTypeData()
                    {
                        Type = Transform2DComponent.Type,
                        Size = Transform2DComponent.Size,
                        NumInstances = 1,
                        EntityIndices = new uint[] { 0 },
                        Data = transformBytes
                    }
                }
            };
            
            // Spawn Pad
            EntityManager.Spawn(GetWorld(0), padResource);
        }
    }
}