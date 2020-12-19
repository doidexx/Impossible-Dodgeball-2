using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CanvasS[] canvasses = null;
    [Header("Preview Renderers")]
    public Renderer ball = null;
    public ModelControl[] models = null;
    public Sprite selectedSprite = null;
    [Header("Buttons")]
    public SkinButton selectedButton = null;
    public SkinButton selectedBallButton = null;
    [Header("Sliders")]
    public Slider musicSlider = null;
    public Slider SFXSlider = null;

    AudioController audioController = null;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        if (musicSlider == null || SFXSlider == null)
            return;
        var holder = FindObjectOfType<DataHolder>();
        musicSlider.value = holder.musicVolume;
        SFXSlider.value = holder.SFXVolume;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }

    public void OpenCanvas(string name)
    {
        if (name == CanvasName.Main.ToString() && Time.timeScale != 0)
            FindObjectOfType<DataHolder>().SaveData();

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

    public void ChangeMusicVolume(Slider slider)
    {
        audioController.GetComponent<AudioSource>().volume = slider.value;
    }

    public void ChangeSFXVolume(Slider slider){
        audioController.SFX_Volume = slider.value;
    }

    public void ChangePreviewBallTo(Material material)
    {
        ball.material = material;
    }

    public void MarkButton(SkinButton button)
    {
        if (selectedButton != null && selectedButton != button)
            selectedButton.RemoveSelection();
        selectedButton = button;
    }

    public void MarkBallButton(SkinButton button)
    {
        if (selectedBallButton != null && selectedBallButton != button)
            selectedBallButton.RemoveSelection();
        selectedBallButton = button;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
