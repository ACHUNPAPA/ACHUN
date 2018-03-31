---
--- Created by Administrator.
--- DateTime: 2017/11/14 20:31
---

local localizations = {}
local function LocalizationsInit()
    localizations = get_cfg_localization()
end

LocalizationsInit()

function get_localization_by_key(key)
    local value = localizations[key]
    if value ~= nil and value ~= "" then
        return value
    end
    return nil
end