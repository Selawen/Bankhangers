using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private bool firstPerson, isometrisch, moveable, zoom;

    [SerializeField] private float minZoom, maxZoom, mouseSensitivity;

    [Header("First Person")]
    [SerializeField] private Vector3 headOffset;

    [Header("Third Person")]
    [SerializeField] private float followSpeed;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private Quaternion rotation;

    private Vector3 lastMouse;
    private float currentZoom = 8;

    private void OnValidate()
    {
        if (firstPerson)
        {
            isometrisch = false;
            zoom = false;
            transform.parent = player;
            transform.SetPositionAndRotation(Vector3.zero+headOffset, Quaternion.identity);
        }
        else
        {
            transform.parent = null;
            transform.position = player.position + followOffset;
            transform.rotation = rotation;

        } 
        
        if (isometrisch)
            Camera.main.orthographic = true;
        else
            Camera.main.orthographic = false;


        if (moveable)
        {
            lastMouse = Input.mousePosition;
        }
    }

    private void Update()
    {
        if (moveable)
        {
            Vector3 moveDir = lastMouse - Input.mousePosition;

            lastMouse = Input.mousePosition;

            followOffset += moveDir* mouseSensitivity;

            transform.position = player.position + followOffset;
            transform.LookAt(player);
        }

        if (zoom)
        {
            currentZoom -= Input.mouseScrollDelta.y;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            if (isometrisch)
            {
                Camera.main.orthographicSize = currentZoom;
            }
            else
            {
                //transform.LookAt(player);
                transform.position = player.position - transform.forward * currentZoom;
            }
        }

        if (!firstPerson && !moveable && !zoom)
        {
            Vector3 direction = (player.position + followOffset) - transform.position;

            transform.Translate(direction * followSpeed * Time.deltaTime);
        }
    }
}
