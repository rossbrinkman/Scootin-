using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shifter : MonoBehaviour
{
    // this script is to set the camera animations
    public Animator caman;
    public GameObject PlayBulb;
    public GameObject SettingsBulb;
    public GameObject CreditsBulb;
    public GameObject ShopBulb;
    void Start()
    {
        caman = GetComponent<Animator> ();
        PlayBulb.SetActive(false);
        SettingsBulb.SetActive(false);
        CreditsBulb.SetActive(false);
        ShopBulb.SetActive(false);

    }
    public void play()
    {
        caman.SetBool ("play", true);
        caman.SetBool("shop", false);
        caman.SetFloat("speed", 1.0f);
        PlayBulb.SetActive(true);
        ShopBulb.SetActive(false);

    }
    public void settings()
    {
        caman.SetBool("settings", true);
        caman.SetBool("play", false);
        caman.SetBool("replay", false);
        caman.SetFloat("speed", 1.0f);
        SettingsBulb.SetActive(true);
        PlayBulb.SetActive(false);

    }
    public void credits()
    {
        caman.SetBool("credits", true);
        caman.SetBool("settings", false);
        caman.SetFloat("speed", 1.0f);
        CreditsBulb.SetActive(true);
        SettingsBulb.SetActive(false);

    }
    public void achievements()
    {
        caman.SetBool("achievements", true);
        caman.SetBool("credits", false);
        caman.SetFloat("speed", 1.0f);
        CreditsBulb.SetActive(false);

    }
    public void shop()
    {
        caman.SetBool("shop", true);
        caman.SetBool("achievements", false);
        caman.SetBool("replay", false);
        caman.SetFloat("speed", 1.0f);
        ShopBulb.SetActive(true);
    }

    public void replay()
    {
        caman.SetBool("replay", true);
        caman.SetBool("shop", false);
        caman.SetFloat("speed", 1.0f);
        PlayBulb.SetActive(true);
    }
    public void antisettings()
    {
        caman.SetBool("settings", true);
        caman.SetBool("replay", true);
        caman.SetBool("credits", false);
        caman.SetFloat("speed", -1.0f);
        SettingsBulb.SetActive(false);
        PlayBulb.SetActive(true);

    }
    public void anticredits()
    {
        caman.SetBool("credits", true);
        caman.SetBool("settings", true);
        caman.SetBool("achievements", false);
        caman.SetFloat("speed", -1.0f);
        CreditsBulb.SetActive(false);
        SettingsBulb.SetActive(true);

    }
    public void antiachievements()
    {
        caman.SetBool("achievements", true);
        caman.SetBool("credits", true);
        caman.SetBool("shop", false);
        caman.SetFloat("speed", -1.0f);
        CreditsBulb.SetActive(true);

    }
    public void antishop()
    {
        caman.SetBool("shop", true);
        caman.SetBool("achievements", true);
        caman.SetBool("replay", false);
        caman.SetFloat("speed", -1.0f);
        ShopBulb.SetActive(false);
    }

    public void antireplay()
    {
        caman.SetBool("replay", true);
        caman.SetBool("shop", true);
        caman.SetBool("settings", false);
        caman.SetFloat("speed", -1.0f);
        PlayBulb.SetActive(false);
        ShopBulb.SetActive(true);
    }

}
