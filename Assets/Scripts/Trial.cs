using DG.Tweening;
using UnityEngine;

public class Trial : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private int planetIndex = 0;
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool shot;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private PlanetGun planetGun;
    [SerializeField] private bool initialContact;
    [SerializeField] private float planetScale;
    [SerializeField] private int conversionNumber;

    private void Awake()
    {
        planetGun = GameObject.Find("PlanetGun").GetComponent<PlanetGun>();
        transform.DOScale(planetScale, 0.3f);
        conversionNumber = Random.Range(0, 101);
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        if (shot)
        {
            transform.Translate(new Vector3(0, -fallSpeed, 0) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit Ground ! !");
            // Since we hit the ground, we want to apply physics.
            circleCollider.isTrigger = false;
            shot = false;
            rb.gravityScale = 1;
        }
        else if (other.gameObject.CompareTag(gameObject.tag))
        {
            // The merge operation should be done here, which I still have not decided how to implement.
            Debug.Log("Hit the same type of object ! !");
            var otherConversion = other.transform.GetComponent<Trial>().GetConversionNumber();
            shot = false;
            var contactPoint = other.GetContact(0);
            if (conversionNumber >= otherConversion)
            {
                other.transform.GetComponent<CircleCollider2D>().enabled = false;
                circleCollider.enabled = false;
                rb.gravityScale = 0;
                other.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
                transform.DOMove(contactPoint.point, 1);
                other.transform.DOMove(contactPoint.point, 1).OnComplete(() =>
                {
                    // Both of them are initializing which is causing errors...
                    Debug.Log("creating");
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    // At this point some sort of particle must spawn.
                    Instantiate(planetPrefabs[planetIndex + 1], contactPoint.point, Quaternion.identity);
                });
            }
        }
    }

    public float GetScale()
    {
        return planetScale;
    }

    public int GetConversionNumber()
    {
        return conversionNumber;
    }

    public void Shoot()
    {
        shot = true;
    }
}