using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieYestSC : MonoBehaviour
{
    public GameObject PrefabObj;
    private PreYestSC preYestSC;
    private GameObject InstObj;
    // Start is called before the first frame update
    void Start()
    {
        InstObj = Instantiate(PrefabObj);
        preYestSC = InstObj.GetComponent<PreYestSC>();
        preYestSC.Naming();
        Instantiate(PrefabObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
