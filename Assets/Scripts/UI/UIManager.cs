using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CanvasS[] canvasses = null;
    [Header("Preview Renderers")]
    public Renderer ball = null;
    public ModelControl[] models = null;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }

    public void OpenCanvas(string name)
    {
        foreach (CanvasS canvas in canvasses)
        {
            if (canvas.canvasName.ToString() != name)
                canvas.gameObject.SetActive(false);
            else
                canvas.gameObject.SetActive(true);
        }

        if (name == CanvasName.Pause.ToString())
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ChangePreviewSkinTo(ModelType modelType, Material material)
    {
        foreach (var model in models)
        {
            if (model.modelType != modelType)
                model.gameObject.SetActive(false);
            else
            { 
                model.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
                model.gameObject.SetActive(true);
            }
        }
    }

    public void ChangePreviewBallTo(Material material)
    {
        ball.material = material;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
