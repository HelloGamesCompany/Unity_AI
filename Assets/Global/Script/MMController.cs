using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMController : MonoBehaviour
{
    public GameObject MM, megaMM;


    public void ChangeState()
    {
        MM.SetActive(false);
        megaMM.SetActive(true);
    }
}
