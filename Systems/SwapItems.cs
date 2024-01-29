using Kitchen;
using KitchenMods;

namespace HereHoldThis.Systems
{
    public class SwapItems : ItemInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;
        
        protected override bool IsPossible(ref InteractionData data)
        {
            return data.Target != data.Interactor && Has<CPlayer>(data.Target) && Has<CItemHolder>(data.Target) && Has<CItemHolder>(data.Interactor);
        }

        protected override void Perform(ref InteractionData data)
        {
            Require(data.Target, out CItemHolder targetHolder);
            Require(data.Interactor, out CItemHolder interactorHolder);

            if (targetHolder.HeldItem != default)
            {
                Require(targetHolder.HeldItem, out CHeldBy targetHeldBy);
                targetHeldBy.Holder = data.Interactor;
                EntityManager.SetComponentData(targetHolder.HeldItem, targetHeldBy);
            }
            
            if (interactorHolder.HeldItem != default)
            {
                Require(interactorHolder.HeldItem, out CHeldBy interactorHeldBy);
                interactorHeldBy.Holder = data.Target;
                EntityManager.SetComponentData(interactorHolder.HeldItem, interactorHeldBy);
            }

            EntityManager.SetComponentData(data.Target, interactorHolder);
            EntityManager.SetComponentData(data.Interactor, targetHolder);
        }
    }
}