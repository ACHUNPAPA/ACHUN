using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Define
{
    public const string UGUI_INPUT_ONCLICK = "UGUI_INPUT_ONCLICK";
    public const string UGUI_INPUT_ONDOUBLECLICK = "UGUI_INPUT_ONDOUBLECLICK";
    public const string UGUI_INPUT_ONDOWN = "UGUI_INPUT_ONDOWN";
    public const string UGUI_INPUT_ONUP = "UGUI_INPUT_ONUP";
    public const string UGUI_INPUT_ONENTER = "UGUI_INPUT_ONENTER";
    public const string UGUI_INPUT_ONEXIT = "UGUI_INPUT_ONEXIT";
    public const string UGUI_INPUT_ONSELECT = "UGUI_INPUT_ONSELECT";
    public const string UGUI_INPUT_ONUPDATESELECT = "UGUI_INPUT_ONUPDATESELECT";
    public const string UGUI_INPUT_ONDESELECT = "UGUI_INPUT_ONDESELECT";
    public const string UGUI_INPUT_ONDRAG = "UGUI_INPUT_ONDRAG";
    public const string UGUI_INPUT_ONDRAGEND = "UGUI_INPUT_ONDRAGEND";
    public const string UGUI_INPUT_ONDROP = "UGUI_INPUT_ONDROP";
    public const string UGUI_INPUT_ONSCROLL = "UGUI_INPUT_ONSCROLL";
    public const string UGUI_INPUT_ONMOVE = "UGUI_INPUT_ONMOVE";
}

namespace AChun.UIExtend.UGUI
{
    public delegate void OnTouchHandle(GameObject _Listener, object _arg, params object[] _params);

    public class TouchHandle
    {
        public string touch;
        private event OnTouchHandle onTouchHandle = null;
        private object[] handleParams;
        public TouchHandle(OnTouchHandle onTouchHandle, params object[] _params)
        {
            SetHandle(onTouchHandle, _params);
        }

        public void TriggleHandle(GameObject _Listener, object _arg)
        {
            if (onTouchHandle != null)
                onTouchHandle(_Listener, _arg, handleParams);
        }

        public void SetHandle(OnTouchHandle onTouchHandle, params object[] _params)
        {
            DestroyHandle();
            this.onTouchHandle += onTouchHandle;
            handleParams = _params;
        }

        public void DestroyHandle()
        {
            if (onTouchHandle != null)
            {
                onTouchHandle -= onTouchHandle;
                onTouchHandle = null;
            }
        }
    }

    public class UGUIInput : MonoBehaviour,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler,

        ISelectHandler,
        IUpdateSelectedHandler,
        IDeselectHandler,

        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler,
        IMoveHandler
    {
        public void OnDeselect(BaseEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONDESELECT, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONDRAG, eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONDROP, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONDRAGEND, eventData);
        }

        public void OnMove(AxisEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONMOVE, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONCLICK, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONDOWN, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONEXIT, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONUP, eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONSCROLL, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONSELECT, eventData);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            TriggleHandle(Define.UGUI_INPUT_ONUPDATESELECT, eventData);
        }

        private Dictionary<string, TouchHandle> touchHandles = null;


        private void Awake()
        {
            touchHandles = new Dictionary<string, TouchHandle>();
        }

        public static void Get(GameObject _Listener)
        {
            if (_Listener != null)
                _Listener.AddComponent<UGUIInput>();
        }


        private TouchHandle GetHandle(string touchType)
        {
            TouchHandle touchHandle = null;
            touchHandles.TryGetValue(touchType, out touchHandle);
            return touchHandle;
        }


        public void SetHandle(string touchType, OnTouchHandle onTouchHandle, params object[] _params)
        {
            TouchHandle touchHandle = null;
            if (touchHandles.TryGetValue(touchType, out touchHandle))
            {
                touchHandle.SetHandle(onTouchHandle, _params);
            }
            else
            {
                touchHandle = new TouchHandle(onTouchHandle, _params);
                touchHandles.Add(touchType, touchHandle);
            }
        }


        private void TriggleHandle(string touchType, BaseEventData eventData)
        {
            TouchHandle touchHandle = null;
            if (touchHandles.TryGetValue(touchType, out touchHandle))
                touchHandle.TriggleHandle(gameObject, eventData);
        }


        public void RemoveHandle(string touchType)
        {
            if (touchHandles.ContainsKey(touchType))
                touchHandles.Remove(touchType);
        }


        public void Test()
        {
            IEC<int> iec = new IEC<int>();
            Dictionary<int, string> dict = new Dictionary<int, string>(iec);
        }
    }


    public class IEC<T> : IEqualityComparer<T> where T : IEquatable<T>
    {
        public bool Equals(T x, T y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }
    }
}