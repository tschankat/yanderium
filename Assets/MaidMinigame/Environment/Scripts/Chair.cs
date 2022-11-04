using System.Collections;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
    public class Chair : MonoBehaviour
    {
        static Chairs chairs;
        public static Chairs AllChairs
        {
            get
            {
                if (chairs == null || chairs.Count == 0)
                {
                    chairs = new Chairs();
                    foreach (Chair ch in FindObjectsOfType<Chair>())
                        chairs.Add(ch);
                }

                return chairs;
            }
        }

        public static Chair RandomChair
        {
            get
            {
                Chairs availableChairs = new Chairs();
                foreach (Chair ch in AllChairs)
                    if (ch.available) availableChairs.Add(ch);
                if (availableChairs.Count > 0)
                {
                    int index = Random.Range(0, availableChairs.Count);
                    availableChairs[index].available = false;
                    return availableChairs[index];
                }
                else return null;
            }
        }

        private void OnDestroy()
        {
            chairs = null;
        }

        public bool available = true;
    }

    [System.Serializable]
    public class Chairs : ReorderableArray<Chair> { }
}