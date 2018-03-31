using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Define
{
    #region UIName
    public const string UI_ = "";
    #endregion    
}

namespace AChun.UI
{
    public class UIManager : IManager
    {
        private Dictionary<string, BaseFacade> ui_map;
        private Stack<BaseFacade> UI_stack;
        private readonly byte ShowUICount = 8;

        public void Init()
        {
            UI_stack = new Stack<BaseFacade>(ShowUICount);
            ui_map = new Dictionary<string, BaseFacade>();
            
        }

        public void OnApplicationQuit()
        {
            foreach (BaseFacade facade in ui_map.Values)
            {
                facade.OnApplicationQuit();
            }
            ui_map.Clear();
            ui_map = null;
            UI_stack.Clear();
            UI_stack = null;
        }

        public void OnDestroy()
        {
            foreach (BaseFacade facade in ui_map.Values)
            {
                facade.OnDestroy();
            }
            ui_map.Clear();
            ui_map = null;
            UI_stack.Clear();
            UI_stack = null;
        }

        public void Update()
        {
            foreach (BaseFacade facade in ui_map.Values)
                facade.Update();
        }

        public void ShowUI(string facadeName)
        {
            if (UI_stack.Count >= 8)
                return;
            BaseFacade facade;
            if (ui_map.TryGetValue(facadeName, out facade))
            {
                UI_stack.Push(facade);
                facade.Show();
            }
        }


        public void CloseUI()
        {

        }

        public void OnApplicationPause()
        {
            
        }
    }
}