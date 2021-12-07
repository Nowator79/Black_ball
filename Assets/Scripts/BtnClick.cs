using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
    protected Manager manager;
    [SerializeField]
    protected int lvlNum;
    protected bool Active = false;
    public void DisableBtn()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        Active = false;
        transform.name = $"BtnLvl{lvlNum} {Active}";
        gameObject.GetComponent<MeshRenderer>().material = manager.Materials[1];
    }
    public void EnableBtn()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        Active = true;
        transform.name = $"BtnLvl{lvlNum} {Active}";
        gameObject.GetComponent<MeshRenderer>().material = manager.Materials[0];
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (Active)
            {
                manager.currentLevelNum = lvlNum;
                manager.LoadLevel(lvlNum);
            }
        }
    }
    public void InitBtn(int num, bool active)
    {
        lvlNum = num;
        TextMesh text = transform.Find("Text").GetComponent<TextMesh>();
        text.text = (num + 1).ToString();

        if (active)
        {
            EnableBtn();
        }
        else
        {
            DisableBtn();
        }
    }
}
