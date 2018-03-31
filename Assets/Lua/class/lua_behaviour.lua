---
--- Created by Administrator.
--- DateTime: 2017/11/14 17:46
---

--该脚本接受unity回调

--实体 View
local LuaObj = {}
--控制器 Controller
local luaController = {}
--数据 Model
local luaModels = {}
--view和controller对应
local view_controller_enroll =
{
    --view = "controller"
}
--数据 model和controller对应
model_controller_enroll =
{
    --controller = "model"
}

--Unity到lua回调
unity_to_lua_event =
{
    Awake = "Awake",
    OnEnable = "OnEnable",
    Start = "Start",
    OnDisable = "OnDisable",
    OnDestroy = "OnDestroy"
}

function awake(gameObject,className,insID,objs,Len,prefabs,plen)
    local cacheObjs = {}
    for i = 0,Len - 1 do
        local obj = objs[i]
        if obj == nil then
            error(gameObject.name .. " 脚本为空！！！" .. i)
        end
        cacheObjs[obj.name] = obj
    end

    local cachePrefabs = {}
    for i = 0,plen - 1 do
        local obj = prefabs[i]
        if obj == nil then
            error(gameObject.name .. " 脚本传入为空！！" .. i)
        end
        cachePrefabs[obj.name] = obj
    end

    local m_cont = view_controller_enroll[className]
    if m_cont == nil then
        local class = _G[className]
        local obj = class.new()
        obj:Init(gameObject,insID,cacheObjs,cachePrefabs)
        LuaObj[insID] = obj
        obj:Awake()
    else
        local obj = luaController[m_cont]
        if obj == nil then
            local modelName = model_controller_enroll[m_cont]
            local class = _G[modelName]
            local modelObj = class.new()
            modelObj:ModelInit()
            luaModels[modelName] = modelObj

            class = _G[m_cont]
            obj = class.new()
            luaController[m_cont] = obj
            obj:GameInitData(modelObj)
        end
        obj:GameControllerInit(className,gameObject,insID,cacheObjs,cachePrefabs)
        obj:unity_to_lua_event(insID,UnityCallLuaEvent.Awake)
    end
end

function OnEnable(insID,className)
    local luaObj = LuaObj[insID]
    if luaObj == nil then
        local name = view_controller_enroll[className]
        if name ~= nil and luaController[name] ~= nil then
            luaController[name]:unity_to_lua_event(insID,UnityCallLuaEvent.OnEnable)
        else
            error(insID .. " OnEnable View " .. className)
        end
    else
        luaObj:OnEnable()
    end
end

function Start(insID,className)
    local luaObj = LuaObj[insID]
    if luaObj ~= nil then
        luaObj:Start()
    else
        local name = view_controller_enroll[className]
        if name ~= nil and luaController[name] ~= nil then
            luaController[name]:unity_to_lua_event(insID,UnityCallLuaEvent.Start)
        else
            error(insID .. " OnDisable View " .. className)
        end
    end
end

function OnDestroy(insID,className)
    local luaObj = LuaObj[insID]
    if luaObj ~= nil then
        luaObj.isDestroy = true
        luaObj:OnDestroy()
    else
        local name = view_controller_enroll[className]
        if name ~= nil and luaController[name] ~= nil then
            luaController[name]:unity_to_lua_event(insID,UnityCallLuaEvent.OnDestroy)
        else
            error(insID .. " OnDestroy View " .. className)
        end
        LuaObj[insID] = nil
    end
end

function Find_luaObj_by_Obj(gameObject)
    for k,v in pairs(LuaObj) do
        if v ~= nil and not v.isDestroy and v.gameObject == gameObject then
            return v
        end
    end
    local luaObj
    for k,v in pairs(luaController) do
        luaObj = v:GetLuaObjFromObj(gameObject)
        if luaObj ~= nil then
            return luaObj
        end
    end
    return nil
end

function find_luaObj_by_insID(insID)
    local luaObj = LuaObj[insID]
    if lua ~= nil then
        return luaObj
    end
    for k,v in pairs(luaController) do
        if v.insID == insID then
            luaObj = v:GetLuaObjFromInsID(insID)
            if luaObj ~= nil then
                return luaObj
            end
        end
    end
    return nil
end




