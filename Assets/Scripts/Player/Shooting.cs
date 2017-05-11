﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public ItemData Weapon;

    // Use this for initialization
    void Start()
    {

        Weapon = GetComponent<Item>().itemData;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Weapon.currentAmmo > 0)
            {
                GameObject t_bullet;
                t_bullet = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                Rigidbody t_rigidbody;
                t_rigidbody = t_bullet.GetComponent<Rigidbody>();
                t_rigidbody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(t_bullet, 10f);

                Weapon.currentAmmo--;
            }
        }
    }
}
