using System;
using System.Collections.Generic;

namespace Csp.Utilities
{
    public sealed class CompositeDisposable : IDisposable
    {
        private List<IDisposable> disposables = new List<IDisposable>();

        public CompositeDisposable(params IDisposable[] disposables) => this.disposables.AddRange(disposables);

        public void Add(IDisposable d) => disposables.Add(d);

        public void Add(params IDisposable[] disposables) => this.disposables.AddRange(disposables);

        public void AddPrior(IDisposable d) => disposables.Insert(0, d);

        public void Dispose()
        {
            foreach(var d in disposables)
                d.Dispose();
        }
    }
}
