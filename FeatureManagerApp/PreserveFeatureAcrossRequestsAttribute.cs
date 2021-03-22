using System;

namespace FeatureManagerApp
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class PreserveFeatureAcrossRequestsAttribute : Attribute
    {
    }
}
