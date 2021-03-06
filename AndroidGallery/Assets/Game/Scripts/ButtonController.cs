﻿using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
    private Transform footer;

    private void Start()
    {
        footer = transform.GetChild(0);
        parent = transform.parent;
        floatStrength = UnityEngine.Random.Range(0.5f, 1f) * floatStrength;
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
        GetComponent<RectTransform>().DOScale(scale, 0.5f).OnComplete(delegate { GetComponent<Button>().enabled = true; footer.DOScaleY(1, 0.5f); });
        
        GetComponent<Button>().enabled = false;
        transform.SetAsLastSibling();
        transform.parent.SetAsLastSibling();
        GalleryController.instance.ZoomOutCollumElements(parent,this);
        
    }

    public void ZoomOut()
    {
        
        GetComponent<Button>().enabled = false;
        Vector3 newPos = new Vector3(originalX, originalY, transform.position.z);
        GetComponent<RectTransform>().DOMove(newPos, 0.5f);
        footer.DOScaleY(0, 0.5f);
        GetComponent<RectTransform>().DOScale(0.4f, 0.5f).OnComplete(delegate { GetComponent<Button>().enabled = true;
            currentState = ButtonState.idle;
            inactivityTime = false;
            StopAllCoroutines();
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
                if (!inactivityTime)
                {
                    StartCoroutine(InactivityTime()); 
                }
                break;

            default:
                break;
        }
    }

    bool inactivityTime = false;
    IEnumerator InactivityTime ()
    {
        inactivityTime = true;
        yield return new WaitForSeconds(15);
        ZoomOut();
    }
}