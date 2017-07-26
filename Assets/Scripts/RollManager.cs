using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RollManager : MonoSingleton<RollManager>
    {
        RollUnit _rollUnit;
        public RollUnit RollUnit
        {
            get
            {
                return _rollUnit;
            }
            set
            {
                _rollUnit = value;
            }
        }

        public bool isRun = false;
        public bool isStop = false;
        public const float INERVAL_TIME = 0.1f;
        public const float LIGHT_SPEED = 0.1f;
        int[] lightid = new int[4];
        string[] itemchange = new string[4];

        private IEnumerator AutoRun()
        {
            while (isRun)
            {
                RollUnit.NumberRound();
                RollUnit.LetterRound();
                yield return new WaitForSeconds(INERVAL_TIME);
            }
        }

        private IEnumerator Light1On()
        {
            while (isRun)
            {
                RollUnit.Lights[0].transform.GetChild(0).gameObject.SetActive(true);
                lightid[0] = 1;
                yield return new WaitForSeconds(LIGHT_SPEED);
                RollUnit.Lights[0].transform.GetChild(0).gameObject.SetActive(false);
                RollUnit.Lights[0].transform.GetChild(1).gameObject.SetActive(true);
                lightid[0] = 2;
                yield return new WaitForSeconds(LIGHT_SPEED);
                RollUnit.Lights[0].transform.GetChild(1).gameObject.SetActive(false);
                RollUnit.Lights[0].transform.GetChild(2).gameObject.SetActive(true);
                lightid[0] = 3;
                yield return new WaitForSeconds(LIGHT_SPEED);
                RollUnit.Lights[0].transform.GetChild(2).gameObject.SetActive(false);
                RollUnit.Lights[0].transform.GetChild(3).gameObject.SetActive(true);
                lightid[0] = 4;
                yield return new WaitForSeconds(LIGHT_SPEED);
                RollUnit.Lights[0].transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        private IEnumerator Light2On()
        {
            while (isRun)
            {
                RollUnit.Lights[1].transform.GetChild(0).gameObject.SetActive(true);
                lightid[1] = 1;
                yield return new WaitForSeconds(0.2f);
                RollUnit.Lights[1].transform.GetChild(0).gameObject.SetActive(false);
                RollUnit.Lights[1].transform.GetChild(1).gameObject.SetActive(true);
                lightid[1] = 2;
                yield return new WaitForSeconds(0.2f);
                RollUnit.Lights[1].transform.GetChild(1).gameObject.SetActive(false);
                RollUnit.Lights[1].transform.GetChild(2).gameObject.SetActive(true);
                lightid[1] = 3;
                yield return new WaitForSeconds(0.2f);
                RollUnit.Lights[1].transform.GetChild(2).gameObject.SetActive(false);
                RollUnit.Lights[1].transform.GetChild(3).gameObject.SetActive(true);
                lightid[1] = 4;
                yield return new WaitForSeconds(0.2f);
                RollUnit.Lights[1].transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        private IEnumerator Light3On()
        {
            while (isRun)
            {
                RollUnit.Lights[2].transform.GetChild(0).gameObject.SetActive(true);
                lightid[2] = 1;
                yield return new WaitForSeconds(0.23f);
                RollUnit.Lights[2].transform.GetChild(0).gameObject.SetActive(false);
                RollUnit.Lights[2].transform.GetChild(1).gameObject.SetActive(true);
                lightid[2] = 2;
                yield return new WaitForSeconds(0.23f);
                RollUnit.Lights[2].transform.GetChild(1).gameObject.SetActive(false);
                RollUnit.Lights[2].transform.GetChild(2).gameObject.SetActive(true);
                lightid[2] = 3;
                yield return new WaitForSeconds(0.23f);
                RollUnit.Lights[2].transform.GetChild(2).gameObject.SetActive(false);
                RollUnit.Lights[2].transform.GetChild(3).gameObject.SetActive(true);
                lightid[2] = 4;
                yield return new WaitForSeconds(0.23f);
                RollUnit.Lights[2].transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        private IEnumerator Light4On()
        {
            while (isRun)
            {
                RollUnit.Lights[3].transform.GetChild(0).gameObject.SetActive(true);
                lightid[3] = 1;
                yield return new WaitForSeconds(0.15f);
                RollUnit.Lights[3].transform.GetChild(0).gameObject.SetActive(false);
                RollUnit.Lights[3].transform.GetChild(1).gameObject.SetActive(true);
                lightid[3] = 2;
                yield return new WaitForSeconds(0.15f);
                RollUnit.Lights[3].transform.GetChild(1).gameObject.SetActive(false);
                RollUnit.Lights[3].transform.GetChild(2).gameObject.SetActive(true);
                lightid[3] = 3;
                yield return new WaitForSeconds(0.15f);
                RollUnit.Lights[3].transform.GetChild(2).gameObject.SetActive(false);
                RollUnit.Lights[3].transform.GetChild(3).gameObject.SetActive(true);
                lightid[3] = 4;
                yield return new WaitForSeconds(0.15f);
                RollUnit.Lights[3].transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        public void Run()
        {
            StartCoroutine("AutoRun");
            StartCoroutine("Light1On");
            StartCoroutine("Light2On");
            StartCoroutine("Light3On");
            StartCoroutine("Light4On");
        }
        public void Stop()
        {
            StopCoroutine("AutoRun");
            StopCoroutine("Light1On");
            StopCoroutine("Light2On");
            StopCoroutine("Light3On");
            StopCoroutine("Light4On");

            for (int i = 0; i < 4; i++)
            {
                if (lightid[i] == 1)
                {

                }
            }
        }
        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
