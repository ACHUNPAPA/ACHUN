---
--- Created by Administrator.
--- DateTime: 2017/11/12 3:53
---

function clone(object)
    local lookUp_tab = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookUp_tab[object] then
            return lookUp_tab[object]
        end
        local new_table = {}
        lookUp_tab[object] = new_table
        for key,value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table,getmetatable(object))
    end
    return _copy(object)
end


function class(className,super)
    local superType = type(super)--具体类型
    local cls

    if superType ~= "function" and super ~= "table" then
        superType = nil
        super = nil
    end

    if superType == "function" or (super and super._copy == 1) then
        cls = {}
        if superType == "table" then
            for k,v in pairs(super) do
                cls[k] = v
            end
            cls.__create = super.__create
            cls.super = super
        else
            cls.__create = super
        end

        cls.ctor = function() end
        cls.__cname = className
        cls.__ctype = 1

        function cls.New(...)
            local instance = cls.__create(...)
            for k,v in pairs(cls) do
                instance[k] = v
            end
            instance.class = cls
            instance:ctor(...)
            return instance
        end
    else
        if super then
            cls = clone(super)
            cls.super = super
        else
            cls = {ctor = function () end}
        end

        cls.__cname = className
        cls.__ctype = 2--lua
        cls.__index = cls

        function cls.New(...)
            local instance = setmetatable({},cls)
            instance.class = cls
            instance:ctor(...)
            return instance
        end
    end
    return cls
end


--云风源码
local _class = {}

function newclass(super)
    --返回的类
    local class_type = {}

    class_type.ctor = false
    --基类
    class_type.super = super
    --实例化方法
    class_type.new = function(...)
        --派生类
        local obj = {}
        do
            local create
            create = function(c,...)
                if c.super then
                    --继续获取基类
                    create(c.super,...)
                end
                if c.ctor then
                    c.ctor(obj,...)
                end
            end
            create(class_type,...)
        end
        --获取基类元素
        setmetatable(obj,{__index = _class[class_type]})
        return obj
    end

    --实例化一个新表，做增、查
    local vtbl = {}
    _class[class_type] = vtbl
    setmetatable(class_type,{__newindex =
        function(t,k,v)
            vtbl[k] = v
        end
    })
    if super then
        setmetatable(vtbl,{__index =
            function(t,k)
                local ret = _class[super][k]
                vtbl[k] = ret
                return ret
            end
        })
    end
    return class_type
end



local achun_class = {}
function achun_newclass(super)
    local class_type = {}
    class_type.ctor = false
    class_type.super = super

    local vtbl = {}
    achun_class[class_type] = vtbl

    if super ~= nil then
        setmetatable(vtbl,{__index = function (t,k)
            local ret = achun_class[super][k]
            vtbl[k] = ret
            return ret
        end})
    end

    setmetatable(class_type,{__newindex = function (t,k,v)
        vtbl[k] = v
    end})

    class_type.new = function(...)
        local obj = {}
        do
            local create
            create = function(c,...)
                if c.super ~= nil then
                    create(c.super)
                end
                if c.ctor ~= nil then
                    c.ctor(...)
                end
            end
            create(class_type)
        end
        setmetatable(obj,{__index = achun_class[class_type]})
        return obj
    end

    return class_type
end