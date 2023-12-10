using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public partial struct CandySystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new CandyUpdateJob() { DeltaTime = SystemAPI.Time.DeltaTime };
        job.ScheduleParallel();
    }
}

partial struct CandyUpdateJob : IJobEntity
{
    public float DeltaTime;

    void Execute(in Candy candy, ref LocalTransform xform)
    {
        xform.Position.y -= DeltaTime * candy.Speed;

        if (xform.Position.y < -10f) xform.Position.y = 10f;
    }
}