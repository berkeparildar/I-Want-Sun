using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialContact : MonoBehaviour
{
    [SerializeField] private bool initialContact;
    [SerializeField] private PlanetGun planetGun;
    // Start is called before the first frame update
    void Start()
    {
        planetGun = GameObject.Find("PlanetGun").GetComponent<PlanetGun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!initialContact)
        {
            initialContact = true;
            planetGun.ReloadGun();
        }
    }
}
