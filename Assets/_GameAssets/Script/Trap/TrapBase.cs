using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zombie;

namespace Trap
{
    public class TrapBase : MonoBehaviour
    {
        public bool OneUse = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                Zombie.Zombie zombie = other.GetComponentInParent<Zombie.Zombie>();
                zombie.Death();
                if (OneUse)
                    Destroy(gameObject);
            }
        }
    }
}