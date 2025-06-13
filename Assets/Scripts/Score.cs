using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject text;
    void Start()
    {
     text.GetComponent<Text>().text = PlayerPrefs.GetInt("FinalScore").ToString();   
    }
    void Update()
    {
        
    }
}
