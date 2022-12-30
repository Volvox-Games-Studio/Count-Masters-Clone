using UnityEngine;

namespace Emre
{
    public static class Balance
    {
        private const string CoinAmountKey = "Coin_Amount";
        private const string GemAmountKey = "Gem_Amount";


        public static int CoinAmount
        {
            get => PlayerPrefs.GetInt(CoinAmountKey, 0);
            private set
            {
                PlayerPrefs.SetInt(CoinAmountKey, value);
                GameEvents.RaiseCoinAmountChanged(value);
            }
        }
        
        public static int GemAmount
        {
            get => PlayerPrefs.GetInt(GemAmountKey, 0);
            private set
            {
                PlayerPrefs.SetInt(GemAmountKey, value);
                GameEvents.RaiseGemAmountChanged(value);
            }
        }


        public static bool HasEnoughCoin(int amount)
        {
            return CoinAmount >= amount;
        }
        
        public static void AddCoin(int amount)
        {
            CoinAmount += amount;
        }

        public static bool TryRemoveCoin(int amount)
        {
            var coinAmount = CoinAmount;

            if (amount > coinAmount) return false;

            CoinAmount = coinAmount - amount;
            return true;
        }

        public static bool HasEnoughGem(int amount)
        {
            return GemAmount >= amount;
        }
        
        public static void AddGem(int amount)
        {
            GemAmount += amount;
        }

        public static bool TryRemoveGem(int amount)
        {
            var gemAmount = GemAmount;

            if (amount > gemAmount) return false;

            GemAmount = gemAmount - amount;
            return true;
        }
    }
}