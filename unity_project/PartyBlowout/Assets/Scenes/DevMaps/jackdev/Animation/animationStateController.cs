using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        // Increase performance
        //isWalkingHash = Animator.StringToHash("isWalking");
        //isWalkingHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("z");
        bool runPressed = Input.GetKey("left shift");
        
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }
        
        
        if (!isRunning && (runPressed && forwardPressed))
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && (!runPressed || !forwardPressed))
        {
            animator.SetBool("isRunning", false);
        }
    }
}
