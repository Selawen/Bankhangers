using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : PlayerMovement
{
    [SerializeField] private float timing = 1;

    private Vector3 moveDir;

    private void Start()
    {
        StartCoroutine(Move());
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(timing);

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (debug)
        {
            Debug.Log($"move {moveDir}");
        }

        player.transform.Translate(moveDir);

        StartCoroutine(Move());
    }
}
