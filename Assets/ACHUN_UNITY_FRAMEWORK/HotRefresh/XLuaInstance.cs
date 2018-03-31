using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class XLuaInstance : BaseHotRefresh
{
    private LuaEnv luaEnv;
    private LuaTable luatab;
    public Injection[] injections;
    private bool isDebug = false;
    private string luaDir = string.Empty;

    private Action AwakeFun;
    private Action StartFun;
    private Action UpdateFun;
    private Action OnDestroyFun;

    public XLuaInstance()
    {
        luaEnv = new LuaEnv();        
        InitLuaPath();
        AddLuaLoader();

        luatab = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();        
        meta.Set("__index", luaEnv.Global);
        luatab.SetMetaTable(meta);
        meta.Dispose();

        luatab.Set("self", this);
        //foreach (var injection in injections)
        //{
        //    luatab.Set(injection.name, injection.value);
        //}

        
        //luatab.Get("Start", out StartFun);
        //luatab.Get("Update", out UpdateFun);
        //luatab.Get("OnDestroy", out OnDestroyFun);

        luaEnv.DoString("require 'Main'");
        AwakeFun = luaEnv.Global.GetInPath<Action>("Main.Awake");

        Debug.Log(AwakeFun == null);
        if (AwakeFun != null)
            AwakeFun();
    }

    public override void OnDestroy()
    {
        if (OnDestroyFun != null)
            OnDestroyFun();

        AwakeFun = null;
        luaEnv.Dispose();
        luaEnv = null;
    }

    public override void Start()
    {        
        if (StartFun != null)
            StartFun();
    }


    private void InitLuaPath()
    {
        if (isDebug)
            AddSearchPath(luaDir);
    }


    private void AddLuaLoader()
    {
        luaEnv.AddLoader(MyLoader);
    }


    private void AddSearchPath(string fullPath)
    {
        if (!Path.IsPathRooted(fullPath))
            throw new LuaException(fullPath + " is not a full path!");

        fullPath = ToPackagePath(fullPath);
    }


    private string ToPackagePath(string path)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(path);
        sb.Replace('\\','/');
        if (sb.Length > 0 && sb[sb.Length - 1] != '/')
            sb.Append("?.lua");
        return sb.ToString();
    }


    private byte[] MyLoader(ref string fullPath)
    {
        fullPath = Application.dataPath + "/Lua/" + fullPath + ".lua";
        if (System.IO.File.Exists(fullPath))
            return File.ReadAllBytes(fullPath);
        else
            return null;
    }


    public override void DoString(string luaScript)
    {
        if (luaEnv != null)
            luaEnv.DoString(luaScript);
    }


    public override void Update()
    {
        if (UpdateFun != null)
            UpdateFun();
    }

    public override Action GetFun(string funName)
    {
        return null;
    }
}
