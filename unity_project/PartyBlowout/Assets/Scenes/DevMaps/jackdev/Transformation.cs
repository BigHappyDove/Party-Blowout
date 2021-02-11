using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    
    private CharacterController _controller; // Méthode pour déplacer un objet

    private float _directionY;

    private bool _canDoubleJump;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

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

        direction.y = _directionY;  // Eviter de réinitialiser la position en y à 0 chaque frame
        
        _controller.Move(direction * (_moveSpeed * Time.deltaTime));
    }
}
