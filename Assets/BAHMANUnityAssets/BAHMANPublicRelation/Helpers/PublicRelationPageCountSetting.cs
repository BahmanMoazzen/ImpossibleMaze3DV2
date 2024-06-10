using System;
[Serializable]
public class PublicRelationPageCountSetting 
{
    public string SceneName;
    public PublicRelationType RelationType;
    public int SceneCount;
}

public enum PublicRelationType { Share, Rate, OtherProduct, Donate, Message }

