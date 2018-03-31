---
--- Created by Administrator.
--- DateTime: 2017/11/12 20:14
---

CtrlName =
{
    LoginCtrl
}

PanelNames =
{
    "LoginPanel",
}

ProtocalType =
{
    BINARY = 0,
    PB_LUA = 1,
    PBC = 2,
    SPROTO = 3,
}

--UnityEngine
Debug = CS.UnityEngine.Debug
GameObject = CS.UnityEngine.GameObject
Transform = CS.UnityEngine.Transform
Component = CS.UnityEngine.Component

--UnityEngine.UI
Image = CS.UnityEngine.UI.Image
Button = CS.UnityEngine.UI.Button
Text = CS.UnityEngine.UI.Text

--logic static
loop = CS.Achun.Loop

--logic instance
local ResManager = CS.Achun.ResourcesManager
ResourcesManager = ResManager()
NetManager = loop:GetNetManager()
UIManager = loop:GetUIManager()
SoundManager = loop:GetSoundManager()
