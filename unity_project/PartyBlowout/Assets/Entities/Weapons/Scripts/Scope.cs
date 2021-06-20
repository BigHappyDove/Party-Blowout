using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{

    public GameObject scopeOverlay;

    public GameObject sniper;

    public Player player;

    public Camera mainCamera;

    public float scopedFOV = 15;
    private float normalFOV;

    public float sensitivity = 0.4f;
    private float normalSensitivity;

    private void Awake()
    {
        normalSensitivity = player.GetSensitivity();
        normalFOV = mainCamera.fieldOfView;
        scopeOverlay.SetActive(false);
    }

    private void OnDisable() => OnUnscoped();

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(OnScoped());
        }

        if (Input.GetButtonUp("Fire2"))
        {
            OnUnscoped();
        }

    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        sniper.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
        player.SetSensitivity(normalSensitivity);
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.01f);
        if (Input.GetButton("Fire2"))
        {
            scopeOverlay.SetActive(true);
            sniper.SetActive(false);
            mainCamera.fieldOfView = scopedFOV;
            player.SetSensitivity(sensitivity);
        }
    }
}
