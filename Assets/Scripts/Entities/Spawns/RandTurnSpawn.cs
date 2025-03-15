using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class RandTurnSpawn : BaseSpawn
    {
        public override void Spawn(Transform transform, Transform model)
        {
            model.rotation = Quaternion.Euler(0, (Random.value > 0.5f ? 0 : 180f), 0); 

            base.Spawn(transform, model);
        }
    }
}
