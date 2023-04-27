using PasswordManager.Utils;
using SharpHook;
using SharpHook.Native;

namespace PasswordManager.Model
{
    public sealed class OptimizedTaskPoolGlobalHook : GlobalHookBase
    {
        private readonly SemaphoreQueue taskQueue;

        /// <summary>
        /// Initializes a new instance of <see cref="OptimizedTaskPoolGlobalHook" />.
        /// </summary>
        /// <remarks>
        /// Calling this constructor is the same as passing <see cref="TaskPoolGlobalHookOptions.Sequential" /> to the
        /// other constructor.
        /// </remarks>
        public OptimizedTaskPoolGlobalHook()
            : this(TaskPoolGlobalHookOptions.Sequential)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OptimizedTaskPoolGlobalHook" />.
        /// </summary>
        /// <param name="options">The options of the hook which include its parallelism level.</param>
        public OptimizedTaskPoolGlobalHook(TaskPoolGlobalHookOptions options)
            : base(options.RunAsyncOnBackgroundThread)
        {
            taskQueue = new SemaphoreQueue(options.ParallelismLevel);
        }

        /// <summary>
        /// Handles the hook event.
        /// </summary>
        /// <param name="e">The event to handle.</param>
        protected override void HandleHookEvent(ref UioHookEvent e)
        {
            if (OptimizationHelper.IsAppActive)
            {
                if (e.Type != EventType.HookDisabled)
                {
                    UioHookEvent copy = e;
                    taskQueue.Enqueue(() => Task.Run(() => DispatchEvent(ref copy)));
                }
                else
                {
                    DispatchEvent(ref e);
                }
            }
        }

        /// <summary>
        /// Destoys the global hook.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true" /> if the method is called from the <see cref="IDisposable.Dispose()" /> method.
        /// Otherwise, <see langword="false" />.
        /// </param>
        /// <exception cref="HookException">Stopping the hook has failed.</exception>
        /// <remarks>
        /// After calling this method, the hook cannot be started again. If you want to do that, create a new instance
        /// of <see cref="IGlobalHook" />.
        /// </remarks>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                taskQueue.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}