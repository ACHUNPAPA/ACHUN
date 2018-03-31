using AChun.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.UI
{
    public abstract class BaseFacade : IFacade
    {
        public string facadeName
        {
            get;
            private set;
        }
        protected GameObject gameObject;
        protected Transform transform;
        protected CanvasGroup canvasGroup;

        protected BaseController _controller;
        public BaseController controller
        {
            get
            {
                return _controller;
            }
        }

        protected BaseModel _model;
        public BaseModel model
        {
            get
            {
                return _model;
            }
        }

        protected BaseView _view;
        public BaseView view
        {
            get
            {
                return _view;
            }
        }


        public BaseFacade(string facadeName,GameObject gameObject)
        {
            this.facadeName = facadeName;
            Init(gameObject);
        }

        public void HandleNotification(INotification notification)
        {
            if (notification is IUINotification)
            {
                _view.HandleNotification(notification as IUINotification);
            }
            else
            {
                _controller.ExcuteCommond(notification);
            }
        }

        public virtual void Init(GameObject gameObject)
        {
            this.gameObject = gameObject;
            transform = gameObject.transform;
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        public virtual void OnApplicationQuit()
        {
            _model.OnApplicationQuit();
            _view.OnApplicationQuit();
            _controller.OnApplicationQuit();
        }

        public virtual void OnDestroy()
        {
            _model.OnDestroy();
            _view.OnDestroy();
            _controller.OnDestroy();
        }

        public virtual void SendNotification(string facadeName, INotification notificaion)
        {
            throw new NotImplementedException();
        }

        public virtual void Update()
        {
            _model.Update();
            _view.Update();
            _controller.Update();
        }

        public virtual void Show()
        {
            canvasGroup.alpha = 255;
            canvasGroup.blocksRaycasts = true;
        }

        public virtual void Close()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}