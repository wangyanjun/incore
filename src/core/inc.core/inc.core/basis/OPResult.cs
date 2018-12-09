using System;

namespace inc.core
{
    /// <summary>
    /// Operation result
    /// </summary>
    public class OpResult
    {
        /// <summary>
        /// Get empty result
        /// </summary>
        public static OpResult Empty { get; } = new OpResult();

        /// <summary>
        /// Get or set error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Get or set message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set wether success
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Get content
        /// </summary>
        /// <returns>Additional content</returns>
        public virtual object GetContent() => null;

        /// <summary>
        /// 检查是否为真
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="falseMessage">检查值为假要设置的消息</param>
        /// <returns>检查结果</returns>
        public bool CheckTrue(bool value, string falseMessage)
        {
            var result = true;
            if (!value)
            {
                IsSuccess = false;
                Message = falseMessage;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 检查是否为假
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="trueMessage">检查值为真要设置的消息</param>
        /// <returns>检查结果</returns>
        public bool CheckFalse(bool value, string trueMessage) => CheckTrue(!value, trueMessage);

        /// <summary>
        /// 检查是否不为空
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="nullMessage">检查值为空要设置的消息</param>
        /// <returns>检查结果</returns>
        public bool CheckNotNull(object value, string nullMessage) => CheckTrue(value != null, nullMessage);

        /// <summary>
        /// 检查是否为空
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="notNullMessage">检查值为非空要设置的消息</param>
        /// <returns>检查结果</returns>
        public bool CheckNull(object value, string notNullMessage) => CheckTrue(value == null, notNullMessage);

        /// <summary>
        /// 检查是否不为空
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="nullMessage">检查值为空要设置的消息</param>
        /// <returns>检查结果</returns>
        public bool CheckNotNullOrWhiteSpace(string value, string nullMessage) => CheckTrue(!string.IsNullOrWhiteSpace(value), nullMessage);

        /// <summary>
        /// 检查结果
        /// </summary>
        /// <param name="result">结果</param>
        /// <returns>是否成功</returns>
        public bool CheckResult(OpResult result) => CheckTrue(result.IsSuccess, result.Message);

        /// <summary>
        /// 尝试执行动作
        /// </summary>
        /// <param name="action">要执行的动作</param>
        public void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsSuccess = false;
            }
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="context"></param>
        public void SetException(Exception ex, string context)
        {
            Message = context + ":" + ex.Message;
            IsSuccess = false;
        }

        /// <summary>
        /// 字符串输出
        /// </summary>
        /// <returns>字符串输出</returns>
        public override string ToString()
        {
            return IsSuccess ? SR.Success : Message;
        }
    }   
}
