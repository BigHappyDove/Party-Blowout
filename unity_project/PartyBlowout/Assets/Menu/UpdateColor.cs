using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateColor : MonoBehaviour
{
    public Light light;
    public Color col1;
    public Color col2;
    public float t;

    // Update is called once per frame
    void Update()
    {
        light.color = Color.Lerp(col1, col2, Mathf.PingPong(Time.time, t));
    }
}
