using UnityEngine;

namespace Emre
{
    public static class Balance
    {
        private const string CoinAmountKey = "Coin_Amount";


        public static int CoinAmount
        {
            get => PlayerPrefs.GetInt(CoinAmountKey, 0);
            private set
            {
                PlayerPrefs.SetInt(CoinAmountKey, value);
                GameEvents.RaiseCoinAmountChanged(value);
            }
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
    }
}