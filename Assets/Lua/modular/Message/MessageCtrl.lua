---
--- Created by Administrator.
--- DateTime: 2017/11/14 22:25
---

MessageCtrl = {}
local this = MessageCtrl

local message
local transform
local gameObject


function MessageCtrl.New()
    return this
end

function MessageCtrl.Awake()
    UIManager:CreatePanel("Message",this.OnCreate)
end


function MessageCtrl.OnCreate(obj)
    gameObject = obj
    message = gameObject:GetComponent("LuaBehaviour")
    message:AddClick(MessagePanel.btnClose,this.OnClick)
end

function MessageCtrl.OnClick(go)
    destroy(gameObject)
end

function MessageCtrl.Close()
    UIManager:Close(CtrlNames.Message)
end