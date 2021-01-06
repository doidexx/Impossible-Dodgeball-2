using UnityEngine;
using UnityEngine.UI;

public class ContentAdapt : MonoBehaviour
{
    public bool vertical = true;

    int constraintCount = 0;
    float cellHeight = 0;
    float cellWidth = 0;
    float verticalSpacing = 0;
    float horizontalSpacing = 0;

    GridLayoutGroup gridLayout;
    RectTransform rectTransform;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        constraintCount = gridLayout.constraintCount;
        cellHeight = gridLayout.cellSize.y;
        cellWidth = gridLayout.cellSize.x;
        verticalSpacing = gridLayout.spacing.y;
        horizontalSpacing = gridLayout.spacing.x;
        if (vertical)
            FixContentVertically();
        else
            FixContentHorizontally();
    }

    private void FixContentHorizontally()
    {
        int columns = Mathf.CeilToInt((float)transform.childCount / constraintCount);
        var newX = (columns * (cellWidth + horizontalSpacing)) + horizontalSpacing;
        rectTransform.sizeDelta = new Vector2(newX, rectTransform.sizeDelta.y);
    }

    private void FixContentVertically()
    {
        int rows = Mathf.CeilToInt((float)transform.childCount / constraintCount);
        var newY = (rows * (cellHeight + verticalSpacing)) + verticalSpacing;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newY);
    }
}
