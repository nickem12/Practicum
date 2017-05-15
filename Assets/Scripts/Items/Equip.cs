using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour {

    private GameObject Slot;
    private GameObject t_Object;
    public GameObject t_Weapon;
    private GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        Slot = GameObject.FindGameObjectWithTag("Weapon_Slot");
        //EquipWeapon(t_Weapon);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipWeapon(GameObject Weapon)
    {
        if(Slot.transform.childCount > 0)
        {
            int counter = 0; 
            for(counter = 0; counter < Slot.transform.childCount; counter++)
            {
                Destroy(Slot.transform.GetChild(counter).gameObject);
            }
        }

        t_Object = Instantiate(Weapon);

        t_Object.transform.parent = Slot.transform;

        Debug.Log(t_Object.transform.position);

        t_Object.transform.position = new Vector3(.44f, -.38f, .76f);

        Debug.Log(t_Object.transform.position);
    }
}
