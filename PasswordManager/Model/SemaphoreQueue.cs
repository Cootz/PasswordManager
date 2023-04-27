namespace PasswordManager.Model
{
    internal sealed class SemaphoreQueue : IDisposable
    {
        private readonly SemaphoreSlim semaphore;

        private bool disposed = false;

        public SemaphoreQueue(int parallelismLevel)
        {
            semaphore = new SemaphoreSlim(parallelismLevel);
        }

        public async void Enqueue(Func<Task> taskGenerator)
        {
            ObjectDisposedException.ThrowIf(disposed, this);

            await semaphore.WaitAsync();

            try
            {
                await taskGenerator();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                semaphore.Dispose();
                disposed = true;
            }
        }
    }
}