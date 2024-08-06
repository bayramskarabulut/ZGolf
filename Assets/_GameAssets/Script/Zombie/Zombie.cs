using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class Zombie : MonoBehaviour
    {
        public Transform target;
        public Animator animator;
        public NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent.SetDestination(target.position + Vector3.right * Random.Range(-20 , 20));
        }

        public void Death()
        {
            navMeshAgent.enabled = false;
            animator.enabled = false;
        }
    }
}