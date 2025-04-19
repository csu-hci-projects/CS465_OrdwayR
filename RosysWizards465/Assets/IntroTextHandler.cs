using UnityEngine;
using UnityEngine.UI;

public class IntroTextHandler : MonoBehaviour
{
    public Text introText;

    void Start()
    {
        // Set the initial text to be empty
        introText.text = "Welcome to Rosy’s class for aspiring wizards!" +
        "\r\nToday you’ll learn how to cast spells using" +
        "\r\n" + GameSettings.Instance.controlType.ToString() +
        "\r\nWe’ll start with the basics of weaving—simple inputs that form the foundation of all spellcasting. " +
        "\r\n" +
        "\r\nReady to begin?";


    }

    // Update is called once per frame
    void Update()
    {

    }
}
