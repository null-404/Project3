using Project3.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Service.Interface
{
    public interface IActionCooldownService
    {
        /// <summary>
        /// 设置一个Action冷却（冷却时间内无法访问此Action）
        /// </summary>
        /// <param name="actionCooldownType">Action 冷却类型</param>
        /// <param name="ip">用户Ip</param>
        void SetCooldown(ActionCooldownType actionCooldownType, string ip);

        /// <summary>
        /// 是否冷却完成
        /// </summary>
        /// <param name="actionCooldownType"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool IsCoolingCompletion(ActionCooldownType actionCooldownType, string ip);
    }
}
