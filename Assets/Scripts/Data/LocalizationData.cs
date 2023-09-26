[System.Serializable]
public class LocalizationData
{
    public LocalizationItems[] items;
}

[System.Serializable]
public class LocalizationItems
{
    public string key;
    public string value;
}