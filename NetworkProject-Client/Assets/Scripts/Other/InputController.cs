using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float _movementSpeed;
    public float _rotationSpeed;

    void Update()
    {
        Vector3 rot = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        rot = rot * _rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.right, rot.x, Space.Self);
        transform.Rotate(Vector3.up, rot.y, Space.World);

        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
    }
}
