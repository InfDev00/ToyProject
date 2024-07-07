using System;
using Entities.EntitySubClass;
using UnityEngine;
using Utils;

namespace Entities
{
    public class InteractObject : Entity
    {
        
        
        private void Awake()
        {
            EntityStatus = new EntityStatus(initialHealth, initialDef);
            gameObject.tag = Tags.OBJECT;
        }

        public virtual void Interact(GameObject obj, string log = "")
        {
            Debug.Log($"Interact End with {log}");
            Destroy(gameObject);
        }

        protected override void OnHitEnemy(GameObject obj)
        {

        }

        protected override void OnHitPlayer(GameObject obj)
        {

        }

        protected override void OnHitObject(GameObject obj)
        {

        }
    }
}