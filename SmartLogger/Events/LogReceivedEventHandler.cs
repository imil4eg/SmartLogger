using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLogger.Events
{
    internal sealed class CallAndWaitEvent<TEventArgs> where TEventArgs : EventArgs
    {
        private readonly object _locker;

        private readonly List<Action<TEventArgs>> _invocationList;

        public CallAndWaitEvent()
        {
            _invocationList = new List<Action<TEventArgs>>();
            _locker = new object();
        }

        public void Add(Action<EventArgs> action)
        {
            lock (_locker)
            {
                _invocationList.Add(action);
            }
        }

        public void Remove(Action<EventArgs> action)
        {
            lock (_locker)
            {
                _invocationList.Remove(action);
            }
        }

        public void InvokeAllAndWait(TEventArgs eventArgs)
        {
            var invocationListTasks = new List<Task>(_invocationList.Count);
            foreach (var invocation in _invocationList)
            {
                var task = Task.Run(() => invocation(eventArgs));
                invocationListTasks.Add(task);
            }

            Task.WaitAll(invocationListTasks.ToArray());
        }

        public static CallAndWaitEvent<TEventArgs> operator +(CallAndWaitEvent<TEventArgs> e, Action<TEventArgs> action)
        {
            if (e == null)
            {
                return null;
            }

            lock (e._locker)
            {
                e._invocationList.Add(action);
            }

            return e;
        }

        public static CallAndWaitEvent<TEventArgs> operator -(CallAndWaitEvent<TEventArgs> e, Action<TEventArgs> action)
        {
            if (e == null)
                return null;

            lock (e._locker)
            {
                e._invocationList.Remove(action);
            }

            return e;
        }
    }
}
