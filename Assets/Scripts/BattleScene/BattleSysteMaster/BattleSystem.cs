using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    private bool CanStartBattle=false;

    private BattleUIMSC battleUIMSC;

    private List<UnitSC> unitSCs=new List<UnitSC>();
    private List<int> UnitIDs = new List<int>();
    private List<int> ConIDs;
    private List<int> EnIDs;
    private int[][] UnitCommandList;//[UnitOrder][n]n=0:スキル n=1:対象 
    private List<int> OrderSortedSPD;

    private WinOrLoseEnum WinOrLose;


    // Start is called before the first frame update
    void Start()
    {

    }

    


    //private関数
    private void MoveUnit(int UnitOrder, int TargetOrder, int SkillID)
    {
        Debug.Log(GM.StSC.Datas[unitSCs[UnitOrder].ID].Name() + "は" + GM.SkSC.Datas[SkillID].Name() + "をはなった");
        if (GM.SkSC.Datas[SkillID].MP()<=unitSCs[UnitOrder].CurMP)
        {
            if (!(unitSCs[UnitOrder].GetIsDead()))
            {
                List<int> TargetOrders = MakeTargetOrdersList(UnitOrder, TargetOrder, SkillID);
                
                foreach (int ATargetOrder in TargetOrders)
                {
                    unitSCs[ATargetOrder].WhenHitSkill(CalculateSkillAcValue(UnitOrder, ATargetOrder, SkillID));
                }

                if (TargetOrders.Count == 0)
                {
                    Debug.Log("しかし　こうかは　なかった");
                }
            }
            else
            {
                Debug.Log(GM.StSC.Datas[unitSCs[UnitOrder].ID].Name() + "は　しんでいる");
            }
        }
        else
        {
            Debug.Log("しかし　MPが　たりない！");
        }
        
    }

    private List<int> MakeTargetOrdersList(int UnitOrder, int TargetOrder, int SKillID)
    {
        
        List<int> TargetOrders=new List<int>();

        switch (GM.SkSC.Datas[SKillID].RANGE())
        {
            case RangeEnum.OneAliveFoe:
                TargetOrders.Add(TargetOrder);
                break;

            case RangeEnum.AllAliveFoes:
                if (unitSCs[UnitOrder].IsEnemy)
                {
                    TargetOrders = GetAliveComOrder();
                }
                else
                {
                    TargetOrders = GetAliveEnOrder();
                }
                break;

            case RangeEnum.OneAliveAlly:
                TargetOrders.Add(TargetOrder);
                break;

            case RangeEnum.AllAliveAllies:
                if (unitSCs[UnitOrder].IsEnemy)
                {
                    TargetOrders = GetAliveEnOrder();
                }
                else
                {
                    TargetOrders = GetAliveComOrder();
                }
                break;

            case RangeEnum.OneDeadAlly:
                TargetOrders.Add(TargetOrder);
                break;

            case RangeEnum.AllDeadAllies:
                if (unitSCs[UnitOrder].IsEnemy)
                {
                    TargetOrders = GetDeadEnOrder();
                }
                else
                {
                    TargetOrders = GetDeadComOrder();
                }
                break;
        }

        

        return TargetOrders;
    }

    private void ActOnSystem()
    {
        List<int> OrdersSortedSPD = MakeOrderList(SortSCBySPD(unitSCs), true, true, false, false);
        int UnitOrder;
        Debug.Log("OrdersSortedSPDのCountは"+ OrdersSortedSPD.Count);
        for (int i = 0; i < OrdersSortedSPD.Count; i++)
        {
            UnitOrder = OrdersSortedSPD[i];
            MoveUnit(UnitOrder, UnitCommandList[UnitOrder][1], UnitCommandList[UnitOrder][0]);
            WinOrLose = JudgeWinOrLose();
        }
    }

    private int CalculateSkillAcValue(int AttackerOrder, int TargetOrder, int SkillID)
    {
        int ReturnValue;
        ReturnValue = (int)(GM.SkSC.Datas[SkillID].DAMAGE() * unitSCs[AttackerOrder].EffectedADSI()[0] / unitSCs[TargetOrder].EffectedADSI()[1]);
        return ReturnValue;
    }

    

    //順番が速いほど素早さが高い順にソートしていく
    private List<UnitSC> SortSCBySPD(List<UnitSC> OriUnitSCs)
    {
        List<UnitSC> ReturnList= new List<UnitSC>();
        List<UnitSC> TargetListSC=new List<UnitSC>();
        List<float> TargetListSpd= new List<float>();
        int IndexOfBig;
        
        foreach(UnitSC aunitSC in OriUnitSCs)
        {
            TargetListSC.Add(aunitSC);
            TargetListSpd.Add(aunitSC.EffectedADSI()[2]);
            
        }
        
        for (int i=0;i < OriUnitSCs.Count; i++)//iは0からUnitOriOrd.count-1まで
        {
            IndexOfBig = 0;
            

            for (int j = 1; j < TargetListSC.Count;j++) //jは1からTargetListSC.count - 1まで
            {
                if(TargetListSpd[IndexOfBig]<TargetListSpd[j])
                {
                    IndexOfBig = j;
                }
                
            }

            ReturnList.Add(TargetListSC[IndexOfBig]);
            TargetListSpd.RemoveAt(IndexOfBig);
            TargetListSC.RemoveAt(IndexOfBig);
        }
        
        return ReturnList;
    }

    

    private void InitUnitStatus()
    {
        foreach (UnitSC aunitSC in unitSCs)
        {
            aunitSC.InitHP();
            aunitSC.CheckIsDead();
            aunitSC.InitPassive();
        }
    }


    private List<int> MakeOrderList(List<UnitSC> ArgList, bool AddComp, bool AddEnemy, bool AddAlive, bool AddDead)
    {
        List<int> ReturnaValue = new List<int>();

        for (int i = 0; i < ArgList.Count; i++)
        {
            if (!ArgList[i].IsEnemy& AddComp)
            {

                ReturnaValue.Add(i);
                
            }
            else if (ArgList[i].IsEnemy& AddEnemy)
            {
                ReturnaValue.Add(i);
            }
            else if (!ArgList[i].GetIsDead()& AddAlive)
            {
                ReturnaValue.Add(i);
            }
            else if (ArgList[i].GetIsDead()& AddDead)
            {
                ReturnaValue.Add(i);
            }

        }
        return ReturnaValue;
    }

    private void InitWinOrLose()
    {
        WinOrLose = WinOrLoseEnum.DuringBattle;
    }

    private WinOrLoseEnum JudgeWinOrLose()
    {
        WinOrLoseEnum ReturnValue;
        if (GetAliveEnOrder().Count == 0 & GetAliveComOrder().Count == 0)
        {
            ReturnValue = WinOrLoseEnum.Draw;

        }
        else if (GetAliveEnOrder().Count == 0)
        {

            ReturnValue = WinOrLoseEnum.CompanionWin;

        }
        else if (GetAliveComOrder().Count == 0)
        {

            ReturnValue = WinOrLoseEnum.EnemyWin;

        }
        else
        {
            ReturnValue = WinOrLoseEnum.DuringBattle;
        }
        return ReturnValue;
    }


    //AI系
    private void AIFillCommandList()
    {
        for (int i=0;i< UnitCommandList.Length;i++)
        {
            if (UnitCommandList[i][0] == -1)
            {
                AISetAUnitCommand(i);
            }
        }
    }

    private void AISetAUnitCommand(int UnitOrder)
    {
        int TargetUOrder;
        int SkillID;
        TargetUOrder= GetLowestHPFoeOrder(UnitOrder);
        SkillID = GetHighestDamageSkill(UnitOrder, TargetUOrder);
        UnitCommandList[UnitOrder][0] = SkillID;
        UnitCommandList[UnitOrder][1] = TargetUOrder;
    }
    

    private int GetLowestHPFoeOrder(int UnitOrder)
    {
        int ReturnValue=UnitOrder;
        int LowestValue=999;
        List<int> AliveFoeOrder;
        if (unitSCs[UnitOrder].IsEnemy)
        {
            AliveFoeOrder = GetAliveComOrder();
        }
        else
        {
            AliveFoeOrder = GetAliveEnOrder();
        }

        foreach(int AOrder in AliveFoeOrder)
        {
            if (LowestValue > unitSCs[AOrder].CurHP)
            {
                LowestValue = unitSCs[AOrder].CurHP;
                ReturnValue = AOrder;
            }
        }

        return ReturnValue;
    }

    private int GetHighestDamageSkill(int UnitOrder,int TargetOrder)
    {
        int ReturnValue=0;
        int HighestValue=0;
        int ADamageValue;
        foreach (int SkillID in unitSCs[UnitOrder].GetAvailableSkillList())
        {
            ADamageValue = CalculateSkillAcValue(UnitOrder, TargetOrder, SkillID);
            if (HighestValue<ADamageValue)
            {
                HighestValue = ADamageValue;
                ReturnValue = SkillID;
            }
        }

        return ReturnValue;
    }


    //public関数



    //When系
    public void WhenStartBattle()
    {
        SetValue();
        InitUnitStatus();
        InitWinOrLose();
    }

    public void WhenTurnStart()
    {
        
        
    }

    public void WhenActStart()
    {
        AIFillCommandList();
        OrderSortedSPD = MakeOrderList(SortSCBySPD(unitSCs), true, true, false, false);
        ActOnSystem();
    }

    public void WhenTurnEnd()
    {

    }

    public void WhenFightFinish()
    {

    }

    //GetOrder系
    public List<int> GetAliveComOrder()
    {
        List<int> ReturnValue = new List<int>();
        ReturnValue = MakeOrderList(unitSCs, true, false, false, false).Intersect<int>(MakeOrderList(unitSCs, false, false, true, false)).ToList<int>();
        return ReturnValue;
    }

    public List<int> GetAliveEnOrder()
    {
        List<int> ReturnValue = new List<int>();
        ReturnValue = MakeOrderList(unitSCs, false, true, false, false).Intersect<int>(MakeOrderList(unitSCs, false, false, true, false)).ToList<int>();
        return ReturnValue;
    }

    public List<int> GetDeadComOrder()
    {
        List<int> ReturnValue = new List<int>();
        ReturnValue = MakeOrderList(unitSCs, true, false, false, false).Intersect<int>(MakeOrderList(unitSCs, false, false, false, true)).ToList<int>();
        return ReturnValue;
    }

    public List<int> GetDeadEnOrder()
    {
        List<int> ReturnValue = new List<int>();
        ReturnValue = MakeOrderList(unitSCs, false, true, false, false).Intersect<int>(MakeOrderList(unitSCs, false, false, false, true)).ToList<int>();
        return ReturnValue;
    }

    public WinOrLoseEnum GetWinOrLose()
    {
        return WinOrLose;
    }

    public void SetUnitCommandList(int[][] ArgArray)
    {
        UnitCommandList = ArgArray;
    }
    public void SetValue()
    {
        unitSCs = GM.unitSCs;
        battleUIMSC = GM.battleUIMSC;
    }


}
public enum WinOrLoseEnum
{
    EnemyWin,
    CompanionWin,
    Draw,
    DuringBattle
}
