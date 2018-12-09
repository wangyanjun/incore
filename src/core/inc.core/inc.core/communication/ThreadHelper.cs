using System.Threading;

namespace inc.core
{
    /// <summary>
    /// The thread related helper
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// Safe close wait handle
        /// </summary>
        /// <param name="mre">The object to be closed</param>
        public static void SafeDispose(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.Close();
            }
        }

        /// <summary>
        /// Safe set event wait handle
        /// </summary>
        /// <param name="mre">The object to be set</param>
        public static void SafeSet(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.Close();
            }
        }

        /// <summary>
        /// Saft wait event wait handle
        /// </summary>
        /// <param name="mre">The object to be wait</param>
        /// <param name="milliseconds">wait space</param>
        public static void SafeWait(this EventWaitHandle mre, int milliseconds)
        {
            if ((milliseconds > 0) && (mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.WaitOne(milliseconds);
            }
        }

        /// <summary>
        /// Saft wait event wait handle
        /// </summary>
        /// <param name="mre">The object to be wait</param>
        public static void SafeWait(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.WaitOne();
            }
        }
    }
}
