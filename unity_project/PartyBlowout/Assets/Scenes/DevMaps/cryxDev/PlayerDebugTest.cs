using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugTest : MonoBehaviour
{
    public GameObject cameraObj = null;
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GetComponentInChildren<Camera>().gameObject;
        cameraObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
