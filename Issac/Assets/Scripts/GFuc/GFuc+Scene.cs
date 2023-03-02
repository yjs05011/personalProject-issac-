using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class GFunc
{
    // Start is called before the first frame update
   
   public static void SceneChanger(string SceneName){
        SceneManager.LoadScene($"{SceneName}");
   }
}
