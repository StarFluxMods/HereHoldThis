using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace HereHoldThis.Systems
{
    public class AssignPlayersInteractive : GameSystemBase, IModSystem
    {
        private EntityQuery _players;
        protected override void Initialise()
        {
            base.Initialise();
            _players = GetEntityQuery(typeof(CPlayer));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp);
            foreach (Entity player in players)
            {
                if (!Has<CIsInteractive>(player))
                {
                    EntityManager.AddComponentData(player, new CIsInteractive
                    {
                        IsLowPriority = true
                    });
                }
            }
        }
    }
}