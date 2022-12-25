// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.Enums
{
    /// <summary>
    /// 异常状态码
    /// 详细提示配置到:exceptionmessagesettings.json/ErrorCodeMessageSettings
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 身份验证失败
        /// </summary>
        [Description("身份验证失败")]
        UNAUTHORIZED,
        /// <summary>
        /// 拒绝访问资源
        /// </summary>
        [Description("拒绝访问资源")]
        FORBIDDEN,
        /// <summary>
        /// 用户锁定
        /// </summary>
        [Description("用户锁定")]
        USER_LOCKED,
        /// <summary>
        /// 用户密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        USER_NAME_OR_PASSWORD_ERROR,
        /// <summary>
        /// 验证码验证失败
        /// </summary>
        [Description("验证码验证失败")]
        VERIFY_CODE_VERIFICATION_FAILED,
        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        USER_NAME_REPEAT,
        /// <summary>
        /// 资源键值重复
        /// </summary>
        [Description("资源键值重复")]
        RESOURCE_KEY_REPEAT,
        /// <summary>
        /// 刷新token不存在或已过期
        /// </summary>
        [Description("刷新token不存在或已过期")]
        REFRESHTOKEN_NO_EXIST_OR_EXPIRE,
        /// <summary>
        /// 未包含文件
        /// </summary>
        [Description("未包含文件")]
        NO_INCLUD_FILE,
        /// <summary>
        /// 条件组中的操作类型错误
        /// </summary>
        [Description("条件组中的操作类型错误")]
        FILTER_GROUP_OPERATE_ERROR,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("指定的属性“{0}”在类型“{1}”中不存在")]
        FIELD_IN_TYPE_NOT_FOUND,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        [Description("查询的值类型“{0}”未找到转换器")]
        QUERY_VALUE_TYPE_NO_FIND_CONVERTER,
        /// <summary>
        /// 请求的地址无效
        /// </summary>
        [Description("请求的地址无效")]
        REQUEST_URL_IS_INVALID,
        /// <summary>
        /// 刷新token不能用于鉴权
        /// </summary>
        [Description("刷新token不能用于鉴权")]
        REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION,
        /// <summary>
        /// TOKEN无效
        /// </summary>
        [Description("TOKEN无效")]
        TOKEN_INVALID,
        /// <summary>
        /// 客户端登录失败
        /// </summary>
        [Description("客户端登录失败")]
        CLIENT_LOGIN_FAIL,
        /// <summary>
        /// 客户端未找到
        /// </summary>
        [Description("客户端未找到")]
        CLIENT_NO_FIND,
        /// <summary>
        /// 时间戳已过期
        /// </summary>
        [Description("时间戳已过期")]
        TIMESPAN_IS_EXPIRED,
        /// <summary>
        /// 邮件服务器未找到
        /// </summary>
        [Description("邮件服务器未找到")]
        EMAIL_SERVER_NO_FIND,
        /// <summary>
        /// 参数不能为空
        /// </summary>
        [Description("参数不能为空")]
        VALUE_CANNOT_BE_NULL,
        /// <summary>
        /// 值班时间未设置
        /// </summary>
        [Description("值班时间未设置")]
        WORK_TIME_CONF_NOT_SET,
        /// <summary>
        /// 已经存在
        /// </summary>
        [Description("该项已经存在")]
        ALREADY_EXIST,
        /// <summary>
        /// 非法的业务类型
        /// </summary>
        [Description("非法的业务类型")]
        INVALID_BUSSINESS_TYPE,
        /// <summary>
        /// 非法的执勤小时数据
        /// </summary>
        [Description("非法的执勤小时数据")]
        INVALID_TIMEITEM_FILE,
    }
}
