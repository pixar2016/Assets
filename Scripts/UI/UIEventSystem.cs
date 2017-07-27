
using System.Collections.Generic;

public class UIEventSystem
{
    private static UIEventSystem _instance;
    public static UIEventSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIEventSystem();
            }
            return _instance;
        }
    }

    private MiniEventDispatcher eventDispatcher;

    private UIEventSystem()
    {
        eventDispatcher = new MiniEventDispatcher();
    }

    public void Register(string eventKey, MiniEventDispatcher.CommonEvent attachEvent)
    {
        eventDispatcher.Register(eventKey, attachEvent);
    }

    public void Remove(string eventKey, MiniEventDispatcher.CommonEvent attachEvent)
    {
        eventDispatcher.Remove(eventKey, attachEvent);
    }

    public void Broadcast(string eventKey, params object[] data)
    {
        eventDispatcher.Broadcast(eventKey, data);
    }
}

