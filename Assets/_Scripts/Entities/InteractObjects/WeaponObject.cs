using UnityEngine;

namespace Entities.InteractObjects
{
    public class WeaponObject : InteractObject
    {
        [Header("Weapon Object")] public GameObject weaponPrefab;

        public override void Interact(GameObject obj, string log = "")
        {
            var player = obj.GetComponent<PlayerController>();
            
            player.AddWeapon(weaponPrefab);
            
            base.Interact(obj, "Weapon");
        }
    }
}