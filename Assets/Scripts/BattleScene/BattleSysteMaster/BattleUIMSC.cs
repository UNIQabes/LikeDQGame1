using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIMSC : MonoBehaviour
{
    const int SkillSelBKind = 0;
    const int TargetSelBKind = 1;
    const int WatchUnitSelBKind = 2;

    private GameObject CanvasObj;
    private BattleSystem battleSystem;
    private ScriptsRunOrderSC scriptsRunOrderSC;

    private int WatchuOrder;
    private int SelSkill;
    private int SelTarget;

    private GameObject MainMenu;
    private GameObject WatchUSelMenu;
    private GameObject SkillSelMenu;
    private GameObject TargetSelMenu;
    private GameObject[][] ButtonObjs;
    private BattleUIButtonSC[][] battleUIButtonSCs;

    private List<UnitSC> unitSCs;
    private List<int> AliveComOrder;
    private List<int> AliveEnOrder;
    private List<int> DeadComOrder;
    private List<int> AliveUnitOrder;
    private List<int> DeadUnitOrder;
    private int[][] UnitCommandList;//[UnitOrder][n]n=0:スキル n=1:対象 

    private int SelWatchUnitOrder;
    private int SelSkillID;
    private int SelTargetOrder;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public関数

    public void SetValue()
    {
        CanvasObj = GM.CanvasObj;
        MainMenu = GM.MainMenu;
        WatchUSelMenu = GM.WatchUSelMenu;
        SkillSelMenu = GM.SkillSelMenu;
        TargetSelMenu = GM.TargetSelMenu;
        ButtonObjs = GM.ButtonObjs;
        battleUIButtonSCs = GM.BattleUIButtonSCs;
        battleSystem = GM.battleSystem;
        unitSCs = GM.unitSCs;
        scriptsRunOrderSC = GM.scriptsRunOrderSC;
    }

    public void WhenBattleStart()
    {
        SetValue();
    }

    public void WhenTurnStart()
    {
       
        SetUnitOrders();
        InitUnitCommandList();
        MakeMainM();
    }

    

    //0:SkillSelButtons 1:TargetSelButtons 2:WatchUnitSelButtons
    public void WhenClickedUIButton(int AssignedInfo,int KindNum)
    {
        switch (KindNum)
        {
            case SkillSelBKind:
                WhenClickedSkillSelButton(AssignedInfo);
                break;
            case TargetSelBKind:
                WhenClickedTargetSelButton(AssignedInfo);
                break;
            case WatchUnitSelBKind:
                WhenClickedWatchUSelButton(AssignedInfo);
                break;
        }
            
    }

    public void WhenClickedSelectActButton()
    {
        MakeWatchM();
    }

    public void WhenClickedActStartButton()
    {
        
        scriptsRunOrderSC.WhenActStart();
    }

    public void WhenActStart()
    {
        battleSystem.SetUnitCommandList(UnitCommandList);
        CloseMenu();
    }

    public void WhenClickedWatchUSelButton(int WatchUnitOrder)
    {
        SelWatchUnitOrder = WatchUnitOrder;
        MakeSkillSelMenu(WatchUnitOrder);
    }

    public void WhenClickedSkillSelButton(int SkillID)
    {
        SelSkillID = SkillID;
        if(GM.SkSC.Datas[SkillID].RANGE()!= RangeEnum.AllAliveFoes)
        {
            MakeTargetSelMenu(SkillID);
        }
        else
        {
            SetUnitCommand(SelWatchUnitOrder, SelSkillID,0);
            MakeMainM();
        }
    }

    public void WhenClickedTargetSelButton(int TargetUnitOrder)
    {
        SelTargetOrder = TargetUnitOrder;
        SetUnitCommand(SelWatchUnitOrder, SelSkillID, SelTargetOrder);
        MakeMainM();
    }

    public void WhenClickedBackButton()
    {
        
    }
    

    //private関数

    private void SetInfoToBattleUIButton(BattleUIButtonSC ArgBattleUIButtonSC, string SetText, int AssignedInfo)
    {
        ArgBattleUIButtonSC.SetAssignedInfo(AssignedInfo);
        ArgBattleUIButtonSC.SetText(SetText);
    }


    private void SetObjsActive(GameObject[] ArgObjs,bool SetState)
    {
        foreach (GameObject AObj in ArgObjs)
        {
            if (AObj.activeSelf == !SetState)
            {
                AObj.SetActive(SetState);
            }
        }
    }

    private void InitUnitCommandList()
    {
        UnitCommandList = new int[unitSCs.Count][];
        for (int i = 0; i < unitSCs.Count; i++)
        {
            UnitCommandList[i] = new int[2] {-1,1};//-1はAIに判断を委ねる
        }
    }

    private void SetUnitCommand(int WatchUOrder,int SkillID,int TargetOrder)
    {
        UnitCommandList[WatchUOrder][0] = SkillID;
        UnitCommandList[WatchUOrder][1] = TargetOrder;
    }
    


    private void MakeMainM()
    {
        CloseMenu();
        MainMenu.SetActive(true);

    }

    private void MakeWatchM()
    {
        CloseMenu();
        WatchUSelMenu.SetActive(true);
        SetUnitOrders();
        SetObjsActive(GM.ButtonObjs[WatchUnitSelBKind], false);

        for (int i = 0; i < AliveComOrder.Count; i++)
        {
            ButtonObjs[WatchUnitSelBKind][i].SetActive(true);
            SetInfoToBattleUIButton(battleUIButtonSCs[WatchUnitSelBKind][i], GM.StSC.Datas[unitSCs[AliveComOrder[i]].ID].Name(), AliveComOrder[i]);
        }
    }

    private void MakeSkillSelMenu(int UnitOrder)
    {
        CloseMenu();
        SkillSelMenu.SetActive(true);
        SetObjsActive(GM.ButtonObjs[SkillSelBKind], false);
        for (int i=0;unitSCs[UnitOrder].SkillList.Length>i;i++)
        {
            ButtonObjs[SkillSelBKind][i].SetActive(true);
            SetInfoToBattleUIButton(battleUIButtonSCs[SkillSelBKind][i], GM.SkSC.Datas[unitSCs[UnitOrder].SkillList[i]].Name(), unitSCs[UnitOrder].SkillList[i]);
        }
    }

    private void MakeTargetSelMenu(int SKillID)
    {
        List<int> TargetUOrder=new List<int>();
        CloseMenu();
        TargetSelMenu.SetActive(true);
        SetObjsActive(GM.ButtonObjs[TargetSelBKind], false);
        switch (GM.SkSC.Datas[SKillID].RANGE())//1:敵単体　2:敵全体
        {
            case RangeEnum.OneAliveFoe:
                TargetUOrder = AliveEnOrder;
                break;
            case RangeEnum.AllAliveFoes:
                TargetUOrder = AliveEnOrder;
                break;
            case RangeEnum.OneAliveAlly:
                TargetUOrder = AliveComOrder;
                break;
            case RangeEnum.AllAliveAllies:
                TargetUOrder = AliveComOrder;
                break;
            case RangeEnum.OneDeadAlly:
                TargetUOrder = DeadComOrder;
                break;
            case RangeEnum.AllDeadAllies:
                TargetUOrder = DeadComOrder;
                break;

        }

        for (int i=0;i< TargetUOrder.Count; i++)
        {
            ButtonObjs[TargetSelBKind][i].SetActive(true);
            SetInfoToBattleUIButton(battleUIButtonSCs[TargetSelBKind][i], GM.StSC.Datas[unitSCs[TargetUOrder[i]].ID].Name(), TargetUOrder[i]);
        }

    }

    private void CloseMenu()
    {
        GameObject[] BattleUIs = new GameObject[] { MainMenu,WatchUSelMenu , SkillSelMenu , TargetSelMenu };
        foreach (GameObject BattleUIObj in BattleUIs)
        {
            if (BattleUIObj.activeSelf)
            {
                BattleUIObj.SetActive(false);
            }
        }
    }

    private void SetUnitOrders()
    {
        AliveComOrder = battleSystem.GetAliveComOrder();
        AliveEnOrder = battleSystem.GetAliveEnOrder();
        DeadComOrder = battleSystem.GetDeadComOrder();
    }



    

    
}
