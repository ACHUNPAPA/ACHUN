---
--- Created by Administrator.
--- DateTime: 2017/11/17 0:40
---

require("Common/define")
require("3rd/pblua/login_pb")
require("3rd/pbc/protobuf")

local sproto = require("3rd/sproto/sproto")
local core = require("sproto.core")
local print_r = require("3rd/sproto/print_r")

promptCtrl = {}
local this = promptCtrl

local Panel
local prompt
local transform
local gameObject

function promptCtrl.New()
    return this
end

function promptCtrl.Awake()
    UIManager:CreateUI('Prompt',this.OnCreate)
end

function promptCtrl.OnCreate(obj)
    gameObject = obj
    transform = gameObject.transform
    Panel = transform:GetComponent('UIPanel')
    prompt = transform:GetComponent('LuaBehaviour')
    prompt:AddClick(PromptUI.btnOpen,this.OnClick)
    ResourcesManager:LoadPrefab('prompt',{'PromptItem'},this.InitUI)
end


function promptCtrl.InitUI(objs)
    local count = 100
    local parent = PromptUI.gridParent
    for i = 1,count do
        local go = newObject(objs[0])
        go.name = 'Item' .. tosting(i)
        go.transform:SetParent(parent)
        go.transform.localScale = Vector3.one
        go.transform.localPosition = Vector3.zero
        prompt:AddClick(go,this.OnItemClick)

        local lable = go.transform:Find('Text')
        lable:GetComponent('Text').text = tostring(i)
    end
end