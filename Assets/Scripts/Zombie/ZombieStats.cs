using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour {

    private int Health;
    public int Damage;

	// Use this for initialization
	void Start () {

        Health = 100;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        if(Health <= 0)
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("HurtPlayer");
        }

    }

}
