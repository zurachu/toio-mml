using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using toio;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private LayoutGroup layoutGroup;
    [SerializeField] private ToioCubeMmlItem toioCubeMmlItemPrefab;
    [SerializeField] private LicenseView licenseViewPrefab;
    [SerializeField] private List<Color> colors;

    private CubeManager cubeManager;
    private List<ToioCubeMmlItem> toioCubeItems;

    private void Start()
    {
        cubeManager = new CubeManager();
        toioCubeItems = new List<ToioCubeMmlItem>();
    }

    public async void OnClickConnect()
    {
        if (colors.Count <= toioCubeItems.Count)
        {
            return;
        }

        var cube = await cubeManager.SingleConnect();
        if (cube == null)
        {
            return;
        }

        var item = Instantiate(toioCubeMmlItemPrefab, layoutGroup.transform);
        var index = toioCubeItems.Count;
        item.Initialize(index, cube, colors[index]);
        toioCubeItems.Add(item);
    }

    public void OnClickPlay()
    {
        toioCubeItems.ForEach(_item => _item.Play());
    }

    public void OnClickLicense()
    {
        Instantiate(licenseViewPrefab, layoutGroup.transform.parent);
    }
}
