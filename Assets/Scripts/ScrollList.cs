using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScrollList : MonoBehaviour
    {
        public enum State
        {
            Static,
            Rolling,
            TweenEnding
        }

        private float halfItemHeight;
        public RectTransform[] items;

        [NonSerialized] public float maxLength;

        //200;
        private Vector2 oldPosition;
        [Tooltip("速度")]
        public float speed = 200;

        private State state = State.Static;

        /// <summary>
        /// 循环连接点  是不是同样的值
        /// </summary>
        [Tooltip("循环连接点  是不是同样的值")]
        public bool isRepeat;

        /// <summary>
        /// False 下滑  True 上滑
        /// </summary>
        [Tooltip("False 下滑  True 上滑")]
        public bool direct;
        public float Distance { get; set; }


        public void StartRoll()
        {
            if (state == State.Static)
            {
                state = State.Rolling;
                StartCoroutine(Rolling(maxLength, speed));
            }
        }

        /// <summary>
        ///     滚动 默认滚动到最初始的位置
        ///     tweenDistance 额外附加的距离
        /// </summary>
        /// <param name="time"></param>
        /// <param name="callback"></param>
        /// <param name="tweenDistance"></param>
        public void TweenRoll(float time = 1.5f, Action callback = null, int indexMaxLength = 0,float tweenDistance = 0)
        {
            if (state != State.TweenEnding)
            {
                state = State.TweenEnding;
                float distance = (Mathf.FloorToInt(Distance / maxLength) + 1 + indexMaxLength) * maxLength;
                DOTween.To((float value) =>
                {
                    DoMove(value);
                }, Distance , distance, time).SetEase(Ease.OutCubic).OnComplete(() =>
                  {
                      state = State.Static;
                      if (callback != null)
                          callback.Invoke();
                  });
            }
        }

        private void DoMove(float distance)
        {
            Distance = distance;
            for (var i = 0; i < items.Length; i++)
            {
                float tempDistance = 0;
                if (direct)
                {
                    tempDistance = -((i * 2 * halfItemHeight) + halfItemHeight) + Distance % maxLength;
                    if (tempDistance > halfItemHeight)
                    {
                        tempDistance -= maxLength;
                    }
                    if (isRepeat)
                    {
                        if (tempDistance <= -(maxLength - halfItemHeight * 2) && tempDistance >= -(maxLength) || tempDistance>=halfItemHeight*0.8)
                        {
                            if (i == items.Length - 1)
                            {
                                items[i].GetComponent<Image>().overrideSprite = items[0].GetComponent<Image>().overrideSprite;
                            }
                            else
                            {
                                items[i].GetComponent<Image>().overrideSprite = items[i + 1].GetComponent<Image>().overrideSprite;
                            }
                        }
                    }
                }
                else
                {
                    tempDistance = -((i * 2 * halfItemHeight) + halfItemHeight) - Distance % maxLength;
                    if (tempDistance < -(maxLength - halfItemHeight))
                    {
                        tempDistance += maxLength;
                        if (isRepeat)
                        {
                            if (i == 0)
                            {
                                items[i].GetComponent<Image>().overrideSprite = items[items.Length - 1].GetComponent<Image>().overrideSprite;
                            }
                            else
                            {
                                items[i].GetComponent<Image>().overrideSprite = items[i - 1].GetComponent<Image>().overrideSprite;
                            }
                        }
                    }
                }
                items[i].anchoredPosition = new Vector2(items[i].anchoredPosition.x, tempDistance);
            }
        }


        private IEnumerator Rolling(float distance, float speed)
        {
            yield return new WaitForEndOfFrame();
            float tempDistance = Distance;
            float targetDistance = distance + Distance;
            while (state == State.Rolling)
            {
                var temp = Time.deltaTime * speed;
                tempDistance += temp;
                if (tempDistance <= targetDistance)
                {
                    DoMove(tempDistance);
                }
                else
                {
                    DoMove(targetDistance);
                    StartCoroutine(Rolling(distance, speed));
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        // Use this for initialization
        private void Start()
        {
            this.ExecuteNextFrame(delegate { GetComponent<VerticalLayoutGroup>().enabled = false; });
            items = new RectTransform[transform.childCount];
            for (var i = 0; i < transform.childCount; i++)
                items[i] = transform.GetChild(i).GetComponent<RectTransform>();
            maxLength = GetComponent<RectTransform>().rect.height;
            halfItemHeight = maxLength / items.Length / 2;
            oldPosition = items[0].anchoredPosition;
        }
    }
}