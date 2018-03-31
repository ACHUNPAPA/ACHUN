using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaTest : MonoBehaviour
{
    private LuaEnv lua;

    private void Awake()
    {
        lua = new LuaEnv();
        lua.AddLoader(MyLoader);
    }

    private void Start()
    {
        lua.DoString("require 'Main'");
    }

    private void Update()
    {
        if (lua != null)
            lua.Tick();
    }

    private void OnDestroy()
    {
        lua.Dispose();
    }


    private byte[] MyLoader(ref string filePath)
    {
        filePath = Application.dataPath + "/Lua/" + filePath.Replace('.', '/') + ".lua";
        if (File.Exists(filePath))
        {
            return File.ReadAllBytes(filePath);
        }
        else
        {
            return null;
        }
    }
}
