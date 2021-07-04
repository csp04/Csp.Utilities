using System;
using System.Text;

namespace Csp.Utilities
{
    public static class Disposable
    {

        public static IDisposable Create(Action onDispose) => new _(onDispose);

        /// <summary>
        /// A class helper for disposing.
        /// </summary>
        private sealed class _ : IDisposable
        {
            private readonly Action onDispose;

            public _(Action onDispose)
            {
                this.onDispose = onDispose;
            }

            public void Dispose()
            {
                onDispose();
            }
        }
    }
}
