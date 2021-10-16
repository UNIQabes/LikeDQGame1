using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSetUpSC : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUpBattleUIButtonSC()
    {
        for(int i = 0; i <GM.BattleUIButtonSCs.Length; i++)
        {
            
            for (int j=0;j< GM.BattleUIButtonSCs[i].Length;j++)
            {
                
                GM.BattleUIButtonSCs[i][j].SetButtonSC(GM.ButtonComps[i][j]);
                GM.BattleUIButtonSCs[i][j].SettextSC(GM.ButtonTextComps[i][j]);
                GM.BattleUIButtonSCs[i][j].SetButtonKind(i);
                GM.BattleUIButtonSCs[i][j].SetEvent();
            }
        }
    }

    public void SetUpMainMButton()
    {
        GM.selectActBButtonComp.onClick.AddListener(GM.battleUIMSC.WhenClickedSelectActButton);
        GM.actStBButtonComp.onClick.AddListener(GM.battleUIMSC.WhenClickedActStartButton);
    }

}
