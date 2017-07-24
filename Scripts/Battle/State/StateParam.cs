
using System.Collections.Generic;

public class StateParam
{
    public CharacterInfo targetInfo;
    public SkillInfo skillInfo;
    public int towerId;
    public StateParam()
    {

    }
    public StateParam(CharacterInfo _targetInfo)
    {
        targetInfo = _targetInfo;
    }
    public StateParam(SkillInfo _skillInfo)
    {
        skillInfo = _skillInfo;
    }
    public StateParam(int _towerId)
    {
        towerId = _towerId;
    }
}
