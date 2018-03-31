using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ELement : MonoBehaviour
{
    private GridLayoutGroup group;
    private RectTransform parent_rect;
    public bool isHide
    {
        get;
        private set;
    }

    private void Awake()
    {
        gameObject.AddComponent<Image>();
    }

    public void SetParent(GridLayoutGroup group)
    {
        if (group != null)
        {
            this.group = group;
            transform.SetParent(group.transform);
            parent_rect = group.GetComponent<RectTransform>();
        }
        isHide = false;
    }

    private void Update()
    {
        if (transform.localPosition.y >= parent_rect.sizeDelta.y + (group.cellSize.y + group.spacing.y) * 2 || transform.localPosition.y <= 0 - (group.cellSize.y + group.spacing.y) * 2)
        {
            isHide = true;
        }
        else
            isHide = false;
    }
}
