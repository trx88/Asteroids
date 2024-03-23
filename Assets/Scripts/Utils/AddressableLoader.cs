using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RovioAsteroids.Utils
{
    public class AddressableLoader
    {
        public GameObject LoadGameObject(string addressableKey)
        {
            return AddressablesManager.LoadAssetSync<GameObject>(addressableKey);
        }

        public void ReleaseGameObject(string addressableKey)
        {
            AddressablesManager.ReleaseAsset(addressableKey);
        }
    }
}
