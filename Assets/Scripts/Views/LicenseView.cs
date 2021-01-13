using UnityEngine;
using UnityEngine.UI;

public class LicenseView : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private TextAsset textAsset;

    private void Start()
    {
        text.text = textAsset.text;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
