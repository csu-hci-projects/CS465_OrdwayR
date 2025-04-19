using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public Slider dialogueSlider;
    public Slider musicSlider;
    public Dropdown controlDropdown;

    void Update()
    {
        GameSettings.Instance.dialogueVolume = dialogueSlider.value;
        GameSettings.Instance.musicVolume = musicSlider.value;
        GameSettings.Instance.controlType = GetControlType();
        Debug.Log("Dialogue Volume: " + GameSettings.Instance.dialogueVolume);
        Debug.Log("Music Volume: " + GameSettings.Instance.musicVolume);
        Debug.Log("Control Type: " + GameSettings.Instance.controlType);
    }

    public void OnStartGame()
    {
        GameSettings.Instance.dialogueVolume = dialogueSlider.value;
        GameSettings.Instance.musicVolume = musicSlider.value;
        GameSettings.Instance.controlType = GetControlType();
        SceneManager.LoadScene(1);
    }

    public ControlType GetControlType()
    {
        return (ControlType)controlDropdown.value;
    }
}
