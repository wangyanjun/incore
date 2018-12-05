using System.Threading;

namespace inc.core
{
    public static class ThreadHelper
    {
        /// <summary>
        /// 安全关闭
        /// </summary>
        /// <param name="mre">要关闭的对象</param>
        public static void SafeDispose(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.Close();
            }
        }

        /// <summary>
        /// 安全关闭
        /// </summary>
        /// <param name="mre">要关闭的对象</param>
        public static void SafeSet(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.Close();
            }
        }

        /// <summary>
        /// 安全关闭
        /// </summary>
        /// <param name="mre">要关闭的对象</param>
        public static void SafeWait(this EventWaitHandle mre, int milliseconds)
        {
            if ((milliseconds > 0) && (mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.WaitOne(milliseconds);
            }
        }

        /// <summary>
        /// 安全关闭
        /// </summary>
        /// <param name="mre">要关闭的对象</param>
        public static void SafeWait(this EventWaitHandle mre)
        {
            if ((mre != null) && (!mre.SafeWaitHandle.IsClosed) && (!mre.SafeWaitHandle.IsInvalid))
            {
                mre.WaitOne();
            }
        }
    }
}
