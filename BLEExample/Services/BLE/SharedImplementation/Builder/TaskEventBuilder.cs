using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLEExample.Services.BLE.SharedCore.Builder
{
    /// <summary>
    /// Builder pattern for creating a task that invokes subscribable generic events with a generic return type
    /// </summary>
    public static class TaskEventBuilder
    {
        /// <summary>
        /// Method that builds a task which invokes subscrible events as defined in the 
        /// static constructor as generics
        /// </summary>
        /// <typeparam name="TReturn">Generic return type</typeparam>
        /// <typeparam name="TEventHandler">Generic event type</typeparam>
        /// <typeparam name="TRejectHandler">Generic error event type</typeparam>
        /// <param name="execute">Action to start execution of the task that was built</param>
        /// <param name="getCompleteHandler">delegate for the completion</param>
        /// <param name="subscribeComplete">subscribe to connection completed</param>
        /// <param name="unsubscribeComplete">unsubscribe to connection completed</param>
        /// <param name="getRejectHandler">delegate for connection rejected</param>
        /// <param name="subscribeReject">subscribe to connection rejected</param>
        /// <param name="unsubscribeReject">unsubscribe from connection rejected</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public static async Task<TReturn> BuildTask<TReturn, TEventHandler, TRejectHandler>(
            Action execute,
            Func<Action<TReturn>, Action<Exception>, TEventHandler> getCompleteHandler,
            Action<TEventHandler> subscribeComplete,
            Action<TEventHandler> unsubscribeComplete,
            Func<Action<Exception>, TRejectHandler> getRejectHandler,
            Action<TRejectHandler> subscribeReject,
            Action<TRejectHandler> unsubscribeReject,
            CancellationToken token = default)
        {
            var tcs = new TaskCompletionSource<TReturn>();
            void Complete(TReturn args) => tcs.TrySetResult(args);
            void CompleteException(Exception ex) => tcs.TrySetException(ex);
            void Reject(Exception ex) => tcs.TrySetException(ex);

            var handler = getCompleteHandler(Complete, CompleteException);
            var rejectHandler = getRejectHandler(Reject);

            try
            {
                subscribeComplete(handler);
                subscribeReject(rejectHandler);
                using (token.Register(() => tcs.TrySetCanceled(), false))
                {
                    execute();
                    return await (tcs?.Task ?? Task.FromResult(default(TReturn)));
                }
            }
            finally
            {
                unsubscribeReject(rejectHandler);
                unsubscribeComplete(handler);
            }
        }
    }
}
