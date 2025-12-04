using UnityEngine;


[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObject/ItemInformation", order = int.MinValue)]
public class ItemInformation : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    [TextArea]
    public string Explain;

    // 이하 SerializeField로 private화

}
