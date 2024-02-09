using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinInstantiate : MonoBehaviour
{
    public int countPrefabs = 0;
    public List<GameObject> CoinPrefabs = null;
    public GameObject[] LessPrefabs;
    public Camera cam;
    public GameObject target;
    public int coinValue;
    public Vector2 ObjectPosition;
    public List<Transform> coinTransformPositions;
    float CoinsXpos = 0;
    float CoinsYpos = 0;
    [SerializeField] public GameObject[] highlightCoinButtons;
    [SerializeField] public Button[] defualtCoinButtonsDarkMode;
    [SerializeField] public Button[] highlightSetSize;
    [SerializeField] public Animator label;
    
    // Switch Coins
    public void SwitchCoins()
    {
        string coinsButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (coinsButton)
        {
            case "1k":
                SwitchCoinModeUI(0, 1, 2, 3, 4, 5);
                coinValue = 1000;
                target = CoinPrefabs[0];
                CoinsXpos = coinTransformPositions[0].position.x;
                CoinsYpos = coinTransformPositions[0].position.y;
                print("xPos: " + CoinsXpos + " yPos: " + CoinsYpos);
                break;
            case "5k":
                SwitchCoinModeUI(1, 0, 2, 3, 4, 5);
                coinValue = 5000;
                target = CoinPrefabs[1];
                CoinsXpos = coinTransformPositions[1].position.x;
                CoinsYpos = coinTransformPositions[1].position.y;
                print("xPos: " + CoinsXpos + " yPos: " + CoinsYpos);
                break;
            case "10k":
                SwitchCoinModeUI(2, 0, 1, 3, 4, 5);
                coinValue = 10000;
                target = CoinPrefabs[2];
                CoinsXpos = coinTransformPositions[2].position.x;
                CoinsYpos = coinTransformPositions[2].position.y;
                break;
            case "20k":
                SwitchCoinModeUI(3, 0, 1, 2, 4, 5);
                coinValue = 20000;
                target = CoinPrefabs[3];
                CoinsXpos = coinTransformPositions[3].position.x;
                CoinsYpos = coinTransformPositions[3].position.y;
                print("xPos: " + CoinsXpos + " yPos: " + CoinsYpos);
                break;            
            case "50k":
                SwitchCoinModeUI(4, 0, 1, 2, 3, 5);
                coinValue = 50000;
                target = CoinPrefabs[4];
                CoinsXpos = coinTransformPositions[4].position.x;
                CoinsYpos = coinTransformPositions[4].position.y;
                print("xPos: " + CoinsXpos + " yPos: " + CoinsYpos);
                break;
            case "100k":
                SwitchCoinModeUI(5, 0, 1, 2, 3, 4);
                coinValue = 100000;
                target = CoinPrefabs[5];
                CoinsXpos = coinTransformPositions[5].position.x;
                CoinsYpos = coinTransformPositions[5].position.y;
                print("xPos: " + CoinsXpos + " yPos: " + CoinsYpos);
                break;
        }
    }

    // switch coinModeUI
    void SwitchCoinModeUI(int indexCoin, params int[] arrCoins)
    {
        highlightCoinButtons[indexCoin].SetActive(true);
        defualtCoinButtonsDarkMode[indexCoin].enabled = false;
        highlightSetSize[indexCoin].GetComponent<RectTransform>().sizeDelta = new Vector2(220, 220);
        foreach (int items in arrCoins)
        {
            defualtCoinButtonsDarkMode[items].image.color = Color.gray;
            highlightCoinButtons[items].SetActive(false);
        }
        defualtCoinButtonsDarkMode[indexCoin].enabled = true;
    }

    public void tiaTxt()
    {
        label.Play("taiTxt");
    }
    public void xiuTxt()
    {
        label.Play("xiuTxt");
    }

    public void InstantiateCoins()
    {
        //label.Play("label");
        string choicTaiXiu = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (choicTaiXiu)
        {
            case "tai":
                if (FindAnyObjectByType<GameManager>().credit >= coinValue)
                {
                    label.Play("taiTxt");
                    FindAnyObjectByType<Audios>().sfxButtonPlay("coins");
                    FindAnyObjectByType<GameManager>().taiBalance += coinValue;
                    FindAnyObjectByType<GameManager>().credit -= coinValue;
                    FindAnyObjectByType<GameManager>().convertNumber(FindAnyObjectByType<GameManager>().taiBalance, FindAnyObjectByType<GameManager>().taiTxt,"");
                    FindAnyObjectByType<GameManager>().convertNumber(FindAnyObjectByType<GameManager>().credit, FindAnyObjectByType<GameManager>().creditTxt,"");
                    ObjectPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    GameObject prefabs = Instantiate(target, new Vector2(CoinsXpos, CoinsYpos), Quaternion.identity);
                    StartCoroutine(Move(prefabs.transform, ObjectPosition, 10f));
                    countPrefabs++;
                   // print("countPrefabs: " + countPrefabs);
                    LessPrefabs[countPrefabs - 1] = prefabs.gameObject;
                   // print("prefabe[" + countPrefabs + "]: " + LessPrefabs[countPrefabs - 1]);
                }
                else
                {
                   // print("Coins Not Enough!!");
                }
                break;
            case "xiu":
                if (FindAnyObjectByType<GameManager>().credit >= coinValue)
                {
                    label.Play("xiuTxt");
                    FindAnyObjectByType<Audios>().sfxButtonPlay("coins");
                    FindAnyObjectByType<GameManager>().xiuBalance += coinValue;
                    FindAnyObjectByType<GameManager>().credit -= coinValue;

                    FindAnyObjectByType<GameManager>().convertNumber(FindAnyObjectByType<GameManager>().credit, FindAnyObjectByType<GameManager>().creditTxt,"");
                    FindAnyObjectByType<GameManager>().convertNumber(FindAnyObjectByType<GameManager>().xiuBalance, FindAnyObjectByType<GameManager>().xiuTxt,"");
                    ObjectPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    GameObject prefabs1 = Instantiate(target, new Vector2(CoinsXpos, CoinsYpos), Quaternion.identity);
                    StartCoroutine(Move(prefabs1.transform, ObjectPosition, 10f));
                    countPrefabs++;
                   // print("countPrefabs: " + countPrefabs);
                    LessPrefabs[countPrefabs - 1] = prefabs1;
                  //  print("prefabe[" + countPrefabs + "]: " + LessPrefabs[countPrefabs - 1]);
                }
                else
                {
                    //print("Coins Not Enough!!");
                }
                break;
        }
    }
    public IEnumerator Move(Transform transformPos, Vector2 location, float speedMove)
    {
        while(transformPos != null)
        {
            transformPos.position = Vector2.MoveTowards(transformPos.position, location, speedMove * Time.deltaTime);
            yield return null;
        }
    }
}
