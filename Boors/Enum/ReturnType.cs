using System.ComponentModel;

namespace InstaMarket.Web.Core.Enum
{
    public enum ReturnType
    {
        [Description("پاسخ صحیح")]
        Ok = 0,
        [Description("اشتباه منطقی")]
        Exception = 1,
        [Description("اشتباه غیر منطقی")]
        UnknownException = 2,
    }
}
