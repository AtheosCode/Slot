using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class RollUnit : MonoBehaviour {
    [SerializeField]
    private Sprite[] Numbers;
    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button StopBtn;
    [SerializeField]
    public GameObject[] NumberBox;
    [SerializeField]
    public GameObject[] Lights;
    [SerializeField]
    public GameObject[] LettersObjs;

    void Start()
    {
        RollManager.Instance.RollUnit = this;

        for (int i = 0; i < 8; i++)
        {
            NumberBox[6].transform.GetChild(i).GetComponent<Image>().sprite = Numbers[i];
        }
        for (int j = 0; j < 4; j++)
        {
            for (int r = 0; r < 4; r++)
            {
                Lights[j].transform.GetChild(r).gameObject.SetActive(false);
            }
        }
    }

    public void OnBtn1Click()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int r = 0; r < 4; r++)
            {
                Lights[j].transform.GetChild(r).gameObject.SetActive(false);
            }
        }
        RollManager.Instance.isRun = true;
        RollManager.Instance.isStop = false;
        RollManager.Instance.Run();
        StartBtn.transform.gameObject.SetActive(false);
        StopBtn.transform.gameObject.SetActive(true);
    }

    public void OnBtn2Click()
    {
        RollManager.Instance.isRun = false;
        RollManager.Instance.isStop = true;
        RollManager.Instance.Stop();
        StopBtn.transform.gameObject.SetActive(false);
        StartBtn.transform.gameObject.SetActive(true);
    }
    // Use this for initialization

    public void NumberRound()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int i = 0; i < 5; i++)
            {
                NumberBox[0].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
                NumberBox[1].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
            }
            for (int i = 0; i < 4; i++)
            {
                NumberBox[2].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
                NumberBox[3].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
                NumberBox[4].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
                NumberBox[5].transform.GetChild(r).GetChild(i).GetComponent<Image>().sprite = Numbers[Random.Range(0, 7)];
            }
        }
    }

    public string[] LetterRound()
    {
        string[] Letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
        string[] Direcs = new string[] { "U", "D" };
        string[] changeStr = new string[4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                LettersObjs[i].transform.GetChild(j).GetComponent<Text>().text = Letters[Random.Range(0, 7)].ToString() + "_" + Direcs[Random.Range(0, 2)].ToString();
            }
            changeStr[i] = LettersObjs[i].transform.GetChild(0).GetComponent<Text>().text.ToString() + "~" + LettersObjs[i].transform.GetChild(1).GetComponent<Text>().text.ToString() + "~" + LettersObjs[i].transform.GetChild(2).GetComponent<Text>().text.ToString();
        }
        return changeStr;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
