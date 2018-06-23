using Project3.Data.Enums;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Service
{
    public class ActionCooldownService : IActionCooldownService
    {
        private readonly ICache _cache;
        private readonly IOptionsCache _optionsCache;
        public ActionCooldownService(ICache cache,IOptionsCache _optionsCache)
        {
            this._cache = cache;
            this._optionsCache = _optionsCache;
        }

        public void SetCooldown(ActionCooldownType actionCooldownType, string ip)
        {
            long cooldowntime = 0;
            var options = _optionsCache.Get().Result;
            switch (actionCooldownType)
            {
                case ActionCooldownType.CommentsPost:
                    cooldowntime = long.Parse(options.postcommentscd);
                    break;
            }
            _cache.Set(actionCooldownType + ip, "0", cooldowntime);
        }

        public bool IsCoolingCompletion(ActionCooldownType actionCooldownType, string ip)
        {
            return _cache.Get(actionCooldownType + ip) == null ? true : false;
        }

    }
}
