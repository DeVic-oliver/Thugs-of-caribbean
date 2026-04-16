using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TOC.Core.Enums;
using UnityEngine;

namespace TOC.Core.Items.LootSystem
{
    public abstract class RarityPoolBase<T> : ScriptableObject
    {
        [SerializeField, ReadOnly] private float m_Fail;
        [SerializeField] protected List<RarityDropRate> m_RarityLevel;

        protected virtual T RollChance()
        {
            CalculateDropFail(out float fail);
            fail /= 100f;

            if (fail >= 1f)
                return default;

            float rnd = 0;
            if (fail > 0)
            {
                rnd = UnityEngine.Random.value;
                if (rnd <= fail)
                    return default;
            }

            Dictionary<RarityLevel, float> weights = GetWeights();
            Dictionary<RarityLevel, List<T>> rarityItems = GetRarityPool();

            float total = 0;
            foreach (var item in m_RarityLevel)
                total += item.Rate;

            rnd = UnityEngine.Random.Range(0, total);
            float sum = 0;
            foreach (var item in weights)
            {
                sum += item.Value;
                if (rnd <= sum)
                    return rarityItems[item.Key].GetRandomItem();
            }

            return default;
        }

        protected Dictionary<RarityLevel, List<T>> GetRarityPool()
        {
            var pool = new Dictionary<RarityLevel, List<T>>();
            foreach (var item in m_RarityLevel)
            {
                if (pool.ContainsKey(item.Rarity))
                    pool[item.Rarity].AddRange(item.Pool);
                else
                    pool[item.Rarity] = item.Pool;
            }

            return pool;
        }

        protected Dictionary<RarityLevel, float> GetWeights()
        {
            var weights = new Dictionary<RarityLevel, float>();
            foreach (var item in m_RarityLevel)
            {
                if (weights.ContainsKey(item.Rarity))
                    weights[item.Rarity] += item.Rate;
                else
                    weights[item.Rarity] = item.Rate;
            }

            return weights;
        }

        private void OnValidate()
        {
            CalculateDropFail(out var x);
            m_Fail = x;
        }

        private void CalculateDropFail(out float x)
        {
            float fail = 100f;
            foreach (var item in m_RarityLevel)
                fail -= item.Rate;

            fail = fail <= 0 ? 0 : fail;
            x = fail;
        }

        [Serializable]
        public class RarityDropRate
        {
            #region Fields
            [SerializeField] protected RarityLevel m_Rarity;
            [SerializeField] protected float m_Rate;
            [SerializeField] protected List<T> m_Pool;
            #endregion

            #region Properties
            public RarityLevel Rarity => m_Rarity;
            public float Rate => m_Rate;
            public List<T> Pool => m_Pool;
            #endregion
        }
    }
}