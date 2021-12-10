using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Player Player;
    public int currentLevelNum = 0;
    [SerializeField]
    private GameObject[] UiElements;
    public GameObject LogInMenu;
    public GameObject LevelGUI;
    private bool IsLevelGUI = false;
    private GameProcess gameProcess = new GameProcess();

    public GameObject GamePrefab;
    public List<GameObject> LevelDetails;
    /*УРОВНИ*/
    public GameObject LevelPrefab;
    public LevelManager currentLevel;
    public LevelManager MainLevel;
    public List<string> LevelsName;
    public BtnClick BtnTestClick;
    public List<BtnClick> LvlBtns = new List<BtnClick>();

    /*ОПЦИОНАЛ*/
    [SerializeField]
    private bool telegramDebug;
    [SerializeField]
    private bool musicOn;

    [SerializeField]
    private Transform BtnsTransform;
    [SerializeField]
    private GameObject BtnPrefab;

    public Material[] Materials;
    [SerializeField]
    private List<AudioClip> audioClips;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private Image LoadingMenuPanel;
    [SerializeField]
    private Text LoadingMenuText;

    private void Start()
    {
        //загрузить данные прогресса
        {
            LevelsName = LevelModule.GetLevels();

            if (GameProcess.IsHas)
            {
                gameProcess = GameProcess.LoadProcess();
                LogIn(gameProcess.Name);
            }
            else
            {
                GameProcess.SaveProcess(gameProcess);
            }
        }
        //инициализация настроек
        {
            TelegramBot.telegramDebug = telegramDebug;
        }
        //музыка
        if(musicOn)
        {
            StartCoroutine(MusicPlay());
            IEnumerator MusicPlay()
            {
                int index = 0;
                while (true)
                {
                    AudioSource.clip = audioClips[index];
                    AudioSource.Play();
                    yield return new WaitForSeconds(AudioSource.clip.length);
                    index = (index++) % audioClips.Count;
                }
            }
        }
        //инициализация плеера 
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Player.OnControl();
        }
        //инициализация кнопок
        {
            InitBtnsForStartLevel();
        }
        //загрузка стартового уровня
        {
            MainLevel.Init();
            LoadMainMenuLevel();
        }
        //Test
        {
            BtnTestClick.EnableBtn();
            TestLevel.Init();
        }

    }
    private void LogIn(string name)
    {
        gameProcess.Name = name;
        Save();
        GamePrefab.SetActive(true);
        LogInMenu.SetActive(false);
    }
    public void Save()
    {
        GameProcess.SaveProcess(gameProcess);
        LogInMenu.SetActive(false);
    }
    public void LoadLevel(int num)
    {
        OpenLoadingMenu();


        TelegramBot.SendMessage(gameProcess.Name, $"Загружен уровень {num}");

        currentLevel = CreateLevel(LevelsName[num]);
        currentLevel.Init();

        MoveOnCurrentLevel();
        Player.OnControl();

    }
    public void LoadMainMenuLevel()
    {
        MoveOnPosition(MainLevel.StartPoint.position);
    }
    private void MoveOnCurrentLevel()
    {
        MoveOnPosition(currentLevel.StartPoint.position);
    }
    private void MoveOnPosition(Vector3 position)
    {
        OpenLoadingMenu();
        Vector3 tppoint = position;
        Player.transform.position = tppoint;
        Player.StopImpulse();
    }
    public void LevelSuccess()
    {
        if (currentLevelNum - 1 >= 0)
        {
            LvlBtns[currentLevelNum - 1].EnableBtn();
        }

        if (!IsLevelGUI)
        {
            LevelGUI.SetActive(true);
            currentLevelNum++;
            if (currentLevelNum >= LevelsName.Count)
            {
                currentLevelNum = 0;
            }
        }
        Player.OffControl();
        IsLevelGUI = true;


        gameProcess.LevelSuccessful(currentLevelNum);


    }
    public void SwitchLevel()
    {
        IsLevelGUI = false;
        LevelGUI.SetActive(false);

        LoadLevel(currentLevelNum);
    }
    public void ExitLevel()
    {
        LoadMainMenuLevel();
    }
    public void LogIn_Input()
    {
        string name = UiElements[0].transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text;
        LogIn(name);
    }
    public void RestartLevel()
    {
        LoadLevel(currentLevelNum);
    }
    private void InitBtnsForStartLevel()
    {
        Vector3 StartVector = new Vector3(0, 5, 0);
        for (int i = 0; i < LevelsName.Count; i++)
        {
            GameObject BtnObj = Instantiate(BtnPrefab, BtnsTransform);
            BtnClick btnClick = BtnObj.GetComponent<BtnClick>();
            LvlBtns.Add(btnClick);

            BtnObj.transform.position = StartVector + new Vector3(i * 5, 0, 0);
            bool active = (i <= gameProcess.LevelSuccess);

            btnClick.InitBtn(i, active);
        }
    }

    private void OpenLoadingMenu(int time = 1)
    {
        LoadingMenuPanel.gameObject.SetActive(true);
        LoadingMenuPanel.canvasRenderer.SetAlpha(1);
        LoadingMenuText.canvasRenderer.SetAlpha(1);
        Invoke(nameof(CloseLoadingMenu), time);
    }
    public void CloseLoadingMenu()
    {
        float time1 = 1;

        LoadingMenuPanel.CrossFadeAlpha(0, 1, false);
        LoadingMenuText.CrossFadeAlpha(0, 1, false);
        Invoke(nameof(FullCloseLoadingMenu), time1);

    }
    public void FullCloseLoadingMenu()
    {
        LoadingMenuPanel.gameObject.SetActive(false);
    }

    [Space]
    [Header("XML Created Level")]
    [SerializeField]
    private LevelManager TestLevel;
    public void CreatedXMLLevel()
    {
        Debug.Log("XML file has ben created");
        LevelModule LevelModule = TestLevel.GetDetailsList();
        LevelModule.SaveLevel(LevelModule);
    }
    public LevelManager CreateLevel(string nameLevel)
    {
        if (currentLevel)
        {
            Destroy(currentLevel.gameObject);
        }

        GameObject gameObject = Instantiate(LevelPrefab, GamePrefab.transform);
        LevelManager levelManager = gameObject.GetComponent<LevelManager>();


        //наполнение уровня
        LevelModule LevelModule = LevelModule.LoadLevel(nameLevel);

        foreach (DetailLevelModule item in LevelModule.Details)
        {
            try
            {

                string name = LevelDetails[item.Id].name;
                GameObject detailGameObject = Instantiate(LevelDetails[item.Id], levelManager.transform);
                detailGameObject.transform.SetPositionAndRotation(new Vector3(item.Transform.Position.X, item.Transform.Position.Y, item.Transform.Position.Z), new Quaternion(item.Transform.Rotation.X, item.Transform.Rotation.Y, item.Transform.Rotation.Z, item.Transform.Rotation.W));
                detailGameObject.transform.localScale = new Vector3(item.Transform.Size.X, item.Transform.Size.Y, item.Transform.Size.Z);
                detailGameObject.name = name;
            }
            catch
            {
                Debug.LogError($"Деталь под id:{item.Id} не была найденна");
            }
        }

        return levelManager;

    }

    public void MoveForTestLevel()
    {
        MoveOnPosition(TestLevel.StartPoint.position);
    }
}

