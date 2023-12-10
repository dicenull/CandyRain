

using Unity.Entities;
using UnityEngine;

struct Candy : IComponentData { public float Speed; }

class CandyAuthoring : MonoBehaviour
{
    public float Speed = 3f;

    class Baker : Baker<CandyAuthoring>
    {
        public override void Bake(CandyAuthoring src)
        {
            var data = new Candy { Speed = src.Speed };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}