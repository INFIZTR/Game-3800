using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{

    public SelectedController SC;
    public void Onclick()
    {
        //SC.MakePosion();
        SC.ReadRecipe();
    }
}
