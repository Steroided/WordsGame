using System;
using System.Threading.Tasks;

public static class Extensions
{
    public static async Task<TResult> TimeoutWithResult<TResult>(this Task<TResult> task, int timeoutMs)
    {
        var completed = await Task.WhenAny(task, Task.Delay(timeoutMs));

        if (task.IsFaulted)
            throw task.Exception.GetBaseException();

        if (completed == task && task.IsCompleted)
        {
            return task.Result;
        }

        throw new TimeoutException();
    }
}
