using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvc.Models;

namespace TestMvc.Controllers
{
    public class HomeController : Controller
    {
        public DataProcess DataProcess = new DataProcess();

        public ActionResult Index()
        {
            //讀取列表資料
            List<ListViewModel> vm = DataProcess.GetListData();

            return View(vm);
        }

        public ActionResult Detail(string id)
        {
            //讀取商品資料
            DetailViewModel vm = DataProcess.GetDetailData(id);

            return View("Detail",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(string[] checkbox)
        {
            //更新訂單狀態
            DataProcess.UpdateStatus(checkbox);

            return RedirectToAction("Index");
        }
    }
}