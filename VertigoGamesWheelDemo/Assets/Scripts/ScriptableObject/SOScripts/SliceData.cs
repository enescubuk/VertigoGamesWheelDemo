using UnityEngine;

[CreateAssetMenu(fileName = "SliceData", menuName = "Wheel/SliceData")]
public class SliceData : ScriptableObject
{
    public string sliceName;
    public Sprite sliceSprite;
    public Rarity rarity;
}

public enum Rarity
{
    Bronze,
    Silver,
    Gold,
    Death
}
