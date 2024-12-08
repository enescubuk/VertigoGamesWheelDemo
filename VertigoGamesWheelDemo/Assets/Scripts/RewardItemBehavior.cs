using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardItemBehavior : MonoBehaviour
{
    private SliceData sliceData;
    private int counter = 0;

    public void Initialize(SliceData _data)
    {
        sliceData = _data;
        GetComponent<Image>().sprite = sliceData.SliceSprite;
    }

    public void ChangeCounter(int newCounter)
    {
        counter = newCounter;
        transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + counter.ToString(); 
    }

    public int GetCounter()
    {
        return counter;
    }

    public SliceData GetSliceData()
    {
        return sliceData;
    }
}
