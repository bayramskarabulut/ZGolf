using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombie;

namespace Wave
{
    public enum ZombieType
    {
        Zombie1,
        Zombie2,
        Zombie3
    }

    [System.Serializable]
    public class WaveDate
    {
        public int WaveEntityNumber;
        public ZombieType ZombieTypeList;
    }

    public class Wave : MonoBehaviour
    {
        public GameObject TargetPoint;
        public float SpamTime = 0.6f;
        public float SpamWaveTime = 1.0f;
        public List<GameObject> ZombieTypePrefab;
        public List<GameObject> SpamPoint = new List<GameObject>();
        public List<WaveDate> WaveDateList = new List<WaveDate>();

        private int WayNumber = 0;
        private int SpamEntityNow = 0;
        private List<GameObject> SpamEntityList = new List<GameObject>();

        void Start()
        {
            StartCoroutine(Spam());
        }


        public IEnumerator Spam()
        {
            
            yield return new WaitForSeconds(SpamTime);
            if (WaveDateList[WayNumber].WaveEntityNumber > SpamEntityNow)
            {
                SpamEntityNow += 1;
                if (WaveDateList[WayNumber].ZombieTypeList == ZombieType.Zombie1)
                {
                    GameObject newZombie = Instantiate(ZombieTypePrefab[0], transform);
                    newZombie.transform.position = SpamPoint[UnityEngine.Random.Range(0, SpamPoint.Count)].transform.position;
                    if (newZombie.TryGetComponent<Zombie.Zombie>(out Zombie.Zombie zombie))
                    {
                        zombie.target = TargetPoint.transform;
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(SpamWaveTime);
                WayNumber += 1;
            }
            StartCoroutine(Spam());
        }
    }
    
    

    
}