using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    idle,
    Opened
}

public class ButtonController : MonoBehaviour
{
    public float floatStrength;
    public Transform zoomOutParent;

    private Vector2 floatY;
    private float originalX;
    private float originalY;

    public ButtonState currentState;
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
        floatStrength = UnityEngine.Random.Range(0.1f, 1f) * floatStrength;
        this.originalY = this.transform.position.y;
        this.originalX = this.transform.position.x;

        GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    private void OnClick()
    {
        switch (currentState)
        {
            case ButtonState.idle:
                ZoomIn();
                break;

            case ButtonState.Opened:
                ZoomOut();
                break;

            default:
                break;
        }
    }

    private void ZoomIn()
    {
        currentState = ButtonState.Opened;
        Vector3 newPos = zoomOutParent.position;
        Vector3 scale = zoomOutParent.localScale;
        GetComponent<RectTransform>().DOMove(newPos, 0.5f);
        GetComponent<RectTransform>().DOScale(scale, 0.5f).OnComplete(delegate { GetComponent<Button>().enabled = true; });
        GetComponent<Button>().enabled = false;
        transform.SetAsLastSibling();
        GalleryController.instance.ZoomOutCollumElements(parent,this);
        
    }

    public void ZoomOut()
    {
        
        GetComponent<Button>().enabled = false;
        Vector3 newPos = new Vector3(originalX, originalY, transform.position.z);
        GetComponent<RectTransform>().DOMove(newPos, 0.5f);
        GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).OnComplete(delegate { GetComponent<Button>().enabled = true;
            currentState = ButtonState.idle;
           
        });
    }

    private void Update()
    {
        switch (currentState)
        {
            case ButtonState.idle:
                transform.position = new Vector2(originalX,
                    originalY + ((float)Math.Sin(Time.time) * floatStrength));
                break;

            case ButtonState.Opened:
                break;

            default:
                break;
        }
    }
}