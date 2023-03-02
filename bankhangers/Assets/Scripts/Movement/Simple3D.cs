using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple3D : PlayerMovement
{
    [Range(0, 10)] [SerializeField] private float speed = 3;
    [Range(0, 15)] [SerializeField] private float jumpSpeed = 3;

    // Update is called once per frame
    void FixedUpdate()
    {
        player.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(player.transform.position + Vector3.down, 0.5f, groundMask))
        {
            player.GetComponent<Rigidbody>().velocity += new Vector3(0, jumpSpeed, 0);
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
