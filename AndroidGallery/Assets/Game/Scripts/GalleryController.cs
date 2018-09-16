using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GalleryController : MonoBehaviour
{
    public static GalleryController instance;

    public GameObject buttonPrefab;

    public Sprite[] gallerySprite;
    public Sprite[] footerSprites;

    public RectTransform fullIcon_1;
    public RectTransform fullIcon_2;
    public RectTransform fullIcon_3;

    public RectTransform scrollContainer_1;
    public RectTransform scrollContainer_2;
    public RectTransform scrollContainer_3;


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        var spawnPoints_1 = scrollContainer_1.GetComponentsInChildren<RectTransform>().Where(s => s != scrollContainer_1).ToArray();
        var spawnPoints_2 = scrollContainer_2.GetComponentsInChildren<RectTransform>().Where(s => s != scrollContainer_2).ToArray();
        var spawnPoints_3 = scrollContainer_3.GetComponentsInChildren<RectTransform>().Where(s => s != scrollContainer_3).ToArray();

        int itemsPerContainer = gallerySprite.Length / 3;
        int lastColumItems = gallerySprite.Length % 3;

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_1[i].position, Quaternion.identity, scrollContainer_1).GetComponent<Image>();
            newButton.sprite = gallerySprite[i];
            newButton.GetComponentsInChildren<Image>()[1].sprite = footerSprites[i];
            newButton.GetComponent<ButtonController>().zoomOutParent = fullIcon_1;
            newButton.SetNativeSize();
            newButton.GetComponentsInChildren<Image>()[1].SetNativeSize();
        }

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_3[i].position, Quaternion.identity, scrollContainer_3).GetComponent<Image>();
            newButton.sprite = gallerySprite[i +( itemsPerContainer + lastColumItems + 3)];
            newButton.GetComponentsInChildren<Image>()[1].sprite = footerSprites[i + (itemsPerContainer + lastColumItems + 3)];
            newButton.GetComponent<ButtonController>().zoomOutParent = fullIcon_3;
            newButton.SetNativeSize();
            newButton.GetComponentsInChildren<Image>()[1].SetNativeSize();
        }

        for (int i = 0; i < itemsPerContainer + lastColumItems; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_2[i].position, Quaternion.identity, scrollContainer_2).GetComponent<Image>();
            newButton.sprite = gallerySprite[i + itemsPerContainer];
            newButton.GetComponentsInChildren<Image>()[1].sprite = footerSprites[i + itemsPerContainer];
            newButton.GetComponent<ButtonController>().zoomOutParent = fullIcon_2;
            newButton.SetNativeSize();
            newButton.GetComponentsInChildren<Image>()[1].SetNativeSize();
        }
    }

    public void ZoomOutCollumElements(Transform colum, ButtonController newBtn)
    {
        ButtonController zoomIned = colum.GetComponentsInChildren<ButtonController>().
                                    Where(b => b.currentState == ButtonState.Opened && b != newBtn).FirstOrDefault();

        if(zoomIned)
        {
            zoomIned.ZoomOut();
        }

    }


}

public static class CameraExtensions
{
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}