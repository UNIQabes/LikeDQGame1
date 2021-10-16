using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSC : MonoBehaviour
{
    public bool IsEnemy;
    private bool IsDead;
    public int ID;
    public int CurHP;
    public int CurMP;
    private int[] PaSADSI;
    private int[] PaTADSI;
    public int[] SkillList;
    


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsDead()
    {
        return IsDead;
    }
    public void InitPassive()
    {
        PaSADSI = new int[] { 1, 1, 1, 1 };
        PaTADSI = new int[] { 0, 0, 0, 0 };   
    }

    public void InitHP()
    {
        CurHP = GM.StSC.Datas[ID].MaxHP();
    }
    public void InitMP()
    {
        CurMP = GM.StSC.Datas[ID].MaxMP();
    }
   

    public float[] EffectedADSI()
    {
        float[] ReturnValue=new float[4];
        ReturnValue[0] = PaSADSI[0] * GM.StSC.Datas[ID].ATK();
        ReturnValue[1] = PaSADSI[1] * GM.StSC.Datas[ID].DEF();
        ReturnValue[2] = PaSADSI[2] * GM.StSC.Datas[ID].SPD();
        ReturnValue[3] = PaSADSI[3] * GM.StSC.Datas[ID].INTE();
        return ReturnValue;
    }

    public void CheckIsDead()
    {
        if (CurHP <= 0)
        {
            IsDead = true;
        }
        else
        {
            IsDead = false;
        }
    }

    public List<int> GetAvailableSkillList()
    {
        List<int> ReturnValue= new List<int>();
        foreach (int ASkillID in SkillList)
        {
            if (GM.SkSC.Datas[ASkillID].MP() >= CurMP)
            {
                ReturnValue.Add(ASkillID);
            }
        }
        return ReturnValue;
    }
    
    public void HitSkill(int ChangeValue)
    {
        Debug.Log(GM.StSC.Datas[ID].Name()+"はスキルの効果をうけました");
        CurHP = CurHP - ChangeValue;
        if (CurHP<0)
        {
            CurHP = 0;
        }
    }

    public void WhenHitSkill(int ChangeValue)
    {
        HitSkill(ChangeValue);
        CheckIsDead();
    }
    

}
