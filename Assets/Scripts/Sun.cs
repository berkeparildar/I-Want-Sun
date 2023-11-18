using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sunImage;
    [SerializeField] private Image sunUI;

    private void Start()
    {
        sunUI = GameObject.Find("SunUI").GetComponent<Image>();
        transform.DOScale(15, 1.5f).OnComplete(() =>
        {
            transform.DOScale(0.5f, 1.5f);
        });
        transform.DOMove(sunUI.transform.position, 1.5f).SetEase(Ease.InQuad);
        sunImage.DOFade(0.5f, 3f);
        Destroy(gameObject, 6);
    }
}
