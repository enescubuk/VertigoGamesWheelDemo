using UnityEngine;
using UnityEngine.UI;

public class SliceBehavior : MonoBehaviour
{
    private SliceData sliceData;

    public void Initialize(SliceData data)
    {
        sliceData = data;
        transform.GetChild(0).GetComponent<Image>().sprite = sliceData.SliceSprite;
    }

    public SliceData GetSliceData()
    {
        return sliceData;
    }
}
