using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Sprite> cubsSprites;
    [SerializeField] List<int> cubsRand = new List<int>(3);
    [SerializeField] List<Image> cubsImageChange;
    private float countTime = 5f;
    private bool isTimeOn = true;
    [SerializeField] Text timeTxt;
    [SerializeField] Animator animator;
    [SerializeField] Animator cover;
    [SerializeField] Slider slider;
    private int cubsTotalValue;
    [SerializeField] public Text creditTxt, taiTxt, xiuTxt;
    public int credit = 999999, taiBalance = 0, xiuBalance = 0;
    [SerializeField] GameObject[] materails;
    [SerializeField] Animator winAnimate;
    public Animator[] btnAnimate;
    [SerializeField] Button[] taiAndxiuButton;
    [SerializeField] Text winTxt;
    [SerializeField] GameObject txtWinActive;
    [SerializeField] Transform creditPos;
    [SerializeField] Transform newPos;

    // Start is called before the first frame update
    void Start()
    {
        FindAnyObjectByType<Audios>().soundClipPlay("background");
        convertNumber(credit, creditTxt,"");
        btnAnimate[4].Play("replay");
        btnAnimate[2].Play("sound");
        btnAnimate[3].Play("music");
        btnAnimate[1].Play("home");
    }

    // Update is called once per frame
    void Update()
    {
        Game_Update();
        convertNumber(credit, creditTxt,"");

    }

    // TODO: Game Update
    void Game_Update()
    {
        if (isTimeOn)
        {
            countTime -= Time.deltaTime;
            timeTxt.text = Mathf.Round(countTime).ToString();
            //slider.value = Mathf.Lerp(1, 5, count * Time.deltaTime);
        }

        if(countTime<= 0 && isTimeOn)
        {
            isTimeOn = false;
            countTime = 0f;
            cubsRand.Clear();
            CubsShakeLucky();
            StartCoroutine(StartPlayAgain());
            
        }
        if(countTime >=2 && countTime <3 && isTimeOn)
        {
        }
        if(countTime >4 && countTime < 5 && isTimeOn)
        {
            coinDisActive(true);
            winAnimate.Play("norm");
            isTimeOn = true;
            StartCoroutine(LapSuffer());

        }
        
    }

    IEnumerator StartPlayAgain()
    {
        animator.Play("normal");
        yield return new WaitForSecondsRealtime(4f);
        coinDisActive(false);
        cover.Play("open");
        FindAnyObjectByType<CoinInstantiate>().label.Play("label");
        yield return new WaitForSecondsRealtime(2f);
        CompareCubs();
        yield return new WaitForSecondsRealtime(4f);
        isTimeOn = true;
        countTime = 5f;
        cubsRand.Clear();
        cubsTotalValue = 0;
        txtWinActive.SetActive(false);
        coinsDestroy();
        clearItems();

    }

    IEnumerator LapSuffer()
    {
        animator.Play("Boil");
        yield return new WaitForSecondsRealtime(2f);
        cover.Play("cover");
        yield return new WaitForSecondsRealtime(1f);
        yield return new WaitForSecondsRealtime(2f);

    }

    // TODO: Cubs shake
    private void CubsShakeLucky()
    {
        for(int i = 0; i<3; i++)
        {
            int getCubs = Random.Range(1,7);
            cubsRand.Add(getCubs);
            //print("getCubs: " + getCubs);
        }
        getChangeCubsSprite();
    }

    private void getChangeCubsSprite()
    {
        for(int i = 0; i<cubsRand.Count; i++)
        {
            for(int j = 1; j<=cubsSprites.Count; j++)
            {
                if (cubsRand[i].Equals(j))
                {
                    cubsImageChange[i].sprite = cubsSprites[j-1];
                    //print("J: " + j);
                }
            }
        }
    }

    // TODO: Compare Cubs 
    void CompareCubs()
    {
        for(int i =0; i<cubsRand.Count; i++)
        {
            cubsTotalValue += cubsRand[i];
        }
        print("Cubs Result Value: " + cubsTotalValue);

        if (cubsTotalValue <= 9)
        {
            materails[2].SetActive(false);
            materails[3].SetActive(true);
            StartCoroutine(animate(("xiuAnimate"),materails[3]));
            materails[2].SetActive(true);
            credit += xiuBalance * 2;
            convertNumber(xiuBalance, winTxt,"+");
            if (xiuBalance > 0) FindAnyObjectByType<Audios>().sfxButtonPlay("win");
            else FindAnyObjectByType<Audios>().sfxButtonPlay("fail");
            //print("Winner: Xiu!! (" + cubsTotalValue + ")");
        }else if(cubsTotalValue > 9)
        {
            materails[0].SetActive(false);
            materails[1].SetActive(true);
            StartCoroutine(animate(("taiAnimate"), materails[1]));
            materails[0].SetActive(true);
            credit += taiBalance * 2;
            txtWinActive.SetActive(true);
            convertNumber(taiBalance, winTxt,"+");
            if (taiBalance>0) FindAnyObjectByType<Audios>().sfxButtonPlay("win");
            else FindAnyObjectByType<Audios>().sfxButtonPlay("fail");
           // print("Winner: Tai!! (" + cubsTotalValue + ")");
        }

    }//
    IEnumerator animate(string animate, GameObject active)
    {
        winAnimate.Play(animate);
        yield return new WaitForSecondsRealtime(1.5f);
        active.SetActive(false);
    }

    void coinDisActive(bool bol)
    {
        if (bol.Equals(false)) {
            taiAndxiuButton[0].enabled = bol;
            taiAndxiuButton[1].enabled = bol;
            FindAnyObjectByType<CoinInstantiate>().label.Play("label");
            for (int i = 0; i < FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode.Length; i++)
            {
                FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode[i].enabled = bol;
                FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode[i].image.color = Color.gray;
            }
        }else if (bol.Equals(true)){
            FindAnyObjectByType<CoinInstantiate>().label.Play("label");
            taiAndxiuButton[0].enabled = bol;
            taiAndxiuButton[1].enabled = bol;
            for (int i = 0; i < FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode.Length; i++)
            {
                FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode[i].enabled = bol;
                FindAnyObjectByType<CoinInstantiate>().defualtCoinButtonsDarkMode[i].image.color = new Color(255,255,255,255);
            }
        }
        
    }

    void clearItems()
    {
        taiBalance = xiuBalance = 0;
        taiTxt.text = xiuTxt.text = xiuBalance.ToString();
    }

    // destroy coins
    void coinsDestroy()
    {
        for(int i  = 0; i<FindAnyObjectByType<CoinInstantiate>().LessPrefabs.Length; i++)
        {
          //  FindAnyObjectByType<CoinInstantiate>().LessPrefabs[i].transform.position = Vector3.MoveTowards(transform.position,creditPos.position, 5f * Time.deltaTime);
            //print("Position: " + newPos);
            Destroy(FindAnyObjectByType<CoinInstantiate>().LessPrefabs[i]);
          //  FindAnyObjectByType<CoinInstantiate>().LessPrefabs[i].transform.position = creditPos.position;
           // print("destroy: " + FindAnyObjectByType<CoinInstantiate>().LessPrefabs[i]+"index: "+i);
        }
        FindAnyObjectByType<CoinInstantiate>().countPrefabs = 0;
    }

    // convert number
    public void convertNumber(int coinsConvert, Text text,string space)
    {
        var convert = string.Format("{0:#,#0}", coinsConvert);
        text.text = space+convert.ToString();
    }
    bool isScale = true;
    [SerializeField] Sprite[] replaySprits;
    [SerializeField] Button pause;
    [SerializeField] GameObject panel;
    // replay game
    public void replay()
    {
        isScale = !isScale;
        if (isScale)
        {
            Time.timeScale = 0;
            btnAnimate[4].Play("replay0");
            btnAnimate[1].Play("home0");
            btnAnimate[2].Play("sound0");
            btnAnimate[3].Play("music0");

            pause.image.sprite = replaySprits[1];
            panel.SetActive(true);
        }
        else if (!isScale)
        {
            Time.timeScale = 1;
            pause.image.sprite = replaySprits[0];
            btnAnimate[4].Play("replay");
            btnAnimate[1].Play("home");
            btnAnimate[2].Play("sound");
            btnAnimate[3].Play("music");
            panel.SetActive(false);
        }

    }

    // calling Home Scene
    public void CallingHomeScene()
    {
        SceneManager.LoadScene("Home Game");
    }
}
