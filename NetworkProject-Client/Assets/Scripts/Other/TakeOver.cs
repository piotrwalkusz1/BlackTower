using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TakeOver : MonoBehaviour
{
    public static TakeOver CurrentTakeOver { get; set; }

    public MonoBehaviour[] _active;
    public MonoBehaviour[] _inactive;
    public Camera _camera;

    public void Activate()
    {
        if (CurrentTakeOver != null)
        {
            CurrentTakeOver.Deactivate();
        }

        CurrentTakeOver = this;

        if (_camera != null)
        {
            if (Camera.main != null)
            {
                Camera.main.gameObject.SetActive(false);
            }         

            _camera.gameObject.SetActive(true);
        }

        foreach (var active in _active)
        {
            active.enabled = true;
        }

        foreach (var inactive in _inactive)
        {
            inactive.enabled = false;
        }
    }

    public void Deactivate()
    {
        if (CurrentTakeOver != this)
        {
            MonoBehaviour.print("Deaktywujesz ten controller, chociaż nie był on aktywny.");
        }

        CurrentTakeOver = null;

        foreach (var active in _active)
        {
            active.enabled = false;
        }

        foreach (var inactive in _inactive)
        {
            inactive.enabled = true;
        }
    }
}
