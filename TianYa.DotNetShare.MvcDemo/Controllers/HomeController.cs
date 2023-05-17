using System.Web.Mvc;
using TianYa.DotNetShare.Service;
using TianYa.DotNetShare.Repository;
using TianYa.DotNetShare.Repository.Impl;

namespace TianYa.DotNetShare.MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 定义仓储层学生实现类对象
        /// </summary>
        public StudentRepository StuRepositoryImpl { get; set; }

        /// <summary>
        /// 定义仓储层学生抽象类对象
        /// </summary>
        protected IStudentRepository StuRepository;

        /// <summary>
        /// 通过属性注入，访问修饰符必须为public，否则会注入失败
        /// </summary>
        public IStudentService StuService { get; set; }

        /// <summary>
        /// 通过构造函数进行注入
        /// 注意：参数是抽象类，而非实现类，因为已经在Global.asax中将实现类映射给了抽象类
        /// </summary>
        /// <param name="stuRepository">仓储层学生抽象类对象</param>
        public HomeController(IStudentRepository stuRepository)
        {
            this.StuRepository = stuRepository;
        }

        public ActionResult Index()
        {
            var stu1 = StuRepository.GetStuInfo("10000");
            var stu2 = StuService.GetStuInfo("10001");
            var stu3 = StuRepositoryImpl.GetStuInfo("10002");
            string msg = $"学号：10000，姓名：{stu1.Name}，性别：{stu1.Sex}，年龄：{stu1.Age}<br />";
            msg += $"学号：10001，姓名：{stu2.Name}，性别：{stu2.Sex}，年龄：{stu2.Age}<br />";
            msg += $"学号：10002，姓名：{stu3.Name}，性别：{stu3.Sex}，年龄：{stu3.Age}";
            return Content(msg);
        }
    }
}