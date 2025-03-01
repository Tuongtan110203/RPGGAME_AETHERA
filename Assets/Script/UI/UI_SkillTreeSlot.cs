using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    private UI ui;
    private Image skillImage;
    private Color lockedSkillColor = new Color(109f / 255f, 86f / 255f, 86f / 255f);
    public bool unlocked;

    [SerializeField] private int skillCost;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;

    [SerializeField] private UI_SkillTreeSlot[] showBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] showBeLocked;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName; 
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillShot());
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;

        if (unlocked)
        {
            skillImage.color = Color.white;
        }
    }

    public void UnlockSkillShot()
    {
        if (!PlayerManager.instance.HaveEnoughMoney(skillCost)) return;
 

        for (int i = 0; i < showBeUnlocked.Length; i++)
        {
            if (showBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can not unlock skill");
                return;
            }
        }

        for (int i = 0; i < showBeLocked.Length; i++)
        {
            if (showBeLocked[i].unlocked == true)
            {

                Debug.Log("Can not unlock skill");
                return;
            }
        }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription, skillName, skillCost);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;

        }
    }

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName,out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }else
        {
            _data.skillTree.Add(skillName,unlocked);
        }

    }
}
