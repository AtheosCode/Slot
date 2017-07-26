using System;
using System.Collections.Generic;
using System.Diagnostics;
using Boo.Lang.Runtime.DynamicDispatching;
using NUnit.Framework.Constraints;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public enum ExchangeEnum
        {
            A_U = 0,
            A_D,
            B_U,
            B_D,
            C_U,
            C_D,
            D_U,
            D_D,
            E_U,
            E_D,
            F_U,
            F_D,
            G_U,
            G_D,
            H_U,
            H_D,
        }

        /// <summary>
        /// 随机区域的值
        /// </summary>
        public int[][] randomNumbers = new int[8][];
        /// <summary>
        /// 计算区域的值
        /// </summary>
        public int[][] valueNumbers = new int[16][];
        /// <summary>
        /// 跑马灯的值
        /// </summary>
        public int[] lights = new int[4];

        public ExchangeEnum[] exchangeEnums = new ExchangeEnum[12];

        /// <summary>
        /// 记录红灯 与 要调换的竖列的关系
        /// </summary>
        public Dictionary<int, ExchangeEnum> exChangedDictionary = new Dictionary<int, ExchangeEnum>();

        /// <summary>
        /// 最大值
        /// </summary>
        private readonly int MaxValue = 8;
        private readonly int LightMaxValue = 4;


        protected override void Init()
        {
            base.Init();
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = new int[4];
            }
            for (int i = 0; i < valueNumbers.Length; i++)
            {
                valueNumbers[i] = new int[3];
            }
            CalculateResult();
        }

        public void CalculateResult()
        {
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                for (int j = 0; j < randomNumbers[i].Length; j++)
                {
                    randomNumbers[i][j] = Random.Range(0, MaxValue);
                }
            }
            for (int i = 0; i < valueNumbers.Length; i++)
            {
                for (int j = 0; j < valueNumbers[i].Length; j++)
                {
                    valueNumbers[i][j] = Random.Range(0, MaxValue);
                }
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i] = Random.Range(0, LightMaxValue);
            }
            for (int i = 0; i < exchangeEnums.Length; i++)
            {
                exchangeEnums[i] = (ExchangeEnum)Random.Range(0, 16);
            }

            exChangedDictionary.Clear();
            for (int i = 0; i < 4; i++)
            {
                int index = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (j != lights[i])
                    {
                        exChangedDictionary.Add(j + i * 4, exchangeEnums[i * 3 + index]);
                        index += 1;
                    }
                }
            }
        }
    }

}
