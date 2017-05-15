using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawnerBehavior : MonoBehaviour {

	public GameObject[] ZombiePrefabs;
	private GameObject[] Spawners;
	private GameObject Player;
	public int Preferred_Number_Of_Zombies;
	public bool DEBUG;

	private float SpawnTime = 0.0f;
	private float MaxSpawnTime = 10.0f;

	void Start () {
		Spawners = GameObject.FindGameObjectsWithTag("Spawner");
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void Update () {
		SpawnTime += Time.deltaTime;

		if(SpawnTime >= MaxSpawnTime){
			SpawnTime = 0.0f;

			if(DEBUG) Debug.Log("Ready to Spawn");

			GameObject[] Zombies = GameObject.FindGameObjectsWithTag("Zombie");
			if(Zombies.Length < Preferred_Number_Of_Zombies || Zombies == null){
				int Prefab;
				int Spawner;

				Prefab = (int)Random.Range(0, ZombiePrefabs.Length);
				Spawner = (int)Random.Range(0, Spawners.Length);

				if(DEBUG) Debug.Log("Spawning " + Prefab + " at " + Spawner);

				GameObject newZombie = Instantiate(ZombiePrefabs[Prefab]);
				newZombie.transform.position = Spawners[Spawner].transform.position;
			}
		}
	}


}
