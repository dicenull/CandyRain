using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CandySpawnSystem : ISystem
{
  public void OnCreate(ref SystemState state)
    => state.RequireForUpdate<Config>();

  [BurstCompile]
  public void OnUpdate(ref SystemState state)
  {
    var config = SystemAPI.GetSingleton<Config>();

    var instances = state.EntityManager.Instantiate
      (config.Prefab, config.SpawnCount, Allocator.Temp);
    var rand = new Random(100);

    foreach (var entity in instances)
    {
      var xform = SystemAPI.GetComponentRW<LocalTransform>(entity);

      var v = rand.NextFloat3(-1, 1);
      var size = rand.NextFloat(.3f, .5f);
      xform.ValueRW = LocalTransform
        .FromPosition(math.float3(v.x, v.y, v.z) * config.SpawnRadius)
        .WithScale(size);
    }

    state.Enabled = false;
  }
}