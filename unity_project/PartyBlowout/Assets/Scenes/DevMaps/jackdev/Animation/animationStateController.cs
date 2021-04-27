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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();

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
        
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
            FindObjectOfType<AudioManager>().Play("Walk");
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
            FindObjectOfType<AudioManager>().Stop("Walk");
        }
        
        if (!isWalkingBack && backwardsPressed)
        {
            animator.SetBool("isWalkingBack", true);
        }
        if (isWalkingBack && !backwardsPressed)
        {
            animator.SetBool("isWalkingBack", false);
        }
        
        if (!isRunning && (runPressed && (forwardPressed || backwardsPressed)))
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && (!runPressed || (!forwardPressed || !backwardsPressed)))
        {
            animator.SetBool("isRunning", false);
        }
    }
}
