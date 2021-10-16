using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillDataBaseSC : ScriptableObject
{
    public List<SkillData> Datas = new List<SkillData>();
}

[System.Serializable]
public class SkillData
{

    [SerializeField] private int DAMAGEFs;
    [SerializeField] private RangeEnum RANGEFs;
    [SerializeField] private SkillElementEnum SkillElement;
    [SerializeField] private int MPFs;
    [SerializeField] private string NameFs;

    



    public int DAMAGE()
    {
        return DAMAGEFs;
        
    }

    public RangeEnum RANGE()
    {
        return RANGEFs;
    }

    

    public int MP()
    {
        return MPFs;
    }

    public string Name()
    {
        return NameFs;
    }

}

[System.Serializable]
public class CureSkillParameter
{
    int CurePoint;
    bool　CanResurrection;

}

[System.Serializable]
public class PhisicSkillDataParameter
{
    int DamagePoint;

}

[System.Serializable]
public class AttackPhisicParameter
{

}


public enum RangeEnum
{
OneAliveFoe,
AllAliveFoes,
OneAliveAlly,
AllAliveAllies,
OneDeadAlly,
AllDeadAllies
}

public enum SkillTypeEnum
{
    Phisics,
    AttackMagic,
    SupportMagic
}

public enum SkillElementEnum
{
    None,
    Fire,
    Explode,
    Cure
}