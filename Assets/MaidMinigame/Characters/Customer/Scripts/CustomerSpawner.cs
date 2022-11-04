using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
    public class CustomerSpawner : MonoBehaviour
    {
        public GameObject[] customerPrefabs;
        float spawnRate = 10f;
        float spawnVariance = 5f;

        float timeTillSpawn;
        int spawnedCustomers;
        bool isPaused;

        private void Start()
        {
            spawnRate = GameController.Instance.activeDifficultyVariables.customerSpawnRate;
            spawnVariance = GameController.Instance.activeDifficultyVariables.customerSpawnVariance;
            isPaused = true;
        }

        private void OnEnable()
        {
            GameController.PauseGame += Pause;
        }

        private void OnDisable()
        {
            GameController.PauseGame -= Pause;
        }

        public void Pause(bool toPause)
        {
            isPaused = toPause;
        }

        private void Update()
        {
            if (isPaused) return;

            if (timeTillSpawn <= 0)
            {
                timeTillSpawn = spawnRate + Random.Range(-spawnVariance, spawnVariance);
                SpawnCustomer();
            }
            else
                timeTillSpawn -= Time.deltaTime;
        }

        void SpawnCustomer()
        {
            GameObject customer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Length)]);
            customer.transform.position = transform.position;
            AIController ai = customer.GetComponent<AIController>();
            ai.Init();
            ai.leaveTarget = transform;
        }

        public void OpenDoor()
        {
            transform.parent.GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }
}