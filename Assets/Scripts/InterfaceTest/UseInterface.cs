using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInter {

    public void Useinter(IMyInterface myInterface) {
        myInterface.MyTest();
    }
}
public class UseInterface : MonoBehaviour {
    private void Update()
    {
        ShixianInterface ShixianInterface = new ShixianInterface();
        MyInter myInter = new MyInter();
        myInter.Useinter(ShixianInterface);
    }


}
