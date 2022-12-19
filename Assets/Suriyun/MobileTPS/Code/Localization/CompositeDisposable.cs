using System;
using System.Collections.Generic;

namespace Suriyun.MobileTPS
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }
    }
}