using System;

/// <summary>
/// 管理所有定时器
/// </summary>
public class TimeManager : Singleton<TimeManager>
{

    public event Action<float> TimerLoopCallback;

    /// <summary>
    /// 创建计时器
    /// </summary>
    /// <param name="intervalTime">间隔时间</param>
    /// <param name="repeatTimes">重复次数(小于0，表示无限循环)</param>
    /// <param name="callBack">需要回调的函数</param>
    /// <returns></returns>
    public Timer CreateTimer(float intervalTime, int repeatTimes, Action callBack)
    {
        Timer timer = new Timer();
        timer.intervalTime = intervalTime;
        timer.repeatTimes = repeatTimes;
        timer.callBack = callBack;
        return timer;
    }

    /// <summary>
    /// 驱动计时器
    /// </summary>
    /// <param name="deltaTime">计时器驱动间隔</param>
    public void Loop(float deltaTime)
    {
        if (TimerLoopCallback != null)
        {
            TimerLoopCallback(deltaTime);
        }
    }

}

/// <summary>
/// 定时器
/// </summary>
public class Timer
{

    /// <summary>
    /// 间隔时间
    /// </summary>
    public float intervalTime;
    /// <summary>
    /// 重复次数
    /// </summary>
    public int repeatTimes;
    /// <summary>
    /// 回调函数
    /// </summary>
    public Action callBack;
    /// <summary>
    /// 计时的持续时间
    /// </summary>
    private float _duringTime;
    /// <summary>
    /// 已经执行的次数
    /// </summary>
    private int _repeatedTimes;

    public bool IsRunning = false;
    //开启
    public void Start()
    {
        IsRunning = true;
        TimeManager.instance.TimerLoopCallback += this.Loop;
    }

    //结束
    public void Pause()
    {
        IsRunning = false;
        TimeManager.instance.TimerLoopCallback -= this.Loop;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    private void Reset()
    {
        Pause();
        _duringTime = 0;
        _repeatedTimes = 0;
    }

    public void Loop(float deltaTime)
    {
        _duringTime += deltaTime;
        //判断间隔时间
        if (_duringTime > intervalTime || Util.FloatEquals(_duringTime, intervalTime)) 
        {
            _repeatedTimes++;
            _duringTime = _duringTime - intervalTime;
            callBack?.Invoke();
            if (repeatTimes > 0
                && _repeatedTimes >= repeatTimes) 
            {
                Pause();
            }
        }
    }

}