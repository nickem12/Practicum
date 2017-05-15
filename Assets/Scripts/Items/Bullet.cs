using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            other.GetComponent<ZombieStats>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
