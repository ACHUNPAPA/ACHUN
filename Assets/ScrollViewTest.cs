using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewTest : MonoBehaviour, IDragHandler,IEndDragHandler
{
    private ScrollRect sr;
    private RectTransform rect;
    private GridLayoutGroup group;
    private Transform elementsParent;
    private int MaxElementCount;
    private Transform pool;
    private enum Dir
    {
        none,
        up,
        down
    }
    private Dir dir = Dir.none;
    private string poolName = "ElementPool";

    private void Awake()
    {
        sr = GetComponent<ScrollRect>();
        rect = GetComponent<RectTransform>();
        group = sr.content.GetComponent<GridLayoutGroup>();
        elementsParent = group.transform;
        int row = (int)(rect.sizeDelta.x / (group.cellSize.x + group.spacing.x));
        int column = (int)(rect.sizeDelta.y / (group.cellSize.y + group.spacing.y)) + 4;
        MaxElementCount = row * column;
        pool = transform.Find("Pool");
    }

    private void Start()
    {
        //PoolManager.Instance.CreatePool(poolName,pool);
        AddElements(30);
    }


    private void AddElements(int number)
    {
        for (int i = elementsParent.childCount; i < MaxElementCount; i++)
        {
            //GameObject go = PoolManager.Instance.Spawn(poolName) as GameObject;
            //if (go == null)
            //{
            //    go = new GameObject(string.Format("Element{0}",i));
            //    go.AddComponent<ELement>().SetParent(group);
            //}
        }
    }

    private void Update()
    {
        //for (int i = 0; i < elementsParent.childCount; i++)
        //    if (elementsParent.GetChild(i).GetComponent<ELement>().isHide)
        //        PoolManager.Instance.EnterObjToPool(poolName, elementsParent.GetChild(i).gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
