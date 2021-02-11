using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class Transformation : MonoBehaviourPunCallbacks
{    
    [SerializeField] // Pour utiliser la variable privée sur unity (mais pas sur les autres scripts)
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.8f;
    [SerializeField]
    private float _rotationSpeed;

    private PhotonView _photonView;

    private CharacterController _controller; // Méthode pour déplacer un objet

    private float _directionY;

    private bool _canDoubleJump;
    
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView.IsMine)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
            Vector3 rotation = new Vector3(0, mouseX, 0);

            if (_controller.isGrounded) // Eviter le jump infini
            {
                _canDoubleJump = true;
                if (Input.GetButtonDown("Jump"))
                {
                    _directionY = _jumpSpeed;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && _canDoubleJump)
                {
                    _directionY = _jumpSpeed * _doubleJumpMultiplier;
                    _canDoubleJump = false;
                }
            }

            _directionY -= _gravity * Time.deltaTime; // * Time.deltaTime : redescente de l'objet en douceur 

            direction.y = _directionY; // Eviter de réinitialiser la position en y à 0 chaque frame


            _controller.Move(direction * (_moveSpeed * Time.deltaTime));
            transform.Rotate(rotation * (Time.deltaTime * _rotationSpeed));
        }
    }
}
