using System.Linq;
using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 匹配对象管理组件
    /// </summary>
    public class MatcherComponent : Component
    {
        private readonly Dictionary<long, Matcher> matchers = new Dictionary<long, Matcher>();

        //匹配对象数量
        public int Count { get { return matchers.Count; } }

        /// <summary>
        /// 添加匹配对象
        /// </summary>
        /// <param name="matcher"></param>
        public void Add(Matcher matcher)
        {
            this.matchers.Add(matcher.UserID, matcher);
        }

        /// <summary>
        /// 获取匹配对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Matcher Get(long id)
        {
            this.matchers.TryGetValue(id, out Matcher matcher);
            return matcher;
        }

        /// <summary>
        /// 获取所有匹配对象
        /// </summary>
        /// <returns></returns>
        public Matcher[] GetAll()
        {
            return this.matchers.Values.ToArray();
        }

        /// <summary>
        /// 移除匹配对象并返回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Matcher Remove(long id)
        {
            Matcher matcher = Get(id);
            this.matchers.Remove(id);
            return matcher;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (var matcher in this.matchers.Values)
            {
                matcher.Dispose();
            }
        }
    }
}
