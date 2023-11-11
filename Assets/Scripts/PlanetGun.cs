using UnityEngine;

public class PlanetGun : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] planetPrefabs;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        ReloadGun();
    }

    private void ShootPlanet()
    {
        if (Input.GetMouseButton(0) && transform.childCount != 0)
        {
            var currentPos = transform.position;
            var touchPosition = Input.mousePosition;
            var touchOnWorld = mainCamera.ScreenToWorldPoint(touchPosition);
            if (touchOnWorld.y >= 4)
            {
                return;
            }
            if (touchOnWorld.x > 2.75f - (planet.GetComponent<Planet>().GetScale() / 2))
            {
                touchOnWorld.x = 2.75f - (planet.GetComponent<Planet>().GetScale() / 2);
            }
            else if (touchOnWorld.x < -2.75f + (planet.GetComponent<Planet>().GetScale() / 2))
            {
                touchOnWorld.x = -2.75f + (planet.GetComponent<Planet>().GetScale() / 2);
            }
            transform.position = new Vector3(touchOnWorld.x, currentPos.y, 0);
            planet.transform.SetParent(null);
            planet.GetComponent<Planet>().Shoot();
        }
    }

    public void ReloadGun()
    {
        var randomInt = Random.Range(0, 60);
        int planetIndex = 0;
        if (randomInt < 20)
        {
            planetIndex = 0;
        }
        else if (randomInt is >= 20 and < 36)
        {
            planetIndex = 1;
        }
        else if (randomInt is >= 36 and < 48)
        {
            planetIndex = 2;
        }
        else if (randomInt is >= 48 and < 56)
        {
            planetIndex = 3;
        }
        else
        {
            planetIndex = 4;
        }
        var initialPlanet = Instantiate(planetPrefabs[planetIndex], transform.position, Quaternion.identity);
        initialPlanet.transform.SetParent(transform);
        planet = initialPlanet;
        Debug.Log(2.85f - (planet.GetComponent<Planet>().GetScale() / 2));
    }

    private void Update()
    {
        ShootPlanet();
    }
    
    
}