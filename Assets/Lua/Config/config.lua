---
--- Created by Administrator.
--- DateTime: 2017/11/14 20:17
---

local config_list = {}

local function configCFG(cfg)
    local cfgs = {}
    for k,v in pairs(cfg) do
        cfgs[v.index] = v
    end
    return cfgs
end

local function configLocalization(cfg)
    local cfgs = {}
    for k,v in pairs(cfg) do
        cfgs[v.key] = v.value
    end
    return cfgs
end


local config_map =
{
    Test = {url = Res.CSV_TEST,func = configCFG},
    LOCALIIZATON = {Res.CSV_LOCALIZATION,func = configLocalization}
}

local function config_analysis(tableName)
    local map = config_map[tableName]
    local url = map.url
    local func = map.func
    require("game/configs/" .. url .. "_csv")
    local cfg = CSV_TABLES[map.url]
    if cfg == nil then
        cfg = func(cfg,tableName)
        CSV_TABLES[tableName] = cfg
    end
    config_list[tableName] = cfg
    return cfg
end

function config_get_cfg(tableName)
    local cfg = config_list[tableName]
    if cfg == nil then
        return config_analysis(tableName)
    end
    return cfg
end

function get_cfg_test(id)
    return config_get_cfg("TEST")[id]
end

function get_cfg_localization()
    return config_get_cfg("LOCALIZATION")
end