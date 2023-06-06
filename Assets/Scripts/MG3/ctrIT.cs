using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ctrIT : MonoBehaviour
{
    private ctrMG3 ctrGame;
    public TouchScreenKeyboard inText;
    public InputField inTextBox;

    private void Awake(){
        ctrGame= GameObject.Find("Game").GetComponent<ctrMG3>();
        inTextBox= GetComponent<InputField>(); 
    }
    // Start is called before the first frame update
    //void Start(){}

    // Update is called once per frame
    void Update()
    {
        if(TouchScreenKeyboard.visible == false && inText != null){
            if(inText.status == TouchScreenKeyboard.Status.Done && inText.text!=""){
                ctrGame.CompareAnswer(inText.text);
                inText.text="";
            }
        }
    }

    public void OpenKeyboard(){
        inText= TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
