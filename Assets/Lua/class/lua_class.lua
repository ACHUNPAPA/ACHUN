---
--- Created by Administrator.
--- DateTime: 2017/11/12 22:13
---

luaClass = {x = 0,y = 0}

luaClass.__index = luaClass

function luaClass:New(x,y)
    local self = {}
    setmetatable(self,luaClass)
    self.x = x
    self.y = y
    return self
end