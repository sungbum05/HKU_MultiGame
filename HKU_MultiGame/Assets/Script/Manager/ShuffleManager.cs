using System;
using System.Collections.Generic;
using System.Threading;

//랜덤 함수 정의
public static class ShuffleManager
{
    [ThreadStatic] private static Random Local;

    public static Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

//위 함수 사용하여 셔플 알고리즘 사용
static class MyExtesions
{
    public static void Shuffle<T> (this IList<T> list)
    {
        int n = list.Count;
        while(n > 1)
        {
            n--;
            int k = ShuffleManager.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
