using UnityEngine;
using UnityEngine.UI;

public class ContentAdapt : MonoBehaviour
{
    int columns = 4;
    float cellheight = 170;
    float verticalSpacing = 35;

    private void Awake()
    {
        var grid = GetComponent<GridLayoutGroup>();
        var rt = GetComponent<RectTransform>();

        columns = grid.constraintCount;
        cellheight = grid.cellSize.y;
        verticalSpacing = grid.spacing.y;
        int rows = Mathf.CeilToInt((float)transform.childCount / columns);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rows * (cellheight + verticalSpacing)) - verticalSpacing);
    }
}
