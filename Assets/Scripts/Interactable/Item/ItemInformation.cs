using UnityEngine;


[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObject/ItemInformation", order = int.MinValue)]
public class ItemInformation : ScriptableObject
{
    public InventoryListType ItemType;
    public Sprite Sprite;
    public string ItemName;
    [TextArea]
    public string ItemContext;

    // 이하 SerializeField로 private화

}
