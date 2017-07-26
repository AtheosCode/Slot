using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LightRoll : MonoBehaviour
    {
        public Image[] itemsImages;
        public float speed;
        private int currentLight = 0;
        private State state = State.Static;

        public enum State
        {
            Static,
            Rolling,
            TweenEnding,
        }

        public int CurrentLight
        {
            get
            {
                return currentLight;
            }

            set
            {
                currentLight = value;
                ChangeLight();
            }
        }
        public void StartRoll()
        {
            if (state == State.Static)
            {
                state = State.Rolling;
                StartCoroutine(Rolling());
            }
        }

        public void TweenRoll(int value, float time = 1.5f)
        {
            if (state == State.Rolling)
            {
                state = State.TweenEnding;
                float tempValue = CurrentLight;
                DOTween.To((float vlaue) =>
                {
                    if (vlaue - tempValue >= 0.5)
                    {
                        tempValue += 0.5f;
                        if (CurrentLight != Mathf.FloorToInt(tempValue % itemsImages.Length))
                        {
                            CurrentLight = Mathf.FloorToInt(tempValue % itemsImages.Length);
                        }
                    }
                }, CurrentLight, value + (10 * itemsImages.Length), time).SetEase(Ease.OutCubic).OnComplete(
                    () =>
                    {
                        CurrentLight = value;
                        state = State.Static;
                    }
                );
            }
        }

        private void ChangeLight()
        {
            for (int i = 0; i < itemsImages.Length; i++)
            {
                itemsImages[i].enabled = (i == CurrentLight);
            }
        }

        private IEnumerator Rolling()
        {
            yield return new WaitForEndOfFrame();
            float tempValue = 0;
            while (state == State.Rolling)
            {
                tempValue += Time.deltaTime * speed;
                int intValue = Mathf.FloorToInt(tempValue % itemsImages.Length);
                if (intValue != CurrentLight)
                {
                    CurrentLight = intValue;
                }
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        void Start()
        {
            itemsImages = new Image[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                itemsImages[i] = transform.GetChild(i).GetComponent<Image>();
            }
        }
    }
}
