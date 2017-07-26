using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Main : MonoBehaviour
    {
        public Sprite[] sprites = new Sprite[6];

        public RollTween[] randomArea = new RollTween[8];
        public RollTween[] rollTweens = new RollTween[16];
        //public RollTween middleRollTween;
        public ScrollList scrollList;
        public LightRoll[] lightRolls = new LightRoll[4];
        public Text[] textRolls = new Text[12];
        // Use this for initialization



        #region  随机模块

        /// <summary>
        /// 随机值开关
        /// </summary>
        private bool rollSwitch = true;
        /// <summary>
        /// 随机的速度
        /// </summary>
        public float rollSpeed = 0.2f;
        /// <summary>
        /// 正在随机的Image字典
        /// </summary>
        private Dictionary<Image, Action<int>> imageCallBackDictionary = new Dictionary<Image, Action<int>>();
        /// <summary>
        /// 正在随机的Text字典
        /// </summary>
        private Dictionary<Text, Action<int>> textCallBackDictionary = new Dictionary<Text, Action<int>>();

        private readonly int textSringLength = 16;
        private void AddImageCallBack(Image iamge, Action<int> callback)
        {
            if (!imageCallBackDictionary.ContainsKey(iamge))
            {
                imageCallBackDictionary.Add(iamge, callback);
            }
        }

        private void RemoveImageCallBack(Image image)
        {
            imageCallBackDictionary.Remove(image);
        }

        private void AddTextCallBack(Text text, Action<int> callback)
        {
            if (!textCallBackDictionary.ContainsKey(text))
            {
                textCallBackDictionary.Add(text, callback);
            }
        }

        private void RemoveTextCallBack(Text text)
        {
            textCallBackDictionary.Remove(text);
        }

        private void TweenRoll(Image image, int index, float time = 1f, float speed = 0.05f)
        {
            float delteTime = 0;
            DOTween.To((float value) =>
            {
                if (value - delteTime >= speed)
                {
                    delteTime += speed;
                    image.overrideSprite = sprites[Random.Range(0, sprites.Length)];
                }

            }, 0, 1, time).SetEase(Ease.OutCubic).OnComplete(() =>
             {
                 image.overrideSprite = sprites[index];
             });
        }
        private void TweenRoll(Text text, int index, float time = 1f, float speed = 0.05f)
        {
            float delteTime = 0;
            DOTween.To((float value) =>
            {
                if (value - delteTime >= speed)
                {
                    delteTime += speed;
                    text.text = ((DataManager.ExchangeEnum)Random.Range(0, textSringLength)).ToString();
                }

            }, 0, 1, time).SetEase(Ease.OutCubic).OnComplete(() =>
             {
                 text.text = ((DataManager.ExchangeEnum)index).ToString();
             });
        }
        #endregion
        void Start()
        {
            DataManager.Create();
            for (int i = 0; i < randomArea.Length; i++)
            {
                if (i < 4)
                {
                    randomArea[i] = transform.FindSafe("Canvas/MainPanel/Random_1/Random_" + (i + 1))
                        .GetSafeComponent<RollTween>();
                }
                else
                {
                    randomArea[i] = transform.FindSafe("Canvas/MainPanel/Random_2/Random_" + (i + 1 - 4))
                        .GetSafeComponent<RollTween>();
                }
            }

            for (int i = 0; i < rollTweens.Length; i++)
            {
                rollTweens[i] = transform.FindSafe("Canvas/MainPanel/Value_" + (i / 4 + 1) + "/Item_" + (i % 4 + 1)).GetComponent<RollTween>();
            }
            //middleRollTween = transform.FindSafe("Canvas/MainPanel/MiddleRoll/Panel").GetComponent<RollTween>();
            //middleRollTween.IsRepeat = true;
            //middleRollTween.Direct = true;
            scrollList = transform.FindSafe("Canvas/MainPanel/MiddleRoll/Panel").GetComponent<ScrollList>();

            for (int i = 0; i < lightRolls.Length; i++)
            {
                lightRolls[i] = transform.FindSafe("Canvas/MainPanel/Lights" + (i + 1)).GetSafeComponent<LightRoll>();
            }
            for (int i = 0; i < textRolls.Length; i++)
            {
                textRolls[i] = transform.FindSafe("Canvas/MainPanel/Change" + (i / 3 + 1) + "/" + (i % 3 + 1)).GetSafeComponent<Text>();
            }


        }
        private void AddAllRollCallBack()
        {
            for (int i = 0; i < randomArea.Length; i++)
            {
                for (int j = 0; j < randomArea[i].items.Length; j++)
                {
                    Image image = randomArea[i].items[j].GetSafeComponent<Image>();
                    AddImageCallBack(image, (int value) =>
                     {
                         image.GetComponent<Image>().overrideSprite = sprites[value];
                     });
                }
            }
            foreach (var item in rollTweens)
            {
                for (int i = 0; i < item.items.Length; i++)
                {

                    Image image = item.items[i].GetSafeComponent<Image>();
                    AddImageCallBack(image, (int value) =>
                     {
                         image.overrideSprite = sprites[value];
                     });
                }
            }
            foreach (var item in textRolls)
            {
                AddTextCallBack(item, (int value) =>
                {
                    item.text = ((DataManager.ExchangeEnum)value).ToString();
                });
            }
        }

        private void RemoveAllRollCallBack()
        {
            for (int i = 0; i < randomArea.Length; i++)
            {
                for (int j = 0; j < randomArea[i].items.Length; j++)
                {
                    RemoveImageCallBack(randomArea[i].items[j].GetComponent<Image>());
                }
            }
            foreach (var item in rollTweens)
            {
                for (int i = 0; i < item.items.Length; i++)
                {
                    RemoveImageCallBack(item.items[i].GetComponent<Image>());
                }
            }
            foreach (var item in textRolls)
            {
                RemoveTextCallBack(item);
            }
        }

        // Update is called once per frame  
        private float DeltaTime { get; set; }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                StartGame();
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                EndingGame();
            }

            if (rollSwitch)
            {
                DeltaTime += Time.deltaTime;
                if (DeltaTime > rollSpeed)
                {
                    DeltaTime = 0;
                    foreach (var callback in imageCallBackDictionary)
                    {
                        if (callback.Key != null && callback.Value != null)
                        {
                            int index = Random.Range(0, sprites.Length);
                            callback.Value.Invoke(index);
                        }
                    }
                    foreach (var callback in textCallBackDictionary)
                    {
                        if (callback.Key != null && callback.Value != null)
                        {
                            int index = Random.Range(0, textSringLength);//DataManager.ExchangeEnum的值域
                            callback.Value.Invoke(index);
                        }
                    }

                }
            }
        }

        public void StartGame()
        {
            AddAllRollCallBack();
            foreach (var item in randomArea)
            {
                item.StartRoll();
            }
            for (int i = 0; i < lightRolls.Length; i++)
            {
                lightRolls[i].StartRoll();
            }
            //foreach (var item in rollTweens)
            //{
            //    item.StartRoll();
            //}
            //middleRollTween.StartRoll();
            scrollList.StartRoll();
        }

        public void EndingGame()
        {
            DataManager.Instance.CalculateResult();
            RemoveAllRollCallBack();

            for (int i = 0; i < randomArea.Length; i++)
            {
                randomArea[i].TweenRoll(1.5f, indexMaxLength: 2);
                for (int j = 0; j < randomArea[i].items.Length; j++)
                {
                    if (j == randomArea[i].items.Length - 1)
                    {
                        TweenRoll(randomArea[i].items[j].GetSafeComponent<Image>(), Random.Range(0, sprites.Length));
                    }
                    else
                    {
                        int number = DataManager.Instance.randomNumbers[i][j];
                        TweenRoll(randomArea[i].items[j].GetSafeComponent<Image>(), number);

                    }
                }
            }
            for (int i = 0; i < rollTweens.Length; i++)
            {
                //rollTweens[i].TweenRoll(1.5f, indexMaxLength: 2);
                for (int j = 0; j < rollTweens[i].items.Length; j++)
                {
                    if (j == rollTweens[i].items.Length - 1)
                    {
                        TweenRoll(rollTweens[i].items[j].GetSafeComponent<Image>(), Random.Range(0, sprites.Length));
                    }
                    else
                    {
                        int number = DataManager.Instance.valueNumbers[i][j];
                        TweenRoll(rollTweens[i].items[j].GetSafeComponent<Image>(), number);
                    }
                }
            }
            for (int i = 0; i < lightRolls.Length; i++)
            {
                lightRolls[i].TweenRoll(DataManager.Instance.lights[i]);
            }
            for (int i = 0; i < textRolls.Length; i++)
            {
                int index = i;
                int tempValue = 0;
                DOTween.To((float value) =>
                {
                    int temp = Mathf.FloorToInt(value * textSringLength) - 1;
                    if (temp != tempValue)
                    {
                        tempValue = temp;
                        textRolls[index].text = ((DataManager.ExchangeEnum)Random.Range(0, textSringLength)).ToString();
                    }
                }, 0f, 1f, 1.5f).OnComplete(() =>
                {
                    textRolls[index].text = DataManager.Instance.exchangeEnums[index].ToString();
                });
            }
            int randomValue = Random.Range(0, scrollList.items.Length);
            
            //middleRollTween.TweenRoll(2, ()=> { CalculateGame(0); }, tweenDistance: middleRollTween.maxLength / middleRollTween.items.Length * randomValue);
            scrollList.TweenRoll(time:2.0f,callback:()=> { CalculateGame(0); }, indexMaxLength:1);
        }

        public void Caculate()
        {
            Debug.Log("这里结算结果");
        }

        public void CalculateGame(int index)
        {
            if (index >= 8)
            {
                Caculate();
                return;
            }
            //int temp = 0;
            List<int> UpList = new List<int>();
            List<int> DownList = new List<int>();
            
            foreach (var item in DataManager.Instance.exChangedDictionary)
            {
                if (item.Value == (DataManager.ExchangeEnum)(index * 2))
                {
                    DownList.Add(item.Key);
                }
                if (item.Value == (DataManager.ExchangeEnum)(index * 2 + 1))
                {
                    UpList.Add(item.Key);
                }
            }

            if (UpList.Count > 0 || DownList.Count > 0)
            {
                foreach (var item in DownList)
                {
                    rollTweens[item].TweenRoll(0.5f, indexMaxLength: 1);
                    for (int i = 0; i < rollTweens[item].items.Length; i++)
                    {
                        rollTweens[item].items[i].GetComponent<Image>().overrideSprite = randomArea[index].items[i].GetComponent<Image>().overrideSprite;
                    }
                    //temp += 1;
                }
                foreach (var item in UpList)
                {
                    rollTweens[item].TweenRoll(0.5f, indexMaxLength: 1);
                    for (int i = 0; i < rollTweens[item].items.Length; i++)
                    {
                        rollTweens[item].items[i].GetComponent<Image>().overrideSprite = randomArea[index].items[(i+1)%4].GetComponent<Image>().overrideSprite;
                    }
                    //temp += 1;
                }
                randomArea[index].TweenRoll(0.5f, indexMaxLength: 1, callback: () =>
                    {
                        CalculateGame(index + 1);
                    });
            }
            else
            {
                CalculateGame(index + 1);
            }
        }
    }
}
