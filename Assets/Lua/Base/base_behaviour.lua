---
--- Created by Administrator.
--- DateTime: 2017/11/12 21:26
---

base_behaviour = {}

function base_behaviour:Init(gameObject,insID,cacheObjs,cachaePrefabs)
    self.gameObject = gameObject
    self.transform = gameObject.transform
    self.name = gameObject.name
    self.insID = insID
    self.cacheObjs = cacheObjs
    self.cachaePrefabs = cachaePrefabs
end

function base_behaviour:Awake()

end

function base_behaviour:OnEnable()

end

function base_behaviour:Start()

end

function base_behaviour:Update()

end

function base_behaviour:OnDisable()

end

function base_behaviour:GetInsID()
    return self.insID
end

function base_behaviour:GetName()
    return self.name
end

function base_behaviour:OnDestroy()

end