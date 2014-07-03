using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class Test : MonoBehaviour
{

    public Transform _target;

    void Start()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            print("Jest zderzenie.");
        }
        else
        {
            print("Nie ma zderzenia - Test");
        }
    }
}
