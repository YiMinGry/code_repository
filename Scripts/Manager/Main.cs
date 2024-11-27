using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.Playables;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class Main : Singleton<Main>
{
    private string saveFilePath;

    public Player player;
    public DayNightTimer dayNightTimer;
    public CamManager camManager;

    protected override void Awake()
    {
        base.Awake();

        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    private void Start()
    {
        dayNightTimer.SetTimeScale(500, 7, 0);
        SoundManager.Instance.Init();
        SoundManager.Instance.Play(SoundManager.mainBGM, SoundType.Bgm);

        UIHelper.Instance.InitFrontUI();

        Main.Instance.player.GetComponent<PlayerInputManager>().CursorLock(true);
        UIHelper.Instance.OpenUI(UIType.UITitle);

        InvokeRepeating(nameof(AutoSaveGame), 180f, 180f);
    }

    private async void AutoSaveGame()
    {
        UIHelper.Instance.uIAutoSave.Play();
        await SaveGameData(false);
    }

    public async void SaveGame(bool isLog = true)
    {
        await SaveGameData(isLog);
    }

    private async Task SaveGameData(bool isLog = true)
    {
        try
        {
            CharacterData _characterData = new CharacterData();
            _characterData.playerPosition = player.transform.position;
            _characterData.playerRotation = player.transform.rotation.eulerAngles;

            SettingsData _settingsData = new SettingsData();
            _settingsData.bgmVolume = SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume;
            _settingsData.effectVolume = SoundManager.Instance.GetAudioSource(SoundType.Effect).volume;
            _settingsData.timeScale = dayNightTimer.timeScale;

            EquipmentData _equipmentData =  EquipmentManager.Instance.SaveData();
            FishingData _fishingData = FishingManager.Instance.SaveData();

            InventoryData _inventoryData = InventoryManager.Instance.SaveData();
            MiniGameData _miniGameData = new MiniGameData();

            GameSaveData data = new GameSaveData
            {
                characterData = _characterData,
                equipmentData = _equipmentData,
                inventoryData = _inventoryData,
                settingsData = _settingsData,
                fishingData = _fishingData,
                miniGameData = _miniGameData
            };

            string jsonData = JsonUtility.ToJson(data, true);

            await File.WriteAllTextAsync(saveFilePath, jsonData);

            Debug.Log("���� ���" + saveFilePath);

            if (isLog)
            {
                UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                "���� �����Ͱ� ����Ǿ����ϴ�.");
            }
        }
        catch (System.Exception e)
        {
            if (isLog)
            {
                UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                "���� ����: " + e.Message);
            }
        }
    }

    public GameSaveData LoadGame(bool isLog = true)
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string jsonData = File.ReadAllText(saveFilePath);

                GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsonData);
                player.SetPosition(data.characterData.playerPosition, data.characterData.playerRotation);

                SoundManager.Instance.GetAudioSource(SoundType.Bgm).volume = data.settingsData.bgmVolume;
                SoundManager.Instance.GetAudioSource(SoundType.Effect).volume = data.settingsData.effectVolume;
                dayNightTimer.SetTimeScale(data.settingsData.timeScale);

                EquipmentManager.Instance.LoadFromFile(data.equipmentData);
                FishingManager.Instance.LoadFromFile(data.fishingData);
                InventoryManager.Instance.LoadFromFile(data.inventoryData);

                if (isLog)
                {
                    UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                    "���� �����͸� �ҷ��Խ��ϴ�.");
                }

                return data;
            }
            catch (System.Exception e)
            {
                if (isLog)
                {
                    UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                    "�ҷ����� ����: " + e.Message);
                }
            }
        }
        else
        {
            if (isLog)
            {
                UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                "����� �����Ͱ� �����ϴ�.");
            }
        }

        return null;
    }

    public void DeleteSaveData(bool isLog = true)
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                File.Delete(saveFilePath);

                if (isLog)
                {
                    UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                    "����� �����Ͱ� �����Ǿ����ϴ�.");
                }

            }
            catch (System.Exception e)
            {
                if (isLog)
                {
                    UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                    "������ ���� ����: " + e.Message);
                }
            }
        }
        else
        {
            if (isLog)
            {
                UIHelper.Instance.uILog.Add(UIHelper.Instance.GetIcon("Info"),
                "������ �����Ͱ� �����ϴ�.");
            }
        }
    }
}
