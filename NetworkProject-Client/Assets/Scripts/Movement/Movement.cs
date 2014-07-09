using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public virtual void SetNewTargetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void SetNewRotation(float rotation)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }

    public virtual void Jump(Vector3 position, Vector3 direction)
    {

    }
}
