using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIButtonSC : MonoBehaviour
{
    private Text textSC;
    private Button buttonSC;
    private int AssignedInfo;

    //0:SkillSelButtons 1:TargetSelButtons 2:WatchUnitSelButtons
    private int ButoonKind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettextSC(Text ArgValue)
    {
        textSC = ArgValue;
    }

    public void SetButtonSC(Button ArgValue)
    {
        buttonSC = ArgValue;
    }

    public void SetAssignedInfo(int ArgValue)
    {
        AssignedInfo = ArgValue;
    }
    public void SetButtonKind(int ArgValue)
    {
        ButoonKind = ArgValue;
    }

    public void SetEvent()
    {
        buttonSC.onClick.AddListener(PassAssignedUnitOrderAndKind);
    }

    public void SetText(string ArgValue)
    {
        textSC.text = ArgValue;
    }

    public void PassAssignedUnitOrderAndKind()
    {
        GM.battleUIMSC.WhenClickedUIButton(AssignedInfo,ButoonKind);
    }

    
}
