using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GalleryController : MonoBehaviour
{
    public GameObject buttonPrefab;

    public Sprite[] gallerySprite;

    public RectTransform scrollContainer_1;
    public RectTransform scrollContainer_2;
    public RectTransform scrollContainer_3;

    // Use this for initialization
    private void Start()
    {
        var spawnPoints_1 = scrollContainer_1.GetComponentsInChildren<RectTransform>().Where(c => c != scrollContainer_1).OrderBy(a => Guid.NewGuid()).ToArray();
        var spawnPoints_2 = scrollContainer_2.GetComponentsInChildren<RectTransform>().Where(c => c != scrollContainer_2).OrderBy(a => Guid.NewGuid()).ToArray();
        var spawnPoints_3 = scrollContainer_3.GetComponentsInChildren<RectTransform>().Where(c => c != scrollContainer_3).OrderBy(a => Guid.NewGuid()).ToArray();

        int itemsPerContainer = gallerySprite.Length / 3;
        int lastColumItems = gallerySprite.Length % 3;

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_1[i].position,Quaternion.identity, scrollContainer_1).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i];
        }

        for (int i = 0; i < itemsPerContainer; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_3[i].position,Quaternion.identity, scrollContainer_3).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i + itemsPerContainer];
        }

        for (int i = 0; i < itemsPerContainer + lastColumItems; i++)
        {
            Image newButton = Instantiate(buttonPrefab, spawnPoints_2[i].position, Quaternion.identity, scrollContainer_2).GetComponentsInChildren<Image>()[1];
            newButton.sprite = gallerySprite[i + (itemsPerContainer * 2)];
        }
    }

    // Update is called once per frame
    private void Update()
    {

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