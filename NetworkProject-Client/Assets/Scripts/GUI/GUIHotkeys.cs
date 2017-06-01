using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToServer;

public class GUIHotkeys : MonoBehaviour
{
    public List<GUIHotkeysObject> _guiHotkeys;

    private static GUIHotkeys _hotkeys;

    void Awake()
    {
        _hotkeys = this;
    }

    public static void SendHotkeysUpdate()
    {
        var request = new UpdateHotkeysToServer(PackageConverter.HotkeysToPackage(GetHotkeys()).ToArray());

        Client.SendRequestAsMessage(request);
    }

    public static void UpdateHotkeys(HotkeysObject[] hotkeys)
    {
        for (int i = 0; i < hotkeys.Length; i++)
        {
            _hotkeys._guiHotkeys[i].HotkeysObject = hotkeys[i];
        }
    }

    private static HotkeysObject[] GetHotkeys()
    {
        var result = new List<HotkeysObject>();

        foreach (var hotkey in _hotkeys._guiHotkeys)
        {
            result.Add(hotkey.HotkeysObject);
        }

        return result.ToArray();
    }
}
