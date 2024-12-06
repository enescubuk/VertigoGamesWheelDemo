using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemBehavior : MonoBehaviour
{
    public void Initialize(SliceData data)
    {
        GetComponent<Image>().sprite = data.SliceSprite;
    }

    public void ChangeCounter(int counter)
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + counter.ToString();
    }
}
