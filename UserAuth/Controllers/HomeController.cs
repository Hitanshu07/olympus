using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuth.Data.Core;
using UserAuth.Data.Entities;
using UserAuth.Data.Service;

namespace UserAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculationService _calculationService;
        private readonly IProcedureRoomAssocService _praService;
        private readonly IProcedureService _procedureService;
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public HomeController()
            : this(UserDbContext.Create())
        {
        }

        public HomeController(UserDbContext dbContext)
            : this(new CalculationService(dbContext), new ProcedureRoomAssocService(dbContext), new ProcedureService(dbContext), new UserService(dbContext), new RoomService(dbContext))
        {
        }

        public HomeController(ICalculationService claculationService, IProcedureRoomAssocService praService, IProcedureService procedureService, IUserService userService, IRoomService roomservice)
        {
            _calculationService = claculationService;
            _praService = praService;
            _userService = userService;
            _procedureService = procedureService;
            _roomService = roomservice;
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }
        public ActionResult ChooseOption()
        {
            ViewBag.Title = "Login Page";

            return View();
        }
        public ActionResult calculator()
        {
            ViewBag.Title = "Calculator";

            return View();
        }

        public ActionResult NewUser()
        {
            ViewBag.Title = "New USer";

            return View();
        }

        public ActionResult EndoscopicSuite(int id)
        {
            ViewBag.Title = "Endoscopic Suite";
            ViewBag.id = id;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddUser(User user)
        {
           var newuser = _userService.Add(user);
            ViewBag.Title = "Home Page";

            return RedirectToAction("EndoscopicSuite", "Home", new { id = newuser.UserId });
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateEndoscopySuite(User user)
        {
            var olduser = _userService.Find(user.UserId);
            olduser.EndocscopySuites = user.EndocscopySuites;

            var newuser = _userService.update(olduser);
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
