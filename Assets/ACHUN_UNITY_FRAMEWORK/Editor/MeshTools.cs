using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class MeshTools
{
    [MenuItem("Assets/Tools/Mesh To Asset")]
    private static void MeshToAsset()
    {
        AnimatorController ac = new AnimatorController();
        ac.AddLayer("One");
        AssetDatabase.CreateAsset(ac,Application.dataPath + "/ani.controller");        
    }
}
