using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie.Death
{
    public class ZombieDeath : MonoBehaviour
    {
        public Zombie zombie;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ball"))
            {
                zombie.Death();
                Destroy(other);
            }
        }

        
    }
}