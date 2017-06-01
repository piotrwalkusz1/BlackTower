using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public virtual void Stop()
    {
    }

    public virtual void SetNewPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public virtual void SetNewRotation(float rotationY)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
    }
}
