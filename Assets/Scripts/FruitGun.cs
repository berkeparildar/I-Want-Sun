using UnityEngine;

public class FruitGun : MonoBehaviour
{
    [SerializeField] private GameObject fruit;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] fruitPrefabs;
    
    private void Start()
    {
        var randomInt = Random.Range(0, fruitPrefabs.Length - 1);
        var initialFruit = Instantiate(fruitPrefabs[randomInt], transform.position, Quaternion.identity);
        initialFruit.transform.SetParent(transform);
        fruit = initialFruit;
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.x is <= 3 and >= -3)
        {
            transform.Translate(new Vector3(horizontalInput * speed, 0, 0) * Time.deltaTime);
        }
    }

    private void ShootFruit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fruit.transform.SetParent(null);
            fruit.GetComponent<Fruit>().Shoot();
        }
    }

    private void Update()
    {
        Movement();
        ShootFruit();
    }
}