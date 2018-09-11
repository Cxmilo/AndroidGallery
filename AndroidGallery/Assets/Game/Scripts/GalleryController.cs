using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GalleryController : MonoBehaviour
{
    public GameObject buttonPrefab;

    public Sprite[] gallerySprite;
    public Image Gallery;

    public Transform scrollContainer_1;
    public Transform scrollContainer_2;
    public Transform scrollContainer_3;

    // Use this for initialization
    private void Start()
    {
        int itemsPerContainer = gallerySprite.Length / 3;
        int lastColumItems = gallerySprite.Length % 3;

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, scrollContainer_1).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i];
            newButton.transform.DOScale(1f, 0.75f).SetLoops(-1, LoopType.Yoyo);
            newButton.GetComponentInParent<Button>().onClick.AddListener(
                delegate
                {
                    Gallery.sprite = newButton.sprite;
                    Gallery.transform.parent.gameObject.SetActive(true);
                });
        }

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, scrollContainer_3).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i + itemsPerContainer];
        }

        for (int i = 0; i < itemsPerContainer + lastColumItems; i++)
        {
            Image newButton = Instantiate(buttonPrefab, scrollContainer_2).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i + (itemsPerContainer * 2)];
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}