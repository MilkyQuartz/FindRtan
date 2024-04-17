using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnToggle : MonoBehaviour
{
    public Button btn;
    public Text txt;
    public int btnNum=1;
    public AudioManager dontDestroyOnLoad;
    
    public void OneBtnToggle()
    {    
        if(btnNum==2){ 
            dontDestroyOnLoad.stageNum = 1;
            txt.text=btnNum.ToString();
        }else if(btnNum==3){   
            dontDestroyOnLoad.stageNum = 2;         
            txt.text=btnNum.ToString();
        }else if(btnNum==4){  
            dontDestroyOnLoad.stageNum = 3;          
            txt.text=btnNum.ToString();
        }else if(btnNum==5){      
            dontDestroyOnLoad.stageNum = 4;      
            txt.text=btnNum.ToString();
        }else{      
            dontDestroyOnLoad.stageNum = 0;      
            txt.text=btnNum.ToString();
        }        
    }    
}
