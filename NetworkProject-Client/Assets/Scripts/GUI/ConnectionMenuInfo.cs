using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using UnityEngine;

public class ConnectionMenuInfo
{
    public string IP { get; set; }
    public string Port { get; set; }
    public bool ManualWritteIP { get; set; }
    public List<HostInfo> HostList { get; set; }
    public DateTime EndConnectionTime { get; set; }
    public ConnectionStage Status { get; set; }
    public HostInfo ChosenHost { get; set; }
    public Vector2 ScrollPosition { get; set; }

    public ConnectionMenuInfo()
    {
        IP = "";
        Port = "";
        ManualWritteIP = false;
        HostList = new List<HostInfo>();
        Status = ConnectionStage.Disconnection;
    }
}
