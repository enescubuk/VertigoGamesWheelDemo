using UnityEngine;

[CreateAssetMenu(fileName = "SliceData", menuName = "Wheel/SliceData")]
public class SliceData : ScriptableObject
{
    public string SliceName;
    public Sprite SliceSprite;
    public Rarity Rarity;
}

public enum Rarity
{
    Bronze,
    Silver,
    Gold,
    Death
}
