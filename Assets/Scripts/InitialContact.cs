using UnityEngine;

public class InitialContact : MonoBehaviour
{
    [SerializeField] private bool initialContact;
    [SerializeField] private PlanetGun planetGun;

    private void Start()
    {
        planetGun = GameObject.Find("PlanetGun").GetComponent<PlanetGun>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (initialContact) return;
        initialContact = true;
        gameObject.layer = 6;
        planetGun.ReloadGun();
    }
}
