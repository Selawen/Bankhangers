using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTurning3D : PlayerMovement
{
    [Range(0, 10)] [SerializeField] private float speed = 3;
    [Range(0, 10)] [SerializeField] private float rotationSpeed = 2;
    [Range(0, 25)] [SerializeField] private float jumpSpeed = 3;

    [SerializeField] private bool relativeJump;
    [Tooltip("An airjump cannot be relative")]
    [SerializeField] private bool airJump;
    [SerializeField] private int airJumpAmount;

    private Rigidbody playerBody;

    // airjump
    private bool jumped = false;
    private int jumpCount = 0;

    private void Awake()
    {
        playerBody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newForward = player.transform.forward;
        newForward = newForward.normalized * Input.GetAxis("Vertical");
        playerBody.MovePosition(transform.position + newForward * speed * Time.deltaTime);

        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = player.transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Horizontal")*rotationSpeed, 0);
        playerBody.MoveRotation(newRotation);

        if (airJump)
        {
            if (!Input.GetKey(KeyCode.Space))
                jumped = false;
            else if (jumpCount < airJumpAmount && !jumped)
            {
                playerBody.velocity += new Vector3(0, jumpSpeed, 0);

                jumpCount++;
                jumped = true;
            }

            if (Physics.CheckSphere(player.transform.position + Vector3.down, 0.3f, groundMask) && playerBody.velocity.y < 0.01)
            {
                jumped = false;
                jumpCount = 0;
            }

        }
        else if (Input.GetKey(KeyCode.Space) && Physics.CheckSphere(player.transform.position + Vector3.down, relativeJump ? 1f : 0.2f, groundMask))
        {
            if (relativeJump || playerBody.velocity.y < 0.01)
                playerBody.velocity += new Vector3(0, jumpSpeed, 0);
        }

        if (debug)
        {
            Debug.Log($"jump button: {Input.GetKey(KeyCode.Space)}, is on ground: {Physics.CheckSphere(player.transform.position + Vector3.down, 0.5f, groundMask)}");
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(player.transform.position + Vector3.down, 0.5f);
        }
    }
}
