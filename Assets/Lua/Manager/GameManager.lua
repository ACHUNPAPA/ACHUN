---
--- Created by Administrator.
--- DateTime: 2017/11/14 21:32
---

require("3rd/pblua/login_pb")
require("3rd/pbc/protobuf")

local lpeg = require("lpeg")
local json = require("cjson")
local util = require("3rd/cjson.util")
local sproto = require("3rd/sproto/sproto")
local print_r = require("3rd/sproto/print_r")
require("logic/lua_class")
require("Manager/CtrlManager")
require("Common/functions")

GameManager = {}
local this = GameManager
local game
local transform
local gameObject

function GameManager.LuaScriptPanel()
    return "Prompt","Message"
end

function GameManager.Awake()

end

function GameManager.Start()

end

function GameManager.Start()

end