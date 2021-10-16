using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsRunOrderSC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GM.startSetUpSC.SetUpBattleUIButtonSC();
        GM.startSetUpSC.SetUpMainMButton();

        WhenBattleStart();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhenBattleStart()
    {
        GM.battleSystem.WhenStartBattle();
        GM.battleUIMSC.WhenBattleStart();
        WhenTurnStart();
    }

    public void WhenTurnStart()
    {
        GM.battleSystem.WhenTurnStart();
        GM.battleUIMSC.WhenTurnStart();
    }

    public void WhenActStart()
    {
        Debug.Log("ActStart!");
        GM.battleUIMSC.WhenActStart();
        GM.battleSystem.WhenActStart();
        WhenTurnEnd();
    }

    public void WhenTurnEnd()
    {
        GM.battleSystem.WhenTurnEnd();
        switch (GM.battleSystem.GetWinOrLose())
        {
            case WinOrLoseEnum.CompanionWin:
                Debug.Log("Companionの勝ち");
                WhenFightFinish(GM.battleSystem.GetWinOrLose());
                break;

            case WinOrLoseEnum.EnemyWin:
                Debug.Log("Companionの負け");
                WhenFightFinish(GM.battleSystem.GetWinOrLose());
                break;

            case WinOrLoseEnum.Draw:
                Debug.Log("引き分け");
                WhenFightFinish(GM.battleSystem.GetWinOrLose());
                break;

            case WinOrLoseEnum.DuringBattle:
                WhenTurnStart();
                break;
        }
        
    }

    public void WhenFightFinish(WinOrLoseEnum BattleResult)
    {

    }

}
