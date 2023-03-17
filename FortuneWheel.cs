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
    public Sprite[] karakterlerSPR�TES;
    public Sprite[] framesSPR�TES;
    public Sprite[] JokersSPR�TES;

    float DonusA�� = 0f;
    float RastegeA�� = 300f;
    int PriceItemNum = 0;

    public GameObject priceImage;
    public GameObject LoadingPanel;
    public GameObject �arkCevirBtn;

    public Sprite QuestionMark; //ba�ta nesneleri kapatmak i�in  //mask the items for begining
    bool �arkaSpinTime; //�ark d�n�yormu ?   //wheel spinning ?


    public GameObject �d�lPanel;


    public static string OdulName; //database e itemi g�ndermek i�in (kullanamayabilirsiniz)    //For send the item ID to database

    public static bool OdulCark�DataT�me;

    public GameObject EquipbTN;
    public TextMeshProUGUI jOKERm�KTARtxt;

    public TextMeshProUGUI UseNowBTNtext;

    public GameObject GoBackBTN; //�ARK D�NERKEN KAPATILMALI

    public static bool ParaVarm�Kontrol; //varsa d�nd�rcek yoksa d�nd�rme

    void Start()
    {
        �arkCevirBtn.SetActive(true);
        �arkaSpinTime = false;


        //bu k�sm� basit bir metot ilede yapabilirsiniz. (ben coin de�erini database den �ekti�imden bu �ekilde yapt�m)
        //You can use simple metot for this (I use like that because the money value is coming database)
        for (int i = 0; i < Items.Length; i++) //yana ge�irdi�im i�in
        {
            Items[i].transform.Rotate(0, 0, -90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!�arkaSpinTime)
        {
            Wheel.transform.Rotate(0, 0, 0.2f);
        }

        if (ParaVarm�Kontrol)
        {
            ParaVarm�Kontrol = false;
            WheelFill();
        }
    }
    public void WheelFill() //itemlerle �ark� doldurmaca
    {
        GoBackBTN.SetActive(false);
        EquipbTN.SetActive(true);
        OdulName = "";
        UseNowBTNtext.text = "";
        �d�lPanel.SetActive(false);
        �arkaSpinTime = true;
        �arkCevirBtn.SetActive(false); //tekrar tekrar bas�lmas�n die butonu kapad�k    //when wheel spinning the button has locked
        LoadingPanel.SetActive(true);

        string[] EldekiItemList = PlayerPrefs.GetString("PurchasedData").Split('/');


        Wheel.transform.rotation = Quaternion.Euler(0, 0, 0); //her �eviri�te rotasyon s�f�rlan�r   //Reset wheel rotation


        int whichOneGive = Random.Range(0, 3); //0-13 1-18 2-23
        if (whichOneGive == 0)
        {
            RastegeA�� = Random.Range(214, 224);
            PriceItemNum = 13;
        }
        else if (whichOneGive == 1)
        {
            RastegeA�� = Random.Range(158f, 168f);
            PriceItemNum = 18;
        }
        else if (whichOneGive == 2)
        {
            RastegeA�� = Random.Range(102f, 112f);
            PriceItemNum = 23;
        }



        //karakter fotosu frame ve joker ihtimalleri d�zenlenecek
        for (int i = 0; i < Items.Length; i++)
        {
        // Items[i].GetComponent<Image>().sprite;
        Bas:
            int rndTur = Random.Range(0, 10); //%40 FOTO %40 FRAME %20 JOKER
            if (rndTur >= 0 && rndTur < 4)//pfoto
            {

                int rndItem = Random.Range(1, karakterlerSPR�TES.Length); //1den ba�lama sebebi ilk item zaten herkeste a��k
                for (int j = 0; j < EldekiItemList.Length; j++)
                {
                    if (EldekiItemList[j] == "pp" + rndItem)
                    {
                        goto Bas;
                    }
                    else if (i == PriceItemNum) //s�ra �d�ldekinde ise
                    {
                        OdulName = "";
                        OdulName = "pp" + rndItem;
                        jOKERm�KTARtxt.text = "";

                        UseNowBTNtext.text = "Bu Profil Foto�raf�n� Kullan";

                    }

                }

                Items[i].GetComponent<Image>().sprite = karakterlerSPR�TES[rndItem];  //item mevcut de�il haliyle ��kabilir

            }
            else if (rndTur >= 4 && rndTur < 8) //pframe
            {

                int rndItem = Random.Range(1, framesSPR�TES.Length); //1den ba�lama sebebi ilk item zaten herkeste a��k
                for (int j = 0; j < EldekiItemList.Length; j++)
                {
                    if (EldekiItemList[j] == "pf" + rndItem)
                    {
                        goto Bas;
                    }
                    else if (i == PriceItemNum) //s�ra �d�ldekinde ise
                    {
                        OdulName = "";
                        OdulName = "pf" + rndItem;
                        jOKERm�KTARtxt.text = "";
                        UseNowBTNtext.text = "Bu �er�eveyi Kullan";

                    }

                }


                Items[i].GetComponent<Image>().sprite = framesSPR�TES[rndItem];

            }
            else if (rndTur >= 8 && rndTur < 10) //joker geldi
            {

                int rndItem = Random.Range(0, JokersSPR�TES.Length); //1den ba�lamama sebebi her joker gelebilmeli
                Items[i].GetComponent<Image>().sprite = JokersSPR�TES[rndItem];


                if (i == PriceItemNum) //s�ra �d�ldekinde ise
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
                    jOKERm�KTARtxt.text = "x" + RealJokerMiktar;
                    EquipbTN.SetActive(false);
                }

            }
        }

        //test
        //Debug.Log("rastgele deper = " + whichOneGive);
        // Debug.Log("rastgele a�� = " + RastegeA��);
        DonusA�� = 720 + RastegeA�� + 90;


        LoadingPanel.SetActive(false);
        StartCoroutine(WheelSpin());


    }
    IEnumerator WheelSpin()
    {
        float anl�kA�� = 0;
        Debug.Log(DonusA��);
        float A�Iart��H�z� = 1;
        while (DonusA�� > anl�kA��)
        {

            if (((DonusA�� - anl�kA��) / 50) < 0.05f)
            {
                A�Iart��H�z� = 0.07f;
            }
            else if (((DonusA�� - anl�kA��) / 50) < 0.1f)
            {
                A�Iart��H�z� = 0.1f;

            }
            else if (((DonusA�� - anl�kA��) / 50) < 0.2f)
            {
                A�Iart��H�z� = 0.2f;

            }
            else
            {
                A�Iart��H�z� = (DonusA�� - anl�kA��) / 50;
            }


            Wheel.transform.Rotate(0, 0, A�Iart��H�z�);
            yield return new WaitForSeconds(0.0001f);
            anl�kA�� += A�Iart��H�z�;
        }

        //�d�l al�nd� database e kaydetme vakti
        OdulCark�DataT�me = true; //data y� yollamak i�in metodu �a��rd�k //update ile y�nettim

        yield return new WaitForSeconds(1);
        priceImage.GetComponent<Image>().sprite = Items[PriceItemNum].GetComponent<Image>().sprite;
        // PriceTXT.text = "�d�l�n�z : " + OdulName;

        �d�lPanel.SetActive(true);
        GoBackBTN.SetActive(true);

        yield return new WaitForSeconds(1);
        //tekrar �evirebilsin diye a�t�k

        �arkaSpinTime = false;
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].GetComponent<Image>().sprite = QuestionMark;
        }
        �arkCevirBtn.SetActive(true);
    }
    public void OdulPanelKapa()
    {
        �d�lPanel.SetActive(false);
    }
}
