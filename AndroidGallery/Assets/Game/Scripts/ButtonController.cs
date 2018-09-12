using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    Vector2 floatY;
    float originalX;
    float originalY;

    public float floatStrength;

    void Start()
    {
        floatStrength = UnityEngine.Random.Range(0.1f, 1f) * floatStrength;
        this.originalY = this.transform.position.y;
        this.originalX = this.transform.position.x;

    }

    void Update()
    {
        /* Old code:
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time) * floatStrength);
        transform.position = floatY;
        */
        transform.position = new Vector2(originalX,
            originalY + ((float)Math.Sin(Time.time) * floatStrength));
    }
}
