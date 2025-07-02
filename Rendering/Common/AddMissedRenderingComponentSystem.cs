using Unity.Burst;
using Unity.Entities;

namespace NSprites
{
        [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateBefore(typeof(SpriteRenderingSystem))]
    [WorldSystemFilter(WorldSystemFilterFlags.Default | WorldSystemFilterFlags.Editor | WorldSystemFilterFlags.EntitySceneOptimizations)]
    public partial class AddMissedRenderingComponentSystem : SystemBase
    {
        private EntityQuery _query;
        
        [BurstCompile]
        protected override void OnCreate() 
        {
            _query = SystemAPI.QueryBuilder()
                .WithAll<PropertyPointer>()
                .WithNoneChunkComponent<PropertyPointerChunk>()
                .WithOptions(EntityQueryOptions.IncludePrefab | EntityQueryOptions.IncludeDisabledEntities | EntityQueryOptions.Default | EntityQueryOptions.IgnoreComponentEnabledState)
                .Build();
            
            RequireForUpdate(_query);   
        }
        

        protected override void OnUpdate()
        {
            EntityManager.AddChunkComponentData(_query, new PropertyPointerChunk());
        }
    }
}
