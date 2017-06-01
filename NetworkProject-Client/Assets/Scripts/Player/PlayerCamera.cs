using UnityEngine;
using System;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public float _minDistance;
    public float _maxDistance;
    public float _speed;
    public float _checkRadius;

    public GameObject _camera;

    void Update()
    {
        LayerMask layerMask = LayerMask.GetMask("Terrain");

        if (IsCollision(layerMask))
        {
            while (true)
            {
                _camera.transform.localPosition = new Vector3(0, 0, Mathf.MoveTowards(_camera.transform.localPosition.z, _minDistance, 0.01f));

                if (_camera.transform.localPosition.z == _minDistance || !IsCollision(layerMask))
                {
                    break;
                }
            }
        }
        else
        {
            Vector3 lastPosition = _camera.transform.position;

            _camera.transform.localPosition = new Vector3(0, 0, Mathf.MoveTowards(_camera.transform.localPosition.z, _maxDistance, _speed * Time.deltaTime));

            if (IsCollision(layerMask))
            {
                _camera.transform.position = lastPosition;
            }
        }       
    }

    public Vector3 GetLookPointOrDirection()
    {
        RaycastHit hitInfo;
        Vector3 dir = _camera.transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(_camera.transform.position, dir, out hitInfo))
        {
            return hitInfo.point;
        }
        else
        {
            return dir * 10000f;
        }
    }

    public GameObject GetLookObject()
    {
        RaycastHit hitInfo;
        Vector3 dir = _camera.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(_camera.transform.position, dir, out hitInfo))
        {
            return hitInfo.collider.gameObject;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private bool IsCollision(LayerMask layerMask)
    {
        return Physics.CheckSphere(_camera.transform.position, _checkRadius, layerMask);
    }
}
