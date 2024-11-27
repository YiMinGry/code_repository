using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkin : UIBase
{
    [SerializeField] GameObject itemBase;
    [SerializeField] GameObject[] ScrollViews;
    [SerializeField] Transform[] content;

    private EquipmentType equipmentState;


    private void OnEnable()
    {
        foreach (GameItem data in ItemDatabase.Instance.LoadAllSkins())
        {
            UIItemBase item = Instantiate(itemBase, content[(int)data.equipmentType]).GetComponent<UIItemBase>();
            item.Set(data);

            Button button = item.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(
                () =>
                {
                    SoundManager.Instance.Play(SoundManager.click);

                    if (data.equipmentType == EquipmentType.Skin)
                    {
                        Material material = ItemDatabase.Instance.GetMaterial(data.itemName);
                        Main.Instance.player.SetMaterialSkin(material);
                    }
                    else if (data.equipmentType == EquipmentType.Emot)
                    {
                        Material material = ItemDatabase.Instance.GetMaterial(data.itemName);
                        Main.Instance.player.SetMaterialEmot(material);
                    }

                    EquipmentSlot slot = EquipmentManager.Instance.FindSlot(data.equipmentType);

                    if (slot.isEquip() && slot.equippedItem.itemName.Equals(data.itemName))
                    {
                        EquipmentManager.Instance.UnequipItem(data.equipmentType);

                        return;
                    }

                    if (slot.isEquip())
                    {
                        EquipmentManager.Instance.UnequipItem(data.equipmentType);
                    }

                    EquipmentManager.Instance.EquipItem(data.equipmentType, "Skin/" + data.itemName);
                }
                );
        }
    }

    private void Awake()
    {
        OnClickTabs(0);
    }

    public void OnClickTabs(int idx)
    {
        equipmentState = (EquipmentType)idx;

        for (int i = 0; i < ScrollViews.Length; i++)
        {
            ScrollViews[i].SetActive(i == idx);
        }
    }
}
