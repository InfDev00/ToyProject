using UnityEngine;

namespace Entities.InteractObjects
{
    public class HealObject : InteractObject
    {
        [Header("Heal Object")]
        public float healAmount;

        public override void Interact(GameObject obj, string log = "")
        {
            var player = obj.GetComponent<PlayerController>();
            
            player.EntityStatus.Heal(healAmount);
            
            base.Interact(obj, "Heal");
        }
    }
}