using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;


public class FairyGUITest : MonoBehaviour
{
    private GComponent LoginView;

    private void Start()
    {
        Debug.Log("start");
        UIConfig.defaultFont = "afont";
        UIPackage.AddPackage("UI/Achun_Pro");
        LoginView = UIPackage.CreateObject("Achun_Pro","LoginUI").asCom;
        LoginView.SetSize(GRoot.inst.width,GRoot.inst.height);
        LoginView.AddRelation(GRoot.inst,RelationType.Size);
        GRoot.inst.AddChild(LoginView);
    }
}
