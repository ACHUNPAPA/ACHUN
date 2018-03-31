---
--- Created by Administrator.
--- DateTime: 2017/11/14 22:26
---


local transform
local gameObject
MessageUI = {}
local this = MessageUI

function MessageUI:Init(obj)
    gameObject = obj
    transform = gameObject.transform
    this.InitUI()
end

function MessageUI.InitUI()
    this.btnClose = transform.Find("child").gameObject
end

function MessageUI.OnDestroy()

end