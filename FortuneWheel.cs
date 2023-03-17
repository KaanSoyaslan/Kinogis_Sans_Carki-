using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FortuneWheel : MonoBehaviour
{
    [Header ("Girdiler")]
    public GameObject Wheel;
    public GameObject[] Items;
    public Sprite[] karakterlerSPRÝTES;
    public Sprite[] framesSPRÝTES;
    public Sprite[] JokersSPRÝTES;

    float DonusAçý = 0f;
    float RastegeAçý = 300f;
    int PriceItemNum = 0;

    public GameObject priceImage;
    public GameObject LoadingPanel;
    public GameObject ÇarkCevirBtn;

    public Sprite QuestionMark; //baþta nesneleri kapatmak için  //mask the items for begining
    bool ÇarkaSpinTime; //çark dönüyormu ?   //wheel spinning ?


    public GameObject ÖdülPanel;


    public static string OdulName; //database e itemi göndermek için (kullanamayabilirsiniz)    //For send the item ID to database

    public static bool OdulCarkýDataTýme;

    public GameObject EquipbTN;
    public TextMeshProUGUI jOKERmÝKTARtxt;

    public TextMeshProUGUI UseNowBTNtext;

    public GameObject GoBackBTN; //çARK DÖNERKEN KAPATILMALI

    public static bool ParaVarmýKontrol; //varsa döndürcek yoksa döndürme

    void Start()
    {
        ÇarkCevirBtn.SetActive(true);
        ÇarkaSpinTime = false;


        //bu kýsmý basit bir metot ilede yapabilirsiniz. (ben coin deðerini database den çektiðimden bu þekilde yaptým)
        //You can use simple metot for this (I use like that because the money value is coming database)
        for (int i = 0; i < Items.Length; i++) //yana geçirdiðim için
        {
            Items[i].transform.Rotate(0, 0, -90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ÇarkaSpinTime)
        {
            Wheel.transform.Rotate(0, 0, 0.2f);
        }

        if (ParaVarmýKontrol)
        {
            ParaVarmýKontrol = false;
            WheelFill();
        }
    }
    public void WheelFill() //itemlerle çarký doldurmaca
    {
        GoBackBTN.SetActive(false);
        EquipbTN.SetActive(true);
        OdulName = "";
        UseNowBTNtext.text = "";
        ÖdülPanel.SetActive(false);
        ÇarkaSpinTime = true;
        ÇarkCevirBtn.SetActive(false); //tekrar tekrar basýlmasýn die butonu kapadýk    //when wheel spinning the button has locked
        LoadingPanel.SetActive(true);

        string[] EldekiItemList = PlayerPrefs.GetString("PurchasedData").Split('/');


        Wheel.transform.rotation = Quaternion.Euler(0, 0, 0); //her çeviriþte rotasyon sýfýrlanýr   //Reset wheel rotation


        int whichOneGive = Random.Range(0, 3); //0-13 1-18 2-23
        if (whichOneGive == 0)
        {
            RastegeAçý = Random.Range(214, 224);
            PriceItemNum = 13;
        }
        else if (whichOneGive == 1)
        {
            RastegeAçý = Random.Range(158f, 168f);
            PriceItemNum = 18;
        }
        else if (whichOneGive == 2)
        {
            RastegeAçý = Random.Range(102f, 112f);
            PriceItemNum = 23;
        }



        //karakter fotosu frame ve joker ihtimalleri düzenlenecek
        for (int i = 0; i < Items.Length; i++)
        {
        // Items[i].GetComponent<Image>().sprite;
        Bas:
            int rndTur = Random.Range(0, 10); //%40 FOTO %40 FRAME %20 JOKER
            if (rndTur >= 0 && rndTur < 4)//pfoto
            {

                int rndItem = Random.Range(1, karakterlerSPRÝTES.Length); //1den baþlama sebebi ilk item zaten herkeste açýk
                for (int j = 0; j < EldekiItemList.Length; j++)
                {
                    if (EldekiItemList[j] == "pp" + rndItem)
                    {
                        goto Bas;
                    }
                    else if (i == PriceItemNum) //sýra ödüldekinde ise
                    {
                        OdulName = "";
                        OdulName = "pp" + rndItem;
                        jOKERmÝKTARtxt.text = "";

                        UseNowBTNtext.text = "Bu Profil Fotoðrafýný Kullan";

                    }

                }

                Items[i].GetComponent<Image>().sprite = karakterlerSPRÝTES[rndItem];  //item mevcut deðil haliyle çýkabilir

            }
            else if (rndTur >= 4 && rndTur < 8) //pframe
            {

                int rndItem = Random.Range(1, framesSPRÝTES.Length); //1den baþlama sebebi ilk item zaten herkeste açýk
                for (int j = 0; j < EldekiItemList.Length; j++)
                {
                    if (EldekiItemList[j] == "pf" + rndItem)
                    {
                        goto Bas;
                    }
                    else if (i == PriceItemNum) //sýra ödüldekinde ise
                    {
                        OdulName = "";
                        OdulName = "pf" + rndItem;
                        jOKERmÝKTARtxt.text = "";
                        UseNowBTNtext.text = "Bu Çerçeveyi Kullan";

                    }

                }


                Items[i].GetComponent<Image>().sprite = framesSPRÝTES[rndItem];

            }
            else if (rndTur >= 8 && rndTur < 10) //joker geldi
            {

                int rndItem = Random.Range(0, JokersSPRÝTES.Length); //1den baþlamama sebebi her joker gelebilmeli
                Items[i].GetComponent<Image>().sprite = JokersSPRÝTES[rndItem];


                if (i == PriceItemNum) //sýra ödüldekinde ise
                {
                    OdulName = "";
                    int JokerMiktar = Random.Range(0, 50);
                    int RealJokerMiktar = 1;
                    if (JokerMiktar <= 5)
                    {
                        RealJokerMiktar = 1;
                    }
                    else if (JokerMiktar > 5 && JokerMiktar <= 10)
                    {
                        RealJokerMiktar = 2;
                    }
                    else if (JokerMiktar > 10 && JokerMiktar <= 20)
                    {
                        RealJokerMiktar = 4;
                    }
                    else if (JokerMiktar > 20 && JokerMiktar <= 30)
                    {
                        RealJokerMiktar = 5;
                    }
                    else if (JokerMiktar > 40 && JokerMiktar <= 45)
                    {
                        RealJokerMiktar = 6;
                    }
                    else
                    {
                        RealJokerMiktar = 7;
                    }


                    
                    for (int k = 0; k < 6; k++)
                    {
                        if (k == rndItem && k == 5)
                        {
                            OdulName += "" + RealJokerMiktar;
                        }
                        else if (k == rndItem && k != 5)
                        {
                            OdulName += RealJokerMiktar + "/";
                        }
                        else if (k != rndItem && k == 5)
                        {
                            OdulName += "0";
                        }
                        else
                        {
                            OdulName += "0/";
                        }

                    }
                    jOKERmÝKTARtxt.text = "x" + RealJokerMiktar;
                    EquipbTN.SetActive(false);
                }

            }
        }

        //test
        //Debug.Log("rastgele deper = " + whichOneGive);
        // Debug.Log("rastgele açý = " + RastegeAçý);
        DonusAçý = 720 + RastegeAçý + 90;


        LoadingPanel.SetActive(false);
        StartCoroutine(WheelSpin());


    }
    IEnumerator WheelSpin()
    {
        float anlýkAçý = 0;
        Debug.Log(DonusAçý);
        float AÇIartýþHýzý = 1;
        while (DonusAçý > anlýkAçý)
        {

            if (((DonusAçý - anlýkAçý) / 50) < 0.05f)
            {
                AÇIartýþHýzý = 0.07f;
            }
            else if (((DonusAçý - anlýkAçý) / 50) < 0.1f)
            {
                AÇIartýþHýzý = 0.1f;

            }
            else if (((DonusAçý - anlýkAçý) / 50) < 0.2f)
            {
                AÇIartýþHýzý = 0.2f;

            }
            else
            {
                AÇIartýþHýzý = (DonusAçý - anlýkAçý) / 50;
            }


            Wheel.transform.Rotate(0, 0, AÇIartýþHýzý);
            yield return new WaitForSeconds(0.0001f);
            anlýkAçý += AÇIartýþHýzý;
        }

        //ödül alýndý database e kaydetme vakti
        OdulCarkýDataTýme = true; //data yý yollamak için metodu çaðýrdýk //update ile yönettim

        yield return new WaitForSeconds(1);
        priceImage.GetComponent<Image>().sprite = Items[PriceItemNum].GetComponent<Image>().sprite;
        // PriceTXT.text = "ödülünüz : " + OdulName;

        ÖdülPanel.SetActive(true);
        GoBackBTN.SetActive(true);

        yield return new WaitForSeconds(1);
        //tekrar çevirebilsin diye açtýk

        ÇarkaSpinTime = false;
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].GetComponent<Image>().sprite = QuestionMark;
        }
        ÇarkCevirBtn.SetActive(true);
    }
    public void OdulPanelKapa()
    {
        ÖdülPanel.SetActive(false);
    }
}
