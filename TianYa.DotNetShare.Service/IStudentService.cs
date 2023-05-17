using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TianYa.DotNetShare.Model;

namespace TianYa.DotNetShare.Service
{
    /// <summary>
    /// 学生类服务层接口
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 根据学号获取学生信息
        /// </summary>
        /// <param name="stuNo">学号</param>
        /// <returns>学生信息</returns>
        Student GetStuInfo(string stuNo);
    }
}
