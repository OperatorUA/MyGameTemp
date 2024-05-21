using UnityEngine;

public class LootBox : ItemsStorage
{
    private void Awake()
    {
        InitStorage(0);
    }
    protected override void OnStorageChanged()
    {
        base.OnStorageChanged();
        
        if (isEmpty())
        {
            StorageChanged.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}
