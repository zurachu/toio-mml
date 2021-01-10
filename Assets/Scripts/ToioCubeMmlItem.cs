using UnityEngine;
using UnityEngine.UI;
using toio;

public class ToioCubeMmlItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button playButton;

    private string PlayerPrefsKey => $"mml{index}";

    private int index;
    private Cube cube;
    private ToioCubeMmlPlayer toioCubeMmlPlayer;

    public void Initialize(int index, Cube cube, Color color)
    {
        this.index = index;
        this.cube = cube;
        UIUtility.TrySetColor(image, color);
        inputField.text = PlayerPrefs.GetString(PlayerPrefsKey);
        cube.TurnLedOn(ColorIntValue(color.r), ColorIntValue(color.g), ColorIntValue(color.b), 0);
    }

    public void Play()
    {
        toioCubeMmlPlayer = new ToioCubeMmlPlayer(cube);
        toioCubeMmlPlayer.Load(inputField.text);
        toioCubeMmlPlayer.Play();
    }

    public void OnValueChanged(string value)
    {
        PlayerPrefs.SetString(PlayerPrefsKey, value);
        playButton.interactable = !string.IsNullOrEmpty(value);
    }

    private void Update()
    {
        if (toioCubeMmlPlayer != null && toioCubeMmlPlayer.Playing)
        {
            toioCubeMmlPlayer.Update();
        }
    }

    private int ColorIntValue(float value)
    {
        return (int)Mathf.Clamp(value * 255, 0, 255);
    }
}
