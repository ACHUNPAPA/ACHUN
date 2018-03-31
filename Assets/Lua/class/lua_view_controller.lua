---
--- Created by Administrator.
--- DateTime: 2017/11/14 18:55
---

--控制器基类
lua_view_controller = newclass(base_behaviour)

function lua_view_controller:GameInitData(modelName)
    self.LuaObjs = {}
    self.modelLuaObj = modelLuaObj
end

function lua_view_controller:GameControllerInit(className,gameObject,insID,cacheObjs,cachePrefabs)
    local class = _G[className]
    local Obj = class.new()
    Obj:Init(gameObject,insID,cacheObjs,cachePrefabs)
    self.luaObjs[insID] = Obj
end

function lua_view_controller:UnityCallLuaEvent(insID,eventName)
    local luaObj = self.luaObjs[insID]
    if luaObj ~= nil then
        if eventName == unity_to_lua_event.Awake then
            self:Awake()
        elseif eventName == unity_to_lua_event.OnEnable then
            self:OnEnable()
        elseif eventName == unity_to_lua_event.Start then
            self:Start()
        elseif eventName == unity_to_lua_event.OnDisable then
            self:OnDisable()
        elseif eventName == unity_to_lua_event.OnDestroy then
            self:OnDestroy()
        else
            error(insID .. " 脚本没有 " .. eventName .. " 方法")
        end
    end
end

function lua_view_controller:Awake(luaObj)
    luaObj:Awake()
end

function lua_view_controller:OnEnable(luaObj)
    luaObj:OnEnable()
end

function lua_view_controller:Start(luaObj)
    luaObj:Start()
end

function lua_view_controller:OnDisable(luaObj)
    luaObj:OnDisable()
end

function lua_view_controller:OnDestroy(luaObj)
    luaObj:OnDestroy()
end

function lua_view_controller:GetLuaObjFromInsID(insID)
    local luaObj = self.LuaObjs[insID]
    if luaObj ~= nil then
        return luaObj
    end
    return nil
end

function lua_view_controller:GetLuaObjFromObj(gameObject)
    for k,v in pairs(self.luaObjs) do
        if v.gameObject == gameObject then
            return v
        end
    end
    return nil
end