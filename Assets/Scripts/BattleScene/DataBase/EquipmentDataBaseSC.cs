using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentDataBaseSC : ScriptableObject
{
    public List<EquipData> Datas = new List<EquipData>();
}

[System.Serializable]
public class EquipData
{
    [SerializeField]
    int HPFs;
    [SerializeField]
    int MPFs;
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
    [SerializeField]
    int EAbilityID1Fs;
    [SerializeField]
    int EAbilityID2Fs;

    public int HP()
    {
        return HPFs;
    }

    public int MP()
    {
        return MPFs;
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

    public int[] EAbilityIDs()
    {
        return new int[] { EAbilityID1Fs, EAbilityID2Fs };
    }

}
