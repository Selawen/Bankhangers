using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private bool firstPerson;

    [Header("First Person")]
    [SerializeField] private Vector3 headOffset;

    [Header("Third Person")]
    [SerializeField] private float followSpeed;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private Quaternion rotation;

    private void OnValidate()
    {
        if (firstPerson)
        {
            transform.parent = player;
            transform.SetPositionAndRotation(Vector3.zero+headOffset, Quaternion.identity);
        }
        else
        {
            transform.parent = null;
            transform.position = player.position + followOffset;
            transform.rotation = rotation;
        }
    }

    private void Update()
    {
        if (!firstPerson)
        {
            Vector3 direction = (player.position + followOffset) - transform.position;

            transform.Translate(direction * followSpeed * Time.deltaTime);
        }
    }
}
