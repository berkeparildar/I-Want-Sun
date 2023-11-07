using UnityEngine;

public class PlanetGun : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private bool reload;
    
    private void Start()
    {
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.x is <= 3 and >= -3)
        {
            transform.Translate(new Vector3(horizontalInput * speed, 0, 0) * Time.deltaTime);
        }
    }

    private void ShootPlanet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            planet.transform.SetParent(null);
            planet.GetComponent<Trial>().Shoot();
        }
    }

    public void ReloadGun()
    {
        if (!reload)
        {
            reload = true;
            var randomInt = Random.Range(0, planetPrefabs.Length - 1);
            var initialPlanet = Instantiate(planetPrefabs[randomInt], transform.position, Quaternion.identity);
            initialPlanet.transform.SetParent(transform);
            planet = initialPlanet;
        }
    }

    private void Update()
    {
        ReloadGun();
        Movement();
        ShootPlanet();
    }
}