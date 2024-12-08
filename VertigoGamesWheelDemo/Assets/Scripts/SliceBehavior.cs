using UnityEngine;
using UnityEngine.UI;

public class SliceBehavior : MonoBehaviour
{
    private SliceData sliceData;

    public void Initialize(SliceData _data)
    {
        sliceData = _data;
        transform.GetChild(0).GetComponent<Image>().sprite = sliceData.SliceSprite;
    }

    public SliceData GetSliceData()
    {
        return sliceData;
    }
}
