﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;

namespace FeatureManagerApp
{
    public class HttpContextFeatureSessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKeyPrefix = "feature_flag_";
        public HttpContextFeatureSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<bool?> GetAsync(string featureName)
        {
            bool keyExistsInHttpSession = _httpContextAccessor.HttpContext.Session.TryGetValue(key: $"{SessionKeyPrefix}{featureName}", value: out byte[] bytes);
            if (keyExistsInHttpSession)
            {
                bool isFeatureEnabledInSession = BitConverter.ToBoolean(bytes);
                return Task.FromResult<bool?>(isFeatureEnabledInSession);
            }
            return Task.FromResult<bool?>(null);
        }

        public Task SetAsync(string featureName, bool enabled)
        {
            if (ShouldPreserveAcrossRequests(featureName))
            {
                _httpContextAccessor.HttpContext.Session.Set(key: $"{SessionKeyPrefix}{featureName}", value: BitConverter.GetBytes(enabled));
            }
            return Task.CompletedTask;
        }
        private static bool ShouldPreserveAcrossRequests(string featureName)
        {
            MemberInfo enumFieldInfo = typeof(Models.FeatureFlags).GetMember(featureName).First();
            return enumFieldInfo.GetCustomAttributes(typeof(PreserveFeatureAcrossRequestsAttribute)).Any();
        }
    }
}
