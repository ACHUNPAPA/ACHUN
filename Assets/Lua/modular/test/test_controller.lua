---
--- Created by Administrator.
--- DateTime: 2017/11/14 20:52
---

test_controller = newclass(lua_view_controller)

require("game/modular/model/test_model")
require("game/modular/view/test_view")

function test_controller:Awake(luaObj)
    luaObj:Awake()
    self.modelLuaObj:test_model_init()
end

function test_controller:OnEnable()

end

function test_controller:Start(luaObj)
    luaObj:Start()
end

function test_controller:OnDisable()

end