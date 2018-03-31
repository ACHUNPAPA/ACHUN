---
--- Created by Administrator.
--- DateTime: 2017/11/14 21:11
---

require("Common/define")
require("Controller/PromptCtrl")
require("Controller/MessageCtrl")

CtrlManager = {}
local this = CtrlManager
local ctrl_list = {}

function CtrlManager.Init()
    warn("")
    ctrl_list[CtrlName.Prompt] = PromptCtrl.New()
    ctrl_list[CtrlName.Message] = MessageCtrl.New()
    return this
end

function CtrlManager.AddCtrl(ctrlName,ctrlObj)
    ctrl_list[ctrlName] = ctrlObj
end

function CtrlManager.GetCtrl(ctrlName)
    return ctrl_list[ctrlName]
end

function CtrlManager.RemoveCtrl(ctrlName)
    ctrl_list[ctrlName] = nil
end

function CtrlManager.close()

end