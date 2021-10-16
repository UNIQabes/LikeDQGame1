using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatusDataBaseSC : ScriptableObject
{
    public List<MonsterStatus> Datas = new List<MonsterStatus>();

}

[System.Serializable]
public class MonsterStatus
{

    [SerializeField]
    int MaxHPFs;
    [SerializeField]
    int MaxMPFs;
    [SerializeField]
    int ATKFs;
    [SerializeField]
    int DEFFs;
    [SerializeField]
    int SPDFs;
    [SerializeField]
    int INTEFs;
    [SerializeField]
    string NameFs;



    public int MaxHP()
    {
        return MaxHPFs;
    }
    
    public int MaxMP()
    {
        return MaxMPFs;
    }

    public int ATK()
    {
        return ATKFs;
    }

    public int DEF()
    {
        return DEFFs;
    }

    public int SPD()
    {
        return SPDFs;
    }

    public int INTE()
    {
        return INTEFs;
    }
    public string Name()
    {
        return NameFs;
    }

    public int[] ADSI()
    {
        return new int[] { ATKFs, DEFFs, SPDFs, INTEFs };
    }
}
