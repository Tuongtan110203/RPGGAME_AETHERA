using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;
    public string itemName;
    public Sprite icon;
    public string itemId;
    [Range(0, 100)]
    public float dropChange;

    protected  StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
         string path = AssetDatabase.GetAssetPath(this);
         itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }

}
