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

    private void Awake()
    {
        planetGun = GameObject.Find("PlanetGun").GetComponent<PlanetGun>();
        transform.DOScale(planetScale, 0.3f);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialContact)
        {
            initialContact = true;
            planetGun.ReloadGun();
        }
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Hit Ground ! !");
            // Since we hit the ground, we want to apply physics.
            circleCollider.isTrigger = false;
            shot = false;
            rb.gravityScale = 1;
        }
        else if (other.CompareTag(gameObject.tag))
        {
            Debug.Log("Hit the same type of object ! !");
            // The merge operation should be done here, which I still have not decided how to implement.
            shot = false;
            var contactPoint = other.ClosestPoint(transform.position);
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<CircleCollider2D>().enabled = false;
            other.transform.DOMove(contactPoint, 1);
            transform.DOMove(contactPoint, 1).OnComplete(() =>
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                // Both of them are initializing which is causing errors...
                Instantiate(planetPrefabs[planetIndex + 1], contactPoint, Quaternion.identity);
            });
            // At this point some sort of particle must spawn.
        }
        
    }

    public float GetScale()
    {
        return planetScale;
    }

    public void Shoot()
    {
        shot = true;
    }
}
