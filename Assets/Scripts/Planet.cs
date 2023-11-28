using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private int planetIndex = 0;
    [SerializeField] private bool shot;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float planetScale;
    [SerializeField] private int conversionNumber;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject popUpText;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject planetContainer;
    [SerializeField] private GameObject popUpContainer;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioClip mergeSound;
    [SerializeField] private AudioClip groundSound;
    [SerializeField] private float numberOfPlays;
    [SerializeField] private bool hasMatched;

    private void Awake()
    {
        transform.DOScale(planetScale, 0.3f);
        conversionNumber = Random.Range(0, 101);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        planetContainer = GameObject.Find("PlanetContainer");
        popUpContainer = GameObject.Find("PopUpContainer");
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        if (shot)
        {
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            soundEffect.volume = 1;
            soundEffect.clip = mergeSound;
            soundEffect.Play();
            var contactPoint = other.GetContact(0);
            float angle = Mathf.Atan2(contactPoint.point.y - transform.position.y,
                contactPoint.point.x - transform.position.x) * Mathf.Rad2Deg;
            // Set the rotation based on the calculated angle
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rb.freezeRotation = true;
            circleCollider.enabled = false;
            rb.gravityScale = 0;
            // The merge operation should be done here, which I still have not decided how to implement.
            Debug.Log("Hit the same type of object ! !");
            var otherConversion = other.transform.GetComponent<Planet>().GetConversionNumber();
            shot = false;
            transform.DOPunchScale(new Vector3(0.2f, 0, 0), 0.2f, 5, 0.1f);
            // This equal is problematic, need to change for equal case
            while (other.transform.GetComponent<Planet>().GetConversionNumber() == conversionNumber)
            {
                conversionNumber = Random.Range(0, 100);
            }

            if (conversionNumber > otherConversion)
            {
                transform.DOMove(contactPoint.point, 0.3f);
                Debug.Log("creating");
                StartCoroutine(Die());
                // At this point some sort of particle must spawn.
                var nextPlanet = gameManager.GetPlanetAtIndex(planetIndex + 1);
                ShowScorePopUp(contactPoint.point, planetIndex + 1);
                Instantiate(particle, contactPoint.point, Quaternion.identity);
                var newPlanet = Instantiate(nextPlanet, contactPoint.point, Quaternion.identity);
                newPlanet.transform.SetParent(planetContainer.transform);
            }
            else if (conversionNumber < otherConversion)
            {
                transform.DOMove(contactPoint.point, 0.3f);
                StartCoroutine(Die());
            }
        }
        else
        {
            if (other.relativeVelocity.magnitude > 2)
            {
                soundEffect.clip = groundSound;
                soundEffect.volume -= 0.2f;
                soundEffect.Play();
            }
        }
    }

    public float GetScale()
    {
        return planetScale;
    }

    private int GetConversionNumber()
    {
        return conversionNumber;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        shot = true;
    }

    private void ShowScorePopUp(Vector3 pos, int score)
    {
        var textGameObject = Instantiate(popUpText, pos, Quaternion.identity);
        textGameObject.transform.SetParent(popUpContainer.transform);
        var text = textGameObject.GetComponent<TextMeshPro>();
        text.text = "+" + score;
        gameManager.IncreaseScore(score);
        text.transform.DOMoveY(1, 1).SetRelative();
        text.DOFade(0, 1).OnComplete(() => { Destroy(text.gameObject, 2); });
    }

    public void DestroyAfterGame()
    {
        ShowScorePopUp(transform.position, planetIndex + 1);
        Destroy(gameObject);
    }

    public bool IsMatched()
    {
        return hasMatched;
    }
}