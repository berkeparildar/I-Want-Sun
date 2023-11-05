using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool shot;
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        
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
        if (rb.gravityScale == 0)
        {
            shot = false;
            rb.gravityScale = 1;
        }
    }

    public void Shoot()
    {
        shot = true;
    }
}