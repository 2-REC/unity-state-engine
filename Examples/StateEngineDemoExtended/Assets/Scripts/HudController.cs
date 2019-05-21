using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public Text livesText;
    public Text pointsText;
    public Slider healthSlider;

    public Image flashImage;

    float flashSpeed = 5.0f; // can be set, but value is lost as soon as calling "Flash"

    public Image fadeImage;
    public float fadeSpeed = 5.0f;
    public Color fadeColour = new Color(0.0f, 0.0f, 0.0f, 1.0f); //?


    Color targetColour;

/*
    void Awake() {
    }
*/

    public void InitLifebar(int value, int maxValue) {
        healthSlider.maxValue = maxValue;
        healthSlider.value = value;
    }

    public void SetLifebar(int value) {
        healthSlider.value = value;
    }

    public void SetLives(int value) {
        livesText.text = value.ToString();
    }

    public void SetPoints(int value) {
// TODO: could specify a specific format for the text ...
        pointsText.text = value.ToString();
    }

    public void Flash(Color colour, float speed) {
        flashSpeed = speed;
        flashImage.color = colour;
        StartCoroutine("FlashFade");
    }

    IEnumerator FlashFade() {
        while (flashImage.color != Color.clear) {
            flashImage.color = Color.Lerp(flashImage.color, Color.clear, flashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void FadeIn() {
        //// HACK: to allow image to have alpha=0 in editor (& not hide everything...)
        Color color = fadeImage.color;
        color.a = 1.0f;
        fadeImage.color = color;
        ////

        targetColour = Color.clear;
        StartCoroutine("Fade");
    }

    public void FadeOut() {
        targetColour = fadeColour;
        StartCoroutine("Fade");
    }

    IEnumerator Fade() {
        while (fadeImage.color != targetColour) {
            fadeImage.color = Color.Lerp(fadeImage.color, targetColour, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public float Begin() {
        FadeIn();
        return fadeSpeed;
    }

    public float End(bool success) {
/*
        if (success) {
        }
        else {
        }
*/
        FadeOut();
        return fadeSpeed;
    }

}
