using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    private PhotonView _photonView;
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
        _audioManager = GetComponentInParent<AudioManager>();

        // Increase performance
        //isWalkingHash = Animator.StringToHash("isWalking");
        //isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine)
            return;

        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool isWalkingBack = animator.GetBool("isWalkingBack");
        bool forwardPressed = Input.GetKey("z");
        bool backwardsPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");

        switch (isWalking)
        {
            case false when forwardPressed:
                animator.SetBool("isWalking", true);
                break;
            case true when !forwardPressed:
                animator.SetBool("isWalking", false);
                break;
        }

        switch (isWalkingBack)
        {
            case false when backwardsPressed:
                animator.SetBool("isWalkingBack", true);
                break;
            case true when !backwardsPressed:
                animator.SetBool("isWalkingBack", false);
                break;
        }

        switch (isRunning)
        {
            case false when (runPressed && (forwardPressed || backwardsPressed)):
                animator.SetBool("isRunning", true);
                break;
            case true when (!runPressed || (!forwardPressed || !backwardsPressed)):
                animator.SetBool("isRunning", false);
                break;
        }
    }
}
