using System;
using Entity.Lifts;
using UnityEngine;

namespace Spawner.Lifts
{
    public class BaseLiftSpawner : MonoBehaviour
    {
        public GameObject liftPrefab;
        public Transform liftOrigin;
        public Transform liftParent;

        public GameObject Spawn()
        {
            var lift = Instantiate(liftPrefab, liftOrigin.position, Quaternion.identity, liftParent);
            lift.tag = "Lift";
            return lift;
        }

        public virtual void Destroy()
        {
            for (var i = liftParent.childCount - 1; i >= 0; i--)
            {
                var child = liftParent.GetChild(i).gameObject;
                Destroy(child);
            }
        }


    }
}