using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

// https://msdn.microsoft.com/ru-ru/library/dd997371(v=vs.110).aspx
// 

namespace StartingPCL.Helpers
{
    public class TransformQueueEventArgs : EventArgs
    {
        public TransformQueueEventArgs(int queueLength)
        {
            QueueLength = queueLength;
        }

        public int QueueLength { get; }
    }

    public class TransformQueue<TKey, TInput, TOutput>
        where TOutput: class
    {
        public delegate Task<TOutput> QueueTransformAsync(TKey key, TInput input);

        class QueueItem
        {
            public TKey Key { get; }
            public TInput Input { get; }

            public QueueTransformAsync QueueTransformAsync { get; }

            public TaskCompletionSource<TOutput> TaskCompletionSource { get; }

            public QueueItem(TKey key, TInput input, QueueTransformAsync queueTransformAsync)
            {
                Key = key;
                Input = input;
                QueueTransformAsync = queueTransformAsync;
                TaskCompletionSource = new TaskCompletionSource<TOutput>();
            }
        }

        public static TransformQueue<TKey, TInput, TOutput> Default = new TransformQueue<TKey, TInput, TOutput>();
        private BlockingCollection<QueueItem> Queue { get; } = new BlockingCollection<QueueItem>();

        public event EventHandler<TransformQueueEventArgs> QueueChanged;

        public TransformQueue()
        {
            Task.Factory.StartNew(ProcessQueue, TaskCreationOptions.LongRunning);
        }

        private async Task ProcessQueue()
        {
            Log.Info("Begin Queue processing");

            QueueItem item;
            while (Queue.TryTake(out item, Timeout.Infinite/*, cts.Token*/))
            {
                QueueChanged?.Invoke(this, new TransformQueueEventArgs(Queue.Count));

                Log.Info($"Process item: {item.Key} ... (count: {Queue.Count})");
                var tcs = item.TaskCompletionSource;

                if (tcs.Task.IsCanceled || tcs.Task.IsCompleted || tcs.Task.IsFaulted)
                {
                    Log.Info($"  canceled: {item.Key}");
                    continue;
                }

                try
                {
                    var result = await item.QueueTransformAsync(item.Key, item.Input);
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            Log.Info("End Queue processing");
        }

        public Task<TOutput> EnqueueTransform(TKey key, TInput input, QueueTransformAsync transformAsync)
        {
            foreach (var anItem in Queue)
            {
                if (anItem.Key.Equals(key))
                {
                    Log.Info($"Task with key {key} already present");
                    return anItem.TaskCompletionSource.Task;
                }
            }

            var item = new QueueItem(key, input, transformAsync);
            Queue.Add(item);

            QueueChanged?.Invoke(this, new TransformQueueEventArgs(Queue.Count));

            return item.TaskCompletionSource.Task;
        }

        public void RemoveTransform(TKey key)
        {
            Log.Info($"Begin Removing task: {key}");

            foreach (var item in Queue)
            {
                var task = item.TaskCompletionSource.Task;
                if (task.IsCanceled || task.IsCompleted || task.IsFaulted)
                    continue;

                if (item.Key.Equals(key))
                {
                    Log.Info($"Cancel task: {item.Key}");
//                    item.TaskCompletionSource.TrySetCanceled();
                    item.TaskCompletionSource.TrySetResult(null);
                }
            }

            Log.Info($"End Removing task: {key}");
        }

        public void Clear()
        {
            foreach (var item in Queue)
            {
                var task = item.TaskCompletionSource.Task;
                if (task.IsCanceled || task.IsCompleted || task.IsFaulted)
                    continue;

                Log.Info($"Cancel job: {item.Key}");
//                item.TaskCompletionSource.TrySetCanceled();
                item.TaskCompletionSource.TrySetResult(null);
            }
        }
    }


}