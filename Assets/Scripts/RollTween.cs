using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RollTween : MonoBehaviour
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

        public float speed = 20;

        private State state = State.Static;

        public bool IsRepeat { get; set; }
        /// <summary>
        /// False 下滑  True 上滑
        /// </summary>
        public bool Direct { get; set; }
        private float Distance { get; set; }


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
        public void TweenRoll(float time = 1.5f, Action callback = null, int indexMaxLength = 0,
            float tweenDistance = 0)
        {
            if (state != State.TweenEnding)
            {
                state = State.TweenEnding;
                var distance = 0f;
                var nowPositon = items[0].anchoredPosition;
                if (nowPositon.y > oldPosition.y)
                    distance = Mathf.Abs(halfItemHeight - nowPositon.y) +
                               (GetComponent<VerticalLayoutGroup>().enabled ? 0 : halfItemHeight) + tweenDistance +
                               maxLength * indexMaxLength;
                else
                    distance = Mathf.Abs(-maxLength - nowPositon.y + oldPosition.y) +
                               (GetComponent<VerticalLayoutGroup>().enabled
                                   ? 0
                                   : halfItemHeight + tweenDistance + maxLength * indexMaxLength);
                Distance = 0;
                DOTween.To(value =>
                {
                    var temp = value - Distance;
                    DoMove(temp);
                    Distance = value;
                    //}, 0f, distance + maxLength * 2, time).SetEase(Ease.OutCubic).OnComplete(() =>
                }, 0f, distance, time).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    state = State.Static;
                    if (callback != null)
                        callback.Invoke();
                });
            }
        }

        private void DoMove(float value)
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i].anchoredPosition -= new Vector2(0f, value);
                if (items[i].anchoredPosition.y <= -maxLength + halfItemHeight)
                {
                    items[i].anchoredPosition += new Vector2(0f, maxLength);
                    if (IsRepeat)
                        if (i == 0)
                            items[i].GetComponent<Image>().overrideSprite =
                                items[items.Length - 1].GetComponent<Image>().overrideSprite;
                        else
                            items[i].GetComponent<Image>().overrideSprite =
                                items[i - 1].GetComponent<Image>().overrideSprite;
                }
            }
        }


        private IEnumerator Rolling(float distance, float speed)
        {
            yield return new WaitForEndOfFrame();
            float tempDistance = 0;
            while (state == State.Rolling)
            {
                var temp = Time.fixedDeltaTime * speed;
                tempDistance += temp;
                if (tempDistance <= distance)
                {
                    DoMove(temp);
                }
                else
                {
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

        #region   new

        private float NewDistance { get; set; }

        public void NewStartRoll()
        {
            if (state == State.Static)
            {
                state = State.Rolling;
                StartCoroutine(NewRolling(maxLength, speed));
            }
        }

        private IEnumerator NewRolling(float distance, float speed)
        {
            yield return new WaitForEndOfFrame();
            float tempDistance = 0;
            while (state == State.Rolling)
            {
                var temp = Time.fixedDeltaTime * speed;
                tempDistance += temp;
                if (tempDistance <= distance)
                {
                    NewDoMove(tempDistance);
                }
                else
                {
                    StartCoroutine(NewRolling(distance, speed));
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        public void NewTweenRoll(float time = 1.5f, Action callback = null, float tweenDistance = 0)
        {
            if (state == State.Rolling)
            {
                state = State.TweenEnding;
                var distance = 0f;
                var nowPositon = items[0].anchoredPosition;
                if (nowPositon.y > oldPosition.y)
                    distance = Mathf.Abs(halfItemHeight - nowPositon.y) +
                               (GetComponent<VerticalLayoutGroup>().enabled ? 0 : halfItemHeight) + tweenDistance;
                else
                    distance = Mathf.Abs(-maxLength - nowPositon.y + oldPosition.y) +
                               (GetComponent<VerticalLayoutGroup>().enabled ? 0 : halfItemHeight + tweenDistance);
                NewDistance = 0;
                DOTween.To(value => { NewDoMove(value); }, NewDistance, NewDistance + distance + maxLength * 2, time)
                    .SetEase(Ease.OutCubic).OnComplete(() =>
                    {
                        state = State.Static;
                        if (callback != null)
                            callback.Invoke();
                    });
            }
        }

        private void NewDoMove(float distance)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var value = distance % maxLength;
                var tempValue = Direct ? oldPosition.y + value : oldPosition.y - value;
                items[i].anchoredPosition = new Vector2(oldPosition.x, tempValue);
                if (items[i].anchoredPosition.y <= -maxLength + halfItemHeight)
                    items[i].anchoredPosition += new Vector2(oldPosition.x, maxLength);
                else if (items[i].anchoredPosition.y >= halfItemHeight)
                    items[i].anchoredPosition -= new Vector2(oldPosition.x, maxLength);
                NewDistance = distance;
            }
        }

        #endregion
    }
}