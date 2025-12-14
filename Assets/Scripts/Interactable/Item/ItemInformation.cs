using UnityEngine;


[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObject/ItemInformation", order = int.MinValue)]
public class ItemInformation : ScriptableObject
{
    public InventoryListType ItemType;
    public int ItemID;

    public Sprite Sprite;
    public GameObject ItemPrefab;
    
    public string ItemName;
    [TextArea]
    public string ItemContext;

    // 이하 SerializeField로 private화

}
