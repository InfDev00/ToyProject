using System;
using Entities.EntitySubClass;
using UnityEngine;
using Utils;

namespace Entities
{
    public abstract class InteractObject : Entity
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
    }
}