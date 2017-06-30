
using System.Collections.Generic;

public class StateParam
{
    public CharacterInfo targetInfo;
    public int towerId;
    public StateParam()
    {

    }
    public StateParam(CharacterInfo _targetInfo)
    {
        targetInfo = _targetInfo;
    }
    public StateParam(int _towerId)
    {
        towerId = _towerId;
    }
}
