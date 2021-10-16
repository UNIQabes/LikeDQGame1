using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    const int ButtonKindNum = 3;

    public static GameObject ThisGameObject;
    public static GameObject CanvasObj;

    public static BattleUIMSC battleUIMSC;
    public static BattleSystem battleSystem;
    public static StartSetUpSC startSetUpSC;
    public static ScriptsRunOrderSC scriptsRunOrderSC;

    public static GameObject[] UnitObjs;
    public static List<UnitSC> unitSCs;
    public static StatusDataBaseSC StSC;
    public static SkillDataBaseSC SkSC;


    //BattleUIAssets
    public static GameObject MainMenu;
    public static GameObject WatchUSelMenu;
    public static GameObject SkillSelMenu;
    public static GameObject TargetSelMenu;

    public static GameObject ActStartButton;
    public static Button actStBButtonComp;
    public static ActStartButtonSC actStartButtonSC;

    public static GameObject SelectActButton;
    public static Button selectActBButtonComp;

    public static GameObject SkillSelMBackButton;
    public static GameObject TargetSelMBackButton;
    public static GameObject WatchUnitSelMBackButton;


    //0:SkillSelButtons 1:TargetSelButtons 2:WatchUnitSelButtons
    public static GameObject[][] ButtonObjs;
    public static Button[][] ButtonComps;
    public static Text[][] ButtonTextComps;
    public static BattleUIButtonSC[][] BattleUIButtonSCs;

    // Start is called before the first frame update
    void Start()
    {
        ThisGameObject = this.gameObject;

        GetDataBase();

        GetBattleUI();

        GetUnitsObjsAndSCs();

        GetObjs();

        GetSCs();
    }

    private void GetDataBase()
    {
        StSC = Resources.Load<StatusDataBaseSC>("DataBase/StatusDataBase");
        SkSC = Resources.Load<SkillDataBaseSC>("DataBase/SkillDataBase");
    }

    private void GetBattleUI()
    {
        CanvasObj = GameObject.Find("Canvas");
        MainMenu = CanvasObj.transform.Find("MainMenu").gameObject;
        WatchUSelMenu = CanvasObj.transform.Find("WatchUSelMenu").gameObject;
        SkillSelMenu = CanvasObj.transform.Find("SkillSelMenu").gameObject;
        TargetSelMenu = CanvasObj.transform.Find("TargetSelMenu").gameObject;

        ButtonObjs = new GameObject[ButtonKindNum][];
        ButtonComps = new Button[ButtonKindNum][];
        ButtonTextComps = new Text[ButtonKindNum][];
        BattleUIButtonSCs = new BattleUIButtonSC[ButtonKindNum][];
        ButtonObjs[0]= GameObject.FindGameObjectsWithTag("SkillSelButton");
        ButtonObjs[1]= GameObject.FindGameObjectsWithTag("TargetSelButton");
        ButtonObjs[2]= GameObject.FindGameObjectsWithTag("WatchUnitButton");
        ActStartButton = CanvasObj.transform.Find("MainMenu/Viewport/Content/ActStartButton").gameObject;
        SelectActButton = CanvasObj.transform.Find("MainMenu/Viewport/Content/SelectActButton").gameObject;
        


        GetButtonsReferences();
    }

    //Button関連
    private void GetButtonsReferences()
    {
        actStBButtonComp = ActStartButton.GetComponent<Button>();
        selectActBButtonComp = SelectActButton.GetComponent<Button>();
        for (int i=0;i< ButtonKindNum;i++)
        {
            
            ButtonComps[i] = GetButtonsButtonSC(ButtonObjs[i]);
            ButtonTextComps[i] = GetButtonsTextSC(ButtonObjs[i]);
            BattleUIButtonSCs[i] = GetButtonsBattleUIButtonSC(ButtonObjs[i]);
        }
    }

    private Button[] GetButtonsButtonSC(GameObject[] ArgObj)
    {
        Button[] ReturnValue= new Button[ArgObj.Length];

        for (int i=0;i< ArgObj.Length;i++)
        {
            ReturnValue[i] = ArgObj[i].GetComponent<Button>();
        }
        return ReturnValue;
    }

    private BattleUIButtonSC[] GetButtonsBattleUIButtonSC(GameObject[] ArgObj)
    {
        BattleUIButtonSC[] ReturnValue = new BattleUIButtonSC[ArgObj.Length];

        for (int i = 0; i < ArgObj.Length; i++)
        {
            ReturnValue[i] = ArgObj[i].GetComponent<BattleUIButtonSC>();
        }
        return ReturnValue;
    }

    private Text[] GetButtonsTextSC(GameObject[] ArgObj)
    {
        Text[] ReturnValue = new Text[ArgObj.Length];

        for (int i = 0; i < ArgObj.Length; i++)
        {
            ReturnValue[i] = ArgObj[i].transform.GetChild(0).gameObject.GetComponent<Text>();
        }
        return ReturnValue;
    }
    //(fin)Button関連

    private void GetUnitsObjsAndSCs()
    {
        UnitObjs = GameObject.FindGameObjectsWithTag("Unit");
        unitSCs = new List<UnitSC>();
        foreach (GameObject unitobj in UnitObjs)
        {
            unitSCs.Add(unitobj.GetComponent<UnitSC>());
        }
    }

    private void GetObjs()
    {
       
    }

    private void GetSCs()
    {
        battleSystem = ThisGameObject.GetComponent<BattleSystem>();
        battleUIMSC = ThisGameObject.GetComponent<BattleUIMSC>();
        startSetUpSC = ThisGameObject.GetComponent<StartSetUpSC>();
        scriptsRunOrderSC = ThisGameObject.GetComponent<ScriptsRunOrderSC>();






    }
}
